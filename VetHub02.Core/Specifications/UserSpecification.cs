using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification()
        {
            Includes.Add(U => U.Articles);
            Includes.Add(U => U.Questions);
            Includes.Add(U => U.Comments);
        }
        public UserSpecification( int id) : base (U => U.Id == id)
        {
            Includes.Add(U => U.Articles);
            Includes.Add(U => U.Questions);
            Includes.Add(U => U.Comments);
        }
        public UserSpecification(string email) : base(U => U.Email == email)
        {
            Includes.Add(U => U.Articles);
            Includes.Add(U => U.Questions);
            Includes.Add(U => U.Comments);
        }

    }
}
