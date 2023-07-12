using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Reciver
{
    public partial class Reciver : Form
    {
        private MessageQueue _queue;
        public Reciver()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            InitializeQueue();

        }

        private void InitializeQueue()
        {
            _queue = new MessageQueue();

            _queue.Path = @".\private$\QueueTest123";

            if (MessageQueue.Exists(_queue.Path))
            {
                _queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                
                _queue.ReceiveCompleted += _queue_ReceiveCompleted;
                
                button1.Enabled = true;
            }
            else
            {
                this.Text = "Queue is exists";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _queue.BeginReceive();
        }

        private void _queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = _queue.EndReceive(e.AsyncResult);
                string data = msg.Body.ToString();

                listBox1.Items.Add(data);
                
                _queue.BeginReceive();
            }
            catch (MessageQueueException qexception)
            {
                listBox1.Items.Clear();

                listBox1.Items.Add(JsonConvert.SerializeObject(qexception));
            }
        }

        private void Reciver_Load(object sender, EventArgs e)
        {
            _queue.Close();
        }
    }
}
