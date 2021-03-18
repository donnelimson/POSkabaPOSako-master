using Context;
using Infrastructure.Repositories.Base;
using Model;
using Model.DTO;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class ItemMasterRepository : RepositoryBase<ItemMaster>
    {
        private readonly CommonContext _ctx;
        public ItemMasterRepository(CommonContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public override void InsertOrUpdate(ItemMaster entity)
        {
            if (entity.Id.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
            _ctx.SaveChanges();
        }
        public ItemMaster GetItemMasterByBarcode(string barcode)
        {
            return GetAll.FirstOrDefault(x => x.BarCode == barcode);
        }
        public ItemMaster GetItemMasterById(int id)
        {
            return GetAll.FirstOrDefault(x => x.Id == id);
        }
        public List<ItemMasterButtonsDTO> GetAllItemMasterButtons(int categoryId)
        {
            return GetAll.Where(x => x.CategoryId == categoryId).Select(a => new ItemMasterButtonsDTO {Id=a.Id, LongDescription=a.LongDescription }).ToList();
        }
    }
}
