<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="FieldControlTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.FieldControlTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">

    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        
        var fieldControl;
        var _stringTestValue = 'Something';;
        var _objectTestValue = document.createElement('span');

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            fieldControl = new Telerik.Sitefinity.Web.UI.Fields.FieldControl(mockElement);
        }

        function tearDown() {
            fieldControl = null;
        }

        //Tests:
        
        // --------------------- property tests ----------------------
        
        function test_get_dataFieldName_returnsTheValueOfPrivateBackingField() {
            fieldControl._dataFieldName = _stringTestValue;
            assertEquals(_stringTestValue, fieldControl.get_dataFieldName());
        }
        
        function test_set_dataFieldName_setsTheValueOfPrivateBackingField() {
            fieldControl.set_dataFieldName(_stringTestValue);
            assertEquals(_stringTestValue, fieldControl._dataFieldName);
        }
        
        function test_get_dataFormatString_returnsTheValueOfPrivateBackingField() {
            fieldControl._dataFormatString = _stringTestValue;
            assertEquals(_stringTestValue, fieldControl.get_dataFormatString());
        }
        
        function test_set_dataFormatString_setsTheValueOfPrivateBackingField() {
            fieldControl.set_dataFormatString(_stringTestValue);
            assertEquals(_stringTestValue, fieldControl._dataFormatString);
        }
        
        function test_get_description_returnsTheValueOfPrivateBackingField() {
            fieldControl._description = _stringTestValue;
            assertEquals(_stringTestValue, fieldControl.get_description());
        }   
        
        function test_set_description_setsTheValueOfPrivateBackingField() {
            fieldControl.set_description(_stringTestValue);
            assertEquals(_stringTestValue, fieldControl._description);
        }
        
        function test_get_descriptionElement_returnsTheValueOfPrivateBackingField() {
            fieldControl._descriptionElement = _objectTestValue;
            assertEquals(_objectTestValue, fieldControl.get_descriptionElement());
        }
        
        function test_set_descriptionElement_setsTheValueOfPrivateBackingField() {
            fieldControl.set_descriptionElement(_objectTestValue);
            assertEquals(_objectTestValue, fieldControl._descriptionElement);
        }
        
        function test_get_displayMode_returnsTheValueOfPrivateBackingField() {
            fieldControl._displayMode = Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read;
            assertEquals(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read, fieldControl.get_displayMode());
        }
        
        function test_set_displayMode_setsTheValueOfPrivateBackingField() {
            fieldControl.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            assertEquals(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write, fieldControl._displayMode);
        }
        
        function test_get_example_returnsTheValueOfPrivateBackingField() {
            fieldControl._example = _stringTestValue;
            assertEquals(_stringTestValue, fieldControl.get_example());
        }
        
        function test_set_example_setsTheValueOfPrivateBackingField() {
            fieldControl.set_example(_stringTestValue);
            assertEquals(_stringTestValue, fieldControl._example);
        }
        
        function test_get_exampleElement_returnsTheValueOfPrivateBackingField() {
            fieldControl._exampleElement = _objectTestValue;
            assertEquals(_objectTestValue, fieldControl.get_exampleElement());
        }
        
        function test_set_exampleElement_setsTheValueOfPrivateBackingField() {
            fieldControl.set_exampleElement(_objectTestValue);
            assertEquals(_objectTestValue, fieldControl._exampleElement);
        }
        
        function test_get_title_returnsTheValueOfPrivateBackingField() {
            fieldControl._title = _stringTestValue;
            assertEquals(_stringTestValue, fieldControl.get_title());
        }
        
        function test_set_title_setsTheValueOfPrivateBackingField() {
            fieldControl.set_title(_stringTestValue);
            assertEquals(_stringTestValue, fieldControl._title);
        }
        
        function test_get_titleElement_returnsTheValueOfPrivateBackingField() {
            fieldControl._titleElement = _objectTestValue;
            assertEquals(_objectTestValue, fieldControl.get_titleElement());
        }
        
        function test_set_titleElement_setsTheValueOfPrivateBackingField() {
            fieldControl.set_titleElement(_objectTestValue);
            assertEquals(_objectTestValue, fieldControl._titleElement);
        }
        
        function test_get_validatorDefinition_returnsTheValueOfPrivateBackingField() {
            fieldControl._validatorDefinition = _objectTestValue;
            assertEquals(_objectTestValue, fieldControl.get_validatorDefinition());
        }
        
        function test_set_validatorDefinition_setsTheValueOfPrivateBackingField() {
            fieldControl.set_validatorDefinition(_objectTestValue);
            assertEquals(_objectTestValue, fieldControl._validatorDefinition);
        }
        
        function test_get_value_returnsTheValueOfPrivateBackingField() {
            fieldControl._value = _stringTestValue;
            assertEquals(_stringTestValue, fieldControl.get_value());
        }
        
        function test_set_value_setsTheValueOfPrivateBackingField() {
            fieldControl.set_value(_stringTestValue);
            assertEquals(_stringTestValue, fieldControl._value);
        }

        // --------------------- events tests ----------------------
        function test_add_valueChanged_properlySubscribes() {
            var mockDelegate = createMockDelegate();
            fieldControl.add_valueChanged(mockDelegate);
            assertDelegateAdded(fieldControl, mockDelegate, 'valueChanged');
        }

        function test_remove_valueChanged_properlyUnsubscribes() {
            var mockDelegate = createMockDelegate();
            fieldControl.add_valueChanged(mockDelegate);
            assertDelegateAdded(fieldControl, mockDelegate, 'valueChanged');
            fieldControl.remove_valueChanged(mockDelegate);
            assertDelegateRemoved(fieldControl, mockDelegate, 'valueChanged');
        }
        
        function test_valueChangedHandler_FiresAnEvent() {
            // event fired flag
            var eventFired = false;
            // subscribe to the event
            fieldControl.add_valueChanged(function() {
                // when called, change the value of the event flat
                eventFired = true;
            });
            // force the firing of the event
            fieldControl._valueChangedHandler();
            // assert that the function that subscribed was called and has modified the flag
            assertEvaluatesToTrue(eventFired);
        }
        
    </script>

</asp:Content>
