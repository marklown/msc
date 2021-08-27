
namespace msc
{
    partial class MainForm
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
            this.tbObjectID = new System.Windows.Forms.TextBox();
            this.lblObjectID = new System.Windows.Forms.Label();
            this.lblFOV = new System.Windows.Forms.Label();
            this.nudFOV = new System.Windows.Forms.NumericUpDown();
            this.nudLimitingMag = new System.Windows.Forms.NumericUpDown();
            this.lblLimitingMag = new System.Windows.Forms.Label();
            this.btnPlot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.starChart1 = new msc.StarChart();
            this.cbLabelDsos = new System.Windows.Forms.CheckBox();
            this.nudObjectsLimitingMag = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudFOV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimitingMag)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjectsLimitingMag)).BeginInit();
            this.SuspendLayout();
            // 
            // tbObjectID
            // 
            this.tbObjectID.Location = new System.Drawing.Point(116, 15);
            this.tbObjectID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbObjectID.Name = "tbObjectID";
            this.tbObjectID.Size = new System.Drawing.Size(132, 22);
            this.tbObjectID.TabIndex = 0;
            this.tbObjectID.Text = "NGC 869";
            // 
            // lblObjectID
            // 
            this.lblObjectID.AutoSize = true;
            this.lblObjectID.Location = new System.Drawing.Point(16, 18);
            this.lblObjectID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectID.Name = "lblObjectID";
            this.lblObjectID.Size = new System.Drawing.Size(66, 16);
            this.lblObjectID.TabIndex = 1;
            this.lblObjectID.Text = "Object ID:";
            // 
            // lblFOV
            // 
            this.lblFOV.AutoSize = true;
            this.lblFOV.Location = new System.Drawing.Point(16, 49);
            this.lblFOV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFOV.Name = "lblFOV";
            this.lblFOV.Size = new System.Drawing.Size(38, 16);
            this.lblFOV.TabIndex = 2;
            this.lblFOV.Text = "FOV:";
            // 
            // nudFOV
            // 
            this.nudFOV.DecimalPlaces = 1;
            this.nudFOV.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFOV.Location = new System.Drawing.Point(116, 47);
            this.nudFOV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudFOV.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFOV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFOV.Name = "nudFOV";
            this.nudFOV.Size = new System.Drawing.Size(133, 22);
            this.nudFOV.TabIndex = 3;
            this.nudFOV.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // nudLimitingMag
            // 
            this.nudLimitingMag.DecimalPlaces = 1;
            this.nudLimitingMag.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudLimitingMag.Location = new System.Drawing.Point(116, 79);
            this.nudLimitingMag.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudLimitingMag.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudLimitingMag.Name = "nudLimitingMag";
            this.nudLimitingMag.Size = new System.Drawing.Size(133, 22);
            this.nudLimitingMag.TabIndex = 4;
            this.nudLimitingMag.Value = new decimal(new int[] {
            140,
            0,
            0,
            65536});
            // 
            // lblLimitingMag
            // 
            this.lblLimitingMag.AutoSize = true;
            this.lblLimitingMag.Location = new System.Drawing.Point(16, 81);
            this.lblLimitingMag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLimitingMag.Name = "lblLimitingMag";
            this.lblLimitingMag.Size = new System.Drawing.Size(86, 16);
            this.lblLimitingMag.TabIndex = 5;
            this.lblLimitingMag.Text = "Limiting Mag:";
            // 
            // btnPlot
            // 
            this.btnPlot.Location = new System.Drawing.Point(20, 123);
            this.btnPlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(229, 28);
            this.btnPlot.TabIndex = 6;
            this.btnPlot.Text = "Plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.starChart1);
            this.panel1.Location = new System.Drawing.Point(271, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 613);
            this.panel1.TabIndex = 8;
            // 
            // starChart1
            // 
            this.starChart1.BackColor = System.Drawing.Color.Black;
            this.starChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.starChart1.Location = new System.Drawing.Point(0, 0);
            this.starChart1.Margin = new System.Windows.Forms.Padding(4);
            this.starChart1.Name = "starChart1";
            this.starChart1.Size = new System.Drawing.Size(873, 613);
            this.starChart1.TabIndex = 0;
            // 
            // cbLabelDsos
            // 
            this.cbLabelDsos.AutoSize = true;
            this.cbLabelDsos.Location = new System.Drawing.Point(20, 173);
            this.cbLabelDsos.Name = "cbLabelDsos";
            this.cbLabelDsos.Size = new System.Drawing.Size(158, 20);
            this.cbLabelDsos.TabIndex = 9;
            this.cbLabelDsos.Text = "Label objects <= mag:";
            this.cbLabelDsos.UseVisualStyleBackColor = true;
            this.cbLabelDsos.CheckedChanged += new System.EventHandler(this.cbLabelDsos_CheckedChanged);
            // 
            // nudObjectsLimitingMag
            // 
            this.nudObjectsLimitingMag.DecimalPlaces = 1;
            this.nudObjectsLimitingMag.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudObjectsLimitingMag.Location = new System.Drawing.Point(185, 171);
            this.nudObjectsLimitingMag.Margin = new System.Windows.Forms.Padding(4);
            this.nudObjectsLimitingMag.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudObjectsLimitingMag.Name = "nudObjectsLimitingMag";
            this.nudObjectsLimitingMag.Size = new System.Drawing.Size(63, 22);
            this.nudObjectsLimitingMag.TabIndex = 10;
            this.nudObjectsLimitingMag.Value = new decimal(new int[] {
            110,
            0,
            0,
            65536});
            this.nudObjectsLimitingMag.ValueChanged += new System.EventHandler(this.nudObjectsLimitingMag_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 640);
            this.Controls.Add(this.nudObjectsLimitingMag);
            this.Controls.Add(this.cbLabelDsos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.lblLimitingMag);
            this.Controls.Add(this.nudLimitingMag);
            this.Controls.Add(this.nudFOV);
            this.Controls.Add(this.lblFOV);
            this.Controls.Add(this.lblObjectID);
            this.Controls.Add(this.tbObjectID);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudFOV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimitingMag)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjectsLimitingMag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbObjectID;
        private System.Windows.Forms.Label lblObjectID;
        private System.Windows.Forms.Label lblFOV;
        private System.Windows.Forms.NumericUpDown nudFOV;
        private System.Windows.Forms.NumericUpDown nudLimitingMag;
        private System.Windows.Forms.Label lblLimitingMag;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.Panel panel1;
        private StarChart starChart1;
        private System.Windows.Forms.CheckBox cbLabelDsos;
        private System.Windows.Forms.NumericUpDown nudObjectsLimitingMag;
    }
}

