namespace dotNetCoreApi.Controllers
{
    public class Restroom
    {
        private string id;
        private string name;

        public Restroom (string settedId, string settedName)
        {
            id = settedId;
            name = settedName;
        }

        public string getId()
        {
            return id;
        }

        public void setId(string settedId)
        {
            id = settedId;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string settedName)
        {
            name = settedName;
        }
    }
}