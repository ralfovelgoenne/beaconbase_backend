namespace dotNetCoreApi.Controllers
{
    public class QueuePosition
    {
        private int position;
        private string identity;
        private bool vip;

        public QueuePosition (int settedPosition)
        {
            position = settedPosition;
        }

        public QueuePosition (int settedPosition, string settedIdentity)
        {
            position = settedPosition;
            identity = settedIdentity;
        }

        public bool isTaken() 
        {
            return identity != null;
        }

        public bool isVip() 
        {
            return vip;
        }

        public void setVip(bool settedVip)
        {
            vip = settedVip;
        }

        public int getPosition()
        {
            return position;
        }

        public void setPosition(int settedPosition)
        {
            position = settedPosition;
        }

        public string getIdentity()
        {
            return identity;
        }

        public void setIdentity(string settedIdentity)
        {
            identity = settedIdentity;
        }
    }
}