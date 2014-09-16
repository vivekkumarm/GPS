using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using JeevanGPS.BO;
using JeevanGPS.Repository.Interfaces;
using JeevanGPS.Repository.Repositories;


namespace JeevanGPS.BL
{
    public class PersonBL
    {
        private readonly IPersonRepository personRepository;

        public PersonBL(IPersonRepository repository)
        {
            personRepository = repository;
        }
       

        public Person CreatePerson(Person person)
        {
            personRepository.Insert(person);
            return person;
        }     
        
    }
}
