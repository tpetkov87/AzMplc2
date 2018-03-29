<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master"
    AutoEventWireup="true" CodeBehind="TextFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.TextFieldTest" %>

<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity"
    TagPrefix="sitefinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:EmbeddedResourcePropertySetter JavaScriptLibrary="JQuery" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.TextField.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
            <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        var textField;
        var _stringTestValue = 'Something';

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            textField = new Telerik.Sitefinity.Web.UI.Fields.TextField(mockElement);
        }

        function tearDown() {
            textField = null;
        }

        //Tests:
        // --------------------- property tests ----------------------
        function test_get_labelElement_returnsTheValueOfPrivateBackingField() {
            textField._labelElement = _stringTestValue;
            assertEquals(_stringTestValue, textField.get_labelElement());

        }
        function test_set_labelElement_setsTheValueOfPrivateBackingField() {
            textField.set_labelElement(_stringTestValue);
            assertEquals(_stringTestValue, textField._labelElement);
        }

        function test_get_textBoxElement_returnsTheValueOfPrivateBackingField() {
            textField._textBoxElement = _stringTestValue;
            assertEquals(_stringTestValue, textField.get_textBoxElement());

        }
        function test_set_textBoxElement_setsTheValueOfPrivateBackingField() {
            textField.set_textBoxElement(_stringTestValue);
         
            assertEquals(_stringTestValue, textField._textBoxElement);
        }

        function test_clearLabel() {
            /* This test does not pass, it could be because displayMode change is buggy when done on the client */

           //at read mode set value to label
           //textField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read);
           textField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read;
           textField.set_value(_stringTestValue);
           //clear the label
           textField._clearLabel();

           var gotValue = textField.get_value();
           assertEquals("", gotValue);
       }

       function test_on_change_isChanged_returns_true() {
           textField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
           textField.set_value(_stringTestValue);
           textField.set_textBoxElement("some new value");
           assertEquals(textField.isChanged(), true);
       }

        // --------------------- property tests ----------------------
    </script>

</asp:Content>
