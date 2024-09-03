using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class HistorySpecification : BaseSpecification<History>
    {
        public HistorySpecification(SpecPrams SpecPrams)
           : base(A => string.IsNullOrEmpty(SpecPrams.search) || A.PredictedName.ToLower().Contains(SpecPrams.search.ToLower()))
        {
            Includes.Add(A => A.User);
            

            if (!string.IsNullOrEmpty(SpecPrams.sort))
            {
                switch (SpecPrams.sort)
                {
                    case "DateAsc":
                        AddOrderBy(A => A.DateOfDetect);
                        break;
                    case "DateDec":
                        AddOrderByDescndein(A => A.DateOfDetect);
                        break;
                    default:
                        AddOrderBy(A => A.PredictedName);
                        break;
                }
            }
          //  ApplyPagination(SpecPrams.PageSize * (SpecPrams.PageIndex - 1), SpecPrams.PageSize);
        }
        public HistorySpecification(int id) : base(A => A.Id == id)
        {
            Includes.Add(A => A.User);
          
            // Predicate(A => A.Id = id);
        }
    }
}
