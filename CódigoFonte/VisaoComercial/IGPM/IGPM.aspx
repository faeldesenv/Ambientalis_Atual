<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="IGPM.aspx.cs" Inherits="IGMP_IGPM" %>
<%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {         
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    IGPM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="5">
            <tr>
                <td align="right" width="40%" style="vertical-align:middle;">
                        Ano &nbsp;
                    <asp:DropDownList CssClass="DropDownList" ID="ddlAno" runat="server" 
                        AutoPostBack="True" onselectedindexchanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </td>
                <td align="left" rowspan="4" valign="top" style="padding-left: 10px;">
                    <div id="grid" style="width: 300px; max-height: 300px; margin: 0px;">
                        <asp:UpdatePanel ID="udpValores" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataGrid ID="dgrValoresAcumuladosMes" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" GridLines="None" Height="45px"
                                    PageSize="12" Width="100%" ForeColor="#333333">
                                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                                        NextPageText="" Mode="NumericPages" CssClass="GridPager" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Mês">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# BindMes(Container.DataItem) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Acumulado 12 meses (%)">
                                            <ItemTemplate>
                                                <asp:Label runat="server" 
                                                    Text='<%# BindValorAcumulado(Container.DataItem) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                        VerticalAlign="Top" CssClass="GridHeader" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlAno" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                    <div style="margin-top: 10px;">
                        * Serão reajustadas as comissões referentes a contratos que têm renovação no próximo
                        mês.
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
