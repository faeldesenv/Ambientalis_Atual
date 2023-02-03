<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="Usuario.aspx.cs" Inherits="Usuario_ManterUsuarioADM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {
            $('#<%=tbxConfirmarNovaSenha.ClientID%>').focusout(function () { VerificarConfirmacaoSenha($('#<%=tbxNovaSenha.ClientID%>'), $('#<%=tbxConfirmarNovaSenha.ClientID%>')); });
        }

        function VerificarConfirmacaoSenha(tbxSenha, tbxConfirmarSenha) {
            if ($(tbxConfirmarSenha).val() != "" && ($(tbxConfirmarSenha).val() != $(tbxSenha).val()))
                alert("A cofirmação não corresponde a senha");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
       alterar senha</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_cadastro_usuarios">
        <table style="width: 100%">
            <tr>
                <td id="descricao_campo_alterarSenha" align="left" runat="server" colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td align="right" width="30%">
                            Senha Antiga*:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbxSenhaAntiga" runat="server" Width="200px" 
                                TextMode="Password" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Obrigatório!"
                                ValidationGroup="rfvAlterarSenha" ControlToValidate="tbxSenhaAntiga" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Nova Senha*:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbxNovaSenha" runat="server" Width="200px" TextMode="Password" 
                                CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Obrigatório!"
                                ValidationGroup="rfvAlterarSenha" ControlToValidate="tbxNovaSenha" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Confirmar Nova Senha*:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbxConfirmarNovaSenha" runat="server" Width="200px" 
                                TextMode="Password" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Obrigatório!"
                                ValidationGroup="rfvAlterarSenha" ControlToValidate="tbxConfirmarNovaSenha" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnAlterarSenha" runat="server" CssClass="Button" Text="Alterar Senha"
                                ValidationGroup="rfvAlterarSenha" OnClick="btnAlterarSenha_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Altera a senha do administrador logado')" />
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:HiddenField ID="hfId" runat="server" Value="&quot;0&quot;" />
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="popups">
    </asp:Content>
