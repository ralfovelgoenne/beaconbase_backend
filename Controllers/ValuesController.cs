using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        
        static Restroom[] restrooms = { new Restroom("id1", "name1"), new Restroom("id1", "name1"), new Restroom("id1", "name1") };

        static List<QueuePosition> queue = new List<QueuePosition> 
        { 
            new QueuePosition(0),
            new QueuePosition(1),
            new QueuePosition(2),
            new QueuePosition(3),
            new QueuePosition(4),
            new QueuePosition(5),
            new QueuePosition(6),
            new QueuePosition(7),
            new QueuePosition(8),
            new QueuePosition(9)
        };  

        // GET api/values
        [HttpGet]
        public IEnumerable<Restroom> Get()
        {
            return restrooms;
        }

        // GET api/values/5
        [HttpGet("{identity}")]
        public int Get(string identity)
        {
            return this.getNextFreePositionInQueue(identity);
        }

        private int getNextFreePositionInQueue(string identity)
        {
            for(int counter = 0; counter < queue.Count; counter++) 
            {
                if (! queue[counter].isTaken())
                {
                    queue[counter].setIdentity(identity);
                    return queue[counter].getPosition();
                }
                else
                {
                    if (queue[counter].getIdentity().Equals(identity))
                    {
                        return queue[counter].getPosition();
                    }
                }
            }
            return 0;
        }

        // GET api/values/5
        /*[HttpGet("{identity}")]
        public int Get(string identity)
        {
            
        }*/

        // POST api/values
        /*[HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
