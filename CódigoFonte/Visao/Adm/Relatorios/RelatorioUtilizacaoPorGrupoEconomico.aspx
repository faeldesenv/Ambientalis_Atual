<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioUtilizacaoPorGrupoEconomico.aspx.cs" Inherits="Adm_Relatorios_RelatorioUtilizacaoPorGrupoEconomico" %>

<%@ Register Src="~/Adm/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    utilização por grupos econômicos
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
    <table width="100%">
        <tr>
            <td align="right" width="30%">
                Sistema:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlSistemaRelUtilizacao" runat="server" AutoPostBack="True" CssClass="DropDownList" ValidationGroup="rfv" Width="50%" onselectedindexchanged="ddlSistemaRelUtilizacao_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">Sustentar</asp:ListItem>
                    <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Grupo Econômico:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlGrupoEconomicoRelUtilizacao" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoRelUtilizacao" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
            <asp:Label ID="lblSistema" runat="server" Visible="false"></asp:Label>
        </tr>
        <tr>
            <td width="30%" align="right">&nbsp;</td>
            <td align="center">
                <br />
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" Width="150px" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetQtEmpresasCadastradas" HeaderText="Empresas Cadastradas" />
            <asp:BoundField DataField="GetTotalProcessosAmbientaisDoGrupo" HeaderText="Processos Ambientais" />
            <asp:BoundField DataField="GetTotalProcessosMinerariosDoGrupo" HeaderText="Processos Minerários" />                                           
            <asp:BoundField DataField="GetTotalContratosDiversosDoGrupo" HeaderText="Contratos" />                     
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
    <div style="margin-top:15px; text-align:left;">
        Totalizadores<br />
        Total de Empresas:&nbsp:<asp:Label ID="lblTotalEmpresas" runat="server"></asp:Label><br />
        Total de Processos Ambientais:&nbsp:<asp:Label ID="lblTotalProcAmbientais" runat="server"></asp:Label><br />
        Total de Processos Minerários:&nbsp:<asp:Label ID="lblTotalProcMinerarios" runat="server"></asp:Label><br />
        Total de Contratos:&nbsp:<asp:Label ID="lblTotalContratos" runat="server"></asp:Label>
    </div>
</asp:Content>

