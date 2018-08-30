using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMessageQueue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            texboxConteudo.Text = string.Empty;

            if (textBoxNomeFila.Text != "")
            {
                if (MessageQueue.Exists($".\\Private$\\{textBoxNomeFila.Text}"))
                {
                    MessageQueue msgQ = new MessageQueue($".\\Private$\\{textBoxNomeFila.Text}");
                    msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                    var mensagem = msgQ.Receive();
                    var retonro = "título: " + mensagem.Label + "\r\n \r\n";
                    retonro += "conteúdo: " + (string)mensagem.Body;
                    texboxConteudo.Text = retonro;
                }
                else
                {
                    MessageBox.Show("A fila informada não existe");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            texboxConteudo.Text = string.Empty;
            if (textBoxNomeFila.Text != "")
            {
                if (MessageQueue.Exists($".\\Private$\\{textBoxNomeFila.Text}"))
                {
                    MessageQueue msgQ = new MessageQueue($".\\Private$\\{textBoxNomeFila.Text}");
                    msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                    var mensagem = "-----Todos títulos---- \r\n";
                    foreach (var msgs in msgQ.GetAllMessages())
                    {
                        mensagem += msgs.Label + "\r\n";
                    }
                    mensagem += "-----fim de títulos----";
                    texboxConteudo.Text = mensagem;
                }
                else
                {
                    MessageBox.Show("A fila informada não existe");
                }
            }
        }
    }
}
