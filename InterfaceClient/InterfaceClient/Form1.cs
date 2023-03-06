using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace InterfaceClient
{
    public partial class Form1 : Form
    {
        public static TcpClient _client;
        private readonly int _Port = 13000;
        private readonly string _dirIP = "localhost";
        public static string _joinUser;
        public static string _joinPassword;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _client = new TcpClient();
            try
            {
                _client.Connect(_dirIP, _Port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            BinaryWriter writer = new BinaryWriter(_client.GetStream(), Encoding.ASCII);
            BinaryReader reader = new BinaryReader(_client.GetStream(), Encoding.ASCII);
            if (_client.Connected)
            {
                writer.Write(user.Text);
                writer.Write(password.Text);
                writer.Flush();
                this.Hide();
                Chat f2 = new Chat();
                f2.Show();
            }
        }

        private void register_Click(object sender, EventArgs e)
        {
            if (newUser.Text != string.Empty && newPassword.Text != string.Empty)
            {
                StreamWriter miDoc = File.AppendText("../../../../Servidor/usuarios.txt");
                string text = newUser.Text + ";" + newPassword.Text;
                miDoc.WriteLine(text);
                miDoc.Close();
                string confirmMsg = "Usuario registrado con exito.";
                MessageBox.Show(confirmMsg);
            }
        }
    }
}
