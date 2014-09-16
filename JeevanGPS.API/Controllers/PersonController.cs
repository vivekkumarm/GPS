using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using JeevanGPS.BO;
using JeevanGPS.BL;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidation.Attributes;
using FluentValidation.Validators;
using FluentValidation.Results;
using System.Web.Http.ModelBinding;
using System.Net.Http.Formatting;
using JeevanGPS.Repository;
using System.Xml;

namespace JeevanGPS.API.Controllers
{
    public class PersonController : ApiController
    {

        PersonBL personBL = null;

        [HttpPost]
        public HttpResponseMessage Post(Person person)
        {
            var personRepository = PersonRepositoryFactory.PersonRepository();

            personBL = new PersonBL(personRepository);

            HttpResponseMessage response;    

            // Initiate Validation.
            var validator = new PersonValidator();
            ValidationResult results = validator.Validate(person, ruleSet: "Names");            
            

            // Add error messages if any.
            ModelStateDictionary modelDictionary = new ModelStateDictionary();
            foreach(ValidationFailure result in results.Errors)
            {
                 modelDictionary.AddModelError(result.PropertyName, result.ErrorMessage);
            }     


            if (results.IsValid)
            {

                // Calls Business Logic to create.
                try
                {
                    personBL.CreatePerson(person);
                }
                catch (Exception ex)
                {
                    modelDictionary.AddModelError("Error", "Service Error");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, modelDictionary);
                }


                // Creates response
                response = Request.CreateResponse(HttpStatusCode.Created, person);
            }
            else
            {
                // Creates response
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, modelDictionary);              
            }

            return response;
        }





        List<Person> person = new List<Person>();

        public IEnumerable<Person> GetAll()
        {
            person.Add(new Person { Id = 1, Age = 21, Email = "r@d.com", Name = "First"});
            person.Add(new Person { Id = 2, Age = 22, Email = "r1@d.com", Name = "Second"});
            person.Add(new Person { Id = 3, Age = 23, Email = "r2@d.com", Name = "Third" });

            return person;
        }

    }
}
