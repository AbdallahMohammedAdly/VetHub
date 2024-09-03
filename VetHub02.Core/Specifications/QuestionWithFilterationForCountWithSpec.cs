using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class QuestionWithFilterationForCountWithSpec : BaseSpecification<Question>
    {
      
      public QuestionWithFilterationForCountWithSpec(SpecPrams SpecPrams)
               : base(A => string.IsNullOrEmpty(SpecPrams.search) || A.Title.ToLower().Contains(SpecPrams.search.ToLower()))
       {


       }
        
    }
}
