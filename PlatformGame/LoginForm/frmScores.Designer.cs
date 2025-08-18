namespace PlatformGame
{
    partial class frmScores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScores));
            this.lblHighScores = new System.Windows.Forms.Label();
            this.dgvScores = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScores)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHighScores
            // 
            this.lblHighScores.AutoSize = true;
            this.lblHighScores.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHighScores.Location = new System.Drawing.Point(739, 29);
            this.lblHighScores.Name = "lblHighScores";
            this.lblHighScores.Size = new System.Drawing.Size(385, 81);
            this.lblHighScores.TabIndex = 0;
            this.lblHighScores.Text = "High Scores";
            // 
            // dgvScores
            // 
            this.dgvScores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScores.Location = new System.Drawing.Point(172, 139);
            this.dgvScores.Name = "dgvScores";
            this.dgvScores.RowHeadersWidth = 102;
            this.dgvScores.RowTemplate.Height = 49;
            this.dgvScores.Size = new System.Drawing.Size(1533, 868);
            this.dgvScores.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(773, 1040);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(312, 102);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1875, 1181);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgvScores);
            this.Controls.Add(this.lblHighScores);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScores";
            this.Text = "High Scores";
            this.Load += new System.EventHandler(this.frmScores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblHighScores;
        private DataGridView dgvScores;
        private Button btnExit;
    }
}