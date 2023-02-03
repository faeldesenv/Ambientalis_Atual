using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Modelo;
using Utilitarios;

public partial class Relatorios_RelatoriosGraficos : PageBase
{
    Msg msg = new Msg();
    Transacao transacao = new Transacao();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                transacao.Abrir();
                int idRevenda = Utilitarios.Criptografia.Seguranca.RecuperarParametro("idRev", this.Request).ToInt32();
                int ano = Utilitarios.Criptografia.Seguranca.RecuperarParametro("ano", this.Request).ToInt32();
                string tipo = Utilitarios.Criptografia.Seguranca.RecuperarParametro("tipo", this.Request);
                if (idRevenda > 0 && ano > 0)
                    if (tipo == "B")
                        this.CarregarGraficoBarras(idRevenda, ano);
                    else
                        this.CarregarGraficoPizza(idRevenda, ano);
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            transacao.Fechar(ref msg);
        }
    }

    public void CarregarGraficoBarras(int idRevenda, int ano)
    {
        List<List<int>> valoresGrafico = new List<List<int>>();
        int valorMaximo = 0;
        int quantVenda;
        int quantProsp;
        IList<Venda> vendas = Venda.FiltrarRelatorio(Revenda.ConsultarPorId(idRevenda), new DateTime(ano, 1, 1), new DateTime(ano, 12, 31), 1);
        IList<Prospecto> prospectos = Prospecto.FiltrarRelatorio(Revenda.ConsultarPorId(idRevenda), null, null, new DateTime(ano, 1, 1), new DateTime(ano, 12, 31), 1);
        //if (vendas.Count > 0)
        //{

        for (int mes = 0; mes < 12; mes++)
        {
            quantVenda = vendas.Where(vd => vd.Data.Month == mes + 1).ToList().Count;
            quantProsp = prospectos.Where(pro => pro.DataCadastro.Month == mes + 1).ToList().Count;
            valoresGrafico.Add(new List<int>() { quantVenda, quantProsp });

            valorMaximo = quantVenda > quantProsp ? quantVenda > valorMaximo ?
                quantVenda : valorMaximo : quantProsp > valorMaximo ? quantProsp : valorMaximo;
        }
        //}

        valorMaximo += (int)(valorMaximo * 0.2);
        JavaScriptSerializer json = new JavaScriptSerializer();
        hfBarrasValorMaximo.Value = valorMaximo.ToString();
        hfBarrasValores.Value = json.Serialize(valoresGrafico);
        hfAno.Value = ano.ToString();
    }

    public void CarregarGraficoPizza(int idRevenda, int ano)
    {
        string nomeEstado = "";
        Dictionary<string, int> valoresGrafico = new Dictionary<string, int>();
        IList<Prospecto> prospectos = Prospecto.FiltrarRelatorio(Revenda.ConsultarPorId(idRevenda), null, null, new DateTime(ano, 1, 1), new DateTime(ano, 12, 31), 1);
        for (int estado = 0; estado < 27; estado++)
        {
            int quantidade = prospectos.Where(props => props.Endereco.Cidade.Estado.Id == estado).ToList().Count;
            if (quantidade > 0)
            {
                nomeEstado = this.GetEstado(estado);
                valoresGrafico.Add(nomeEstado, quantidade);
            }
        }
        JavaScriptSerializer json = new JavaScriptSerializer();
        hfPizzaValores.Value = json.Serialize(valoresGrafico);
    }

    private string GetEstado(int id)
    {
        string[] estados =  {
                 "Acre", 
                 "Alagoas", 
                 "Amapá", 
                 "Amazonas", 
                 "Bahia", 
                 "Ceará", 
                 "Distrito Federal", 
                 "Espírito Santo", 
                 "Goiás", 
                 "Maranhão", 
                 "Mato Grosso", 
                 "Mato Grosso do Sul", 
                 "Minas Gerais", 
                 "Pará", 
                 "Paraíba", 
                 "Paraná", 
                 "Pernambuco", 
                 "Piauí", 
                 "Rio de Janeiro", 
                 "Rio Grande do Norte", 
                 "Rio Grande do Sul", 
                 "Rondônia", 
                 "Roraima", 
                 "Santa Catarina", 
                 "São Paulo", 
                 "Sergipe", 
                 "Tocantins"
                            };
        return estados[id];
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        UsuarioComercial user = (UsuarioComercial)Session["UsuarioLogado_SistemaComercial"];
        if (user != null && user.GetType() == typeof(UsuarioSupervisorComercial) || user.GetType() == typeof(UsuarioAdministradorComercial))
            Response.Redirect("FiltroRelatoriosSupervisor.aspx", false);
        else
            Response.Redirect("FiltrosRelatoriosRevendas.aspx", false);
    }
}