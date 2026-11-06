namespace BeepBop
{
    partial class CRfid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CRfid));
            this.m_lblNumeroCarte = new System.Windows.Forms.Label();
            this.m_BpConnecterSerialPort = new System.Windows.Forms.Button();
            this.m_TxtSerialPort = new System.Windows.Forms.TextBox();
            this.m_TxtSerialBaud = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_LblCarteNum = new System.Windows.Forms.Label();
            this.m_TimerSeconde = new System.Windows.Forms.Timer(this.components);
            this.m_BpAcquittement = new System.Windows.Forms.Button();
            this.m_TxtNbrPortique = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_BoxNumPortique = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_TxtTpOuverturePortique = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_TxtTpMinPassage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_BpSimulateurRfid = new System.Windows.Forms.Button();
            this.m_TxtBoxListesCartes = new System.Windows.Forms.ComboBox();
            this.m_TxtAjouterCarte = new System.Windows.Forms.TextBox();
            this.m_BpAjouterNumCarte = new System.Windows.Forms.Button();
            this.m_TxtBoxOutils = new System.Windows.Forms.ComboBox();
            this.m_BpModifierCarte = new System.Windows.Forms.Button();
            this.m_TxtBoxListOutils = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.m_TxtBoxSecteurPortique = new System.Windows.Forms.ComboBox();
            this.m_TxtBoxSecteur = new System.Windows.Forms.ComboBox();
            this.m_BpSecteurModifier = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblNumeroCarte
            // 
            this.m_lblNumeroCarte.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_lblNumeroCarte.AutoSize = true;
            this.m_lblNumeroCarte.Location = new System.Drawing.Point(631, 183);
            this.m_lblNumeroCarte.Name = "m_lblNumeroCarte";
            this.m_lblNumeroCarte.Size = new System.Drawing.Size(0, 16);
            this.m_lblNumeroCarte.TabIndex = 0;
            // 
            // m_BpConnecterSerialPort
            // 
            this.m_BpConnecterSerialPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpConnecterSerialPort.Location = new System.Drawing.Point(548, 274);
            this.m_BpConnecterSerialPort.Name = "m_BpConnecterSerialPort";
            this.m_BpConnecterSerialPort.Size = new System.Drawing.Size(108, 40);
            this.m_BpConnecterSerialPort.TabIndex = 13;
            this.m_BpConnecterSerialPort.Text = "Se Connecter";
            this.m_BpConnecterSerialPort.UseVisualStyleBackColor = true;
            this.m_BpConnecterSerialPort.Click += new System.EventHandler(this.m_BpConnecterSerialPort_Click);
            // 
            // m_TxtSerialPort
            // 
            this.m_TxtSerialPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtSerialPort.Location = new System.Drawing.Point(430, 240);
            this.m_TxtSerialPort.Name = "m_TxtSerialPort";
            this.m_TxtSerialPort.Size = new System.Drawing.Size(100, 22);
            this.m_TxtSerialPort.TabIndex = 11;
            // 
            // m_TxtSerialBaud
            // 
            this.m_TxtSerialBaud.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtSerialBaud.Location = new System.Drawing.Point(430, 274);
            this.m_TxtSerialBaud.Name = "m_TxtSerialBaud";
            this.m_TxtSerialBaud.Size = new System.Drawing.Size(100, 22);
            this.m_TxtSerialBaud.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud";
            // 
            // m_LblCarteNum
            // 
            this.m_LblCarteNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_LblCarteNum.AutoSize = true;
            this.m_LblCarteNum.Location = new System.Drawing.Point(672, 288);
            this.m_LblCarteNum.Name = "m_LblCarteNum";
            this.m_LblCarteNum.Size = new System.Drawing.Size(62, 16);
            this.m_LblCarteNum.TabIndex = 6;
            this.m_LblCarteNum.Text = "Carte N° :";
            // 
            // m_TimerSeconde
            // 
            this.m_TimerSeconde.Enabled = true;
            this.m_TimerSeconde.Interval = 1000;
            // 
            // m_BpAcquittement
            // 
            this.m_BpAcquittement.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpAcquittement.Location = new System.Drawing.Point(548, 331);
            this.m_BpAcquittement.Name = "m_BpAcquittement";
            this.m_BpAcquittement.Size = new System.Drawing.Size(108, 40);
            this.m_BpAcquittement.TabIndex = 15;
            this.m_BpAcquittement.Text = "Acquitement";
            this.m_BpAcquittement.UseVisualStyleBackColor = true;
            this.m_BpAcquittement.Click += new System.EventHandler(this.m_BpAcquittement_Click);
            // 
            // m_TxtNbrPortique
            // 
            this.m_TxtNbrPortique.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtNbrPortique.Location = new System.Drawing.Point(263, 111);
            this.m_TxtNbrPortique.Name = "m_TxtNbrPortique";
            this.m_TxtNbrPortique.Size = new System.Drawing.Size(108, 22);
            this.m_TxtNbrPortique.TabIndex = 3;
            this.m_TxtNbrPortique.TextChanged += new System.EventHandler(this.m_TxtNbrPortique_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nombre de portique";
            // 
            // m_BoxNumPortique
            // 
            this.m_BoxNumPortique.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_BoxNumPortique.FormattingEnabled = true;
            this.m_BoxNumPortique.Location = new System.Drawing.Point(548, 240);
            this.m_BoxNumPortique.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_BoxNumPortique.Name = "m_BoxNumPortique";
            this.m_BoxNumPortique.Size = new System.Drawing.Size(108, 24);
            this.m_BoxNumPortique.TabIndex = 10;
            this.m_BoxNumPortique.SelectedIndexChanged += new System.EventHandler(this.m_BoxNumPortique_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Duree Ouverture Portique";
            // 
            // m_TxtTpOuverturePortique
            // 
            this.m_TxtTpOuverturePortique.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtTpOuverturePortique.Location = new System.Drawing.Point(263, 56);
            this.m_TxtTpOuverturePortique.Name = "m_TxtTpOuverturePortique";
            this.m_TxtTpOuverturePortique.Size = new System.Drawing.Size(108, 22);
            this.m_TxtTpOuverturePortique.TabIndex = 1;
            this.m_TxtTpOuverturePortique.TextChanged += new System.EventHandler(this.m_TxtTpOuverturePortique_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Temp min avant passage";
            // 
            // m_TxtTpMinPassage
            // 
            this.m_TxtTpMinPassage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtTpMinPassage.Location = new System.Drawing.Point(263, 84);
            this.m_TxtTpMinPassage.Name = "m_TxtTpMinPassage";
            this.m_TxtTpMinPassage.Size = new System.Drawing.Size(108, 22);
            this.m_TxtTpMinPassage.TabIndex = 2;
            this.m_TxtTpMinPassage.TextChanged += new System.EventHandler(this.m_TxtTpMinPassage_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(377, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "s";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(377, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "s";
            // 
            // m_BpSimulateurRfid
            // 
            this.m_BpSimulateurRfid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpSimulateurRfid.Location = new System.Drawing.Point(422, 331);
            this.m_BpSimulateurRfid.Name = "m_BpSimulateurRfid";
            this.m_BpSimulateurRfid.Size = new System.Drawing.Size(108, 40);
            this.m_BpSimulateurRfid.TabIndex = 14;
            this.m_BpSimulateurRfid.Text = "Simulateur RFID";
            this.m_BpSimulateurRfid.UseVisualStyleBackColor = true;
            this.m_BpSimulateurRfid.Click += new System.EventHandler(this.m_BpSimulateurRfid_Click);
            // 
            // m_TxtBoxListesCartes
            // 
            this.m_TxtBoxListesCartes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_TxtBoxListesCartes.FormattingEnabled = true;
            this.m_TxtBoxListesCartes.Location = new System.Drawing.Point(496, 106);
            this.m_TxtBoxListesCartes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TxtBoxListesCartes.Name = "m_TxtBoxListesCartes";
            this.m_TxtBoxListesCartes.Size = new System.Drawing.Size(108, 24);
            this.m_TxtBoxListesCartes.TabIndex = 22;
            // 
            // m_TxtAjouterCarte
            // 
            this.m_TxtAjouterCarte.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_TxtAjouterCarte.Location = new System.Drawing.Point(496, 51);
            this.m_TxtAjouterCarte.Name = "m_TxtAjouterCarte";
            this.m_TxtAjouterCarte.Size = new System.Drawing.Size(108, 22);
            this.m_TxtAjouterCarte.TabIndex = 20;
            // 
            // m_BpAjouterNumCarte
            // 
            this.m_BpAjouterNumCarte.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpAjouterNumCarte.Location = new System.Drawing.Point(619, 42);
            this.m_BpAjouterNumCarte.Name = "m_BpAjouterNumCarte";
            this.m_BpAjouterNumCarte.Size = new System.Drawing.Size(108, 40);
            this.m_BpAjouterNumCarte.TabIndex = 21;
            this.m_BpAjouterNumCarte.Text = "Ajouter Carte";
            this.m_BpAjouterNumCarte.UseVisualStyleBackColor = true;
            this.m_BpAjouterNumCarte.Click += new System.EventHandler(this.m_BpAjouterNumCarte_Click);
            // 
            // m_TxtBoxOutils
            // 
            this.m_TxtBoxOutils.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_TxtBoxOutils.FormattingEnabled = true;
            this.m_TxtBoxOutils.Location = new System.Drawing.Point(619, 93);
            this.m_TxtBoxOutils.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TxtBoxOutils.Name = "m_TxtBoxOutils";
            this.m_TxtBoxOutils.Size = new System.Drawing.Size(108, 24);
            this.m_TxtBoxOutils.TabIndex = 23;
            this.m_TxtBoxOutils.SelectedIndexChanged += new System.EventHandler(this.m_TxtBoxOutils_SelectedIndexChanged);
            // 
            // m_BpModifierCarte
            // 
            this.m_BpModifierCarte.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpModifierCarte.Location = new System.Drawing.Point(567, 156);
            this.m_BpModifierCarte.Name = "m_BpModifierCarte";
            this.m_BpModifierCarte.Size = new System.Drawing.Size(108, 40);
            this.m_BpModifierCarte.TabIndex = 25;
            this.m_BpModifierCarte.Text = "Modifier";
            this.m_BpModifierCarte.UseVisualStyleBackColor = true;
            this.m_BpModifierCarte.Click += new System.EventHandler(this.m_BpModifierCarte_Click);
            // 
            // m_TxtBoxListOutils
            // 
            this.m_TxtBoxListOutils.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_TxtBoxListOutils.FormattingEnabled = true;
            this.m_TxtBoxListOutils.Location = new System.Drawing.Point(619, 127);
            this.m_TxtBoxListOutils.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TxtBoxListOutils.Name = "m_TxtBoxListOutils";
            this.m_TxtBoxListOutils.Size = new System.Drawing.Size(108, 24);
            this.m_TxtBoxListOutils.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(491, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 25);
            this.label9.TabIndex = 30;
            this.label9.Text = "Forfait";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(85, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 25);
            this.label10.TabIndex = 31;
            this.label10.Text = "Parametres";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(391, 200);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 25);
            this.label11.TabIndex = 32;
            this.label11.Text = "Portiques";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(133, 200);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 25);
            this.label12.TabIndex = 33;
            this.label12.Text = "Secteur";
            // 
            // m_TxtBoxSecteurPortique
            // 
            this.m_TxtBoxSecteurPortique.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_TxtBoxSecteurPortique.FormattingEnabled = true;
            this.m_TxtBoxSecteurPortique.Location = new System.Drawing.Point(138, 243);
            this.m_TxtBoxSecteurPortique.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TxtBoxSecteurPortique.Name = "m_TxtBoxSecteurPortique";
            this.m_TxtBoxSecteurPortique.Size = new System.Drawing.Size(108, 24);
            this.m_TxtBoxSecteurPortique.TabIndex = 30;
            this.m_TxtBoxSecteurPortique.SelectedIndexChanged += new System.EventHandler(this.m_TxtBoxSecteurPortique_SelectedIndexChanged);
            // 
            // m_TxtBoxSecteur
            // 
            this.m_TxtBoxSecteur.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_TxtBoxSecteur.FormattingEnabled = true;
            this.m_TxtBoxSecteur.Location = new System.Drawing.Point(138, 280);
            this.m_TxtBoxSecteur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TxtBoxSecteur.Name = "m_TxtBoxSecteur";
            this.m_TxtBoxSecteur.Size = new System.Drawing.Size(108, 24);
            this.m_TxtBoxSecteur.TabIndex = 31;
            this.m_TxtBoxSecteur.SelectedIndexChanged += new System.EventHandler(this.m_TxtBoxSecteur_SelectedIndexChanged);
            // 
            // m_BpSecteurModifier
            // 
            this.m_BpSecteurModifier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_BpSecteurModifier.Location = new System.Drawing.Point(138, 309);
            this.m_BpSecteurModifier.Name = "m_BpSecteurModifier";
            this.m_BpSecteurModifier.Size = new System.Drawing.Size(108, 40);
            this.m_BpSecteurModifier.TabIndex = 32;
            this.m_BpSecteurModifier.Text = "Modifier";
            this.m_BpSecteurModifier.UseVisualStyleBackColor = true;
            this.m_BpSecteurModifier.Click += new System.EventHandler(this.m_BpSecteurModifier_Click);
            // 
            // CRfid
            // 
            this.ClientSize = new System.Drawing.Size(831, 436);
            this.Controls.Add(this.m_BpSecteurModifier);
            this.Controls.Add(this.m_TxtBoxSecteur);
            this.Controls.Add(this.m_TxtBoxSecteurPortique);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_TxtBoxListOutils);
            this.Controls.Add(this.m_BpModifierCarte);
            this.Controls.Add(this.m_TxtBoxOutils);
            this.Controls.Add(this.m_BpAjouterNumCarte);
            this.Controls.Add(this.m_TxtAjouterCarte);
            this.Controls.Add(this.m_TxtBoxListesCartes);
            this.Controls.Add(this.m_BpSimulateurRfid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_TxtTpOuverturePortique);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_TxtTpMinPassage);
            this.Controls.Add(this.m_BoxNumPortique);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_TxtNbrPortique);
            this.Controls.Add(this.m_BpAcquittement);
            this.Controls.Add(this.m_LblCarteNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_TxtSerialBaud);
            this.Controls.Add(this.m_TxtSerialPort);
            this.Controls.Add(this.m_BpConnecterSerialPort);
            this.Controls.Add(this.m_lblNumeroCarte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CRfid";
            this.Text = "BeepBop Amelioration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //private System.IO.Ports.SerialPort m_serialPortRfid;
        private System.Windows.Forms.Label m_lblNumeroCarte;
        private System.Windows.Forms.Button m_BpConnecterSerialPort;
        private System.Windows.Forms.TextBox m_TxtSerialPort;
        private System.Windows.Forms.TextBox m_TxtSerialBaud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_LblCarteNum;
        public System.Windows.Forms.Timer m_TimerSeconde;
        private System.Windows.Forms.Button m_BpAcquittement;
        private System.Windows.Forms.TextBox m_TxtNbrPortique;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_BoxNumPortique;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_TxtTpOuverturePortique;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_TxtTpMinPassage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button m_BpSimulateurRfid;
        private System.Windows.Forms.ComboBox m_TxtBoxListesCartes;
        private System.Windows.Forms.TextBox m_TxtAjouterCarte;
        private System.Windows.Forms.Button m_BpAjouterNumCarte;
        private System.Windows.Forms.ComboBox m_TxtBoxOutils;
        private System.Windows.Forms.Button m_BpModifierCarte;
        private System.Windows.Forms.ComboBox m_TxtBoxListOutils;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox m_TxtBoxSecteurPortique;
        private System.Windows.Forms.ComboBox m_TxtBoxSecteur;
        private System.Windows.Forms.Button m_BpSecteurModifier;
    }
}

