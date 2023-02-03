<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="Permissoes.aspx.cs" Inherits="Permissoes_Permissoes" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function marcarCheckBoxGrid(check) {
            if ($(check).attr('checked') == "checked")
                $(check).parent().parent().parent().children("tr").find("input").attr("checked", "checked");
            else
                $(check).parent().parent().parent().children("tr").find("input").removeAttr("checked", "checked");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Permissões
    </p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_permissoes">
        <table style="width: 100%;">
            <tr>
                <td align="right" style="width: 25%">Sistema*:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlSistema" runat="server" CssClass="DropDownList" Width="250px"
                        ValidationGroup="rfv" OnSelectedIndexChanged="ddlSistema_SelectedIndexChanged" Enabled="false"
                        AutoPostBack="True" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Seleciona se serão exibidos  os grupos da base Sustentar ou da base Ambientalis')">
                        <asp:ListItem Value="0" Selected="True">Sustentar</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div>
            <asp:UpdatePanel ID="upSistemaPermissoes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="base_sustentar" runat="server" visible="true">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 25%">Grupo Econômico:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCliente" runat="server" Width="250px" CssClass="DropDownList" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Seleciona o grupo econômico, que serão controladas as permissões')">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 25%">Permissões por:</td>
                                <td align="left">                                    
                                    <asp:RadioButtonList ID="rblTipoPermissaoPor" runat="server" RepeatDirection="Horizontal" CellSpacing="2">
                                        <asp:ListItem Value="N">Gestão com edição coletiva</asp:ListItem>
                                        <asp:ListItem Value="C">Gestão com edição nomeada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSistema" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div>
            <asp:UpdatePanel ID="upSistemaModulosPermissoes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="grid_menus" style="margin-top: 10px;" runat="server" visible="true">
                        <asp:UpdatePanel ID="upMenus" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataGrid ID="dgrModulos" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnPageIndexChanged="dgrModulos_PageIndexChanged"
                                    Width="100%" AllowPaging="True">
                                    <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small"
                                        ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                        NextPageText="" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Nome" HeaderText="Módulo">
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" Width="20%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Descricao" HeaderText="Descrição">
                                            <HeaderStyle Width="35%" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Sel">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarModulos" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Permitido
                                            <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarCheckBoxGrid(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True"
                                        ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCliente" EventName="SelectedIndexChanged" />                                
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSistema" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="opcoes" style="text-align: right; margin-top: 5px;">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="Button" OnClick="btnSalvar_Click"
                Width="150px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as alterações nas permissões')" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
