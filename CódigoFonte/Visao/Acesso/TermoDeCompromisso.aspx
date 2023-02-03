<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TermoDeCompromisso.aspx.cs"
    Inherits="Acesso_TermoDeCompromisso" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {
            $('#<%=chkAceite.ClientID %>').click(function () { HabilitarDesabilitarCampoAceito(); });
        }

        function HabilitarDesabilitarCampoAceito() {
            if ($('#<%=chkAceite.ClientID %>').attr('checked')) {
                $('#<%=btnAceito.ClientID %>').removeAttr('disabled');
            }
            else {
                $('#<%=btnAceito.ClientID %>').attr('disabled', true);
            }
        }
    </script>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="alinhar_center" style=" width:80%; margin:0 auto; margin-top:100px;">
        <div style="text-align: center;">
            <img src="../imagens/logo_login.png" />
        </div>
        <div id="campos_termo_compromisso">
            <span style="font-weight: bold">Termo de Compromisso:</span>
            <div id="termo Compromisso" style="font-size:11pt; margin-top:5px; background-color:#fafafa; border: 1px solid silver; padding: 5px">
                <asp:Label ID="lblTermoCompromisso" runat="server" Width="100%"></asp:Label>
            </div>
            <div id="aceite" style="margin-top:5px;">
                <asp:CheckBox ID="chkAceite" runat="server" Text="Li e concordo com o Termo de Compromisso acima"
                    Style="color: #666666" />
            </div>
            <div id="opcoes" style="margin-top: 10px; text-align: right">
                <asp:HiddenField ID="hfIdGrupoEconomico" runat="server" />
                <asp:Button ID="btnAceito" runat="server" Text="Aceito" OnClick="btnAceito_Click"
                    Enabled="False" />
                <asp:Button ID="btnNaoAceito" runat="server" Text="Não Aceito" OnClick="btnNaoAceito_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
