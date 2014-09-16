using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeevanGPS.Repository.Interfaces;
using JeevanGPS.DAL; 

namespace JeevanGPS.Repository.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IPersonDAL personDAL;

        public PersonRepository(IPersonDAL person)
        {
            personDAL = person;
        }

        public void Insert(JeevanGPS.BO.Person person)
        {
            personDAL.Insert(TransferObject(person));
        }


        private Person TransferObject(JeevanGPS.BO.Person person)
        {
            return (new Person
            {
                Name = person.Name,
                Age = person.Age,
                Email = person.Email
            });
        }
    }
}
