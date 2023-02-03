<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true" CodeFile="Interacoes.aspx.cs" Inherits="Prospecto_Interacoes" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    <p>
        Interações</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="15%">
                        Revenda:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:DropDownList ID="ddlRevenda" runat="server" CssClass="DropDownList" 
                            Width="95%" AutoPostBack="True" 
                            onselectedindexchanged="ddlRevenda_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros" width="13%">
                        Cliente:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:UpdatePanel ID="UPprospectos" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlProspecto" runat="server" 
                                    CssClass="DropDownList" 
                                    Width="95%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlRevenda" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td rowspan="3" width="13%" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click"
                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa as interações cadastradas, de acordo com o(s) filtro(s) escolhido(s)')" />
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                       Tipo:
                    </td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="DropDownList" Width="95%">
                          <asp:ListItem Selected="True">-- Todos --</asp:ListItem>
                            <asp:ListItem>Pessoal</asp:ListItem>
                            <asp:ListItem>Telefone</asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                            <asp:ListItem>Chat</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                        Data de:</td>
                    <td class="controlFiltros" valign="middle">
                       <asp:TextBox ID="tbxDataDe" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="tbxDataDe">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp;Até:
                        <asp:TextBox ID="tbxDataAte" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="tbxDataAte">
                        </asp:CalendarExtender>
                     </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        &nbsp;&nbsp;Status:
                    </td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" 
                            Width="95%">
                            <asp:ListItem Selected="True">-- Todos --</asp:ListItem>
                            <asp:ListItem>Realizada</asp:ListItem>
                            <asp:ListItem>Agendada</asp:ListItem>
                            <asp:ListItem>Adiada</asp:ListItem>
                            <asp:ListItem>Cancelada</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                        &nbsp;</td>
                    <td class="controlFiltros">
                        
                        &nbsp;</td>
                </tr>
                </table>
        </div>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">
                        Quantidade de itens por pagina:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" CssClass="DropDownList"
                            Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGrid_SelectedIndexChanged"
                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de grupos que serão exibidos em cada página do resultado da pesquisa')">
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
        <div>
              <strong>
                <asp:ImageButton ID="ibtnAddInteracao" runat="server" ImageUrl="~/imagens/icone_adicionar.png"  OnClick="ibtnAddInteracao_Click" OnInit="ibtnAddInteracao_Init" style="width: 20px" />&nbsp;INTERAÇÃO:</strong>
              <asp:Label ID="lblPopUpInteracao" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="lblPopUpInteracao_popupextender" runat="server" 
                  BackgroundCssClass="simplemodal" CancelControlID="fecharInteracao" 
                  PopupControlID="divPopUpInteracao" TargetControlID="lblPopUpInteracao">
            </asp:ModalPopupExtender>
        </div>
        <div id="grid">
            <asp:UpdatePanel ID="upPesquisa" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgrinteracoes" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" GridLines="None"
                        OnEditCommand="dgrinteracoes_EditCommand" Width="100%" 
                        ForeColor="#333333" ondeletecommand="dgrinteracoes_DeleteCommand" 
                        OnInit="dgrinteracoes_Init" AllowPaging="True" 
                        onitemdatabound="dgrinteracoes_ItemDataBound" 
                        onpageindexchanged="dgrinteracoes_PageIndexChanged">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Revenda">
                              <ItemTemplate>
                                    <%# bindingRevenda(Container.DataItem)%>
                               </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cliente">
                               <ItemTemplate>
                                    <%# bindingProspecto(Container.DataItem)%>
                               </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Data" DataFormatString="{0:d}" HeaderText="Data">
                                <HeaderStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Tipo" HeaderText="Tipo">
                                <HeaderStyle Width="115px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Pessoa (Cargo)">
                                <ItemTemplate>
                                    <%# bindingPessoaCargo(Container.DataItem)%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Status" HeaderText="Status">
                                <HeaderStyle Width="115px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                              <ItemTemplate>
                                   <asp:ImageButton ID="imgAbrir1" runat="server" AlternateText="." 
                                       CommandName="Edit" ImageUrl="../imagens/icone_editar.png" 
                                       onmouseout="tooltip.hide();" 
                                       onmouseover="tooltip.show('Abre a empresa para edição dos dados')" 
                                       ToolTip="Abrir" />
                              </ItemTemplate>
                                <HeaderStyle Width="22px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir1" runat="server" CommandName="Delete" 
                                        ImageUrl="~/imagens/excluir.gif" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Exclui a(s) empresa(s) selecionada(s)')" 
                                        OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                    <input id="ckbSelecionar0" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                </ItemTemplate>
                                <HeaderStyle 
                                    Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
  <div id="divPopUpInteracao" style="width: 697px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharInteracao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Interação</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upInteracoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" style="width: 30%">
                                    Cliente:&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProspectoInteracao" runat="server" CssClass="DropDownList" Width="305px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxData" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxData_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxData">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Tipo:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoInteracao" runat="server" CssClass="DropDownList" Width="170px">
                                        <asp:ListItem>Pessoal</asp:ListItem>
                                        <asp:ListItem>Telefone</asp:ListItem>
                                        <asp:ListItem>Email</asp:ListItem>
                                        <asp:ListItem>Chat</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusInteracao" runat="server" CssClass="DropDownList" Width="170px">
                                        <asp:ListItem>Realizada</asp:ListItem>
                                        <asp:ListItem>Agendada</asp:ListItem>
                                        <asp:ListItem>Adiada</asp:ListItem>
                                        <asp:ListItem>Cancelada</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Nome da Pessoa:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNomePessoa" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Cargo da Pessoa:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxCargoPessoa" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Descrição:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDescricao" runat="server" CssClass="TextBox" Height="77px" TextMode="MultiLine"
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfIdInteracao" runat="server" />
                                    <asp:Button ID="btnNovo" runat="server" CssClass="Button" 
                                        OnClick="btnNovo_Click" Text="Novo" />&nbsp;
                                    <asp:Button ID="btnSalvarInteracao" runat="server" CssClass="Button" Text="Salvar"
                                        OnClick="btnSalvarInteracao_Click" OnInit="btnSalvarInteracao_Init" />&nbsp;
                                    <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" 
                                        OnClick="btnExcluir_Click" Text="Excluir" oninit="btnExcluir_Init" 
                                        onprerender="btnExcluir_PreRender" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarInteracao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

