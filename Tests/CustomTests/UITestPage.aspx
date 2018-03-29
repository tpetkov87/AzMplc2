<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sfFields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile JavaScriptLibrary="JQuery"></sitefinity:ResourceFile>
    <sitefinity:ResourceFile Name="Styles/Tabstrip.css" />
</sitefinity:ResourceLinks>         

<sfFields:FormManager id="formManager" runat="server"/>

<sitefinity:Message ID="message" runat="server" 
            ElementTag="div" 
            CssClass="sfMessage sfDesignerMessage" 
            RemoveAfter="50000" 
            FadeDuration="10"  />
            
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" 
                     Skin="Default" 
                     MultiPageID="designerTabsMultiPage" 
                     SelectedIndex="0" 
                     OnClientTabSelected="function() {dialogBase.resizeToContent();}" 
                     CssClass="sfSwitchControlViews">
    <Tabs>
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="designerTabsMultiPage" runat="server" 
                      SelectedIndex="0" 
                      CssClass="sfContentViews">
</telerik:RadMultiPage>