using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckListSL.Models
{
    public class Translation
    {
        public int Id { get; set; }

        public string Language { get; set; }

        public string TranslationString { get; set; }

        public int? ItemId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}