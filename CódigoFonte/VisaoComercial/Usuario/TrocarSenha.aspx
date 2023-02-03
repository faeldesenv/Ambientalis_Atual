<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="TrocarSenha.aspx.cs" Inherits="Usuario_TrocarSenha" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= rbtnRevendas.ClientID %>').change(function () {
                $('.campo_revenda').show();
            });
            $('#<%= rbtnSupervisor.ClientID %>').change(function () {
                $('.campo_revenda').hide();
            });
            HabilitaRevenda();
        });

        function HabilitaRevenda() {
            if ($('#<%= rbtnRevendas.ClientID %>').prop("checked"))
                $('.campo_revenda').show();
            else
                $('.campo_revenda').hide();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Alterar Senha
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_trocar_senha">
        <asp:Panel ID="pnlTrocaSenha" runat="server" DefaultButton="btnAlterarSenha">
            <table width="100%">
                <tr>
                    <td align="right" width="30%">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Label ID="lblInformativoSenha" runat="server" Text="A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras"
                            Style="font-weight: 700; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="30%">
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlCamposSupervisor" runat="server">
                <table width="100%">
                    <tr>
                        <td align="right" width="30%">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rbtnSupervisor" runat="server" GroupName="tipoUsuario" Text="Supervisor" />&nbsp;&nbsp;
                            <asp:RadioButton ID="rbtnRevendas" runat="server" GroupName="tipoUsuario" Text="Revendas" />
                        </td>
                    </tr>
                    <tr class="campo_revenda">
                        <td align="right" width="30%">
                            Revenda*:
                        </td>
                        <td align="left">
                            <asp:DropDownList CssClass="DropDownList" Width="205px" ID="ddlRevenda" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td align="right" width="30%">
                        Nova Senha*:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbxNovaSenha" runat="server" CssClass="TextBox" Width="200px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Obrigatório!"
                            ValidationGroup="rfvAlterar" ControlToValidate="tbxNovaSenha"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Confirmação da Nova Senha*:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbxConfirmaNovaSenha" runat="server" CssClass="TextBox" Width="200px"
                            TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Obrigatório!"
                            ValidationGroup="rfvAlterar" ControlToValidate="tbxConfirmaNovaSenha"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td align="left">
                        *Campos Obrigatórios</td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAlterarSenha" runat="server" CssClass="Button" Text="Alterar Senha"
                                    ValidationGroup="rfvAlterar" OnClick="btnAlterarSenha_Click" />
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
