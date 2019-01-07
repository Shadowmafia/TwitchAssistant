using System;

namespace DateBaseController.Models
{
    public class MessageTimer
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public string Message { get; set; }
        public long Interval { get; set; }
        public DateTime LastShow { get; set; }
    }
}
