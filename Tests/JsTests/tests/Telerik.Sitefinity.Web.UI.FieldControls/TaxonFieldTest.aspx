<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="TaxonFieldTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.TaxonFieldTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">

    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:EmbeddedResourcePropertySetter JavaScriptLibrary="JQuery" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Scripts.TaxonField.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        
        var taxonField;
        var _stringTestValue = 'Something';;
        var _objectTestValue = document.createElement('span');

        // SetUp and TearDown
        function setUp() {
            var mockElement = document.createElement('div');
            taxonField = new Telerik.Sitefinity.Web.UI.Fields.TaxonField(mockElement);
        }

        function tearDown() {
            taxonField = null;
        }

        //Tests:
        
        // --------------------- property tests ----------------------
        
        function test_get_webServiceUrl_ReturnsTheValueOfPrivateBackingField() {
            taxonField._webServiceUrl = _stringTestValue;
            assertEquals(_stringTestValue, taxonField.get_webServiceUrl());
        }
        
        function test_set_webServiceUrl_SetsTheValueOfPrivateBackingField() {
            taxonField.set_webServiceUrl(_stringTestValue);
            assertEquals(_stringTestValue, taxonField._webServiceUrl);
        }
        
        function test_get_taxonomyId_ReturnsTheValueOfPrivateBackingField() {
            taxonField._taxonomyId = _objectTestValue;
            assertEquals(_objectTestValue, taxonField.get_taxonomyId());
        }
        
        function test_set_taxonomyId_SetsTheValueOfPrivateBackingField() {
            taxonField.set_taxonomyId(_objectTestValue);
            assertEquals(_objectTestValue, taxonField._taxonomyId);
        }
        
        function test_get_taxonomyProvider_ReturnsTheValueOfPrivateBackingField() {
            taxonField._taxonomyProvider = _stringTestValue;
            assertEquals(_stringTestValue, taxonField.get_taxonomyProvider());
        }
        
        function test_set_taxonomyProvider_SetsTheValueOfPrivateBackingField() {
            taxonField.set_taxonomyProvider(_stringTestValue);
            assertEquals(_stringTestValue, taxonField._taxonomyProvider);
        }
        
        function test_get_allowMultipleSelection_ReturnsTheValueOfPrivateBackingField() {
            taxonField._allowMultipleSelection = true;
            assertEvaluatesToTrue(taxonField.get_allowMultipleSelection());
        }
        
        function test_set_allowMultipleSelection_SetsTheValueOfPrivateBackingField() {
            taxonField.set_allowMultipleSelection(true);
            assertEvaluatesToTrue(taxonField._allowMultipleSelection);
        }

        function test_get_selectedTaxaList_ElementIsNotNull_ReturnsTheElement() {
            var testElement = document.createElement('div');
            taxonField._selectedTaxaList = testElement;
            assertEquals(testElement, taxonField.get_selectedTaxaList());
        }

        function test_get_selectedTaxaList_ElementIsNullAndElementIdIsNull_ReturnsNull() {
            assertNull(taxonField.get_selectedTaxaList());
        }

        function test_get_selectedTaxaList_ElementIsNullIdIsSet_FindsTheElementAndReturnsIt() {
            var element = document.getElementById(dummyElementId);
            taxonField._selectedTaxaListId = dummyElementId;
            assertObjectEquals(element, taxonField.get_selectedTaxaList());
        }
        
        // --------------------- private method tests ----------------------
        
        function test_createSelectedTaxonElement_RemovableItemTemplateNull_ThrowsException() {
            var taxonTitle = 'Tag 1';
            var thrown = false;
            try
            {
                taxonField._createSelectedTaxonElement(taxonTitle);
            }
            catch(error)
            {
                assertEquals('TaxonField._removableItemTemplate field has not been initialized to a client template.', error);
                thrown = true;
            }
            if(!thrown)
                fail('No exception has been thrown.');
        }

        function test_createSelectedTaxonElement_selectedTaxaListIsNull_ThrowsException() {
            var removableItemTemplate = "<li><span class='sfRemovableItemTitle'></span><a href='#' class='sfRemoveBtn'>[x]</a></li>";
            var taxonTitle = 'Tag 1';
            taxonField._removableItemTemplate = removableItemTemplate;
            var thrown = false;
            try
            {
                taxonField._createSelectedTaxonElement(taxonTitle);
            }
            catch(error)
            {
                assertEquals('TaxonField._selectedTaxaList not been initialized to a respective DOM element.', error);
                thrown = true;
            }
            if(!thrown)
                fail('No exception has been thrown.');
        }
        
        function test_createSelectedTaxonElement_taxonTitleWasNotPassed_throwsException() {
            var list = document.createElement('ul');
            var removableItemTemplate = "<li><span class='sfRemovableItemTitle'></span><a href='#' class='sfRemoveBtn'>[x]</a></li>";
            taxonField._selectedTaxaList = list;
            taxonField._removableItemTemplate = removableItemTemplate;
            var thrown = false;
            try
            {
                taxonField._createSelectedTaxonElement();
            }
            catch(error)
            {
                assertEquals('You must supply taxonTitle parameter.', error);
                thrown = true;
            }
            if(!thrown)
                fail('No exception has been thrown.');
        }
        
        function test_createSelectedTaxonElement_AppendsTheElementToTheSelectedTaxonList() {
            var list = document.createElement('ul');
            var removableItemTemplate = "<li><span class='sfRemovableItemTitle'></span><a href='#' class='sfRemoveBtn'>[x]</a></li>";
            var expectedHTML = "<li><span class='sfRemovableItemTitle'>Tag 1</span><a href='#' class='sfRemoveBtn'>[x]</a></li>";
            var taxonTitle = 'Tag 1';
            taxonField._selectedTaxaList = list;
            taxonField._removableItemTemplate = removableItemTemplate;
            taxonField._createSelectedTaxonElement(taxonTitle);
            assertHTMLEquals(expectedHTML, list.innerHTML);
        }
        
    </script>
</asp:Content>
