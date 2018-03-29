using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DataGenerator.MediaContentHierarchyGenerator;
using Telerik.Sitefinity.DataGenerator.MediaContentHierarchyGenerator.Configuration;
using Telerik.Sitefinity.DataGenerator.MediaContentHierarchyGenerator.Images;

namespace SitefinityWebApp.Tools
{
    public partial class ContentGeneratorTestPage : System.Web.UI.Page
    {
        
        public ImageHierarchyGeneratorConfig Configuration = null;
        public MediaContentHierarchyGeneratorBase Generator = null;

        public static readonly int AlbumsCount = 8;
        public static readonly string FoldersCountPerLevel = "{8, 8, 8}";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.Configuration = new ImageHierarchyGeneratorConfig(new ConfigSection(null));

            this.Configuration.GeneralAlbumName = "Album";
            this.Configuration.GeneralFolderName = "Folder";
            this.Configuration.GeneralImageName = "Image";
            this.Configuration.ContentLibrariesCount = AlbumsCount;
            this.Configuration.ChildFoldersHierarchyElementsCount = FoldersCountPerLevel;

            this.Generator = new ImageHierarchyGenerator(this.Configuration);
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            this.Generator.GenerateLibrariesHierarchy();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            var createdItems = this.Generator.CreatedItems;

            foreach(var id in createdItems)
            {
                this.Generator.Manager.DeleteItem(id);
            }

            this.Generator.Manager.SaveChanges();
        }
    }
}