///////////////////////////////////////////////////////
//NSTCPFramework
//�汾��1.0.0.1
//////////////////////////////////////////////////////
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Reflection;

namespace FlyTcpFramework
{
    /// <summary> 
    /// �ṩTcp�������ӷ���Ŀͻ����� 
    /// 
    /// ԭ��: 
    /// 1.ʹ���첽SocketͨѶ�����������һ����ͨѶ��ʽͨѶ,��ע�����������ͨ 
    /// Ѷ��ʽһ��Ҫһ��,���������ɷ������������,��������û�п˷�,��ô��byte[] 
    /// �ж����ı����ʽ 
    /// 2.֧�ִ���ǵ����ݱ��ĸ�ʽ��ʶ��,����ɴ����ݱ��ĵĴ������Ӧ���ӵ��� 
    /// �绷��. 
    /// </summary> 
    public class TcpCli
    {
        #region �ֶ�

        /// <summary> 
        /// �ͻ����������֮��ĻỰ�� 
        /// </summary> 
        private Session _session;

        /// <summary> 
        /// �ͻ����Ƿ��Ѿ����ӷ����� 
        /// </summary> 
        private bool _isConnected = false;

        /// <summary> 
        /// �������ݻ�������С64K 
        /// </summary> 
        public const int DefaultBufferSize = 4 * 1024*1024;

        /// <summary> 
        /// ���Ľ����� 
        /// </summary> 
        private DatagramResolver _resolver;

        /// <summary> 
        /// ͨѶ��ʽ��������� 
        /// </summary> 
        private Coder _coder;

        /// <summary> 
        /// �������ݻ����� 
        /// </summary> 
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];

		/// <summary>
		/// �ͻ����ļ�����·��
		/// </summary>
		private string _filePath;

        #endregion

        #region �¼�����

        //��Ҫ�����¼������յ��¼���֪ͨ������������˳�������ȡ������ 

        /// <summary> 
        /// �Ѿ����ӷ������¼� 
        /// </summary> 
        public event NetEvent ConnectedServer;

        /// <summary> 
        /// ���յ����ݱ����¼� 
        /// </summary> 
        public event NetEvent ReceivedDatagram;

        /// <summary> 
        /// ���ӶϿ��¼� 
        /// </summary> 
        public event NetEvent DisConnectedServer;
        #endregion

        #region ����

        /// <summary> 
        /// ���ؿͻ����������֮��ĻỰ���� 
        /// </summary> 
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }

        /// <summary> 
        /// ���ؿͻ����������֮�������״̬ 
        /// </summary> 
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        /// <summary> 
        /// ���ݱ��ķ����� 
        /// </summary> 
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }

        /// <summary> 
        /// ��������� 
        /// </summary> 
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }
		/// <summary> 
		/// �ͻ����ļ�����·�� 
		/// </summary> 
		public string FilePath
		{
			get
			{
				return _filePath;
			}

		}
        #endregion

        #region ���з���

        /// <summary> 
        /// Ĭ�Ϲ��캯��,ʹ��Ĭ�ϵı����ʽ 
        /// </summary> 
        public TcpCli(string saveFilePath)
        {
            _coder = new Coder(Coder.EncodingMothord.Default);
			_filePath=saveFilePath;
        }

        /// <summary> 
        /// ���캯��,ʹ��һ���ض��ı���������ʼ�� 
        /// </summary> 
        /// <param name="_coder">���ı�����</param> 
        public TcpCli(Coder coder,string saveFilePath)
        {
            _coder = coder;
			_filePath=saveFilePath;
        }

        /// <summary> 
        /// ���ӷ����� 
        /// </summary> 
        /// <param name="ip">������IP��ַ</param> 
        /// <param name="port">�������˿�</param> 
        public virtual void Connect(string ip, int port)
        {
            if (IsConnected)
            {
                //�������� 
                Debug.Assert(_session != null);

                Close();
            }

            Socket newsock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);

        }

        /// <summary> 
        /// �������ݱ��� 
        /// </summary> 
        /// <param name="datagram"></param> 
        public virtual void SendText(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("û�����ӷ����������ܷ�������"));
            }

            //��ñ��ĵı����ֽ� 
            byte[] data = _coder.GetTextBytes(datagram);

            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
                new AsyncCallback(SendDataEnd), _session.ClientSocket);
        }
        public virtual void SendFile(string FilePath)
        {
            if (FilePath.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("û�����ӷ����������ܷ�������"));
            }

            if (File.Exists(FilePath))
            {
                byte[] data = _coder.GetFileBytes(FilePath);

                _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
                    new AsyncCallback(SendDataEnd), _session.ClientSocket);
            }
            else
            {
                throw new Exception("�ļ�������");
            }
        }

        /// <summary> 
        /// �ر����� 
        /// </summary> 
        public virtual void Close()
        {
            if (!_isConnected)
            {
                return;
            }

            _session.Close();

            _session = null;

            _isConnected = false;
        }

        #endregion

        #region �ܱ�������

        /// <summary> 
        /// ���ݷ�����ɴ����� 
        /// </summary> 
        /// <param name="iar"></param> 
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            Debug.Assert(sent != 0);

        }

        /// <summary> 
        /// ����Tcp���Ӻ������ 
        /// </summary> 
        /// <param name="iar">�첽Socket</param> 
        protected virtual void Connected(IAsyncResult iar)
        {
            Socket socket = (Socket)iar.AsyncState;

            socket.EndConnect(iar);

            //�����µĻỰ 
            _session = new Session(socket);

            _isConnected = true;

            //�������ӽ����¼� 
            if (ConnectedServer != null)
            {
                ConnectedServer(this, new NetEventArgs(_session));
            }

            //�������Ӻ�Ӧ�������������� 
            _session.ClientSocket.BeginReceive(_recvDataBuffer, 0,
                DefaultBufferSize, SocketFlags.None,
                new AsyncCallback(RecvData), socket);
        }

        /// <summary> 
        /// ���ݽ��մ����� 
        /// </summary> 
        /// <param name="iar">�첽Socket</param> 
        protected virtual void RecvData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;

            try
            {
                int recv = remote.EndReceive(iar);

                //�������˳� 
                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }

                    return;
                }
 

                //������������ 
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None,
                    new AsyncCallback(RecvData), _session.ClientSocket);
            }
            catch (SocketException ex)
            {
                //�ͻ����˳� 
                if (10054 == ex.ErrorCode)
                {
                    //������ǿ�ƵĹر����ӣ�ǿ���˳� 
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                else
                {
                    throw (ex);
                }
            }
            catch (ObjectDisposedException ex)
            {
                //�����ʵ�ֲ������� 
                //������CloseSession()ʱ,��������ݽ���,�������ݽ��� 
                //�����л����int recv = client.EndReceive(iar); 
                //�ͷ�����CloseSession()�Ѿ����õĶ��� 
                //����������ʵ�ַ���Ҳ�����˴��ŵ�. 
                if (ex != null)
                {
                    ex = null;
                    //DoNothing; 
                }
            }

        }

        #endregion


    } 
}
