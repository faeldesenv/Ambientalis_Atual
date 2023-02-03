using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class ContratoComercial : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ContratoComercial ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ContratoComercial classe = new ContratoComercial();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ContratoComercial>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ContratoComercial ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ContratoComercial>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ContratoComercial Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ContratoComercial>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ContratoComercial SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ContratoComercial>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ContratoComercial> SalvarTodos(IList<ContratoComercial> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ContratoComercial>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ContratoComercial> SalvarTodos(params ContratoComercial[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ContratoComercial>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ContratoComercial>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ContratoComercial>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ContratoComercial> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ContratoComercial obj = Activator.CreateInstance<ContratoComercial>();
            return fabrica.GetDAOBase().ConsultarTodos<ContratoComercial>(obj);
        }


        /// <summary>
        /// Filtra uma certa quantidade de ArquivoFisico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ContratoComercial> Filtrar(int qtd)
        {
            ContratoComercial cidade = new ContratoComercial();
            if (qtd > 0)
                cidade.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ContratoComercial>(cidade);
        }

        /// <summary>
        /// Retorna o ultimo ArquivoFisico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo ArquivoFisico</returns>
        public virtual ContratoComercial UltimoInserido()
        {
            ContratoComercial ArquivoFisico = new ContratoComercial();
            ArquivoFisico.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ContratoComercial>(ArquivoFisico);
        }

        public static int GetUltimoNumeroDeContrato
        {
            get
            {
                ContratoComercial aux = new ContratoComercial();
                aux.AdicionarFiltro(Filtros.Eq("Ano", DateTime.Now.Year));
                aux.AdicionarFiltro(Filtros.ValorMaximo("Numero"));
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                object resultado = fabrica.GetDAOBase().ConsultarProjecao(aux);
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
        }

        public static String GetNomeMes(int i)
        {
            string retorno = "";
            if (i > 0)
            {
                switch (i)
                {
                    case 1: retorno = "Janeiro"; break;
                    case 2: retorno = "Fevereiro"; break;
                    case 3: retorno = "Março"; break;
                    case 4: retorno = "Abril"; break;
                    case 5: retorno = "Maio"; break;
                    case 6: retorno = "Junho"; break;
                    case 7: retorno = "Julho"; break;
                    case 8: retorno = "Agosto"; break;
                    case 9: retorno = "Setembro"; break;
                    case 10: retorno = "Outubro"; break;
                    case 11: retorno = "Novembro"; break;
                    case 12: retorno = "Dezembro"; break;
                }
                return retorno;
            }
            else { return retorno; }
        }

    }
}