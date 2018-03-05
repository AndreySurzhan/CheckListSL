using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckListSL.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ChecklistId { get; set; }

        public virtual ICollection<Translation> Translations { get; set; }

        public bool? isTranslated { get; set; }

        public bool? isChecked { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}