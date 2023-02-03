using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo.Auditoria
{
    public partial class Auditoria : ObjetoBase
    {
        #region __________________ Padrao __________________

        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Auditoria ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Auditoria classe = new Auditoria();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Auditoria>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Auditoria ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Auditoria>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Auditoria Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Auditoria>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Auditoria SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Auditoria>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Auditoria> SalvarTodos(IList<Auditoria> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Auditoria>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Auditoria> SalvarTodos(params Auditoria[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Auditoria>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Auditoria>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Auditoria>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Auditoria> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Auditoria obj = Activator.CreateInstance<Auditoria>();
            return fabrica.GetDAOBase().ConsultarTodos<Auditoria>(obj);
        }

        #endregion

        public virtual string GetValorNEW
        {
            get{
              if (this.xtype == 175 && this.valor_new == "T")
                {
                    return "SIM";
                }
                else if (this.xtype == 175 && this.valor_new == "F")
                {
                    return "NÃO";
                }
                else
                    return valor_new;
            }
        }

        public virtual string GetValorOLD
        {
            get
            {
                if (this.xtype == 175 && this.valor_old == "T")
                {
                    return "SIM";
                }
                else if (this.xtype == 175 && this.valor_old == "F")
                {
                    return "NÃO";
                }
                else
                    return valor_old;
            }
        }

        public virtual string GetUsuario
        {
            get
            {
                if (this.Auditoria_user == null)
                {
                    return "Alteração realizada diretamente no Banco de Dados";
                }
                else
                {
                    return this.Auditoria_user.Usuario;
                }

            }
        }

        public virtual string GetData
        {
            get
            {
                if (this.Auditoria_user == null)
                {
                    return " -- ";
                }
                else
                {
                    return this.Auditoria_user.Data.ToString();
                }

            }
        }

        public static IList<Auditoria> Filtrar(string usuario, String dataDe, String dataAte, string operacao, string tabela, string valorAntigo, string valorNovo)
        {
            Auditoria ee = new Auditoria();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Id"));
            try
            {
                if (operacao != "")
                    ee.AdicionarFiltro(Filtros.Like("Operacao", operacao));
                if (tabela != "")
                    ee.AdicionarFiltro(Filtros.Like("Tabela", tabela));
                if (valorNovo != "")
                    ee.AdicionarFiltro(Filtros.Like("Valor_new", valorNovo));
                if (valorAntigo != "")
                    ee.AdicionarFiltro(Filtros.Like("Valor_old", valorAntigo));

                ee.AdicionarFiltro(Filtros.CriarAlias("Auditoria_user", "E"));
                ee.AdicionarFiltro(Filtros.Like("E.Usuario", usuario));

                if (dataAte.Trim() != "" && dataDe.Trim() != "")
                {                    
                    ee.AdicionarFiltro(Filtros.Between("E.Data", Convert.ToDateTime(dataDe), Convert.ToDateTime(dataAte)));
                }
                else if (dataDe.Trim() != "")
                {
                    ee.AdicionarFiltro(Filtros.Between("E.Data", dataDe, DateTime.Now));
                }
            }
            catch (Exception)
            { }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Auditoria>(ee);
        }

        public static void ExcluirAuditoriasComSQLDateTimeMinValue()
        {
            Auditoria ee = new Auditoria();
            ee.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Valor_new", "Jan  1 1753 12:00AM"), Filtros.Eq("Valor_old", "Jan  1 1753 12:00AM")));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();

            IList<Auditoria> audits = fabrica.GetDAOBase().ConsultarComFiltro<Auditoria>(ee);

            foreach (Auditoria auditoria in audits)
            {
                auditoria.Excluir();
            }
        }
    }
}
