<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisaVencimentosDiversos.aspx.cs" Inherits="Vencimentos_PesquisaVencimentosDiversos" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script type="text/javascript">
    function marcarEmailsGrupo() {
        $('.chkMarcarEmailsGrupo').change(function () {
            var estacochk = new Boolean();
            estadochk = $(this).attr('checked');

            $('.chkEmailsGruposEconomicosCont').children().find('input').each(function () {
                if (estadochk) {
                    $(this).attr('checked', 'checked');
                } else {
                    $(this).removeAttr('checked');
                }
            });
        });
    }

    function marcarEmailsEmpresa() {
        $('.chkMarcarEmailsEmpresa').change(function () {
            var estacochk = new Boolean();
            estadochk = $(this).attr('checked');

            $('.chkEmailsEmpresasCont').children().find('input').each(function () {
                if (estadochk) {
                    $(this).attr('checked', 'checked');
                } else {
                    $(this).removeAttr('checked');
                }
            });
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    Pesquisa de Vencimentos Diversos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="filtros_pesquisa">
    <asp:Label ID="lblExtenderRenovacao" runat="server"></asp:Label>
                            <asp:ModalPopupExtender ID="ModalRenovacaoExtender" runat="server" 
                                BackgroundCssClass="simplemodal" CancelControlID="fecharRenovacao" 
                                PopupControlID="divPopUpRenovacao" TargetControlID="lblExtenderRenovacao">
                            </asp:ModalPopupExtender>
                            <asp:Label ID="lblExtenderAlteracaoStatus" runat="server"></asp:Label>
                            <asp:ModalPopupExtender ID="ModalExtenderAlteracaoRenovacao" runat="server" 
                                BackgroundCssClass="simplemodal" CancelControlID="fecharAlteracaoStatus" 
                                PopupControlID="divPopUpAlteracaoStatus" 
                                TargetControlID="lblExtenderAlteracaoStatus">
                            </asp:ModalPopupExtender>

    <asp:UpdatePanel ID="UPFiltros" runat="server">
        <ContentTemplate>
            <table width="100%">
                    <tr>
                        <td class="labelFiltros" width="10%">
                            Grupo Econômico:
                        </td>
                        <td class="controlFiltros" width="16%">
                            <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList" 
                                Width="95%" AutoPostBack="True" 
                                onselectedindexchanged="ddlGrupoEconomico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="labelFiltros" width="20%">
                            
                                <table width="100%">
                                    <tr>
                                        <td width="15%">
                                            Tipo:
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:DropDownList ID="ddlTipoDiverso" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                                Width="95%" onselectedindexchanged="ddlTipoDiverso_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>                            
                        </td>
                                                
                    </tr>
                    <tr>
                        <td class="labelFiltros">
                            &nbsp;Empresa:
                        </td>
                        <td class="controlFiltros">                            
                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList" 
                                Width="95%" AutoPostBack="True" 
                                onselectedindexchanged="ddlEmpresa_SelectedIndexChanged"></asp:DropDownList>                               
                        </td>
                        <td class="labelFiltros">
                            
                                <table width="100%">
                                    <tr>
                                        <td width="15%">
                                            Status:
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:DropDownList ID="ddlStatusDiverso" runat="server" CssClass="DropDownList" Width="95%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>                            
                        </td>
                    </tr>
                    <tr>
                        <td class="labelFiltros">
                           Descrição:
                        </td>
                        <td class="controlFiltros">
                            <asp:TextBox ID="tbxDescricao" CssClass="TextBox" Width="95%" runat="server"></asp:TextBox>
                        </td>
                        <td class="labelFiltros">                            
                                <table width="100%">
                                    <tr>
                                        <td width="15%">
                                            
                                        </td>
                                        <td align="right" width="40%">
                                             <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" onclick="btnPesquisar_Click"/>
                                        </td>
                                    </tr>
                                </table>                            
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
   </div>
    <div>
        <div class="contador_itens_grid">
        <table width="100%">
            <tr>
                <td width="90%" align="right">
                    Quantidade de itens por pagina:
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" CssClass="DropDownList"
                        Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGrid_SelectedIndexChanged"
                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de orgãos ambientais que serão exibidos em cada página do resultado da pesquisa')">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>1000</asp:ListItem>
                        <asp:ListItem Value="1">-- Todos --</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    </div>
     
    <div id="grid">
        <asp:UpdatePanel ID="UPPesquisa" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    Width="100%" OnPageIndexChanging="dgr_PageIndexChanging" OnRowDeleting="dgr_RowDeleting" OnRowEditing="dgr_RowEditing" OnInit="dgr_Init1">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Id" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="Grupo/Empresa">
                            <ItemTemplate>
                               <%# bindGrupoEmpresa(Container.DataItem) %>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="left" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                               <%# bindTipoDiverso(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descricao" HeaderText="Descricao"></asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                          <ItemTemplate>
                           <%# bindStatus(Container.DataItem) %>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Próximo Vencimento">
                          <ItemTemplate>
                            <%# bindProximoVencimento(Container.DataItem) %>
                          </ItemTemplate>
                          <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Próxima Notificação">
                          <ItemTemplate>
                            <%# bindProximaNotificacao(Container.DataItem) %>
                          </ItemTemplate>
                          <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renovar">
                          <ItemTemplate>
                          <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Edit" Visible="<%#BindingVisivel(Container.DataItem) %>" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" 
                          ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>" OnPreRender="ibtnRenovar_PreRender" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"   onmouseover="tooltip.show('Renova um Requerimento de pesquisa periódico')" />
                          </ItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Width="45px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit.">
                            <ItemTemplate>
                              <asp:ImageButton ID="ibtnEditar" runat="server" AlternateText="." Visible="<%#BindingVisivel(Container.DataItem) %>"
                                        ImageUrl="../imagens/icone_editar.png" PostBackUrl="<%# BindEditarDiverso(Container.DataItem) %>" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o vencimento para edição dos dados')"/>
                            </ItemTemplate>
                            <HeaderStyle Width="22px"/>
                            <ItemStyle HorizontalAlign="Right"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir">                        
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnExcluirCont" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif" Visible="<%#BindingVisivel(Container.DataItem) %>"
                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o vencimento diverso')" OnPreRender="ibtnExcluir_PreRender" />                               
                            </ItemTemplate>
                            <HeaderStyle Width="45px"/>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                        VerticalAlign="Top" CssClass="GridHeader" />
                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                <br />
                <asp:HiddenField ID="hfQuantidadeExibicao" runat="server" />
                <asp:Label ID="lblQuantidade" runat="server"></asp:Label>                                    
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" 
                    EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="dgr" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="dgr" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
<div id="popups" style="width:100%">
<div id="divPopUpRenovacao" style="width: 596px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div id="fecharRenovacao" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            RENOVAÇÃO</div>
        <asp:UpdatePanel ID="upRenovacao" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="right" style="width: 30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Próxima data de vencimento:
                        </td>
                        <td>                            
                            <asp:TextBox ID="tbxDataValidadeRenovacao" runat="server" CssClass="TextBox" 
                                Width="170px"></asp:TextBox>&nbsp;
                            <asp:CalendarExtender ID="tbxDataValidadeRenovacao_CalendarExtender" runat="server"
                                Format="dd/MM/yyyy" TargetControlID="tbxDataValidadeRenovacao">
                            </asp:CalendarExtender>
                            <asp:Button ID="btnSalvarRenovacao" runat="server" CssClass="Button" OnClick="btnSalvarRenovacao_Click"
                                Text="Renovar" Width="170px" OnInit="btnSalvarRenovacao_Init" onmouseout="tooltip.hide();"
                                onmouseover="tooltip.show('Renova o vencimento')" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Status:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatusRenovacao" runat="server" CssClass="DropDownList"
                                DataTextField="Nome" DataValueField="Id" Width="170px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="ckbUtilizarUltimasNotificacoes" runat="server" Checked="True" Text="Utilizar dias de notificações iguais ao anterior"
                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria automaticamente as mesmas notificações do vencimento anterior, levando em consideração a nova data de vencimento')" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            &nbsp;
                            <asp:HiddenField ID="hfIdDiverso" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divPopUpAlteracaoStatus" style="width: 805px; display: block; top: 0px;
        left: 0px;" class="pop_up_super_super_super">
        <div>
            <div id="fecharAlteracaoStatus" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Alteração de Status
            </div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopStatus" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td align="left">
                                    <strong>Você alterou os seguintes Status de Vencimento - Caso deseja enviar um email
                                        com esta alteração, preencha os campos abaixo e clique em Enviar/Salvar: </strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" class="TextBox">
                                    <asp:Label ID="lblAlteracao" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>Selecione os emails que receberão uma notificação com esta alteração:</strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" width="33%">
                                                <input id="Checkbox7" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                <strong>Grupo Econômico: </strong>
                                            </td>
                                            <td align="left" width="33%" valign="top">
                                                <input id="Checkbox10" type="checkbox" class="chkMarcarEmailsEmpresa" checked="checked" />&nbsp;
                                                <strong>Empresa:</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="ckbGrupos" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="ckbEmpresasAlteracao" runat="server" CssClass="chkEmailsEmpresasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <hr style="height: -12px" />
                                    <strong>Observação</strong> &nbsp;<asp:TextBox ID="tbxHistoricoAlteracao" runat="server"
                                        CssClass="TextBox" Height="56px" TextMode="MultiLine" Width="100%" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:CheckBox ID="chkEnviarEmail" runat="server" Checked="True" 
                                        Text="Enviar e-mail com alteração de status" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnAlterarStatus" runat="server" CssClass="Button" 
                                        OnClick="btnAlterarStatus_Click" oninit="btnAlterarStatus_Init" 
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')" 
                                        Text="Enviar/Salvar" Width="170px" />
                                    &nbsp;
                                    <asp:Button ID="btnFecharStatus" runat="server" CssClass="Button" 
                                        OnClick="btnFecharStatus_Click" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')" 
                                        Text="Fechar" Width="170px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
</asp:Content>

