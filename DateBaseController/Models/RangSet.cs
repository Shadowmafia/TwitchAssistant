using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DateBaseController.Models
{
    public partial class RangSet
    {
        public RangSet()
        {
            Rangs = new List<Rang>();
            Name = "New User";
            Booster = 0;
            MessageTimeOut = 1000;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Booster { get; set; }

        public int MessageTimeOut { get; set; }

        public virtual List<Rang> Rangs { get; set; }
    }
}
