using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeevanGPS.DAL;
using JeevanGPS.Repository.Interfaces;
using JeevanGPS.Repository.Repositories;

namespace JeevanGPS.Repository
{
    public class PersonRepositoryFactory
    {
        public static IPersonRepository PersonRepository()
        {
            var personDAL = new PersonDAL();

            return new PersonRepository(personDAL); 
        }
    }
}
