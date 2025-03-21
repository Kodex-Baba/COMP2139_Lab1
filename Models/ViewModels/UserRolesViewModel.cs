﻿namespace COMP2139_Lab1.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }

        public string FirstName {  get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
         
        public IEnumerable<string> Roles { get; set; }
    }
}
