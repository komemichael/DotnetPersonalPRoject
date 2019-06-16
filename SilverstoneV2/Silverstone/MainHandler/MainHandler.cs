using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Silverstone.MainHandler
{
    class MainHandler
    {
        Form1 gui;
        Client.Client client;
        String command = "";
        System.Configuration.Configuration config;
        RegistryKey key;
        String CurrentUser;

        public MainHandler(Form1 gui, Client.Client client)
        {
            this.gui = gui;
            this.client = client;
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitConfig();
        }

        public void SetCommand(String command)
        {
            this.command = command;
        }

        public void InitConfig()
        {
            bool silverstoneExist = false;
            String[] subkeys = Registry.CurrentUser.OpenSubKey(name: "Software", writable: true).GetSubKeyNames();
            foreach (var subkey in subkeys)
            {
                if (subkey.Equals("Silverstone"))
                {
                    silverstoneExist = true;
                }
                Console.WriteLine(subkey);
            }

            if (!silverstoneExist)
            {
                key = Registry.CurrentUser.OpenSubKey(name: "Software", writable: true).CreateSubKey("Silverstone");
            }
            else
            {
                key = Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Silverstone", true);
            }
        }

        public void EditConfig()
        {
            //Use key.open sub key
            Random rand = new Random();
            int randomint = rand.Next();
            String name = $"name{randomint}";
            String value = $"value{randomint}";
            
            key.SetValue(name, value);//key.Close();
        }

        public void checkLoggedIn()
        {
            Thread.Sleep(1000);
            if (client.getIsLoggedIn() == true)
            {
                MessageBox.Show("You are now  logged in");
                key.DeleteValue("user", true);
                key.SetValue("user", CurrentUser);
                gui.Set_Logged_in();
            }
        }


        public void Run()
        {
            switch (command)
            {
                case "connect":
                    client.Connect();
                    if (client.IsConnected())
                    {
                        gui.Update("Connected to Server");
                    }
                    else
                    {
                        gui.Update("Not Connected to Server");
                    }
                    break;
                case "disconnect":
                    if (client.IsConnected())
                    {
                        client.StopThread();
                        if (client.IsConnected())
                        {
                            gui.Update("Not disconnected");
                        }
                        else
                        {
                            gui.Update("Disconnected from Server");
                        }
                    }
                    else
                    {
                        gui.Update("Not Connected to Server");
                    }
                    break;
                case "test":
                    gui.Update("Testing This ...");
                    break;
                case "t":
                    if (client.IsConnected())
                    {
                        client.SendString(command);
                        System.Windows.Forms.MessageBox.Show("Sent time request to server");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry Not Connected to server");
                    }
                    break;
                case "login":
                    if (client.IsConnected())
                    {
                        String username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username", "", "", 500, 300);
                        String password = Microsoft.VisualBasic.Interaction.InputBox("Enter Password", "", "", 500, 300);
                        String sendString = $"validate {username}&{password}";

                        CurrentUser = username;
                        client.SendString(sendString);
                        Thread.Sleep(1000);
                        checkLoggedIn();
                    }
                    else
                    {
                        gui.Update("Not Connected to Server");
                    }
                    break;
                default:
                    EditConfig();
                    System.Windows.Forms.MessageBox.Show("Hi" + command);
                    break;
            }
        }
    }
}
