<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitializeMessage.aspx.cs"
    MasterPageFile="~/Tests/ComponentTests/SitefinityJS/Message/MessageTest.Master" Inherits="SitefinityWebApp.ComponentTests.SitefinityJS.Message.InitializeMessage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="testTitle" runat="server">
    Initialize Message Basic
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="testDescription" runat="server">
    <p>
        This is a jQuery plug-in for displaying messages in the UI. The plugin has the abilities
        to do following:
    </p>
    <ul>
        <li>Show a positive message (e.g. something in green)</li>
        <li>Show a negative message (e.g. an error, something in red)</li>
        <li>Show a neutral message (e.g. something in gray; something that informs a user)</li>
    </ul>
    <p>
        Note:&nbsp;Styling for the states is available for designers in MessageStates.css<br />
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="testCode" runat="server">
    <pre> 
   <font color="#2040a0"><strong><font color="4444FF">&lt;div</font> id=<font color="#008000">&quot;divMessage&quot;</font><font color="4444FF">&gt;
    </font>The name cannot be empty!</strong></font>
    <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong>/div&gt;</font></strong></font> 
</pre>
    <pre> 
   <font color="#444444">/* test code */</font> 
    
    // initialization: automatically hides the div element
    $(<font color="#008000">&quot;#<font color="#2040a0"><strong><font color="#008000">divMessage</font></strong></font>&quot;</font>).message();
      
    // some event happens 
    <span style="color: #4444FF">switch</span> (selectedState) 
    { 
      <span style="color: #4444FF">case</span> <span style="color: #800000">&#39;Normal</span>&#39;: $(<span 
        style="color: #800000">&quot;#divMessage&quot;</span>).message(<span 
        style="color: #800000">&#39;normal&#39;</span>) break; 
      <span style="color: #4444FF">case</span> <span style="color: #800000">&#39;Positive&#39;</span>: $(<span 
        style="color: #800000">&quot;#divMessage&quot;</span>).message(<span 
        style="color: #800000">&#39;positive&#39;</span>) break; 
      <span style="color: #4444FF">case</span> <span style="color: #800000">&#39;Negative&#39;</span>: $(<span 
        style="color: #800000">&quot;#divMessage&quot;</span>).message(<span 
        style="color: #800000">&#39;negative&#39;</span>) break; 
      <span style="color: #4444FF">default:</span> $(<span style="color: #800000">&quot;#divMessage&quot;</span>).message(); 
    }</pre>
    <pre>
</pre>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="test" runat="server">
    <ul id="myForm" style="list-style-type: none;">
        <li>
            <div id="divMessage">The name cannot be empty</div>
        </li>
        <li>
            <select id="messageTypes" >
                <option>Initialize State</option>
                <option>Positive State</option>
                <option>Negative State</option>
                <option>Normal State</option>
            </select>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="buttons" runat="server">
    <input id="buttonApplyMessageType" type="button" value="Apply Message State" />
    <script type="text/javascript">

        /* test code */
        $("#buttonApplyMessageType").click(function () {
            var selectedState = $("#messageTypes option:selected").text();
            switch (selectedState) {
                case 'Normal State':
                    $("#divMessage").message('normal')
                    break;
                case 'Positive State':
                    $("#divMessage").message('positive')
                    break;
                case 'Negative State':
                    $("#divMessage").message('negative')
                    break;
                default:
                    $("#divMessage").message();
            }
        });

    </script>
</asp:Content>
