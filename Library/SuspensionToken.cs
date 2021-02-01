using System;
using System.Threading;

namespace Library
{
    public class SuspensionToken
    {

        public bool Suspended { get; private set; } = true;
        public bool WorkThreadTaken { get; set; } = true;
        public SuspensionToken()
        {
        }


        public void Suspend()
        {
            Suspended = true;
        }

        public void Release()
        {
            Suspended = false;
        }
    }
}
