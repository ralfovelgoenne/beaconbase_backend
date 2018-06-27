using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class PositionController : Controller
    {
        //Init a new database connector
        DBConnector db = new DBConnector();

        // GET all persons and rooms
        [HttpGet]
        public string Get()
        {
            db.Connect();
            StringContent result = db.ExecuteQuery(DBQueries.GetAllUsers());
            return result.ReadAsStringAsync().Result;
        }

        //POST person position
        [HttpPost]
        public string Post(string userId, string beaconId)
        {
            db.Connect();
            db.ExecuteNonQuery(DBQueries.RegisterUserInRoom(userId, beaconId));
            return string.Format("Echo: userId= {0}, beaconId={1}", userId, beaconId);
        }

        // DELETE person registration
        [HttpDelete]
        public string Delete(string userId)
        {
            db.Connect();
            db.ExecuteNonQuery(DBQueries.DeregisterUserInRoom(userId));
            return string.Format("Echo: userId= {0}", userId);
        }
    }
}
