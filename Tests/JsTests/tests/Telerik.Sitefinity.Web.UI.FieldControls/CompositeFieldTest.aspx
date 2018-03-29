<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="CompositeFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.CompositeFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
   <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.CompositeFieldControl.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>
    <script type="text/javascript">
        var compositeField;

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            compositeField = new Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl(mockElement);
        }

        function tearDown() {
            compositeField = null;
        }

        //Tests:

        // --------------------- property tests ----------------------
        function test_get_title_returnsTheValueOfPrivateBackingField() {
            compositeField._title = ['Test'];
            assertArrayEquals(['Test'], compositeField.get_title());
            
        }
        function test_set_title_setsTheValueOfPrivateBackingField() {
            compositeField.set_title(['Test']);
            assertArrayEquals(['Test'], compositeField._title);
        }

        function test_get_example_returnsTheValueOfPrivateBackingField() {
            compositeField._example = ['Test'];
            assertArrayEquals(['Test'], compositeField.get_example());

        }
        function test_set_example_setsTheValueOfPrivateBackingField() {
            compositeField.set_example(['Test']);
            assertArrayEquals(['Test'], compositeField._example);
        }

        function test_get_description_returnsTheValueOfPrivateBackingField() {
            compositeField._description = ['Test'];
            assertArrayEquals(['Test'], compositeField.get_description());

        }
        function test_set_description_setsTheValueOfPrivateBackingField() {
            compositeField.set_description(['Test']);
            assertArrayEquals(['Test'], compositeField._description);
        }

        function test_get_titleElement_returnsTheValueOfPrivateBackingField() {
            compositeField._titleElement = 'Test';
            assertEquals('Test', compositeField.get_titleElement());

        }
        function test_set_titleElement_setsTheValueOfPrivateBackingField() {
            compositeField.set_titleElement('Test');
            assertEquals('Test', compositeField._titleElement);
        }

        function test_get_exampleElement_returnsTheValueOfPrivateBackingField() {
            compositeField._exampleElement = 'Test';
            assertEquals('Test', compositeField.get_exampleElement());

        }
        function test_set_exampleElement_setsTheValueOfPrivateBackingField() {
            compositeField.set_exampleElement('Test');
            assertEquals('Test', compositeField._exampleElement);
        }

        function test_get_descriptionElement_returnsTheValueOfPrivateBackingField() {
            compositeField._descriptionElement = 'Test';
            assertEquals('Test', compositeField.get_descriptionElement());

        }
        function test_set_descriptionElement_setsTheValueOfPrivateBackingField() {
            compositeField.set_descriptionElement('Test');
            assertEquals('Test', compositeField._descriptionElement);
        }

    </script>
</asp:Content>

