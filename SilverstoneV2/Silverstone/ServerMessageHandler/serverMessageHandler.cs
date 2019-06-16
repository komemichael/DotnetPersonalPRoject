using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Silverstone.ServerMessageHandler
{
    public class ServerMessageHandler
    {
        Client.Client myClient;
        String command;
        int run = 0;

        public ServerMessageHandler(Client.Client client)
        {
            myClient = client;
        }

        public void SetCommand(char cmd)
        {
            if (cmd != 0xFF)
            {
                command = command + cmd.ToString();
            }
            else
            {
                Thread smh = new Thread(Run);
                smh.Start();
            }
        }

        public void Run()
        {
            if (command.Contains(":"))
            {
                run = 1;
            }

            if (command.Contains("validate"))
            {
                run = 2;
            }
            switch (run)
            {
                case 1:
                    System.Windows.Forms.MessageBox.Show("The time is " + command);
                    break;
                case 2:
                    System.Windows.Forms.MessageBox.Show("Setting logged in to be true");
                    myClient.setIsLoggedIn(true);
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("Just a random String");
                    break;
            }
            this.command = string.Empty;
            run = 0;
        }
    }
}
