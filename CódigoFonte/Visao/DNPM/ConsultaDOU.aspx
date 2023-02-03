<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="ConsultaDOU.aspx.cs" Inherits="Site_ConsultaDNPM" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: 8pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="20%">
                        Grupo Econômico:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList" Height="25px"
                            Width="90%" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True"
                            DataTextField="Nome" DataValueField="Id" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Selecione o grupo econômico do qual deseja pesquisar os processos no Diário Oficial da União')" >
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros" width="15%" rowspan="4">
                        Processos ANM:
                    </td>
                    <td width="25%" rowspan="4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:ListBox ID="lbxProcessos" runat="server" CssClass="TextBox" Height="152px" 
                                    SelectionMode="Multiple" Width="95%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para selecionar mais de um processo, utilize as teclas ctrl para seleção individual ou shift para seleção em grupo, além de clicar no processo desejado')" ></asp:ListBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbxEmpresas" 
                                    EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td rowspan="5" width="10%" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Consultar" OnClientClick="tooltip.hide();"
                            OnClick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Consulta a ocorrência do(s) processo(s) selecionado(s), utilizando a consulta disponível no site da Imprensa Oficial')"  />
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Empresas:</td>
                    <td class="controlFiltros">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:ListBox ID="lbxEmpresas" runat="server" AutoPostBack="True" 
                                    CssClass="TextBox" onselectedindexchanged="lbxEmpresas_SelectedIndexChanged" 
                                    SelectionMode="Multiple" Width="90%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para selecionar mais de uma empresa, utilize as teclas ctrl para seleção individual ou shift para seleção em grupo, além de clicar na empresa desejada')" ></asp:ListBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Data da Publicação de:</td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxDiaDe" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox>
&nbsp;/
                        <asp:TextBox ID="tbxMesDe" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Até:</td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxDiaAte" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox>
&nbsp;/
                        <asp:TextBox ID="tbxMesAte" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox>
                        &nbsp; de&nbsp;
                        <asp:DropDownList ID="ddlAnosDe" runat="server" CssClass="TextBox">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div align="center" 
                style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-size: small; margin-top: 5px; margin-bottom: 5px;">
                <strong>ATENÇÃO:</strong> Os resultados das consultas são gerados pelo site da Imprensa Nacional 
                não sendo de responsabilidade do Sistema SUSTENTAR.<br />
                <span class="style1">* Por limitações no site do DOU, as consultas são 
                realizadas apenas pelos primeiros números do processo, podendo consultar 
                processos de outros anos que não são da empresa selecionada</span></div>
        </div>
        <div id="grid">
            <asp:UpdatePanel ID="UpdateGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgr" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" GridLines="None" Width="100%" ForeColor="#333333">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager"/>
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Processo" HeaderText="Processo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Empresa" HeaderText="Empresa"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Ocorrencia" HeaderText="Ocorrência">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DOU">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Link") %>' runat="server" Target="_blank" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza as ocorrências do processo ecnontradas no site da Imprensa Nacional')" >Visualizar</asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Width="22px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
   Consulta de Processos ANM no Diário Oficial
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
</asp:Content>
