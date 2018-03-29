<%@ Page Title="SitefinityJS | Form > Initialize Form" 
         Language="C#" 
         MasterPageFile="~/Tests/ComponentTests/SitefinityJS/Form/FormTestMaster.master" 
         AutoEventWireup="true" 
         CodeBehind="InitializeForm.aspx.cs" 
         Inherits="SitefinityWebApp.ComponentTests.SitefinityJS.Form.InitializeForm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="testTitle" runat="server">
    Initialize TextField Basic
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="testDescription" runat="server">
    <p>
        This test verifies Sitefinity jQuery plugin for converting an HTML element into a Sitefinity form.
    </p>
    <p>
        In order for an HTML element to become a Sitefinity field, it must have attribute "data-field". The value of this attribute
        is the name of the data property that field represents.
    </p>
    <div>
        There are three additional attributes that allow any HTML element to have common Sitefinity UI elements:
        <ul>
            <li>
                <strong>data-sf-title</strong> generates a label before the enhanced element and sets its html to the value of the attribute. Sets it's class attribute to "sfTxtLbl", sets the "for" attribute
                to the id of the enhanced element.
            </li>
            <li>
                <strong>data-sf-example</strong> generates a DIV element after the enhanced element and sets its html to the value of the attribute. Sets it's class attribute to "sfExample".
            </li>
            <li>
                <strong>data-sf-description</strong> generates a DIV element after the enhanced example (and optionally after example label, if present) and sets its html to the value of the
                attribute.
            </li>
        </ul> 
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="testCode" runat="server">
    <pre> 
   <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">ul</font> <font color="#2040a0">id=</font><font color="#008000">&quot;myForm&quot;</font> <font color="#2040a0">style=</font><font color="#008000">&quot;list-style-type:none;&quot;</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
        <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">li</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
            <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">input</font> <font color="#2040a0">id=</font><font color="#008000">&quot;textField1&quot;</font> <font color="#2040a0"> 
</font>                   <font color="#2040a0">type=</font><font color="#008000">&quot;text&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-field=</font><font color="#008000">&quot;TextField1&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-sf-title=</font><font color="#008000">&quot;Text field 1 title&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-sf-example=</font><font color="#008000">&quot;Text field 1 example&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-sf-description=</font><font color="#008000">&quot;Text field 1 description&quot;</font> <font color="#2040a0">/</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
        <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/li</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
        <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">li</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
            <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">select</font> <font color="#2040a0">id=</font><font color="#008000">&quot;selectField1&quot;</font><font color="#2040a0"> 
</font>                  <font color="#2040a0">data-field=</font><font color="#008000">&quot;SelectField1&quot;</font><font color="#2040a0"> 
</font>                  <font color="#2040a0">data-sf-title=</font><font color="#008000">&quot;Select field 1 title&quot;</font><font color="#2040a0"> 
</font>                  <font color="#2040a0">data-sf-example=</font><font color="#008000">&quot;Select field 1 example&quot;</font><font color="#2040a0"> 
</font>                  <font color="#2040a0">data-sf-description=</font><font color="#008000">&quot;Select field 1 description&quot;</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
                <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">option</font><font color="4444FF"><strong>&gt;</strong></font></strong></font>Choice 1<font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/option</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
                <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">option</font><font color="4444FF"><strong>&gt;</strong></font></strong></font>Choice 2<font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/option</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
                <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">option</font><font color="4444FF"><strong>&gt;</strong></font></strong></font>Choice 3<font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/option</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
            <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/select</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
        <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/li</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
        <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">li</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
            <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">input</font> <font color="#2040a0">id=</font><font color="#008000">&quot;checkboxField1&quot;</font> <font color="#2040a0"> 
</font>                   <font color="#2040a0">type=</font><font color="#008000">&quot;checkbox&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-field=</font><font color="#008000">&quot;CheckboxField1&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-sf-title=</font><font color="#008000">&quot;Checkbox field 1 title&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-sf-example=</font><font color="#008000">&quot;Checkbox field 1 example&quot;</font><font color="#2040a0"> 
</font>                   <font color="#2040a0">data-sf-description=</font><font color="#008000">&quot;Checkbox field 1 description&quot;</font> <font color="#2040a0">/</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
        <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/li</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
    <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">/ul</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
 
    <font color="#2040a0"><strong><font color="4444FF"><strong>&lt;</strong></font><font color="#2040a0">input</font> <font color="#2040a0">id=</font><font color="#008000">&quot;executeTest1&quot;</font> <font color="#2040a0">type=</font><font color="#008000">&quot;button&quot;</font> <font color="#2040a0">value=</font><font color="#008000">&quot;Execute&quot;</font> <font color="#2040a0">/</font><font color="4444FF"><strong>&gt;</strong></font></strong></font> 
</pre> 
       <pre> 
   <font color="#444444">/* test code */</font> 
    $(<font color="#008000">&quot;#executeTest1&quot;</font>).<font color="a52a2a"><strong>click</strong></font>(function () <font color="4444FF"><strong>{</strong></font> 
        $(<font color="#008000">&quot;#myForm&quot;</font>).sf().<font color="#2040a0"><strong>form</strong></font>()<font color="4444FF">;</font> 
    <font color="4444FF"><strong>}</strong></font>)<font color="4444FF">;</font> 
</pre>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="test" runat="server">
    
    <ul id="myForm" style="list-style-type:none;">
        <li>
            <input id="textField1" 
                   type="text"
                   data-field="TextField1"
                   data-sf-title="Text field 1 title"
                   data-sf-example="Text field 1 example"
                   data-sf-description="Text field 1 description" />
        </li>
        <li>
            <select id="selectField1"
                  data-field="SelectField1"
                  data-sf-title="Select field 1 title"
                  data-sf-example="Select field 1 example"
                  data-sf-description="Select field 1 description">
                <option>Choice 1</option>
                <option>Choice 2</option>
                <option>Choice 3</option>
            </select>
        </li>
        <li>
            <input id="checkboxField1" 
                   type="checkbox"
                   data-field="CheckboxField1"
                   data-sf-title="Checkbox field 1 title"
                   data-sf-example="Checkbox field 1 example"
                   data-sf-description="Checkbox field 1 description" />
        </li>
    </ul>
    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="buttons" runat="server">
    <input id="executeTest1" type="button" value="Execute" />

    <script type="text/javascript">

    /* test code */
    $("#executeTest1").click(function () {
        $("#myForm").sf().form();
    });

    </script>

</asp:Content>