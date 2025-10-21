using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccountService: IAccountService
    {
        public readonly IUserRepositry _userRepository;
        public AccountService(IUserRepositry userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> RegisterUser(RegisterModel model)
        {
           var user = await _userRepository.GetUserByEmail(model.Email);
            if(user != null)
            {
                // user already exists
                throw new Exception("User already exists, please login");
                //return Task.FromResult(false);
            }
            // hash the password
            // create a new user object
            // save to the database
            var salt = GetRandomSalt();
            var hashedPassword = HashPassword(model.Password, salt);
            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };
            await _userRepository.Add(newUser);

            return true;
        }
        public async Task<UserInfoModel> ValidateUser(string email, string password)
        {
           var user =  await _userRepository.GetUserByEmail(email);
            if(user == null)
            {
                // user does not exist
                throw new Exception("User does not exist, please register");
            }
            // hash the incoming password with the stored salt
            var hashedPassword = HashPassword(password, user.Salt);
            if(hashedPassword == user.HashedPassword)
            {
                // password is correct
                var userInfo = new UserInfoModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return userInfo;
            }
            else
            {
                // password is incorrect
                throw new Exception("Password is incorrect, please try again");
            }
        }
        private string GetRandomSalt()
        {
            // implement salt generation here
            byte[] salt = RandomNumberGenerator.GetBytes(128/8); // 128 bits
            return Convert.ToBase64String(salt);

        }
        private string HashPassword(string password, string salt)
        {
            // implement password hashing here
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2( // derive a 256-bit subkey (use HMACSHA256 with 10,000 iterations)
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)); // 256 bits
            return hashed;
        }
    }
}
