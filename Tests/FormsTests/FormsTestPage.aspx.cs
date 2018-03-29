using System;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Model;
using Telerik.Web.Data;
using System.Web;

namespace SitefinityWebApp
{
    public partial class FormsTestPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PageManager.ConfigureScriptManager(
                this.Page,
                ScriptRef.MicrosoftAjax |
                ScriptRef.MicrosoftAjaxWebForms |
                ScriptRef.MicrosoftAjaxTemplates |
                ScriptRef.MicrosoftAjaxAdoNet |
                ScriptRef.JQuery |
                ScriptRef.JQueryValidate |
                ScriptRef.TelerikSitefinity);

            this.btnCreateFormDescription.Click += new EventHandler(btnCreateFormDescription_Click);
            this.btnCreateMetaType.Click += new EventHandler(btnCreateMetaType_Click);
            this.btnCreateFormEntry.Click += new EventHandler(btnCreateFormEntry_Click);
            this.btnGetFormDescriptionFields.Click += new EventHandler(btnGetFormDescriptionFields_Click);
            this.btnGoToEntriesList.Click += new EventHandler(btnGoToEntriesList_Click);
        }

        void btnGoToEntriesList_Click(object sender, EventArgs e)
        {
            Response.Redirect(VirtualPathUtility.ToAbsolute(String.Concat("~/Sitefinity/Content/Forms/entries/", this.formName.Text)));
        }

        void btnGetFormDescriptionFields_Click(object sender, EventArgs e)
        {
            //FormsManager.Get
        }

        void btnCreateMetaType_Click(object sender, EventArgs e)
        {
            //FormDescription formDescription = this.FormsManager.GetFormDescription(this.formName.Text);

            //MetaField textBoxMetaField = new MetaField();
            //textBoxMetaField.FieldName = "FirstName";
            //textBoxMetaField.DefaultValue = "";
            //textBoxMetaField.DBType = "VARCHAR";
            //textBoxMetaField.ClrType = typeof(string).FullName;

            //var formTextBox = new FormTextBox();
            //formTextBox.MetaField = textBoxMetaField;

            //FormControl formFieldControl = this.FormsManager.CreateControl<FormControl>(formTextBox, "bla");

            //formDescription.Controls.Add(formFieldControl);

            //this.FormsManager.BuildDynamicType(formDescription);
            //this.FormsManager.SaveChanges();
        }

        public FormsManager FormsManager
        {
            get
            {
                return FormsManager.GetManager();
            }
        }

        void btnCreateFormEntry_Click(object sender, EventArgs e)
        {
            //var rand = new Random();
            
            //FormDescription formDescription = this.FormsManager.GetFormDescription(this.formName.Text);
            //FormEntry entry = this.FormsManager.CreateFormEntry(String.Format("{0}.{1}", this.FormsManager.Provider.FormsNamespace, formDescription.Name));
            //entry.SetValue("FirstName", rand.Next().ToString());
            //var dummy = entry.LastModified;
            //this.FormsManager.SaveChanges();
        }

        void btnCreateFormDescription_Click(object sender, EventArgs e)
        {
            //var formDescription = this.FormsManager.CreateFormDescription(this.formName.Text);
            //formDescription.Title = this.formName.Text;
            //this.FormsManager.SaveChanges();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
