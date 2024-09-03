using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class JoinSpecification : BaseSpecification<Join>
    {
        public JoinSpecification(SpecPrams SpecPrams)
              : base(A => string.IsNullOrEmpty(SpecPrams.search) || A.Name.ToLower().Contains(SpecPrams.search.ToLower()))
        {
            Includes.Add(A => A.User);
           

            if (!string.IsNullOrEmpty(SpecPrams.sort))
            {
                switch (SpecPrams.sort)
                {
                    case "DateAsc":
                        AddOrderBy(A => A.DateTime);
                        break;
                    case "DateDec":
                        AddOrderByDescndein(A => A.DateTime);
                        break;
                    default:
                        AddOrderBy(A => A.Name);
                        break;
                }
            }

            ApplyPagination(SpecPrams.PageSize * (SpecPrams.PageIndex - 1), SpecPrams.PageSize);
        }
        public JoinSpecification(int id) : base(A => A.Id == id)
        {
            Includes.Add(A => A.User);
            // Predicate(A => A.Id = id);
        }

    }
}
