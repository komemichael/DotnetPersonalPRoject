using System;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace Silverstone.Client
{

    public class Client
    {
        public int portNumber;
        private bool stopThisThread = true;
        private int run = 0;
        bool isLoggedIn;
        public ServerMessageHandler.ServerMessageHandler mySMH;
        NetworkStream stream;
        TcpClient clientSocket;
        

        public Client(int portnumber)
        {
            portNumber = portnumber;
            mySMH = new ServerMessageHandler.ServerMessageHandler(this);
        }

        public void Connect()
        {
            try
            {
                clientSocket = new TcpClient("localhost", portNumber);
                stream = clientSocket.GetStream();
                Thread clientThread = new Thread(Run);
                clientThread.Start();
                stopThisThread = false;
                System.Windows.Forms.MessageBox.Show("Connected to Server");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot connect to Server");
            }
        }

        public bool IsConnected()
        {
            bool isConnected = true;
            if (stopThisThread == true)
            {
                return false;
            }
            return isConnected;
        }

        public void SendString(String str)
        {
            Byte[] Data = System.Text.Encoding.ASCII.GetBytes(str);
            stream.Write(Data, 0, Data.Length);
            stream.Flush();

            Byte[] EOF = { Convert.ToByte(255) };
            stream.Write(EOF, 0, EOF.Length);
            stream.Flush();
        }

        public void StopThread()
        {
            stopThisThread = true;
            try
            {
                SendString("d");
                clientSocket = null;
                stream = null;
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("Cannot close streams");
            }

        }

        public void setIsLoggedIn(bool isLoggedIn)
        {
            this.isLoggedIn = isLoggedIn;
        }

        public bool getIsLoggedIn()
        {
            return this.isLoggedIn;
        }

        //Run method for Client class thread
        public void Run()
        {
            char code;
            while (stopThisThread == false)
            {
                switch (run)
                {
                    case 0:
                        try
                        {
                            code = Convert.ToChar(stream.ReadByte());
                            mySMH.SetCommand(code);
                            code = ' ';
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show("Cannot read from Streams");
                        }
                        break;
                }
            }
        }
    }
}