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

namespace MsmqProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titulo = textBoxTituloEnviar.Text;

            string body = textBoxConteudoEnviar.Text;



            messageQueue.Send(body, titulo);
            MessageBox.Show("Enviado para a fila");
            textBoxTituloEnviar.Text = null;

            textBoxConteudoEnviar.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxTituloReceber.Text = string.Empty;

            textBoxConteudoReceber.Text = string.Empty;

            int timeout = 1000; //tempo de espera por uma nova mensagem, caso não exista nenhuma na fila

            try
            {
                var retorno = messageQueue.ReceiveById(textBoxTituloReceber.Text);

                System.Messaging.Message message = messageQueue.Receive(new TimeSpan(timeout));

                message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(String) });

                textBoxTituloReceber.Text = message.Label;

                textBoxConteudoReceber.Text = (string)message.Body;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
