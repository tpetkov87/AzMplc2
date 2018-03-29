<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="ClientManagerTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Data.ClientManagerTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TestBody" runat="server">
    
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.Scripts.ClientManager.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        
        var clientManager;

        // SetUp and TearDown
        function setUp() {
            clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }

        function tearDown() {
            clientManager = null;
        }

        //Tests:
        function test_GetEmptyGuid_ReturnsStringRespresentationOfAnEmptyGuid() {
            var guid = clientManager.GetEmptyGuid();
            assertEquals(guid, '00000000-0000-0000-0000-000000000000');
        }
        
    </script>

</asp:Content>
