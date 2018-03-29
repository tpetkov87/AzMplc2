<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="StatusFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.StatusFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
   <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.StatusField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>
    <script type="text/javascript">
        var statusField;

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            statusField = new Telerik.Sitefinity.Web.UI.Fields.StatusField(mockElement);
        }

        function tearDown() {
            statusField = null;
        }

        //Tests:

        // --------------------- property tests ----------------------
        function test_get_expirationDateComponent_returnsTheValueOfPrivateBackingField() {
            statusField._expirationDateComponent = ['Test'];
            assertArrayEquals(['Test'], statusField.get_expirationDateComponent());
            
        }
        function test_set_expirationDateComponent_setsTheValueOfPrivateBackingField() {
            statusField.set_expirationDateComponent(['Test']);
            assertArrayEquals(['Test'], statusField._expirationDateComponent);
        }

        function test_get_publicationDateComponent_returnsTheValueOfPrivateBackingField() {
            statusField._publicationDateComponent = ['Test'];
            assertArrayEquals(['Test'], statusField.get_publicationDateComponent());

        }
        function test_set_publicationDateComponent_setsTheValueOfPrivateBackingField() {
            statusField.set_publicationDateComponent(['Test']);
            assertArrayEquals(['Test'], statusField._publicationDateComponent);
        }

        function test_get_statusChoiseComponent_returnsTheValueOfPrivateBackingField() {
            statusField._statusChoiseComponent = ['Test'];
            assertArrayEquals(['Test'], statusField.get_statusChoiseComponent());

        }
        function test_set_statusChoiseComponent_setsTheValueOfPrivateBackingField() {
            statusField.set_statusChoiseComponent(['Test']);
            assertArrayEquals(['Test'], statusField._statusChoiseComponent);
        }
    </script>
</asp:Content>

