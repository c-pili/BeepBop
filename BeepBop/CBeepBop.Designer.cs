namespace BeepBop
{
    partial class CBeepBop
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
            this.SuspendLayout();
            // 
            // m_serialPortRfid
            // 
            this.m_serialPortRfid.BaudRate = 2400;
            this.m_serialPortRfid.DtrEnable = true;
            this.m_serialPortRfid.PortName = "COM3";
            this.m_serialPortRfid.ReceivedBytesThreshold = 12;
            // 
            // CBeepBop
            // 
            this.ClientSize = new System.Drawing.Size(831, 436);
            this.Name = "CBeepBop";
            this.ResumeLayout(false);

        }

        private System.IO.Ports.SerialPort m_serialPortRfid;
    }
}

