using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace CSharpNetApp1
{
    partial class ExampleWindow
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(337, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(301, 356);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "发送";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 204);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(203, 23);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ExampleWindow
            // 
            this.ClientSize = new System.Drawing.Size(650, 380);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "ExampleWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExampleWindow_Closing);
            this.Load += new System.EventHandler(this.ExampleWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public static ExampleWindow createWindow()
        {

            ExampleWindow wikiWindow = new ExampleWindow();
            /**
            wikiWindow.Text = "我喜欢维基教科书";
            wikiWindow.Width = 400;
            wikiWindow.Height = 300;

            Button HelloButton = new Button();
            HelloButton.Location = new Point(20, 20);
            HelloButton.Size = new Size(100, 30);
            HelloButton.Text = "点击我";
            HelloButton.Click += new System.EventHandler(WhenHelloButtonClick);
            wikiWindow.Controls.Add(HelloButton);

            TextBox Box = new TextBox();
            Box.Location = new Point(20, 60);
            Box.Size = new Size(150, 30);
            Box.Font = new Font("Arial", 12);
            Box.TextChanged += new System.EventHandler(WhenTextChanged);
            wikiWindow.Controls.Add(Box);
            */
            //createThread(1);
            //createThread(2);
            /**
            Thread NetThread = new Thread(new ThreadStart(NetworkFunction));
            NetThread.Start();
            */
            /**
            //UDP 发送消息
            Thread UDPNetThread = new Thread(new ThreadStart(UDPNetworkFunction));
            UDPNetThread.IsBackground = true; // 设置为后台线程，才能让应用程序在主线程完成后退出
            UDPNetThread.Start();
            */

            //UDP 接收消息
            Thread UDPNetAcceptThread = new Thread(UDPNetworkAcceptFunction);
            UDPNetAcceptThread.IsBackground = true; // 设置为后台线程，才能让应用程序在主线程完成后退出
            UDPNetAcceptThread.Start(wikiWindow);

            return wikiWindow;
        }

        // 窗体运行在子线程中，所以创建监听事件在此 --> below
        /*******************************************************************/
        static void WhenTextChanged(object sender, System.EventArgs e)
        {
            TextBox Box = (TextBox)sender;
            MessageBox.Show(Box.Text);
        }

        public static void createThread(int i)
        {
            Thread newThread = new Thread(ThreadFunction);
            newThread.Start(i);
        }

        static void ThreadFunction(object i)
        {
            while (true)
            {
                // 将Console重定向至TextBox
                MessageBox.Show(String.Format("Hi Thread: {0}", i));
                Thread.Sleep(2000);
            }
        }
        /*******************************************************************/
        /*****************************网络程序******************************/
        /*
         * TCP网络程序
         */
        static void NetworkFunction()
        {
            String server = "webcode.me";
            int port = 80;
            var request = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: Close\r\n\r\n";

            Byte[] requestBytes = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            IPHostEntry hostEntry = Dns.GetHostEntry(server);

            var ipe = new IPEndPoint(hostEntry.AddressList[0], port);
            using var socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipe);

            if (socket.Connected)
            {
                MessageBox.Show("连接建立");
            }
            else
            {
                MessageBox.Show("连接失败");
                return;
            }

            socket.Send(requestBytes, requestBytes.Length, 0);

            int bytes = 0;
            var sb = new StringBuilder();

            do
            {
                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes));
            } while (bytes > 0);

            MessageBox.Show(sb.ToString());
        }
        /**
         * UDP网络程序-发送
         */
        void UDPNetworkFunction()
        {
            UdpClient udpClient = new UdpClient("255.255.255.255", 5000);

            Byte[] data = Encoding.UTF8.GetBytes(this.textBox2.Text);
            udpClient.Send(data, data.Length);

            /**接收服务端回复
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Byte[] received = udpClient.Receive(ref RemoteIpEndPoint);
            string output = Encoding.UTF8.GetString(received);

            MessageBox.Show(output);
            */
            udpClient.Close();
        }
        /**
         * UDP网络程序-接收
         */
        static void UDPNetworkAcceptFunction(object window)
        {
            try
            {
                ExampleWindow mWindow = (ExampleWindow)window;
                int PORT = 5000;

                //UdpClient Server = new UdpClient(PORT);
                UdpClient Server = new UdpClient();
                Server.ExclusiveAddressUse = false;// 设置地址能重复使用
                Server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                Server.Client.Bind(new IPEndPoint(IPAddress.Any, 5000));// 绑定到5000端口

                var ResponseData = Encoding.UTF8.GetBytes("Hi");
                
                while (true)
                {
                    //var ClientEp = new IPEndPoint(IPAddress.Any, 5000);
                    var ClientEp = new IPEndPoint(IPAddress.Broadcast, 5000);
                    var ClientRequestData = Server.Receive(ref ClientEp);
                    var ClientRequest = Encoding.UTF8.GetString(ClientRequestData);

                    mWindow.textBox1.Invoke((MethodInvoker)delegate {
                        // Running on the UI thread
                        mWindow.textBox1.Text += ClientRequest+"\r\n";
                    });

                    //MessageBox.Show(string.Format("Received {0} from {1}, sending response",ClientRequest, ClientEp.Address.ToString()));
                }
                
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private TextBox textBox2;
        /*******************************************************************/
    }
}

