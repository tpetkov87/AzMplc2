<%@ Page Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" AutoEventWireup="true" CodeBehind="ExpandableExtenderTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtenderTest" Title="Untitled Page" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">

    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Extenders.Scripts.ExpandableExtender.js" AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>
    
    <script type="text/javascript">
        
        var extender;
        var _stringTestValue = 'Something';;
        var _objectTestValue = document.createElement('span');

        // SetUp and TearDown
        function setUp() {
            extender = new Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender();
        }

        function tearDown() {
            extender = null;
        }

        //Tests:
        
        // --------------------- property tests ----------------------
        
        function test_get_expanded_returnsTheValueOfPrivateBackingField() {
            extender._expanded = true;
            assertEvaluatesToTrue(extender.get_expanded());
        }
        
        function test_set_expanded_setsTheValueOfPrivateBackingField() {
            extender.set_expanded(true);
            assertEvaluatesToTrue(extender._expanded);
        }

        function test_get_expandElement_returnsTheValueOfPrivateBackingField() {
            extender._expandElement = _objectTestValue;
            assertEquals(_objectTestValue, extender.get_expandElement());
        }
        
        function test_set_expandElement_setsTheValueOfPrivateBackingField() {
            extender.set_expandElement(_objectTestValue);
            assertEquals(_objectTestValue, extender._expandElement);
        }
        
        function test_get_expandText_returnsTheValueOfPrivateBackingField() {
            extender._expandText = _stringTestValue;
            assertEquals(_stringTestValue, extender.get_expandText());
        }
        
        function test_set_expandText_setsTheValueOfPrivateBackingField() {
            extender.set_expandText(_stringTestValue);
            assertEquals(_stringTestValue, extender._expandText);
        }

        function test_get_expandTarget_returnsTheValueOfPrivateBackingField() {
            extender._expandTarget = _objectTestValue;
            assertEquals(_objectTestValue, extender.get_expandTarget());
        }

        function test_set_expandTarget_setsTheValueOfPrivateBackingField() {
            extender.set_expandTarget(_objectTestValue);
            assertEquals(_objectTestValue, extender._expandTarget);
        }

    </script>
</asp:Content>
