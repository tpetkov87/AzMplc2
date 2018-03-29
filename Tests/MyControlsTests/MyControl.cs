using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Sitefinity.Services;
using System.Web.Caching;
using System.ComponentModel;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SitefinityWebApp.MyControls
{
    public class MyControl : Literal
    {
        public string Prop1
        {
            get;
            set;
        }

        public string Prop2
        {
            get;
            set;
        }

        public string Prop3
        {
            get;
            set;
        }

        public string Prop4
        {
            get;
            set;
        }

        public string Prop5
        {
            get;
            set;
        }

        public string Prop6
        {
            get
            {
                return this.prop6;
            }
            set
            {
                this.prop6 = value;
            }
        }

        public string Prop7
        {
            get
            {
                return this.prop7;
            }
            set
            {
                this.prop7 = value;
            }
        }

        public string Prop8
        {
            get
            {
                return this.prop8;
            }
            set
            {
                this.prop8 = value;
            }
        }

        public bool BoolProp
        {
            get;
            set;
        }

        public int IntProp
        {
            get;
            set;
        }

        [TypeConverter(typeof(GenericCollectionConverter))]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        public IList<DummyViewDefinition> MyList
        {
            get
            {
                if (this.myList == null)
                {
                    this.myList = new List<DummyViewDefinition>();
                    this.myList.Add(new DummyViewDefinition() { ViewName = "1" });
                }
                return this.myList;

            }
        }
        private IList<DummyViewDefinition> myList;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DummyControlDefinition Definition
        {
            get
            {
                if (this.definition == null)
                {
                    this.definition = new DummyControlDefinition();
                }
                return this.definition;

            }
        }

        private DummyControlDefinition definition;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this.IsBackend())
            {
                SystemManager.CurrentHttpContext.Response.AddCacheDependency(new CacheDependency(this.Context.Server.MapPath("~/test.txt")));
            }

            this.Text = String.Format(@"<br />
Prop1 = {0}<br />
Prop2 = {1}<br />
Prop3 = {2}<br />
Prop4 = {3}<br />
Prop5 = {4}<br />
Prop6 = {5}<br />
Prop7 = {6}<br />
Prop8 = {7}<br />
BoolProp = {8}<br />
IntProp = {9}<br />

", this.Prop1, this.Prop2, this.Prop3, this.Prop4, this.Prop5, this.Prop6, this.Prop7 ?? "NULL", this.Prop8 ?? "NULL", this.BoolProp, this.IntProp);
            this.Text += "Current time:" + DateTime.Now.ToLongTimeString();
        }

        private string prop6 = "default6";
        private string prop7 = null;
        private string prop8 = string.Empty;
    }

    public class DummyControlDefinition
    {
        public DummyControlDefinition()
        {
            this.ContentType = typeof(Telerik.Sitefinity.News.Model.NewsItem);
        }

        public virtual string ProviderName
        {
            get;
            set;
        }

        public virtual bool? UseWorkflow
        {
            get;
            set;
        }

        public virtual Type ContentType
        {
            get;
            set;
        }


        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        public virtual DummyViewDefinitionCollection Views
        {
            get
            {
                if (this.views == null)
                {
                    this.views = new DummyViewDefinitionCollection();
                    this.views.Add(new DummyListViewDefinition() { ViewName = "ListView", ItemsPerPage = 20 });
                    this.views.Add(new DummyDetailsViewDefinition() { ViewName = "DetailsView" });
                }
                return this.views;
            }
        }

        private DummyViewDefinitionCollection views;
    }

    [DefaultProperty("ViewName")]
    public class DummyViewDefinition
    {
        public string ViewName
        {
            get;
            set;
        }

        public string TemplateName { get; set; }

        public string TemplatePath { get; set; }

        public bool IsMasterView { get; internal set; }

        public bool? UseWorkflow { get; set; }

        public Type ViewType { get; set; }
    }

    public class DummyListViewDefinition : DummyViewDefinition
    {
        public DummyListViewDefinition()
        {
            this.IsMasterView = true;
        }

        public int? ItemsPerPage
        {
            get;
            set;
        }

        public string ControlId
        {
            get;
            set;
        }
    }

    public class DummyDetailsViewDefinition : DummyViewDefinition
    {
        public string FieldCssClass
        {
            get;
            set;
        }
    }

    public class DummyViewDefinitionCollection : KeyedCollection<string, DummyViewDefinition>
    {
        protected override string GetKeyForItem(DummyViewDefinition item)
        {
            return item.ViewName;
        }
    }
}