using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.ViewModel
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public int Type { get; set; }
        public int Stick { get; set; }
    }
}
