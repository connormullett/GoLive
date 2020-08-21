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
            
            Match emailMatch = _emailValidator.Match(model.Email);
            if (!emailMatch.Success)
            {
                throw new InvalidEmailException();
            }
            
            if (model.UserName.Length < 5 || model.UserName.Length > 20)
            {
                throw new InvalidUsernameException("username must be 5 - 20 characters");
            }

            if (model.UserName.Contains(" "))
            {
                throw new InvalidUsernameException("username cannot have whitespace");
            }
        }
    }
}