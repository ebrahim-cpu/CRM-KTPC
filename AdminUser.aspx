<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUser.aspx.cs" Inherits="CRM.AdminUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" PageSize="25">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="EmpId" HeaderText="EmpId" SortExpression="EmpId" />
                    <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                    <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                    <asp:BoundField DataField="Divisyen" HeaderText="Divisyen" SortExpression="Divisyen" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                    <asp:CheckBoxField DataField="IsUser" HeaderText="IsUser" SortExpression="IsUser" />
                    <asp:CheckBoxField DataField="IsAdmin" HeaderText="IsAdmin" SortExpression="IsAdmin" />
                    <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" />
                    <asp:CheckBoxField DataField="IsHead" HeaderText="IsHead" SortExpression="IsHead" />
                    <asp:CheckBoxField DataField="IsDelete" HeaderText="IsDelete" SortExpression="IsDelete" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" DeleteCommand="DELETE FROM [Pekerja] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Pekerja] ([EmpId], [EmpName], [Username], [Divisyen], [Email], [Department], [IsUser], [IsAdmin], [DisplayName], [IsHead], [IsDelete]) VALUES (@EmpId, @EmpName, @Username, @Divisyen, @Email, @Department, @IsUser, @IsAdmin, @DisplayName, @IsHead, @IsDelete)" SelectCommand="SELECT [Id], [EmpId], [EmpName], [Username], [Divisyen], [Email], [Department], [IsUser], [IsAdmin], [DisplayName], [IsHead], [IsDelete] FROM [Pekerja] ORDER BY [EmpName]" UpdateCommand="UPDATE [Pekerja] SET [EmpId] = @EmpId, [EmpName] = @EmpName, [Username] = @Username, [Divisyen] = @Divisyen, [Email] = @Email, [Department] = @Department, [IsUser] = @IsUser, [IsAdmin] = @IsAdmin, [DisplayName] = @DisplayName, [IsHead] = @IsHead, [IsDelete] = @IsDelete WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="EmpId" Type="String" />
                <asp:Parameter Name="EmpName" Type="String" />
                <asp:Parameter Name="Username" Type="String" />
                <asp:Parameter Name="Divisyen" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Department" Type="String" />
                <asp:Parameter Name="IsUser" Type="Boolean" />
                <asp:Parameter Name="IsAdmin" Type="Boolean" />
                <asp:Parameter Name="DisplayName" Type="String" />
                <asp:Parameter Name="IsHead" Type="Boolean" />
                <asp:Parameter Name="IsDelete" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="EmpId" Type="String" />
                <asp:Parameter Name="EmpName" Type="String" />
                <asp:Parameter Name="Username" Type="String" />
                <asp:Parameter Name="Divisyen" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Department" Type="String" />
                <asp:Parameter Name="IsUser" Type="Boolean" />
                <asp:Parameter Name="IsAdmin" Type="Boolean" />
                <asp:Parameter Name="DisplayName" Type="String" />
                <asp:Parameter Name="IsHead" Type="Boolean" />
                <asp:Parameter Name="IsDelete" Type="Boolean" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
