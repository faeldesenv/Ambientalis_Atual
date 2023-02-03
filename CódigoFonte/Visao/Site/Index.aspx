<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Site_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            addDrag();
        });

        function addDrag() {
            $("#<%= divPopAviso.ClientID %>").draggable({ containment: "#content", cancel: "#divPopAvisoCorpo" });
        }
    </script>
    <style>
        .containet_index
        {
            width: 28%;
            height: 400px;
            background-color: #f4f7f6;
            border-radius: 5px;
            float: left;
            margin: 5px;
            padding: 15px;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="containet_index">
        <div class="barra_titulo" style="background-color: #1c5e55; margin-bottom: 3px">
            Notificações de Hoje
        </div>
        <div>
            <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="grid_notificacoes">
                        <asp:DataGrid ID="dgr" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnEditCommand="dgr_EditCommand"
                            OnItemDataBound="dgr_ItemDataBound" OnPageIndexChanged="dgr_PageIndexChanged"
                            Width="100%" OnInit="dgr_Init">
                            <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                                Mode="NumericPages" NextPageText="" CssClass="GridPager" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Tipo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoTemplate" runat="server"
                                            Text="<%# bindingTipoTemplate(Container.DataItem) %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Empresa / Grupo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpresaGrupoEconomico" runat="server"
                                            Text="<%# bindingEmpresaGrupoEconomico(Container.DataItem) %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Visualizar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnVisualizar" runat="server" CommandName="Edit" ImageUrl="~/imagens/visualizar20x20.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o e-mail que será enviado')" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="55px" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                        HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                VerticalAlign="Top" CssClass="GridHeader" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                        <asp:Label ID="lblQtdNotificacoes" Style="margin-top: 6px;" runat="server"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="containet_index">
        <div class="barra_titulo" style="background-color: #1c5e55; margin-bottom: 3px">
            Eventos baixados do Site do DNPM
        </div>
        <div>
            <asp:DataGrid ID="dgrProcessos" runat="server" AutoGenerateColumns="False"
                CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnEditCommand="dgr_EditCommand"
                OnItemDataBound="dgr_ItemDataBound" OnPageIndexChanged="dgr_PageIndexChanged"
                Width="100%" OnInit="dgr_Init">
                <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                    Mode="NumericPages" NextPageText="" CssClass="GridPager" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="GetNumeroProcessoComMascara" HeaderText="Processo" Visible="True"></asp:BoundColumn>
                    <asp:BoundColumn DataField="GetQuantidadeDeEventosNaoModificados" HeaderText="Nº de Atualizações" Visible="True"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Visualizar">
                        <ItemTemplate>
                            <asp:HyperLink ID="ibtnVisualizar0" runat="server" ImageUrl="~/imagens/visualizar20x20.png" NavigateUrl='<%# "~/DNPM/Eventos.aspx"+ this.MontarParametro(Container.DataItem) %>' OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abrir atualizações do processo')" />
                        </ItemTemplate>
                        <HeaderStyle Width="55px" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                    VerticalAlign="Top" CssClass="GridHeader" />
                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
        </div>
    </div>
    <div class="containet_index" style="background-color: white; float: right; width: 33%">
        <img src="../imagens/logo_index.jpg" style="width: 100%; margin-top: 50px;" />
        <div style="text-align: right; margin-top: 88px;">
            <div id="email_seguranca_usuario" style="font-size: 14px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblEmailSeguranca" Style="font-size: 12px" runat="server"></asp:Label><br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="btnAlterarEmailSeguranca" runat="server" Style="margin-top: 15px" Text="Alterar E-mail de Segurança" CssClass="Button" OnClick="btnAlterarEmailSeguranca_Click" OnInit="btnAlterarEmailSeguranca_Init" />

            </div>

            <div id="divPopAviso" runat="server" style="background-color: #d4dfdb; position: absolute; right: 10px; *margin-top: 10px; top: 10px; width: 400px; z-index: 1000; padding: 5px; border-radius: 5px; behavior: url(../Utilitarios/htc/PIE.htc);">
                <div style="float: right;" id="divPopAvisoFechar" onclick='javascript:$("#<%= divPopAviso.ClientID %>").hide();'>
                    <img alt="." src="../imagens/x.png" style="height: 20px; cursor: pointer" title="Fechar" />
                </div>
                <div style="float: left;">
                    <img alt="AVISO" src="../imagens/icone_aviso.png" />
                </div>
                <div style="float: left; margin-left: 5px; margin-top: 7px; font-size: small;">
                    <b>SUSTENTAR AVISA</b><br />
                </div>
                <div style="border-top: 2px solid #687a60; width: 100%; float: left;"></div>

                <div id="divPopAvisoCorpo" style="float: left; background-color: #eff8f5; behavior: url(../Utilitarios/htc/PIE.htc); border-radius: 4px; padding: 4px; text-align: justify; margin-top: 5px;">
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:Label ID="lblAviso" runat="server"></asp:Label>
                    </asp:Panel>
                </div>

            </div>
            <asp:Label ID="lblAuxSenha" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="lblAuxSenha_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
                DynamicServicePath="" Enabled="True" PopupControlID="popUpTrocarSenha" TargetControlID="lblAuxSenha"
                CancelControlID="btnTrocarDepois">
            </asp:ModalPopupExtender>

            <asp:Label ID="lblAuxEmailSeg" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="lblAuxEmailSeg_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
                DynamicServicePath="" Enabled="True" PopupControlID="popSetor" TargetControlID="lblAuxEmailSeg"
                CancelControlID="fecharPopSetor">
            </asp:ModalPopupExtender>

            <asp:Label ID="Label1" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="LinkButton1_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" DynamicServicePath="" Enabled="True"
                PopupControlID="divPopUpAtualizacao" TargetControlID="Label1">
            </asp:ModalPopupExtender>
        </div>
    </div>
    <div style="clear: both"></div>


</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
    Sistema de Gestão Ambiental e Minerário.
    <asp:Label ID="lblAux" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="modal_visualizar_Email" runat="server" BackgroundCssClass="simplemodal"
        CancelControlID="div_fechar_popup_visualizar_Email" DynamicServicePath="" Enabled="True"
        PopupControlID="pop_up_visualizar_Email" TargetControlID="lblAux">
    </asp:ModalPopupExtender>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
    <div id="popUpTrocarSenha">
        <div style="width: 340px; height: auto; font-family: Tahoma;">
            <div id="header_login" style="font-family: Tahoma;">
                Alteração de Senha
            </div>
            <div id="box_login" style="padding: 10px;">
                <div style="font-family: Tahoma; font-size: 14px; padding-top: 10px; padding-bottom: 10px; text-align: center;">
                    Primeiro Acesso. Para sua segurança, sugerimos a alteração de sua senha.
                </div>
                <label class="req">
                    Usuário:
                </label>
                <br />
                <asp:UpdatePanel ID="upLogin" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="tbxLogin" runat="server" CssClass="tbxAcesso" ValidationGroup="rfv"
                            ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <p style="margin-top: 10px;">
                    <label class="req">
                        Senha:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxSenha"
                            ErrorMessage="*" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                    </label>
                    <br />
                    <asp:TextBox ID="tbxSenha" runat="server" CssClass="tbxAcesso" TextMode="Password"
                        ValidationGroup="rfv"></asp:TextBox>
                </p>
                <p style="margin-top: 10px;">
                    <label class="req">
                        Confirmação de Senha:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="tbxConfirmacaoSenha" ErrorMessage="*" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                    </label>
                    <br />
                    <asp:TextBox ID="tbxConfirmacaoSenha" runat="server" CssClass="tbxAcesso" TextMode="Password"
                        ValidationGroup="rfv"></asp:TextBox>
                </p>
                <p style="text-align: right; margin-top: 15px;">
                    &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <input id="btnTrocarDepois" type="button" value="Trocar Depois" class="botao_login" />
                            <asp:Button ID="btnLogin" runat="server" Style="float: right" Text="Alterar Senha"
                                CssClass="botao_login" ValidationGroup="rfv" OnClick="btnLogin_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </p>
            </div>
        </div>
    </div>
    <div id="popSetor" class="pop_up" style="width: 500px; display: none;">
        <div id="fecharPopSetor" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            E-mail de Segurança
        </div>
        <div>
            <asp:UpdatePanel ID="UPPopSetor" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%" cellspacing="5">
                        <tr>
                            <td align="right" width="15%">E-mail*:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbxEmailSeguranca" runat="server" Width="300px" TextMode="SingleLine" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Obrigatório!"
                                    ValidationGroup="rfvSalvarSetor" ControlToValidate="tbxEmailSeguranca" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnSalvarEmailSeguranca" runat="server" CssClass="Button" Text="Salvar" ValidationGroup="rfvSalvarSetor" OnClick="btnSalvarEmailSeguranca_Click" OnInit="btnSalvarEmailSeguranca_Init" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="pop_up_visualizar_Email" class="pop_up" style="width: 700px; max-height: 600px; display: block">
        <div id="div_fechar_popup_visualizar_Email" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Visualizar E-Mail
        </div>
        <asp:UpdatePanel ID="upCamposVisualizar" runat="server" ChildrenAsTriggers="False"
            UpdateMode="Conditional">
            <ContentTemplate>
                <div id="campos_visualizar_Email">
                    <table style="width: 100%">
                        <tr>
                            <td align="right" style="width: 20%">Empresa / Grupo:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbxEmpresaGrupoVisualizar" runat="server" ReadOnly="True"
                                    Width="80%" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">E-mails:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbxEmailsVisualizar" runat="server" ReadOnly="True"
                                    Width="99%" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Assunto:
                            </td>
                            <td align="left">
                                <div style="float: left; width: 65%">
                                    <asp:TextBox ID="tbxAssuntoVisualizar" runat="server" ReadOnly="True"
                                        Width="100%" CssClass="TextBox"></asp:TextBox>
                                </div>
                                <div style="float: right; width: 30%">
                                    <asp:Button ID="btnEnviarVisualizar" runat="server" CssClass="Button" OnClick="btnEnviarVisualizar_Click"
                                        Text="Enviar" Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Envia a notificação aberta')" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div id="campo_Enviar_EMails" style="margin: 10px; text-align: right;">
                        <asp:HiddenField ID="hfNotificacao" runat="server" Value="0" />
                    </div>
                    E-mail:
                    <hr />
                    <div id="corpo_Email" class="containerEmail" style="overflow: auto; width: 100%; height: 380px">
                        <asp:Label ID="lblEmailVisualizar" runat="server"></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnEnviarVisualizar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <uc1:MBOX ID="MBOX1" runat="server" />
</asp:Content>
