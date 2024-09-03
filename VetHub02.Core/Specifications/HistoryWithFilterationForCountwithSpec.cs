using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class HistoryWithFilterationForCountwithSpec : BaseSpecification<History>
    {

            public HistoryWithFilterationForCountwithSpec(SpecPrams SpecPrams)
               : base(A => string.IsNullOrEmpty(SpecPrams.search) || A.PredictedName.ToLower().Contains(SpecPrams.search.ToLower()))
            {


            }
        
    }
}
