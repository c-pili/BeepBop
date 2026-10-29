using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    public partial class CBeepBop : Form
    {
        const float temp_passage = 10;
        CAutomate Automate;
        CBaseDeDonnees BaseDeDonnees;
        CPortique Portique;
        public CBeepBop()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            BaseDeDonnees = new CBaseDeDonnees();
            Automate = new CAutomate();
            Portique = new CPortique();
            //this.m_serialPortRfid.Open();
        }

        private int LectureNumeroCarte()
        {
            // Récupération des 12 octets avec ReadExisting :
            string l_trame = this.m_serialPortRfid.ReadExisting();
            int l_numeroCarte;
            string l_derniersOctets = l_trame.Substring(7, 4);
            l_numeroCarte = Int32.Parse(l_derniersOctets,
            System.Globalization.NumberStyles.HexNumber);
            return l_numeroCarte;
        }

        private void CBeepBop_Load(object sender, EventArgs e)
        {

        }

        private void m_serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int NumCarte = LectureNumeroCarte();
            m_lblNumeroCarte.Text = NumCarte.ToString();

            if (BaseDeDonnees.EstAutorise(NumCarte))
            {
                label3.BackColor = Color.LightGreen;
            }
            else
            {
                label3.BackColor = Color.LightCoral;
            }
        }

        private void m_BpConnecterSerialPort_Click(object sender, EventArgs e)
        {
            if (!this.m_serialPortRfid.IsOpen)
            {
                if (this.m_serialPortRfid.PortName == "")
                {
                    MessageBox.Show("Veuillez choisir un port série.");
                    return;
                }
                if (this.m_serialPortRfid.BaudRate == 0)
                {
                    MessageBox.Show("Veuillez choisir une vitesse de transmission en bauds.");
                    return;
                }
                try
                {
                    this.m_serialPortRfid.PortName = "COM" + this.m_TxtSerialPort.Text.ToString();
                    this.m_serialPortRfid.BaudRate = int.Parse(this.m_TxtSerialBaud.Text);
                    this.m_serialPortRfid.Open();
                    this.m_BpConnecterSerialPort.Text = "Se Déconnecter";
                }
                catch (Exception x_exception)
                {
                    MessageBox.Show(x_exception.Message);
                }
            }
            else
            {
                try
                {
                    this.m_serialPortRfid.Close();
                    this.m_BpConnecterSerialPort.Text = "Se Connecter";
                }
                catch (Exception x_exception)
                {
                    MessageBox.Show(x_exception.Message);
                }
            }
        }

        private void m_Timer2s_Tick(object sender, EventArgs e)
        {
            Automate.Evolue(TEvent.TIMER2S);
            this.m_Timer2s.Enabled = false;
        }

        private void m_BpAcquittement_Click(object sender, EventArgs e)
        {
            Automate.Evolue(TEvent.APPUIACQ);
        }

        public void FermerPortique()
        {
            Portique.Fermer();
            Portique.Show();
        }

        public void OuvrirPortique()
        {
            Portique.Ouvrir();
            Portique.Show();
        }
        public void ErreurPortique()
        {
            Portique.Erreur();
            Portique.Show();
        }
    }
}