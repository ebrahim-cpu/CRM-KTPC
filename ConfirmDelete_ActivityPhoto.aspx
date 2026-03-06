<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmDelete_ActivityPhoto.aspx.cs" Inherits="CRM.ConfirmDelete_ActivityPhoto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ConfirmationMessage" runat="server" EnableViewState="false"></asp:Literal>
        </div>
        <asp:Button ID="ConfirmButton" runat="server" Text="Confirm Delete" OnClick="ConfirmButton_Click" />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" />
    </form>
</body>
</html>
