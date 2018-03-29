<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormsTestPage.aspx.cs" Inherits="SitefinityWebApp.FormsTestPage" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
  <head runat="server">
    <title></title>
  </head>
  <body>
    <form id="form1" runat="server">
      <div>
        <%--Configured in the code-behind--%>
        <asp:ScriptManager ID="theScriptManager" runat="server">
          <Scripts>
            <asp:ScriptReference Assembly="Telerik.Sitefinity" Name="Telerik.Sitefinity.Web.Scripts.ClientManager.js" />                    
          </Scripts>
        </asp:ScriptManager>

        <asp:TextBox ID="formName" runat="server"></asp:TextBox>
        <asp:Button ID="btnCreateFormDescription" runat="server" Text="Create form description"/>
        <asp:Button ID="btnCreateMetaType" runat="server" Text="Create meta type"/>
        <asp:Button ID="btnCreateFormEntry" runat="server" Text="Create form entry"/>
        <asp:Button ID="btnGetFormDescriptionFields" runat="server" Text="Get form description fields"/>
        <asp:Button ID="btnGoToEntriesList" runat="server" Text="Go to rntries list"/>

      </div>
    </form>
    <script type="text/javascript">        
    </script>
  </body>
</html>
