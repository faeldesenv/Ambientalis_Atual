<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="ManterUsuario.aspx.cs" Inherits="Usuario_ManterUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {
            $('#<%=tbxConfirmarSenha.ClientID%>').focusout(function () { VerificarConfirmacaoSenha($('#<%=tbxSenha.ClientID%>'), $('#<%=tbxConfirmarSenha.ClientID%>')); });
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
        Cadastro de usuários</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_cadastro_usuarios">
        <asp:UpdatePanel ID="upAdministradorCliente" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td id="descricao_Administrador" runat="server" align="right" style="width: 30%">
                            Administrador*:
                        </td>
                        <td id="campo_Administrador" runat="server" align="left">
                            <asp:DropDownList ID="ddlAdministrador" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                OnSelectedIndexChanged="ddlAdministrador_SelectedIndexChanged" Width="50%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Escolhe se será criado um usuário  para o administrador logus ou ambientalis')">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td id="descricao_cliente" runat="server" align="right" style="width: 30%">
                            Grupo Econômico*:
                        </td>
                        <td id="campo_cliente" runat="server" align="left">
                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="DropDownList" 
                                Width="50%" onmouseout="tooltip.hide();" 
                                onmouseover="tooltip.show('Escolhe em qual grupo econômico será criado o usuário')" 
                                AutoPostBack="True" onselectedindexchanged="ddlCliente_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table style="width: 100%">
            <tr>
                <td align="right" style="width: 30%">
                    &nbsp;</td>
                <td align="left">
                    <asp:CheckBox ID="chkUsuarioAdministrador" runat="server" 
                        Text="Usuário Administrador (Usuário com permissão de cadastrar e controlar as permissões de outros usuários)" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%">
                    Nome*:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNome"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório!!" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Login*:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxLogin" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxLogin"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório!!" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="label_senha" style="margin-top:3px" runat="server">
                     <table width="100%">
                         <tr>
                             <td width="30%">
                                 &nbsp;</td>
                             <td>
                    <asp:Label ID="lblInformativoSenha" runat="server" 
                        Text="A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras" 
                        style="font-weight: 700; font-size: small"></asp:Label>
                             </td>
                         </tr>
                     </table>
                    </div></td>
            </tr>
            <tr>
                <td id="descricao_campo_alterarSenha" align="right" runat="server">
                    Senha:
                </td>
                <td id="campo_alterarSenha" runat="server" align="left">
                    <asp:Button ID="btnAlteracaoSenha" runat="server" Text="Alterar Senha" OnPreRender="btnAlteracaoSenha_PreRender"
                        OnClick="btnAlteracaoSenha_Click" OnInit="btnAlteracaoSenha_Init" />
                </td>
            </tr>
            <tr>
                <td id="descricao_campo_senha" runat="server" align="right">
                    Senha*:
                </td>
                <td id="campo_senha" runat="server" align="left">
                    <asp:TextBox ID="tbxSenha" runat="server" CssClass="TextBox" Width="50%" TextMode="Password" onmouseout="tooltip.hide();" onmouseover="tooltip.show('A senha deve ter no minímo 6 digitos, com no minímo 2 números e 2 letras.')"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxSenha"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório!!" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="descricao_campo_confirmar_senha" runat="server" align="right">
                    Confirmar Senha*:
                </td>
                <td id="campo_confirmar_senha" runat="server" align="left">
                    <asp:TextBox ID="tbxConfirmarSenha" runat="server" CssClass="TextBox" Width="50%"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxConfirmarSenha"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório!!" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    E-mail:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxEmail" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td align="right">
                </td>
                <td align="left">
                    *Campos Obrigatórios
                    <asp:Label ID="lblAux" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="modal_alteracao_senha" runat="server" BackgroundCssClass="simplemodal"
                        CancelControlID="div_fechar_popup_alteracao_Senha" DynamicServicePath="" Enabled="True"
                        PopupControlID="alteracao_Senha" TargetControlID="lblAux">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:HiddenField ID="hfId" runat="server" Value="&quot;0&quot;" />
                </td>
                <td align="left">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="Button" OnClick="btnSalvar_Click"
                        Text="Salvar" Width="120px" ValidationGroup="rfv" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva o novo usuário ou salva as alterações feitas no usuário')"/>
                    <asp:Button ID="btnNovo" runat="server" CssClass="Button" OnClick="btnNovo_Click"
                        Text="Novo" Width="120px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os campos para cadastro de usuário')"/>
                    <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" OnClick="btnExcluir_Click"
                        Text="Excluir" Width="120px" OnPreRender="btnExcluir_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o usuário')" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="popups">
    <div id="popups">
        <div id="alteracao_Senha" class="pop_up" style="width: 500px">
            <div id="div_fechar_popup_alteracao_Senha" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Alteração de Senha</div>
            <div id="campos_alteracao_senha">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" align="center">
                    <asp:Label ID="lblInformativoSenha02" runat="server" 
                        Text="A nova senha deve ter no mínimo 6 digitos, com no minímo 2 números e 2 letras" 
                        style="font-weight: 700; font-size: x-small"></asp:Label>
                             </td>
                    </tr>
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
                                CssClass="TextBox" onmouseout="tooltip.hide();" onmouseover="tooltip.show('A nova senha deve ter no minímo 6 digitos, com no minímo 2 números e 2 letras.')"></asp:TextBox>
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
                                ValidationGroup="rfvAlterarSenha" OnClick="btnAlterarSenha_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
