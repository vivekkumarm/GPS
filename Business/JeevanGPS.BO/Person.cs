using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;


namespace JeevanGPS.BO
{
   //IvalidatableObject
   //ValidationAttribute
   //Annotations



    [Validator(typeof(PersonValidator))]
    public class Person
    {      
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            // Annotations.
            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.Name).NotNull();            

            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Age).InclusiveBetween(18, 60);         


            // For busines validations.
            RuleSet("Names", () =>
          {
              RuleFor(x => x.Email).EmailAddress();
              RuleFor(x => x.Name).Length(0, 10);
          });

          
        }
     
    }
}
