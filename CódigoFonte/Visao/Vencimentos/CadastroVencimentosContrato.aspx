<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="CadastroVencimentosContrato.aspx.cs" Inherits="Vencimentos_CadastroVencimentosDiversosContrato" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
        .box_renovacoes
        {
            width: 120px;
            float: left;
            margin: 3px;
            padding: 5px;
            background-color: #fcfcfc;
            text-align: center;
            height: 60px;
            border-radius: 5px;
            behavior: url(../Utilitarios/htc/PIE.htc);
        }
        .box_renovacoes:hover
        {
            background-color: #f0f3ed;
        }
        .box_renovacoes_selecionado
        {
            width: 120px;
            float: left;
            margin: 3px;
            padding: 5px;
            background-color: #f9f9fc;
            border: 1px solid #037629;
            text-align: center;
            height: 60px;
            border-radius: 5px;
            behavior: url(../Utilitarios/htc/PIE.htc);
        }
        .renovado
        {
            width: 20px;
            float: left;
            background-image: url('../imagens/setaDireita.jpg');
            background-position: center;
            background-repeat: no-repeat;
            margin: 1px;
            height: 60px;
        }
        .boxRenovacoes
        {
            padding: 10px;
            border: 1px solid #f0f3f1;
            border-radius: 11px;
            behavior: url(../Utilitarios/htc/PIE.htc);
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= tbxValorContrato.ClientID %>").maskMoney({ thousands: '.', decimal: ',' });
        });

        function marcarEmailsGrupo() {
            $('.chkMarcarEmailsGrupo').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.chkEmailsGruposEconomicosCont').children().find('input').each(function () {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                    } else {
                        $(this).removeAttr('checked');
                    }
                });
            });
        }

        function marcarEmailsEmpresa() {
            $('.chkMarcarEmailsEmpresa').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.chkEmailsEmpresasCont').children().find('input').each(function () {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                    } else {
                        $(this).removeAttr('checked');
                    }
                });
            });
        }

        function verificarPermissaoSetor() {
            if (alert('Você não possui permissão para acessar contratos deste setor.')) {
                $(window.document.location).attr('href', 'PesquisaVencimentosContrato.aspx');
            } else
            {
                $(window.document.location).attr('href', 'PesquisaVencimentosContrato.aspx');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Cadastro de contrato
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="pnlDefault" runat="server" DefaultButton="btnSalvar">
            <table width="100%">
                <tr>
                    <td align="center" colspan="2" style="width: 100%">
                        <div class="boxRenovacoes">
                            <table>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rptRenovacoes" runat="server">
                                                    <ItemTemplate>
                                                        <div class="<%# BindCss(Container.DataItem) %>">
                                                            <asp:HyperLink ID="hplRenovacao" runat="server" Font-Bold="<%# BindBold(Container.DataItem) %>"
                                                                NavigateUrl="<%# BindLink(Container.DataItem) %>"><%# BindRenovacao(Container.DataItem) %></asp:HyperLink>
                                                        </div>
                                                    </ItemTemplate>
                                                    <SeparatorTemplate>
                                                        <div class="renovado">
                                                        </div>
                                                    </SeparatorTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnRenovar" runat="server" CssClass="Button" OnClick="btnRenovar_Click"
                                                    OnPreRender="btnRenovar_PreRender" Text="Renovar este Contrato" ValidationGroup="rfvSalvarDiverso"
                                                    Width="230px" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnRenovar" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;
                    </td>
                    <td width="65%">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblAcao" runat="server" Font-Names="Arial" Font-Size="Large" ForeColor="#CCCCCC"
                                    Font-Italic="True"></asp:Label>
                                <asp:HiddenField ID="hfRenovacao" runat="server" />
                                <asp:HiddenField ID="hfId" runat="server" />
                                <asp:HiddenField ID="hfContrato" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Grupo Econômico:
                    </td>
                    <td width="65%">
                        <asp:DropDownList ID="ddlGrupoEconomico" runat="server" AutoPostBack="True" CssClass="DropDownList"
                            OnSelectedIndexChanged="ddlGrupoEconomico_SelectedIndexChanged" Width="358px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Empresa:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPEmpresa" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList" Width="358px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- Selecione um Grupo --</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="imgComo" runat="server" ImageUrl="~/imagens/seta_como.png" />
                                        </td>
                                        <td valign="middle">
                                            <asp:DropDownList ID="ddlComo" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                                OnSelectedIndexChanged="ddlComo_SelectedIndexChanged" Width="200px" OnInit="ddlComo_Init">
                                                <asp:ListItem>-- Selecione --</asp:ListItem>
                                                <asp:ListItem>Contratada</asp:ListItem>
                                                <asp:ListItem>Contratante</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%" colspan="2">
                        <asp:UpdatePanel ID="upClienteFornecedor" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="35%">
                                            <asp:Label ID="lblClienteFornecedor" runat="server"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlClienteFornecedor" runat="server" CssClass="DropDownList"
                                                Width="358px">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lxbVisualizarEmpresa" runat="server" Font-Size="8pt" OnClick="lxbVisualizarEmpresa_Click1">Visualizar dados da Empresa</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlComo" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Número do Contrato
                    </td>
                    <td>
                        <asp:TextBox ID="tbxCodigo" CssClass="TextBox" Width="350px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Data de Abertura:
                    </td>
                    <td>
                        <asp:TextBox ID="tbxDataAbertura" CssClass="TextBox" Width="120px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbxDataAbertura_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="tbxDataAbertura">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Objeto:
                    </td>
                    <td>
                        <asp:TextBox ID="tbxObjeto" CssClass="TextBox" Width="350px" runat="server" Height="81px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Moeda:<br />
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="DropDownList" Width="200px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlMoeda_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        <asp:UpdatePanel ID="upSigla" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                Valor do Contrato<asp:Label ID="lblSiglaMoeda" runat="server"></asp:Label>
                                :
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlMoeda" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxValorContrato" CssClass="TextBox" Width="239px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;<asp:UpdatePanel ID="upFormaRecebimento" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblFormaPagamentoRecebimento" runat="server" Text="Forma de Recebimento/Pagamento"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPRecebimento" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlFormaRecebimento" runat="server" CssClass="DropDownList"
                                    Width="200px">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnAdicionarFormaRecebimento" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    Style="width: 20px" ToolTip="Novo" OnClick="btnAdicionarFormaRecebimento_Click"
                                    OnInit="btnAdicionarFormaRecebimento_Init" />
                                &nbsp;
                                <asp:ImageButton ID="btnEditarFormaRecebimento" runat="server" ImageUrl="~/imagens/icone_editar.png"
                                    Style="width: 20px" ToolTip="Editar" OnClick="btnEditarFormaRecebimento_Click"
                                    OnInit="btnEditarFormaRecebimento_Init" />
                                &nbsp;
                                <asp:ImageButton ID="ibtnExcluirFormaRecebimento" runat="server" ImageUrl="~/imagens/excluir.gif"
                                    ToolTip="Excluír" Width="20px" OnClick="ibtnExcluirFormaRecebimento_Click" OnPreRender="ibtnExcluirFormaRecebimento_PreRender" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Centro de Custo:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPCentroCusto" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCentroCusto" runat="server" Width="358px" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnAdicionarCentroCusto" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    OnClick="btnAdicionarCentroCusto_Click" Style="width: 20px" ToolTip="Novo" OnInit="btnAdicionarCentroCusto_Init" />&nbsp;
                                <asp:ImageButton ID="btnEditarCentroCusto" runat="server" ImageUrl="~/imagens/icone_editar.png"
                                    OnClick="btnEditarCentroCusto_Click" Style="width: 20px" ToolTip="Editar" OnInit="btnEditarCentroCusto_Init" />
                                &nbsp;
                                <asp:ImageButton ID="ibtnExcluirCentroCusto" runat="server" ImageUrl="~/imagens/excluir.gif"
                                    OnClick="ibtnExcluirCentroCusto_Click" OnPreRender="ibtnExcluirCentroCusto_PreRender"
                                    Width="20px" ToolTip="Excluír" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Setor:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPSetor" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlSetor" runat="server" Width="358px" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnAdicionarSetor" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    Style="width: 20px" OnClick="btnAdicionarSetor_Click" ToolTip="Novo" OnInit="btnAdicionarSetor_Init" />&nbsp;
                                <asp:ImageButton ID="btnEditarSetor" runat="server" ImageUrl="~/imagens/icone_editar.png"
                                    OnClick="btnEditarSetor_Click" Style="width: 20px" ToolTip="Editar" OnInit="btnEditarSetor_Init" />
                                &nbsp;
                                <asp:ImageButton ID="ibtnExcluirSetor" runat="server" ImageUrl="~/imagens/excluir.gif"
                                    OnClick="ibtnExcluirSetor_Click" OnPreRender="ibtnExcluirSetor_PreRender" Width="20px"
                                    ToolTip="Excluír" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Índice de Reajuste:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPIndice" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlIndiceReajuste" runat="server" Width="358px" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnAdicionarReajuste" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    OnClick="btnAdicionarReajuste_Click" ToolTip="Novo" OnInit="btnAdicionarReajuste_Init"
                                    Height="20px" />&nbsp;
                                <asp:ImageButton ID="btnEditarIndice" runat="server" ImageUrl="~/imagens/icone_editar.png"
                                    OnClick="btnEditarIndice_Click" Style="width: 20px" ToolTip="Editar" OnInit="btnEditarIndice_Init" />
                                &nbsp;
                                <asp:ImageButton ID="ibtnExcluirReajuste" runat="server" ImageUrl="~/imagens/excluir.gif"
                                    OnClick="ibtnExcluirReajuste_Click" OnPreRender="ibtnExcluirReajuste_PreRender"
                                    Width="20px" ToolTip="Excluír" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Anexos:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UPanexosDiversos" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnUploadDiverso" runat="server" CssClass="ButtonUpload" Height="22px"
                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona arquivos ao Contrato')"
                                    Text="      Inserir / Visualizar Anexos" Width="170px" OnClick="btnUploadDiverso_Click" OnInit="btnUploadDiverso_Init" />
                                <asp:Label ID="lblUpload" runat="server" Font-Italic="True" Font-Overline="False"
                                    ForeColor="#999999" OnPreRender="lblSalvarContrato_PreRender"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Status:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upStatus" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="358px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnAdicionarStatus" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    Style="width: 20px" ToolTip="Novo" OnClick="btnAdicionarStatus_Click1" OnInit="btnAdicionarStatus_Init2" />
                                &nbsp;
                                <asp:ImageButton ID="btnEditarStatus" runat="server" ImageUrl="~/imagens/icone_editar.png"
                                    Style="width: 20px" ToolTip="Editar" OnClick="btnEditarStatus_Click1" OnInit="btnEditarStatus_Init1" />
                                &nbsp;
                                <asp:ImageButton ID="ibtnExcluirStatus" runat="server" ImageUrl="~/imagens/excluir.gif"
                                    ToolTip="Excluír" Width="20px" OnClick="ibtnExcluirStatus_Click" OnPreRender="ibtnExcluirStatus_PreRender" />
                                <asp:LinkButton ID="lkbHistoricos" runat="server" Font-Size="8pt" OnClick="lkbHistoricos_Click"
                                    OnInit="lkbHistoricos_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico dos registros')">Registros em Histórico</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ddlStatus" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Aditivos:
                    </td>
                    <td valign="middle">
                        <asp:UpdatePanel ID="upAditivosTela" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lkbAditivos" runat="server" Font-Size="8pt" OnClick="lkbAditivos_Click"
                                    OnInit="lkbAditivos_Init" OnPreRender="lkbAditivos_PreRender">Abrir todos Aditivos do Contrato</asp:LinkButton>
                                &nbsp;
                                <asp:Label ID="lblUpload2" runat="server" Font-Italic="True" Font-Overline="False"
                                    ForeColor="#999999" OnPreRender="lblSalvarContrato_PreRender"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        <asp:UpdatePanel ID="UPbtnProcesso" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="pnlBotoesProcessos" runat="server">
                                    <asp:Button ID="btnProcessoDNPM" runat="server" CssClass="Button" Text="Processo DNPM"
                                        OnClick="btnProcessoDNPM_Click" OnInit="btnProcessoDNPM_Init" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnProcessoAmbiental" runat="server" Text="Processo Ambiental" CssClass="Button"
                                        OnClick="btnProcessoAmbiental_Click" OnInit="btnProcessoAmbiental_Init" />
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Data de Vencimento:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="tbxVencimento" runat="server" CssClass="TextBox" Width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbxVencimento_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="tbxVencimento">
                                </asp:CalendarExtender>
                                <br />
                                <asp:LinkButton ID="lkbProrrogacao" runat="server" Font-Size="8pt" OnClick="lkbProrrogacao_Click"
                                    OnInit="lkbProrrogacao_Init" OnPreRender="lkbProrrogacao_PreRender">Abrir Prorrogações</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%" colspan="2">
                        <div id="content_conteudos" style="text-align: center !important;">
                            <asp:UpdatePanel ID="UPNotificacoes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="text-align: left; margin-bottom: 8px;">
                                        <strong><span class="style1">Notificações do Vencimento:</span></strong>
                                        <asp:ImageButton ID="ibtnAddNotificacoes" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClick="ibtnAddNotificacoes_Click" OnInit="ibtnAddNotificacoes_Init" />
                                        <br />
                                        <asp:Label ID="lblUpload0" runat="server" Font-Italic="True" Font-Overline="False"
                                            ForeColor="#999999" OnPreRender="lblSalvarContrato_PreRender"></asp:Label>
                                    </div>
                                    <div class="container_grids" style="text-align: center !important;">
                                        <asp:GridView ID="grvNotificacoes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" OnRowDeleting="grvNotificacoes_RowDeleting" PageSize="4" Width="100%"
                                            OnPageIndexChanging="grvNotificacoes_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoes" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir5" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')"
                                                            OnPreRender="ibtnExcluir5_PreRender" />
                                                        <input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoes')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="False" ForeColor="White"
                                                HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                        Data de Reajuste
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="tbxVencimentoReajuste" runat="server" CssClass="TextBox" Width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbxVencimentoReajuste_CalendarExtender" runat="server"
                                    Format="dd/MM/yyyy" TargetControlID="tbxVencimentoReajuste">
                                </asp:CalendarExtender>
                                <br />
                                <asp:LinkButton ID="lkbProrrogacaoReajuste" runat="server" Font-Size="8pt" OnClick="lkbProrrogacaoReajuste_Click"
                                    OnInit="lkbProrrogacaoReajuste_Init" OnPreRender="lkbProrrogacaoReajuste_PreRender">Abrir Prorrogações</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%" colspan="2">
                        <div id="content_conteudos0" style="text-align: center !important;">
                            <asp:UpdatePanel ID="UPNotificacoesReajuste" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="text-align: left; margin-bottom: 8px;">
                                        <strong><span class="style1">Notificações do Reajuste:</span></strong>
                                        <asp:ImageButton ID="ibtnAddNotificacoesReajuste" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClick="ibtnAddNotificacoesReajuste_Click" OnInit="ibtnAddNotificacoes_Init" />
                                        <br />
                                        <asp:Label ID="lblUpload1" runat="server" Font-Italic="True" Font-Overline="False"
                                            ForeColor="#999999" OnPreRender="lblSalvarContrato_PreRender"></asp:Label>
                                    </div>
                                    <div class="container_grids" style="text-align: center !important;">
                                        <asp:GridView ID="grvNotificacoesReajuste" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" OnRowDeleting="grvNotificacoesReajuste_RowDeleting" PageSize="4"
                                            Width="100%" OnPageIndexChanging="grvNotificacoesReajuste_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesReajuste" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir6" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')"
                                                            OnPreRender="ibtnExcluir5_PreRender" />
                                                        <input id="ckbSelecionar2" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesReajuste')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="False" ForeColor="White"
                                                HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                    </td>
                    <td>
                        <asp:Label ID="lblPopCentroCusto" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalPopCentroCusto" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fechar_cadastro_status" PopupControlID="cadastro_status_diverso"
                            TargetControlID="lblPopCentroCusto">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblAditivos" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalPopupAditivos" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fechar_aditivos" PopupControlID="pop_aditivos" TargetControlID="lblAditivos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblPopStatus" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalPopupStatus" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fechar_status" PopupControlID="pop_status" TargetControlID="lblPopStatus">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblFormaRecebimento" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalPopFormarecebimento" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="cancelarRecebimento" PopupControlID="popRecebimento" TargetControlID="lblFormaRecebimento">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblPopUpSetor" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopUpSetorExtender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharPopSetor" PopupControlID="popSetor" TargetControlID="lblPopUpSetor">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblPopUpIndice" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopUpIndiceExtender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharPopIndice" PopupControlID="popIndice" TargetControlID="lblPopUpIndice">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblPopNotificacoes" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalExtenderNotificoes" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharCadastroNotificacao" PopupControlID="divPopUpCadastroNotificacao"
                            TargetControlID="lblPopNotificacoes">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblPopupAlteracaoVencimentos" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblAlteracaoStatus_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharAlteracaoStatus" PopupControlID="divPopUpAlteracaoStatus"
                            TargetControlID="lblPopupAlteracaoVencimentos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblExtenderVerHistoricos" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalExtenderHistoricos" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharHistoricos" PopupControlID="divPopHistoricos" TargetControlID="lblExtenderVerHistoricos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblEnvioHistorico" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblEnvioHistorico_popupextender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharEnviarEmailHistorico" PopupControlID="divPopUpEnviarEmailHistorico"
                            TargetControlID="lblEnvioHistorico">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblAdicionarContrato" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblAdicionarContrato_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fechar_processos" PopupControlID="divPopUpProcessos"
                            TargetControlID="lblAdicionarContrato">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblSelecionarContrato" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblSelecionarContrato_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fechar_processo_selecao" PopupControlID="divPopUpProcessoSelecao"
                            TargetControlID="lblSelecionarContrato">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblAdicionarProcessoAmbiental" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="lblAdicionarProcessoAmbiental_ModalPopupExtender" runat="server"
                            DynamicServicePath="" Enabled="True" BackgroundCssClass="simplemodal" PopupControlID="divPopUpAmbiental"
                            TargetControlID="lblAdicionarProcessoAmbiental" CancelControlID="fechar_ambiental">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblSelecionarProcessoAmbiental" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="lblSelecionarProcessoAmbiental_ModalPopupExtender" runat="server"
                            DynamicServicePath="" Enabled="True" BackgroundCssClass="simplemodal" PopupControlID="divPopUpAmbientalSelecao"
                            TargetControlID="lblSelecionarProcessoAmbiental" CancelControlID="fechar_ambiental_selecao">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblUploadArquivos" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="lblUploadArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_arquivos_teste" 
                            DynamicServicePath="" Enabled="True" PopupControlID="arquivos_teste" TargetControlID="lblUploadArquivos">
                        </asp:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="35%">
                    </td>
                    <td>
                        <asp:Button ID="btnSalvar" CssClass="Button" runat="server" Text="Salvar" OnClick="Button1_Click"
                            ValidationGroup="rfvSalvarDiverso" OnInit="Button1_Init" />&nbsp;
                        <asp:Button ID="btnNovo" runat="server" CssClass="Button" Text="Novo" OnClick="btnNovo_Click" />&nbsp;
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="ButtonExcluir"
                            OnClick="btnExcluir_Click" OnPreRender="btnExcluir_PreRender" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="popups" style="width: 100%">
        <div id="popRecebimento" class="pop_up" style="width: 500px">
            <div id="cancelarRecebimento" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Formas de Recebimento/Pagamento</div>
            <div id="Div5">
                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnSalvarRecebimento">
                    <asp:UpdatePanel ID="upPopRecebimento" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%" cellspacing="5">
                                <tr>
                                    <td align="right" width="20%">
                                        Forma*:<br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxForma" runat="server" Width="200px" TextMode="SingleLine" CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="vgForma" ControlToValidate="tbxForma" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnSalvarRecebimento" runat="server" CssClass="Button" Text="Salvar"
                                            ValidationGroup="vgForma" OnClick="btnSalvarRecebimento_Click" OnInit="btnSalvarRecebimento_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
        <div id="pop_status" class="pop_up" style="width: 500px">
            <div id="fechar_status" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Status</div>
            <div id="Div6">
                <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSalvarRecebimento">
                    <asp:UpdatePanel ID="upPopStatusCriados" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%" cellspacing="5">
                                <tr>
                                    <td align="right" width="20%">
                                        Status*:<br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxStatus" runat="server" Width="200px" TextMode="SingleLine" CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="vgStatus" ControlToValidate="tbxStatus" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnSalvarStatus" runat="server" CssClass="Button" Text="Salvar" ValidationGroup="vgStatus"
                                            OnClick="btnSalvarStatus_Click" OnInit="btnSalvarStatus_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
        <div id="cadastro_status_diverso" class="pop_up" style="width: 500px">
            <div id="fechar_cadastro_status" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Centro de Custo
            </div>
            <div id="Div3">
                <asp:Panel ID="pnlPopCusto" runat="server" DefaultButton="btnSavarCentroCusto">
                    <asp:UpdatePanel ID="UPPopCusto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%" cellspacing="5">
                                <tr>
                                    <td align="right" width="20%">
                                        Nome*:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxCentroCusto" runat="server" Width="200px" TextMode="SingleLine"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvarStatus" ControlToValidate="tbxCentroCusto" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnSavarCentroCusto" runat="server" CssClass="Button" OnClick="btnSavarCentroCusto_Click"
                                            OnInit="btnSavarCentroCusto_Init" Text="Salvar" ValidationGroup="rfvSalvarStatus" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
        <div id="popSetor" class="pop_up" style="width: 500px">
            <div id="fecharPopSetor" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Setor
            </div>
            <div>
                <asp:Panel ID="pnlPopSetor" runat="server" DefaultButton="btnSalvarSetor">
                    <asp:UpdatePanel ID="UPPopSetor" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%" cellspacing="5">
                                <tr>
                                    <td align="right" width="20%">
                                        Nome*:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxSetor" runat="server" Width="200px" TextMode="SingleLine" CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvarSetor" ControlToValidate="tbxSetor" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnSalvarSetor" runat="server" CssClass="Button" OnClick="btnSalvarSetor_Click"
                                            Text="Salvar" ValidationGroup="rfvSalvarSetor" OnInit="btnSalvarSetor_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
        <div id="popIndice" class="pop_up" style="width: 500px">
            <div id="fecharPopIndice" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Índice Financeiro</div>
            <div>
                <asp:Panel ID="pnlPopIndiceFin" runat="server" DefaultButton="btnSalvarIndice">
                    <asp:UpdatePanel ID="UPPopIndice" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%" cellspacing="5">
                                <tr>
                                    <td align="right" width="20%">
                                        Nome*:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxIndice" runat="server" Width="200px" TextMode="SingleLine" CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvarIndice" ControlToValidate="tbxIndice" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;<asp:Button ID="btnSalvarIndice" runat="server" CssClass="Button" Text="Salvar"
                                            ValidationGroup="rfvSalvarIndice" OnClick="btnSalvarIndice_Click" OnInit="btnSalvarIndice_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
        <div id="divPopUpCadastroNotificacao" style="width: 805px; display: block; top: 0px;
            left: 0px;" class="pop_up_super_super">
            <div>
                <div id="fecharCadastroNotificacao" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Cadastro de Notificações</div>
                <div>
                    <asp:UpdatePanel ID="upPopNotificacoes" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 25%">
                                        Dias de Aviso:
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="cblDias" runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>Selecione os emails para envio:</strong>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <hr style="height: -12px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" width="33%">
                                                    <input id="chkMarcarEmailsGrupo" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                    <strong>Grupo Econômico: </strong>
                                                </td>
                                                <td align="left" width="33%" valign="top">
                                                    <input id="chkMarcarEmailsEmpresa" type="checkbox" class="chkMarcarEmailsEmpresa"
                                                        checked="checked" />&nbsp; <strong>Empresa:</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                        border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                        -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                        box-shadow: 0 1px 1px #DDD;">
                                                        <asp:CheckBoxList ID="chkGruposEconomicos" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td align="left">
                                                    <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                        border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                        -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                        box-shadow: 0 1px 1px #DDD;">
                                                        <asp:CheckBoxList ID="chkEmpresas" runat="server" CssClass="chkEmailsEmpresasCont">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <hr style="height: -12px" />
                                        <strong>Outros (separar com ;)</strong>
                                        <asp:TextBox ID="tbxOutrosEmails" runat="server" CssClass="TextBox" Height="56px"
                                            TextMode="MultiLine" Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:HiddenField ID="hfTipo" runat="server" />
                                        <asp:HiddenField ID="hfIdNotificacao" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnSalvarNotificacao" runat="server" CssClass="Button" Text="Salvar"
                                            Width="170px" OnClick="btnSalvarNotificacao_Click" OnInit="btnSalvarNotificacao_Init"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divPopUpAlteracaoStatus" style="width: 805px; display: block; top: 0px;
            left: 0px;" class="pop_up_super_super_super">
            <div>
                <div id="fecharAlteracaoStatus" class="btn_cancelar_popup" style="display: none">
                </div>
                <div class="barra_titulo">
                    Alteração de Histórico
                </div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="upPopStatus" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td align="left">
                                        <strong>Você alterou os seguintes Vencimento - Caso deseja enviar um email com esta
                                            alteração, preencha os campos abaixo e clique em Enviar/Salvar: </strong>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" class="TextBox">
                                        <asp:Label ID="lblAlteracao" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>Selecione os emails que receberão uma notificação com esta alteração:</strong>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" width="33%">
                                                    <input id="Checkbox7" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                    <strong>Grupo Econômico: </strong>
                                                </td>
                                                <td align="left" width="33%" valign="top">
                                                    <input id="Checkbox10" type="checkbox" class="chkMarcarEmailsEmpresa" checked="checked" />&nbsp;
                                                    <strong>Empresa:</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                        border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                        -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                        box-shadow: 0 1px 1px #DDD;">
                                                        <asp:CheckBoxList ID="ckbGrupos" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td align="left">
                                                    <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                        border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                        -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                        box-shadow: 0 1px 1px #DDD;">
                                                        <asp:CheckBoxList ID="ckbEmpresasAlteracao" runat="server" CssClass="chkEmailsEmpresasCont">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <hr style="height: -12px" />
                                        <strong>Registros em Histórico</strong> &nbsp;<asp:TextBox ID="tbxHistoricoAlteracao"
                                            runat="server" CssClass="TextBox" Height="56px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:CheckBox ID="chkEnviarEmail" runat="server" Checked="True" Text="Enviar e-mail com alteração de status" />
                                        <asp:HiddenField ID="hfIdAlteracao" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnAlterarStatus" runat="server" CssClass="Button" OnClick="btnAlterarStatus_Click"
                                            OnInit="btnAlterarStatus_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')"
                                            Text="Enviar/Salvar" Width="170px" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divPopHistoricos" style="width: 866px; display: block; top: 0px; left: 0px;"
            class="pop_up_super_super">
            <div>
                <div id="fecharHistoricos" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Registros em Históricos</div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="UPHistoricos" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" width="20%">
                                        Título:
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxTituloObs" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="20%">
                                        Descrição:
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxObservacaoObs" runat="server" CssClass="TextBox" Width="90%"
                                            Height="45px" TextMode="MultiLine"></asp:TextBox>
                                        <strong>
                                            <asp:ImageButton ID="ibtnAddObs" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                OnClick="ibtnAddObs_Click" />
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>Histórico:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="container_grids" style="height: 200px; overflow: auto;">
                                            <asp:GridView ID="grvHistoricos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                PageSize="3" Width="100%">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Data do Registro">
                                                        <ItemTemplate>
                                                            <%# bindDataRegistro(Container.DataItem) %></ItemTemplate>
                                                        <ItemStyle Width="105px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Alteracao" HeaderText="Título">
                                                        <ItemStyle Width="435px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Observacao" HeaderText="Descrição">
                                                        <ItemStyle Width="300px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnEnviarHistorico" runat="server" CssClass="Button" OnClick="btnEnviarHistorico_Click"
                                            OnInit="btnEnviarHistorico_Init" Text="Enviar Histórico Por Email" Width="220px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ibtnAddObs" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divPopUpEnviarEmailHistorico" style="width: 866px; display: block; top: 0px;
            left: 0px;" class="pop_up_super_super">
            <div>
                <div id="fecharEnviarEmailHistorico" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Enviar Histórico</div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="upEnvioHistorico" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>Os registros históricos serão enviados para o(s) e-mail(s) selecionados abaixo:
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>Selecione os emails para envio:</strong>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <hr style="height: -12px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" width="33%">
                                                    <input id="Checkbox12" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                    <strong>Grupo Econômico: </strong>
                                                </td>
                                                <td align="left" width="33%" valign="top">
                                                    <input id="Checkbox13" type="checkbox" class="chkMarcarEmailsEmpresa" checked="checked" />&nbsp;
                                                    <strong>Empresa:</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                        border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                        -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                        box-shadow: 0 1px 1px #DDD;">
                                                        <asp:CheckBoxList ID="chkGruposHistorico" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                                <td align="left">
                                                    <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                        border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                        -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                        box-shadow: 0 1px 1px #DDD;">
                                                        <asp:CheckBoxList ID="chkEmpresaHistorico" runat="server" CssClass="chkEmailsEmpresasCont">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <hr style="height: -12px" />
                                        <strong>Outros (separar com ;)</strong>
                                        <asp:TextBox ID="tbxEmailsHistorico" runat="server" CssClass="TextBox" Height="56px"
                                            TextMode="MultiLine" Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnEnviar" runat="server" CssClass="Button" Text="Enviar" Width="170px"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Envia o histórico para os e-mails selecionados')"
                                            OnClick="btnEnviar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="pop_aditivos" style="width: 866px; display: block; top: 0px; left: 0px;"
            class="pop_up_super_super">
            <div>
                <div id="fechar_aditivos" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Aditivos</div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="upAditivos" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="Panel6" runat="server" DefaultButton="btnSalvarAditivo">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right" width="20%" style="width: 80%">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="right" style="width: 35%">
                                                        Número do Aditivo:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbxNumeroAditivo" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 30%">
                                                        Data de assinatura:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox runat="server" CssClass="TextBox" Width="100px" ID="tbxDataAssinaturaAditivo"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="tbxDataAssinaturaAditivo">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 30%">
                                                        Motivo:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbxMotivoAditivo" runat="server" CssClass="TextBox" Height="50px"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Número do Aditamento')"
                                                            TextMode="MultiLine" Width="303px"></asp:TextBox>
                                                        <strong>
                                                            <asp:Button ID="btnSalvarAditivo" runat="server" CssClass="Button" Text="Salvar Aditivo"
                                                                OnClick="btnSalvarAditivo_Click" OnInit="btnSalvarAditivo_Init" />
                                                        </strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 30%">
                                                        TEVE PRORROGAÇÃO DE PRAZO NAS DATAS:
                                                    </td>
                                                    <td align="left" valign="middle">
                                                        <asp:CheckBox ID="ckbAlteracaoVencimento" runat="server" AutoPostBack="True" Text="do Vencimento"
                                                            OnCheckedChanged="ckbAlteracaoVencimento_CheckedChanged" OnInit="ckbAlteracaoVencimento_Init" />
                                                        &nbsp;
                                                        <asp:CheckBox ID="ckbAlteracaoReajuste" runat="server" AutoPostBack="True" Text="do Reajuste"
                                                            OnCheckedChanged="ckbAlteracaoReajuste_CheckedChanged" OnInit="ckbAlteracaoReajuste_Init" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2">
                                                        <asp:UpdatePanel ID="upAlteracaoVencimento" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnlProrrogacaoVencimento" runat="server" Visible="False">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" width="35%">
                                                                                Nova data de Vencimento:
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="tbxDataProrrogacaoVencimento" runat="server" CssClass="TextBox"
                                                                                    Width="100px"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="tbxDataProrrogacaoVencimento_CalendarExtender" runat="server"
                                                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataProrrogacaoVencimento">
                                                                                </asp:CalendarExtender>
                                                                                <asp:Label ID="lblVencimentoAtual" runat="server" Font-Italic="True" ForeColor="Silver"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2">
                                                        <asp:UpdatePanel ID="upAlteracaoReajuste" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnlProrrogacaoReajuste" runat="server" Visible="False">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" width="35%">
                                                                                Nova data de Reajuste:
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="tbxDataProrrogacaoReajuste" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="tbxDataProrrogacaoReajuste_CalendarExtender" runat="server"
                                                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataProrrogacaoReajuste">
                                                                                </asp:CalendarExtender>
                                                                                <asp:Label ID="lblReajusteAtual" runat="server" Font-Italic="True" ForeColor="Silver"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <div class="container_grids" style="height: 180px; overflow: auto;">
                                                <asp:GridView ID="gdvAditivos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                    PageSize="3" Width="100%" OnRowDeleting="gdvAditivos_RowDeleting" OnInit="gdvAditivos_Init" OnRowEditing="gdvAditivos_RowEditing">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                        <asp:BoundField DataField="DataAssinatura" HeaderText="Data de Assinatura" DataFormatString="{0:d}" />
                                                        <asp:BoundField DataField="Motivo" HeaderText="Motivo" />
                                                        <asp:TemplateField HeaderText="Prorrogou o Vencimento">
                                                            <ItemTemplate>
                                                                <%# BindProrrogouVencimento(Container.DataItem) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prorrogou o Reajuste">
                                                            <ItemTemplate>
                                                                <%# BindProrrogouReajuste(Container.DataItem) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Anexos">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibtnAbrirDownload" runat="server" ImageUrl="~/imagens/icone_anexo.png" CommandName="Edit"
                                                                    ToolTip="Ver Arquivos" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="excluir">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarProrrogacaoPrazo" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:ImageButton ID="ibtnExcluir6" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                    OnInit="ibtnExcluir6_Init" OnPreRender="ibtnExcluir6_PreRender1" ToolTip="Excluir" />
                                                                <input id="Checkbox15" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarProrrogacaoPrazo')" />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="45px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#7C6F57" />
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divPopUpProcessos" style="width: 700px; display: block; top: 0px; left: 0px;"
            class="pop_up_super_super">
            <div>
                <div id="fechar_processos" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Processos DNPM</div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="UPListagemProcessoDNPM" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="container_grids">
                                            <asp:GridView ID="gdvContratos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                GridLines="None" OnRowDeleting="gdvContratos_RowDeleting" PageSize="4" Width="100%"
                                                OnPageIndexChanging="gdvContratos_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                    <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                    <asp:TemplateField HeaderText="Objeto">
                                                        <ItemTemplate>
                                                            <%# bindingObjeto(Container.DataItem) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <%# bindingStatusContrato(Container.DataItem) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DataAbertura" DataFormatString="{0:d}" HeaderText="Data de Abertura">
                                                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkDeletarContratos" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:ImageButton ID="ibtnExcluirProcesso" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                OnPreRender="ibtnExcluirProcesso_PreRender" ToolTip="Excluir" />
                                                            <input id="ckbSelecionar5" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkDeletarContratos')" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle Width="45px" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <div style="float: left">
                                            <asp:Label ID="lblQuantidadeProcessosDNPM" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:Button ID="btnSelecionarMaisContratos" runat="server" CssClass="Button" OnClick="btnSelecionarMaisContratos_Click"
                                            Text="Selecionar Processos" Width="170px" OnInit="btnSelecionarMaisContratos_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divPopUpProcessoSelecao" style="width: 700px; display: block; top: 0px;
            left: 0px;" class="pop_up_super_super">
            <div id="fechar_processo_selecao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Seleção de Processos DNPM</div>
            <div>
                <asp:UpdatePanel ID="UPSelecaoContratos" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="35%">
                                    Substância:
                                </td>
                                <td width="65%">
                                    <asp:TextBox ID="tbxSubstancia" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="35%">
                                    Número:
                                </td>
                                <td width="65%">
                                    <asp:TextBox ID="tbxNumeroDNPM" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnPesquisarDNPM" runat="server" OnClick="btnPesquisarDNPM_Click"><img alt="" src="../imagens/visualizar20x20.png" style="border:0px"/> Pesquisar</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="35%">
                                    &nbsp;
                                </td>
                                <td width="65%" align="right">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="gdvContratosSelecao" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" PageSize="4" Width="100%" OnPageIndexChanging="gdvContratosSelecao_PageIndexChanging"
                                            OnInit="gdvContratosSelecao_Init">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarContratosDiversos" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        Sel.<input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarContratosDiversos')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                <asp:TemplateField HeaderText="Substância">
                                                    <ItemTemplate>
                                                        <%# bindingObjeto(Container.DataItem) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identificação">
                                                    <ItemTemplate>
                                                        <%# bindingStatusContrato(Container.DataItem) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DataAbertura" DataFormatString="{0:d}" HeaderText="Data de Abertura">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarContratoDNPM" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarContratoDNPM_Click" OnInit="btnSalvarContratoDNPM_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnPesquisarDNPM" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="divPopUpAmbiental" style="width: 700px; display: block; top: 0px; left: 0px;"
            class="pop_up_super_super">
            <div>
                <div id="fechar_ambiental" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Processos Ambiental</div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="UPProcessosAmbientais" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="container_grids">
                                            <asp:GridView ID="gdvProcessosAmbientais" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                GridLines="None" OnRowDeleting="gdvProcessosAmbientais_RowDeleting" PageSize="4"
                                                Width="100%" OnPageIndexChanging="gdvProcessosAmbientais_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                    <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                    <asp:TemplateField HeaderText="Empresa">
                                                        <ItemTemplate>
                                                            <%# bindingEmpresa(Container.DataItem)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Consultora">
                                                        <ItemTemplate>
                                                            <%# bindingConsultora(Container.DataItem)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DataAbertura" DataFormatString="{0:d}" HeaderText="Data de Abertura">
                                                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkDeletarContratos" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>"/>
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:ImageButton ID="ibtnExcluirProcessoAmbiental" runat="server" CommandName="Delete"
                                                                ImageUrl="~/imagens/excluir.gif" OnPreRender="ibtnExcluirProcessoAmbiental_PreRender"
                                                                ToolTip="Excluir" />
                                                            <input id="ckbSelecionar5" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkDeletarContratos')" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle Width="45px" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <div style="float: left">
                                            <asp:Label ID="lblQuantidadeProcessosAmbiental" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:Button ID="btnSelecionarMaisProcessos" runat="server" CssClass="Button" OnClick="btnSelecionarMaisProcessos_Click"
                                            Text="Selecionar Processos" Width="170px" OnInit="btnSelecionarMaisProcessos_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divPopUpAmbientalSelecao" style="width: 700px; display: block; top: 0px;
            left: 0px;" class="pop_up_super_super">
            <div id="fechar_ambiental_selecao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Seleção de Processos Ambientais</div>
            <div>
                <asp:UpdatePanel ID="UPProcessosSelecaoAmbiental" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:RadioButtonList ID="rblTipoProcesso" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="4" CellSpacing="4" Font-Bold="False" Font-Names="Arial" Font-Size="Small">
                                        <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                                        <asp:ListItem Value="1">Municipais</asp:ListItem>
                                        <asp:ListItem Value="2">Estaduais</asp:ListItem>
                                        <asp:ListItem Value="3">Federais</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <table style="width: 89%">
                                        <tr>
                                            <td align="right">
                                                Numero:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbxNumeroProcessoAmbiental" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Empresa:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlEmpresasConsulta" CssClass="DropDownList" Width="200px"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnPesquisarProcessosAmbientais" OnClick="btnPesquisarProcessosAmbientais_Click"
                                                    runat="server">
                                                <img alt="" src="../imagens/visualizar20x20.png" style="border:0px"/> Pesquisar</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="35%">
                                    &nbsp;
                                </td>
                                <td width="65%" align="right">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="gdvProcessosAmbientaisSelecao" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" PageSize="4" Width="100%" OnPageIndexChanging="gdvProcessosAmbientaisSelecao_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarContratosDiversos" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        Sel.<input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarContratosDiversos')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                <asp:TemplateField HeaderText="Empresa">
                                                    <ItemTemplate>
                                                        <%# bindingEmpresa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Consultora">
                                                    <ItemTemplate>
                                                        <%# bindingConsultora(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DataAbertura" DataFormatString="{0:d}" HeaderText="Data de Abertura">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarProcessosAmbientais" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarProcessosAmbientais_Click" OnInit="btnSalvarProcessosAmbientais_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnPesquisarProcessosAmbientais" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="arquivos_teste" style="width: 550px; height:440px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
        <div id="fechar_arquivos_teste" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Arquivos</div>
        <div>
            <asp:UpdatePanel ID="UPFrameArquivos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <iframe runat="server" name="conteudo" width="545px" height="435px" marginwidth="0" marginheight="0" scrolling="no" frameborder="0" id="conteudo" ></iframe> 
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
</asp:Content>
