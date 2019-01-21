using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DateBaseController.Models
{
    public partial class Rang
    {     
        public Rang()
        {
            Users = new List<Viewer>();
            RangSets = new List<RangSet>();
            Priority = 0;
            Name = "TwitchRangs";
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public int Priority { get; set; }

      
        public virtual List<Viewer> Users { get; set; }
        
        public virtual List<RangSet> RangSets { get; set; }

        public bool IsFollow { get; set; }

        public bool IsSubscriber { get; set; }
    }
}
