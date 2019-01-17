using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsValid { get; set; }
    }

    public class SelectViewModel
    {
        public string querystr { get; set; }

        public int page { get; set; }
    }

    public class ClientSetViewModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string ParkId { get; set; }

        public string ParkName { get; set; }
    }
}
