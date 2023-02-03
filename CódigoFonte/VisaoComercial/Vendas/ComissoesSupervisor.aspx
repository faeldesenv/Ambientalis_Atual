<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="ComissoesSupervisor.aspx.cs" Inherits="Vendas_ComissoesSupervisor" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Controle do Comissões do Supervisor
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_sistema" style="min-height: 300px;">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" style="width: 10px;">
                        Ano:
                    </td>
                    <td style="width: 115px;" class="controlFiltros">
                        <asp:DropDownList ID="ddlAno" runat="server" CssClass="DropDownList" Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td rowspan="3" valign="bottom">
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnPesquisar" runat="server" CssClass="Button"
                            Text="Pesquisar" OnClick="btnPesquisar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="grid">
            <asp:UpdatePanel ID="upPesquisa" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgr" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="None"
                        Height="45px" OnPageIndexChanged="dgr_PageIndexChanged" PageSize="8" ShowFooter="True"
                        Width="100%" CellSpacing="1">
                        <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small" ForeColor="White"
                            HorizontalAlign="Center" Mode="NumericPages" NextPageText="" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Revenda">
                                <ItemTemplate>
                                    <%# BindRevenda(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cliente">
                                <ItemTemplate>
                                    <%# BindCliente(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CPF / CNPJ">
                                <ItemTemplate>
                                    <%# BindCPF(Container.DataItem) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Total:&nbsp;&nbsp;
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="JAN">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 1) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral1" runat="server" Text="<%# BindTotalGeral(1) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="FEV">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 2) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral2" runat="server" Text="<%# BindTotalGeral(2) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MAR">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 3) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral3" runat="server" Text="<%# BindTotalGeral(3) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ABR">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 4) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral4" runat="server" Text="<%# BindTotalGeral(4) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MAI">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 5) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral5" runat="server" Text="<%# BindTotalGeral(5) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="JUN">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 6) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral6" runat="server" Text="<%# BindTotalGeral(6) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="JUL">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 7) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral7" runat="server" Text="<%# BindTotalGeral(7) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="AGO">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 8) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral8" runat="server" Text="<%# BindTotalGeral(8) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SET">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 9) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral9" runat="server" Text="<%# BindTotalGeral(9) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="OUT">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 10) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral10" runat="server" Text="<%# BindTotalGeral(10) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOV">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 11) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral11" runat="server" Text="<%# BindTotalGeral(11) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DEZ">
                                <ItemTemplate>
                                    <%# BindMeses(Container.DataItem, 12) %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalGeral12" runat="server" Text="<%# BindTotalGeral(12) %>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" ForeColor="White"
                            HorizontalAlign="Center" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                    <br />
                    <div style="margin-top: 4px;">
                        <div style="background-color: Green; margin: 2px; width: 10px; height: 10px; float: left">
                        </div>
                        <div style="margin: 2px; width: auto; height: 10px; font-size: x-small; float: left">
                            Mensalidade PAGA
                        </div>
                        <div style="height: 10px; clear: both">
                        </div>
                    </div>
                    * Mês cujo valor da mensalidade será reajustado.
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
