namespace VideoDownloaderV1
{
    partial class AnimeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimeForm));
            this.Start_Button = new System.Windows.Forms.Button();
            this.AnimeLink_TextBox = new System.Windows.Forms.TextBox();
            this.AnimeLink_Label = new System.Windows.Forms.Label();
            this.Start_Label = new System.Windows.Forms.Label();
            this.Start_TextBox = new System.Windows.Forms.TextBox();
            this.End_TextBox = new System.Windows.Forms.TextBox();
            this.End_Label = new System.Windows.Forms.Label();
            this.UserEmail_Label = new System.Windows.Forms.Label();
            this.Custom_Button = new System.Windows.Forms.Button();
            this.Email_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_Button.Location = new System.Drawing.Point(603, 6);
            this.Start_Button.Margin = new System.Windows.Forms.Padding(8);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(104, 94);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "Start";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // AnimeLink_TextBox
            // 
            this.AnimeLink_TextBox.Location = new System.Drawing.Point(193, 6);
            this.AnimeLink_TextBox.Margin = new System.Windows.Forms.Padding(8);
            this.AnimeLink_TextBox.Name = "AnimeLink_TextBox";
            this.AnimeLink_TextBox.Size = new System.Drawing.Size(394, 40);
            this.AnimeLink_TextBox.TabIndex = 1;
            // 
            // AnimeLink_Label
            // 
            this.AnimeLink_Label.AutoSize = true;
            this.AnimeLink_Label.Location = new System.Drawing.Point(17, 9);
            this.AnimeLink_Label.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.AnimeLink_Label.Name = "AnimeLink_Label";
            this.AnimeLink_Label.Size = new System.Drawing.Size(160, 33);
            this.AnimeLink_Label.TabIndex = 2;
            this.AnimeLink_Label.Text = "Anime Link";
            // 
            // Start_Label
            // 
            this.Start_Label.AutoSize = true;
            this.Start_Label.Location = new System.Drawing.Point(17, 63);
            this.Start_Label.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.Start_Label.Name = "Start_Label";
            this.Start_Label.Size = new System.Drawing.Size(158, 33);
            this.Start_Label.TabIndex = 3;
            this.Start_Label.Text = "Starting Ep";
            // 
            // Start_TextBox
            // 
            this.Start_TextBox.Location = new System.Drawing.Point(193, 60);
            this.Start_TextBox.Name = "Start_TextBox";
            this.Start_TextBox.Size = new System.Drawing.Size(100, 40);
            this.Start_TextBox.TabIndex = 4;
            // 
            // End_TextBox
            // 
            this.End_TextBox.Location = new System.Drawing.Point(487, 60);
            this.End_TextBox.Name = "End_TextBox";
            this.End_TextBox.Size = new System.Drawing.Size(100, 40);
            this.End_TextBox.TabIndex = 5;
            // 
            // End_Label
            // 
            this.End_Label.AutoSize = true;
            this.End_Label.Location = new System.Drawing.Point(328, 63);
            this.End_Label.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.End_Label.Name = "End_Label";
            this.End_Label.Size = new System.Drawing.Size(148, 33);
            this.End_Label.TabIndex = 6;
            this.End_Label.Text = "Ending Ep";
            // 
            // UserEmail_Label
            // 
            this.UserEmail_Label.AutoSize = true;
            this.UserEmail_Label.Location = new System.Drawing.Point(17, 118);
            this.UserEmail_Label.Name = "UserEmail_Label";
            this.UserEmail_Label.Size = new System.Drawing.Size(159, 33);
            this.UserEmail_Label.TabIndex = 7;
            this.UserEmail_Label.Text = "User Email";
            // 
            // Custom_Button
            // 
            this.Custom_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_Button.Location = new System.Drawing.Point(603, 111);
            this.Custom_Button.Name = "Custom_Button";
            this.Custom_Button.Size = new System.Drawing.Size(104, 40);
            this.Custom_Button.TabIndex = 8;
            this.Custom_Button.Text = "Custom";
            this.Custom_Button.UseVisualStyleBackColor = true;
            this.Custom_Button.Click += new System.EventHandler(this.Custom_Button_Click);
            // 
            // Email_TextBox
            // 
            this.Email_TextBox.Location = new System.Drawing.Point(193, 111);
            this.Email_TextBox.Margin = new System.Windows.Forms.Padding(8);
            this.Email_TextBox.Name = "Email_TextBox";
            this.Email_TextBox.Size = new System.Drawing.Size(394, 40);
            this.Email_TextBox.TabIndex = 9;
            // 
            // AnimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 165);
            this.Controls.Add(this.Email_TextBox);
            this.Controls.Add(this.Custom_Button);
            this.Controls.Add(this.UserEmail_Label);
            this.Controls.Add(this.End_Label);
            this.Controls.Add(this.End_TextBox);
            this.Controls.Add(this.Start_TextBox);
            this.Controls.Add(this.Start_Label);
            this.Controls.Add(this.AnimeLink_Label);
            this.Controls.Add(this.AnimeLink_TextBox);
            this.Controls.Add(this.Start_Button);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.MaximizeBox = false;
            this.Name = "AnimeForm";
            this.Text = "Anime Downloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AnimeForm_FormClosed);
            this.Load += new System.EventHandler(this.AnimeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.TextBox AnimeLink_TextBox;
        private System.Windows.Forms.Label AnimeLink_Label;
        private System.Windows.Forms.Label Start_Label;
        private System.Windows.Forms.TextBox Start_TextBox;
        private System.Windows.Forms.TextBox End_TextBox;
        private System.Windows.Forms.Label End_Label;
        private System.Windows.Forms.Label UserEmail_Label;
        private System.Windows.Forms.Button Custom_Button;
        private System.Windows.Forms.TextBox Email_TextBox;
    }
}

