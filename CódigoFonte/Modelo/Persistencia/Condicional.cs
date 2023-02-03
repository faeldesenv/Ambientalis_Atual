using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;

namespace Modelo
{
    public partial class Condicional : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Condicional ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Condicional classe = new Condicional();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Condicional>(classe);
            }
        }

        public virtual Vencimento GetUltimoVencimento
        {
            get
            {
                if (this.Vencimentos != null && this.Vencimentos.Count > 0)
                {
                    this.Vencimentos = this.Vencimentos.OrderBy(i => i.Data).ToList();
                    return this.Vencimentos[this.Vencimentos.Count - 1];
                }

                return new Vencimento();
            }
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Condicional> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Condicional obj = Activator.CreateInstance<Condicional>();
            return fabrica.GetDAOBase().ConsultarTodos<Condicional>(obj);
        }

        public virtual GrupoEconomico GetGrupoEconomico
        {
            get
            {
                if (this.GetEmpresa == null)
                    return null;
                return this.GetEmpresa.GrupoEconomico;
            }
        }

        public virtual string GetNomeGrupoEconomico
        {
            get
            {
                if (this.GetEmpresa == null)
                    return "Não Definido";
                return this.GetEmpresa.GrupoEconomico.Nome;
            }
        }

        public virtual Empresa GetEmpresa
        {
            get
            {
                if (this.GetType() == typeof(Condicionante))
                {
                    return ((Condicionante)this).Licenca.Processo.Empresa;
                }
                else if (this.GetType() == typeof(OutrosEmpresa))
                {
                    return ((OutrosEmpresa)this).Empresa;
                }
                else if (this.GetType() == typeof(OutrosProcesso))
                {
                    return ((OutrosProcesso)this).Processo.Empresa;
                }
                return null;
            }
        }

        public virtual string GetNomeEmpresa
        {
            get
            {
                if (this.GetType() == typeof(Condicionante))
                {
                    return ((Condicionante)this).Licenca != null && ((Condicionante)this).Licenca.Processo != null && ((Condicionante)this).Licenca.Processo.Empresa != null ? ((Condicionante)this).Licenca.Processo.Empresa.Nome + " - " + ((Condicionante)this).Licenca.Processo.Empresa.GetNumeroCNPJeCPFComMascara : "--";
                }
                else if (this.GetType() == typeof(OutrosEmpresa))
                {
                    return ((OutrosEmpresa)this).Empresa != null ? ((OutrosEmpresa)this).Empresa.Nome + " - " + ((OutrosEmpresa)this).Empresa.GetNumeroCNPJeCPFComMascara : "--";
                }
                else if (this.GetType() == typeof(OutrosProcesso))
                {
                    return ((OutrosProcesso)this).Processo != null && ((OutrosProcesso)this).Processo.Empresa != null ? ((OutrosProcesso)this).Processo.Empresa.Nome + " - " + ((OutrosProcesso)this).Processo.Empresa.GetNumeroCNPJeCPFComMascara : "--";
                }
                return "Não definido";
            }
        }        

        public virtual Consultora GetConsultora
        {
            get
            {
                if (this.GetType() == typeof(Condicionante))
                {
                    return ((Condicionante)this).Licenca.Processo.Consultora;
                }
                else if (this.GetType() == typeof(OutrosEmpresa))
                {
                    return ((OutrosEmpresa)this).Consultora;
                }
                else if (this.GetType() == typeof(OutrosProcesso))
                {
                    return ((OutrosProcesso)this).Processo.Consultora;
                }
                return null;
            }
        }

        public virtual string GetDataUltimoVencimento 
        {
            get 
            {
                return this.GetUltimoVencimento != null && this.GetUltimoVencimento.Id > 0 ? this.GetUltimoVencimento.Data.ToShortDateString() : "--";
            }
        }        

        public virtual string GetTipo
        {
            get
            {
                if (this.GetType() == typeof(Condicionante))
                {
                    return "Condicionante";
                }
                else if (this.GetType() == typeof(OutrosEmpresa))
                {
                    return "Outros Empresa";
                }
                else if (this.GetType() == typeof(OutrosProcesso))
                {
                    return "Outros Processo";
                }
                return "Não definido";
            }
        }

        public virtual bool GetPeriodico
        {
            get
            {
                return this.GetUltimoVencimento.Id > 0 ? this.GetUltimoVencimento.Periodico : false;
            }
        }

        public virtual string GetDescPeriodico
        {
            get
            {
                return this.GetUltimoVencimento!= null &&  this.GetUltimoVencimento.Id > 0 ? this.GetUltimoVencimento.Periodico ? "Sim" : "Não" : "Não";
            }
        }

        public virtual string GetQtdProrrogacoes
        {
            get
            {
                return this.GetUltimoVencimento != null && this.GetUltimoVencimento.ProrrogacoesPrazo != null ? this.GetUltimoVencimento.ProrrogacoesPrazo.Count.ToString() : "0";
            }
        }

        public virtual string GetNumeroProcesso
        {
            get
            {
                return this.GetProcesso != null ? this.GetProcesso.Numero : "--";
            }
        }        

        public virtual Processo GetProcesso
        {
            get
            {
                if (this.GetType() == typeof(OutrosProcesso))                 
                {
                    OutrosProcesso outrProc = (OutrosProcesso)this;
                    return outrProc.Processo;
                }

                if (this.GetType() == typeof(Condicionante))
                {
                    Condicionante outrProc = (Condicionante)this;
                    return outrProc.Licenca != null ? outrProc.Licenca.Processo : null;
                }

                return null;
            }
        }

        public virtual string GetNomeOrgaoAmbiental
        {
            get
            {
                return this.GetOrgaoAmbiental != null ? this.GetOrgaoAmbiental.Nome : "--";
            }
        }

        public virtual OrgaoAmbiental GetOrgaoAmbiental
        {
            get
            {
                if (this.GetType() == typeof(OutrosEmpresa))
                {
                    OutrosEmpresa outrEmp = (OutrosEmpresa)this;
                    return outrEmp.OrgaoAmbiental;
                }

                if (this.GetType() == typeof(Condicionante))
                {
                    Condicionante outrProc = (Condicionante)this;
                    return outrProc.Licenca != null && outrProc.Licenca.Processo != null ? outrProc.Licenca.Processo.OrgaoAmbiental : null;
                }

                return null;
            }
        }
    }
}
