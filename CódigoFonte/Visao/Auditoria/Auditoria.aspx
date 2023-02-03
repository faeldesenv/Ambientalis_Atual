<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="Auditoria.aspx.cs" Inherits="Relatorio_auditoria" %>
<%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        auditoria</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="20%">
                        Usuário:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                    <td width="25%" rowspan="6" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os tipos de licença cadastrados, de acordo com o(s) filtro(s) escolhido(s)')" />
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        De:</td>
                    <td width="25%" class="controlFiltros">
                                        <asp:TextBox ID="tbxDatade" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDatade_CalendarExtender"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" 
                                            TargetControlID="tbxDatade"></asp:CalendarExtender>
                    &nbsp;Até:<asp:TextBox ID="tbxDataAte" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataAte_CalendarExtender"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" 
                                            TargetControlID="tbxDataAte"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        Operação</td>
                    <td width="25%" class="controlFiltros">
                                        <asp:DropDownList ID="ddlOperacao" runat="server" CssClass="DropDownList" 
                                            Width="200px">
                                            <asp:ListItem>Todos</asp:ListItem>
                                            <asp:ListItem>EXCLUSÃO</asp:ListItem>
                                            <asp:ListItem>ALTERAÇÃO</asp:ListItem>
                                            <asp:ListItem>INSERÇÃO</asp:ListItem>
                                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        Registro:</td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxRegistro" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        Dado Antigo:</td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxValorAntigo" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        &nbsp;Novo Dado:</td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxValorNovo" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                </table>
        </div>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">
                        Quantidade de itens por pagina:</td>
                    <td>
            <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" 
                CssClass="DropDownList" Width="100%" AutoPostBack="True" 
                            onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de tipos de licença que serão exibidos em cada página do resultado da pesquisa')" >
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
        <div id="grid">
            <asp:UpdatePanel ID="UpdateGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgrAuditoria" runat="server" AllowPaging="True"
                        CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" 
                        OnPageIndexChanged="dgr_PageIndexChanged" Width="100%" 
                        AutoGenerateColumns="False">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" 
                        HorizontalAlign="Center" Mode="NumericPages" NextPageText="" CssClass="GridPager"/>
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="GetData" HeaderText="Data/Hora"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GetUsuario" HeaderText="Usuário"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Operacao" HeaderText="Operação"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Tabela" HeaderText="Registro"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Coluna" HeaderText="Campo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="GetValorOLD" HeaderText="Dado antigo">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="GetValorNEW" HeaderText="Novo Dado">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Registro" HeaderText="Referência"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
