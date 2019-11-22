using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Model.Infrastucture.Sql;
using Store.Model.Entities;
using Store.Model.Infrastucture.Casts;

namespace Store.Model.Infrastucture.DataAcess
{
    public class CategoriaDao : BaseDao<Categoria>
    {
        public CategoriaDao() { }
        public CategoriaDao(Connection connection) : base(connection) { }

        public override List<Categoria> CastToObject(SqlDataReader Reader)
        {
            List<Categoria> Categorias = new List<Categoria>();
            while (Reader.Read())
            {
                Categorias.Add(DataCast.CastCategoria(Reader));
            }
            return Categorias;
        }

        public List<Categoria> Select()
        {
            this.SqlBase();

            using (var Reader = base.ExecuteReader())
            {
                return this.CastToObject(Reader);
            }
        }

        protected override void SqlBase()
        {
            base.Sql.Append(" SELECT ");
            base.Sql.Append("    ID AS CATEGORIA_ID, ");
            base.Sql.Append("    DESCRICAO AS CATEGORIA_DESCRICAO ");
            base.Sql.Append(" FROM TB_CATEGORIA ");
        }
    }
}
