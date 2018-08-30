using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Demo.Web.Framework.HtmlHelpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Web.Features.Users
{
    public class UserModel : IValidatableObject
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [HiddenInput]
        public string RandomId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Married?")]
        public bool? Married { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Range(0, 10)]
        public decimal? Rating { get; set; }

        public int Pets { get; set; }

        [CheckboxList(nameof(TagOptions))]
        public ICollection<string> Tags { get; set; } = new List<string>() {"tag1", "tag2"};

        [DisplayInTemplates(false)]
        public IEnumerable<SelectListItem> TagOptions => new List<SelectListItem>
        {
            new SelectListItem("Option 1", "1"),
            new SelectListItem("Option 2", "2") ,
            new SelectListItem("Option 3", "3")
        };

        [UIHint("YesNoRadio")]
        public bool HasCertification { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Prompt = "user@example.com")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Description = "Your password must be 8-20 characters long, contain letters and numbers, and must not contain spaces, special characters, or emoji.")]
        public string Password { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal CurrencyValue { get; set; }

        public Address HomeAddress { get; set; } = new Address();
        public Address WorkAddress { get; set; } = new Address();

        public List<Phone> Phones { get; set; } = new List<Phone>();

        public static UserModel Get(long userId)
        {
            return new UserModel
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Married = true,
                Gender = Gender.Male,
                BirthDate = new DateTime(1972, 7, 15),
                Rating = 7.1m,
                Email = "john.doe@gmail.com",
                Website = "https://peterburrell.github.io",
                Phone = "555-111-2222",
                Description = "blue collar software developer",
                CurrencyValue = 2000000m,
                Phones = { new Phone { Number = "555-222-3333", Type = PhoneType.Cell} }
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!FirstName.Equals("Peter", StringComparison.InvariantCultureIgnoreCase))
                yield return new ValidationResult("The only valid value is 'Peter'.", new[] { nameof(FirstName) });

            if (Tags == null || Tags.Count == 0)
                yield return new ValidationResult("Please select one or more Tags.", new []{ nameof(Tags) });

            yield return new ValidationResult("Something went wrong...");
        }
    }

    public class Phone
    {
        public PhoneType Type { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }
    }

    public enum PhoneType
    {
        Home,
        Office,
        Cell
    }

    public class Address
    {
        [Required]
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }
    }

    public enum Gender
    {
        Other,
        Male,
        Female
    }
}
