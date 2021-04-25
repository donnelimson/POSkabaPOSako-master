using Context;
using Infrastructure.Repositories.Base;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ParkService
    {
        ParkRepository _parkRepository = new ParkRepository(new CommonContext());

        public void ParkItems(BindingList<ItemMasterListboxMainPOS> items, int appuserId, int tableNo)
        {
            var park = new Park
            {
                CreatedByAppuserId=appuserId,
                ModifiedByAppuserId = appuserId,
                TableNo = tableNo
            };
            foreach(var item in items)
            {
                park.ParkItems.Add(new ParkItem
                {
                    ItemMasterId = item.Id,
                    Quantity = item.Quantity,
                    Index = item.Index
                });
            };
            _parkRepository.InsertOrUpdate(park);
        }
        public List<ParkedTableDTO> GetAllUnProcessedPark()
        {
            return _parkRepository.GetAllUnProcessedPark();
        }
        public BindingList<ItemMasterListboxMainPOS> RetrievePark(int id)
        {
            var data = _parkRepository.GetById(id);
            var items = new BindingList<ItemMasterListboxMainPOS>();
            foreach(var item in data.ParkItems)
            {
                items.Add(new ItemMasterListboxMainPOS
                {
                    Id = item.ItemMasterId,
                    LongDescription = item.ItemMaster.LongDescription,
                    Index = item.Index,
                    Quantity = item.Quantity
                });
            }
            return items;
        }
    }
}
