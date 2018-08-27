using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Features.Users
{
    public class UserModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

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

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Prompt = "user@example.com")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal CurrencyValue { get; set; }

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
                CurrencyValue = 2000000m
            };
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

    public enum Gender
    {
        Other,
        Male,
        Female
    }
}
