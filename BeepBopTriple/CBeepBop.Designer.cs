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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CBeepBop));
            this.m_serialPortRfid = new System.IO.Ports.SerialPort(this.components);
            this.m_lblNumeroCarte = new System.Windows.Forms.Label();
            this.m_BpConnecterSerialPort = new System.Windows.Forms.Button();
            this.m_TxtSerialPort = new System.Windows.Forms.TextBox();
            this.m_TxtSerialBaud = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_Timer2s = new System.Windows.Forms.Timer(this.components);
            this.m_BpAcquittement = new System.Windows.Forms.Button();
            this.m_TxtNbrPortique = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_BoxNumPortique = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_serialPortRfid
            // 
            this.m_serialPortRfid.DtrEnable = true;
            this.m_serialPortRfid.ReceivedBytesThreshold = 12;
            this.m_serialPortRfid.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.m_serialPortRfid_DataReceived);
            // 
            // m_lblNumeroCarte
            // 
            this.m_lblNumeroCarte.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_lblNumeroCarte.AutoSize = true;
            this.m_lblNumeroCarte.Location = new System.Drawing.Point(631, 167);
            this.m_lblNumeroCarte.Name = "m_lblNumeroCarte";
            this.m_lblNumeroCarte.Size = new System.Drawing.Size(0, 16);
            this.m_lblNumeroCarte.TabIndex = 0;
            // 
            // m_BpConnecterSerialPort
            // 
            this.m_BpConnecterSerialPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpConnecterSerialPort.Location = new System.Drawing.Point(368, 195);
            this.m_BpConnecterSerialPort.Name = "m_BpConnecterSerialPort";
            this.m_BpConnecterSerialPort.Size = new System.Drawing.Size(108, 40);
            this.m_BpConnecterSerialPort.TabIndex = 1;
            this.m_BpConnecterSerialPort.Text = "Se Connecter";
            this.m_BpConnecterSerialPort.UseVisualStyleBackColor = true;
            this.m_BpConnecterSerialPort.Click += new System.EventHandler(this.m_BpConnecterSerialPort_Click);
            // 
            // m_TxtSerialPort
            // 
            this.m_TxtSerialPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtSerialPort.Location = new System.Drawing.Point(250, 161);
            this.m_TxtSerialPort.Name = "m_TxtSerialPort";
            this.m_TxtSerialPort.Size = new System.Drawing.Size(100, 22);
            this.m_TxtSerialPort.TabIndex = 2;
            // 
            // m_TxtSerialBaud
            // 
            this.m_TxtSerialBaud.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtSerialBaud.Location = new System.Drawing.Point(250, 195);
            this.m_TxtSerialBaud.Name = "m_TxtSerialBaud";
            this.m_TxtSerialBaud.Size = new System.Drawing.Size(100, 22);
            this.m_TxtSerialBaud.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(570, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "ID Carte";
            // 
            // m_Timer2s
            // 
            this.m_Timer2s.Interval = 2000;
            this.m_Timer2s.Tick += new System.EventHandler(this.m_Timer2s_Tick);
            // 
            // m_BpAcquittement
            // 
            this.m_BpAcquittement.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpAcquittement.Location = new System.Drawing.Point(368, 252);
            this.m_BpAcquittement.Name = "m_BpAcquittement";
            this.m_BpAcquittement.Size = new System.Drawing.Size(108, 40);
            this.m_BpAcquittement.TabIndex = 7;
            this.m_BpAcquittement.Text = "Acquitement";
            this.m_BpAcquittement.UseVisualStyleBackColor = true;
            this.m_BpAcquittement.Click += new System.EventHandler(this.m_BpAcquittement_Click);
            // 
            // m_TxtNbrPortique
            // 
            this.m_TxtNbrPortique.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtNbrPortique.Location = new System.Drawing.Point(368, 92);
            this.m_TxtNbrPortique.Name = "m_TxtNbrPortique";
            this.m_TxtNbrPortique.Size = new System.Drawing.Size(108, 22);
            this.m_TxtNbrPortique.TabIndex = 8;
            this.m_TxtNbrPortique.TextChanged += new System.EventHandler(this.m_TxtNbrPortique_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nombre de portique";
            // 
            // m_BoxNumPortique
            // 
            this.m_BoxNumPortique.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_BoxNumPortique.FormattingEnabled = true;
            this.m_BoxNumPortique.Location = new System.Drawing.Point(368, 161);
            this.m_BoxNumPortique.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_BoxNumPortique.Name = "m_BoxNumPortique";
            this.m_BoxNumPortique.Size = new System.Drawing.Size(108, 24);
            this.m_BoxNumPortique.TabIndex = 12;
            this.m_BoxNumPortique.SelectedIndexChanged += new System.EventHandler(this.m_BoxNumPortique_SelectedIndexChanged);
            // 
            // CBeepBop
            // 
            this.ClientSize = new System.Drawing.Size(831, 436);
            this.Controls.Add(this.m_BoxNumPortique);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_TxtNbrPortique);
            this.Controls.Add(this.m_BpAcquittement);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_TxtSerialBaud);
            this.Controls.Add(this.m_TxtSerialPort);
            this.Controls.Add(this.m_BpConnecterSerialPort);
            this.Controls.Add(this.m_lblNumeroCarte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CBeepBop";
            this.Text = "BeepBop Parametres";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.IO.Ports.SerialPort m_serialPortRfid;
        private System.Windows.Forms.Label m_lblNumeroCarte;
        private System.Windows.Forms.Button m_BpConnecterSerialPort;
        private System.Windows.Forms.TextBox m_TxtSerialPort;
        private System.Windows.Forms.TextBox m_TxtSerialBaud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Timer m_Timer2s;
        private System.Windows.Forms.Button m_BpAcquittement;
        private System.Windows.Forms.TextBox m_TxtNbrPortique;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_BoxNumPortique;
    }
}

