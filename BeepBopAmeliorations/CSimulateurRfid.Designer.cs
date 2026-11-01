namespace BeepBop
{
    partial class CSimulateurRfid
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
            this.m_BpBadger = new System.Windows.Forms.Button();
            this.m_TxtBoxNumCarte = new System.Windows.Forms.ComboBox();
            this.m_TxtAjouterNumCarte = new System.Windows.Forms.TextBox();
            this.m_BpAjouterNumCarte = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_BpBadger
            // 
            this.m_BpBadger.Location = new System.Drawing.Point(427, 202);
            this.m_BpBadger.Name = "m_BpBadger";
            this.m_BpBadger.Size = new System.Drawing.Size(131, 45);
            this.m_BpBadger.TabIndex = 0;
            this.m_BpBadger.Text = "Badger";
            this.m_BpBadger.UseVisualStyleBackColor = true;
            this.m_BpBadger.Click += new System.EventHandler(this.m_BpBadger_Click);
            // 
            // m_TxtBoxNumCarte
            // 
            this.m_TxtBoxNumCarte.FormattingEnabled = true;
            this.m_TxtBoxNumCarte.Location = new System.Drawing.Point(244, 213);
            this.m_TxtBoxNumCarte.Name = "m_TxtBoxNumCarte";
            this.m_TxtBoxNumCarte.Size = new System.Drawing.Size(143, 24);
            this.m_TxtBoxNumCarte.TabIndex = 1;
            // 
            // m_TxtAjouterNumCarte
            // 
            this.m_TxtAjouterNumCarte.Location = new System.Drawing.Point(244, 134);
            this.m_TxtAjouterNumCarte.Name = "m_TxtAjouterNumCarte";
            this.m_TxtAjouterNumCarte.Size = new System.Drawing.Size(143, 22);
            this.m_TxtAjouterNumCarte.TabIndex = 2;
            // 
            // m_BpAjouterNumCarte
            // 
            this.m_BpAjouterNumCarte.Location = new System.Drawing.Point(427, 123);
            this.m_BpAjouterNumCarte.Name = "m_BpAjouterNumCarte";
            this.m_BpAjouterNumCarte.Size = new System.Drawing.Size(131, 45);
            this.m_BpAjouterNumCarte.TabIndex = 3;
            this.m_BpAjouterNumCarte.Text = "Ajouter";
            this.m_BpAjouterNumCarte.UseVisualStyleBackColor = true;
            this.m_BpAjouterNumCarte.Click += new System.EventHandler(this.m_BpAjouterNumCarte_Click);
            // 
            // CSimulateurRfid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_BpAjouterNumCarte);
            this.Controls.Add(this.m_TxtAjouterNumCarte);
            this.Controls.Add(this.m_TxtBoxNumCarte);
            this.Controls.Add(this.m_BpBadger);
            this.Name = "CSimulateurRfid";
            this.Text = "CSimulateurRfid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_BpBadger;
        private System.Windows.Forms.ComboBox m_TxtBoxNumCarte;
        private System.Windows.Forms.TextBox m_TxtAjouterNumCarte;
        private System.Windows.Forms.Button m_BpAjouterNumCarte;
    }
}