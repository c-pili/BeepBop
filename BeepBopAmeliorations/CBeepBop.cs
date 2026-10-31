using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    public partial class CBeepBop : Form
    {
        CAutomate[] Automate;
        CBaseDeDonnees BaseDeDonnees;
        CPortique[] Portique;
        SerialPort[] m_serialPortRfid;
        private int NbrPortique;
        private int DureeOuverture;
        private float TempMinPassage;
        private int m_TempTimerSecondes;
        public struct TPortique
        {
            public bool Connectee;
            public int Port;
            public int Baud;
            public int trame;
        }
        TPortique[] m_Portique;
        public CBeepBop()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            //this.m_serialPortRfid.Open();
            this.m_BoxNumPortique.Text = "Selectionne le numero du portique";
        }

        private int LectureNumeroCarte()
        {
            // Récupération des 12 octets avec ReadExisting :
            string l_trame = this.m_serialPortRfid[0].ReadExisting();
            int l_numeroCarte;
            string l_derniersOctets = l_trame.Substring(7, 4);
            l_numeroCarte = Int32.Parse(l_derniersOctets,
            System.Globalization.NumberStyles.HexNumber);
            return l_numeroCarte;
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
            if(this.m_BoxNumPortique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Port = int.Parse(this.m_TxtSerialPort.Text);
            this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Baud = int.Parse(this.m_TxtSerialBaud.Text);

            if (!this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Connectee)
            {
                if (m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].PortName == "")
                {
                    MessageBox.Show("Veuillez choisir un port série");
                    return;
                }
                if (this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].BaudRate == 0)
                {
                    MessageBox.Show("Veuillez choisir une vitesse de transmission en bauds");
                    return;
                }
                try
                {
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].PortName = "COM" + this.m_TxtSerialPort.Text.ToString();
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].BaudRate = int.Parse(this.m_TxtSerialBaud.Text);
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Open();
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Connectee = true;
                    this.m_BpConnecterSerialPort.Text = "Se Déconnecter";
                }
                catch (Exception x_exception)
                {
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Connectee = false;
                    MessageBox.Show(x_exception.Message);
                }
            }
            else
            {
                try
                {
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Close();
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Connectee = false;
                    this.m_BpConnecterSerialPort.Text = "Se Connecter";
                }
                catch (Exception x_exception)
                {
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Connectee = true;
                    MessageBox.Show(x_exception.Message);
                }
            }
        }

        private void m_TimerSeconde_Tick(object sender, EventArgs e)
        {
            if (this.m_TempTimerSecondes >= DureeOuverture)
            {
                this.m_TempTimerSecondes = 0;
                for (int i = 0; i < NbrPortique; i++)
                {
                    Automate[i].Evolue(TEvent.TIMER2S, i);
                }
            }
            this.m_TempTimerSecondes++;
        }

        private void m_BpAcquittement_Click(object sender, EventArgs e)
        {
            if(this.m_BoxNumPortique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            Automate[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Evolue(TEvent.APPUIACQ, int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()));
        }

        public void FermerPortique(int index)
        {
            Portique[index].Fermer(index);
            Portique[index].Show();
        }

        public void OuvrirPortique(int index)
        {
            Portique[index].Ouvrir(index);
            Portique[index].Show();
        }
        public void ErreurPortique(int index)
        {
            Portique[index].Erreur(index);
            Portique[index].Show();
        }

        private void m_TxtNbrPortique_TextChanged(object sender, EventArgs e)
        {
            this.NbrPortique = int.Parse(this.m_TxtNbrPortique.Text);

            Portique = new CPortique[NbrPortique]; // tableau de portique
            Automate = new CAutomate[NbrPortique];
            m_serialPortRfid = new SerialPort[NbrPortique];
            m_Portique = new TPortique[NbrPortique]; //fenetre portique
            BaseDeDonnees = new CBaseDeDonnees(this.TempMinPassage);

            this.m_BoxNumPortique.Items.Clear();
            for (int i = NbrPortique; i > 0; i--)
            {
                this.m_BoxNumPortique.Items.Add(i);

                Portique[i - 1] = new CPortique();
                Automate[i - 1] = new CAutomate();
                m_serialPortRfid[i - 1] = new SerialPort();
                m_Portique[i - 1] = new TPortique();
            }
        }

        private void m_BoxNumPortique_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_TxtSerialPort.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Port).ToString());
            this.m_TxtSerialBaud.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Baud).ToString());
            this.m_BpConnecterSerialPort.Text = m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())].Connectee ? "Se Déconnecter" : "Se Connecter";
        }

        private void serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
           
        }

        private void m_TxtTpOuverturePortique_TextChanged(object sender, EventArgs e)
        {
            this.DureeOuverture = int.Parse(this.m_TxtTpOuverturePortique.Text);
        }

        private void m_TxtTpMinPassage_TextChanged(object sender, EventArgs e)
        {
            this.TempMinPassage = int.Parse(this.m_TxtTpMinPassage.Text);
        }
    }
}