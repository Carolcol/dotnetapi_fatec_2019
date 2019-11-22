using Store.BusinessLogic.BL.Interfaces;
using Store.Model.Entities;
using Store.Model.Infrastucture.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.BL
{
    public class CategoriaBL : ICategoriaBL
    {
        private CategoriaDao _categoriaDao;

        public CategoriaBL()
        {
            this._categoriaDao = new CategoriaDao();
        }

        public List<Categoria> Listar()
        {
            return this._categoriaDao.Select();
        }

        public void Dispose()
        {
            this._categoriaDao.Dispose();
        }
    }
}
