using System;
using System.Linq;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Scheduling;

namespace SitefinityWebApp
{
    public partial class LibrariesTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var urlName = libUrlName.Text.Trim();
            var newUrlName = libNewUrlName.Text.Trim();
            var newBlobStorage = libNewStorage.Text.Trim();

            var manager = LibrariesManager.GetManager();

            var album = manager.GetAlbums().Where(i => i.UrlName == urlName).FirstOrDefault();
            
            // Transfer
            if (!newBlobStorage.IsNullOrWhitespace())
                album.BlobStorageProvider = newBlobStorage;
            
            // Rename
            if (!newUrlName.IsNullOrWhitespace())
            {
                album.UrlName = newUrlName;
                manager.RecompileItemUrls(album);
                libUrlName.Text = newUrlName;

            }

            manager.SaveChanges();

            LibrariesManager.StartRelocateLibraryItemsTask(album.Id, manager.Provider.Name, LibraryRelocationTask.relocateModeTitle);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var urlName = libUrlName.Text.Trim();
            var manager = LibrariesManager.GetManager();
            var album = manager.GetAlbums().Where(i => i.UrlName == urlName).FirstOrDefault();

            var taskId = album.RunningTask;
            if (taskId != Guid.Empty)
            {
                Scheduler.Instance.StopTask(taskId);
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var urlName = libUrlName.Text.Trim();
            var manager = LibrariesManager.GetManager();
            var album = manager.GetAlbums().Where(i => i.UrlName == urlName).FirstOrDefault();

            var taskId = album.RunningTask;
            if (taskId != Guid.Empty)
            {
                Scheduler.Instance.RestartTask(taskId);
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var urlName = libUrlName.Text.Trim();
            var manager = LibrariesManager.GetManager();
            var album = manager.GetAlbums().Where(i => i.UrlName == urlName).FirstOrDefault();

            var taskStatus = "Completed";
            var taskId = album.RunningTask;
            if (taskId != Guid.Empty)
            {
                var schedulingManager = SchedulingManager.GetManager();
                var task = schedulingManager.GetTaskData().Where(t => t.Id == taskId).SingleOrDefault();
                if (task != null)
                {
                    taskStatus = task.Status == Telerik.Sitefinity.Scheduling.Model.TaskStatus.Started ? String.Format("{0} %", task.Progress) : task.Status.ToString();
                }
                else
                {
                    album = manager.GetAlbums().Where(i => i.UrlName == urlName).FirstOrDefault();
                    if (album.RunningTask != Guid.Empty)
                        taskStatus = "Opps!";
                }
            }
            progress.Text = taskStatus;
        }

        //protected void Button4_Click(object sender, EventArgs e)
        //{
        //    //var libToRename = this.libraryToRename.Text;
        //    //var libNewName = this.libraryNewName.Text;
        //    //var libraryManager = LibrariesManager.GetManager();

        //    //var library = libraryManager.GetAlbums().SingleOrDefault(a => a.UrlName == libToRename);

        //    var request = System.Net.WebRequest.Create("http://localhost:1733/images/default-album/formations-oak-creek-arizona.jpg") as System.Net.HttpWebRequest;
        //    var response = request.GetResponse() as System.Net.HttpWebResponse;
        //    using (var stream = response.GetResponseStream()) //new System.IO.MemoryStream())
        //    {
        //        //using (var source = response.GetResponseStream())
        //        //{
        //        //    source.CopyTo(stream);
        //        //}

        //        var fluent = Telerik.Sitefinity.App.WorkWith();
        //        var userUploadAlbum = fluent.Albums().Where(a => a.UrlName == "default-album").Get().SingleOrDefault();
        //        var newImage = fluent.Image().CreateNew().Do(
        //            image =>
        //            {
        //                image.Description = "Image Description";
        //                image.Author = "Image Author";
        //                image.Parent = userUploadAlbum;
        //                image.Title = "Image Title";
        //                image.UrlName = "image-title";
        //                image.Extension = ".jpg";
        //            })
        //        .CheckOut()
        //        .UploadContent(stream, ".jpg")
        //        .CheckIn()
        //        .Do(image => { image.ApprovalWorkflowState = "Published"; })
        //        .Publish()
        //        .SaveChanges();
        //    }

        //}
    }
}

namespace Sitefinity.Samples
{
    using System.IO;
    using System.Linq;
    using Telerik.Sitefinity.Configuration;
    using Telerik.Sitefinity.GenericContent.Model;
    using Telerik.Sitefinity.Libraries.Model;
    using Telerik.Sitefinity.Modules.Libraries;
    using Telerik.Sitefinity.Modules.Libraries.Configuration;

    public class LibrariesTest
    {
        /// <summary>
        /// Creates an album with the specified BLOB storage provider and uploads there the images from the specified path.
        /// </summary>
        /// <param name="rootFolderPath">The root folder path.</param>
        /// <param name="blobStorageProvider">The BLOB storage provider.</param>
        public static void CreateAlbumAndUploadImages(string rootFolderPath, string albumTitle = null, string blobStorageProvider = null)
        {           
            DirectoryInfo rootFolder = new DirectoryInfo(rootFolderPath);
            if (!rootFolder.Exists)
                throw new ApplicationException(string.Format("A folder with path '{0}' does not exist", rootFolder.FullName));

            albumTitle = albumTitle ?? rootFolder.Name;
            blobStorageProvider = blobStorageProvider ?? Config.Get<LibrariesConfig>().BlobStorage.DefaultProvider;

            var manager = LibrariesManager.GetManager();

            var albumName = albumTitle.ToLower().Replace(' ', '_');
            if (manager.GetAlbums().Where(i => i.UrlName == albumName).Any())
                throw new ApplicationException(string.Format("An album with UrlName '{0}' already exists", albumName));

            var album = manager.CreateAlbum();
            album.Title = albumTitle;
            album.UrlName = albumName;
            // Set blob storage provider for  this album
            // Currently available blob storage providers can be found in Config.Get<LibrariesConfig>().BlobStorage.Providers
            // or BlobStorageManager.ProvidersCollection
            album.BlobStorageProvider = blobStorageProvider;
            manager.RecompileItemUrls<Album>(album);

            foreach (var file in rootFolder.GetFiles())
            {
                var image = manager.CreateImage();

                var extension = file.Extension;
                var title = file.Name;
                if (extension.Length > 0)
                    title = title.Substring(0, title.Length - extension.Length);
                image.Parent = album;
                image.Title = title;
                image.UrlName = title.ToLower().Replace(' ', '-');
                manager.RecompileItemUrls<Telerik.Sitefinity.Libraries.Model.Image>(image);

                using (var fileStream = file.OpenRead())
                {
                    manager.Upload(image, fileStream, file.Extension);
                }
                manager.Publish(image);
            }
            manager.SaveChanges();

        }


        /// <summary>
        /// Change the album BLOB Storage Provider and begins transfering album images to the BLOB storage of the album.
        /// This might be a long time operation. If it doesn't finish for some reason, can be resumed.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <param name="newBlobStorageProvider">The new BLOB storage provider.</param>
        public static void BeginChangeAlbumBlobStorage(string albumName, string newBlobStorageProvider)
        {
            var manager = LibrariesManager.GetManager();

            var album = manager.GetAlbums().Where(i => i.UrlName == albumName).FirstOrDefault();
            album.BlobStorageProvider = newBlobStorageProvider;
            manager.SaveChanges();

            LibrariesManager.StartRelocateLibraryItemsTask(album.Id, manager.Provider.Name);
            

            //var imagesToTransfer = album.Images.Where(i => i.Status == ContentLifecycleStatus.Master && i.BlobStorageProvider != newBlobStorageProvider);
            //foreach (var image in imagesToTransfer)
            //{
            //    manager.TransferItemStorage(image, newBlobStorageProvider);
            //    var liveImage = manager.GetLive(image);
            //    if (liveImage != null)
            //        manager.TransferItemStorage(liveImage, newBlobStorageProvider);
            //    var tempImage = manager.GetTemp(image);
            //    if (tempImage != null)
            //        manager.TransferItemStorage(tempImage, newBlobStorageProvider);

            //    manager.SaveChanges();
            //}
        }

        /// <summary>
        /// Begins move album images to another album.
        /// If the target album use a different blob storage, the images will be transfered to the target album storage.
        /// In this case the operation could take a long time. If it doesn't finish for some reason, can be resumed.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <param name="newBlobStorageProvider">The new BLOB storage provider.</param>
        public static void BeginMoveAlbumImagesToAnotherAlbum(string sourceAlbumName, string targetAlbumName)
        {
            var manager = LibrariesManager.GetManager();

            var sourceAlbum = manager.GetAlbums().Where(i => i.UrlName == sourceAlbumName).FirstOrDefault();
            if (sourceAlbum != null)
            {
                var targetAlbum = manager.GetAlbums().Where(i => i.UrlName == targetAlbumName).FirstOrDefault();
                if (targetAlbum != null)
                {
                    var imagesToMove = sourceAlbum.Images().Where(i => i.Status == ContentLifecycleStatus.Master).ToList();
                    foreach (var image in imagesToMove)
                    {
                        manager.ChangeItemParent(image, targetAlbum, true);
                        manager.SaveChanges();
                    }
                }
            }
        }
    }
}
