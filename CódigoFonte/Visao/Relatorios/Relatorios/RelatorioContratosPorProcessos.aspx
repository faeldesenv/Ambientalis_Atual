<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioContratosPorProcessos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioContratosPorProcessos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            exibirEsconderTipoPesquisa();
            $('#<%=ddlTipoProcesso.ClientID %>').change(function () { exibirEsconderTipoPesquisa(); });

        });

        function exibirEsconderTipoPesquisa() {

            if (eval($('#<%=ddlTipoProcesso.ClientID %>').val()) > 0) {

                if (eval($('#<%=ddlTipoProcesso.ClientID %>').val()) == 1) {

                    $('#pesquisa_substancia').css('display', 'block');
                    $('#pesquisa_tipo_orgao_processo').css('display', 'none');

                } else {

                    $('#pesquisa_substancia').css('display', 'none');
                    $('#pesquisa_tipo_orgao_processo').css('display', 'block');
                }

            } else {

                $('#pesquisa_substancia').css('display', 'none');
                $('#pesquisa_tipo_orgao_processo').css('display', 'none');
                $('#<%=tbxSubstanciaContratoPorProcesso.ClientID %>').val()
                $('#<%=rblTipoProcesso.ClientID %>').val('0')               

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
    Contratos por Processos
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
    <table width="100%">
        <tr>
            <td width="30%" align="right">Tipo de Processo:
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoProcesso" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoProcesso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">Grupo Econômico:</td>
            <td>
                <asp:DropDownList ID="ddlGrupoEconomicoContratoPorProcesso" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoEconomicoContratoPorProcesso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">Empresa:
            </td>
            <td>
                <asp:UpdatePanel ID="upEmpresas" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaContratoPorProcesso" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoContratoPorProcesso" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoProcesso" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td align="right">Número do processo:
            </td>
            <td>
                <asp:TextBox ID="tbxNumeroProcesso" runat="server" CssClass="TextBox" Width="50%">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="pesquisa_substancia" style="display:none;">
                    <table width="100%">
                        <tr>
                            <td align="right" width="30%">Substância:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxSubstanciaContratoPorProcesso" runat="server" CssClass="TextBox" Width="50%">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="pesquisa_tipo_orgao_processo" style="display:none;">
                    <table width="100%">
                        <tr>
                            <td align="right" width="30%">Tipo dos Processos Ambientais:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblTipoProcesso" runat="server" CellPadding="4" CellSpacing="4" Font-Bold="False" Font-Names="Arial" Font-Size="Small" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Municipais</asp:ListItem>
                                    <asp:ListItem Value="2">Estaduais</asp:ListItem>
                                    <asp:ListItem Value="3">Federais</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td width="30%" align="right">&nbsp;</td>
            <td align="center">
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" Width="150px" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="Processo" HeaderText="Processo" />
            <asp:BoundField DataField="Numero" HeaderText="Número do Contrato" />
            <asp:BoundField DataField="Objeto" HeaderText="Objeto" />            
            <asp:BoundField DataField="Status" HeaderText="Status" />            
            <asp:BoundField DataField="Abertura" HeaderText="Data de Abertura" />  
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

