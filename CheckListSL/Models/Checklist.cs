using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckListSL.Models
{
    public class Checklist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public bool isActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}