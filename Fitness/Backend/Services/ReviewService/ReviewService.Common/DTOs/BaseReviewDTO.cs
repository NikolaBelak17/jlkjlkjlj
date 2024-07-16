using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Common.DTOs
{
    public class BaseReviewDTO
    {
        public string TrainerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
