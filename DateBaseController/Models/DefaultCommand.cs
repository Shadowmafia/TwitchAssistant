using System;

namespace DateBaseController.Models
{
   public class DefaultCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Whisp { get; set; }
        public bool Message { get; set; }
        public DateTime LastCommandCall { get; set; }
    }
}
