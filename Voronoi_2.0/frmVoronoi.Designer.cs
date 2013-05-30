namespace Voronoi_2._0
{
    partial class frmVoronoi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVoronoi));
            this.grpInputOptions = new System.Windows.Forms.GroupBox();
            this.txtCellSize = new System.Windows.Forms.TextBox();
            this.lblSpatialReference = new System.Windows.Forms.Label();
            this.lblCellSizeFactor = new System.Windows.Forms.Label();
            this.chkSpatialJoin = new System.Windows.Forms.CheckBox();
            this.cboWeightField = new System.Windows.Forms.ComboBox();
            this.chkWeightField = new System.Windows.Forms.CheckBox();
            this.btnOpenShapefile = new System.Windows.Forms.Button();
            this.cboShapefile = new System.Windows.Forms.ComboBox();
            this.grpDistance = new System.Windows.Forms.GroupBox();
            this.chkAddDistRaster = new System.Windows.Forms.CheckBox();
            this.btnOpenDistRaster = new System.Windows.Forms.Button();
            this.txtOutDistRaster = new System.Windows.Forms.TextBox();
            this.grpVoronoiDiagram = new System.Windows.Forms.GroupBox();
            this.chkAddVoronoi = new System.Windows.Forms.CheckBox();
            this.btnOpenOutVoronoi = new System.Windows.Forms.Button();
            this.txtOutVoronoi = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.grpInputOptions.SuspendLayout();
            this.grpDistance.SuspendLayout();
            this.grpVoronoiDiagram.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInputOptions
            // 
            this.grpInputOptions.Controls.Add(this.txtCellSize);
            this.grpInputOptions.Controls.Add(this.lblSpatialReference);
            this.grpInputOptions.Controls.Add(this.lblCellSizeFactor);
            this.grpInputOptions.Controls.Add(this.chkSpatialJoin);
            this.grpInputOptions.Controls.Add(this.cboWeightField);
            this.grpInputOptions.Controls.Add(this.chkWeightField);
            this.grpInputOptions.Controls.Add(this.btnOpenShapefile);
            this.grpInputOptions.Controls.Add(this.cboShapefile);
            this.grpInputOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInputOptions.Location = new System.Drawing.Point(13, 13);
            this.grpInputOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpInputOptions.Name = "grpInputOptions";
            this.grpInputOptions.Padding = new System.Windows.Forms.Padding(4);
            this.grpInputOptions.Size = new System.Drawing.Size(383, 145);
            this.grpInputOptions.TabIndex = 1;
            this.grpInputOptions.TabStop = false;
            this.grpInputOptions.Text = "Input Feature Class";
            // 
            // txtCellSize
            // 
            this.txtCellSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellSize.Location = new System.Drawing.Point(141, 112);
            this.txtCellSize.Name = "txtCellSize";
            this.txtCellSize.Size = new System.Drawing.Size(170, 20);
            this.txtCellSize.TabIndex = 9;
            this.txtCellSize.TextChanged += new System.EventHandler(this.txtCellSize_TextChanged);
            // 
            // lblSpatialReference
            // 
            this.lblSpatialReference.AutoSize = true;
            this.lblSpatialReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpatialReference.Location = new System.Drawing.Point(316, 120);
            this.lblSpatialReference.Name = "lblSpatialReference";
            this.lblSpatialReference.Size = new System.Drawing.Size(0, 13);
            this.lblSpatialReference.TabIndex = 8;
            // 
            // lblCellSizeFactor
            // 
            this.lblCellSizeFactor.AutoSize = true;
            this.lblCellSizeFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCellSizeFactor.Location = new System.Drawing.Point(34, 122);
            this.lblCellSizeFactor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCellSizeFactor.Name = "lblCellSizeFactor";
            this.lblCellSizeFactor.Size = new System.Drawing.Size(100, 13);
            this.lblCellSizeFactor.TabIndex = 6;
            this.lblCellSizeFactor.Text = "Cell Size Factor:";
            // 
            // chkSpatialJoin
            // 
            this.chkSpatialJoin.AutoSize = true;
            this.chkSpatialJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSpatialJoin.Location = new System.Drawing.Point(9, 93);
            this.chkSpatialJoin.Margin = new System.Windows.Forms.Padding(4);
            this.chkSpatialJoin.Name = "chkSpatialJoin";
            this.chkSpatialJoin.Size = new System.Drawing.Size(113, 17);
            this.chkSpatialJoin.TabIndex = 4;
            this.chkSpatialJoin.Text = "Spatial Joining:";
            this.chkSpatialJoin.UseVisualStyleBackColor = true;
            this.chkSpatialJoin.CheckedChanged += new System.EventHandler(this.chkSpatialJoin_CheckedChanged);
            // 
            // cboWeightField
            // 
            this.cboWeightField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeightField.FormattingEnabled = true;
            this.cboWeightField.Location = new System.Drawing.Point(142, 64);
            this.cboWeightField.Margin = new System.Windows.Forms.Padding(4);
            this.cboWeightField.Name = "cboWeightField";
            this.cboWeightField.Size = new System.Drawing.Size(169, 23);
            this.cboWeightField.TabIndex = 3;
            this.cboWeightField.SelectedIndexChanged += new System.EventHandler(this.cboWeightField_SelectedIndexChanged);
            // 
            // chkWeightField
            // 
            this.chkWeightField.AutoSize = true;
            this.chkWeightField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWeightField.Location = new System.Drawing.Point(9, 68);
            this.chkWeightField.Margin = new System.Windows.Forms.Padding(4);
            this.chkWeightField.Name = "chkWeightField";
            this.chkWeightField.Size = new System.Drawing.Size(127, 17);
            this.chkWeightField.TabIndex = 2;
            this.chkWeightField.Text = "Use Weight Field:";
            this.chkWeightField.UseVisualStyleBackColor = true;
            this.chkWeightField.CheckedChanged += new System.EventHandler(this.chkWeightField_CheckedChanged);
            // 
            // btnOpenShapefile
            // 
            this.btnOpenShapefile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenShapefile.Image")));
            this.btnOpenShapefile.Location = new System.Drawing.Point(319, 26);
            this.btnOpenShapefile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenShapefile.Name = "btnOpenShapefile";
            this.btnOpenShapefile.Size = new System.Drawing.Size(56, 30);
            this.btnOpenShapefile.TabIndex = 1;
            this.btnOpenShapefile.UseVisualStyleBackColor = true;
            this.btnOpenShapefile.Click += new System.EventHandler(this.btnOpenShapefile_Click);
            // 
            // cboShapefile
            // 
            this.cboShapefile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboShapefile.FormattingEnabled = true;
            this.cboShapefile.Location = new System.Drawing.Point(9, 26);
            this.cboShapefile.Margin = new System.Windows.Forms.Padding(4);
            this.cboShapefile.Name = "cboShapefile";
            this.cboShapefile.Size = new System.Drawing.Size(302, 21);
            this.cboShapefile.TabIndex = 0;
            this.cboShapefile.SelectedIndexChanged += new System.EventHandler(this.cboShapefile_SelectedIndexChanged);
            // 
            // grpDistance
            // 
            this.grpDistance.Controls.Add(this.chkAddDistRaster);
            this.grpDistance.Controls.Add(this.btnOpenDistRaster);
            this.grpDistance.Controls.Add(this.txtOutDistRaster);
            this.grpDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDistance.Location = new System.Drawing.Point(13, 166);
            this.grpDistance.Margin = new System.Windows.Forms.Padding(4);
            this.grpDistance.Name = "grpDistance";
            this.grpDistance.Padding = new System.Windows.Forms.Padding(4);
            this.grpDistance.Size = new System.Drawing.Size(383, 114);
            this.grpDistance.TabIndex = 2;
            this.grpDistance.TabStop = false;
            this.grpDistance.Text = "Output Weighted Distance Raster";
            // 
            // chkAddDistRaster
            // 
            this.chkAddDistRaster.AutoSize = true;
            this.chkAddDistRaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddDistRaster.Location = new System.Drawing.Point(10, 85);
            this.chkAddDistRaster.Margin = new System.Windows.Forms.Padding(4);
            this.chkAddDistRaster.Name = "chkAddDistRaster";
            this.chkAddDistRaster.Size = new System.Drawing.Size(156, 17);
            this.chkAddDistRaster.TabIndex = 3;
            this.chkAddDistRaster.Text = "Add Output To ArcMap";
            this.chkAddDistRaster.UseVisualStyleBackColor = true;
            // 
            // btnOpenDistRaster
            // 
            this.btnOpenDistRaster.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDistRaster.Image")));
            this.btnOpenDistRaster.Location = new System.Drawing.Point(319, 49);
            this.btnOpenDistRaster.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDistRaster.Name = "btnOpenDistRaster";
            this.btnOpenDistRaster.Size = new System.Drawing.Size(56, 27);
            this.btnOpenDistRaster.TabIndex = 2;
            this.btnOpenDistRaster.UseVisualStyleBackColor = true;
            this.btnOpenDistRaster.Click += new System.EventHandler(this.btnOpenDistRaster_Click);
            // 
            // txtOutDistRaster
            // 
            this.txtOutDistRaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutDistRaster.Location = new System.Drawing.Point(10, 49);
            this.txtOutDistRaster.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutDistRaster.Name = "txtOutDistRaster";
            this.txtOutDistRaster.Size = new System.Drawing.Size(306, 20);
            this.txtOutDistRaster.TabIndex = 0;
            this.txtOutDistRaster.TextChanged += new System.EventHandler(this.txtOutDistRaster_TextChanged);
            // 
            // grpVoronoiDiagram
            // 
            this.grpVoronoiDiagram.Controls.Add(this.chkAddVoronoi);
            this.grpVoronoiDiagram.Controls.Add(this.btnOpenOutVoronoi);
            this.grpVoronoiDiagram.Controls.Add(this.txtOutVoronoi);
            this.grpVoronoiDiagram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpVoronoiDiagram.Location = new System.Drawing.Point(13, 288);
            this.grpVoronoiDiagram.Margin = new System.Windows.Forms.Padding(4);
            this.grpVoronoiDiagram.Name = "grpVoronoiDiagram";
            this.grpVoronoiDiagram.Padding = new System.Windows.Forms.Padding(4);
            this.grpVoronoiDiagram.Size = new System.Drawing.Size(383, 122);
            this.grpVoronoiDiagram.TabIndex = 3;
            this.grpVoronoiDiagram.TabStop = false;
            this.grpVoronoiDiagram.Text = "Output Voronoi Diagram (Shapefile)";
            // 
            // chkAddVoronoi
            // 
            this.chkAddVoronoi.AutoSize = true;
            this.chkAddVoronoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddVoronoi.Location = new System.Drawing.Point(10, 85);
            this.chkAddVoronoi.Margin = new System.Windows.Forms.Padding(4);
            this.chkAddVoronoi.Name = "chkAddVoronoi";
            this.chkAddVoronoi.Size = new System.Drawing.Size(156, 17);
            this.chkAddVoronoi.TabIndex = 3;
            this.chkAddVoronoi.Text = "Add Output To ArcMap";
            this.chkAddVoronoi.UseVisualStyleBackColor = true;
            // 
            // btnOpenOutVoronoi
            // 
            this.btnOpenOutVoronoi.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenOutVoronoi.Image")));
            this.btnOpenOutVoronoi.Location = new System.Drawing.Point(319, 49);
            this.btnOpenOutVoronoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenOutVoronoi.Name = "btnOpenOutVoronoi";
            this.btnOpenOutVoronoi.Size = new System.Drawing.Size(56, 27);
            this.btnOpenOutVoronoi.TabIndex = 2;
            this.btnOpenOutVoronoi.UseVisualStyleBackColor = true;
            this.btnOpenOutVoronoi.Click += new System.EventHandler(this.btnOpenOutVoronoi_Click);
            // 
            // txtOutVoronoi
            // 
            this.txtOutVoronoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutVoronoi.Location = new System.Drawing.Point(10, 49);
            this.txtOutVoronoi.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutVoronoi.Name = "txtOutVoronoi";
            this.txtOutVoronoi.Size = new System.Drawing.Size(306, 20);
            this.txtOutVoronoi.TabIndex = 0;
            this.txtOutVoronoi.TextChanged += new System.EventHandler(this.txtOutVoronoi_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(296, 438);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(188, 438);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(13, 438);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(100, 35);
            this.btnHelp.TabIndex = 5;
            this.btnHelp.Text = "&Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // frmVoronoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 486);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.grpVoronoiDiagram);
            this.Controls.Add(this.grpDistance);
            this.Controls.Add(this.grpInputOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVoronoi";
            this.Text = "Generate Voronoi Diagram";
            this.grpInputOptions.ResumeLayout(false);
            this.grpInputOptions.PerformLayout();
            this.grpDistance.ResumeLayout(false);
            this.grpDistance.PerformLayout();
            this.grpVoronoiDiagram.ResumeLayout(false);
            this.grpVoronoiDiagram.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInputOptions;
        private System.Windows.Forms.Label lblCellSizeFactor;
        private System.Windows.Forms.CheckBox chkSpatialJoin;
        private System.Windows.Forms.ComboBox cboWeightField;
        private System.Windows.Forms.CheckBox chkWeightField;
        private System.Windows.Forms.Button btnOpenShapefile;
        private System.Windows.Forms.ComboBox cboShapefile;
        private System.Windows.Forms.GroupBox grpDistance;
        private System.Windows.Forms.CheckBox chkAddDistRaster;
        private System.Windows.Forms.Button btnOpenDistRaster;
        private System.Windows.Forms.TextBox txtOutDistRaster;
        private System.Windows.Forms.GroupBox grpVoronoiDiagram;
        private System.Windows.Forms.CheckBox chkAddVoronoi;
        private System.Windows.Forms.Button btnOpenOutVoronoi;
        private System.Windows.Forms.TextBox txtOutVoronoi;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label lblSpatialReference;
        private System.Windows.Forms.TextBox txtCellSize;
    }
}