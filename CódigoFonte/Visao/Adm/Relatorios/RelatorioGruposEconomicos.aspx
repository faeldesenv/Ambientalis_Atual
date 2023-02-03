<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioGruposEconomicos.aspx.cs" Inherits="Adm_Relatorios_RelatorioGruposEconomicos" %>

<%@ Register Src="~/Adm/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataCadastroRelatorioGruposEconomicos.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataCadastroAtehRelatorioGruposEconomicos.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Grupos Econômicos
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
                <asp:DropDownList ID="ddlSistema" runat="server" AutoPostBack="True" CssClass="DropDownList" onselectedindexchanged="ddlSistema_SelectedIndexChanged" ValidationGroup="rfv" Width="50%">
                    <asp:ListItem Selected="True" Value="0">Sustentar</asp:ListItem>
                    <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Administrador:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlAdministradorGruposEconomicos" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSistema" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Cadastro de:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataCadastroRelatorioGruposEconomicos" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxDataCadastroAtehRelatorioGruposEconomicos" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
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
        <tr>
            <td align="right">
                Ativo:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlAtivo" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                    <asp:ListItem Value="2">Não</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Cancelado:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlCancelado" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                    <asp:ListItem Value="2">Não</asp:ListItem>
                </asp:DropDownList>
            </td>
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
            <asp:BoundField DataField="GetAdministrador" HeaderText="Administrador" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="DataCadastro" HeaderText="Assinatura do Contrato" DataFormatString="{0:d}" />
            <asp:BoundField DataField="GetQuantidadeEmpresas" HeaderText="Empresas" />            
            <asp:TemplateField HeaderText="Usuarios">
                <ItemTemplate>
                    <%# BindQuantidadeUsuarios(Container.DataItem)%>
                </ItemTemplate>                
            </asp:TemplateField>                    
            <asp:BoundField DataField="GetDataCancelamento" HeaderText="Cancelamento" />                     
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

