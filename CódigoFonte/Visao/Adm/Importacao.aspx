<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="Importacao.aspx.cs" Inherits="Importacao_Importacao" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Importação de dados</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_importacao">
        <table style="width: 100%">
            <tr>
                <td style="width: 30%" align="right">
                    Grupo Econômico*:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="DropDownList" 
                        Width="50%" ValidationGroup="rfv" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Escolhe para qual grupo será importado os dados orgãos ambientais e os dados de tipo de licença')">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCliente"
                        ErrorMessage="Obrigatório!" InitialValue="0" ValidationGroup="rfv" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div style="text-align: center; background-color: #EEEEEE; max-height: 600px; overflow: auto;
            border-radius: 9px">
            <div style="width: 48%; float: left; margin: 1%; text-align: left">
                <span style="font-weight: bold">Selecione os Orgãos Ambientais:</span>
                <asp:DataGrid ID="dgrOrgaosAmbientais" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnPageIndexChanged="dgrOrgaosAmbientais_PageIndexChanged"
                    Width="100%" OnItemDataBound="dgr_ItemDataBound" PageSize="50">
                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" NextPageText="" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Left" />
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
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                Sel.
                                <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGrid(this,'chkSelecionarOrgaoAmbiental')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarOrgaoAmbiental" />
                            </ItemTemplate>
                            <HeaderStyle Width="45px" />
                        </asp:TemplateColumn>
                    </Columns>
                    <EditItemStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                        VerticalAlign="Top" />
                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
            </div>
            <div style="width: 48%; float: left; margin: 1%; text-align: left">
                <span style="font-weight: bold">Selecione os Tipos de Licença:</span>
                <asp:DataGrid ID="dgrTiposLicencas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnPageIndexChanged="dgrTiposLicencas_PageIndexChanged"
                    Width="100%" OnItemDataBound="dgr_ItemDataBound" PageSize="50">
                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" NextPageText="" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Sigla" HeaderText="Sigla"></asp:BoundColumn>
                        <asp:BoundColumn DataField="diasValidadePadrao" HeaderText="Dias de Validade Padrão">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="diasAvisoPadrao" HeaderText="Dias de Aviso Pardão"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                Sel.
                                <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGrid(this,'chkSelecionarTipoLicenca')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarTipoLicenca" />
                            </ItemTemplate>
                            <HeaderStyle Width="45px" />
                        </asp:TemplateColumn>
                    </Columns>
                    <EditItemStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                        VerticalAlign="Top" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
            </div>
        </div>
        <div id="opcoes" style="text-align: right; margin: 10px; clear: both">
            <asp:Button ID="btnImportar" runat="server" Text="Importar" CssClass="Button" Width="150px"
                OnClick="btnImportar_Click" ValidationGroup="rfv" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Importa os dados de orgão ambiental e de tipo de licença selecionados para o grupo escolhido')" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
