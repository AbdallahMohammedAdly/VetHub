using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Specifications
{
    public class SpecPrams
    {
      
      private const int MaxPageSize = 10;

      private int pageSize = 7;
      public int PageSize 
        {
            get { return pageSize; }

            set { pageSize = value > MaxPageSize ? MaxPageSize : value ; }
        }
        public int PageIndex { get; set; } = 1;

        public  string? sort {  get; set; }
      public   string? search { get; set; }
    }
}
