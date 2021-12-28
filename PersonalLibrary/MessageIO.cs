using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
    public class MessageIO
    { // (Low) - Make the library useable across other UIs /palts
        public delegate void AlertUser(string message); // (High) - Work on accessibility modifier understanding. 

        public AlertUser Message { get;  set; }

        public MessageIO()
        {}

        public MessageIO(AlertUser alertUserMethod)
        {
            Message = alertUserMethod;
        }

        public void SendMessage(string message)
        {
            Message(message);
        }
    }
}
