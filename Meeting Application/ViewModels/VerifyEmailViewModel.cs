﻿namespace Meeting_Application.ViewModels;
using System.ComponentModel.DataAnnotations;


    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }

