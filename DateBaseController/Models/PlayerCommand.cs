using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseController.Models
{
    public class PlayerCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Whisp { get; set; }
        public bool Message { get; set; }

        public string Action { get; set; }
        public DateTime LastCommandCall { get; set; }
    }
}
