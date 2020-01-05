using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Media;

namespace AlgMgrWindow
{
    public partial class SpreadViewer : Form
    {
        SortedList<String, SpreadVData> _rootIndex;
        Color _upColor, _downColor;
        private int _totalPnl=0;
        private int _totalDelta = 0;
        private bool _soundOn = true;
        private static Mutex mut = new Mutex();
        private SoundPlayer player = new SoundPlayer("C:\\Windows\\Media\\ding.wav");
        public SpreadViewer(String broker)
        {          
            InitializeComponent();
            _rootIndex = new SortedList<string, SpreadVData>();
            InitializeSpreadTree();
            treeListView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            if (broker == "ITG" ||
                broker == "itg")
                _soundOn = false;
        }
      
        public int totalPnl
        {
            get { return _totalPnl; }
            set { mut.WaitOne(); _totalPnl = value; mut.ReleaseMutex(); }
        }

        public int totalDelta
        {
            get { return _totalDelta; }
            set { mut.WaitOne(); _totalDelta = value; mut.ReleaseMutex(); }
        }
        public void UpdateTree(string xmlstring)
        {
            try
            {
                int prevNet = 0;
                int currentNet = 0;
                //Console.WriteLine(xmlstring);
                XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlstring));
                String spreadname;

                if (reader.MoveToContent() == XmlNodeType.Element &&
                    reader.Name == "UPDATE")
                {
                    spreadname = reader.GetAttribute("spread");
                    //Console.WriteLine("Found spread name " + spreadname);
                }
                else
                {
                    MessageBox.Show("Did not find UPDATE, problems: " + xmlstring);
                    return;
                }
                //Console.WriteLine(" Trying to find " + spreadname);
                SpreadVData aSpread;

                bool foundSpread = false;
                if (_rootIndex.TryGetValue(spreadname, out aSpread))
                {
                    foundSpread = true;
                    totalPnl -= Convert.ToInt32(aSpread.Pnl);
                    totalDelta -= (int)Convert.ToDouble(aSpread.Delta);
                    prevNet = Convert.ToInt32(aSpread.Net);
                }
                else
                {
                    aSpread = new SpreadVData(spreadname);
                }

                aSpread.Net = reader.GetAttribute("tnet");
                currentNet = Convert.ToInt32(aSpread.Net);
                aSpread.Delta = reader.GetAttribute("tdelta");
                totalDelta += (int) Convert.ToDouble(aSpread.Delta);
                aSpread.Pnl = reader.GetAttribute("tpnl");
                totalPnl += Convert.ToInt32(aSpread.Pnl);
                aSpread.FvBid = reader.GetAttribute("bbid");
                aSpread.FvAsk = reader.GetAttribute("bask");
                aSpread.Buys = reader.GetAttribute("tbuys");
                aSpread.Sells = reader.GetAttribute("tsells");

                int itemIndex = 0;
                while (reader.Read())
                {
                    XmlNodeType nType = reader.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        //Console.WriteLine("Element:" + reader.Name.ToString());
                        if (reader.Name.ToString() == "Base")
                        {
                            SpreadInfo item;
                            if (foundSpread)
                            {
                                item = (SpreadInfo) aSpread._bases[itemIndex];
                            }
                            else
                                item = new SpreadInfo();
                            //Console.WriteLine("Attr:" + reader.GetAttribute("symbol"));
                            //item.Name = reader.GetAttribute("symbol");
                            item.Name = reader.GetAttribute("symbol");
                            //Console.WriteLine("Attr:" + reader.GetAttribute("buys"));
                            item.Buys = reader.GetAttribute(("buys"));
                            //Console.WriteLine("Attr:" + reader.GetAttribute("sells"));
                            item.Sells = reader.GetAttribute("sells");
                            //Console.WriteLine("Attr:" + reader.GetAttribute("avgb"));
                            item.AvgBuy = reader.GetAttribute("avgb");
                            //Console.WriteLine("Attr:" + reader.GetAttribute("avgs"));
                            item.AvgSell = reader.GetAttribute("avgs");
                            item.Net = reader.GetAttribute("net");
                            item.Delta = reader.GetAttribute("delta");
                            item.Pnl = reader.GetAttribute("pnl");
                            item.Bid = reader.GetAttribute("bid");
                            item.Ask = reader.GetAttribute("ask");
                            item.Last = reader.GetAttribute("last");
                            item.FvBid = reader.GetAttribute("fvbid");
                            item.FvAsk = reader.GetAttribute("fvask");
                            if (!foundSpread)
                                aSpread.AddBase(item);
                            else
                            {
                                // aSpread.UpdateBase(item, itemIndex);
                                itemIndex++;
                            }
                        }
                    }

                }
                aSpread.CalcTotalAvgBuyAndSell();
                if (!foundSpread)
                {
                    treeListView1.AddObject(aSpread);
                    Console.WriteLine("Index of " + spreadname + treeListView1.IndexOf(aSpread).ToString());
                    _rootIndex.Add(spreadname, aSpread);
                    
                }
                else
                {
                    treeListView1.RefreshObject(aSpread);
                }
               // Console.WriteLine(currentNet.ToString() + " " + prevNet.ToString());
                if (_soundOn && (currentNet != prevNet))
                    player.Play();
            }
            catch (XmlException x)
            {
                Console.WriteLine(" Rcvd xml exception " + x.Message);
                Console.WriteLine(xmlstring);
            }
        }

        public void InitializeSpreadTree()
        {

            treeListView1.CanExpandGetter = delegate(object x)
            {
                //bool test = (x is SpreadVData);
                //Console.WriteLine("Am i here ?");
                return (x is SpreadVData);
            };

            treeListView1.ChildrenGetter = delegate(object x)
            {
                SpreadVData data = (SpreadVData)x;
                try
                {
                    return data.GetBases();
                }
                catch (ArgumentNullException)
                {
                    return new ArrayList();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(this, ex.Message, "SpreadTree", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return new ArrayList();
                }
                catch (KeyNotFoundException)
                {
                    return new ArrayList();
                }
            };
            _upColor = Color.Green;
            _downColor = Color.Red;
            treeListView1.FormatRow += new EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(treeListView1_FormatRow);
            //treeListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        void treeListView1_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            if (e.Model is SpreadVData)
            {
                SpreadVData data = (SpreadVData)e.Model;
                if (data.Pnl != null)
                {
                    if (data.Pnl.Contains("-"))
                        e.Item.BackColor = _downColor;
                    else
                        e.Item.BackColor = _upColor;
                }
            }
            //throw new Exception("The method or operation is not implemented.");
        }

        private void MakeXmlString()
        {
            string xmlstring = "<UPDATE spread=\"DELTA\" tpnl=\"100\">";
            xmlstring += "<Base symbol=\"XLF\" buys=\"100\" sells=\"100\" avgb=\"14.2\" avgs=\"14.25\"/>";
            xmlstring += "<Base symbol=\"XLK\" buys=\"100\" sells=\"100\" avgb=\"24.2\" avgs=\"24.25\"/>";
            xmlstring += "<Hedge symbol=\"SPY\" buys=\"100\" sells=\"100\" avgb=\"104.2\" avgs=\"104.25\"/>";
            xmlstring += "</UPDATE>";
            MessageBox.Show(xmlstring);
            UpdateTree(xmlstring);
        }
        private void MakeXmlString2()
        {
            string xmlstring = "<UPDATE spread=\"TESTA\" tpnl=\"100\">";
            xmlstring += "<Base symbol=\"XLF\" buys=\"100\" sells=\"100\" avgb=\"14.2\" avgs=\"14.25\"/>";
            xmlstring += "<Base symbol=\"XLK\" buys=\"100\" sells=\"100\" avgb=\"24.2\" avgs=\"24.25\"/>";
            xmlstring += "<Hedge symbol=\"SPY\" buys=\"100\" sells=\"100\" avgb=\"104.2\" avgs=\"104.25\"/>";
            xmlstring += "</UPDATE>";
            MessageBox.Show(xmlstring);
            UpdateTree(xmlstring);
        }
        private void MakeXmlString3()
        {
            string xmlstring = "<UPDATE spread=\"TESTA\"/>";
            xmlstring += "<Base symbol=\"XLF\" buys=\"300\" sells=\"100\" avgb=\"14.2\" avgs=\"14.25\"/>";
            xmlstring += "<Base symbol=\"XLK\" buys=\"300\" sells=\"100\" avgb=\"24.2\" avgs=\"24.25\"/>";
            xmlstring += "<Hedge symbol=\"SPY\" buys=\"100\" sells=\"100\" avgb=\"104.2\" avgs=\"104.25\"/>";
            xmlstring += "</UPDATE>";
            MessageBox.Show(xmlstring);
            UpdateTree(xmlstring);
        }

        private void SpreadViewer_Load(object sender, EventArgs e)
        {

        }

        private void SpreadViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void treeListView1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            Console.WriteLine(e.Item.Text);
            if (e.ColumnIndex == olvColumn7.Index)
            {
                Console.WriteLine(e.Item.Text);
            }
        }

        private void treeListView1_BeforeSorting(object sender, BrightIdeasSoftware.BeforeSortingEventArgs e)
        {
            e.Canceled = true;

            //Console.WriteLine("** Calling Sortorder");
            //if (e.ColumnToSort != null && e.ColumnToSort.DisplayIndex == 0)
            //{
            //    if (e.SortOrder == SortOrder.Ascending)
            //    {
            //        treeListView1.SuspendLayout();
            //        treeListView1.ClearObjects();
            //        foreach (KeyValuePair<string, SpreadVData> kvp in _rootIndex)
            //        {
            //            Console.WriteLine("root= " + kvp.Key);
            //            treeListView1.AddObject(_rootIndex[kvp.Key]);

            //        }
            //        treeListView1.ResumeLayout();
            //    }
            //    e.Handled = true;
            //}
            //else
            //    e.Canceled = true;

            
            ////IEnumerable roots = treeListView1.Roots;
            ////foreach (SpreadVData model in roots)
            ////{
            ////    Console.WriteLine("root= " + model.Name);
            ////    
            ////}
        }

        private void treeListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //Console.WriteLine(e.Column);
            if (e.Column == 0)
            {
                treeListView1.SuspendLayout();
                treeListView1.ClearObjects();
                foreach (KeyValuePair<string, SpreadVData> kvp in _rootIndex)
                {
                   // Console.WriteLine("root= " + kvp.Key);
                    treeListView1.AddObject(_rootIndex[kvp.Key]);

                }
                treeListView1.ResumeLayout();
            }
        }
    }

    public class SpreadInfo : IDisposable
    {
        private bool disposed = false;

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                }
            }
            disposed = true;
        }

        private string _spreadUnderlying;
        public string Name
        {
            get { return _spreadUnderlying; }
            set { _spreadUnderlying = value; }
        }
        private string _buys;
        public string Buys
        {
            get { return _buys; }
            set { _buys = value; }
        }
        private string _avgBuyPrice;
        public string AvgBuy
        {
            get { return _avgBuyPrice; }
            set { _avgBuyPrice = value; }
        }
        private string _sells;
        public string Sells
        {
            get { return _sells; }
            set { _sells = value; }
        }
        private string _avgSellPrice;
        public string AvgSell
        {
            get { return _avgSellPrice; }
            set { _avgSellPrice = value; }
        }

        private string _net;
        public string Net
        {
            get { return _net; }
            set { _net = value; }
        }
        private string _bid;
        public string Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }
        private string _ask;
        public string Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }
        private string _last;
        public string Last
        {
            get { return _last; }
            set { _last = value; }
        }
        private string _delta;
        public string Delta
        {
            get { return _delta; }
            set { _delta = value; }
        }
        private string _pnl;
        public string Pnl
        {
            get { return _pnl; }
            set { _pnl = value; }
        }
        private string _fvBid;
        public string FvBid
        {
            get { return _fvBid; }
            set { _fvBid = value; }
        }
        private string _fvAsk;
        public string FvAsk
        {
            get { return _fvAsk; }
            set { _fvAsk = value; }
        }
    }
    public class SpreadVData //: IDisposable
    {
        //private bool disposed = false;

        //void IDisposable.Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //private void Dispose(bool disposing)
        //{
        //    // Check to see if Dispose has already been called.
        //    if (!this.disposed)
        //    {
        //        // If disposing equals true, dispose all managed 
        //        // and unmanaged resources.
        //        if (disposing)
        //        {
        //            _bases.Clear();
        //            _hedges.Clear();
        //            _bases = null;
        //            _hedges = null;
        //        }
        //    }
        //    disposed = true;
        //}

        public SpreadVData(string name)
        {
            _spreadName = name;
            //_bases = new ArrayList();
            //_hedges = new ArrayList();
        }

        private string _spreadName;

        public string Name
        {
            get { return _spreadName; }
            set { _spreadName = value; }
        }

        private string _totalNet;
        public string Net
        {
            get { return _totalNet; }
            set { _totalNet = value; }
        }

        private String _totalDelta;
        public string Delta
        {
            get { return _totalDelta; }
            set { _totalDelta = value; }
        }

        private String _totalPnl;
        public string Pnl
        {
            get { return _totalPnl; }
            set { _totalPnl = value; }
        }

        private String _basketBid;
        public string FvBid
        {
            get { return _basketBid; }
            set { _basketBid = value; }
        }
        private String _basketAsk;
        public string FvAsk
        {
            get { return _basketAsk; }
            set { _basketAsk = value; }
        }

        private String _totalBuys;
        public string Buys
        {
            get { return _totalBuys; }
            set { _totalBuys = value; }
        }
        private String _totalSells;
        public string Sells
        {
            get { return _totalSells; }
            set { _totalSells = value; }
        }
        public String _tAvgSell = "-";
        public string AvgSell
        {
            get { return _tAvgSell; }
            set { _tAvgSell = value; }
        }
        public string _tAvgBuy = "-";
        public string AvgBuy
        {
            get { return _tAvgBuy; }
            set { _tAvgSell = value; }
        }

        public string Bid
        {
            get { return "-"; }
        }
        public string Ask
        {
            get { return "-"; }
        }

        public string Last
        {
            get { return "-"; }
        }

        public ArrayList GetBases()
        {
            return _bases;
        }
        public ArrayList GetHedges()
        {
            return _hedges;
        }

        public void AddBase(SpreadInfo item)
        {
            _bases.Add(item);
        }
        public void AddHedge(SpreadInfo item)
        {
            _hedges.Add(item);
        }

        public void UpdateBase(SpreadInfo item, int itemIndex)
        {
            _bases.RemoveAt(itemIndex);
            _bases.Insert(itemIndex, item);
        }

        public void CalcTotalAvgBuyAndSell()
        {
            _tAvgBuy = "-";
            _tAvgSell = "-";
            if (_bases.Count == 2)
            {
                double avgBuyBase = Convert.ToDouble(((SpreadInfo)_bases[0]).AvgBuy);
                double avgSellHdg = Convert.ToDouble(((SpreadInfo)_bases[1]).AvgSell);
                if (avgSellHdg > 0.0)
                {
                    _tAvgBuy = String.Format("{0:0.0000}",(avgBuyBase / avgSellHdg));
                }
                double avgSellBase = Convert.ToDouble(((SpreadInfo)_bases[0]).AvgSell);
                double avgBuyHdg = Convert.ToDouble(((SpreadInfo)_bases[1]).AvgBuy);
                if (avgBuyHdg > 0.0)
                {
                    _tAvgSell = String.Format("{0:0.0000}", (avgSellBase / avgBuyHdg));
                }
            }
        }

        public ArrayList _bases = new ArrayList();
        public ArrayList _hedges = new ArrayList();
    }
}