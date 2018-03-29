<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="HierarchicalTaxonFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.HierarchicalTaxonFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">

<sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.TaxonField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.HierarchicalTaxonField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        
        var taxonField;
        var _stringTestValue = 'Something';;
        var _objectTestValue = document.createElement('span');

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            taxonField = new Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField(mockElement);
        }

        function tearDown() {
            taxonField = null;
        }

        //Tests:
        
        // --------------------- property tests ----------------------
        function test_get_changeTaxonButton_returnsTheValueOfPrivateBackingField() {
            taxonField._changeTaxonButton = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_changeTaxonButton());
        }
        function test_set_changeTaxonButton_setsTheValueOfPrivateBackingField() {
            taxonField.set_changeTaxonButton(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._changeTaxonButton);
        }
        function test_get_createTaxonButton_returnsTheValueOfPrivateBackingField() {
            taxonField._createTaxonButton = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_createTaxonButton());
        }
        function test_set_createTaxonButton_setsTheValueOfPrivateBackingField() {
            taxonField.set_createTaxonButton(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._createTaxonButton);
        }
        function test_get_selectedTaxonPanel_returnsTheValueOfPrivateBackingField() {
            taxonField._selectedTaxonPanel = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_selectedTaxonPanel());
        }
        function test_set_selectedTaxonPanel_setsTheValueOfPrivateBackingField() {
            taxonField.set_selectedTaxonPanel(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._selectedTaxonPanel);
        }
        function test_get_taxaSelector_returnsTheValueOfPrivateBackingField() {
            taxonField._taxaSelector = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_taxaSelector());
        }
        function test_set_taxaSelector_setsTheValueOfPrivateBackingField() {
            taxonField.set_taxaSelector(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._taxaSelector);
        }
    </script>

</asp:Content>
