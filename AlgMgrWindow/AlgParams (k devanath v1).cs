using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AlgMgrWindow
{
    public partial class AlgParams : Form
    {
        public DataTable _settingsTable;
        private Hashtable _nameToRowIndexTable;
        private Hashtable _cellToFieldIdTable;

        public AlgParams()
        {
            InitializeComponent();

            _nameToRowIndexTable = Hashtable.Synchronized(new Hashtable());
            _cellToFieldIdTable = Hashtable.Synchronized(new Hashtable());

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            _cellToFieldIdTable.Add(SKEW.Index, "SKEW");
            _cellToFieldIdTable.Add(WIDTH.Index, "WIDTH");
            WIDTH.Tag = 30;
            _cellToFieldIdTable.Add(START_VALUE.Index, "START_VALUE");
            START_VALUE.Tag = 0.0;
            _cellToFieldIdTable.Add(POSITION.Index, "POSITION");
            POSITION.Tag = 0;
            SKEW.Tag = 4;
            _cellToFieldIdTable.Add(TAKE_LIQ.Index, "TAKE_LIQ");
            TAKE_LIQ.Tag = 0;
            TAKE_LIQ.TrueValue = true;
            TAKE_LIQ.FalseValue = false;
            TAKE_LIQ.ValueType = typeof(bool);
            _cellToFieldIdTable.Add(WORK_LIQ.Index, "WORK_LIQ");
            WORK_LIQ.Tag = 0;
            WORK_LIQ.TrueValue = true;
            WORK_LIQ.FalseValue = false;
            WORK_LIQ.ValueType = typeof(bool);
            _cellToFieldIdTable.Add(SIZE_ADJ.Index, "SIZE_ADJ");
            SIZE_ADJ.Tag = 0;
            _cellToFieldIdTable.Add(CXL_THRESH.Index, "CXL_THRESH");
            CXL_THRESH.Tag = 20;
            _cellToFieldIdTable.Add(CXL_THRESH.Index, "UPD_THESH");
            UPDTHRESH.Tag = 20;
            _cellToFieldIdTable.Add(CXLWORSE.Index, "CXL_WORSE");
            CXLWORSE.Tag = 0;
            _cellToFieldIdTable.Add(CXLBETTER.Index, "CXL_BETTER");
            CXLBETTER.Tag = 4;
            _cellToFieldIdTable.Add(MINHDGSIZE.Index, "MINHDGSIZE");
            MINHDGSIZE.Tag = "MINHDGSIZE";
            _cellToFieldIdTable.Add(MAX_POSITION.Index, "MAX_POSITION");
            MAX_POSITION.Tag = 10;
            _cellToFieldIdTable.Add(MAX_PENDING.Index, "MAX_PENDING");
            MAX_PENDING.Tag = 5;
            _cellToFieldIdTable.Add(SELLSIZE.Index, "SELL_SIZE");
            SELLSIZE.Tag = 1;
            _cellToFieldIdTable.Add(BUYSIZE.Index, "BUY_SIZE");
            BUYSIZE.Tag = 1;
        }

        private void LoadFromXml()
        {
            _settingsTable = new DataTable("AlgSettingsTable1");
            try
            {
                _settingsTable.ReadXmlSchema("C:\\FSB\\data\\AlgSettings1.xml");
                _settingsTable.ReadXml("C:\\FSB\\data\\AlgSettings1.xml");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            DataRow row;
            dataGridView1.Rows.Clear();

            for (int i = 0; i < _settingsTable.Rows.Count - 1; i++)
            {
                row = _settingsTable.Rows[i];
                _nameToRowIndexTable.Add(row[0], i);
                dataGridView1.Rows.Add(row.ItemArray);
                DataGridViewBand band = dataGridView1.Rows[i]; //.Cells[Start_Stop.Index].Tag = 0;
                band.Tag = band.DefaultCellStyle.BackColor;
            }
        }

        private void SaveDataToXml()
        {
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
                myRow = _settingsTable.NewRow();
                for (int i = 0; i < cols - 1; i++)
                {
                    if (drow.Cells[i].Value != null)
                        myRow[i] = drow.Cells[i].Value;
                }

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
            _settingsTable.WriteXml("C:\\FSB\\data\\AlgSettings1.xml");
        }

        private void GetRowValue(string rowname, string data)
        {
            int rowIndex = -1;
            if (_nameToRowIndexTable.ContainsKey(rowname))
                rowIndex = (int)_nameToRowIndexTable[rowname];

            object[] aRow = _settingsTable.Rows[rowIndex].ItemArray;

            foreach ( 
            string message = WIDTH.Tag.ToString() + "=" + aRow[WIDTH.Index] + ";";
            message += START_VALUE.Tag.ToString() + "=" + aRow[START_VALUE.Index] + ";";
            message += POSITION.Tag.ToString() + "=" + aRow[POSITION.Index] + ";";
            message += SKEW.Tag.ToString() + "=" + aRow[SKEW.Index] + ";";
            if (aRow[TAKE_LIQ.Index].ToString().Length != 0)
            {
                if (aRow[TAKE_LIQ.Index].Equals("True"))
                    message += TAKE_LIQ.Tag.ToString() + "=" + 1 + ";";
                else
                    message += TAKE_LIQ.Tag.ToString() + "=" + 0 + ";";
            }
            if (aRow[WORK_LIQ.Index].ToString().Length != 0)
            {
                if (aRow[WORK_LIQ.Index].Equals("True"))
                    message += WORK_LIQ.Tag.ToString() + "=" + 1 + ";";
                else
                    message += WORK_LIQ.Tag.ToString() + "=" + 0 + ";";
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}