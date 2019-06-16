using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Silverstone
{
    public partial class Form1 : Form
    {

        MainHandler.MainHandler handler;
        Client.Client client;
        //bool isLoggedIn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'message.DirectMessage' table. You can move, or remove it, as needed.
            this.directMessageTableAdapter.Fill(this.message.DirectMessage);
            // TODO: This line of code loads data into the 'util._Util' table. You can move, or remove it, as needed.
            this.utilTableAdapter.Fill(this.util._Util);
            // TODO: This line of code loads data into the 'util._Util' table. You can move, or remove it, as needed.
            //this.utilTableAdapter1.Fill(this.util._Util);
            // TODO: This line of code loads data into the 'database1DataSet.Util' table. You can move, or remove it, as needed.
            //this.utilTableAdapter.Fill(this.database1DataSet.Util);
            client = new Client.Client(5555);
            handler = new MainHandler.MainHandler(this, client);
            //isLoggedIn = false;
            //logged_in_panel.Visible = false;
        }

        public bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;
            return true;
        }

        public void Update(String str)
        {
            if (ControlInvokeRequired(servermsg, () => Update(str))) return;
            servermsg.Text = str;
        }

        public void Set_Logged_in()
        {
            if (ControlInvokeRequired(logged_in_panel, () => Set_Logged_in())) return;
            logged_in_panel.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            handler.SetCommand("connect");
            Thread connectThread = new Thread(handler.Run);
            connectThread.Start();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handler.SetCommand("disconnect");
            Thread connectThread = new Thread(handler.Run);
            connectThread.Start();
        }

        private void debug_Click(object sender, EventArgs e)
        {
            handler.SetCommand(servermsg.Text);
            Thread connectThread = new Thread(handler.Run);
            connectThread.Start();
        }
        

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //helpProvider1.GetHelpNavigator(System.Windows.Forms);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void go_to_url_btn_Click(object sender, EventArgs e)
        {
            String url = go_to_url_textbox.Text;
            webBrowser.Navigate(url);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void message_Send_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                MessageBox.Show("Data Saved","Information",MessageBoxButtons.OK);
                directMessageBindingSource.AddNew();
                fromComboBox.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void message_Reset_Btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                directMessageBindingSource.RemoveCurrent();
                fromComboBox.Select();
            }
            else if (result == DialogResult.No)
            {
                //code for No
            }
        }

        private void logInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            handler.SetCommand("login");
            Thread loginThread = new Thread(handler.Run);
            loginThread.Start();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //utilBindingSource.EndEdit();
            //tableAdapterManager.UpdateAll();
           
        }
     
    }
}
