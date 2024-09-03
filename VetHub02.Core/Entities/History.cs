using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class History : BaseEntity
    {
        public string ImageUrl { get; set; }

        public string PredictedName { get; set; }

        public string Cause {  get; set; }

        public string Symptoms { get; set; }

        public string Transmission {  get; set; }

        public string Prevention { get; set; }

        public string Treatment { get; set; }

        public DateTime DateOfDetect { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
