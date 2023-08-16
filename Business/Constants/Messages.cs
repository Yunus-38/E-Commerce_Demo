using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        
        public static string AuthorizationDenied = "No authorization.";
        public static string UserRegistered = "User Registered";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin = "Successful login";
        public static string UserAlreadyExists = "User Exists";
        public static string AccessTokenCreated = "Access Token Created";
        public static string GetByMailSuccess = "Retrieved User by mail";
        
        public static string UpdateUserSuccess = "User updated";
        public static string UpdateUserError = "An error occured updating the user";
        public static string CreateUserSuccess = "User created";
        public static string GetUserSuccess = "User retrieved";
    }
}
