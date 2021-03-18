using Context;
using Model;
using Model.DTO;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class ParkRepository:RepositoryBase<Park>
    {
        private readonly CommonContext _ctx;
        public ParkRepository(CommonContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public Park GetById(int id)
        {
            return GetAll.Include(p=>p.ParkItems.Select(a=>a.ItemMaster)).FirstOrDefault(x => x.Id == id);
        }
        public override void InsertOrUpdate(Park entity)
        {
            if (entity.Id.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
            Context.SaveChanges();
        }
        public List<ParkedTableDTO> GetAllUnProcessedPark()
        {
            var data = GetAll.Where(x => !x.IsProcessed);
            var dataDTO = data.Select(a => new ParkedTableDTO
            {
                Id = a.Id,
                TableNo = a.TableNo
            }).ToList();
            return dataDTO;
        }
    }
}
