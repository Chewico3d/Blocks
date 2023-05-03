using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Protocol {
    //By thefault it returns an error
    internal class MessageAction {

        internal static MessageAction InvalidMessageAction() {
            return new MessageAction() { ErrorFormat = true };
        }
        internal bool ErrorFormat;
        internal string ErrorMessage;

        internal void Execute() {
            if (ErrorFormat) {
                ErrorDebug();
                return;
            }
            Execution();
        }

        protected virtual void Execution() { }

        private void ErrorDebug() {
            Console.WriteLine("Error in defining action");
            Console.WriteLine(ErrorMessage);
        }
        

    }
}
