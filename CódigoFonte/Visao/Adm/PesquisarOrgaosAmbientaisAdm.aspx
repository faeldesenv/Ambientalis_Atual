<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true" CodeFile="PesquisarOrgaosAmbientaisAdm.aspx.cs" Inherits="OrgaoAmbiental_PesquisarOrgaosAmbientaisAdm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
    <script src="../Scripts/Funcoes.js" type="text/javascript"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    <p>
        pesquisa de orgãos ambientais</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="filtros">
 
     <asp:UpdatePanel ID="UPOrgaos" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
             <table width="100%">
                 <tr>
                     <td class="labelFiltros" width="10%">Tipo: </td>
                     <td class="controlFiltros" width="16%">
                         <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" 
                             CssClass="DropDownList" onselectedindexchanged="ddlTipo_SelectedIndexChanged" 
                             Width="95%">
                             <asp:ListItem Value="0">-- Selecione --</asp:ListItem>
                             <asp:ListItem Value="1">Federal</asp:ListItem>
                             <asp:ListItem Value="2">Estadual</asp:ListItem>
                             <asp:ListItem Value="3">Municipal</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td class="labelFiltros" width="20%">
                     <div ID="estado_orgao" runat="server" visible="false">                
                         <table width="100%">
                             <tr>
                                 <td width="27%" class="labelFiltros">
                                     Estado:</td>
                                 <td align="left">
                                     <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" 
                                         CssClass="DropDownList" onselectedindexchanged="ddlEstado_SelectedIndexChanged" 
                                         Width="95%">
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                         </table>
                          </div>
                     </td>
                     <td rowspan="2" valign="bottom" width="10%">
                         <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" 
                             OnClick="btnPesquisar_Click" Text="Pesquisar" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os orgãos ambientais cadastrados, de acordo com o(s) filtro(s) escolhido(s)')"  />
                     </td>
                 </tr>
                 <tr>                
                        
                     <td class="labelFiltros" width="10%">
                         &nbsp;Nome:
                     </td>
                     <td class="controlFiltros" width="16%">
                         <asp:TextBox ID="tbxNomeOrgao" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                     </td>
                     <td class="labelFiltros" width="20%">
                     <div id="cidades_orgao" runat="server" visible="false">
                         <table width="100%">
                              <tr>
                                  <td width="15%">
                                      Cidade:</td>
                                  <td align="left" width="40%">
                                      <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" 
                                          Width="95%">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                          </table>
                           </div>
                     </td>
                      
                 </tr>
             </table>
         </ContentTemplate>
         <Triggers>
             <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="SelectedIndexChanged" />
             <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
         </Triggers>
     </asp:UpdatePanel>    
    </div>
    <div id="grid">
    
        <asp:UpdatePanel ID="UPPesquisa" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:DataGrid ID="dgr" runat="server" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyField="Id" 
                    ForeColor="#333333" GridLines="None" OnDeleteCommand="dgr_DeleteCommand" 
                    OnItemDataBound="dgr_ItemDataBound"
                    Width="100%">
                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" 
                        HorizontalAlign="Center" Mode="NumericPages" NextPageText="" CssClass="GridPager"/>
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Tipo">
                            <ItemTemplate>
                              <%# bindTipo(Container.DataItem) %>
                             </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Cidade/Estado">
                            <ItemTemplate>
                              <%# bindCidadeEstado(Container.DataItem) %>
                             </ItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Edit.">
                            <ItemTemplate>
                                <asp:HyperLink ID="hplEditar" runat="server" 
                                    ImageUrl="~/imagens/icone_editar.png" 
                                    NavigateUrl="<%# bindEditar(Container.DataItem) %>" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o orgão ambiental para edição dos dados')" ></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Width="22px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:ImageButton ID="ibtnExcluir0" runat="server" CommandName="Delete" 
                                    ImageUrl="~/imagens/excluir.gif"
                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui os orgãos ambientais selecionados')"  />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                </HeaderTemplate>
                            <HeaderStyle Width="45px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                    </Columns>
                    <EditItemStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" VerticalAlign="Top" CssClass="GridHeader" />
                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
                <br/>                
                <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                <br/>
                <br></br>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

