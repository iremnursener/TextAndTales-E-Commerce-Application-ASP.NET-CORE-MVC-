using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ShoppingCardRepository : Repository<ShoppingCard>,IShoppingCardRepository 
    {


        private  ApplicationDbContext _db;
        public ShoppingCardRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }


       
        public void Update(ShoppingCard obj)
        {
            _db.ShoppingCards.Update(obj);
        }
    }
}
