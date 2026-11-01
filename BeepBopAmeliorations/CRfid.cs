using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    public partial class CRfid : Form
    {
        CAutomate[] Automate;
        CBaseDeDonnees BaseDeDonnees;
        CPortique[] Portique;
        SerialPort[] m_serialPortRfid;
        CSimulateurRfid[] SimulateurRfid;
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
        public CRfid()
        {
            /*
             * Amelioration
             * -> Ajout parametre tp passage min + Ouverture portique
             * -> Ajout date de validité forfait(en jour)
             * -> Ajout TxtBox pour chaque portique
            */
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.m_BoxNumPortique.Text = "Selectionne le numero du portique";
        }

        private int LectureNumeroCarte(int index)
        {
            // Récupération des 12 octets avec ReadExisting :
            string l_trame = this.m_serialPortRfid[index].ReadExisting();
            int l_numeroCarte;
            string l_derniersOctets = l_trame.Substring(7, 4);
            l_numeroCarte = Int32.Parse(l_derniersOctets,
            System.Globalization.NumberStyles.HexNumber);
            return l_numeroCarte;
        }

        private void m_serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort spEmetteur = (SerialPort)sender;
            int index = Array.IndexOf(this.m_serialPortRfid, spEmetteur); //numero du tableau du port com

            m_Portique[index].trame = LectureNumeroCarte(index); //num carte

            bool Autorisée = BaseDeDonnees.EstAutorise(m_Portique[index].trame);
            if (Autorisée)
            {
                Automate[index].Evolue(TEvent.AUTORISEE, m_Portique[index].trame);
            }
            else
            {
                Automate[index].Evolue(TEvent.NON_AUTORISEE, m_Portique[index].trame);
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

        private void m_BpAcquittement_Click(object sender, EventArgs e)
        {
            if(this.m_BoxNumPortique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            Automate[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Evolue(TEvent.APPUIACQ, 0);
        }

        public void FermerPortique(int index)
        {
            Portique[index].Fermer(index);
            Portique[index].Show();
        }

        public void OuvrirPortique(int index, int NumForfait)
        {
            Portique[index].Ouvrir(index, NumForfait);
            Portique[index].Show();
        }
        public void ErreurPortique(int index, int NumForfait)
        {
            Portique[index].Erreur(index, NumForfait);
            Portique[index].Show();
        }

        private void m_TxtNbrPortique_TextChanged(object sender, EventArgs e)
        {
            if(this.m_TxtNbrPortique.Text == null)
            {
                return;
            }
            else if(this.m_TxtTpOuverturePortique == null)
            {
                MessageBox.Show("Veuillez choisir une durée de temps pour l'ouverture du portique");
                return;
            }

            try
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
                    Automate[i - 1] = new CAutomate(this, this.DureeOuverture, i);
                    m_serialPortRfid[i - 1] = new SerialPort();
                    m_Portique[i - 1] = new TPortique();
                    m_serialPortRfid[i - 1].DataReceived += m_serialPortRfid_DataReceived;
                }
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
            }
        }

        private void m_BoxNumPortique_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_TxtSerialPort.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Port).ToString());
            this.m_TxtSerialBaud.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Baud).ToString());
            this.m_BpConnecterSerialPort.Text = m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Connectee ? "Se Déconnecter" : "Se Connecter";
        }

        private void m_TxtTpOuverturePortique_TextChanged(object sender, EventArgs e)
        {
            if(m_TxtTpOuverturePortique.Text == "")
            {
                return;
            }
            this.DureeOuverture = int.Parse(this.m_TxtTpOuverturePortique.Text);
        }

        private void m_TxtTpMinPassage_TextChanged(object sender, EventArgs e)
        {
            if (m_TxtTpMinPassage.Text == "")
            {
                return;
            }
            this.TempMinPassage = int.Parse(this.m_TxtTpMinPassage.Text);
        }

        private void m_BpSimulateurRfid_Click(object sender, EventArgs e)
        {
            if (this.m_TxtTpOuverturePortique.Text == "")
            {
                MessageBox.Show("Veuillez renseigner une duree d'ouverture du portique avant simulation");
                return;
            }
            if (this.m_TxtTpMinPassage.Text == "")
            {
                MessageBox.Show("Veuillez renseigner un temp de passage minimum entre 2 forfait");
                return;
            }
            SimulateurRfid = new CSimulateurRfid[NbrPortique];
            SimulateurRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1] = new CSimulateurRfid(int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())); // Instanciation de la fenêtre
            SimulateurRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].BadgeScannee += SimulateurRfid_BadgeScannee;
        }

        private void SimulateurRfid_BadgeScannee(object sender, EventArgs e)
        {
            CSimulateurRfid spEmetteur = (CSimulateurRfid)sender;
            int NumCarte = spEmetteur.NumeroCarte;
            int index = Array.IndexOf(SimulateurRfid, spEmetteur);

            bool Autorisée = BaseDeDonnees.EstAutorise(NumCarte);
            if (Autorisée)
            {
                Automate[index].Evolue(TEvent.AUTORISEE, NumCarte);
            }
            else
            {
                Automate[index].Evolue(TEvent.NON_AUTORISEE, NumCarte);
            }
        }
    }
}