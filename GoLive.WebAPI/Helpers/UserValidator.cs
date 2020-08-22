using System;
using System.Text.RegularExpressions;
using GoLive.Exceptions;
using GoLive.Models.UserDtos;
using Microsoft.Extensions.Logging;

namespace GoLive.Helpers
{
    public class UserValidator
    {
        private Regex _passwordValidator;
        private Regex _emailValidator;

        public UserValidator()
        {
            _passwordValidator = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!#%*?&]{6,20}$");
            _emailValidator = new Regex(@"^[a-z0-9]+[\._]?[a-z0-9]+[@]\w+[.]\w{2,3}$");
        }
        public void ValidateUserCreate(UserCreate model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new PasswordMismatchException();
            }
            
            Match passwordMatch = _passwordValidator.Match(model.Password);
            if (!passwordMatch.Success)
            {
                throw new WeakPasswordException();
            }
            
            if (!ValidateEmail(model.Email)) throw new InvalidEmailException();
            
            if (!ValidateUserName(model.UserName)) throw new InvalidUsernameException(
                "Username must be between 6 and 20 characters with no whitespace"
            );

            if (model.UserName.Contains(" "))
            {
                throw new InvalidUsernameException("username cannot have whitespace");
            }
        }

        public bool ValidateEmail(string email)
        {
            Match emailMatch = _emailValidator.Match(email);
            return emailMatch.Success ? true : false;
        }

        public bool ValidateUserName(string username)
        {
            if (username.Length < 5 || username.Length > 20)
            {
                return false;
            }

            if (username.Contains(" "))
            {
                return false;
            }
            return true;
        }
    }
}