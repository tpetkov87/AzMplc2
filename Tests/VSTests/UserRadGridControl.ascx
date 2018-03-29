<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserRadGridControl.ascx.cs" Inherits="SitefinityWebApp.VSTests.UserRadGridControl" %>
<asp:Label ID="label" runat="server"></asp:Label>

<telerik:RadGrid ID="RadGrid1" GridLines="None" runat="server" AllowAutomaticDeletes="True"
            AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
            AutoGenerateColumns="False" OnItemUpdated="RadGrid1_ItemUpdated"
            OnItemDeleted="RadGrid1_ItemDeleted" OnItemInserted="RadGrid1_ItemInserted" OnDataBound="RadGrid1_DataBound">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView Width="100%" CommandItemDisplay="TopAndBottom" DataKeyNames="column1" HorizontalAlign="NotSet" AutoGenerateColumns="False">
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                        <ItemStyle CssClass="MyImageButton" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="column1" HeaderText="column1" SortExpression="column1"
                        UniqueName="column1" ColumnEditorID="GridTextBoxColumnEditor1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="column2" HeaderText="column2" SortExpression="column2"
                        UniqueName="column2" ColumnEditorID="GridTextBoxColumnEditor1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="column3" HeaderText="column3" SortExpression="column3"
                        UniqueName="column3" ColumnEditorID="GridTextBoxColumnEditor1">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ConfirmText="Delete this product?" ConfirmDialogType="RadWindow"
                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                        UniqueName="DeleteColumn">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>