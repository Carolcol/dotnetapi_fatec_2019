using Store.Model.Entities;
using Store.Model.Infrastucture.Casts;
using Store.Model.Infrastucture.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model.Infrastucture.DataAcess
{
    public class ClienteDao : BaseDao<Cliente>
    {
        public ClienteDao() { }
        public ClienteDao(Connection connection) : base(connection) { }

        public override List<Cliente> CastToObject(SqlDataReader Reader)
        {
            List<Cliente> Clientes = new List<Cliente>();
            while (Reader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(Reader["ID"]);
                cliente.Nome = Convert.ToString(Reader["NOME"]);
                cliente.Cpf = Convert.ToString(Reader["CPF"]);
                cliente.Email = Convert.ToString(Reader["EMAIL"]);
                cliente.Senha = Convert.ToString(Reader["SENHA"]);
                Clientes.Add(DataCast.CastCliente(Reader));
            }
            return Clientes;
        }

        public Cliente Insert(Cliente cliente)
        {
            base.Sql.Append(" INSERT INTO TB_CLIENTE (NOME, CPF, EMAIL, SENHA) ");
            base.Sql.Append(" OUTPUT inserted.ID ");
            base.Sql.Append(" VALUES (@NOME, @CPF, @EMAIL, @SENHA) ");

            base.AddParameter("@NOME", cliente.Nome);
            base.AddParameter("@CPF", cliente.Cpf);
            base.AddParameter("@EMAIL", cliente.Email);
            base.AddParameter("@SENHA", cliente.Senha);

            cliente.Id = (int)base.ExecuteComand();

            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            base.Sql.Append(" UPDATE TB_CLIENTE SET ");
            base.Sql.Append("   NOME = @NOME, SENHA = @SENHA ");
            base.Sql.Append(" WHERE ID = @ID ");

            base.AddParameter("@NOME", cliente.Nome);
            base.AddParameter("@SENHA", cliente.Senha);
            base.AddParameter("@ID", cliente.Id);

            base.ExecuteComand();

            return cliente;
        }

        public Cliente UpdatePassword(Cliente cliente)
        {
            base.Sql.Append(" UPDATE TB_CLIENTE SET ");
            base.Sql.Append("   SENHA = @SENHA ");
            base.Sql.Append(" WHERE ID = @ID ");

            base.AddParameter("@SENHA", cliente.Senha);
            base.AddParameter("@ID", cliente.Id);

            base.ExecuteComand();

            return cliente;
        }

        protected override void SqlBase()
        {
            base.Sql.Append(" SELECT ");
            base.Sql.Append("   ID AS CLIENTE_ID, ");
            base.Sql.Append("   NOME AS CLIENTE_NOME, ");
            base.Sql.Append("   CPF AS CLIENTE_CPF, ");
            base.Sql.Append("   EMAIL AS CLIENTE_EMAIL, ");
            base.Sql.Append("   SENHA AS CLIENTE_SENHA ");
            base.Sql.Append(" FROM TB_CLIENTE ");
        }

        public List<Cliente> Select()
        {
            this.SqlBase();
            using (var Reader = base.ExecuteReader())
            {
                return this.CastToObject(Reader);
            }
        }

        public Cliente Select(int id)
        {
            this.SqlBase();
            base.Sql.Append(" WHERE ID = @ID ");

            base.AddParameter("@ID", id);

            using (var Reader = base.ExecuteReader())
            {
                return this.CastToObject(Reader).FirstOrDefault();
            }
        }

        public Cliente Select(Cliente cliente)
        {            
            this.SqlBase();
            base.Sql.Append(" WHERE EMAIL = @EMAIL AND SENHA = @SENHA ");

            base.AddParameter("@EMAIL", cliente.Email);
            base.AddParameter("@SENHA", cliente.Senha);

            using (var Reader = base.ExecuteReader())
            {
                return this.CastToObject(Reader).FirstOrDefault();
            } 
        }
    }
}
