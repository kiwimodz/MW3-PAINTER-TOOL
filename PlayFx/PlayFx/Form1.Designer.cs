namespace PlayFx
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.Box = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.FXC = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Painter = new System.Windows.Forms.Timer(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.FXValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MODE = new System.Windows.Forms.Label();
            this.MAP = new System.Windows.Forms.Label();
            this.HOST = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FXC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FXValue)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect / Attach";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Box
            // 
            this.Box.BackColor = System.Drawing.SystemColors.Menu;
            this.Box.ForeColor = System.Drawing.Color.Black;
            this.Box.FormattingEnabled = true;
            this.Box.Items.AddRange(new object[] {
            "Green",
            "Red"});
            this.Box.Location = new System.Drawing.Point(22, 151);
            this.Box.Name = "Box";
            this.Box.Size = new System.Drawing.Size(86, 21);
            this.Box.TabIndex = 1;
            this.Box.SelectedIndexChanged += new System.EventHandler(this.Box_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(174, 67);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.Size = new System.Drawing.Size(183, 429);
            this.dataGridView1.StandardTab = true;
            this.dataGridView1.TabIndex = 31;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.DividerWidth = 1;
            this.Column2.FillWeight = 30F;
            this.Column2.HeaderText = "Client";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 130;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "FX index";
            // 
            // FXC
            // 
            this.FXC.Location = new System.Drawing.Point(131, 192);
            this.FXC.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.FXC.Name = "FXC";
            this.FXC.Size = new System.Drawing.Size(37, 20);
            this.FXC.TabIndex = 80;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 79;
            this.button2.Text = "Painting [ OFF ]";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Client";
            // 
            // Painter
            // 
            this.Painter.Interval = 50;
            this.Painter.Tick += new System.EventHandler(this.Fx_Tick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 96);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 23);
            this.button6.TabIndex = 82;
            this.button6.Text = "Get Clients";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // FXValue
            // 
            this.FXValue.Location = new System.Drawing.Point(131, 152);
            this.FXValue.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.FXValue.Name = "FXValue";
            this.FXValue.Size = new System.Drawing.Size(37, 20);
            this.FXValue.TabIndex = 83;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 89;
            this.label3.Text = "GAME TYPE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 88;
            this.label4.Text = "MAP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 87;
            this.label5.Text = "HOST:";
            // 
            // MODE
            // 
            this.MODE.AutoSize = true;
            this.MODE.Location = new System.Drawing.Point(77, 264);
            this.MODE.Name = "MODE";
            this.MODE.Size = new System.Drawing.Size(25, 13);
            this.MODE.TabIndex = 86;
            this.MODE.Text = "Null";
            // 
            // MAP
            // 
            this.MAP.AutoSize = true;
            this.MAP.Location = new System.Drawing.Point(46, 244);
            this.MAP.Name = "MAP";
            this.MAP.Size = new System.Drawing.Size(25, 13);
            this.MAP.TabIndex = 85;
            this.MAP.Text = "Null";
            // 
            // HOST
            // 
            this.HOST.AutoSize = true;
            this.HOST.Location = new System.Drawing.Point(46, 224);
            this.HOST.Name = "HOST";
            this.HOST.Size = new System.Drawing.Size(25, 13);
            this.HOST.TabIndex = 84;
            this.HOST.Text = "Null";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 90;
            this.label6.Text = "Select Your Color";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(7, 282);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(120, 23);
            this.button9.TabIndex = 91;
            this.button9.Text = "Fast Restart Map";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "1st. Connect / Attach",
            "2nd. Click Get Clients",
            "3rd. Select Your Color",
            "4th. Turn On Painting",
            "5th. Hold L1 To Draw",
            "------------------------------------",
            "Repeat Steps 3 After",
            "Map Rotation.",
            "------------------------------------",
            "If Fx \"Paint\" Stops ",
            "Spawning >",
            "Restart the map",
            "> \"Limit Reached\"..",
            "------------------------------------",
            "Credits. >>Scroll Down :) <<",
            "VezahHFH For RPC, Play FX ",
            "xCSBKx For AnglestoForward",
            "And if button \"L1\" Is Down :)",
            "And Me kiwi_modz For SetFX",
            "Function + Method -- Enjoy."});
            this.listBox1.Location = new System.Drawing.Point(7, 324);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(161, 199);
            this.listBox1.TabIndex = 92;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "How To \"Tut\"";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 525);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MODE);
            this.Controls.Add(this.MAP);
            this.Controls.Add(this.HOST);
            this.Controls.Add(this.FXValue);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FXC);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.DarkMagenta;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Become Painter By Kiwi_modz";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FXC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FXValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Box;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown FXC;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer Painter;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.NumericUpDown FXValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label MODE;
        private System.Windows.Forms.Label MAP;
        private System.Windows.Forms.Label HOST;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label7;
    }
}

