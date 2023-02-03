<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="Notificacoes.aspx.cs" Inherits="Notificacao_Notificacoes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Envio de notificações</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_envio_notificacoes" class="container">
        <div id="calendario" class="div_container" style="width: 25%; padding: 5px">
            Selecione o dia:
            <asp:UpdatePanel ID="upCalendario" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Calendar ID="clDiasNotificacao" runat="server" Font-Bold="True" Height="200px"
                        OnDayRender="clDiasNotificacao_DayRender" OnSelectionChanged="clDiasNotificacao_SelectionChanged"
                        Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Clique no dia desejado para visualizar as notificações programadas')" >
                        <SelectedDayStyle ForeColor="#CCCCCC" />
                    </asp:Calendar>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="margin: 10px">
                <div style="float: left; height: 13px; width: 20px; background-color: #cc3300; margin-right: 2px">
                </div>
                Dias com Notificações ainda não enviadas!</div>
            <div style="margin: 10px">
                <div style="float: left; height: 13px; width: 20px; background-color: #93DB70; margin-right: 2px">
                </div>
                Dias com Notificações enviadas!</div>
        </div>
        <div id="campos_envio" class="div_container" style="width: 65%; margin-left: 1%;
            padding: 5px">
            <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div id="filtros_notificacoes">
                    <div style="float: left; height: 20px">
                        Notificações:<asp:Label ID="lblDia" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;</div>
                    </div>
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
                            <asp:BoundColumn DataField="Data" HeaderText="Data" DataFormatString="{0:d}"></asp:BoundColumn>                            
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
                            <asp:TemplateColumn HeaderText="Enviado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnviado" runat="server" Text="<%# bindingEnviado(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="75px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Visualizar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnVisualizar" runat="server" CommandName="Edit" ImageUrl="~/imagens/visualizar20x20.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o e-mail que será enviado')"  />
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
                    <asp:Label ID="lblQtdNotificacoes" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="clDiasNotificacao" EventName="SelectionChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="opcoes" style="clear: both; text-align: right; margin: 15px">
        <asp:Label ID="lblAux" runat="server"></asp:Label>
        <asp:ModalPopupExtender ID="modal_visualizar_Email" runat="server" BackgroundCssClass="simplemodal"
            CancelControlID="div_fechar_popup_visualizar_Email" DynamicServicePath="" Enabled="True"
            PopupControlID="pop_up_visualizar_Email" TargetControlID="lblAux">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="pop_up_visualizar_Email" class="pop_up" style="width: 700px; max-height: 500px;
        display: block">
        <div id="div_fechar_popup_visualizar_Email" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Visualizar E-Mail</div>
        <asp:UpdatePanel ID="upCamposVisualizar" runat="server" ChildrenAsTriggers="False"
            UpdateMode="Conditional">
            <ContentTemplate>
                <div id="campos_visualizar_Email">
                    <table style="width: 100%">
                        <tr>
                            <td align="right" style="width: 20%">
                                Empresa / Grupo:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbxEmpresaGrupoVisualizar" runat="server" ReadOnly="True" 
                                    Width="80%" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                E-mails:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbxEmailsVisualizar" runat="server" ReadOnly="True" 
                                    Width="99%" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Assunto:
                            </td>
                            <td align="left">
                                <div style="float: left; width: 65%">
                                    <asp:TextBox ID="tbxAssuntoVisualizar" runat="server" ReadOnly="True" 
                                        Width="100%" CssClass="TextBox"></asp:TextBox>
                                </div>
                                <div style="float: right; width: 30%">
                                    <asp:Button ID="btnEnviarVisualizar" runat="server" CssClass="Button" OnClick="btnEnviarVisualizar_Click"
                                        Text="Enviar" Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Envia a notificação aberta')"  />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div id="campo_Enviar_EMails" style="margin: 10px; text-align: right">
                        <asp:HiddenField ID="hfNotificacao" runat="server" Value="0" />
                    </div>
                    E-mail:
                    <hr />
                    <div id="corpo_Email" class="containerEmail">
                        <asp:Label ID="lblEmailVisualizar" runat="server"></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnEnviarVisualizar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
