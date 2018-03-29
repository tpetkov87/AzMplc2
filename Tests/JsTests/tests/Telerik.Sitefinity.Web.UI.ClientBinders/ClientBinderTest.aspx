<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master"
    AutoEventWireup="true" CodeBehind="ClientBinderTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.ClientBinders.ClientBinderTest" %>

<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity"
    TagPrefix="sitefinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resources" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.Scripts.ClientBinder.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    

    <script type="text/javascript">
        var binder;
        var mockControl;
        // SetUp and TearDown
        function setUp() {
            binder = new Telerik.Sitefinity.Web.UI.ClientBinder();
            mockControl = new MockControl();
        }

        function tearDown() {
            binder = null;
            mockControl = null;
        }

        //Tests:

        // --------------------- property tests ----------------------
        // Test fails with unexpected call. Probably should be redone with
        // stubbing mock functions.
//        function test_AssignCommands_Calls_All_assign_methods() {
//            var mockElement = document.createElement('div');

//            var binderMock = mockControl.createMock(Telerik.Sitefinity.Web.UI.ClientBinder);

//            binderMock.expects()._assignDeleteCommand();
//            binderMock.expects()._assignEditCommand();
//            binderMock.expects()._assignSelectCommand();
//            binderMock.expects()._assignCancelCommand();
//            binderMock.expects()._assignSaveCommand();
//            binderMock.expects()._assignCustomCommand();

//            binderMock.AssignCommands(mockElement);
//            
//            mockControl.verify();
//        }
       
    </script>

</asp:Content>
