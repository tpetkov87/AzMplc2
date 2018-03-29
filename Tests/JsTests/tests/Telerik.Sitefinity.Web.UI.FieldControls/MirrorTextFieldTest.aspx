<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="MirrorTextFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.MirrorTextFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
   <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.MirrorTextField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>
    <script type="text/javascript">
        var mirrorTextField;
        var _domTestValue = document.createElement('div');

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            mirrorTextField = new Telerik.Sitefinity.Web.UI.Fields.MirrorTextField(mockElement);
        }

        function tearDown() {
        }

        //Tests:

        // --------------------- property tests ----------------------
        function test_get_regularExpressionFilter_returnsTheValueOfPrivateBackingField() {
            mirrorTextField._regularExpressionFilter = ['Test'];
            assertArrayEquals(['Test'], mirrorTextField.get_regularExpressionFilter());
        }
        function test_set_regularExpressionFilter_setsTheValueOfPrivateBackingField() {
            mirrorTextField.set_regularExpressionFilter(['Test']);
            assertArrayEquals(['Test'], mirrorTextField._regularExpressionFilter);
        }
        function test_get_mirroredControlId_returnsTheValueOfPrivateBackingField() {
            mirrorTextField._mirroredControlId = ['Test'];
            assertArrayEquals(['Test'], mirrorTextField.get_mirroredControlId());
        }
        function test_set_mirroredControlId_setsTheValueOfPrivateBackingField() {
            mirrorTextField.set_mirroredControlId(['Test']);
            assertArrayEquals(['Test'], mirrorTextField._mirroredControlId);
        }
        
    </script>
</asp:Content>
