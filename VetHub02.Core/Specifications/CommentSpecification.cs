using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class CommentSpecification : BaseSpecification<Comment>
    {
        public CommentSpecification()
        {
            Includes.Add(C => C.User);

            Includes.Add(C => C.Article!);

            Includes.Add(C => C.question!);
        }
        public CommentSpecification(int id) : base (C => C.Id == id)
        {
            Includes.Add(C => C.User);

            Includes.Add(C => C.Article!);

            Includes.Add(C => C.question!);
        }
    }
}
