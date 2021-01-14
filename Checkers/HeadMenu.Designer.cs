
namespace Checkers
{
    partial class HeadMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeadMenu));
            this.pvp_b = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Exit_b = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pvp_b
            // 
            this.pvp_b.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pvp_b.AutoSize = true;
            this.pvp_b.BackColor = System.Drawing.Color.Transparent;
            this.pvp_b.BackgroundImage = global::Checkers.Properties.Resources.button;
            this.pvp_b.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pvp_b.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pvp_b.FlatAppearance.BorderSize = 0;
            this.pvp_b.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.pvp_b.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.pvp_b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pvp_b.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pvp_b.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pvp_b.Location = new System.Drawing.Point(212, 143);
            this.pvp_b.Name = "pvp_b";
            this.pvp_b.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pvp_b.Size = new System.Drawing.Size(231, 66);
            this.pvp_b.TabIndex = 6;
            this.pvp_b.Text = "Player vs Player";
            this.pvp_b.UseVisualStyleBackColor = false;
            this.pvp_b.Click += new System.EventHandler(this.pvp_b_Click);
            this.pvp_b.MouseEnter += new System.EventHandler(this.HM_button_hover);
            this.pvp_b.MouseLeave += new System.EventHandler(this.HM_button_leave);
            this.pvp_b.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HM_button_leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Checkers.Properties.Resources.image;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(664, 119);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Exit_b
            // 
            this.Exit_b.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit_b.AutoSize = true;
            this.Exit_b.BackColor = System.Drawing.Color.Transparent;
            this.Exit_b.BackgroundImage = global::Checkers.Properties.Resources.button;
            this.Exit_b.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Exit_b.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_b.FlatAppearance.BorderSize = 0;
            this.Exit_b.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Exit_b.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Exit_b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit_b.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit_b.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Exit_b.Location = new System.Drawing.Point(212, 405);
            this.Exit_b.Name = "Exit_b";
            this.Exit_b.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Exit_b.Size = new System.Drawing.Size(231, 66);
            this.Exit_b.TabIndex = 10;
            this.Exit_b.Text = "Exit";
            this.Exit_b.UseVisualStyleBackColor = false;
            this.Exit_b.Click += new System.EventHandler(this.Exit_b_Click);
            this.Exit_b.MouseEnter += new System.EventHandler(this.HM_button_hover);
            this.Exit_b.MouseLeave += new System.EventHandler(this.HM_button_leave);
            this.Exit_b.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HM_button_leave);
            // 
            // HeadMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Checkers.Properties.Resources.checkers_6174;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(664, 528);
            this.Controls.Add(this.Exit_b);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pvp_b);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HeadMenu";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button pvp_b;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Exit_b;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}