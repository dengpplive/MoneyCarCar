using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Models;
using MoneyCarCar.DAL;

namespace MoneyCarCar.DataApi.Controllers
{
    public class TestController : ApiController
    {
        SystemUsersOper oper = new SystemUsersOper();
        [HttpGet]
        public IEnumerable<SystemUsers> All()
        {
            return new SystemUsers[] { new SystemUsers(), new SystemUsers() };
        }

        [HttpGet]
        public SystemUsers Time()
        {
            SystemUsers user = new SystemUsers();

            return user;
        }

        [HttpGet]
        public string GetOne(int id)
        {
            return "value";
        }
        [HttpGet]
        public string GetOne1(string value)
        {
            return value;
        }

        [HttpPost]
        public SystemUsers PostUser(SystemUsers value)
        {
            return value;
        }

        [HttpPut]
        public void Put(int id, SystemUsers value)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }


        [HttpGet]
        public List<SystemUsers> GetAllUser()
        {
            return oper.GetList("");
        }

        [HttpPost]
        public int Add(SystemUsers model)
        {
            return oper.Add(model);
        }
        [HttpGet]
        public SystemLog TestDatabase()
        {
            //SystemLogOper oper = new SystemLogOper();
            //return oper.GetModel();
            return null;
        }
    }
}
