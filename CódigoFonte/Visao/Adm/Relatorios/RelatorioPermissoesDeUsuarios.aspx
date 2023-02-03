<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioPermissoesDeUsuarios.aspx.cs" Inherits="Adm_Relatorios_RelatorioPermissoesDeUsuarios" %>

<%@ Register Src="~/Adm/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Permissões por usuários
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
                Grupo Econômico:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="upGruposEconomico" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoEconomico_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSistema" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Usuário:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="upUsuarios" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSistema" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
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
            <asp:TemplateField HeaderText="Permissões">
                <ItemTemplate>
                    <br />
                    <strong style="font-size:12px;">Usuário:</strong> <asp:Label ID="Label1" runat="server" Text='<%# BindNome(Container.DataItem)%>' style="font-size:12px;"></asp:Label><br /><br />
                    <strong style="font-size:12px;">Permissões:</strong><br />  
                    <asp:Label ID="Label2" runat="server" Text='<%# BindPermissoesUsuario(Container.DataItem)%>'></asp:Label>      
                </ItemTemplate>                
            </asp:TemplateField>                                                    
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

