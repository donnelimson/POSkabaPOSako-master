using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Park: BaseModel
    {
        public Park()
        {
            this.ParkItems = new HashSet<ParkItem>();
        }
        [Key]
        public int Id { get; set; }
        public int TableNo { get; set; }
        public bool IsProcessed { get; set; } = false;
        public virtual ICollection<ParkItem> ParkItems { get; set; }
    }
}
