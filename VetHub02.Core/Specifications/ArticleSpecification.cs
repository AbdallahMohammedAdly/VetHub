using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class ArticleSpecification : BaseSpecification<Article>
    {
        public ArticleSpecification(SpecPrams SpecPrams)
            :base(A => string.IsNullOrEmpty(SpecPrams.search) || A.Title.ToLower().Contains(SpecPrams.search.ToLower())  )
        {
            Includes.Add(A => A.User);
            Includes.Add(A => A.Comments);

            if (!string.IsNullOrEmpty(SpecPrams.sort))
            {
                switch (SpecPrams.sort)
                {
                    case "DateAsc":
                        AddOrderBy(A => A.TimeOfArticle);
                        break;
                    case "DateDec":
                        AddOrderByDescndein(A => A.TimeOfArticle);
                        break;
                    default:
                        AddOrderBy(A => A.Title);
                        break;
                }
            }

            ApplyPagination(SpecPrams.PageSize*(SpecPrams.PageIndex - 1),SpecPrams.PageSize);
        }
        public ArticleSpecification(int id) : base(A => A.Id == id) 
        {
            Includes.Add(A => A.User);
            Includes.Add(A => A.Comments);
            // Predicate(A => A.Id = id);
        }
    }
}
