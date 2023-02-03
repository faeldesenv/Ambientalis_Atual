<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioContratosDiversos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioContratosDiversos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataReajusteDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataReajusteAte.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataVencimentoContratoDiversoDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataVencimentoContratoDiversoAte.ClientID %>"));    
            
            exibirEsconderEmpresaComo();
            $('#<%=ddlEmpresaContratosDiversos.ClientID %>').change(function () { exibirEsconderEmpresaComo(); });

            exibirEsconderClienteOuFornecor();
            $('#<%=ddlComoContratosDiversos.ClientID %>').change(function () { exibirEsconderClienteOuFornecor(); });
        });

        function exibirEsconderEmpresaComo() {            
            if (eval($('#<%=ddlEmpresaContratosDiversos.ClientID %>').val()) > 0) {
                $('#empresa_como').css('display', 'block');
                
            } else {
                $('#empresa_como').css('display', 'none');
                $('#cliente_contrato_diverso').css('display', 'none');
                $('#fornecedor_contrato').css('display', 'none');
                $('#<%=ddlComoContratosDiversos.ClientID %>').val('0');
                $('#<%=ddlFornecedorContratosDiversos.ClientID %>').val('0');
                $('#<%=ddlClienteContratosDiversos.ClientID %>').val('0');
            }
        }

        function exibirEsconderClienteOuFornecor() {
            if (eval($('#<%=ddlComoContratosDiversos.ClientID %>').val()) > 0) {

                if (eval($('#<%=ddlComoContratosDiversos.ClientID %>').val()) == 1) {
                    $('#cliente_contrato_diverso').css('display', 'none');
                    $('#fornecedor_contrato').css('display', 'block');
                } else {
                    $('#cliente_contrato_diverso').css('display', 'block');
                    $('#fornecedor_contrato').css('display', 'none');
                }                

            } else {
                $('#cliente_contrato_diverso').css('display', 'none');
                $('#fornecedor_contrato').css('display', 'none');
            }
        }
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Contratos Diversos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" Runat="Server">
    <div style="padding-top: 10px; padding-bottom: 15px;">
        <div style="width: 150px; text-align: left; font-family: 'Open Sans'; color: black; font-size: 13px;">
            Exibir as Colunas:
        </div>
        <div style="border: 1px solid white; padding: 10px;">
            <div>
                <asp:CheckBoxList ID="ckbColunas" runat="server" CssClass="chekListRelatorios" RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" CellSpacing="0" Font-Names="Open Sans" Font-Size="13px" ForeColor="Black">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <table style="width: 100%; margin-right: 0px;">
        <tr>
            <td width="30%" align="right">
                Grupo Econômico:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGrupoContratosDiversos" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlGrupoContratosDiversos_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Empresa:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <script type="text/javascript">
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { exibirEsconderEmpresaComo(); $('#<%=ddlEmpresaContratosDiversos.ClientID %>').change(function () { exibirEsconderEmpresaComo(); }); });
                        </script>
                        <asp:DropDownList ID="ddlEmpresaContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoContratosDiversos" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="empresa_como" style="display:none;">
                    <table width="100%">
                        <tr>
                            <td width="30%" align="right">
                                Como:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlComoContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                                    <asp:ListItem Selected="True" Value="0">-- Todos --</asp:ListItem>
                                    <asp:ListItem Value="1">Contratante</asp:ListItem>
                                    <asp:ListItem Value="2">Contratada</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="fornecedor_contrato" style="display:none;">
                    <table width="100%">
                        <tr>
                            <td width="30%" align="right">
                                Fornecedor(Contratada):
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFornecedorContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="cliente_contrato_diverso" style="display:none;">
                    <table width="100%">
                        <tr>
                            <td width="30%" align="right">
                                Cliente(Contratante):
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlClienteContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlStatusContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Forma de Pagamento:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlFormaPagamentoContratoDiverso" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Vencimento:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataVencimentoContratoDiversoDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                &nbsp;até&nbsp;
                <asp:TextBox ID="tbxDataVencimentoContratoDiversoAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Reajuste:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataReajusteDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                &nbsp;até&nbsp;
                <asp:TextBox ID="tbxDataReajusteAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Centro de Custo:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlCentroCusto" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Índice Financeiro:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlIndiceContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Setor:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlSetorContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td align="center">
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" Width="150px" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="GetGrupoEconomico" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetNomeEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="Como" HeaderText="Como" />
            <asp:BoundField DataField="Numero" HeaderText="Número" />            
            <asp:TemplateField HeaderText="Fornecedor/Cliente">                
                <ItemTemplate>
                    <%# BindFornecedorCliente(Container.DataItem)%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GetDescricaoStatus" HeaderText="Status" />
            <asp:BoundField DataField="GetFormaPagamento" HeaderText="Forma de Pagamento" />
            <asp:BoundField DataField="GetDataAbertura" HeaderText="Abertura" />
            <asp:BoundField DataField="GetDataVencimento" HeaderText="Vencimento" />
            <asp:BoundField DataField="GetDataReajuste" HeaderText="Reajuste" />            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

