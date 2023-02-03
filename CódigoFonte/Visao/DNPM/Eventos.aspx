<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="Eventos.aspx.cs" Inherits="DNPM_Eventos" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 28%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Eventos DNPM</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="20%">
                        Filtro por
                        Grupo Econômico:
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList" Height="25px"
                            Width="90%" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True"
                            DataTextField="Nome" DataValueField="Id">
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros" width="15%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        Filtro por
                        Empresa:
                    </td>
                    <td class="style1">
                        <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList"
                                    DataTextField="Nome" DataValueField="Id" Height="25px"
                                    Width="90%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="labelFiltros" width="15%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        Processo DNPM:</td>
                    <td class="style1">
                        <asp:Label ID="lblProcessoDNPM" runat="server" Text="TODOS"></asp:Label>
                    </td>
                    <td class="labelFiltros" width="15%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="labelFiltros" width="20%">
                        &nbsp;
                    </td>
                    <td class="style1">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" 
                            style="float:left;" Text="Exibir Eventos Já Baixados do Site"
                            OnClick="btnPesquisar_Click" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnBaixarAtualizacoes" runat="server" CssClass="Button" 
                                    style="float:left;margin-left:10px;"  OnClick="btnBaixarAtualizacoes_Click"
                                    Text="Baixar Atualizações no Site do DNPM" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="labelFiltros" width="15%" align="right">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSalvarTodos" runat="server" CssClass="Button" 
                                    OnClick="btnSalvarTodos_Click" Text="Salvar Alterações" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div align="center" 
                style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-size: small; margin-top: 5px; margin-bottom: 5px;">
                <strong>ATENÇÃO:</strong> Os Eventos baixados do Site do DNPM são para <strong>
                CONSULTA</strong>, cabendo ao usuário inseri-los <strong>MANUALMENTE</strong> 
                nos processos.</div>
        </div>
        <div id="grid">
            <asp:UpdatePanel ID="upGridEventos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="rptItens" runat="server">
                        <ItemTemplate>
                        <div style="font-family:Arial; padding:8px 3px; background-color:#0C696A; color:White;  font-size:12px; font-weight:bold;">
                            <asp:Label runat="server" Text='<%# Eval("Numero") %>'></asp:Label>
                            <asp:Label runat="server" Text='<%# BindEmpresaGE(Container.DataItem) %>'></asp:Label></div>
                            <asp:DataGrid ID="dgrEventos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                DataSource='<%# BindEventos(Container.DataItem) %>' DataKeyField="Id" ForeColor="#333333"
                                GridLines="None" Width="100%"  OnItemDataBound="dgrEventos_ItemCreated">
                                <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small" ForeColor="White"
                                    HorizontalAlign="Center" Mode="NumericPages" NextPageText="" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <Columns>
                                 <asp:TemplateColumn HeaderText="Evento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEvento" runat="server" ForeColor='<%# BindColor(Container.DataItem) %>' Font-Bold='<%# Eval("Atualizado") %>' Text='<%# Eval("Descricao") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="Data" DataFormatString="{0:d/M/yyyy}" HeaderText="Data">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Atualizado">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAtualizado" runat="server" Checked='<%# Eval("Atualizado") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Irrelevante">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIrrelevante" runat="server" Checked='<%# Eval("Irrelevante") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <EditItemStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" CssClass="GridHeaderEventosDNPM" Font-Bold="True" ForeColor="White"
                                    HorizontalAlign="Left" VerticalAlign="Top" />
                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            </asp:DataGrid>
                            <div style="text-align: center">
                                <asp:Label ID="lblResultado" runat="server" Text='<%# BindResultado(Container.DataItem) %>'></asp:Label>
                            </div>
                            <asp:Button ID="btnAtualizacaoProcesso" runat="server" CommandArgument='<%# Eval("Id") %>'
                                CssClass="ButtonAtualizarProcesso" OnClick="btnAtualizacaoProcesso_Click" OnInit="btnAtualizacaoProcesso_Init"
                                Text="Baixar Eventos" />
                            <br />
                            <div style="width:100%; height:25px; margin-top:25px; border-top:1px solid #e3eaeb;"></div>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                    <asp:ModalPopupExtender ID="LinkButton1_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
                        DynamicServicePath="" Enabled="True" PopupControlID="divPopUpAtualizacao" TargetControlID="LinkButton1">
                    </asp:ModalPopupExtender>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnBaixarAtualizacoes" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div style="text-align: right">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="divPopUpAtualizacao" style="width: 596px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div class="barra_titulo">
                Processando...</div>
        </div>
        <div>
            <asp:UpdatePanel ID="upAtualizar" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <asp:Label ID="lblProcesso" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <br />
                    Aguarde, o processo pode levar alguns minutos.<br />
                    <div style="text-align:right;">
                    <asp:Button ID="btnParar" CssClass="Button" runat="server" Text="Parar" OnClick="btnParar_Click" OnInit="btnParar_Init"
                        Height="26px" /></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>
