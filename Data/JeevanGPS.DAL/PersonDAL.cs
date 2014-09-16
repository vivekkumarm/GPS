using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanGPS.DAL
{
    public class PersonDAL : IPersonDAL
    {
        public void Insert(Person person)
        {

            using (PersonDataContext context = new PersonDataContext())
            {
                context.Persons.InsertOnSubmit(person);
                context.SubmitChanges();

            }
     
        }

    }
}
