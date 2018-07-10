﻿namespace MadeLine.Api.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginResponseViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}
