<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master"
    AutoEventWireup="true" CodeBehind="AsyncCommandMediatorTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediatorTest" %>

<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity"
    TagPrefix="sitefinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resources" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.Scripts.AsyncCommandMediator.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    

    <script type="text/javascript">
        var mediator;

        // SetUp and TearDown
        function setUp() {
            mediator = new Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator();
            var mockElement = document.createElement('div');
        }

        function tearDown() {
            mediator = null;
        }

        //Tests:

        // --------------------- property tests ----------------------
        function test_get_asyncCommandPairs_returnsTheValueOfPrivateBackingField() {
            mediator._asyncCommandPairs = ['Test'];
            assertArrayEquals(['Test'], mediator.get_asyncCommandPairs());

        }
        function test_set_asyncCommandPairs_setsTheValueOfPrivateBackingField() {
            mediator.set_asyncCommandPairs(['Test']);
            assertArrayEquals(['Test'], mediator._asyncCommandPairs);
        }

        // checks if the initialization sets attaches handlers to add_command and add_onEndProcessing
        function test_initAllPairsSetsEventHandlersToSenderAndReceiver() {
            var add_onEndProcessingCalled = false;
            var add_commandCalled = false;

            var mockSender = {
                add_command: function() { add_commandCalled = true; }
            };


            var mockReceiver = {
                add_onEndProcessing: function() { add_onEndProcessingCalled = true; }
            };

            mediator._asyncCommandPairs = [{ CommandSenderClientId: "test", CommandReceiverClientId: "test"}];
            mediator.get_senderFromPair = function() { return mockSender; }
            mediator.get_receiverFromPair = function() { return mockReceiver; }

            mediator._initAllPairs();
            assertTrue(add_onEndProcessingCalled);
            assertTrue(add_commandCalled);

        }

        function test_commandHandler_Executes_onCommand_On_Receiver() {

            var onCommandCalled = false;
            var commandEventArgs = {
                get_commandName: function() { return "commandName: "; },
                get_commandArgument: function() { return null; }
            };
            var mockReceiver = {
                _onCommand: function() { onCommandCalled = true; }
            };
            mediator._commandHandler("sender", commandEventArgs, mockReceiver);
            assertTrue(onCommandCalled);
        }

        function test_endProcessingHandler_Executes_endProcessingHandler_On_Sender() {
            var endProcessingHandlerCalled = false;
            var mockSender = {
            _endProcessingHandler: function() { endProcessingHandlerCalled = true; }
            };
            mediator._endProcessingHandler("sender", "args", mockSender);
            assertTrue(endProcessingHandlerCalled);
        }
       
    </script>

</asp:Content>
