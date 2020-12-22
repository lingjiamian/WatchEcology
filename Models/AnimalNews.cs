using System;
using System.Collections.Generic;

namespace WatchEcology.Models
{
    public partial class AnimalNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Source { get; set; }
        public DateTime? PubDate { get; set; }
    }
}
