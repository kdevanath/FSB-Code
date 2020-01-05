using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace AlgMgrWindow
{
    public partial class AlgMgrForm : Form
    {
        private Socket _listener;
        private int _port;
        private LoginForm _login;
        
        public AsyncCallback pfnWorkerCallBack;
        private Hashtable _clientConnTable;
        private Hashtable _nameToRowIndexTable;
        private Hashtable _updatabaleCells;
        public DataTable _settingsTable;

        private AlgParams _paramsForm;
        private SpreadViewer _spreadViewer;

        public delegate void UpdateGridViewCallback(string text, int rowIndex);       
        public delegate void UpdateGVConnectionCallback(string clientId);
        public delegate void UpdateViewsDelegate(string text, int rowIndex,int colIndex);
        
        public class cellUpdate
        {
            public cellUpdate(int i, UpdateViewsDelegate d)
            {
                colIndex = i;
                updDelegate = d;
            }
            public int colIndex;
            public UpdateViewsDelegate updDelegate;
        }

        private Color _startColor;
        private Color _stopColor;

        public AlgMgrForm()
        {
            InitializeComponent();
            
            _startColor = Color.YellowGreen;
            _stopColor = Color.Crimson;

            _login = new LoginForm();
            _login.ShowDialog();
       
            _nameToRowIndexTable = Hashtable.Synchronized(new Hashtable());
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            //****** NEW******
            //this.SetStyle(
            //ControlStyles.UserPaint |
            //ControlStyles.AllPaintingInWmPaint |
            //ControlStyles.OptimizedDoubleBuffer, true);

            //this.UpdateStyles();
            
            DP0.Tag = "DP0";
            MA1.Tag = "MA1";
            MA2.Tag = "MA2";
            TBID.Tag = "TBID";
            TASK.Tag = "TASK";
            OURBID.Tag = "OURBID";
            OURASK.Tag = "OURASK";
            BID.Tag = "CURBID";
            ASK.Tag = "CURASK";
            Mfr.Tag = "MFR";

            TakeLiq.Tag = "TAKE_LIQ";
            TakeLiq.TrueValue = true;
            TakeLiq.FalseValue = false;
            TakeLiq.ValueType = typeof(bool);

            WorkLiq.Tag = "WORK_LIQ";
            WorkLiq.TrueValue = true;
            WorkLiq.FalseValue = false;
            WorkLiq.ValueType = typeof(bool);

            CloseOnly.Tag = "CLOSE_ONLY";
            CloseOnly.TrueValue = true;
            CloseOnly.FalseValue = false;
            CloseOnly.ValueType = typeof(bool);

            BSFLAG.Tag = "BS_FLAG";
            BSFLAG.TrueValue = true;
            BSFLAG.FalseValue = false;
            BSFLAG.ValueType = typeof(bool);

            DELTA_CLOSE.Tag = "DELTA_CLOSE";
            DELTA_CLOSE.TrueValue = true;
            DELTA_CLOSE.FalseValue = false;
            DELTA_CLOSE.ValueType = typeof(bool);

            BASE_HTB.Tag = "BASE_HTB";
            BASE_HTB.TrueValue = true;
            BASE_HTB.FalseValue = false;
            BASE_HTB.ValueType = typeof(bool);

            HEDGE_HTB.Tag = "HEDGE_HTB";
            HEDGE_HTB.TrueValue = true;
            HEDGE_HTB.FalseValue = false;
            HEDGE_HTB.ValueType = typeof(bool);

            //Names have to match the field ids coming from the client.
            // I wish there was an easy eay to share a header file
            _updatabaleCells = Hashtable.Synchronized(new Hashtable());
            _updatabaleCells.Add("dp0", new cellUpdate(DP0.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("ma1", new cellUpdate(MA1.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("ma2", new cellUpdate(MA2.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("obid", new cellUpdate(OURBID.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("oask", new cellUpdate(OURASK.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("cask", new cellUpdate(ASK.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("cbid", new cellUpdate(BID.Index,UpdateGridViewMethod));
            _updatabaleCells.Add("bbid", new cellUpdate(TBID.Index, UpdateGridViewMethod));
            _updatabaleCells.Add("bask", new cellUpdate(TASK.Index, UpdateGridViewMethod));
            _updatabaleCells.Add("mfr", new cellUpdate(Mfr.Index, UpdateGridViewMethod));

            _clientConnTable = Hashtable.Synchronized(new Hashtable());

            //Initialize datagrid view from an xml file ?
            _paramsForm = new AlgParams(_login.ShareHost,_login.Test);
            LoadFromAlgParams();
            //*******NEW***
            //Application.DoEvents();
            //MessageBox.Show("Staring Listener");
            
            _spreadViewer = new SpreadViewer(_login.Broker);
            _spreadViewer.Show();
            _port = _login.Port;
            StartListeningSocket();
            //Class1.DoubleBuffered(dataGridView1, true);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadFromAlgParams()
        {
            _settingsTable = new DataTable("AlgSettingsTable");
            for (int d = 0; d < dataGridView1.ColumnCount; d++)
            {
                try
                {
                    _settingsTable.Columns.Add(dataGridView1.Columns[d].Name, typeof(System.String));
                }
                catch (DuplicateNameException)
                {
                }
            }
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DataRow row; ;
            int numRows = _paramsForm._settingsTable.Rows.Count;
            for (int i = 0; i < numRows; i++)
            {
                //settingsrow = _settingsTable.NewRow();
                row = _paramsForm._settingsTable.Rows[i];
                _nameToRowIndexTable.Add(row[0], i);
                dataGridView1.Rows.Add(row[0], 0);
                _settingsTable.Rows.Add(row[0]);
                DataGridViewBand band = dataGridView1.Rows[i]; //.Cells[Start_Stop.Index].Tag = 0;
                //dataGridView1.RowsDefaultCellStyle.BackColor; .
                
                dataGridView1.Rows[i].Cells[Start_Stop.Index].Tag = 0;
                dataGridView1.Rows[i].Cells[Start_Stop.Index].Value = "STOP";
                dataGridView1.Rows[i].Cells[TakeLiq.Index].Value = false;
                dataGridView1.Rows[i].Cells[WorkLiq.Index].Value = false;
                dataGridView1.Rows[i].Cells[CloseOnly.Index].Value = false;
                dataGridView1.Rows[i].Cells[BSFLAG.Index].Value = true;
                dataGridView1.Rows[i].Cells[DELTA_CLOSE.Index].Value = false;
                //band.DefaultCellStyle.BackColor = dataGridView1.RowsDefaultCellStyle.BackColor;
                band.Tag = dataGridView1.RowsDefaultCellStyle.BackColor;
                Console.WriteLine(" the color is" + band.DefaultCellStyle.BackColor.ToString() + band.Tag.ToString());
            }
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void printvalues()
        {
            foreach (DictionaryEntry de in _nameToRowIndexTable)
                MessageBox.Show(" _nameToRowIndexTable " + de.Key.ToString());

            foreach (DictionaryEntry de in _clientConnTable)
                MessageBox.Show(" _clientConnTable " + de.Key.ToString());
        }

        private void SaveData()
        {
            _paramsForm.SaveDataToXml();
            if (_settingsTable.Rows.Count > 0)
                _settingsTable.Clear();

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                try
                {
                    _settingsTable.Columns.Add(dataGridView1.Columns[i].Name, typeof(System.String));
                }
                catch (DuplicateNameException)
                {
                }
            }

            DataRow myRow;
            int cols = dataGridView1.Columns.Count;
            int rowIndex = 0;
            foreach (DataGridViewRow drow in this.dataGridView1.Rows)
            {
                if (drow.Cells[0].Value == null)
                    return;
                myRow = _settingsTable.NewRow();
                for (int i = 0; i < cols - 1; i++)
                {
                    if (dataGridView1.Columns[i].Name.Equals("Save") ||
                        dataGridView1.Columns[i].Name.Equals("Start&Stop"))
                        continue;
                    if (drow.Cells[i].Value != null)
                        myRow[i] = drow.Cells[i].Value;
                }
                rowIndex++;
                _settingsTable.Rows.Add(myRow);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 ||
                    (e.ColumnIndex != dataGridView1.Columns["Save"].Index &&
                e.ColumnIndex != dataGridView1.Columns[Start_Stop.Index].Index ))
                    return;
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Unknown Spreadname");
                    return;
                }
                DataGridViewBand band = dataGridView1.Rows[e.RowIndex];
                if (e.ColumnIndex == dataGridView1.Columns[Start_Stop.Index].Index)
                {
                    if (dataGridView1[TakeLiq.Index, e.RowIndex].Value == null &&
                        dataGridView1[WorkLiq.Index, e.RowIndex].Value == null &&
                        dataGridView1[CloseOnly.Index, e.RowIndex].Value == null)
                    {
                        MessageBox.Show(" Check one of the trade modes(null)");
                        DataGridViewButtonCell stopcell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[Start_Stop.Index];
                        stopcell.Value = "STOP";
                        band.DefaultCellStyle.BackColor = (Color)band.Tag;
                        //band.DefaultCellStyle.BackColor = Color.DarkSlateGray;
                        return;
                    }
                    DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[Start_Stop.Index];
                    //MessageBox.Show(cell.Tag.ToString() + cell.Value.ToString());
                    int stopValue = 0;
                    if (cell.Tag != null)
                        stopValue = (int)cell.Tag;
                    else
                        stopValue = 1;

                    if (stopValue == 0)
                    {
                        if (dataGridView1[TakeLiq.Index, e.RowIndex].Value.ToString() == "True" ||
                            dataGridView1[WorkLiq.Index, e.RowIndex].Value.ToString() == "True" ||
                            dataGridView1[CloseOnly.Index, e.RowIndex].Value.ToString() == "True")
                        {
                            cell.Tag = 1;
                            band.DefaultCellStyle.BackColor = _startColor; //Color.GreenYellow;
                            cell.Value = "START";
                        }
                        else
                        {
                            band.DefaultCellStyle.BackColor = (Color)band.Tag;
                            MessageBox.Show(" Check one of the trade modes");
                            return;
                        }
                    }
                    else
                    {
                        cell.Tag = 0;
                        band.DefaultCellStyle.BackColor = _stopColor;//Color.Crimson;
                        cell.Value = "STOP";
                        Console.WriteLine("Hit stop : stopValue != 0 " + band.DefaultCellStyle.BackColor.ToString());
                        //dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = _stopColor;// Color.Red;                     
                        //cell.Style.BackColor = Color.IndianRed;
                        //cell.Style.ForeColor = Color.Black;

                    }
                    if (!SendStopStart(cell.Value.ToString(), e.RowIndex))
                    {
                        cell.Value = "STOP";
                        cell.Tag = 0;
                        band.DefaultCellStyle.BackColor = _stopColor; //Color.Crimson;//(Color)band.Tag;
                       
                        Console.WriteLine("Hit stop/Problem send stop " + band.DefaultCellStyle.BackColor.ToString());
                        //dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = _stopColor;// Color.Red;
                    }
                }
                if (e.ColumnIndex == dataGridView1.Columns["Save"].Index)
                {
                    DataGridViewButtonCell stopCell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[Start_Stop.Index];
                    if (stopCell.Tag == null)
                    {
                        stopCell.Tag = 0;
                        stopCell.Value = "STOP";
                        //band.DefaultCellStyle.BackColor = (Color)band.Tag;

                    }
                    if ((int)stopCell.Tag != 0)
                    {
                        stopCell.Value = "STOP";
                        stopCell.Tag = 0;
                        SendStopStart("STOP", e.RowIndex);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = _stopColor;// Color.Red;
                    }
                    DataGridViewCell cell0 = dataGridView1.Rows[e.RowIndex].Cells[0];
                    if (cell0.Value == null)
                    {
                        MessageBox.Show("Enter some data");
                        return;
                    }
                    if (!_nameToRowIndexTable.ContainsKey(cell0.Value.ToString()))
                    {
                        _nameToRowIndexTable.Add(cell0.Value.ToString(), e.RowIndex);
                    }
                    if (_paramsForm.Visible)
                        _paramsForm.Hide();

                    SaveData();
                    SendData(e.RowIndex);
                }
               // Console.WriteLine("End: " + band.DefaultCellStyle.BackColor.ToString());
                //dataGridView1.Refresh();
            }
            catch (System.ArgumentOutOfRangeException i)
            {
                MessageBox.Show(i.Message);
            }
        }

        String ParseMessage2(String msg, String fieldtype)
        {
            String spreadname="";
            try
            {
                XmlReader reader = XmlReader.Create(new System.IO.StringReader(msg));

                if (reader.MoveToContent() == XmlNodeType.Element &&
                    reader.Name == fieldtype)
                {
                    spreadname = reader.GetAttribute("spread");
                    Console.WriteLine("Found connection  name " + spreadname);
                }
            }
            catch (XmlException x)
            {
                Console.WriteLine(x.Message);
                Console.WriteLine(" Exception while reading " + fieldtype + ":" + msg);
            }
            return spreadname;
        }

        String ParseMessage(String msg, String fieldtype)
        {
            char[] delimiterChars = { '=', ';' };
            int i = 0;
            string fieldValue = "";
            string[] fields = msg.Split(delimiterChars);
            foreach (string s in fields)
            {
                if (s.Equals(fieldtype))
                {
                    fieldValue = fields[i + 1];
                    break;
                }
                i++;

            }
            return fieldValue;
        }

        String GetIP()
        {
            String strHostName = Dns.GetHostName();

            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // Grab the first IP addresses
            String IPStr = "";
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                IPStr = ipaddress.ToString();
                return IPStr;
            }
            return IPStr;
        }
        //private String _remoteHost;
        private void StartListeningSocket()
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                String strHostName = Dns.GetHostName();
                // Find host by name           
                IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
                IPAddress hostIP = iphostentry.AddressList[0];
                for (int index = 0; index < iphostentry.AddressList.Length; index++)
                {
                    hostIP = iphostentry.AddressList[index];
                    if (hostIP.ToString().StartsWith("10."))
                        break;
                }
                IPEndPoint addr = new IPEndPoint(hostIP, _port);
               // MessageBox.Show(addr.ToString(),hostIP.ToString());
                _listener.Bind(addr);
                _listener.Listen(100); // "10" Que incoming connections and put them on wait untill the get their Turn to be processed by server Socket
                _listener.BeginAccept(new AsyncCallback(OnClientConnect), _listener);
                //System.Diagnostics.Trace.WriteLine("Started listening");
                //MessageBox.Show("Started Listening");
                toolStripStatusLabel1.Text = "Listening " + addr.ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }

        }

        void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                // Add the callback code here.
                // Here we complete/end the BeginAccept() asynchronous call
                // by calling EndAccept() - which returns the reference to
                // a new Socket object
                //MessageBox.Show("OnClientConnect");
                byte[] buffer = new byte[50];
                Socket workerSocket = _listener.EndAccept(asyn);
                //MessageBox.Show("OnClientConnect");
                int bytesRcvd = workerSocket.Receive(buffer);
                Console.WriteLine(" a new connection " + workerSocket.ReceiveBufferSize);
                String clientMsg = Encoding.UTF8.GetString(buffer);
                //MessageBox.Show(" Rcvd " + clientMsg);
                String clientId = ParseMessage2(clientMsg, "CONNECT");
                //MessageBox.Show(" Rcvd " + clientId);
                //System.Diagnostics.Debug.WriteLine("Rcvd. message from " + workerSocket.ToString() + "::" + clientId);
                // Add the workerSocket reference to our map

                // Let the worker Socket do the further processing for the 
                // just connected client
                int rowIndex = -1;
                if (_nameToRowIndexTable.ContainsKey(clientId))
                {
                    rowIndex = (int)_nameToRowIndexTable[clientId];
                    dataGridView1.Rows[rowIndex].Tag = Color.CadetBlue;
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.CadetBlue;
                    //MessageBox.Show("Found " + clientId + rowIndex.ToString());
                }
                else
                {
                    //it does not exist
                    throw new Exception(clientId + ": Not found ");
                }

                try
                {
                    _clientConnTable[clientId] = workerSocket;
                }
                catch (ArgumentNullException)
                {
                    _clientConnTable.Add(clientId, workerSocket);
                }

                WaitForData(workerSocket, clientId, rowIndex);

                // Since the main Socket is now free, it can go back and wait for
                // other clients who are attempting to connect
                _listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debug.WriteLine("\n OnClientConnection: Socket has been closed\n");
                MessageBox.Show("OnClientConnection: Socket has been closed");

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }

        }

        public class SocketPacket
        {
            // Constructor which takes a Socket and a client number
            public SocketPacket(System.Net.Sockets.Socket socket, string clientNumber, int rowIndex)
            {
                _currentSocket = socket;
                _clientNumber = clientNumber;
                _rowIndex = rowIndex;
            }
            public System.Net.Sockets.Socket _currentSocket;
            public string _clientNumber;
            public int _rowIndex;
            // Buffer to store the data sent by the client
            public byte[] dataBuffer = new byte[8192];
        }
        // Start waiting for data from the client
        public void WaitForData(System.Net.Sockets.Socket soc, string clientNumber,int rowIndex)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket(soc, clientNumber, rowIndex);

                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                    theSocPkt.dataBuffer.Length,
                    SocketFlags.None,
                    pfnWorkerCallBack,
                    theSocPkt);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debug.WriteLine("\n OnClientConnection: Socket has been closed\n");
                MessageBox.Show("OnClientConnection: Socket has been closed");
                UpdateGVClientDisconnection(clientNumber);

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (System.ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        public bool SendStopStart(string request, int rowIndex)
        {
            request += "=1";
            int reqLen = request.Length;
            try
            {
                object[] aRow = _settingsTable.Rows[rowIndex].ItemArray;
                // convert string length value to network order
                int reqLenH2N = IPAddress.HostToNetworkOrder(reqLen);

                // get string length value into a byte array -- for use with
                // Socket.Send
                byte[] reqLenArray = BitConverter.GetBytes(reqLenH2N);

                byte[] dataArray = Encoding.ASCII.GetBytes(request);

                if (_clientConnTable.ContainsKey(aRow[0]))
                {
                    try
                    {
                        Socket soc = (Socket)_clientConnTable[aRow[0]];
                        int bytesSent = soc.Send(reqLenArray, 4, System.Net.Sockets.SocketFlags.None);
                        soc.Send(dataArray, reqLen, System.Net.Sockets.SocketFlags.None);

                    }
                    catch (ObjectDisposedException)
                    {
                        System.Diagnostics.Debug.WriteLine("\n OnClientConnection: Socket has been closed\n");
                        MessageBox.Show("OnClientConnection: Socket has been closed");
                        UpdateGVClientDisconnection(aRow[0].ToString());

                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show(" Problem Sending Start/Stop" + se.Message);
                    }
                }
                else
                {
                    MessageBox.Show(" No connection yet " + aRow[0].ToString());
                    //dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = (Color)dataGridView1.Rows[rowIndex].Tag;
                    //Console.WriteLine(dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor.ToString());
                    return false;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
            }
            return true;
        }
        public void SendData(int rowIndex)
        {
            object[] aRow = _settingsTable.Rows[rowIndex].ItemArray;
            string message;
            if (aRow[TakeLiq.Index].ToString().Length != 0)
            {
                if (aRow[TakeLiq.Index].Equals("True"))
                    message = TakeLiq.Tag.ToString() + "=" + 1 + ";";
                else
                    message = TakeLiq.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message = TakeLiq.Tag.ToString() + "=" + 0 + ";";
            }
            if (aRow[WorkLiq.Index].ToString().Length != 0)
            {
                if (aRow[WorkLiq.Index].Equals("True"))
                    message += WorkLiq.Tag.ToString() + "=" + 1 + ";";
                else
                    message += WorkLiq.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message += WorkLiq.Tag.ToString() + "=" + 0 + ";";
            }

            if (aRow[CloseOnly.Index].ToString().Length != 0)
            {
                if (aRow[CloseOnly.Index].Equals("True"))
                    message += CloseOnly.Tag.ToString() + "=" + 1 + ";";
                else
                    message += CloseOnly.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message += CloseOnly.Tag.ToString() + "=" + 0 + ";";
            }

            if (aRow[BSFLAG.Index].ToString().Length != 0)
            {
                if (aRow[BSFLAG.Index].Equals("True"))
                    message += BSFLAG.Tag.ToString() + "=" + 1 + ";";
                else
                    message += BSFLAG.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message += BSFLAG.Tag.ToString() + "=" + 0 + ";";
            }

            if (aRow[DELTA_CLOSE.Index].ToString().Length != 0)
            {
                if (aRow[DELTA_CLOSE.Index].Equals("True"))
                    message += DELTA_CLOSE.Tag.ToString() + "=" + 1 + ";";
                else
                    message += DELTA_CLOSE.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message += DELTA_CLOSE.Tag.ToString() + "=" + 0 + ";";
            }

            if (aRow[BASE_HTB.Index].ToString().Length != 0)
            {
                if (aRow[BASE_HTB.Index].Equals("True"))
                    message += BASE_HTB.Tag.ToString() + "=" + 1 + ";";
                else
                    message += BASE_HTB.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message += BASE_HTB.Tag.ToString() + "=" + 0 + ";";
            }

            if (aRow[HEDGE_HTB.Index].ToString().Length != 0)
            {
                if (aRow[HEDGE_HTB.Index].Equals("True"))
                    message += HEDGE_HTB.Tag.ToString() + "=" + 1 + ";";
                else
                    message += HEDGE_HTB.Tag.ToString() + "=" + 0 + ";";
            }
            else
            {
                message += HEDGE_HTB.Tag.ToString() + "=" + 0 + ";";
            }

            //MessageBox.Show(message);
            _paramsForm.GetRowValue(aRow[0].ToString(), ref message);
            MessageBox.Show(message);

            int reqLen = message.Length;
            // convert string length value to network order
            int reqLenH2N = IPAddress.HostToNetworkOrder(reqLen);
            // get string length value into a byte array -- for use with
            // Socket.Send
            byte[] reqLenArray = BitConverter.GetBytes(reqLenH2N);

            byte[] dataArray = Encoding.ASCII.GetBytes(message);
            Socket soc;
            if (_clientConnTable.ContainsKey(aRow[0]))
            {
                try
                {
                    soc = (Socket)_clientConnTable[aRow[0]];
                    int bytesSent = soc.Send(reqLenArray, 4, System.Net.Sockets.SocketFlags.None);
                    soc.Send(dataArray, reqLen, System.Net.Sockets.SocketFlags.None);
                }
                catch (ObjectDisposedException)
                {
                    System.Diagnostics.Debug.WriteLine("\n OnClientConnection: Socket has been closed\n");
                    MessageBox.Show("OnClientConnection: Socket has been closed");
                    UpdateGVClientDisconnection(aRow[0].ToString());

                }
                catch (SocketException se)
                {
                    MessageBox.Show(" Problem Sending Start/Stop" + se.Message);
                }
            }
        }


        // This the call back function which will be invoked when the socket
        // detects any client writing of data on the stream
        public void OnDataReceived(IAsyncResult asyn)
        {
            SocketPacket socketData = (SocketPacket)asyn.AsyncState;
            try
            {
                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream 
                // by the client
                int iRx = socketData._currentSocket.EndReceive(asyn);
                if (iRx <= 0)
                {
                    Console.WriteLine("Problem with data, closing the socket");
                    SocketException se = new SocketException(10054);
                    throw (se);
                }
                //char[] chars = new char[iRx + 1];//original
                char[] chars = new char[iRx];
                // Extract the characters as a buffer
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(socketData.dataBuffer,
                    0, iRx, chars, 0);
                System.String clientData = new System.String(chars);
                //Console.WriteLine(" OnDataRcvd. " + clientData.Length + iRx);
                UpdateDataGridViewControl(clientData, socketData._rowIndex);
                socketData.dataBuffer = null;
                clientData = null;
                // Continue the waiting for data on the Socket
                WaitForData(socketData._currentSocket, socketData._clientNumber, socketData._rowIndex);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
                {
                    string msg = "Client " + socketData._clientNumber + " Disconnected" + "\n";

                    // Remove the reference to the worker socket of the closed client
                    // so that this object will get garbage collected
                    //_clientConnTable[socketData._clientNumber] = null;
                    _clientConnTable.Remove(socketData._clientNumber);
                    //clientDisconnected(socketData._clientNumber);
                    UpdateGVClientDisconnection(socketData._clientNumber);
                }
                else
                {
                    MessageBox.Show(se.Message);
                }
            }
            catch (System.ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateGVClientDisconnection(string clientId)
        {
            if (InvokeRequired)
            {
                // We cannot update the GUI on this thread.
                // All GUI controls are to be updated by the main (GUI) thread.
                // Hence we will use the invoke method on the control which will
                // be called when the Main thread is free
                // Do UI update on UI thread
                object obj = clientId;
                dataGridView1.BeginInvoke(new UpdateGVConnectionCallback(OnClientDisconnected), obj);

            }
            else
            {
                // This is the main thread which created this control, hence update it
                // directly 
                OnClientDisconnected(clientId);
            }
        }

        private void OnClientDisconnected(string clientId)
        {
            try
            {
                if (_nameToRowIndexTable.ContainsKey(clientId))
                {
                    int rowIndex = (int)_nameToRowIndexTable[clientId];
                    DataGridViewBand band = dataGridView1.Rows[rowIndex];
                    band.DefaultCellStyle.BackColor = Color.DarkGray;//(Color)band.Tag;
                    DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView1.Rows[rowIndex].Cells[Start_Stop.Index];
                    cell.Value = "STOP";
                    cell.Tag = 0;
                    //cell.Style.BackColor = Color.Red;
                    //cell.Style.ForeColor = Color.Black;
                    //MessageBox.Show("Found " + clientId + rowIndex.ToString());
                    string ma1 = dataGridView1.Rows[rowIndex].Cells[MA1.Index].Value.ToString();
                    string ma2 = dataGridView1.Rows[rowIndex].Cells[MA2.Index].Value.ToString();
                    _paramsForm.setMA(ma1,ma2, rowIndex);
                    dataGridView1.Rows[rowIndex].Cells[TakeLiq.Index].Value = false;
                    dataGridView1.Rows[rowIndex].Cells[WorkLiq.Index].Value = false;
                    dataGridView1.Rows[rowIndex].Cells[CloseOnly.Index].Value = false;

                }
            }
            catch (System.ArgumentNullException)
            {
            }
            catch (System.NullReferenceException)
            {                
            }

            //richTextBoxRcvdMsg.AppendText(" Spread id :" + clientId + ": is disconnected\n");
        }

        private void UpdateDataGridViewControl(string data, int rowIndex)
        {
            // Check to see if this method is called from a thread 
            // other than the one created the control
            if (InvokeRequired)
            {
                // We cannot update the GUI on this thread.
                // All GUI controls are to be updated by the main (GUI) thread.
                // Hence we will use the invoke method on the control which will
                // be called when the Main thread is free
                // Do UI update on UI thread
                
                object[] pList = { data, rowIndex };
                //Console.WriteLine("InvokeReq " + pList[0].ToString().Length);
                dataGridView1.BeginInvoke(new UpdateGridViewCallback(OnUpdateGridView), pList);

            }
            else
            {
                // This is the main thread which created this control, hence update it
                // directly 
                //MessageBox.Show(data);
                //Console.WriteLine("from main " + data);
                OnUpdateGridView(data, rowIndex);
            }
        }

        public void OnUpdateGridView(string data, int rowIndex)
        {
            //string clientId = ParseMessage2(data, "STOP");
            //if (clientId.Length > 0)
            //{
            //    MessageBox.Show("STOP TRADING!!!!, " + clientId);
            //    OnClientDisconnected(clientId);
            //}
            //else
            //{
               _spreadViewer.UpdateTree(data);
               dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                try
                {
                    XmlReader reader = XmlReader.Create(new System.IO.StringReader(data));
                    if (reader.MoveToContent() == XmlNodeType.Element &&
                    reader.Name == "UPDATE")
                    {
                        foreach (DictionaryEntry d in _updatabaleCells)
                        {
                            try
                            {
                                string value = reader.GetAttribute(d.Key.ToString());
                                cellUpdate upd = (cellUpdate)d.Value;
                                int colIndex = upd.colIndex;
                                upd.updDelegate(value, rowIndex, colIndex);
                            }
                            catch (XmlException x)
                            {
                                Console.WriteLine(" Xml Exception updaing from window");
                                Console.WriteLine(x.Message);
                                Console.WriteLine(data);
                            }
                        }
                    }
                    toolStripStatusLabel2.Text = "Pnl: " + _spreadViewer.totalPnl.ToString() + "  Delta: " + _spreadViewer.totalDelta.ToString();
                    if (_spreadViewer.totalPnl < 0)
                        toolStripStatusLabel2.BackColor = Color.Red;
                    else
                    {
                        toolStripStatusLabel2.BackColor = Color.Green;
                        //statusStrip2.BackColor = Color.Green;
                    }
                }
                catch (XmlException x)
                {
                    Console.WriteLine(" Xml Exception updating front window ");
                    Console.WriteLine(x.Message);
                    Console.WriteLine(data);

                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Console.WriteLine(e.RowIndex);
            //if (e.ColumnIndex == dataGridView1.Columns[Start_Stop.Index].Index)
            //{

            //    Console.WriteLine(dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor.ToString());
            //    if (e.Value.ToString() == "STOP")
            //        e.CellStyle.BackColor = Color.DarkRed;
            //    else
            //        e.CellStyle.BackColor = Color.DarkGreen;
            //}              

        }

        public void UpdateGridViewMethod(string value, int rowIndex, int colIndex)
        {
            dataGridView1[colIndex, rowIndex].Value = value;
        }

        public void UpdateListViewMethod(string value, int rowIndex, int colIndex)
        {
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_paramsForm == null)
                    _paramsForm = new AlgParams(_login.ShareHost,_login.Test);

                _paramsForm.suspendColAutoResize();

                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    if (dataGridView1.IsCurrentCellInEditMode)
                    {
                        dataGridView1.EndEdit();
                        if (dataGridView1.CurrentCell.Value == null)
                        {
                            MessageBox.Show("Enter the Spread name in the name col.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter the Spread name in the name col.");
                        return;
                    }

                }
                else
                {
                    if (dataGridView1.IsCurrentCellInEditMode)
                        dataGridView1.EndEdit();
                }
                string rowname = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                _paramsForm.Show();
                _paramsForm.resumeColAutoResize();
                _paramsForm.SelectRow(rowname, e.RowIndex);
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
        }

        private void setupFefaultValues(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void AlgMgrForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (DataGridViewRow aRow in dataGridView1.Rows)
            {
                try
                {
                    string ma1 = aRow.Cells[MA1.Index].Value.ToString();
                    string ma2 = aRow.Cells[MA2.Index].Value.ToString();
                    _paramsForm.setMAOnly(ma1, ma2, aRow.Index);
                }
                catch (System.NullReferenceException)
                {
                }
            }
            _paramsForm.CopyAlgParamsToRemote();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _spreadViewer.Show();
        }

        private void dataGridView1_RowDefaultCellStyleChanged(object sender, DataGridViewRowEventArgs e)
        {
            Console.WriteLine("Caused an event " + e.Row.DefaultCellStyle.BackColor.ToString());
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }
        //************ NEW*********
        //private void AlgMgrForm_ResizeEnd(object sender, EventArgs e)
        //{
        //    Application.DoEvents();
        //}

        //private void AlgMgrForm_Resize(object sender, EventArgs e)
        //{
        //    this.Refresh();
        //}

        //private void AlgMgrForm_Load(object sender, EventArgs e)
        //{
        //    Class1.DoubleBuffered(dataGridView1, true);
        //}

    }

}