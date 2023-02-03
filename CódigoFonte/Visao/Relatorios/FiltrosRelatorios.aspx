<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="FiltrosRelatorios.aspx.cs" Inherits="Relatorios_FiltrosRelatorios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>&nbsp;&nbsp;&nbsp;Escolha de Relatório</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_escolha_relatorio" class="container" style="height:400px;">
        <div class="div_container" style="width: 30%">
            <b>&nbsp;&nbsp;&nbsp; Escolha o Relatório:</b>
            <br />
            <asp:TreeView ID="trvRelatorios" runat="server" Width="100%" 
                OnSelectedNodeChanged="trvRelatorios_SelectedNodeChanged" 
                ImageSet="XPFileExplorer" NodeIndent="15">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                    HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle BackColor="#B5B5B5" 
                    Font-Bold="True" Font-Underline="False" HorizontalPadding="0px" 
                    VerticalPadding="0px" />
            </asp:TreeView>
        </div>
        <div class="div_container" style="width: 68%; margin-left: 1%">
            &nbsp;<asp:UpdatePanel ID="upFiltros" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mtvFiltros" runat="server" ActiveViewIndex="0">
                        <asp:View ID="view_inicial" runat="server">
                            <div style="text-align: center">
                                Escolha algum Relatório ao lado!</div>
                        </asp:View>
                        <asp:View ID="viewGruposEconomicos" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" width="30%">
                                        Administrador:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlAdministradorGruposEconomicos" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Cadastro de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataCadastroRelatorioGruposEconomicos" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataCadastroRelatorioGruposEconomicos_CalendarExtender"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataCadastroRelatorioGruposEconomicos"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataCadastroAtehRelatorioGruposEconomicos" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataCadastroAtehRelatorioGruposEconomicos_CalendarExtender"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataCadastroAtehRelatorioGruposEconomicos"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Possui Usuários Cadastrados:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPossuiUsuarios" runat="server" CssClass="DropDownList" Width="50%">
                                            <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                            <asp:ListItem Value="1">Possui Usuários Cadastrados</asp:ListItem>
                                            <asp:ListItem Value="2">Não Possui Usuários Cadastrados</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>                               
                            </table>
                        </asp:View>
                        <asp:View ID="view_OrgaosAmbientais" runat="server">
                            <div style="text-align: center">
                                Este relatório é carregado sem filtros!</div>
                        </asp:View>
                        <asp:View ID="view_Empresas" runat="server">
                            <div style="text-align: center">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right" width="30%">
                                            Grupo Econômico:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlGrupoEconomicoEmpresas" runat="server" CssClass="DropDownList"
                                                Width="50%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:View>
                        <asp:View ID="view_processo_meioAmbiente" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoProcessosMeioAmbiente" runat="server" AutoPostBack="True"
                                            CssClass="DropDownList" OnSelectedIndexChanged="ddlGrupoEconomicoProcessosMeioAmbiente_SelectedIndexChanged"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upEmpresasProcessosMeioAmbiente" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaProcessosMeioAmbiente" runat="server" CssClass="DropDownList"
                                                    Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Orgão Ambiental:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlOrgaoAmbientalProcessosMeioAmbiente" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_licencas_ambientais" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoLicencaAmbiental" runat="server" AutoPostBack="True"
                                            CssClass="DropDownList" OnSelectedIndexChanged="ddlGrupoEconomicoLicencaAmbiental_SelectedIndexChanged"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upEmpresasLicencasAmbientais" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaLicencaAmbiental" runat="server" CssClass="DropDownList"
                                                    Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoLicencaAmbiental" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Tipo de Licença:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlTipoLicencaAmbiental" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Validade:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataValidadeDeLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataValidadeDeLicencaAmbiental"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataValidadeAtehLicencaAmbiental" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataValidadeAtehLicencaAmbiental"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Prazo Limite de Renovação:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataLimiteLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataLimiteLicencaAmbiental"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataLimiteAtehLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataLimiteAtehLicencaAmbiental"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Órgão Ambiental:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlOrgaoAmbientalLicencaAmbiental" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEstadoLicencaAmbiental" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_condicionantes" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoCondicionantes" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlGrupoEconomicoCondicionantes_SelectedIndexChanged"
                                            Width="50%" CssClass="DropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upEmpresasCondicionantes" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaCondicionantes" runat="server" Width="50%" CssClass="DropDownList">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoCondicionantes" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de vencimento:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataVencimentoDeCondicionantes" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoDeCondicionantes"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataVencimentoAtehCondicionantes" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoAtehCondicionantes"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Órgão Ambiental:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlOrgaosAmbientaisCondicionantes" runat="server" Width="50%"
                                            CssClass="DropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>                               
                                <tr>
                                    <td align="right">
                                        Status:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlStatusCondicionante" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEstadoCondicionante" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>                                 
                                <tr>
                                    <td align="right">
                                        &nbsp;Condicionantes Periódicas:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCondicionantePeriodica" runat="server" CssClass="DropDownList" Width="30%">
                                         <asp:ListItem Value="0" Selected="True">-- Todas --</asp:ListItem>
                                         <asp:ListItem Value="1">Sim</asp:ListItem>
                                         <asp:ListItem Value="2">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Condicionantes com Prorrogação de Prazo:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCondicionanteProrrogacaoPrazo" runat="server" CssClass="DropDownList" Width="30%">
                                         <asp:ListItem Value="0" Selected="True">-- Todas --</asp:ListItem>
                                         <asp:ListItem Value="1">Sim</asp:ListItem>
                                         <asp:ListItem Value="2">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_outros_vencimentos" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoOutrosVencimentos" runat="server" CssClass="DropDownList"
                                            Width="50%" AutoPostBack="True" 
                                            onselectedindexchanged="ddlGrupoEconomicoOutrosVencimentos_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upEmpresasOutrosVencimentos" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaOutrosVencimentos" runat="server" CssClass="DropDownList"
                                                    Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoOutrosVencimentos" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Vencimento:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataVencimentoDeOutrosVencimentos" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoDeOutrosVencimentos"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataVencimentoAtehOutrosVencimentos" runat="server" Width="22%"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoAtehOutrosVencimentos"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Tipo:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlTipoOutrosVencimentos" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                        &nbsp;Vencimentos Periódicos:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlOutrosPeriodicos" runat="server" CssClass="DropDownList" Width="30%">
                                         <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                         <asp:ListItem Value="1">Sim</asp:ListItem>
                                         <asp:ListItem Value="2">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vencimentos com Prorrogação de Prazo:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlOutrosProrrogacaoPrazo" runat="server" CssClass="DropDownList" Width="30%">
                                         <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                         <asp:ListItem Value="1">Sim</asp:ListItem>
                                         <asp:ListItem Value="2">Não</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_processos_DNPM" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoProcessoDNPM" runat="server" CssClass="DropDownList"
                                            Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoEconomicoProcessoDNPM_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upEmpresasProcessoDNPM" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaProcessoDNPM" runat="server" CssClass="DropDownList"
                                                    Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoProcessoDNPM" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Regime / Fase:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlRegimeAtualProcessoDNPM" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEstadoProcessoDNPM" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_vencimentosPorPeriodo" runat="server">
                            <table style="width: 100%; margin-right: 0px;">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoVencimentos" runat="server" AutoPostBack="True"
                                            CssClass="DropDownList" OnSelectedIndexChanged="ddlGrupoEconomicoVencimentos_SelectedIndexChanged"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upEmpresaVencimentosPeriodo" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaVencimentos" runat="server" CssClass="DropDownList"
                                                    Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoVencimentos" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Tipo de Vencimento:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlTipoVencimentos" runat="server" CssClass="DropDownList"
                                            Width="50%" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTipoVencimentos_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" >
                                     <div id="vencimentos_diversos_periodo" runat="server" visible="false">
                                     <asp:UpdatePanel ID="UPStatusDiversoPeriodo" runat="server" 
                                             UpdateMode="Conditional" ChildrenAsTriggers="False">
                                         <ContentTemplate>
                                         <table width="100%">
                                             <tr>
                                                 <td width="30%" align="right">
                                                     Tipo de Vencimento Diverso:</td>
                                                 <td>
                                                     <asp:DropDownList ID="ddlTipoVencimentoDiversoPeriodo" runat="server" 
                                                         CssClass="DropDownList" Width="50%" 
                                                         
                                                         onselectedindexchanged="ddlTipoVencimentoDiversoPeriodo_SelectedIndexChanged" 
                                                         AutoPostBack="True">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td align="right">
                                                     Status do Vencimento:</td>
                                                 <td>
                                                    <asp:DropDownList ID="ddlStatusVencimentoDiversoPeriodo" runat="server" CssClass="DropDownList" Width="50%">
                                                     </asp:DropDownList>                                                         
                                                 </td>
                                             </tr>
                                         </table>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentoDiversoPeriodo" 
                                                 EventName="SelectedIndexChanged" />
                                             <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentos" 
                                                 EventName="SelectedIndexChanged" />
                                         </Triggers>
                                        </asp:UpdatePanel>
                                        </div>
                                     </td>
                                </tr>
                                <tr>
                                    <td colspan="2" >
                                     <div id="vencimentos_por_periodo_simples" runat="server" visible="true">
                                         <table width="100%">
                                             <tr>
                                                 <td width="30%" align="right">
                                                     Status do Vencimento:</td>
                                                 <td>
                                                     <asp:DropDownList ID="ddlStatusVencimentoPorPeriodo" runat="server" 
                                                         CssClass="DropDownList" Width="50%">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                         </table>
                                        </div>
                                     </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Período:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataDePeriodoVencimentos" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataDePeriodoVencimentos"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataAtehPeriodoVencimentos" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataAtehPeriodoVencimentos"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEstadoVencimentoPeriodo" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;Vencimentos Periódicos:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPeriodicosVencimentoPeriodo" runat="server" CssClass="DropDownList" Width="30%">
                                         <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                         <asp:ListItem Value="1">Sim</asp:ListItem>
                                         <asp:ListItem Value="2">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vencimentos com Prorrogação de Prazo:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlProrrogacaoPrazoVencimentoPeriodo" runat="server" CssClass="DropDownList" Width="30%">
                                         <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                         <asp:ListItem Value="1">Sim</asp:ListItem>
                                         <asp:ListItem Value="2">Não</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_rals" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoRal" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                            OnSelectedIndexChanged="ddlGrupoEconomicoRal_SelectedIndexChanged" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upRals" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaRal" runat="server" CssClass="DropDownList" Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoRal" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vencimento de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataVencimentoRalDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender11" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoRalDe"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataVencimentoRalAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender12" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoRalAte"></asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_guiasUtilizacao" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoGuiaUtilizacao" runat="server" AutoPostBack="True"
                                            CssClass="DropDownList" OnSelectedIndexChanged="ddlGrupoEconomicoGuiaUtilizacao_SelectedIndexChanged"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upGuiaUtilizacao" runat="server" ChildrenAsTriggers="False"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaGuiaUtilizacao" runat="server" CssClass="DropDownList"
                                                    Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoGuiaUtilizacao" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vencimento de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataVencimentoDeGuiaUtilizacao" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender13" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoDeGuiaUtilizacao"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataVencimentoAteGuiaUtilizacao" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender14" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoAteGuiaUtilizacao"></asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_cadastroTecnico" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Grupo Econômico:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGrupoEconomicoCTF" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                            OnSelectedIndexChanged="ddlGrupoEconomicoCTF_SelectedIndexChanged" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Empresa:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpresaCTF" runat="server" CssClass="DropDownList" Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers><asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoGuiaUtilizacao" EventName="SelectedIndexChanged" /></Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Entrega do Relatório Anual de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataEntregaRelatorioAnualDe" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender15" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataEntregaRelatorioAnualDe"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataEntregaRelatorioAnualAte" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender16" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataEntregaRelatorioAnualAte"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vencimeto da Taxa Trimestral de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataVencimentoTaxaTrimestralDe" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender17" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoTaxaTrimestralDe"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataVencimentoTaxaTrimestralAte" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender18" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoTaxaTrimestralAte"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vencimento do Certificado de Regularidade de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataVencimentoCertificadoRegularidadeDe" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender19" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoCertificadoRegularidadeDe"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataVencimentoCertificadoRegularidadeAte" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender20" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataVencimentoCertificadoRegularidadeAte"></asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_pendencias_grupos_economicos" runat="server">
                            <div style="text-align: center">
                                Este relatório é carregado sem filtros!</div>
                        </asp:View>
                        <asp:View ID="view_notificacoes_enviadas" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%" align="right">
                                        Data de envio da Notificação de:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDataDeNotificacaoEnviada" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender21" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataDeNotificacaoEnviada"></asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataAtehNotificacaoEnviada" runat="server" CssClass="TextBox"
                                            Width="22%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender22" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataAtehNotificacaoEnviada"></asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_vencimentos_diversos" runat="server">                         
                             <asp:UpdatePanel ID="UPVencimentosDiversos" runat="server" 
                                 UpdateMode="Conditional">                                 
                                 <ContentTemplate>
                                     <table width="100%">
                                         <tr>
                                             <td align="right" width="30%">Grupo Econômico:</td>
                                             <td align="left">
                                                 <asp:DropDownList ID="ddlGrupoEconomicoVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" 
                                                     onselectedindexchanged="ddlGrupoEconomicoVencimentoDiverso_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right" width="30%">Empresa:</td>
                                             <td align="left">
                                                 <asp:DropDownList ID="ddlEmpresaVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right" width="30%">
                                                 Descrição:</td>
                                             <td align="left">
                                                 <asp:TextBox ID="tbxDescricaoVencimentoDiverso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right" width="30%">
                                                 Tipo de vencimento:</td>
                                             <td align="left">
                                                 <asp:DropDownList ID="ddlTipoVencimentoDiverso" runat="server" 
                                                     CssClass="DropDownList" Width="50%" AutoPostBack="True" 
                                                     onselectedindexchanged="ddlTipoVencimentoDiverso_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right" width="30%">
                                                 Status do vencimento:</td>
                                             <td align="left">
                                                 <asp:DropDownList ID="ddlStatusVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right" width="30%">
                                                 Data de vencimento de:</td>
                                             <td align="left">
                                                 <asp:TextBox ID="tbxDataDeVencimentoDiverso" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender23" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataDeVencimentoDiverso">
                                                 </asp:CalendarExtender>
                                                 &nbsp;até&nbsp;
                                                 <asp:TextBox ID="tbxDataAteVencimentoDiverso" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender24" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataAteVencimentoDiverso">
                                                 </asp:CalendarExtender>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">&nbsp;Vencimentos Periódicos:</td>
                                             <td align="left">
                                                 <asp:DropDownList ID="ddlPeriodicosVencimentosDiverso" runat="server" CssClass="DropDownList" Width="30%">
                                                     <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                                     <asp:ListItem Value="1">Sim</asp:ListItem>
                                                     <asp:ListItem Value="2">Não</asp:ListItem>
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                     </table>
                                 </ContentTemplate>                                 
                                 <Triggers>
                                     <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoVencimentoDiverso" 
                                         EventName="SelectedIndexChanged" />
                                     <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentoDiverso" 
                                         EventName="SelectedIndexChanged" />
                                 </Triggers>
                             </asp:UpdatePanel>
                        </asp:View>
                        <asp:View ID="view_renuncias_alvaras" runat="server">
                            <asp:UpdatePanel ID="UPRenunciasAlvaras" runat="server" 
                                UpdateMode="Conditional">
                                <ContentTemplate>                                    
                                    <table width="100%">
                                        <tr>
                                            <td width="30%" align="right">
                                                Grupo Econômico:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlGrupoRenuncias" runat="server" CssClass="DropDownList" 
                                                    Width="50%" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlGrupoRenuncias_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Empresa:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlEmpresaRenuncias" runat="server" CssClass="DropDownList" Width="50%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vencimento de:</td>
                                            <td>
                                                <asp:TextBox ID="tbxDataVencimentoRenunciaDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender25" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataVencimentoRenunciaDe">
                                                </asp:CalendarExtender>&nbsp;até&nbsp;
                                                <asp:TextBox ID="tbxDataVencimentoRenunciaAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender26" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataVencimentoRenunciaAte">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Estado:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlEstadoRenuncias" runat="server" CssClass="DropDownList" Width="50%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoRenuncias" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:View>
                        <asp:View ID="view_clientes" runat="server">
                          <div id="clientes">
                              <table width="100%">
                                  <tr>
                                      <td width="30%" align="right">
                                          Nome/Razão Social:</td>
                                      <td align="left">
                                          <asp:TextBox ID="tbxNomeRazaoCliente" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          CNPJ/CPF:</td>
                                      <td align="left">
                                          <asp:TextBox ID="tbxCnpjCpfCliente" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Atividade:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlAtividadeCliente" runat="server" CssClass="DropDownList" Width="50%">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Status:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlStatusCliente" runat="server" CssClass="DropDownList" Width="50%">
                                            <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                            <asp:ListItem Value="1">ATIVOS</asp:ListItem>
                                            <asp:ListItem Value="2">INATIVOS</asp:ListItem>
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Estado:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlEstadoCliente" runat="server" CssClass="DropDownList" 
                                              Width="50%" AutoPostBack="True" 
                                              onselectedindexchanged="ddlEstadoCliente_SelectedIndexChanged">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Cidades:</td>
                                      <td align="left">
                                          <asp:UpdatePanel ID="UPCidadeCliente" runat="server" UpdateMode="Conditional">
                                              <ContentTemplate>
                                                  <asp:DropDownList ID="ddlCidadeCliente" runat="server" CssClass="DropDownList" Width="50%">
                                                  </asp:DropDownList>
                                              </ContentTemplate>
                                              <Triggers>
                                                  <asp:AsyncPostBackTrigger ControlID="ddlEstadoCliente" 
                                                      EventName="SelectedIndexChanged" />
                                              </Triggers>
                                          </asp:UpdatePanel>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                              </table>
                            </div>
                        </asp:View>
                        <asp:View ID="view_fornecedores" runat="server">
                         <div id="fornecedores">
                           <table width="100%">
                                  <tr>
                                      <td width="30%" align="right">
                                          Nome/Razão Social:</td>
                                      <td align="left">
                                          <asp:TextBox ID="tbxNomeRazaoFornecedor" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          CNPJ/CPF:</td>
                                      <td align="left">
                                          <asp:TextBox ID="tbxCnpjCpfFornecedor" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Atividade:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlAtividadeFornecedor" runat="server" CssClass="DropDownList" Width="50%">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Status:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlStatusFornecedor" runat="server" CssClass="DropDownList" Width="50%">
                                            <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                            <asp:ListItem Value="1">ATIVOS</asp:ListItem>
                                            <asp:ListItem Value="2">INATIVOS</asp:ListItem>
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Estado:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlEstadoFornecedor" runat="server" CssClass="DropDownList" 
                                              Width="50%" AutoPostBack="True" 
                                              onselectedindexchanged="ddlEstadoFornecedor_SelectedIndexChanged">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Cidades:</td>
                                      <td align="left">
                                          <asp:UpdatePanel ID="UPCidadeFornecedor" runat="server" UpdateMode="Conditional">
                                              <ContentTemplate>
                                                  <asp:DropDownList ID="ddlCidadeFornecedor" runat="server" CssClass="DropDownList" Width="50%">
                                                  </asp:DropDownList>
                                              </ContentTemplate>
                                              <Triggers>
                                                  <asp:AsyncPostBackTrigger ControlID="ddlEstadoFornecedor" 
                                                      EventName="SelectedIndexChanged" />
                                              </Triggers>
                                          </asp:UpdatePanel>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                              </table>
                         </div>
                        </asp:View>
                        <asp:View ID="view_contratos_diversos" runat="server">
                        <div id="contratos_diversos">                        
                            <asp:UpdatePanel ID="UPCOntratos" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                               <table width="100%">
                                  <tr>
                                      <td width="30%" align="right">
                                          Grupo Econômico:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlGrupoContratosDiversos" runat="server" 
                                              CssClass="DropDownList" Width="50%" AutoPostBack="True" 
                                              onselectedindexchanged="ddlGrupoContratosDiversos_SelectedIndexChanged">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right" width="30%">
                                          Empresa:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlEmpresaContratosDiversos" runat="server" 
                                              CssClass="DropDownList" Width="50%" AutoPostBack="True" 
                                              onselectedindexchanged="ddlEmpresaContratosDiversos_SelectedIndexChanged">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                   <tr>
                                       <td colspan="2">
                                          <div id="empresa_como" runat="server" visible="false">                                          
                                            <table width="100%">
                                                <tr>
                                                    <td width="30%" align="right">
                                                        Como:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlComoContratosDiversos" runat="server" 
                                                            AutoPostBack="True" CssClass="DropDownList" 
                                                            onselectedindexchanged="ddlComoContratosDiversos_SelectedIndexChanged" 
                                                            Width="50%">
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
                                        <div id="fornecedor_contrato" runat="server" visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td width="30%" align="right">
                                                        Fornecedor(Contratada):</td>
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
                                        <div id="cliente_contrato_diverso" runat="server" visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td width="30%" align="right">
                                                        Cliente(Contratante):</td>
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
                                           Status:</td>
                                       <td align="left">
                                           <asp:DropDownList ID="ddlStatusContratosDiversos" runat="server" CssClass="DropDownList" 
                                               Width="50%">                                               
                                           </asp:DropDownList>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="right">
                                           Forma de Pagamento:</td>
                                       <td align="left">
                                           <asp:DropDownList ID="ddlFormaPagamentoContratoDiverso" runat="server" CssClass="DropDownList" Width="50%">
                                           </asp:DropDownList>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="right">
                                           Data de Vencimento:</td>
                                       <td align="left">
                                         <asp:TextBox ID="tbxDataVencimentoContratoDiversoDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender27" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataVencimentoContratoDiversoDe">
                                                </asp:CalendarExtender>&nbsp;até&nbsp;
                                                <asp:TextBox ID="tbxDataVencimentoContratoDiversoAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender28" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataVencimentoContratoDiversoAte">
                                                </asp:CalendarExtender>
                                        </td>
                                   </tr>
                                   <tr>
                                       <td align="right">
                                           Data de Reajuste:</td>
                                       <td align="left">
                                           <asp:TextBox ID="tbxDataReajusteDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender29" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataReajusteDe">
                                                </asp:CalendarExtender>&nbsp;até&nbsp;
                                                <asp:TextBox ID="tbxDataReajusteAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender30" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataReajusteAte">
                                                </asp:CalendarExtender></td>
                                   </tr>
                                  <tr>
                                      <td align="right">
                                          Centro de Custo:</td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddlCentroCusto" runat="server" CssClass="DropDownList" 
                                              Width="50%">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          Índice Financeiro:</td>
                                      <td align="left">                                          
                                          <asp:DropDownList ID="ddlIndiceContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                                                  </asp:DropDownList>                                              
                                      </td>
                                  </tr>
                                   <tr>
                                       <td align="right">
                                           Setor:</td>
                                       <td align="left">
                                           <asp:DropDownList ID="ddlSetorContratosDiversos" runat="server" CssClass="DropDownList" Width="50%">
                                           </asp:DropDownList>
                                       </td>
                                   </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                              </table>
                             </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlComoContratosDiversos" 
                                        EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoContratosDiversos" 
                                        EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEmpresaContratosDiversos" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        </asp:View>
                        <asp:View ID="view_ContratosPorProcessos" runat="server">
                         <div id="conteudo_contratos_por_processos">
                             <asp:UpdatePanel ID="UPContratos_por_processos" runat="server" 
                                 UpdateMode="Conditional">
                              <ContentTemplate>
                                  <table width="100%">
                                      <tr>
                                          <td width="30%" align="right">
                                              Agrupar por:</td>
                                          <td align="left">
                                              <asp:DropDownList ID="ddlAgrupor" runat="server" CssClass="DropDownList" 
                                                  Width="50%" AutoPostBack="True" 
                                                  onselectedindexchanged="ddlAgrupor_SelectedIndexChanged">
                                               <asp:ListItem Selected="True" Value="0">-- Selecione --</asp:ListItem>
                                               <asp:ListItem Value="1">Processos</asp:ListItem>
                                               <asp:ListItem Value="2">Contratos</asp:ListItem>
                                              </asp:DropDownList>
                                          </td>
                                      </tr>
                                  </table>
                                  <div id="contratos_por_processos" runat="server" visible="false">
                                      <table width="100%">
                                          <tr>
                                              <td width="30%" align="right">
                                                  Tipo de Processo:</td>
                                              <td>
                                                  <asp:DropDownList ID="ddlTipoProcesso" runat="server" CssClass="DropDownList" 
                                                      Width="50%" AutoPostBack="True" 
                                                      onselectedindexchanged="ddlTipoProcesso_SelectedIndexChanged">                                                    
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="right" width="30%">
                                                  Empresa:</td>
                                              <td>
                                                  <asp:DropDownList ID="ddlEmpresaContratoPorProcesso" runat="server" CssClass="DropDownList" Width="50%">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="right">
                                                  Número do processo:</td>
                                              <td>
                                                  <asp:TextBox ID="tbxNumeroProcesso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td colspan="2">
                                                <div id="pesquisa_substancia" runat="server" visible="false">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" width="30%">
                                                                Substância:</td>
                                                            <td>
                                                                <asp:TextBox ID="tbxSubstanciaContratoPorProcesso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                  </div>
                                                <div id="pesquisa_tipo_orgao_processo" runat="server" visible="false">
                                                   <table width="100%">
                                                        <tr>
                                                            <td align="right" width="30%">
                                                                Tipo dos Processos Ambientais:</td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblTipoProcesso" runat="server" CellPadding="4" 
                                                                    CellSpacing="4" Font-Bold="False" Font-Names="Arial" Font-Size="Small" 
                                                                    RepeatDirection="Horizontal">
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
                                      </table>
                                  </div>
                                  <div id="processos_por_contratos" runat="server" visible="false">
                                      <table width="100%">
                                          <tr>
                                              <td width="30%" align="right">
                                                  Tipo de Processo:</td>
                                              <td>
                                                  <asp:DropDownList ID="ddlTipoProcessoProcessosPorContrato" runat="server" CssClass="DropDownList" Width="50%">                                                    
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="right" width="30%">
                                                  Número do Contrato:</td>
                                              <td>
                                                  <asp:TextBox ID="tbxNumeroContratoPorProcesso" runat="server" 
                                                      CssClass="TextBox" Width="50%"></asp:TextBox>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="right">
                                                  Objeto do Contrato:</td>
                                              <td>
                                                  <asp:TextBox ID="tbxObjetoContratoPorProcesso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td align="right">
                                                  Status do Contrato:</td>
                                              <td>
                                                  <asp:DropDownList ID="ddlStatusContratoProcessoPorContrato" runat="server" CssClass="DropDownList" Width="50%">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                      </table>
                                  </div>
                              </ContentTemplate>
                                 <Triggers>
                                     <asp:AsyncPostBackTrigger ControlID="ddlAgrupor" 
                                         EventName="SelectedIndexChanged" />
                                 </Triggers>
                             </asp:UpdatePanel>
                         </div>
                        </asp:View>
                    </asp:MultiView>
                    <div align="center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" 
                            OnClick="btnExibirRelatório_Click" OnClientClick="aspnetForm.target =’_blank’;" 
                            Text="Exibir Relatório" Visible="False" Width="150px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o relatório escolhido com base nos filtros escolhidos')"  />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="trvRelatorios" EventName="SelectedNodeChanged">
                    </asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
