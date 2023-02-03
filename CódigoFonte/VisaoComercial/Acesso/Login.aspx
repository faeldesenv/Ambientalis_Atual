<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Acesso_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Sustentar</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../imagens/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../imagens/favicon.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="../imagens/favicon.png" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="logo">
        </div>
        <div style="width: 100%; height: 10px;">
        </div>
        <div id="header_login">
            LOGIN - COMERCIAL
        </div>
        <div id="box_login">
            <p>
                <label class="req">
                    Usuário:
                </label>
                <br />
                <asp:TextBox ID="tbxLogin" runat="server" CssClass="tbxAcesso" ValidationGroup="rfv"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxLogin"
                    ErrorMessage="*" ValidationGroup="rfv"></asp:RequiredFieldValidator>
            </p>
            <p>
                <label class="req">
                    Senha:
                </label>
                <br />
                <asp:TextBox ID="tbxSenha" runat="server" CssClass="tbxAcesso" TextMode="Password"
                    ValidationGroup="rfv"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxSenha"
                    ErrorMessage="*" ValidationGroup="rfv"></asp:RequiredFieldValidator>
            </p>
            <p style="text-align: right; margin-left: 40px;">
                <asp:Button ID="btnLogin" runat="server" Text="Acessar" CssClass="botao_login" OnClick="btnLogin_Click"
                    ValidationGroup="rfv" />
            </p>
        </div>
        <div style="margin-top: 3px; font-size: xx-small; text-align: center; color: #999999;">
            Sustentar Sistema © - Todos os direitos reservados</div>
    </div>
    </form>
</body>
</html>
