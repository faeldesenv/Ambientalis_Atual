<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="ADMFiltrosRelatorios.aspx.cs" Inherits="ADMRelatorios_FiltrosRelatorios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:content id="Content1" contentplaceholderid="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1
        {
            height: 18px;
        }
    </style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="Barra" runat="Server">
    <p>
        Escolha de Relatório</p>
</asp:content>
<asp:content id="Content5" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id="campos_escolha_relatorio" class="container" style="height:330px">
        <div class="div_container" style="width: 30%">
            <b>Escolha o Relatório:</b>
            <asp:treeview id="trvRelatorios" runat="server" width="100%" onselectednodechanged="trvRelatorios_SelectedNodeChanged" ForeColor="#000099" >
                <selectednodestyle cssclass="NoSelecionado" />
            </asp:treeview>
        </div>
        <div class="div_container" style="width: 68%; margin-left: 1%">
            &nbsp;<asp:updatepanel id="upFiltros" runat="server" updatemode="Conditional">
                <contenttemplate>
                    <asp:MultiView ID="mtvFiltros" runat="server" ActiveViewIndex="0">
                        <asp:View ID="view_inicial" runat="server">
                            <div style="text-align: center">
                                Escolha algum Relatório ao lado!</div>
                        </asp:View>
                        <asp:View ID="viewGruposEconomicos" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="right" width="30%">
                                                Sistema:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSistema" runat="server" AutoPostBack="True" 
                                                    CssClass="DropDownList" 
                                                    onselectedindexchanged="ddlSistema_SelectedIndexChanged" ValidationGroup="rfv" 
                                                    Width="50%">
                                                    <asp:ListItem Selected="True" Value="0">Sustentar</asp:ListItem>
                                                    <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="30%">
                                                Administrador:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAdministradorGruposEconomicos" runat="server" 
                                                    CssClass="DropDownList" Width="50%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Data de Cadastro:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbxDataCadastroRelatorioGruposEconomicos" runat="server" 
                                                    CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="tbxDataCadastroRelatorioGruposEconomicos_CalendarExtender" 
                                                    runat="server" Enabled="True" Format="dd/MM/yyyy" 
                                                    TargetControlID="tbxDataCadastroRelatorioGruposEconomicos">
                                                </asp:CalendarExtender>
                                                &nbsp;até
                                                <asp:TextBox ID="tbxDataCadastroAtehRelatorioGruposEconomicos" runat="server" 
                                                    CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="tbxDataCadastroAtehRelatorioGruposEconomicos_CalendarExtender" 
                                                    runat="server" Enabled="True" Format="dd/MM/yyyy" 
                                                    TargetControlID="tbxDataCadastroAtehRelatorioGruposEconomicos">
                                                </asp:CalendarExtender>
                                            </td>
                                             <tr>
                                                 <td align="right">
                                                     Data de Cancelamento de:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="tbxDataCancelamentoDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                     <asp:CalendarExtender ID="tbxDataCancelamentoDe_Extender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataCancelamentoDe">
                                                     </asp:CalendarExtender>
                                                     &nbsp;até&nbsp;
                                                     <asp:TextBox ID="tbxDataCancelamentoAte" runat="server" CssClass="TextBox" Width="22%" TargetControlID="tbxDataCancelamentoAte"></asp:TextBox>
                                                     <asp:CalendarExtender ID="tbxDataCancelamentoAte_Extender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataCancelamentoAte">
                                                     </asp:CalendarExtender>
                                                 </td>
                                             </tr>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Possui Usuários Cadastrados:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlPossuiUsuarios" runat="server" CssClass="DropDownList" 
                                                    Width="50%">
                                                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                                    <asp:ListItem Value="1">Possui Usuários Cadastrados</asp:ListItem>
                                                    <asp:ListItem Value="2">Não Possui Usuários Cadastrados</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Ativo:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAtivo" runat="server" CssClass="DropDownList" 
                                                    Width="50%">
                                                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                    <asp:ListItem Value="2">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Cancelado:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlCancelado" runat="server" CssClass="DropDownList" 
                                                    Width="50%">
                                                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                    <asp:ListItem Value="2">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:View>
                        <asp:View ID="view_relatorio_utilizacao" runat="server">
                            <asp:UpdatePanel ID="upUtilizacao" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                               <table width="100%">
                                <tr>
                                    <td align="right" width="30%">
                                      Sistema:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSistemaRelUtilizacao" runat="server" AutoPostBack="True" 
                                          CssClass="DropDownList" ValidationGroup="rfv" Width="50%" 
                                          onselectedindexchanged="ddlSistemaRelUtilizacao_SelectedIndexChanged">
                                          <asp:ListItem Selected="True" Value="0">Sustentar</asp:ListItem>
                                          <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                                        </asp:DropDownList>
                                     </td>
                                </tr>
                                <tr>
                                    <td align="right" width="30%">
                                     Grupo Econômico:
                                    </td>
                                    <td align="left">
                                      <asp:DropDownList ID="ddlGrupoEconomicoRelUtilizacao" runat="server" 
                                       CssClass="DropDownList" Width="50%">
                                      </asp:DropDownList>
                                    </td>
                                      <asp:Label ID="lblSistema" runat="server" Visible="false"></asp:Label>
                                </tr>
                               </table>
                             </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSistemaRelUtilizacao" EventName="SelectedIndexChanged" />                                    
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:View>
                        <asp:View ID="view_acessos" runat="server">
                            <asp:UpdatePanel ID="upAcessos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%">
                                <tr>
                                    <td align="right" width="30%">
                                      Sistema:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSistemaAcessos" runat="server" AutoPostBack="True" 
                                          CssClass="DropDownList" ValidationGroup="rfv" Width="50%" OnSelectedIndexChanged="ddlSistemaAcessos_SelectedIndexChanged" >
                                          <asp:ListItem Selected="True" Value="0">Sustentar</asp:ListItem>
                                          <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                                        </asp:DropDownList>
                                     </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="administrador_acessos" runat="server" visible="false">
                                          <table width="100%">
                                              <tr>
                                                  <td align="right" width="30%">
                                                      Administrador:</td>
                                                  <td align="left">
                                                      <asp:DropDownList ID="ddlAdministradorAcessos" runat="server" AutoPostBack="True" CssClass="DropDownList" 
                                                          Width="50%" OnSelectedIndexChanged="ddlAdministradorAcessos_SelectedIndexChanged" >
                                                      </asp:DropDownList>
                                                  </td>
                                              </tr>
                                          </table>
                                        </div>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td align="right" width="30%">
                                     Grupo Econômico:
                                    </td>
                                    <td align="left">
                                      <asp:DropDownList ID="ddlGrupoAdministradorAcessos" runat="server" CssClass="DropDownList" Width="50%" 
                                          AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoAdministradorAcessos_SelectedIndexChanged1">
                                      </asp:DropDownList>
                                    </td>                                      
                                </tr>
                                <tr>
                                    <td align="right" width="30%">
                                     Usuário:
                                    </td>
                                    <td align="left">
                                      <asp:DropDownList ID="ddlUsuarioAcessos" runat="server" 
                                       CssClass="DropDownList" Width="50%">
                                      </asp:DropDownList>
                                    </td>                                      
                                </tr>
                                <tr>
                                    <td align="right">
                                     Período de:</td>
                                    <td align="left" valign="bottom">
                                        <asp:TextBox ID="tbxDataAcessoDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataAcessoDe">
                                        </asp:CalendarExtender>
                                        &nbsp;até&nbsp;
                                        <asp:TextBox ID="tbxDataAcessoAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataAcessoAte">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                               </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSistemaAcessos" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoAdministradorAcessos" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlAdministradorAcessos" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:View>
                    </asp:MultiView>
                    <div ID="opcoes_pesquisar" align="center" 
                        style="text-align: right; margin-top: 10px; margin-right: 10px">
                        <asp:Button ID="btnExibirRelatorio" runat="server" cssclass="Button" 
                            onclick="btnExibirRelatório_Click" onclientclick="aspnetForm.target =’_blank’;" 
                            onmouseout="tooltip.hide();" 
                            onmouseover="tooltip.show('Monta o relatório de acordo com os filtros escolhidos')" 
                            text="Exibir Relatório" Visible="False" width="150px" />
                    </div>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="trvRelatorios" EventName="SelectedNodeChanged">
                    </asp:AsyncPostBackTrigger>
                </triggers>
            </asp:updatepanel>
        </div>
    </div>
</asp:content>
<asp:content id="Content4" contentplaceholderid="popups" runat="Server">
</asp:content>
