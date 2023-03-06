using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InterfaceClient
{
    public partial class Chat : Form
    {
        private BinaryWriter _writer = new BinaryWriter(Form1._client.GetStream(), Encoding.ASCII);
        private BinaryReader _reader = new BinaryReader(Form1._client.GetStream(), Encoding.ASCII);

        public Chat()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            string data = null;

            if (data == null)
            {
                data = message.Text;
                _writer.Write(data);
                _writer.Flush();
                data = null;
            }
            //Limpia mensajes
            message.Clear();
        }

        private void readMessages(string msg)
        {
            //StringBuilder sb = new StringBuilder(receiveMessage.Text);
            //sb.AppendLine(msg);
            //receiveMessage.Text = sb.ToString();
            receiveMessage.Text += msg + "\r\n";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                string msg = _reader.ReadString();
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(readMessages), new object[] { msg });
                }
                msg = null;
            }
        }
    }
}