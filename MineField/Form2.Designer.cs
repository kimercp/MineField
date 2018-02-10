namespace MineField
{
    partial class frmResults
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblPlayersName = new System.Windows.Forms.Label();
            this.txtPlayersName = new System.Windows.Forms.TextBox();
            this.lblPlayerPoints = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblPlayerLevel = new System.Windows.Forms.Label();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnShowOnly = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnUpdate.Location = new System.Drawing.Point(525, 29);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblPlayersName
            // 
            this.lblPlayersName.AutoSize = true;
            this.lblPlayersName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblPlayersName.Location = new System.Drawing.Point(232, 32);
            this.lblPlayersName.Name = "lblPlayersName";
            this.lblPlayersName.Size = new System.Drawing.Size(113, 16);
            this.lblPlayersName.TabIndex = 1;
            this.lblPlayersName.Text = "Enter Your Name:";
            // 
            // txtPlayersName
            // 
            this.txtPlayersName.Location = new System.Drawing.Point(345, 30);
            this.txtPlayersName.MaxLength = 25;
            this.txtPlayersName.Name = "txtPlayersName";
            this.txtPlayersName.Size = new System.Drawing.Size(145, 20);
            this.txtPlayersName.TabIndex = 2;
            // 
            // lblPlayerPoints
            // 
            this.lblPlayerPoints.AutoSize = true;
            this.lblPlayerPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblPlayerPoints.Location = new System.Drawing.Point(108, 32);
            this.lblPlayerPoints.Name = "lblPlayerPoints";
            this.lblPlayerPoints.Size = new System.Drawing.Size(54, 16);
            this.lblPlayerPoints.TabIndex = 3;
            this.lblPlayerPoints.Text = "Points : ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(29, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(571, 353);
            this.dataGridView1.TabIndex = 4;
            // 
            // lblPlayerLevel
            // 
            this.lblPlayerLevel.AutoSize = true;
            this.lblPlayerLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblPlayerLevel.Location = new System.Drawing.Point(26, 32);
            this.lblPlayerLevel.Name = "lblPlayerLevel";
            this.lblPlayerLevel.Size = new System.Drawing.Size(47, 16);
            this.lblPlayerLevel.TabIndex = 5;
            this.lblPlayerLevel.Text = "Level: ";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(29, 460);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(113, 23);
            this.btnShowAll.TabIndex = 6;
            this.btnShowAll.Text = "Show All Levels";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnShowOnly
            // 
            this.btnShowOnly.Location = new System.Drawing.Point(184, 460);
            this.btnShowOnly.Name = "btnShowOnly";
            this.btnShowOnly.Size = new System.Drawing.Size(113, 23);
            this.btnShowOnly.TabIndex = 7;
            this.btnShowOnly.Text = "Show only ";
            this.btnShowOnly.UseVisualStyleBackColor = true;
            this.btnShowOnly.Click += new System.EventHandler(this.btnShowOnly_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(525, 460);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 512);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnShowOnly);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.lblPlayerLevel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblPlayerPoints);
            this.Controls.Add(this.txtPlayersName);
            this.Controls.Add(this.lblPlayersName);
            this.Controls.Add(this.btnUpdate);
            this.Name = "frmResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MineField";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmResults_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblPlayersName;
        private System.Windows.Forms.TextBox txtPlayersName;
        private System.Windows.Forms.Label lblPlayerPoints;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblPlayerLevel;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnShowOnly;
        private System.Windows.Forms.Button btnBack;
    }
}