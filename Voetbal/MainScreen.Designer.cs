namespace Voetbal
{
    partial class MainScreen
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.matchDataLabel = new System.Windows.Forms.Label();
            this.SimulateNextMatchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(3, 121);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(795, 628);
            this.MainPanel.TabIndex = 0;
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(40, 12);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(197, 42);
            this.ProcessButton.TabIndex = 1;
            this.ProcessButton.Text = "Simuleer een Toernooi";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // matchDataLabel
            // 
            this.matchDataLabel.AutoSize = true;
            this.matchDataLabel.Location = new System.Drawing.Point(317, 82);
            this.matchDataLabel.Name = "matchDataLabel";
            this.matchDataLabel.Size = new System.Drawing.Size(133, 17);
            this.matchDataLabel.TabIndex = 2;
            this.matchDataLabel.Text = "Wedstrijd gegevens";
            // 
            // SimulateNextMatchButton
            // 
            this.SimulateNextMatchButton.Enabled = false;
            this.SimulateNextMatchButton.Location = new System.Drawing.Point(40, 69);
            this.SimulateNextMatchButton.Name = "SimulateNextMatchButton";
            this.SimulateNextMatchButton.Size = new System.Drawing.Size(246, 42);
            this.SimulateNextMatchButton.TabIndex = 3;
            this.SimulateNextMatchButton.Text = "Simuleer de volgende wedstrijd";
            this.SimulateNextMatchButton.UseVisualStyleBackColor = true;
            this.SimulateNextMatchButton.Click += new System.EventHandler(this.SimulateNextMatchButton_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 751);
            this.Controls.Add(this.SimulateNextMatchButton);
            this.Controls.Add(this.matchDataLabel);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.MainPanel);
            this.Name = "MainScreen";
            this.Text = "Voetbal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.Label matchDataLabel;
        private System.Windows.Forms.Button SimulateNextMatchButton;
        public System.Windows.Forms.Panel MainPanel;
    }
}

