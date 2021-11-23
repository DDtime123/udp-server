using System;
using System.Windows.Forms;
using System.Threading;

namespace CSharpNetApp1
{
    public partial class ExampleWindow : Form
    {
        public ExampleWindow(){
            InitializeComponent();
        }

        private void ExampleWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        /**
         * 按下右上角退出红×后的事件
         */
        private void ExampleWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        /**
         * 发送消息
         */
        private void button2_Click(object sender, EventArgs e)
        {
            Thread UDPNetThread = new Thread(new ThreadStart(UDPNetworkFunction));
            UDPNetThread.IsBackground = true; // 设置为后台线程，才能让应用程序在主线程完成后退出
            UDPNetThread.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
