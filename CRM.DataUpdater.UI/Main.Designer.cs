namespace CRM.DataUpdater.UI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControlTabs = new System.Windows.Forms.TabControl();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.lblConnectionTestResult = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtOrganizationUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabData = new System.Windows.Forms.TabPage();
            this.gridSource = new SourceGrid.DataGrid();
            this.tabSourceFileOrFetch = new System.Windows.Forms.TabPage();
            this.lblFetchResult = new System.Windows.Forms.Label();
            this.btnExecuteFetch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFetchXml = new System.Windows.Forms.TextBox();
            this.btnImportFile = new System.Windows.Forms.Button();
            this.btnSearchFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabFetchResults = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gridFetchResults = new SourceGrid.ArrayGrid();
            this.tabControlTabs.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabSourceFileOrFetch.SuspendLayout();
            this.tabFetchResults.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlTabs
            // 
            this.tabControlTabs.Controls.Add(this.tabOptions);
            this.tabControlTabs.Controls.Add(this.tabData);
            this.tabControlTabs.Controls.Add(this.tabSourceFileOrFetch);
            this.tabControlTabs.Controls.Add(this.tabFetchResults);
            this.tabControlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTabs.HotTrack = true;
            this.tabControlTabs.Location = new System.Drawing.Point(0, 0);
            this.tabControlTabs.Name = "tabControlTabs";
            this.tabControlTabs.SelectedIndex = 0;
            this.tabControlTabs.Size = new System.Drawing.Size(784, 562);
            this.tabControlTabs.TabIndex = 0;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.lblConnectionTestResult);
            this.tabOptions.Controls.Add(this.btnTestConnection);
            this.tabOptions.Controls.Add(this.txtPassword);
            this.tabOptions.Controls.Add(this.txtUserName);
            this.tabOptions.Controls.Add(this.txtOrganizationUrl);
            this.tabOptions.Controls.Add(this.label5);
            this.tabOptions.Controls.Add(this.label4);
            this.tabOptions.Controls.Add(this.label3);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(776, 536);
            this.tabOptions.TabIndex = 0;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // lblConnectionTestResult
            // 
            this.lblConnectionTestResult.AutoSize = true;
            this.lblConnectionTestResult.Location = new System.Drawing.Point(269, 92);
            this.lblConnectionTestResult.Name = "lblConnectionTestResult";
            this.lblConnectionTestResult.Size = new System.Drawing.Size(0, 13);
            this.lblConnectionTestResult.TabIndex = 7;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(110, 87);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(153, 23);
            this.btnTestConnection.TabIndex = 6;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(110, 61);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(153, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(110, 34);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(153, 20);
            this.txtUserName.TabIndex = 4;
            // 
            // txtOrganizationUrl
            // 
            this.txtOrganizationUrl.Location = new System.Drawing.Point(110, 7);
            this.txtOrganizationUrl.Name = "txtOrganizationUrl";
            this.txtOrganizationUrl.Size = new System.Drawing.Size(409, 20);
            this.txtOrganizationUrl.TabIndex = 3;
            this.txtOrganizationUrl.Text = "https://digital.crm2.dynamics.com";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Urser name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Organization URL:";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.gridSource);
            this.tabData.Location = new System.Drawing.Point(4, 22);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(776, 536);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // gridSource
            // 
            this.gridSource.AutoStretchColumnsToFitWidth = true;
            this.gridSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridSource.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut) 
            | SourceGrid.ClipboardMode.Paste) 
            | SourceGrid.ClipboardMode.Delete)));
            this.gridSource.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSource.EnableSort = false;
            this.gridSource.FixedRows = 1;
            this.gridSource.Location = new System.Drawing.Point(3, 3);
            this.gridSource.Name = "gridSource";
            this.gridSource.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridSource.Size = new System.Drawing.Size(770, 530);
            this.gridSource.TabIndex = 0;
            this.gridSource.TabStop = true;
            this.gridSource.ToolTipText = "";
            // 
            // tabSourceFileOrFetch
            // 
            this.tabSourceFileOrFetch.Controls.Add(this.lblFetchResult);
            this.tabSourceFileOrFetch.Controls.Add(this.btnExecuteFetch);
            this.tabSourceFileOrFetch.Controls.Add(this.label2);
            this.tabSourceFileOrFetch.Controls.Add(this.txtFetchXml);
            this.tabSourceFileOrFetch.Controls.Add(this.btnImportFile);
            this.tabSourceFileOrFetch.Controls.Add(this.btnSearchFile);
            this.tabSourceFileOrFetch.Controls.Add(this.label1);
            this.tabSourceFileOrFetch.Controls.Add(this.textBox1);
            this.tabSourceFileOrFetch.Location = new System.Drawing.Point(4, 22);
            this.tabSourceFileOrFetch.Name = "tabSourceFileOrFetch";
            this.tabSourceFileOrFetch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSourceFileOrFetch.Size = new System.Drawing.Size(776, 536);
            this.tabSourceFileOrFetch.TabIndex = 2;
            this.tabSourceFileOrFetch.Text = "Source";
            this.tabSourceFileOrFetch.UseVisualStyleBackColor = true;
            // 
            // lblFetchResult
            // 
            this.lblFetchResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFetchResult.Location = new System.Drawing.Point(612, 87);
            this.lblFetchResult.Name = "lblFetchResult";
            this.lblFetchResult.Size = new System.Drawing.Size(158, 441);
            this.lblFetchResult.TabIndex = 7;
            // 
            // btnExecuteFetch
            // 
            this.btnExecuteFetch.Location = new System.Drawing.Point(612, 60);
            this.btnExecuteFetch.Name = "btnExecuteFetch";
            this.btnExecuteFetch.Size = new System.Drawing.Size(158, 20);
            this.btnExecuteFetch.TabIndex = 6;
            this.btnExecuteFetch.Text = "Execute fetch";
            this.btnExecuteFetch.UseVisualStyleBackColor = true;
            this.btnExecuteFetch.Click += new System.EventHandler(this.btnExecuteFetch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fetch XML:";
            // 
            // txtFetchXml
            // 
            this.txtFetchXml.Location = new System.Drawing.Point(9, 60);
            this.txtFetchXml.Multiline = true;
            this.txtFetchXml.Name = "txtFetchXml";
            this.txtFetchXml.Size = new System.Drawing.Size(597, 468);
            this.txtFetchXml.TabIndex = 4;
            // 
            // btnImportFile
            // 
            this.btnImportFile.Location = new System.Drawing.Point(693, 6);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(75, 20);
            this.btnImportFile.TabIndex = 3;
            this.btnImportFile.Text = "Import";
            this.btnImportFile.UseVisualStyleBackColor = true;
            // 
            // btnSearchFile
            // 
            this.btnSearchFile.Location = new System.Drawing.Point(612, 6);
            this.btnSearchFile.Name = "btnSearchFile";
            this.btnSearchFile.Size = new System.Drawing.Size(75, 20);
            this.btnSearchFile.TabIndex = 2;
            this.btnSearchFile.Text = "Search";
            this.btnSearchFile.UseVisualStyleBackColor = true;
            this.btnSearchFile.Click += new System.EventHandler(this.btnSearchFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(44, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(562, 20);
            this.textBox1.TabIndex = 0;
            // 
            // tabFetchResults
            // 
            this.tabFetchResults.Controls.Add(this.gridFetchResults);
            this.tabFetchResults.Location = new System.Drawing.Point(4, 22);
            this.tabFetchResults.Name = "tabFetchResults";
            this.tabFetchResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabFetchResults.Size = new System.Drawing.Size(776, 536);
            this.tabFetchResults.TabIndex = 3;
            this.tabFetchResults.Text = "Fetch Results";
            this.tabFetchResults.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // gridFetchResults
            // 
            this.gridFetchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFetchResults.EnableSort = true;
            this.gridFetchResults.Location = new System.Drawing.Point(3, 3);
            this.gridFetchResults.Name = "gridFetchResults";
            this.gridFetchResults.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridFetchResults.Size = new System.Drawing.Size(770, 530);
            this.gridFetchResults.TabIndex = 0;
            this.gridFetchResults.TabStop = true;
            this.gridFetchResults.ToolTipText = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControlTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CRM Data Updater";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControlTabs.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tabSourceFileOrFetch.ResumeLayout(false);
            this.tabSourceFileOrFetch.PerformLayout();
            this.tabFetchResults.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlTabs;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabSourceFileOrFetch;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSearchFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnImportFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFetchXml;
        private System.Windows.Forms.Label lblFetchResult;
        private System.Windows.Forms.Button btnExecuteFetch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabFetchResults;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtOrganizationUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblConnectionTestResult;
        private System.Windows.Forms.Button btnTestConnection;
        private SourceGrid.DataGrid gridSource;
        private SourceGrid.ArrayGrid gridFetchResults;
    }
}

