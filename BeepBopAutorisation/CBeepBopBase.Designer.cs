namespace BeepBop
{
    partial class CBeepBopBase
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_serialPortRfid = new System.IO.Ports.SerialPort(this.components);
            this.m_lblNumeroCarte = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_serialPortRfid
            // 
            this.m_serialPortRfid.BaudRate = 2400;
            this.m_serialPortRfid.DtrEnable = true;
            this.m_serialPortRfid.PortName = "COM4";
            this.m_serialPortRfid.ReceivedBytesThreshold = 12;
            this.m_serialPortRfid.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.m_serialPortRfid_DataReceived);
            // 
            // m_lblNumeroCarte
            // 
            this.m_lblNumeroCarte.AutoSize = true;
            this.m_lblNumeroCarte.Location = new System.Drawing.Point(398, 195);
            this.m_lblNumeroCarte.Name = "m_lblNumeroCarte";
            this.m_lblNumeroCarte.Size = new System.Drawing.Size(35, 13);
            this.m_lblNumeroCarte.TabIndex = 0;
            this.m_lblNumeroCarte.Text = "label1";
            // 
            // CBeepBop
            // 
            this.ClientSize = new System.Drawing.Size(831, 436);
            this.Controls.Add(this.m_lblNumeroCarte);
            this.Name = "CBeepBop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.IO.Ports.SerialPort  m_serialPortRfid;
        private System.Windows.Forms.Label m_lblNumeroCarte;
    }
}

