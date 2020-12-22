using System;
using System.Collections.Generic;

namespace WatchEcology.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public string QuestionContent { get; set; }
        public string Answer { get; set; }
        public string BgKlg { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public ICollection<Option> Option { get; internal set; }
    }
}
