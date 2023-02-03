<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioVencimentosPorPeriodo.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioVencimentosPorPeriodo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataDePeriodoVencimentos.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataAtehPeriodoVencimentos.ClientID %>"));

            exibirEsconderTiposDiversos();
            $('#<%=ddlTipoVencimentos.ClientID %>').change(function () { exibirEsconderTiposDiversos(); });
        });

        function exibirEsconderTiposDiversos() {
            if ($('#<%=ddlTipoVencimentos.ClientID %>').val() == 'Vencimento Diverso') {
                $('#vencimentos_diversos_periodo').css('display', 'block');
                $('#vencimentos_por_periodo_simples').css('display', 'none');
            } else {
                $('#vencimentos_diversos_periodo').css('display', 'none');
                $('#vencimentos_por_periodo_simples').css('display', 'block');
            }

        }
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Vencimentos por período
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" runat="Server">
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
    <div>
        <table style="width: 100%; margin-right: 0px;">
            <tr>
                <td align="right" style="width: 30%;">Grupo Econômico:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlGrupoEconomicoVencimentos" runat="server" AutoPostBack="True" CssClass="DropDownList"
                        OnSelectedIndexChanged="ddlGrupoEconomicoVencimentos_SelectedIndexChanged" Width="50%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">Empresa:
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="upEmpresaVencimentosPeriodo" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlEmpresaVencimentos" runat="server" CssClass="DropDownList" Width="50%">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoVencimentos" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentos" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right">Tipo de Vencimento:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlTipoVencimentos" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoVencimentos_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="vencimentos_diversos_periodo" style="display: none;">
                        <asp:UpdatePanel ID="UPStatusDiversoPeriodo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td width="30%" align="right">Tipo de Vencimento Diverso:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoVencimentoDiversoPeriodo" runat="server" CssClass="DropDownList" Width="50%" OnSelectedIndexChanged="ddlTipoVencimentoDiversoPeriodo_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Status do Vencimento:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatusVencimentoDiversoPeriodo" runat="server" CssClass="DropDownList" Width="50%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentoDiversoPeriodo" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentos" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="vencimentos_por_periodo_simples">
                        <table width="100%">
                            <tr>
                                <td width="30%" align="right">Status do Vencimento:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusVencimentoPorPeriodo" runat="server" CssClass="DropDownList" Width="50%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">Período:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxDataDePeriodoVencimentos" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>&nbsp;até
                <asp:TextBox ID="tbxDataAtehPeriodoVencimentos" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Estado:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlEstadoVencimentoPeriodo" runat="server" CssClass="DropDownList" Width="50%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">&nbsp;Vencimentos Periódicos:</td>
                <td align="left">
                    <asp:DropDownList ID="ddlPeriodicosVencimentoPeriodo" runat="server" CssClass="DropDownList" Width="30%">
                        <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                        <asp:ListItem Value="1">Sim</asp:ListItem>
                        <asp:ListItem Value="2">Não</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">Vencimentos com Prorrogação de Prazo:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProrrogacaoPrazoVencimentoPeriodo" runat="server" CssClass="DropDownList" Width="30%">
                        <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                        <asp:ListItem Value="1">Sim</asp:ListItem>
                        <asp:ListItem Value="2">Não</asp:ListItem>
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
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="GetNomeGrupoEconomico" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetNomeEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="GetTipo" HeaderText="Tipo" />
            <asp:BoundField DataField="GetNumeroProcessoVencimento" HeaderText="Nº Proc/Venc" />
            <asp:BoundField DataField="getDescricaoStatus" HeaderText="Status" />
            <asp:BoundField DataField="GetEhPeriodico" HeaderText="Períodico" />
            <asp:BoundField DataField="GetProrrogacoesPrazo" HeaderText="Prorrogações" />
            <asp:BoundField DataField="Data" HeaderText="Vencimento" DataFormatString="{0:d}" />
            <asp:BoundField DataField="GetDescricaoTipo" HeaderText="Descrição" />
            <asp:BoundField DataField="GetSiglaEstado" HeaderText="UF" />

        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

