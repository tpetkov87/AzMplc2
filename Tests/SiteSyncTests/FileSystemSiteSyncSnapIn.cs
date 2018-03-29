using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Configuration;

// NOTE: Uncomment the following line to make the snap-in install itself on application start.
// [assembly: System.Web.PreApplicationStartMethod(typeof(SitefinityWebApp.FileSystemSiteSyncSnapIn), "PreApplication_Start")]

namespace SitefinityWebApp
{
    public class FileSystemSiteSyncSnapIn : ISiteSyncSnapIn
    {
        #region Installation

        public static void PreApplication_Start()
        {
            SiteSyncModule.Initialized += SiteSyncModule_Initialized;
        }

        static void SiteSyncModule_Initialized(object sender, EventArgs e)
        {
            var sync = App.WorkWith().SiteSync();

            string resType = "FS.Resources";
            string resPath = SystemManager.CurrentHttpContext.Server.MapPath("~/App_Data/Sitefinity/GlobalResources");

            var snapIn = ObjectFactory.Container.Resolve<FileSystemSiteSyncSnapIn>(
                new ParameterOverride("typeKey", resType),
                new ParameterOverride("dir", resPath));

            sync.SnapIns().Register(resType, "Resources", snapIn);
        }

        #endregion

        public virtual string SupportedType { get; set; }

        public FileSystemSiteSyncSnapIn(string typeKey, string dir, ISiteSyncContext context) 
        {
            this.typeKey = typeKey;
            this.dir = dir;
            this.context = context;
        }

        public IQueryable<ISiteSyncLogEntry> GetPendingItems(ISiteSyncExportContext context)
        {
            return this.GetEntries().AsQueryable();
        }

        public int GetExportItemsCount(ISiteSyncExportContext context, Guid? siteId = null)
        {
            return this.GetEntries().Count();
        }

        public IEnumerable<ISiteSyncExportTransaction> Export(ISiteSyncExportContext context)
        {
            foreach (var entry in this.GetEntries())
            {
                dynamic item = new WrapperObject(null);

                //TODO: item.objectTypeId, Action
                item.FilePath = entry.ItemId;
                item.Hash = entry.AdditionalInfo;

                // NOTE: While Site Sync does support binary transfer, used for MediaContent sync, its API is still internal,
                // so we need to take a less performant approach here, using text encoding.

                //TODO: item.Data - load and serialize the data

                var transaction = ObjectFactory.Resolve<ISiteSyncExportTransaction>();

                transaction.Type = this.typeKey;
                transaction.LogEntry = entry;
                transaction.Items = new WrapperObject[] { item };

                yield return transaction;
            }
        }

        public void Import(ISiteSyncImportTransaction transaction)
        {
            // TODO: Implement this method
        }

        private IEnumerable<ISiteSyncLogEntry> GetEntries()
        {
            // HINT: To improve performance, implement traversing the directory only the first time
            // this method is called (needed as changes might have been made while the process was not running)
            // and start a FileSystemWatcher to track any further changes.

            // HINT: Also consider using Directory.EnumerateFiles,
            // saving the changes after processing a certain amount of files.
            var files = Directory.GetFiles(this.dir, "*", SearchOption.AllDirectories);

            var dataStore = this.context.GetDataStore();

            // TODO: set all items' Action property to DataEventAction.Deleted but not Modified

            // WARNING: Marking everything for delete beforehand would be too risky in a production code,
            // as a failure to mark them as existing would cause their subsequent deletion on the target server.
            // This approach is chosen here to simplify the example code.

            var entries = new ISiteSyncLogEntry[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                entries[i] = this.CreateOrUpdateFileEntry(files[i], dataStore);
            }

            dataStore.SaveChanges();

            // TODO: if everything seems OK (no exceptions) set Modfieid = true for the Deleted ones

            return entries;
        }

        private ISiteSyncLogEntry CreateOrUpdateFileEntry(string filePath, ISiteSyncDataStore dataStore)
        {
            string fileId = filePath.Remove(0, this.dir.Length + 1);
            string serverId = Config.Get<SiteSyncConfig>().ReceivingServers.Elements.First().ServerId; //TODO: enumerate all servers;

            string currentHash = this.ComputeHash(filePath);

            var entry = dataStore.GetLogEntries()
                .SingleOrDefault(e => e.ServerId == serverId && e.TypeName == this.typeKey && e.ItemId == fileId);

            bool update = false;

            if (entry == null)
            {
                entry = dataStore.CreateLogEntry();

                entry.ServerId = serverId;
                entry.TypeName = this.typeKey;
                entry.ItemId = fileId;
                entry.Title = fileId;

                entry.ItemAction = DataEventAction.Created;

                update = true;
            }
            else if (currentHash != entry.AdditionalInfo)
            {
                entry.ItemAction = DataEventAction.Updated;
                update = true;
            }
            else
            {
                entry.ItemAction = DataEventAction.Updated; // to overwrite the previously set "deleted" action
            }

            if (update)
            {
                entry.ModifiedSinceLastSync = true;
                entry.Timestamp = DateTime.UtcNow;
                entry.AdditionalInfo = currentHash;
            }

            return entry;
        }

        private string ComputeHash(string f)
        {
            byte[] hashBytes;

            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(f))
                hashBytes = md5.ComputeHash(stream);

            var hashStr = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                hashStr.AppendFormat("{0:X2}", hashBytes[i]);

            return hashStr.ToString();
        }

        private readonly string typeKey;
        private readonly string dir;
        private readonly ISiteSyncContext context;
    }
}