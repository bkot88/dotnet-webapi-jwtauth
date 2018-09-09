
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TokenDemo.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }

    public class BasicAuthModel
    {
        [Required(ErrorMessage = "Username & Password required!")]
        [FromHeader(Name = "Authorization")]
        public string Authorization { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            if (Authorization.StartsWith("Basic"))
            {
                return true;
            }
            return false;
        }

        public void Parse()
        {
            string encoded = Authorization.Substring("Basic ".Length).Trim();
            string UserPass = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            string[] splitToken = UserPass.Split(":");
            Username = splitToken[0];
            Password = splitToken[1];
        }

        public string GetUserName()
        {
            return Username;
        }

        public string GetPassword()
        {
            return Password;
        }
    }
}
