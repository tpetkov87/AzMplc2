using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace SitefinityWebApp.CustomTests
{
    public partial class ControlPropertiesTest : System.Web.UI.Page
    {
        //private void ManagePagesPersistance()
        //{
        //    var manager = PageManager.GetManager();
        //    foreach (var page in manager.GetPageDataList())
        //    {
        //        ManageControlsPersistance(manager, page.Controls);
                
        //        // Manage drafts' controls
        //        foreach (var draft in page.Drafts)
        //        {
        //            ManageControlsPersistance(manager, draft.Controls);
        //        }
        //    }
        //}

        //private void ManageControlsPersistance(PageManager manager, IEnumerable<ObjectData> controls)
        //{
        //    foreach (var control in controls)
        //    {
        //        ManagePropertiesPersistance(manager, control.Properties);
        //    }
        //}

        //private void ManagePropertiesPersistance(PageManager manager, IEnumerable<ControlProperty> properties)
        //{
        //    foreach (var property in properties)
        //    {
        //        if (property.Value != null)
        //        {
        //            // this is stringable property
        //        }
        //        else if (property.ChildProperties.Count > 0)
        //        { 
        //            // this is a complex property which has child properties
        //            ManagePropertiesPersistance(manager, property.ChildProperties); 
        //        }
        //        else if (property.ListItems.Count > 0)
        //        { 
        //            // this is a collection property
        //            ManageControlsPersistance(manager, property.ListItems); 
        //        }
        //    }
        //}




        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var pageManager = PageManager.GetManager();
            var pageTitle = TextBox1.Text;
            
            var page = pageManager.GetPageDataList().Where(x => x.NavigationNode.UrlName == pageTitle).FirstOrDefault();
            if (page != null)
            {
                var strBuilder = new StringBuilder();

                WriteControls(strBuilder, page);

                foreach (var draft in page.Drafts)
                {
                    WriteControls(strBuilder, draft);
                }

                Literal1.Text = strBuilder.ToString();
            }
            else
            {
                Literal1.Text = "No standard page found with this URL name: " + pageTitle;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Context.Server.ScriptTimeout = 600;

            var task = new FixControlPropertiesTask();
            task.ExecuteTask();

            //var pageManager = PageManager.GetManager();

            //this.BulkUpdateItems(
            //    pageManager,
            //    pageManager.GetPageDataList(),
            //    i => 
            //    {
            //        this.PersistPageControls(pageManager, i);
            //        pageManager.Provider.FlushTransaction();
            //    }, 
            //    commitOnPage: true);
            //pageManager.SaveChanges();

        }

        private void PersistPageControls(PageManager manager, PageData page)
        {
            PersistControls(manager, page.Controls);

            foreach (var draft in page.Drafts)
            {
                PersistControls(manager, draft.Controls);
            }
        }

        private void PersistControls(PageManager manager, IEnumerable<ControlData> controls)
        {
            foreach (var control in controls)
            {
                var component = manager.LoadControl(control);
                var list = new List<ControlProperty>(control.Properties);
                foreach (var prop in list)
                    manager.Delete(prop);
                control.Properties.Clear();
                manager.ReadProperties(component, control);
            }
        }

        private void WriteControls(StringBuilder strBuilder, IControlsContainer controlsContainer)
        {
            string title;
            if (controlsContainer is PageData)
            {
                title = "Live";
            }
            else if (controlsContainer is PageDraft)
            {
                title = ((PageDraft)controlsContainer).IsTempDraft ? "Temp" : "Draft";
            }
            else
            {
                title = controlsContainer.GetType().Name;
            }

            strBuilder.AppendFormat("<h2>{0}</h2>", title);
            if (controlsContainer.Controls.Any())
            {
                strBuilder.Append("<ul>");
                foreach (var control in controlsContainer.Controls)
                {
                    strBuilder.Append("<li>");
                    WriteControlProperties(strBuilder, control.ObjectType, control.Properties);
                    strBuilder.Append("</li>");
                }
                strBuilder.Append("</ul>");
            }
            else
            {
                strBuilder.Append("<p>No controls</p>");
            }
        }

        private void WriteControlProperties(StringBuilder strBuilder, string title, IEnumerable<ControlProperty> properties, int level = 0)
        {
            strBuilder.AppendFormat("<h3>{0}</h3>", title);
            strBuilder.Append("<ul>");
            foreach (var property in properties)
            {
                strBuilder.Append("<li>");
                WriteProperty(strBuilder, property, level);
                strBuilder.Append("</li>");
            }
            strBuilder.Append("</ul>");
        }

        private void WriteProperty(StringBuilder strBuilder, ControlProperty property, int level)
        {
            if (property.Value == null)
            {
                if (property.ChildProperties.Count > 0)
                {
                    WriteControlProperties(strBuilder, this.GetPropertyName(property, level), property.ChildProperties, ++level);
                }
                else if (property.ListItems.Count > 0)
                {
                    strBuilder.AppendFormat("<h3>{0}</h3>", this.GetPropertyName(property, level));
                    strBuilder.Append("<ul>");
                    int index = 0;
                    foreach (var obj in property.ListItems)
                    {
                        strBuilder.Append("<li>");
                        WriteControlProperties(strBuilder, string.Format("{0} ({1})", obj.ObjectType, index), obj.Properties, ++level);
                        strBuilder.Append("</li>");
                        index++;
                    }
                    strBuilder.Append("</ul>");
                }
                else
                {
                    strBuilder.AppendFormat("<b style=\"color:red\">{0} has no value</b>", this.GetPropertyName(property, level));
                }
            }
            else
                strBuilder.AppendFormat("{0} = {1}", this.GetPropertyName(property, level), property.Value);
        }

        private string GetPropertyName(ControlProperty property, int level)
        {
            if (level == 0 && !string.IsNullOrEmpty(property.Language))
                return string.Format("{0} [<b style=\"color:green\">{1}</b>]", property.Name, property.Language);
            return property.Name;
        }

        private void BulkUpdateItems<TItem>(IManager manger, IEnumerable<TItem> items, Action<TItem> setAction, int pageSize = 20, bool commitOnPage = false)
        {
            var itemsCount = items.Count();

            var pagesCount = 0;
            if (itemsCount > 0)
            {
                pagesCount = (int)Math.Ceiling((double)itemsCount / (double)pageSize);
            }
            for (var index = 0; index < pagesCount; index++)
            {
                var pageItems = items.Skip(index * pageSize).Take(pageSize);
                foreach (var item in pageItems)
                {
                    setAction(item);
                }
                if (manger.TransactionName.IsNullOrEmpty())
                {
                    if (commitOnPage)
                        manger.SaveChanges();
                    else
                        manger.Provider.FlushTransaction();
                }
                else
                {
                    if (commitOnPage)
                        TransactionManager.CommitTransaction(manger.TransactionName);
                    else
                        TransactionManager.FlushTransaction(manger.TransactionName);
                }
            }
        }


    }
}