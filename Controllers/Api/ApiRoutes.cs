using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers.Api
{
    public static class ApiRoutes
    {
        public const string ROOT = "api";

        public static class AccountRoutes
        {
            public const string ROOT = ApiRoutes.ROOT +"/account";
            public const string ALL_USERS = "";
            public const string SPECIFIC_USER = "{id}";
            public const string CREATE_USER = "";
            public const string UPDATE_USER = "{id}"; 
            public const string DELETE_USER = "{id}";

            public const string ROLE = "role";
            public const string CREATE_ROLE = ROLE; // post
            public const string ADD_USER_TO_ROLE = ROLE + "/{userId}";
            public const string GET_USER_ROLS = ROLE; //get

        }

    }
}
