<%@ Page Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true"
    CodeBehind="ChoiceFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.ChoiceFieldTest"
    Title="Untitled Page" %>

<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity"
    TagPrefix="sitefinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
    <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.Choice.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.RenderChoicesAs.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.ChoiceField.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
            <sitefinity:ResourceFile JavaScriptLibrary="JQuery" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        var choiceField;
        var _stringTestValue = 'Something'; ;
        var _domTestValue = document.createElement('div');
        var valueChangedFired = false;

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            choiceField = new Telerik.Sitefinity.Web.UI.Fields.ChoiceField(mockElement);
            choiceField._valueChangedHandler = function() { valueChangedFired = true; }
        }

        function tearDown() {
            choice = null;
        }

        //Tests:

        // --------------------- property tests ----------------------
        function test_get_choices_returnsTheValueOfPrivateBackingField() {
            choiceField._choices = ['Test'];
            assertArrayEquals(['Test'], choiceField.get_choices());
        }
        function test_set_choices_setsTheValueOfPrivateBackingField() {
            choiceField.set_choices(['Test']);
            assertArrayEquals(['Test'], choiceField._choices);
        }


        function test_get_mutuallyExclusive_returnsTheValueOfPrivateBackingField() {
            choiceField._mutuallyExclusive = true;
            assertEvaluatesToTrue(choiceField.get_mutuallyExclusive());
        }
        function test_set_mutuallyExclusive_setsTheValueOfPrivateBackingField() {
            choiceField.set_mutuallyExclusive(true);
            assertEvaluatesToTrue(choiceField._mutuallyExclusive);
        }

        function test_get_renderChoicesAs_returnsTheValueOfPrivateBackingField() {
            choiceField._renderChoicesAs = Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown;
            assertEquals(Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown, choiceField.get_renderChoicesAs());
        }
        function test_set_renderChoicesAs_setsTheValueOfPrivateBackingField() {
            choiceField.set_renderChoicesAs(Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons);
            assertEquals(Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons, choiceField._renderChoicesAs);
        }

        //        function not_test_get_selectedChoicesIndex_returnsTheValueOfPrivateBackingField() {
        //            choiceField._selectedChoicesIndex = [1,2];
        //            assertArrayEquals([1,2], choiceField.get_selectedChoicesIndex());
        //        }

        //        function not_test_set_selectedChoicesIndex_setsTheValueOfPrivateBackingField() {
        //            choiceField.set_selectedChoicesIndex([1,2,3]);
        //            assertArrayEquals([1,2,3], choiceField._selectedChoicesIndex);
        //        }


        function test_get_value_Calls_get_selectedItemValues_and_Reurns_ItsResult() {
            choiceField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            choiceField.get_selectedItemsValues = function() { return [1, 2, 3]; }

            var res = choiceField.get_value();
            assertArrayEquals([1, 2, 3], res);
        }

        function test_set_value_Calls_selectListItemsByValue_andSets_BackingField() {
            choiceField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            choiceField.clearAllSelectedItems = function() { };

            var called = false;
            choiceField.selectListItemsByValue = function(value) { called = true; };
            choiceField.set_value([1, 2, 3]);
            assertArrayEquals([1, 2, 3], choiceField._value);
            assertTrue(called);

        }

        function test_set_value_fires_valueChangedHandler() {
            choiceField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            choiceField.clearAllSelectedItems = function() { };
            choiceField.selectListItemsByValue = function(value) { };
            choiceField.set_value([1, 2, 3]);
            assertTrue(valueChangedFired);

        }


        function test_on_change_isChanged_returns_true() {
            choiceField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
            choiceField.clearAllSelectedItems = function () { };
            choiceField.selectListItemsByValue = function (value) { };
            choiceField.set_value([1, 2, 3]);
            choiceField._value = [4, 4, 4];
            var isChanged = choiceField.isChanged();
            assertEquals(isChanged, true);
        }
        
        
        
        
    </script>

</asp:Content>
