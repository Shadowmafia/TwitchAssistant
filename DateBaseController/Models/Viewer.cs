using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DateBaseController.Models
{
    public partial class Viewer
    {
        public Viewer()
        {
            Coins = 0;
            LastConnectToStream= FirstConnectToStream = LastCoinUpdate = DateTime.Now;
            IsSubscriber = false;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public int Coins { get; set; }

        public bool IsFollower { get; set; }
        public bool IsSubscriber { get; set; }
        public DateTime WatchingTime { get; set; }

        public DateTime LastConnectToStream { get; set; }
        public DateTime FirstConnectToStream { get; set; }
        public DateTime LastCoinUpdate { get; set; }
       
        public virtual Rang Rang { get; set; }
        
    }
}
