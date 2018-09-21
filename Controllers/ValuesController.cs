using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotNetCoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        
        static Restroom[] restrooms = { new Restroom("id1", "name1"), new Restroom("id1", "name1"), new Restroom("id1", "name1") };

        static int approxWaitTimePerPosition = 3;

        static List<QueuePosition> queue = new List<QueuePosition> 
        { 
            new QueuePosition(0),
            new QueuePosition(1)
        };  

        // GET api/values
        [HttpGet]
        public IEnumerable<Restroom> GetRooms()
        {
            return restrooms;
        }

        // GET api/values/5
        [HttpGet("{identity}")]
        public int GetCurrentPosition(string identity)
        {
            return this.getCurrentPositionInQueue(identity);
        }

        [HttpGet("{identity}")]
        public int GetCurrentWaitTime(string identity)
        {
            int position = this.getCurrentPositionInQueue(identity);
            return position * approxWaitTimePerPosition;
        }

        // GET api/values/5
        [HttpPut("{identity}")]
        public int CancelPosition(string identity)
        {
            return this.cancelPositionInQueue(identity);
        }

        // GET api/values/5
        [HttpGet("{identity}")]
        public int GetNewPosition(string identity)
        {
            return this.getNextFreePositionInQueue(identity);
        }

        // GET api/values/5
        [HttpGet("{identity}")]
        public int GetQueueLength(string identity)
        {
            return queue.Count;
        }

        private int getNextFreePositionInQueue(string identity)
        {
            int count = 0;
            for(int counter = 0; counter < queue.Count; counter++) 
            {
                count++;
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

            QueuePosition position = new QueuePosition(count);
            position.setIdentity(identity);
            queue.Add(position);
            return position.getPosition();
        }

        private int cancelPositionInQueue(string identity)
        {
            for(int counter = 0; counter < queue.Count; counter++) 
            {
                if (queue[counter].getIdentity().Equals(identity))
                {
                        queue[counter].setIdentity(null);
                        return 0;
                }
            }

            return 1;
        }

        private int getCurrentPositionInQueue(string identity)
        {
            for(int counter = 0; counter < queue.Count; counter++) 
            {
                if (queue[counter].getIdentity().Equals(identity))
                {
                        return queue[counter].getPosition();
                }
            }

            return 1000;
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
