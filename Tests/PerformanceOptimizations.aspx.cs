using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace SitefinityWebApp.Tests
{
    public partial class PerformanceOptimizations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_ChangeDynModulesConn_Click(object sender, EventArgs e)
        {
            ChangeDynModulesConnectionString("Sitefinity_ModuleBuilder");
        }

        private void ChangeDynModulesConnectionString(string dynamicModulesConnectionString)
        { 
            var manager = ConfigManager.GetManager();

            InstallConnectionString(manager, dynamicModulesConnectionString, DataConfig.DefaultConnectionName);
            var configs = new List<ModuleConfigBase>();
            configs.Add(manager.GetSection("moduleBuilderConfig") as ModuleConfigBase);
            configs.Add(manager.GetSection("dynamicModulesConfig") as ModuleConfigBase);
            foreach (var config in configs)
            { 
                TryChangeConnectionString(config.Providers.Values, dynamicModulesConnectionString, DataConfig.DefaultConnectionName);
                manager.SaveSection(config);
            }
        }

        private void InstallConnectionString(ConfigManager manager, string newConnectionStringName, string oldConnectionStringName, string databaseSufix = null)
        {
            var dataConfig = manager.GetSection<DataConfig>();
            
            IConnectionStringSettings settings;
            
            if (!dataConfig.TryGetConnectionString(newConnectionStringName, out settings) &&
                dataConfig.TryGetConnectionString(oldConnectionStringName, out settings))
            {
                var defaultConnection = new SqlConnectionStringBuilder(settings.ConnectionString);
                if (!databaseSufix.IsNullOrEmpty())
                {
                    defaultConnection.InitialCatalog += databaseSufix;
                }

                ConnStringSettings connStringSetting = new ConnStringSettings(dataConfig.ConnectionStrings);
                connStringSetting.Name = newConnectionStringName;
                connStringSetting.ConnectionString = defaultConnection.ToString();
                dataConfig.ConnectionStrings.Add(connStringSetting);
                manager.SaveSection(dataConfig);
            }
        }

        private void TryChangeConnectionString(IEnumerable<DataProviderSettings> providers, string newConnectionString, string currentConnectionString)
        {
            this.TryChangeConnectionString<IOpenAccessDataProvider>(providers, newConnectionString, currentConnectionString);
        }

        private void TryChangeConnectionString<TProvider>(IEnumerable<DataProviderSettings> providers, string newConnectionString, string currentConnectionString) where TProvider : IDataProviderBase
        {
            foreach (var provider in providers)
            {
                if (typeof(TProvider).IsAssignableFrom(provider.ProviderType))
                {
                    string connectionString = provider.Parameters["connectionString"];
                    if (connectionString.IsNullOrEmpty())
                        connectionString = DataConfig.DefaultConnectionName;

                    if (currentConnectionString == connectionString)
                        provider.Parameters["connectionString"] = newConnectionString;
                }
            }
        }

        protected void Button_VersioningSepration_Click(object sender, EventArgs e)
        {
            var manager = ConfigManager.GetManager();
            InstallConnectionString(manager, "Sitefinity_Versioning", DataConfig.DefaultConnectionName, "_Versioning");
            var config = manager.GetSection("versionConfig") as ModuleConfigBase;
            TryChangeConnectionString(config.Providers.Values, "Sitefinity_Versioning", DataConfig.DefaultConnectionName);
            manager.SaveSection(config);

        }

        protected void Button_BlobSeparation_Click(object sender, EventArgs e)
        {
            var manager = ConfigManager.GetManager();
            InstallConnectionString(manager, "Sitefinity_Blobs", DataConfig.DefaultConnectionName, "_Blobs");
            var config = manager.GetSection<LibrariesConfig>();
            TryChangeConnectionString<Telerik.Sitefinity.Modules.Libraries.BlobStorage.OpenAccessBlobStorageProvider>(config.BlobStorage.Providers.Values, "Sitefinity_Blobs", DataConfig.DefaultConnectionName);
            manager.SaveSection(config);

        }
    }
}