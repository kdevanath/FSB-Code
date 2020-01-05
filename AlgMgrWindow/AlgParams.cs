using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace AlgMgrWindow
{
    public partial class AlgParams : Form
    {
        //public struct RowInfo
        //{
        //    public RowInfo(int index)
        //    {
        //        rowIndex = index;
        //        beginningOfDay = true;
        //    }
        //    public int rowIndex;
        //    public bool beginningOfDay;
        //}

        public struct cellInfo
        {
            public cellInfo(DataGridViewColumn col, string value, bool celledited, int rowIndex)
            {
                _sendString = value;
                _edited = celledited;
                _rowIndex = rowIndex;
                _column = col;
                _prevValue = "0";
            }
            public string _sendString;
            public bool _edited;
            public int _rowIndex;
            public DataGridViewColumn _column;
            public string _prevValue;
        }

        public DataTable _settingsTable;
        private Hashtable _nameToRowIndexTable;
        //private Hashtable _rowIndexToDayTable;

        private Dictionary<int, cellInfo> _cellToSendString;
        private List<int> _editedCellsList;
        private int lastSelectedRowIndex;
        private bool _testMode;
        private String _remoteHost;
        private String _sharePath;
        public AlgParams(String remote, bool mode)
        {
            InitializeComponent();

            _testMode = mode;
            _remoteHost = remote;
            _sharePath = "\\\\" + _remoteHost + "\\Share\\AlgSettings.xml";
            Console.WriteLine(_sharePath);

            lastSelectedRowIndex = -1;

            _nameToRowIndexTable = Hashtable.Synchronized(new Hashtable());
            //_rowIndexToDayTable = Hashtable.Synchronized(new Hashtable());

            _cellToSendString = new Dictionary<int, cellInfo>();

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            _cellToSendString.Add(SKEW.Index, new cellInfo(SKEW, "SKEW", false, -1));
            SKEW.Tag = 4;
            _cellToSendString.Add(WIDTH.Index, new cellInfo(WIDTH, "WIDTH", false, -1));
            WIDTH.Tag = 30;
            _cellToSendString.Add(MA1.Index, new cellInfo(MA1, "MA1", false, -1));
            MA1.Tag = 0;
            _cellToSendString.Add(MA2.Index, new cellInfo(MA2, "MA2", false, -1));
            MA2.Tag = 0;
            _cellToSendString.Add(SIZE_ADJ.Index, new cellInfo(SIZE_ADJ, "SIZE_ADJ", false, -1));
            SIZE_ADJ.Tag = 0;
            _cellToSendString.Add(UPD_THRESH.Index, new cellInfo(UPD_THRESH, "UPD_THRESH", false, -1));
            UPD_THRESH.Tag = 10;
            _cellToSendString.Add(CXL_WORSE.Index, new cellInfo(CXL_WORSE, "CXL_WORSE", false, -1));
            CXL_WORSE.Tag = 0;
            _cellToSendString.Add(CXL_BETTER.Index, new cellInfo(CXL_BETTER, "CXL_BETTER", false, -1));
            CXL_BETTER.Tag = 5;
            _cellToSendString.Add(MIN_HDG_SIZE.Index, new cellInfo(MIN_HDG_SIZE, "MIN_HDG_SIZE", false, -1));
            MIN_HDG_SIZE.Tag = 1;
            _cellToSendString.Add(MAX_POSITION.Index, new cellInfo(MAX_POSITION, "MAX_POSITION", false, -1));
            MAX_POSITION.Tag = 10;
            _cellToSendString.Add(MAX_PENDING.Index, new cellInfo(MAX_PENDING, "MAX_PENDING", false, -1));
            MAX_PENDING.Tag = 10;
            _cellToSendString.Add(BASE_TAKE_SIZE.Index, new cellInfo(BASE_TAKE_SIZE, "BASE_TAKE_SIZE", false, -1));
            BASE_TAKE_SIZE.Tag = 1;
            _cellToSendString.Add(HEDGE_TAKE_SIZE.Index, new cellInfo(HEDGE_TAKE_SIZE, "HEDGE_TAKE_SIZE", false, -1));
            HEDGE_TAKE_SIZE.Tag = 1;
            _cellToSendString.Add(BASE_WORK_SIZE.Index, new cellInfo(BASE_WORK_SIZE, "BASE_WORK_SIZE", false, -1));
            BASE_WORK_SIZE.Tag = 1;
            _cellToSendString.Add(FX_MULTI.Index, new cellInfo(FX_MULTI, "FX_MULTI", false, -1));
            FX_MULTI.Tag = 1;
            _cellToSendString.Add(BETA_MULTI.Index, new cellInfo(BETA_MULTI, "BETA_MULTI", false, -1));
            BETA_MULTI.Tag = 1;
            _cellToSendString.Add(HEDGE_PREMIUM.Index, new cellInfo(HEDGE_PREMIUM, "HEDGE_PREMIUM", false, -1));
            HEDGE_PREMIUM.Tag = 10;
            _cellToSendString.Add(RANDOMIZE_BASE.Index, new cellInfo(RANDOMIZE_BASE, "RANDOMIZE_BASE", false, -1));
            RANDOMIZE_BASE.TrueValue = true;
            RANDOMIZE_BASE.FalseValue = false;
            RANDOMIZE_BASE.ValueType = typeof(bool);
            RANDOMIZE_BASE.Tag = false;

            _cellToSendString.Add(TRADE_THRESH.Index, new cellInfo(TRADE_THRESH, "TRADE_THRESH", false, -1));
            TRADE_THRESH.Tag = 10;

            _cellToSendString.Add(MA1_LENGTH.Index, new cellInfo(MA1_LENGTH, "MA1_LENGTH", false, -1));
            MA1_LENGTH.Tag = 4000;

            _cellToSendString.Add(MA2_LENGTH.Index, new cellInfo(MA2_LENGTH, "MA2_LENGTH", false, -1));
            MA2_LENGTH.Tag = 1000;

            _cellToSendString.Add(DELTA_THRESH.Index, new cellInfo(DELTA_THRESH, "DELTA_THRESH", false, -1));
            DELTA_THRESH.Tag = 100000;

            _cellToSendString.Add(MIX_RATIO.Index, new cellInfo(MIX_RATIO, "MIX_RATIO", false, -1));
            MIX_RATIO.Tag = 0;

            _cellToSendString.Add(BUMP.Index, new cellInfo(BUMP, "BUMP", false, -1));
            BUMP.Tag = 0;

            _cellToSendString.Add(PRICE_LIMIT.Index, new cellInfo(PRICE_LIMIT, "PRICE_LIMIT", false, -1));
            PRICE_LIMIT.Tag = "SQZ";

            _cellToSendString.Add(SKEW_UNIT.Index, new cellInfo(SKEW_UNIT, "SKEW_UNIT", false, -1));
            SKEW_UNIT.Tag = 10000;

            _cellToSendString.Add(CLOSE_TARGET.Index, new cellInfo(CLOSE_TARGET, "CLOSE_TARGET", false, -1));
            CLOSE_TARGET.Tag = -1;

            _editedCellsList = new List<int>(_cellToSendString.Count);
            // MessageBox.Show(PRICE_LIMIT.Index.ToString());

            LoadFromXml();
        }

        public void suspendColAutoResize()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }
        public void resumeColAutoResize()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
;
        }

        private void LoadFromXml()
        {
            _settingsTable = new DataTable("AlgSettingsTable");
            try
            {
                if (_testMode)
                {
                    _settingsTable.ReadXmlSchema("C:\\FSB\\data\\AlgSettings.xml");
                    _settingsTable.ReadXml("C:\\FSB\\data\\AlgSettings.xml");
                }
                else
                {
                    Console.WriteLine("Reading from shared: " + _sharePath);
                    _settingsTable.ReadXmlSchema(_sharePath);
                    _settingsTable.ReadXml(_sharePath);
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Read from Share directory,will read from LOCAL dir. " + e.Message);
                _settingsTable.ReadXmlSchema("C:\\FSB\\data\\AlgSettings.xml");
                _settingsTable.ReadXml("C:\\FSB\\data\\AlgSettings.xml");
            }
            DataView view =_settingsTable.DefaultView;
            view.Sort = "[" + _settingsTable.Columns[0].ColumnName + "] asc";
            _settingsTable = view.ToTable();
            DataRow row;
            dataGridView1.Rows.Clear();
            int numRows = _settingsTable.Rows.Count;
            for (int i = 0; i < numRows; i++)
            {
                row = _settingsTable.Rows[i];
                _nameToRowIndexTable.Add(row[0], i);
                dataGridView1.Rows.Add(row.ItemArray);
                DataGridViewBand band = dataGridView1.Rows[i]; //.Cells[Start_Stop.Index].Tag = 0;
                band.Tag = band.DefaultCellStyle.BackColor;
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)(dataGridView1.Rows[i].Cells[PRICE_LIMIT.Index]);
                //MessageBox.Show(cell.Value.ToString() + "/" + dataGridView1.Rows[i].Cells[MIX_RATIO.Index].Value.ToString(),row[0].ToString());
                //if( cell.Value == null)
                //    cell.Value = "SQZ";

            }
        }

        public void SaveDataToXml()
        {
            if (this.dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show(" There is no data to save");
                return;
            }

            if (dataGridView1.IsCurrentRowDirty)
                dataGridView1.EndEdit();

            _settingsTable.Clear();
            int cols = dataGridView1.Columns.Count;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                try
                {
                    _settingsTable.Columns.Add(dataGridView1.Columns[i].Name, typeof(System.String));
                    Console.WriteLine("Save data " + dataGridView1.Columns[i].Name);
                }
                catch (DuplicateNameException)
                {
                }
            }

            DataRow myRow;

            int rowIndex = 0;
            DateTime start = DateTime.Now;
            foreach (DataGridViewRow drow in this.dataGridView1.Rows)
            {
                if (drow.Cells[0].Value == null)
                    continue;
                myRow = _settingsTable.NewRow();
                for (int i = 0; i < cols; i++)
                {

                    if (drow.Cells[i].Value != null)
                    {
                        myRow[i] = drow.Cells[i].Value;
                        //if (i == RANDOMIZE_BASE.Index)
                        //{
                        //    if (drow.Cells[i].Equals("True"))                         
                        //        myRow[i] = 1;
                        //    else
                        //        myRow[i] = 0;
                        //}
                        //else
                        //    myRow[i] = drow.Cells[i].Value;
                    }
                    else if (i == RANDOMIZE_BASE.Index)
                    {
                        myRow[i] = false;
                    }
                    else
                    {
                        myRow[i] = drow.Cells[i].Tag;
                        //MessageBox.Show(myRow[i].ToString(), i.ToString());
                    }

                }
                //MessageBox.Show(myRow[0].ToString(),rowIndex.ToString());
                try
                {
                    _nameToRowIndexTable[myRow[0]] = rowIndex;
                }
                catch (ArgumentNullException)
                {
                    _nameToRowIndexTable.Add(myRow[0], rowIndex);
                }
                rowIndex++;
                _settingsTable.Rows.Add(myRow);
            }

            start = DateTime.Now;
            _settingsTable.WriteXml("C:\\FSB\\data\\AlgSettings.xml");
            try
            {

                //if (!_testMode)
                //_settingsTable.WriteXml("\\\\10.27.113.10\\Share\\AlgSettings.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot save to Share directory " + e.Message);
            }
            //endloop = DateTime.Now;
            //delta = ((TimeSpan)(endloop - start)).Milliseconds;
            //Console.WriteLine(" The number of seconds to write to remote {0}", delta);
        }

        public void SelectRow(string rowname, int rowIndex)
        {
            //MessageBox.Show(rowIndex.ToString(),rowname);
            if (_nameToRowIndexTable.ContainsKey(rowname))
            {
                rowIndex = (int)_nameToRowIndexTable[rowname];
                //dataGridView1.Rows[rowIndex].Selected = true;

            }
            else
            {
                if (_nameToRowIndexTable.ContainsValue(rowIndex))
                {
                    //MessageBox.Show(rowIndex.ToString());
                    // Row index already exists, I must have over written the name
                    _nameToRowIndexTable.Add(rowname, rowIndex);

                    //if we have chosen a new row to edit and have not save the information
                    if (lastSelectedRowIndex != rowIndex)
                        Undo(lastSelectedRowIndex);

                    dataGridView1.BeginEdit(true);
                    lastSelectedRowIndex = rowIndex;
                    SetReadOnlyProperty(rowIndex, true);
                    dataGridView1.Rows[rowIndex].Cells[0].Value = rowname;
                    //if (dataGridView1.IsCurrentRowDirty)
                    dataGridView1.EndEdit();
                    return;
                }
                _nameToRowIndexTable.Add(rowname, rowIndex);
                dataGridView1.Rows.Add();
                //rowIndex = _nameToRowIndexTable.Count;
                //it is a new column, update with the new values
                //dataGridView1.
                dataGridView1[0, rowIndex].Value = rowname;
                dataGridView1.Rows[rowIndex].Tag = dataGridView1.DefaultCellStyle.BackColor;
                if (lastSelectedRowIndex != rowIndex)
                    Undo(lastSelectedRowIndex);
                foreach (KeyValuePair<int, cellInfo> d in _cellToSendString)
                {
                    if (d.Value._column.Tag != null)
                    {
                        // MessageBox.Show(c.Index.ToString(), rowIndex.ToString());
                        dataGridView1[d.Key, rowIndex].Value = d.Value._column.Tag;
                        _editedCellsList.Add(d.Key);
                    }

                }
                //if (dataGridView1.IsCurrentRowDirty)
                dataGridView1.EndEdit();

            }
            lastSelectedRowIndex = rowIndex;
            SetReadOnlyProperty(rowIndex, true);
        }

        public void SetReadOnlyProperty(int rowIndex, bool readOnly)
        {
            if (rowIndex < 0) return;
            int numRows = dataGridView1.Rows.Count;

            for (int i = 0; i < numRows; i++)
            {
                if (i != rowIndex)
                    dataGridView1.Rows[i].ReadOnly = readOnly;
                else if (i == rowIndex)
                    dataGridView1.Rows[i].ReadOnly = !readOnly;
            }

            if (readOnly)
            {
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Ivory;
            }
            else
            {
                if (dataGridView1.Rows[rowIndex].Tag != null)
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = (Color)dataGridView1.Rows[rowIndex].Tag;
            }
        }

        public void GetRowValue(string rowname, ref string data)
        {
            int rowIndex = -1;

            if (_nameToRowIndexTable.ContainsKey(rowname))
            {
                lastSelectedRowIndex = rowIndex = (int)_nameToRowIndexTable[rowname];

            }
            if (rowIndex < 0)
            {
                MessageBox.Show(rowname + " does not exist in the Params window, did you forget to enter values ?");
                return;
            }

            SetReadOnlyProperty(lastSelectedRowIndex, false);
            lastSelectedRowIndex = -1;
            object[] aRow = _settingsTable.Rows[rowIndex].ItemArray;

            foreach (int editedCol in _editedCellsList)
            {
                try
                {
                    cellInfo d = _cellToSendString[editedCol];
                    if (editedCol == RANDOMIZE_BASE.Index)
                    {
                        if (aRow[editedCol].ToString().Length != 0)
                        {
                            if (aRow[editedCol].Equals("True"))
                                data += d._sendString + "=" + 1 + ";";
                            else
                                data += d._sendString + "=" + 0 + ";";
                        }
                        else
                        {
                            data += d._sendString + "=" + 0 + ";";
                        }
                    }
                    else
                    {
                        if (aRow[editedCol].ToString().Length != 0)
                            data += d._sendString + "=" + aRow[editedCol] + ";";
                    }
                }
                catch (KeyNotFoundException)
                {
                }
            }
            if (_editedCellsList.Count > 0)
                _editedCellsList.Clear();
        }

        public void setMAOnly(string ma1Value, string ma2Value, int rowIndex)
        {

            dataGridView1[MA1.Index, rowIndex].Value = ma1Value;
            _settingsTable.Rows[rowIndex][MA1.Index] = ma1Value;
            dataGridView1[MA2.Index, rowIndex].Value = ma2Value;
            _settingsTable.Rows[rowIndex][MA2.Index] = ma2Value;
            _settingsTable.AcceptChanges();
            if (dataGridView1.IsCurrentRowDirty)
                dataGridView1.EndEdit();
        }

        public void setMA(string ma1Value, string ma2Value, int rowIndex)
        {

            dataGridView1[MA1.Index, rowIndex].Value = ma1Value;
            _settingsTable.Rows[rowIndex][MA1.Index] = ma1Value;
            dataGridView1[MA2.Index, rowIndex].Value = ma2Value;
            _settingsTable.Rows[rowIndex][MA2.Index] = ma2Value;

            _settingsTable.AcceptChanges();
            _settingsTable.WriteXml("C:\\FSB\\data\\AlgSettings.xml");
            try
            {
                if (!_testMode)
                    _settingsTable.WriteXml(_sharePath);
                // _settingsTable.WriteXml("\\\\10.27.113.10\\Share\\AlgSettings.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot save to Share directory " + e.Message);
            }
            if (dataGridView1.IsCurrentRowDirty)
                dataGridView1.EndEdit();

        }

        public void CopyAlgParamsToRemote()
        {
            _settingsTable.WriteXml("C:\\FSB\\data\\AlgSettings.xml");
            try
            {
                if (!_testMode)
                {
                    _settingsTable.WriteXml(_sharePath);
                    //_settingsTable.WriteXml("\\\\10.27.113.10\\Share\\AlgSettings.xml");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot save to Share directory " + e.Message);
            }
        }

        private void AlgParams_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetReadOnlyProperty(lastSelectedRowIndex, false);
            e.Cancel = true;
            this.Visible = false;
        }

        private void Undo(int rowIndex)
        {
            foreach (int i in _editedCellsList)
            {
                //MessageBox.Show(i.ToString(), rowIndex.ToString());
                //MessageBox.Show(_cellToSendString[i]._prevValue,i.ToString());
                dataGridView1[i, rowIndex].Value = _cellToSendString[i]._prevValue;
            }
            if (_editedCellsList.Count > 0)
                _editedCellsList.Clear();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;


            MessageBox.Show(e.ColumnIndex.ToString(), e.RowIndex.ToString());
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                try
                {
                    cellInfo c = _cellToSendString[e.ColumnIndex];
                    c._rowIndex = e.RowIndex;
                    c._prevValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                    _cellToSendString[e.ColumnIndex] = c;
                    _editedCellsList.Add(e.ColumnIndex);
                    MessageBox.Show(c._prevValue.ToString());
                }
                catch (KeyNotFoundException)
                {
                }
            }
            else
                MessageBox.Show(e.ColumnIndex.ToString(), "Nothing in the col.");


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            // MessageBox.Show(e.ColumnIndex.ToString(), e.RowIndex.ToString());
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                try
                {
                    cellInfo c = _cellToSendString[e.ColumnIndex];
                    c._rowIndex = e.RowIndex;
                    c._prevValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                    _cellToSendString[e.ColumnIndex] = c;
                    _editedCellsList.Add(e.ColumnIndex);
                    //MessageBox.Show(c._prevValue.ToString());
                }
                catch (KeyNotFoundException)
                {
                }
            }
            else
                MessageBox.Show("Nothing in the column " + _cellToSendString[e.ColumnIndex]._column.HeaderText);
        }
    }
}