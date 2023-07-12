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

namespace Sender
{
    public partial class Sender : Form
    {
        private MessageQueue _queue;
        public Sender()
        {
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
            }
            else
            {
                MessageQueue.Create(_queue.Path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _queue.Send(textBox1.Text);
        }

        private void Sender_FormClosing(object sender, FormClosingEventArgs e)
        {
            _queue.Close();
        }
    }
}
