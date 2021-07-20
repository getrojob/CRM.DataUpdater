using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTG.Climb.XRM.Connect;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;
using SourceGrid;
using System.Security.Principal;

namespace CRM.DataUpdater.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        //private void dataGridViewData_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //DataObject o = (DataObject)Clipboard.GetDataObject();
        //    //if (o.GetDataPresent(DataFormats.Text))
        //    //{
        //    //    if (dataGridViewData.RowCount > 0)
        //    //        dataGridViewData.Rows.Clear();

        //    //    if (dataGridViewData.ColumnCount > 0)
        //    //        dataGridViewData.Columns.Clear();

        //    //    bool columnsAdded = false;
        //    //    string[] pastedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
        //    //    foreach (string pastedRow in pastedRows)
        //    //    {
        //    //        string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });

        //    //        if (!columnsAdded)
        //    //        {
        //    //            for (int i = 0; i < pastedRowCells.Length; i++)
        //    //                dataGridViewData.Columns.Add("col" + i, pastedRowCells[i]);

        //    //            columnsAdded = true;
        //    //            continue;
        //    //        }

        //    //        dataGridViewData.Rows.Add();
        //    //        int myRowIndex = dataGridViewData.Rows.Count - 1;

        //    //        using (DataGridViewRow myDataGridViewRow = dataGridViewData.Rows[myRowIndex])
        //    //        {
        //    //            for (int i = 0; i < pastedRowCells.Length; i++)
        //    //                myDataGridViewRow.Cells[i].Value = pastedRowCells[i];
        //    //        }
        //    //    }
        //    //}
        //}

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        //private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    PasteClipboardValue();
        //}

        //private void PasteClipboardValue()
        //{
        //    //Get the starting Cell
        //    DataGridViewCell startCell = GetStartCell(dataGridViewData);
        //    //Get the clipboard value in a dictionary
        //    Dictionary<int, Dictionary<int, string>> cbValue =
        //            ClipBoardValues(Clipboard.GetText());

        //    int iRowIndex = startCell.RowIndex;
        //    foreach (int rowKey in cbValue.Keys)
        //    {
        //        int iColIndex = startCell.ColumnIndex;
        //        foreach (int cellKey in cbValue[rowKey].Keys)
        //        {
        //            //Check if the index is within the limit
        //            if (iColIndex <= dataGridViewData.Columns.Count - 1)
        //            {
        //                if(iRowIndex > dataGridViewData.Rows.Count - 1)
        //                {
        //                    dataGridViewData.Rows.Add();
        //                }

        //                DataGridViewCell cell = dataGridViewData.Rows[0].Cells[iColIndex];

        //                cell.Value = cbValue[rowKey][cellKey].ToString().TrimEnd("\r\n".ToCharArray());
        //            }
        //            iColIndex++;
        //        }
        //        iRowIndex++;
        //    }
        //}

        private DataGridViewCell GetStartCell(DataGridView dgView)
        {
            //get the smallest row,column index
            if (dgView.SelectedCells.Count == 0)
                return dgView[0, 0];

            int rowIndex = dgView.Rows.Count - 1;
            int colIndex = dgView.Columns.Count - 1;

            foreach (DataGridViewCell dgvCell in dgView.SelectedCells)
            {
                if (dgvCell.RowIndex < rowIndex)
                    rowIndex = dgvCell.RowIndex;
                if (dgvCell.ColumnIndex < colIndex)
                    colIndex = dgvCell.ColumnIndex;
            }

            return dgView[colIndex, rowIndex];
        }

        private Dictionary<int, Dictionary<int, string>> ClipBoardValues(string clipboardValue)
        {
            Dictionary<int, Dictionary<int, string>>
            copyValues = new Dictionary<int, Dictionary<int, string>>();

            String[] lines = clipboardValue.Split('\n');

            for (int i = 0; i <= lines.Length - 1; i++)
            {
                copyValues[i] = new Dictionary<int, string>();
                String[] lineContent = lines[i].Split('\t');

                //if an empty cell value copied, then set the dictionary with an empty string
                //else Set value to dictionary
                if (lineContent.Length == 0)
                    copyValues[i][0] = string.Empty;
                else
                {
                    for (int j = 0; j <= lineContent.Length - 1; j++)
                        copyValues[i][j] = lineContent[j];
                }
            }
            return copyValues;
        }

        private List<ImportModel> dataImportList = new List<ImportModel>();
        private DevAge.ComponentModel.BoundList<ImportModel> mBoundList;
        private void Main_Load(object sender, EventArgs e)
        {
            //gridSource.BorderStyle = BorderStyle.FixedSingle;
            //gridSource.ColumnsCount = 7;
            //gridSource.FixedRows = 1;
            //gridSource.Rows.Insert(0);
            //gridSource[0, 0] = new SourceGrid.Cells.ColumnHeader("entityname");
            //gridSource[0, 1] = new SourceGrid.Cells.ColumnHeader("entityid");
            //gridSource[0, 2] = new SourceGrid.Cells.ColumnHeader("attributename");
            //gridSource[0, 3] = new SourceGrid.Cells.ColumnHeader("attributetype");
            //gridSource[0, 4] = new SourceGrid.Cells.ColumnHeader("lookupentitylogicalname");
            //gridSource[0, 5] = new SourceGrid.Cells.ColumnHeader("attributevalue");
            //gridSource[0, 6] = new SourceGrid.Cells.ColumnHeader("newrecordkey");

            //gridSource.Rows.InsertRange(1, 5);

            //int i = 1;
            //while(i < 2)
            //{
            //    gridSource[i, 0] = new SourceGrid.Cells.Cell("", typeof(string));
            //    gridSource[i, 1] = new SourceGrid.Cells.Cell("", typeof(string));
            //    gridSource[i, 2] = new SourceGrid.Cells.Cell("", typeof(string));
            //    gridSource[i, 3] = new SourceGrid.Cells.Cell("", typeof(string));
            //    gridSource[i, 4] = new SourceGrid.Cells.Cell("", typeof(string));
            //    gridSource[i, 5] = new SourceGrid.Cells.Cell("", typeof(string));
            //    gridSource[i, 6] = new SourceGrid.Cells.Cell("", typeof(string));
            //    i++;
            //}

            //gridSource.ClipboardMode = SourceGrid.ClipboardMode.All;
            //gridSource.ClipboardUseOnlyActivePosition = true;

            dataImportList.Add(new ImportModel
            {
               
            });

            mBoundList = new DevAge.ComponentModel.BoundList<ImportModel>(dataImportList);
            mBoundList.ListChanged += new ListChangedEventHandler(mBoundList_ListChanged);

            gridSource.Columns.Add("entityname", "entityname", typeof(string));
            gridSource.Columns.Add("entityid", "entityid", typeof(string));
            gridSource.Columns.Add("attributename", "attributename", typeof(string));
            gridSource.Columns.Add("attributetype", "attributetype", typeof(string));
            gridSource.Columns.Add("lookupentitylogicalname", "lookupentitylogicalname", typeof(string));
            gridSource.Columns.Add("attributevalue", "attributevalue", typeof(string));
            gridSource.Columns.Add("newrecordkey", "newrecordkey", typeof(string));

            gridSource.DataSource = mBoundList;
        }

        private void mBoundList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void btnExecuteFetch_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                var connector = new XrmConnector(txtUserName.Text, txtPassword.Text, txtOrganizationUrl.Text);

                if(connector.IsFaulted)
                {
                    lblFetchResult.Text = "Cannot connect: " + connector.FaultMessage;
                    return;
                }

                lblFetchResult.Text = "Connected to: " + connector.Service.ConnectedOrgFriendlyName;
                lblFetchResult.Text += "\nExecuting fetch...";

                var result = connector.Service.RetrieveMultiple(new FetchExpression(txtFetchXml.Text));
                System.Array array = Array.CreateInstance(typeof(string), result.Entities.Count, result.Entities.FirstOrDefault().Attributes.Count);

                if(result != null && result.Entities != null && result.Entities.Count > 0)
                {
                    //headers
                    foreach (var attribute in result.Entities.FirstOrDefault().Attributes)
                    {
                        gridFetchResults.Columns.Add(new ColumnInfo(gridFetchResults));
                    }
                }

                gridFetchResults.DataSource = array;
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                lblConnectionTestResult.Text = "";

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                var service = new XrmConnector(txtUserName.Text, txtPassword.Text, txtOrganizationUrl.Text).Service;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

                if (!String.IsNullOrEmpty(service.LastCrmError))
                {
                    lblConnectionTestResult.Text = "Failure: " + service.LastCrmError;
                }
                else
                {
                    lblConnectionTestResult.Text = "Success.";
                }
            }
            catch (Exception ex)
            {
                lblConnectionTestResult.Text = "Failure: " + ex.Message;
            }            
        }
    }
}
