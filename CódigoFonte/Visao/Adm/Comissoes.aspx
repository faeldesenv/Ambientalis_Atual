<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="Comissoes.aspx.cs" Inherits="Vendas_Comissoes" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    controle de
    Comissões
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_sistema" style="min-height: 300px;">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td align="right">
                        Comissões da:</td>
                    <td width="210" align="left">
                        <asp:DropDownList ID="ddlComissao" runat="server" CssClass="DropDownList" 
                            Width="200px">
                            <asp:ListItem>Revenda</asp:ListItem>
                            <asp:ListItem>Supervisor</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                        Revenda:
                    </td>
                    <td width="260" class="controlFiltros">
                        <asp:DropDownList ID="ddlRevenda" runat="server" CssClass="DropDownList" 
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Ano:
                    </td>
                    <td width="260" class="controlFiltros">
                        <asp:DropDownList ID="ddlAno" runat="server" CssClass="DropDownList" 
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td rowspan="3" valign="bottom">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" 
                                    OnClick="btnPesquisar_Click" Text="Pesquisar" />
                                &nbsp;<asp:Button ID="btnSalvar" runat="server" CssClass="Button" 
                                    OnClick="btnSalvar_Click" Text="Salvar alterações" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="grid">
            <asp:UpdatePanel ID="upPesquisa" runat="server" UpdateMode="Conditional">
                <contenttemplate>
                    <asp:DataGrid ID="dgr" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        Height="45px" Width="100%" ForeColor="#333333" ShowFooter="True" 
                        onpageindexchanged="dgr_PageIndexChanged" PageSize="20" CellSpacing="1">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager"/>
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Cliente">
                                <ItemTemplate>
                                   <%# BindCliente(Container.DataItem) %><br/>(<%# BindCPF(Container.DataItem) %>)
                                </ItemTemplate>
                            </asp:TemplateColumn>  
                             <asp:TemplateColumn HeaderText="Revenda">
                                <ItemTemplate>
                                   <%# BindRevenda(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>                              
                            <asp:TemplateColumn HeaderText="JAN">
                            <ItemTemplate>
                                   <b><%# BindMeses(Container.DataItem, 1) %></b><br/>
                                  <asp:CheckBox ID="ckbPGRevenda1" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 1) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 1) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral1" runat="server" Text='<%# BindTotalGeral(1) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="FEV">
                            <ItemTemplate>
                                    <b><%# BindMeses(Container.DataItem, 2) %></b><br/>
                                <asp:CheckBox ID="ckbPGRevenda2" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 2) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 2) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral2" runat="server" Text='<%# BindTotalGeral(2) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MAR">
                            <ItemTemplate>
                                    <b><%# BindMeses(Container.DataItem, 3) %></b><br/>
                                 <asp:CheckBox ID="ckbPGRevenda3" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 3) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 3) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral3" runat="server" Text='<%# BindTotalGeral(3) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ABR">
                            <ItemTemplate>
                                   <b><%# BindMeses(Container.DataItem, 4) %></b><br/>
                                 <asp:CheckBox ID="ckbPGRevenda4" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 4) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 4) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral4" runat="server" Text='<%# BindTotalGeral(4) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MAI">
                            <ItemTemplate>
                                     <b><%# BindMeses(Container.DataItem, 5) %></b><br/>
                                  <asp:CheckBox ID="ckbPGRevenda5" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 5) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 5) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral5" runat="server" Text='<%# BindTotalGeral(5) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="JUN">
                            <ItemTemplate>
                                    <b><%# BindMeses(Container.DataItem, 6) %></b><br/>
                                  <asp:CheckBox ID="ckbPGRevenda6" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 6) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 6) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral6" runat="server" Text='<%# BindTotalGeral(6) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="JUL">
                            <ItemTemplate>
                                     <b><%# BindMeses(Container.DataItem, 7) %></b><br/>
                                   <asp:CheckBox ID="ckbPGRevenda7" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 7) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 7) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral7" runat="server" Text='<%# BindTotalGeral(7) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="AGO">
                            <ItemTemplate>
                                     <b><%# BindMeses(Container.DataItem, 8) %></b><br/>
                                  <asp:CheckBox ID="ckbPGRevenda8" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 8) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 8) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral8" runat="server" Text='<%# BindTotalGeral(8) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SET">
                            <ItemTemplate>
                                    <b><%# BindMeses(Container.DataItem, 9) %></b><br/>
                                  <asp:CheckBox ID="ckbPGRevenda9" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 9) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 9) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral9" runat="server" Text='<%# BindTotalGeral(9) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="OUT">
                            <ItemTemplate>
                                    <b><%# BindMeses(Container.DataItem, 10) %></b><br/>
                                  <asp:CheckBox ID="ckbPGRevenda10" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 10) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 10) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral10" runat="server" Text='<%# BindTotalGeral(10) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOV">
                            <ItemTemplate>
                                     <b><%# BindMeses(Container.DataItem, 11) %></b><br/>
                                   <asp:CheckBox ID="ckbPGRevenda11" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 11) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 11) %>" />
                              
                                </ItemTemplate>
                                <FooterTemplate>                                           
                                            <asp:Label ID="lblTotalGeral11" runat="server" Text='<%# BindTotalGeral(11) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DEZ">
                            <ItemTemplate>
                                     <b><%# BindMeses(Container.DataItem, 12) %></b><br/>
                                   <asp:CheckBox ID="ckbPGRevenda12" runat="server" Text="PAGO" Checked="<%# BindPago(Container.DataItem, 12) %>" Enabled="<%# BindPagoVisivel(Container.DataItem, 12) %>" />
                                </ItemTemplate>
                                <FooterTemplate>
                                            <asp:Label ID="lblTotalGeral12" runat="server" Text='<%# BindTotalGeral(12) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                    <br />
                    * Mês cujo valor da mensalidade será reajustado.
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                </triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
