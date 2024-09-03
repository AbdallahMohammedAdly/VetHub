using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class QuestionSpecification : BaseSpecification<Question>
    {
        public QuestionSpecification(SpecPrams specPrams)
            : base (Q => string.IsNullOrEmpty(specPrams.search) || Q.Title.ToLower().Contains(specPrams.search.ToLower()))
        {
            Includes.Add(Q => Q.User);
            Includes.Add(Q => Q.Comments);

            if (!string.IsNullOrEmpty(specPrams.sort))
            {
                switch (specPrams.sort)
                {
                    case "DateAsc":
                        AddOrderBy(Q => Q.CreatedDate);
                        break;
                    case "DateDec":
                        AddOrderByDescndein(Q => Q.CreatedDate);
                        break;
                    default:
                        AddOrderBy(Q => Q.Title);
                        break;
                }
            }

            ApplyPagination(specPrams.PageSize * (specPrams.PageIndex - 1), specPrams.PageSize);

        }
        public QuestionSpecification(int id) : base (Q=>Q.Id == id)
        {
            Includes.Add(Q => Q.User);
            Includes.Add(Q => Q.Comments);
        }
    }
}
