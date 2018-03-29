<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="FlatTaxonFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.FlatTaxonFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">

    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.TaxonField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FlatTaxonField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        
        var taxonField;
        var _stringTestValue = 'Something';
        var _objectTestValue = document.createElement('span');

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            taxonField = new Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField(mockElement);
        }

        function tearDown() {
            taxonField = null;
        }

        //Tests:
        
        // --------------------- property tests ----------------------
        
        function test_get_taxaInput_returnsTheValueOfPrivateBackingField() {
            taxonField._taxaInput = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_taxaInput());
        }
        
        function test_set_taxaInput_setsTheValueOfPrivateBackingField() {
            taxonField.set_taxaInput(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._taxaInput);
        }
        
        function test_get_addTaxaButton_returnsTheValueOfPrivateBackingField() {
            taxonField._addTaxaButton = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_addTaxaButton());
        }
        
        function test_set_addTaxaButton_setsTheValueOfPrivateBackingfield() {
            taxonField.set_addTaxaButton(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._addTaxaButton);
        }

        function test_get_existingTaxaPanel_returnsTheValueOfPrivateBackingField() {
            taxonField._existingTaxaPanel = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_existingTaxaPanel());
        }

        function test_set_existingTaxaPanel_setsTheValueOfPrivateBackingField() {
            taxonField.set_existingTaxaPanel(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._existingTaxaPanel);
        }

        function test_get_existingTaxaBinder_returnsTheValueOfPrivateBackingField() {
            taxonField._existingTaxaBinder = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_existingTaxaBinder());
        }

        function test_set_existingTaxaBinder_setsTheValueOfPrivateBackingField() {
            taxonField.set_existingTaxaBinder(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._existingTaxaBinder);
        }

        function test_get_selectFromExistingPanel_returnsTheValueOfPrivateBackingField() {
            taxonField._selectFromExistingPanel = _objectTestValue;
            assertEquals(_objectTestValue, taxonField._selectFromExistingPanel);
        }

        function test_set_selectFromExistingPanel_setsTheValueOfPrivateBackingField() {
            taxonField.set_selectFromExistingPanel(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._selectFromExistingPanel);
        }

        function test_get_selectFromExistingButton_returnsTheValueOfPrivateBackingField() {
            taxonField._selectFromExistingButton = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_selectFromExistingButton());
        }

        function test_set_selectFromExistingButton_setsTheValueOfPrivateBackingField() {
            taxonField.set_selectFromExistingButton(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._selectFromExistingButton);
        }

        function test_get_closeExistingButton_returnsTheValueOfPrivateBackingField() {
            taxonField._closeExistingButton = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_closeExistingButton());
        }

        function test_set_closeExistingButton_setsTheValueOfPrivateBackingField() {
            taxonField.set_closeExistingButton(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._closeExistingButton);
        }

        // --------------------- private functions tests ----------------------

        // _getEnteredTaxa tests
        function test_getEnteredTaxa_taxaInputIsNull_throwsException() {
            try
            {
                taxonField._getEnteredTaxa();
            }
            catch(error)
            {
                assertEquals('TaxaInput element is not initialized.', error);
            }
        }

        function test_getEnteredTaxa_noTextEntered_returnsAnEmptyArray() {
            var textFieldStub = document.createElement('input');
            taxonField._taxaInput = textFieldStub;
            assertEquals(0, taxonField._getEnteredTaxa().length);
        }

        function test_getEnteredTaxa_returnsAnEmptyArray() {
            var enteredText = ',    ,  ,';
            var textFieldStub = document.createElement('input');
            textFieldStub.value = enteredText;
            taxonField._taxaInput = textFieldStub;
            assertEquals(0, taxonField._getEnteredTaxa().length);
        }

        function test_getEnteredTaxa_threeTaxonsTwoCommas_returnsAnArrayWithThreeStrings() {
            var enteredText = ',Tag 1, Tag 2  , Tag 3, ';
            var expectedResult = ['Tag 1', 'Tag 2', 'Tag 3'];

            var textFieldStub = document.createElement('input');
            textFieldStub.value = enteredText;

            taxonField._taxaInput = textFieldStub;
            assertArrayEquals(expectedResult, taxonField._getEnteredTaxa());
        }
        
    </script>

</asp:Content>
