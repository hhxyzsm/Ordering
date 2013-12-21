<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <asp:image id="imgPhoto" runat="server" ImageUrl="Default2.aspx"></asp:image> 
    <%--<asp:image id="Image1" runat="server" ImageUrl="Default2.aspx"></asp:image> --%>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <%--<asp:FileUpload ID="FileUpload2" runat="server" /><br />
        <asp:FileUpload ID="FileUpload3" runat="server" /><br />
        <asp:FileUpload ID="FileUpload4" runat="server" /><br />
        <asp:FileUpload ID="FileUpload5" runat="server" /><br />
        <asp:FileUpload ID="FileUpload6" runat="server" /><br />
        <asp:FileUpload ID="FileUpload7" runat="server" /><br />
        <asp:FileUpload ID="FileUpload8" runat="server" /><br />
        <asp:FileUpload ID="FileUpload9" runat="server" /><br />
        <asp:FileUpload ID="FileUpload10" runat="server" /><br />
        <asp:FileUpload ID="FileUpload11" runat="server" /><br />--%>

        
        <asp:Button id="btnAdd" name="btnAdd" runat="server" Text="上传" OnClick="btnAdd_Click"></asp:Button> 
    </div>
    </form>
</body>
</html>
