using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyBox.Business.DTO
{
    public class RegisterUserDTO
    {
        public string Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

    }
}