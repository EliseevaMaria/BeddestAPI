using Models;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeddestAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public UserDTO Login(string userName, string password)
        {
            UserDTO user = CtrlToDal.LogIn(userName, password);
            return user;
        }

        [HttpGet]
        // [HttpPut]
        public UserDTO Register(string newUserName, string password)
        {
            UserDTO user = CtrlToDal.Register(newUserName, password);
            return user;
        }
    }
}
