using System;
using System.Collections.Generic;

namespace WatchEcology.Models
{
    public partial class Option
    {
        public int Id { get; set; }
        public string OptionContent { get; set; }
        public int? QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
