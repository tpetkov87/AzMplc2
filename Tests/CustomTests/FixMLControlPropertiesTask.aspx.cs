using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Tests.CustomTests
{
    public partial class ControlPropertiesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.siteList.SelectedIndexChanged += (s, args) => this.BindPages();
            this.execute.Click += execute_Click;
            this.report.Click += report_Click;
            if (!this.IsPostBack)
            {
                var multiSiteManager = MultisiteManager.GetManager();
                var allSites = multiSiteManager.GetSites();
                this.siteList.DataSource = allSites;
                this.siteList.DataBind();
                this.BindPages();
            }
        }

        void report_Click(object sender, EventArgs e)
        {
            var selectedPageIds = new List<Guid>();

            foreach (TreeNode checkedNode in this.pageTree.CheckedNodes)
                selectedPageIds.Add(Guid.Parse(checkedNode.Value));

            var siteRootId = Guid.Parse(this.siteList.SelectedValue);
            var site = MultisiteManager.GetManager().GetSites().FirstOrDefault(x => x.SiteMapRootNodeId == siteRootId);

            var xmlReport = this.GenerateReport(site.Id, selectedPageIds);

            if (xmlReport != null)
            {
                var htmlReport = new XDocument();
                using (XmlWriter writer = htmlReport.CreateWriter())
                {
                    // Load the style sheet.
                    var xslt = new XslCompiledTransform();
                    xslt.Load(XmlReader.Create(new StringReader(xsl)));

                    // Execute the transform and output the results to a writer.
                    xslt.Transform(xmlReport.CreateReader(), writer);
                }
                this.htmlContainer.InnerHtml = htmlReport.ToString();
            }
        }

        void execute_Click(object sender, EventArgs e)
        {
            var selectedPageIds = new List<Guid>();

            foreach (TreeNode checkedNode in this.pageTree.CheckedNodes)
                selectedPageIds.Add(Guid.Parse(checkedNode.Value));

            var siteRootId = Guid.Parse(this.siteList.SelectedValue);
            var site = MultisiteManager.GetManager().GetSites().FirstOrDefault(x => x.SiteMapRootNodeId == siteRootId);

            var task = new FixMultilingualSiteTask(site.Id);
            task.OnValidate += (s, args) =>
            {
                var provider = PageManager.GetManager().Provider;
                var deletedNode = args.DirtyItems.OfType<PageNode>().Where(x => provider.GetDirtyItemStatus(x) == SecurityConstants.TransactionActionType.Deleted).FirstOrDefault();
                if (deletedNode != null)
                {
                    args.Cancel = true;
                    var message = "Deletion of page node : {0} - {1}. Transaction is aborted.".Arrange(deletedNode.Title, deletedNode.Id);
                    this.errorMessage.InnerHtml = "<h2 style=\"color:red\">{0}</h2>".Arrange(message);
                }
            };
            task.IncludedPages = selectedPageIds;
            task.ExecuteTask();
        }

        void BindPages()
        {
            var pageManager = PageManager.GetManager();
            var rootId = Guid.Parse(siteList.SelectedValue);
            this.pageTree.Nodes.Clear();

            var pageNodes = pageManager.GetPageNodes().Where(x => x.ParentId == rootId).OrderBy(x => x.Ordinal);

            foreach (var pageNode in pageNodes)
            {
                var treeNode = new TreeNode()
                {
                    Text = pageNode.Title,
                    Value = pageNode.Id.ToString(),
                    ShowCheckBox = true
                };
                this.pageTree.Nodes.Add(treeNode);
                PopulateHierarchy(treeNode, pageNode);
            }

            var siteManager = MultisiteManager.GetManager();
            var site = siteManager.GetSites().FirstOrDefault(x => x.Name == siteList.SelectedItem.Text);
            if (site != null && site.CultureKeys.Count > 1)
                this.report.Visible = true;
            else
                this.report.Visible = false;
        }

        void PopulateHierarchy(TreeNode node, PageNode pageNode)
        {
            var childNodes = pageNode.Nodes.OrderBy(x => x.Ordinal);
            foreach (var childNode in childNodes)
            {
                var treeNode = new TreeNode()
                {
                    Text = childNode.Title,
                    Value = childNode.Id.ToString(),
                    ShowCheckBox = true
                };
                node.ChildNodes.Add(treeNode);
                PopulateHierarchy(treeNode, childNode);
            }
        }

        #region Generate Report

        private XDocument GenerateReport(Guid siteId, IList<Guid> includedPages)
        {
            var task = new FixMultilingualSiteTask(siteId);
            task.IncludedPages = includedPages;
            XDocument result = null;
            var multisiteManager = MultisiteManager.GetManager();
            var site = multisiteManager.GetSite(siteId);

            if (site.CultureKeys.Count > 1)
            {
                task.OnValidate += (s, args) =>
                {
                    args.Cancel = true;
                    var dirtyProperties = args.DirtyItems.OfType<ControlProperty>().Where(x => !x.Language.IsNullOrEmpty());
                    var allElements = new List<XElement>();
                    foreach (var prop in dirtyProperties)
                    {
                        var prevValue = args.Provider.GetOriginalValue<string>(prop, "Value");
                        if (prevValue != prop.Value && args.Provider.GetDirtyItemStatus(prop) == SecurityConstants.TransactionActionType.Updated)
                            this.GenHierarchy(prop, allElements);
                    }
                    var root = allElements.FirstOrDefault(x => x.Name == "pages");
                    result = new XDocument(root);
                };
                task.ExecuteTask();
            }
            return result;
        }

        private void GenHierarchy(ControlProperty prop, List<XElement> allElements, XElement element = null)
        {
            if (element == null)
            {
                element = allElements.FirstOrDefault(x => x.Attribute("Id").Value == prop.Id.ToString());
                if (element == null)
                {
                    var provider = PageManager.GetManager().Provider;
                    var originalValue = provider.GetOriginalValue<string>(prop, "Value");
                    element = new XElement("prop",
                        new XAttribute("Id", prop.Id.ToString()),
                        new XAttribute("name", prop.Name),
                        new XAttribute("lang", prop.Language ?? "NULL"),
                        new XAttribute("lastModified", prop.LastModified));
                    if (!(originalValue.IsNullOrEmpty() && prop.Value.IsNullOrEmpty()))
                    {
                        element.Add(new XElement("new", prop.Value));
                        element.Add(new XElement("old", originalValue));
                    }
                    allElements.Add(element);
                }
            }
            if (prop.ParentProperty != null)
            {
                var parentProp = prop.ParentProperty;
                var parentPropElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == parentProp.Id.ToString());
                if (parentPropElement == null)
                {
                    parentPropElement = new XElement("prop",
                        new XAttribute("name", parentProp.Name),
                        new XAttribute("Id", parentProp.Id.ToString()),
                        new XAttribute("lang", parentProp.Language ?? "NULL"),
                        new XAttribute("lastModified", parentProp.LastModified));
                    allElements.Add(parentPropElement);
                }
                if (element.Parent == null)
                    parentPropElement.Add(element);
                this.GenHierarchy(parentProp, allElements);
            }
            else if (prop.Control != null)
            {
                var control = prop.Control;
                var controlElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == control.Id.ToString());
                if (controlElement == null)
                {
                    controlElement = new XElement("control",
                        new XAttribute("Id", control.Id.ToString()),
                        new XAttribute("type", control.ObjectType));
                    var controlData = control as ControlData;
                    if (controlData != null)
                        controlElement.Add(new XAttribute("caption", controlData.Caption));
                    else
                        controlElement.Add(new XAttribute("caption", "NULL"));
                    allElements.Add(controlElement);
                }
                controlElement.Add(element);
                this.GenHierarchy(control, allElements, controlElement);
            }
        }

        private void GenHierarchy(ObjectData control, List<XElement> allElements, XElement controlElement)
        {
            if (control.ParentProperty != null)
            {
                var parentProp = control.ParentProperty;
                var parentPropElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == parentProp.Id.ToString());
                if (parentPropElement == null)
                {
                    parentPropElement = new XElement("prop",
                        new XAttribute("name", parentProp.Name),
                        new XAttribute("Id", parentProp.Id.ToString()),
                        new XAttribute("lang", parentProp.Language ?? "NULL"),
                        new XAttribute("lastModified", parentProp.LastModified));
                    allElements.Add(parentPropElement);
                }
                if (controlElement.Parent == null)
                    parentPropElement.Add(controlElement);
                this.GenHierarchy(parentProp, allElements, parentPropElement);
            }
            else
            {
                var pageControl = control as PageControl;
                if (pageControl != null)
                {
                    var pageData = pageControl.Page;
                    var pageDataElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == pageData.Id.ToString());
                    if (pageDataElement == null)
                    {
                        pageDataElement = new XElement("pageData",
                            new XAttribute("Id", pageData.Id),
                            new XAttribute("lang", pageData.Culture ?? "NULL"),
                            new XElement("controls"),
                            new XElement("drafts"));
                        allElements.Add(pageDataElement);
                        var pageNode = pageData.NavigationNode;
                        var pageNodeElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == pageNode.Id.ToString());
                        if (pageNodeElement == null)
                        {
                            var editUrl = GetUrl(pageNode, "Edit");
                            var previewUrl = GetUrl(pageNode, "Preview");
                            pageNodeElement = new XElement("pageNode", new XAttribute("Id", pageNode.Id), new XAttribute("Title", pageNode.Title), new XAttribute("Editurl", editUrl), new XAttribute("Previewurl", previewUrl));
                            var rootElement = allElements.FirstOrDefault(x => x.Name == "pages");
                            if (rootElement == null)
                            {
                                rootElement = new XElement("pages", new XAttribute("Id", string.Empty));
                                allElements.Add(rootElement);
                            }

                            rootElement.Add(pageNodeElement);
                            allElements.Add(pageNodeElement);
                        }
                        pageNodeElement.Add(pageDataElement);
                    }

                    if (controlElement.Parent == null)
                        pageDataElement.Element("controls").Add(controlElement);
                }
                var draftControl = control as PageDraftControl;
                if (draftControl != null)
                {
                    var pageDraft = draftControl.Page;
                    var pageDraftElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == pageDraft.Id.ToString());
                    if (pageDraftElement == null)
                    {
                        pageDraftElement = new XElement("pageDraft", new XAttribute("Id", pageDraft.Id), new XAttribute("type", pageDraft.IsTempDraft ? "Temp" : "Master"), new XElement("controls"));
                        allElements.Add(pageDraftElement);
                        var pageData = pageDraft.ParentPage;
                        var pageDataElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == pageData.Id.ToString());
                        if (pageDataElement == null)
                        {
                            pageDataElement = new XElement("pageData",
                                new XAttribute("Id", pageData.Id),
                                new XAttribute("lang", pageData.Culture ?? "NULL"),
                                new XElement("controls"),
                                new XElement("drafts"));
                            allElements.Add(pageDataElement);
                            var pageNode = pageData.NavigationNode;
                            var pageNodeElement = allElements.FirstOrDefault(x => x.Attribute("Id").Value == pageNode.Id.ToString());
                            if (pageNodeElement == null)
                            {
                                var editUrl = GetUrl(pageNode, "Edit");
                                var previewUrl = GetUrl(pageNode, "Preview");
                                pageNodeElement = new XElement("pageNode", new XAttribute("Id", pageNode.Id), new XAttribute("Title", pageNode.Title), new XAttribute("Editurl", editUrl), new XAttribute("Previewurl", previewUrl));
                                var rootElement = allElements.FirstOrDefault(x => x.Name == "pages");
                                if (rootElement == null)
                                {
                                    rootElement = new XElement("pages", new XAttribute("Id", string.Empty));
                                    allElements.Add(rootElement);
                                }

                                rootElement.Add(pageNodeElement);
                                allElements.Add(pageNodeElement);
                            }
                            pageNodeElement.Add(pageDataElement);
                        }
                        pageDataElement.Element("drafts").Add(pageDraftElement);
                    }
                    if (controlElement.Parent == null)
                        pageDraftElement.Element("controls").Add(controlElement);
                }
            }
        }

        private string GetUrl(PageNode pageNode, string action)
        {
            var resolver = ObjectFactory.Resolve<SiteMapProviderResolver>();
            var provider = resolver.GetSiteMapProviderForPageNode(pageNode);
            var siteNode = provider.FindSiteMapNodeFromKey(pageNode.Id.ToString()) as PageSiteNode;
            var site = (SystemManager.CurrentContext as IMultisiteContext).GetSiteBySiteMapRoot(pageNode.RootNodeId);

            var paramString = "/Action/" + action;
            if (site.PublicCultures.Count > 1)
                paramString = paramString + "/" + site.DefaultCulture;

            var url = pageNode.GetUrl(CultureInfo.GetCultureInfo(site.DefaultCulture)) + paramString;
            url = string.Concat(url, "?sf_site=" + site.Id.ToString() + "&sf_site_temp=true");
            return UrlPath.ResolveUrl(url);
        }



        private string xsl = @"
<xsl:transform version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">

<xsl:template match=""pages"">
    <ul>
        <xsl:for-each select=""pageNode"">
            <li>
                PageNode[<xsl:value-of select=""@Title"" />, <xsl:value-of select=""@Id"" />]
                <div>
                    <xsl:element name=""a"">
                      <xsl:attribute name=""href"">
                        <xsl:value-of select=""@Editurl"" />
                      </xsl:attribute>
                      <xsl:attribute name=""target"">
                        _blank
                      </xsl:attribute>
                      Edit
                    </xsl:element>
                    
                    <xsl:element name=""a"">
                      <xsl:attribute name=""href"">
                        <xsl:value-of select=""@Previewurl"" />
                      </xsl:attribute>
                      <xsl:attribute name=""target"">
                        _blank
                      </xsl:attribute>
                      Preview
                    </xsl:element>
                </div>
                <ul>
                    <xsl:for-each select=""pageData"">
                        <li>
                            <span>PageData[<xsl:value-of select=""@Id"" />, <xsl:value-of select=""@lang"" />]</span>
                            <ul>
                                <xsl:apply-templates select=""controls/control"" />
                            </ul>
                            <ul>
                                <xsl:for-each select=""drafts/pageDraft"">
                                    <li>
                                        <span>PageDraft[<xsl:value-of select=""@Id"" />, <xsl:value-of select=""@type"" />]</span>
                                        <ul>
                                            <xsl:apply-templates select=""controls/control"" />
                                        </ul>
                                    </li>
                                </xsl:for-each>
                            </ul>
                        </li>
                    </xsl:for-each>
                </ul>
            </li>
        </xsl:for-each>
    </ul>
</xsl:template>
<xsl:template match=""control"">
    <li>
        <span>Control[<xsl:value-of select=""@Id"" />, <xsl:value-of select=""@caption"" />, <xsl:value-of select=""@type"" />]</span>
        <ul>
            <xsl:apply-templates select=""prop"" />
        </ul>
    </li>
</xsl:template>
<xsl:template match=""prop"">
    <li>
        <span>Prop[<xsl:value-of select=""@Id"" />, <xsl:value-of select=""@name"" />, <xsl:value-of select=""@lang"" />, <xsl:value-of select=""@lastModified"" />]</span>
        <p><b><xsl:value-of select=""new"" /></b></p>
        <p style=""color:gray""><xsl:value-of select=""old"" /></p>
        <xsl:if test=""prop"">
            <ul>
                <xsl:apply-templates select=""prop"" />    
            </ul>
        </xsl:if>
        <xsl:if test=""control"">
            <ul>
                <xsl:apply-templates select=""control"" />    
            </ul>
        </xsl:if>
    </li>
</xsl:template>
</xsl:transform>
";
    }

    internal class CustomTask : FixMultilingualSiteTask
    {
        public CustomTask(Guid siteId)
            : base(siteId)
        {

        }
        protected override void SyncProps(PageManager manager, ControlProperty inv, ControlProperty def)
        {
            // logic copied from base class
            if (inv.LastModified >= def.LastModified)
                this.ReplaceProperty(manager, inv, def);
            else
                this.ReplaceProperty(manager, def, inv);
        }
    }

        #endregion
}