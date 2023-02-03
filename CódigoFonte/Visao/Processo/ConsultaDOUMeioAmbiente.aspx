<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="ConsultaDOUMeioAmbiente.aspx.cs" Inherits="Processo_ConsultaDOUMeioAmbiente" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="../Scripts/Funcoes.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(document).ready(function () { CriarEventos(); });

     function CriarEventos() {
         $('#<%=rbtnCnPJ.ClientID %>').change(function () { rbtnTipoPesquisaClick(this) });
         $('#<%=rbtnNumeroProcesso.ClientID %>').change(function () { rbtnTipoPesquisaClick(this) });         
     }

     function rbtnTipoPesquisaClick(radioBtn) { 
     if ($(radioBtn).attr('id') == $('#<%=rbtnCnPJ.ClientID%>').attr('id')) {
             $('#<%=rbtnNumeroProcesso.ClientID%>').attr('checked', false);
             habilitarPesquisaEmpresa('block');
             habilitarPesquisaProcesso('none');
         } else {
             $('#<%=rbtnCnPJ.ClientID%>').attr('checked', false);
             habilitarPesquisaEmpresa('none');
             habilitarPesquisaProcesso('block');
         }
          
     }     

     function habilitarPesquisaEmpresa(habilitar) {
         $('#pesquisa_empresas').css('display', habilitar);
     }

     function habilitarPesquisaProcesso(habilitar) {
         $('#pesquisa_processos').css('display', habilitar);
         
     }
 </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
   Consulta de Processos Federais no Diário Oficial da União
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id="conteudo_sistema">
  <div id="filtros">
      <table width="100%">
          <tr>
              <td class="labelFiltros" width="20%">
                  Grupo Econômico:</td>
              <td>
                  <asp:DropDownList ID="ddlGrupoEconômico" runat="server" AutoPostBack="True" 
                      CssClass="DropDownList" Height="25px" Width="90%" onselectedindexchanged="ddlGrupoEconômico_SelectedIndexChanged" DataTextField="Nome" 
                      DataValueField="Id" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Selecione o grupo econômico do qual deseja pesquisar os processos no Diário Oficial da União')">
                  </asp:DropDownList>
              </td>
              <td class="labelFiltros" width="20%">
                  Data de Publicação de:</td>
              
              <td class="controlFiltros" width="20%">
                  <asp:TextBox ID="tbxDiaDe" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox> &nbsp;/
                  <asp:TextBox ID="tbxMesDe" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox></td>
              
              <td rowspan="4" width="10%" valign="bottom">
                  <asp:Button ID="btnConsultar" runat="server" CssClass="Button" 
                      OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Consulta a ocorrência do(s) processo(s) selecionado(s), utilizando a consulta disponível no site da Imprensa Oficial')"
                      Text="Consultar" onclick="btnConsultar_Click" />
              </td>
          </tr>
          <tr>
              <td class="labelFiltros" width="20%">
                  Pesquisar por:</td>
              <td>
                  <asp:RadioButton ID="rbtnCnPJ" runat="server" Text="CNPJ" 
                      AutoPostBack="True" oncheckedchanged="rbtnCnPJ_CheckedChanged" />&nbsp;
                  <asp:RadioButton ID="rbtnNumeroProcesso" runat="server" 
                      Text="Número do Processo Federal" AutoPostBack="True" 
                      oncheckedchanged="rbtnNumeroProcesso_CheckedChanged" />
                                  
                  </td>
              <td class="labelFiltros">
                  Até:</td>
              <td class="controlFiltros">
                  <asp:TextBox ID="tbxDiaAte" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox>&nbsp;/
                  <asp:TextBox ID="tbxMesAte" runat="server" CssClass="TextBox" Width="25px"></asp:TextBox>
                  &nbsp;de&nbsp;
                  <asp:DropDownList ID="ddlAnos" runat="server" CssClass="DropDownList" 
                      Width="100px">
                  </asp:DropDownList>
                  </td>
          </tr>
          <tr>
              <td colspan="2">
               <div id="pesquisa_empresas" style="display:none;">
                   <table width="100%">
                       <tr>
                           <td width="40%" class="labelFiltros">
                               Empresas:</td>
                           <td class="controlFiltros">
                             <asp:UpdatePanel ID="UPEmpresas" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
                          <asp:ListBox ID="lbxEmpresas" runat="server" 
                              CssClass="TextBox" SelectionMode="Multiple" onmouseout="tooltip.hide();" 
                              
                              
                              onmouseover="tooltip.show('Para selecionar mais de uma empresa, utilize as teclas ctrl para seleção individual ou shift para seleção em grupo, além de clicar na empresa desejada')" 
                              Width="100%" ></asp:ListBox>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconômico" 
                              EventName="SelectedIndexChanged" />
                          <asp:AsyncPostBackTrigger ControlID="rbtnCnPJ" EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                      </Triggers>
                  </asp:UpdatePanel>
                               </td>
                       </tr>
                   </table>
                  </div>
                </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td colspan="2" >
                <div id="pesquisa_processos" style="display:none;">
                  <table width="100%">
                       <tr>
                           <td width="40%" class="labelFiltros">
                               Processos Federais:</td>
                           <td class="controlFiltros">
                             <asp:UpdatePanel ID="UPProcessos" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
                          <asp:ListBox ID="lbxProcessos" runat="server" CssClass="TextBox"  
                              SelectionMode="Multiple" Width="90%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para selecionar mais de um processo, utilize as teclas ctrl para seleção individual ou shift para seleção em grupo, além de clicar no processo desejado')"></asp:ListBox>
                     <br/> <asp:Label ID="lblresult" runat="server"></asp:Label>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconômico" 
                              EventName="SelectedIndexChanged" />
                          <asp:AsyncPostBackTrigger ControlID="rbtnNumeroProcesso" 
                              EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                      </Triggers>
                  </asp:UpdatePanel>
                               </td>
                       </tr>
                   </table>
                </div>
                  </td>
              <td >
                  &nbsp;</td>
              <td >
                  &nbsp;</td>
          </tr>
          </table>
  </div>
  <div align="center" 
                style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-size: small; margin-top: 5px; margin-bottom: 5px;">
                <strong>ATENÇÃO:</strong> Os resultados das consultas são gerados pelo site da Imprensa Nacional 
                não sendo de responsabilidade do Sistema SUSTENTAR.</div>
    <div id="grid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
            
        </asp:UpdatePanel>
     </div>
        
 </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

