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
            this.lblxInput = new System.Windows.Forms.Label();
            this.lblOutputExtent = new System.Windows.Forms.Label();
            this.cboOutputExtent = new System.Windows.Forms.ComboBox();
            this.cboWeightFieldPolygon = new System.Windows.Forms.ComboBox();
            this.chkWeightFieldPolygon = new System.Windows.Forms.CheckBox();
            this.cboWeightFieldLine = new System.Windows.Forms.ComboBox();
            this.chkWeightFieldLine = new System.Windows.Forms.CheckBox();
            this.chkPolyEnabled = new System.Windows.Forms.CheckBox();
            this.chkLineEnabled = new System.Windows.Forms.CheckBox();
            this.chkPointEnabled = new System.Windows.Forms.CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpenPolyFC = new System.Windows.Forms.Button();
            this.cboFCPoly = new System.Windows.Forms.ComboBox();
            this.btnOpenLineFC = new System.Windows.Forms.Button();
            this.cboFCLine = new System.Windows.Forms.ComboBox();
            this.txtCellSize = new System.Windows.Forms.TextBox();
            this.lblSpatialReference = new System.Windows.Forms.Label();
            this.lblCellSize = new System.Windows.Forms.Label();
            this.cboWeightFieldPoint = new System.Windows.Forms.ComboBox();
            this.chkWeightFieldPoint = new System.Windows.Forms.CheckBox();
            this.btnOpenPointFC = new System.Windows.Forms.Button();
            this.cboFCPoint = new System.Windows.Forms.ComboBox();
            this.chkSpatialJoin = new System.Windows.Forms.CheckBox();
            this.grpDistance = new System.Windows.Forms.GroupBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.chkAddDistRaster = new System.Windows.Forms.CheckBox();
            this.btnOpenDistRaster = new System.Windows.Forms.Button();
            this.txtOutDistRaster = new System.Windows.Forms.TextBox();
            this.grpVoronoiDiagram = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.chkAddVoronoi = new System.Windows.Forms.CheckBox();
            this.btnOpenOutVoronoi = new System.Windows.Forms.Button();
            this.txtOutVoronoi = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.grpInputOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpDistance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.grpVoronoiDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInputOptions
            // 
            this.grpInputOptions.Controls.Add(this.lblxInput);
            this.grpInputOptions.Controls.Add(this.lblOutputExtent);
            this.grpInputOptions.Controls.Add(this.cboOutputExtent);
            this.grpInputOptions.Controls.Add(this.cboWeightFieldPolygon);
            this.grpInputOptions.Controls.Add(this.chkWeightFieldPolygon);
            this.grpInputOptions.Controls.Add(this.cboWeightFieldLine);
            this.grpInputOptions.Controls.Add(this.chkWeightFieldLine);
            this.grpInputOptions.Controls.Add(this.chkPolyEnabled);
            this.grpInputOptions.Controls.Add(this.chkLineEnabled);
            this.grpInputOptions.Controls.Add(this.chkPointEnabled);
            this.grpInputOptions.Controls.Add(this.pictureBox3);
            this.grpInputOptions.Controls.Add(this.pictureBox2);
            this.grpInputOptions.Controls.Add(this.pictureBox1);
            this.grpInputOptions.Controls.Add(this.btnOpenPolyFC);
            this.grpInputOptions.Controls.Add(this.cboFCPoly);
            this.grpInputOptions.Controls.Add(this.btnOpenLineFC);
            this.grpInputOptions.Controls.Add(this.cboFCLine);
            this.grpInputOptions.Controls.Add(this.txtCellSize);
            this.grpInputOptions.Controls.Add(this.lblSpatialReference);
            this.grpInputOptions.Controls.Add(this.lblCellSize);
            this.grpInputOptions.Controls.Add(this.cboWeightFieldPoint);
            this.grpInputOptions.Controls.Add(this.chkWeightFieldPoint);
            this.grpInputOptions.Controls.Add(this.btnOpenPointFC);
            this.grpInputOptions.Controls.Add(this.cboFCPoint);
            this.grpInputOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInputOptions.Location = new System.Drawing.Point(13, 13);
            this.grpInputOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpInputOptions.Name = "grpInputOptions";
            this.grpInputOptions.Padding = new System.Windows.Forms.Padding(4);
            this.grpInputOptions.Size = new System.Drawing.Size(383, 314);
            this.grpInputOptions.TabIndex = 1;
            this.grpInputOptions.TabStop = false;
            this.grpInputOptions.Text = "Input Feature Class(es)";
            // 
            // lblxInput
            // 
            this.lblxInput.AutoSize = true;
            this.lblxInput.Location = new System.Drawing.Point(284, 283);
            this.lblxInput.Name = "lblxInput";
            this.lblxInput.Size = new System.Drawing.Size(50, 15);
            this.lblxInput.TabIndex = 26;
            this.lblxInput.Text = "x Input";
            // 
            // lblOutputExtent
            // 
            this.lblOutputExtent.AutoSize = true;
            this.lblOutputExtent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputExtent.Location = new System.Drawing.Point(8, 285);
            this.lblOutputExtent.Name = "lblOutputExtent";
            this.lblOutputExtent.Size = new System.Drawing.Size(89, 13);
            this.lblOutputExtent.TabIndex = 25;
            this.lblOutputExtent.Text = "Output Extent:";
            // 
            // cboOutputExtent
            // 
            this.cboOutputExtent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOutputExtent.FormattingEnabled = true;
            this.cboOutputExtent.Items.AddRange(new object[] {
            "1",
            "1.1",
            "1.2",
            "1.5",
            "2"});
            this.cboOutputExtent.Location = new System.Drawing.Point(111, 282);
            this.cboOutputExtent.Name = "cboOutputExtent";
            this.cboOutputExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboOutputExtent.Size = new System.Drawing.Size(170, 21);
            this.cboOutputExtent.TabIndex = 24;
            // 
            // cboWeightFieldPolygon
            // 
            this.cboWeightFieldPolygon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeightFieldPolygon.FormattingEnabled = true;
            this.cboWeightFieldPolygon.Location = new System.Drawing.Point(190, 213);
            this.cboWeightFieldPolygon.Margin = new System.Windows.Forms.Padding(4);
            this.cboWeightFieldPolygon.Name = "cboWeightFieldPolygon";
            this.cboWeightFieldPolygon.Size = new System.Drawing.Size(121, 23);
            this.cboWeightFieldPolygon.TabIndex = 23;
            // 
            // chkWeightFieldPolygon
            // 
            this.chkWeightFieldPolygon.AutoSize = true;
            this.chkWeightFieldPolygon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWeightFieldPolygon.Location = new System.Drawing.Point(59, 217);
            this.chkWeightFieldPolygon.Margin = new System.Windows.Forms.Padding(4);
            this.chkWeightFieldPolygon.Name = "chkWeightFieldPolygon";
            this.chkWeightFieldPolygon.Size = new System.Drawing.Size(127, 17);
            this.chkWeightFieldPolygon.TabIndex = 22;
            this.chkWeightFieldPolygon.Text = "Use Weight Field:";
            this.chkWeightFieldPolygon.UseVisualStyleBackColor = true;
            this.chkWeightFieldPolygon.CheckedChanged += new System.EventHandler(this.chkWeightFieldPolygon_CheckedChanged);
            // 
            // cboWeightFieldLine
            // 
            this.cboWeightFieldLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeightFieldLine.FormattingEnabled = true;
            this.cboWeightFieldLine.Location = new System.Drawing.Point(190, 138);
            this.cboWeightFieldLine.Margin = new System.Windows.Forms.Padding(4);
            this.cboWeightFieldLine.Name = "cboWeightFieldLine";
            this.cboWeightFieldLine.Size = new System.Drawing.Size(121, 23);
            this.cboWeightFieldLine.TabIndex = 21;
            // 
            // chkWeightFieldLine
            // 
            this.chkWeightFieldLine.AutoSize = true;
            this.chkWeightFieldLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWeightFieldLine.Location = new System.Drawing.Point(59, 142);
            this.chkWeightFieldLine.Margin = new System.Windows.Forms.Padding(4);
            this.chkWeightFieldLine.Name = "chkWeightFieldLine";
            this.chkWeightFieldLine.Size = new System.Drawing.Size(127, 17);
            this.chkWeightFieldLine.TabIndex = 20;
            this.chkWeightFieldLine.Text = "Use Weight Field:";
            this.chkWeightFieldLine.UseVisualStyleBackColor = true;
            this.chkWeightFieldLine.CheckedChanged += new System.EventHandler(this.chkWeightFieldLine_CheckedChanged);
            // 
            // chkPolyEnabled
            // 
            this.chkPolyEnabled.AutoSize = true;
            this.chkPolyEnabled.Location = new System.Drawing.Point(35, 189);
            this.chkPolyEnabled.Name = "chkPolyEnabled";
            this.chkPolyEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkPolyEnabled.TabIndex = 19;
            this.chkPolyEnabled.UseVisualStyleBackColor = true;
            this.chkPolyEnabled.CheckedChanged += new System.EventHandler(this.chkPolyEnabled_CheckedChanged);
            // 
            // chkLineEnabled
            // 
            this.chkLineEnabled.AutoSize = true;
            this.chkLineEnabled.Location = new System.Drawing.Point(35, 111);
            this.chkLineEnabled.Name = "chkLineEnabled";
            this.chkLineEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkLineEnabled.TabIndex = 18;
            this.chkLineEnabled.UseVisualStyleBackColor = true;
            this.chkLineEnabled.CheckedChanged += new System.EventHandler(this.chkLineEnabled_CheckedChanged);
            // 
            // chkPointEnabled
            // 
            this.chkPointEnabled.AutoSize = true;
            this.chkPointEnabled.Location = new System.Drawing.Point(35, 37);
            this.chkPointEnabled.Name = "chkPointEnabled";
            this.chkPointEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkPointEnabled.TabIndex = 17;
            this.chkPointEnabled.UseVisualStyleBackColor = true;
            this.chkPointEnabled.CheckedChanged += new System.EventHandler(this.chkPointEnabled_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 184);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 19);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 16;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 109);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 19);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnOpenPolyFC
            // 
            this.btnOpenPolyFC.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenPolyFC.Image")));
            this.btnOpenPolyFC.Location = new System.Drawing.Point(319, 184);
            this.btnOpenPolyFC.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenPolyFC.Name = "btnOpenPolyFC";
            this.btnOpenPolyFC.Size = new System.Drawing.Size(56, 30);
            this.btnOpenPolyFC.TabIndex = 13;
            this.btnOpenPolyFC.UseVisualStyleBackColor = true;
            this.btnOpenPolyFC.Click += new System.EventHandler(this.btnOpenPolyFC_Click);
            // 
            // cboFCPoly
            // 
            this.cboFCPoly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFCPoly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFCPoly.FormattingEnabled = true;
            this.cboFCPoly.Location = new System.Drawing.Point(57, 184);
            this.cboFCPoly.Margin = new System.Windows.Forms.Padding(4);
            this.cboFCPoly.Name = "cboFCPoly";
            this.cboFCPoly.Size = new System.Drawing.Size(254, 21);
            this.cboFCPoly.TabIndex = 12;
            this.cboFCPoly.SelectedIndexChanged += new System.EventHandler(this.cboFCPoly_SelectedIndexChanged);
            // 
            // btnOpenLineFC
            // 
            this.btnOpenLineFC.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLineFC.Image")));
            this.btnOpenLineFC.Location = new System.Drawing.Point(319, 109);
            this.btnOpenLineFC.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenLineFC.Name = "btnOpenLineFC";
            this.btnOpenLineFC.Size = new System.Drawing.Size(56, 30);
            this.btnOpenLineFC.TabIndex = 11;
            this.btnOpenLineFC.UseVisualStyleBackColor = true;
            this.btnOpenLineFC.Click += new System.EventHandler(this.btnOpenLineFC_Click);
            // 
            // cboFCLine
            // 
            this.cboFCLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFCLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFCLine.FormattingEnabled = true;
            this.cboFCLine.Location = new System.Drawing.Point(57, 109);
            this.cboFCLine.Margin = new System.Windows.Forms.Padding(4);
            this.cboFCLine.Name = "cboFCLine";
            this.cboFCLine.Size = new System.Drawing.Size(254, 21);
            this.cboFCLine.TabIndex = 10;
            this.cboFCLine.SelectedIndexChanged += new System.EventHandler(this.cboFCLine_SelectedIndexChanged);
            // 
            // txtCellSize
            // 
            this.txtCellSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellSize.Location = new System.Drawing.Point(111, 250);
            this.txtCellSize.Name = "txtCellSize";
            this.txtCellSize.Size = new System.Drawing.Size(170, 20);
            this.txtCellSize.TabIndex = 9;
            // 
            // lblSpatialReference
            // 
            this.lblSpatialReference.AutoSize = true;
            this.lblSpatialReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpatialReference.Location = new System.Drawing.Point(314, 326);
            this.lblSpatialReference.Name = "lblSpatialReference";
            this.lblSpatialReference.Size = new System.Drawing.Size(0, 13);
            this.lblSpatialReference.TabIndex = 8;
            // 
            // lblCellSize
            // 
            this.lblCellSize.AutoSize = true;
            this.lblCellSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCellSize.Location = new System.Drawing.Point(8, 257);
            this.lblCellSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCellSize.Name = "lblCellSize";
            this.lblCellSize.Size = new System.Drawing.Size(60, 13);
            this.lblCellSize.TabIndex = 6;
            this.lblCellSize.Text = "Cell Size:";
            // 
            // cboWeightFieldPoint
            // 
            this.cboWeightFieldPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeightFieldPoint.FormattingEnabled = true;
            this.cboWeightFieldPoint.Location = new System.Drawing.Point(190, 64);
            this.cboWeightFieldPoint.Margin = new System.Windows.Forms.Padding(4);
            this.cboWeightFieldPoint.Name = "cboWeightFieldPoint";
            this.cboWeightFieldPoint.Size = new System.Drawing.Size(121, 23);
            this.cboWeightFieldPoint.TabIndex = 3;
            // 
            // chkWeightFieldPoint
            // 
            this.chkWeightFieldPoint.AutoSize = true;
            this.chkWeightFieldPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWeightFieldPoint.Location = new System.Drawing.Point(59, 68);
            this.chkWeightFieldPoint.Margin = new System.Windows.Forms.Padding(4);
            this.chkWeightFieldPoint.Name = "chkWeightFieldPoint";
            this.chkWeightFieldPoint.Size = new System.Drawing.Size(127, 17);
            this.chkWeightFieldPoint.TabIndex = 2;
            this.chkWeightFieldPoint.Text = "Use Weight Field:";
            this.chkWeightFieldPoint.UseVisualStyleBackColor = true;
            this.chkWeightFieldPoint.CheckedChanged += new System.EventHandler(this.chkWeightFieldPoint_CheckedChanged);
            // 
            // btnOpenPointFC
            // 
            this.btnOpenPointFC.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenPointFC.Image")));
            this.btnOpenPointFC.Location = new System.Drawing.Point(319, 35);
            this.btnOpenPointFC.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenPointFC.Name = "btnOpenPointFC";
            this.btnOpenPointFC.Size = new System.Drawing.Size(56, 30);
            this.btnOpenPointFC.TabIndex = 1;
            this.btnOpenPointFC.UseVisualStyleBackColor = true;
            this.btnOpenPointFC.Click += new System.EventHandler(this.btnOpenPointFC_Click);
            // 
            // cboFCPoint
            // 
            this.cboFCPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFCPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFCPoint.FormattingEnabled = true;
            this.cboFCPoint.Location = new System.Drawing.Point(57, 35);
            this.cboFCPoint.Margin = new System.Windows.Forms.Padding(4);
            this.cboFCPoint.Name = "cboFCPoint";
            this.cboFCPoint.Size = new System.Drawing.Size(254, 21);
            this.cboFCPoint.TabIndex = 0;
            this.cboFCPoint.SelectedIndexChanged += new System.EventHandler(this.cboFCPoint_SelectedIndexChanged);
            // 
            // chkSpatialJoin
            // 
            this.chkSpatialJoin.AutoSize = true;
            this.chkSpatialJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSpatialJoin.Location = new System.Drawing.Point(175, 85);
            this.chkSpatialJoin.Margin = new System.Windows.Forms.Padding(4);
            this.chkSpatialJoin.Name = "chkSpatialJoin";
            this.chkSpatialJoin.Size = new System.Drawing.Size(113, 17);
            this.chkSpatialJoin.TabIndex = 4;
            this.chkSpatialJoin.Text = "Spatial Joining:";
            this.chkSpatialJoin.UseVisualStyleBackColor = true;
            this.chkSpatialJoin.CheckedChanged += new System.EventHandler(this.chkSpatialJoin_CheckedChanged);
            // 
            // grpDistance
            // 
            this.grpDistance.Controls.Add(this.pictureBox5);
            this.grpDistance.Controls.Add(this.chkAddDistRaster);
            this.grpDistance.Controls.Add(this.btnOpenDistRaster);
            this.grpDistance.Controls.Add(this.txtOutDistRaster);
            this.grpDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDistance.Location = new System.Drawing.Point(13, 335);
            this.grpDistance.Margin = new System.Windows.Forms.Padding(4);
            this.grpDistance.Name = "grpDistance";
            this.grpDistance.Padding = new System.Windows.Forms.Padding(4);
            this.grpDistance.Size = new System.Drawing.Size(383, 114);
            this.grpDistance.TabIndex = 2;
            this.grpDistance.TabStop = false;
            this.grpDistance.Text = "Output Weighted Distance Raster";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(7, 49);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(22, 19);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 17;
            this.pictureBox5.TabStop = false;
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
            this.txtOutDistRaster.Location = new System.Drawing.Point(37, 49);
            this.txtOutDistRaster.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutDistRaster.Name = "txtOutDistRaster";
            this.txtOutDistRaster.Size = new System.Drawing.Size(279, 20);
            this.txtOutDistRaster.TabIndex = 0;
            this.txtOutDistRaster.TextChanged += new System.EventHandler(this.txtOutDistRaster_TextChanged);
            // 
            // grpVoronoiDiagram
            // 
            this.grpVoronoiDiagram.Controls.Add(this.pictureBox4);
            this.grpVoronoiDiagram.Controls.Add(this.chkAddVoronoi);
            this.grpVoronoiDiagram.Controls.Add(this.btnOpenOutVoronoi);
            this.grpVoronoiDiagram.Controls.Add(this.txtOutVoronoi);
            this.grpVoronoiDiagram.Controls.Add(this.chkSpatialJoin);
            this.grpVoronoiDiagram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpVoronoiDiagram.Location = new System.Drawing.Point(13, 457);
            this.grpVoronoiDiagram.Margin = new System.Windows.Forms.Padding(4);
            this.grpVoronoiDiagram.Name = "grpVoronoiDiagram";
            this.grpVoronoiDiagram.Padding = new System.Windows.Forms.Padding(4);
            this.grpVoronoiDiagram.Size = new System.Drawing.Size(383, 122);
            this.grpVoronoiDiagram.TabIndex = 3;
            this.grpVoronoiDiagram.TabStop = false;
            this.grpVoronoiDiagram.Text = "Output Voronoi Diagram (Shapefile)";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(7, 50);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 19);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 17;
            this.pictureBox4.TabStop = false;
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
            this.txtOutVoronoi.Location = new System.Drawing.Point(37, 49);
            this.txtOutVoronoi.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutVoronoi.Name = "txtOutVoronoi";
            this.txtOutVoronoi.Size = new System.Drawing.Size(279, 20);
            this.txtOutVoronoi.TabIndex = 0;
            this.txtOutVoronoi.TextChanged += new System.EventHandler(this.txtOutVoronoi_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(296, 585);
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
            this.btnOK.Location = new System.Drawing.Point(188, 585);
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
            this.btnHelp.Location = new System.Drawing.Point(13, 585);
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
            this.ClientSize = new System.Drawing.Size(409, 631);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.grpVoronoiDiagram);
            this.Controls.Add(this.grpDistance);
            this.Controls.Add(this.grpInputOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVoronoi";
            this.Text = "Generate Voronoi Diagrams";
            this.grpInputOptions.ResumeLayout(false);
            this.grpInputOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpDistance.ResumeLayout(false);
            this.grpDistance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.grpVoronoiDiagram.ResumeLayout(false);
            this.grpVoronoiDiagram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInputOptions;
        private System.Windows.Forms.Label lblCellSize;
        private System.Windows.Forms.CheckBox chkSpatialJoin;
        private System.Windows.Forms.ComboBox cboWeightFieldPoint;
        private System.Windows.Forms.CheckBox chkWeightFieldPoint;
        private System.Windows.Forms.Button btnOpenPointFC;
        private System.Windows.Forms.ComboBox cboFCPoint;
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
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpenPolyFC;
        private System.Windows.Forms.ComboBox cboFCPoly;
        private System.Windows.Forms.Button btnOpenLineFC;
        private System.Windows.Forms.ComboBox cboFCLine;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.CheckBox chkPolyEnabled;
        private System.Windows.Forms.CheckBox chkLineEnabled;
        private System.Windows.Forms.CheckBox chkPointEnabled;
        private System.Windows.Forms.ComboBox cboWeightFieldPolygon;
        private System.Windows.Forms.CheckBox chkWeightFieldPolygon;
        private System.Windows.Forms.ComboBox cboWeightFieldLine;
        private System.Windows.Forms.CheckBox chkWeightFieldLine;
        private System.Windows.Forms.Label lblOutputExtent;
        private System.Windows.Forms.ComboBox cboOutputExtent;
        private System.Windows.Forms.Label lblxInput;
    }
}