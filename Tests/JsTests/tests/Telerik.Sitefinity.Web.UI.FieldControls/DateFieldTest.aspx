<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master"
    AutoEventWireup="true" CodeBehind="DateFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.DateFieldTest" %>

<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity"
    TagPrefix="sitefinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile JavaScriptLibrary="JQuery" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />    
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.DateField.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript" language="javascript">
        var fieldControl;
        var stringTestValue = "Something";
        var objectTestValue = document.createElement('span');
        var componentTestValue = {};
        var valueChangedFired = false;

        var mockDatePicker = {
            _value: null,
            set_selectedDate: function(v) { this._value = v; },
            get_selectedDate: function() { return this._value; }
        };

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            fieldControl = new Telerik.Sitefinity.Web.UI.Fields.DateField(mockElement);
            fieldControl._textControl = document.createElement('div');
            fieldControl._valueChangedHandler = function() { valueChangedFired = true; }
        }

        function tearDown() {
            fieldControl = null;
            mockDatePicker._value = null;
            valueChangedFired = false;
        }

        //tests getting of datePicker backing field
        function test_get_datePicker_returnsTheValueOfPrivateBackingField() {
            fieldControl._datePicker = mockDatePicker;
            assertEquals(mockDatePicker, fieldControl.get_datePicker());
        }

        //tests setting of datePicker backing field
        function test_set_datePicker_setsTheValueOfPrivateBackingField() {
            fieldControl.set_datePicker(mockDatePicker);
            assertEquals(mockDatePicker, fieldControl._datePicker);
        }

        //tests getting of textControl backing field
        function test_get_textControl_returnsTheValueOfPrivateBackingField() {
            fieldControl._textControl = objectTestValue;
            assertEquals(objectTestValue, fieldControl.get_textControl());
        }

        //tests setting of textControl backing field
        function test_set_textControl_setsTheValueOfPrivateBackingField() {
            fieldControl.set_textControl(objectTestValue);
            assertEquals(objectTestValue, fieldControl._textControl);
        }
        
        //tests if get_value returns the _ backing field in write mode
        function test_get_value_returnsTheValueOfPrivateBackingField_In_WriteMode() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            mockDatePicker.set_selectedDate(stringTestValue);
            fieldControl._datePicker = mockDatePicker;
            assertEquals(stringTestValue, fieldControl.get_value());
        }

        //tests if set_value sets the _ backing field in write mode
        function test_set_value_setsTheValueOfPrivateBackingField_In_WriteMode() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            fieldControl._datePicker = mockDatePicker;
            fieldControl.set_value(stringTestValue);
            assertEquals(stringTestValue, fieldControl._value);
        }

        //tests if get_value returns the _ backing field in read mode
        function test_get_value_returnsTheValueOfPrivateBackingField_In_ReadMode() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read);
            fieldControl._value = stringTestValue;
            assertEquals(stringTestValue, fieldControl.get_value());
        }

        //tests if set_value sets the _ backing field in read mode
        function test_set_value_setsTheValueOfPrivateBackingField_In_ReadMode() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read);
            fieldControl.set_value(stringTestValue);
            assertEquals(stringTestValue, fieldControl._value);
        }

        // tests if set_value fires _valueChangedHandler
        function test_set_value_fires_valueChangedHandler() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            fieldControl._datePicker = mockDatePicker;
            fieldControl.set_value(stringTestValue);
            assertTrue(valueChangedFired);
        }

        function test_on_change_isChanged_returns_true() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            fieldControl.set_value(stringTestValue);
            fieldControl._datePicker = mockDatePicker;
            assertEquals(fieldControl.isChanged(), true);
        }
        
    </script>

</asp:Content>
