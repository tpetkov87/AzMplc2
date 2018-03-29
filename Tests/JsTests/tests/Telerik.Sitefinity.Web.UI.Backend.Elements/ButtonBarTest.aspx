<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master" CodeBehind="ButtonBarTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBarTest" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>
        
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
        <sitefinity:ResourceLinks ID="resources" runat="server">
            <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.WidgetBar.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.ButtonBar.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />

        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.Scripts.IAsyncCommandSender.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />    
         <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />        
            
    </sitefinity:ResourceLinks>

    
    <script type="text/javascript">
        var buttonBar;
        var mockElement = document.createElement('div');
 
        // SetUp and TearDown
        function setUp() {
            buttonBar = new Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar(mockElement);
        }

        function tearDown() {
            buttonBar = null;
        }

        //Tests:
        
        // --------------------- events tests ----------------------
        function test_add_onStartProcessing_properlySubscribes() {
            var mockDelegate = createMockDelegate();
            buttonBar.add_onStartProcessing(mockDelegate);
            assertDelegateAdded(buttonBar, mockDelegate, 'onStartProcessing');
        }

        function test_remove_onStartProcessing_properlyUnsubscribes() {
            var mockDelegate = createMockDelegate();
            buttonBar.add_onStartProcessing(mockDelegate);
            assertDelegateAdded(buttonBar, mockDelegate, 'onStartProcessing');
            buttonBar.remove_onStartProcessing(mockDelegate);
            assertDelegateRemoved(buttonBar, mockDelegate, 'onStartProcessing');
        }

        function test_remove_onEndProcessing_properlyUnsubscribes() {
            var mockDelegate = createMockDelegate();
            buttonBar.add_onEndProcessing(mockDelegate);
            assertDelegateAdded(buttonBar, mockDelegate, 'onEndProcessing');
            buttonBar.remove_onEndProcessing(mockDelegate);
            assertDelegateRemoved(buttonBar, mockDelegate, 'onEndProcessing');
        }

        function test_onStartProcessing_FiresAnEvent() {
            // event fired flag
            var eventFired = false;
            // subscribe to the event
            buttonBar.add_onStartProcessing(function() {
                // when called, change the value of the event flat
                eventFired = true;
            });
            var mockDelegate = createMockDelegate();

            var commandEventArgs = {
                get_commandName: function() { return "commandName: "; },
                get_commandArgument: function() { return null; }
            };
      
            // force the firing of the event
            buttonBar._startProcessingHandler(mockDelegate, commandEventArgs);
            // assert that the function that subscribed was called and has modified the flag
            assertEvaluatesToTrue(eventFired);
        }

        function test_onEndProcessing_FiresAnEvent() {
            // event fired flag
            var eventFired = false;
            // subscribe to the event
            buttonBar.add_onEndProcessing(function() {
                // when called, change the value of the event flat
                eventFired = true;
            });
            var mockDelegate = createMockDelegate();

            var commandEventArgs = {
                get_commandName: function() { return "commandName: "; },
                get_commandArgument: function() { return null; }
            };

            // force the firing of the event
            buttonBar._endProcessingHandler(mockDelegate, commandEventArgs);
            // assert that the function that subscribed was called and has modified the flag
            assertEvaluatesToTrue(eventFired);
        }
    </script>
</asp:Content>
