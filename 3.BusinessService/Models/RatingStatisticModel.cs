using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class RatingStatisticModel
    {
        public string OneStar { get; set; }
        public string TwoStar { get; set; }
        public string ThreeStar { get; set; }
        public string FourStar { get; set; }
        public string FiveStar { get; set; }

        public int OneStarCount { get; set; }
        public int TwoStarCount { get; set; }
        public int ThreeStarCount { get; set; }
        public int FourStarCount { get; set; }
        public int FiveStarCount { get; set; }
    }
}
