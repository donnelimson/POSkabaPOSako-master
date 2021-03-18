using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseModel
    {
        [ForeignKey("CreatedByAppuser")]
        public int CreatedByAppuserId { get; set; } = 1;
        public virtual Appuser CreatedByAppuser { get; set; }
        [ForeignKey("ModifiedByAppuser")]
        public int? ModifiedByAppuserId { get; set; } = 1;
        public virtual Appuser ModifiedByAppuser { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; } = DateTime.Now;

   

    }
}
