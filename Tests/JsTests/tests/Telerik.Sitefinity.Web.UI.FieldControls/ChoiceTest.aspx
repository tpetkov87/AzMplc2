<%@ Page Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="ChoiceTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.ChoiceTest" Title="Untitled Page" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">

    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.Choice.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile JavaScriptLibrary="JQuery" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        
        var choice;
        var _stringTestValue = 'Something';;

        // SetUp and TearDown
        function setUp() {
            choice = new Telerik.Sitefinity.Web.UI.Fields.Choice();
        }

        function tearDown() {
            choice = null;
        }

        //Tests:
        
        // --------------------- property tests ----------------------
        function test_get_text_returnsTheValueOfPrivateBackingField() {
            choice._text = _stringTestValue;
            assertEquals(_stringTestValue, choice.get_text());
        }
        function test_set_text_setsTheValueOfPrivateBackingField() {
            choice.set_text(_stringTestValue);
            assertEquals(_stringTestValue, choice._text);
        }
        function test_get_value_returnsTheValueOfPrivateBackingField() {
            choice._value = _stringTestValue;
            assertEquals(_stringTestValue, choice.get_value());
        }
        function test_set_value_setsTheValueOfPrivateBackingField() {
            choice.set_value(_stringTestValue);
            assertEquals(_stringTestValue, choice._value);
        }
        function test_get_description_returnsTheValueOfPrivateBackingField() {
            choice._description = _stringTestValue;
            assertEquals(_stringTestValue, choice.get_description());
        }   
        function test_set_description_setsTheValueOfPrivateBackingField() {
            choice.set_description(_stringTestValue);
            assertEquals(_stringTestValue, choice._description);
        }
        function test_get_enabled_returnsTheValueOfPrivateBackingField() {
            choice._enabled = true;
            assertEvaluatesToTrue(choice.get_enabled());
        }
        function test_set_enabled_setsTheValueOfPrivateBackingField() {
            choice.set_enabled(true);
            assertEvaluatesToTrue(choice._enabled);
        }
    </script>

</asp:Content>
