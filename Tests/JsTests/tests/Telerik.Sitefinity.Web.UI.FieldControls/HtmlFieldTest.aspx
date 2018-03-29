<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="HtmlFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.HtmlFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:EmbeddedResourcePropertySetter JavaScriptLibrary="JQuery" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />  
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.HtmlField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>
    <script type="text/javascript">
        var htmlField;
        var _stringTestValue = 'Something';
        var _objectTestValue = document.createElement('span');
        var _mockRadEditor = {
            _content: null,
            set_html: function(value) { this._content = value; },
            get_html: function() { return this._content; }
        };
        
        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            htmlField = new Telerik.Sitefinity.Web.UI.Fields.HtmlField(mockElement);
        }

        function tearDown() {
            htmlField = null;
        }
        
        //Tests:

        // ------------------ public methods tests ------------------

        function test_reset_wasCalled_in_writeMode() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
            htmlField._editControl = _mockRadEditor;
            htmlField.reset();
            assertEquals('', htmlField._editControl.get_html(true));
        }

        function test_reset_wasCalled_in_readMode() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read;
            htmlField._viewControl = _objectTestValue;
            htmlField.reset();
            assertEquals('', htmlField._viewControl.innerHTML);
        }

        // ----------------- event handlers tests --------------------
        
        function test_handleEditorBlur_valueChangedHandler_wasFired() {
            htmlField._originalValue = _stringTestValue;
            htmlField._editControl = _mockRadEditor;
            var eventFired = false;
            // subscribe to the event
            htmlField.add_valueChanged(function() {
                // when called, change the value of the event flag
                eventFired = true;
            });
            // force the firing of the event
            htmlField._handleEditorBlur();
            // assert that the function that subscribed was called and has modified the flag
            assertEvaluatesToTrue(eventFired);
        }
        
        // ----------------- private methods tests -------------------

        function test_setEditorHtml_wasCalled_value_is_null() {
            htmlField._editControl = _mockRadEditor;
            htmlField._setEditorHtml(null);
            assertEquals('', htmlField._editControl.get_html(true));
        }

        function test_setEditorHtml_wasCalled_value_is_not_null() {
            htmlField._editControl = _mockRadEditor;
            htmlField._setEditorHtml(_stringTestValue);
            assertEquals(_stringTestValue, htmlField._editControl.get_html(true));
        }
        
        // --------------------- property tests ----------------------

        function test_get_editControlId_returnsTheValueOfPrivateBackingField() {
            htmlField._editControlId = _stringTestValue;
            assertEquals(_stringTestValue, htmlField.get_editControlId());
        }

        function test_set_editControlId_setsTheValueOfPrivateBackingField() {
            htmlField.set_editControlId(_stringTestValue);
            assertEquals(_stringTestValue, htmlField._editControlId);
        }

        function test_get_viewControl_returnsTheValueOfPrivateBackingField() {
            htmlField._viewControl = _objectTestValue;
            assertEquals(_objectTestValue, htmlField.get_viewControl());
        }

        function test_set_viewControl_setsTheValueOfPrivateBackingField() {
            htmlField.set_viewControl(_objectTestValue);
            assertEquals(_objectTestValue, htmlField._viewControl);
        }
        
        function test_get_value_returnsTheValue_in_writeMode() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
            _mockRadEditor.set_html(_stringTestValue);
            htmlField._editControl = _mockRadEditor;
            assertEquals(_stringTestValue, htmlField.get_value());
        }

        function test_get_value_returnsTheValue_in_readMode() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read;
            _objectTestValue.innerHTML = _stringTestValue;
            htmlField._viewControl = _objectTestValue;
            assertEquals(_stringTestValue, htmlField.get_value());
        }

        function test_set_value_setsTheValue_in_writeMode() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
            htmlField._editControl = _mockRadEditor;
            htmlField.set_value(_stringTestValue);
            assertEquals(_stringTestValue, htmlField._editControl.get_html());
        }

        function test_set_value_setsTheValue_in_readMode() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read;
            htmlField._viewControl = _objectTestValue;
            htmlField.set_value(_stringTestValue);
            assertEquals(_stringTestValue, htmlField._viewControl.innerHTML);
        }


        function test_on_change_isChanged_returns_true() {
            htmlField._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
            htmlField._editControl = _mockRadEditor;
            htmlField.set_value(_stringTestValue);
            htmlField._editControl.set_html("some new value");
            assertEquals(htmlField.isChanged(), true);
        }
        
        // --------------------- mocks ----------------------
        
    </script>
</asp:Content>
