<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="IGPM.aspx.cs" Inherits="IGMP_IGPM" %>
<%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ValidaNumeros();
            $('#<%= btnAdicionarComissao.ClientID %>').mouseover(function () {
                ConfirmarAdicao();
            });
            
        });
        function ValidaNumeros() {
            $('#<%= tbxValorAcumuladoMes.ClientID %>').keyup(function () {
                var valorCampo = $(this).val();
                var novoValor = "";
                for (var i = 0; i < valorCampo.length; i++) {
                    novoValor += valorCampo[i].replace(/[^\.^\-^\,0-9]/, "");
                }
                $(this).val(novoValor);
            });
            ConfirmarAdicao();
        }

        function ConfirmarAdicao() {
            var comando = "javascript: return confirm('{0}')";
            var mensagem = "Confima a adição de um novo IGPM com o valor de {0}% em {1}/{2}?";
            mensagem = mensagem.replace('{0}', $('#<%= tbxValorAcumuladoMes.ClientID %>').val());
            mensagem = mensagem.replace('{1}', $('#<%= ddlMes.ClientID %> option:selected').text());
            mensagem = mensagem.replace('{2}', $('#<%= ddlAno.ClientID %> option:selected').text());
            comando = comando.replace('{0}', mensagem);
            $('#<%= btnAdicionarComissao.ClientID %>').attr("onclick", comando);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    IGPM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="5">
            <tr>
                <td align="right" width="40%">
                    <asp:DropDownList CssClass="DropDownList" ID="ddlAno" runat="server" 
                        AutoPostBack="True" onselectedindexchanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:DropDownList CssClass="DropDownList" ID="ddlMes" runat="server">
                    </asp:DropDownList>
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
                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarComissao" EventName="Click" />
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
                    Acumulado no Mês (%):
                    <asp:TextBox ID="tbxValorAcumuladoMes" runat="server" Width="80px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                        <asp:Button ID="btnAdicionarComissao" runat="server" CssClass="Button" 
                            Text="Adicionar e Reajustar Comissões" 
                            onclick="btnAdicionarComissao_Click" 
                            onprerender="btnAdicionarComissao_PreRender" />
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
