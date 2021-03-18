using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ParkItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ItemMaster")]
        public int ItemMasterId { get; set; }
        public virtual ItemMaster ItemMaster { get; set; }
        public int Quantity { get; set; }

        public int Index { get; set; }
        [ForeignKey("Park")]
        public int ParkId { get; set; }

        public virtual Park Park { get; set; }
     
    }
}
