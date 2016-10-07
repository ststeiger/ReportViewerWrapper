namespace ClientReport
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designer-Variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Alle derzeit verwendeten Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">'true', wenn verwaltete Ressourcen freigegeben werden sollen, andernfalls 'false'.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Von Windows Form Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designer-Unterstützung - Ändern Sie den
        /// Inhalt dieser Methode nicht mit dem Code-Editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.RoomPlanningDataSet = new ClientReport.RoomPlanningDataSet();
            this.T_BenutzerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.T_BenutzerTableAdapter = new ClientReport.RoomPlanningDataSetTableAdapters.T_BenutzerTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.RoomPlanningDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.T_BenutzerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.T_BenutzerBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ClientReport.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // RoomPlanningDataSet
            // 
            this.RoomPlanningDataSet.DataSetName = "RoomPlanningDataSet";
            this.RoomPlanningDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // T_BenutzerBindingSource
            // 
            this.T_BenutzerBindingSource.DataMember = "T_Benutzer";
            this.T_BenutzerBindingSource.DataSource = this.RoomPlanningDataSet;
            // 
            // T_BenutzerTableAdapter
            // 
            this.T_BenutzerTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RoomPlanningDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.T_BenutzerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource T_BenutzerBindingSource;
        private RoomPlanningDataSet RoomPlanningDataSet;
        private RoomPlanningDataSetTableAdapters.T_BenutzerTableAdapter T_BenutzerTableAdapter;
    }
}

