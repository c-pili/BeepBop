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
             * -> Ajout Simulateur pour chaque portique
             * -> Ajout Modification Forfait
             * -> Ajout Secteur Forfait
            */
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            this.m_BoxNumPortique.Text = "Selectionne le numero du portique";
            this.m_TxtBoxListesCartes.Text = "Listes des cartes";
            this.m_TxtBoxOutils.Text = "Outils";
            this.m_TxtBoxListOutils.Text = "Propriété";
            this.m_TxtBoxSecteurPortique.Text = "Choix Portiques";
            this.m_TxtBoxSecteur.Text = "Choix du Secteur";

            this.m_TxtBoxOutils.Items.Add("Duree Validitee");
            this.m_TxtBoxOutils.Items.Add("Autorisée");
            this.m_TxtBoxOutils.Items.Add("Supprimer");
            this.m_TxtBoxOutils.Items.Add("Secteur");

            this.m_TxtBoxSecteur.Items.Add("Chatel");
            this.m_TxtBoxSecteur.Items.Add("Avoriaz");
            this.m_TxtBoxSecteur.Items.Add("Portes du Soleil");
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

            bool Autorisée = BaseDeDonnees.EstAutorise(m_Portique[index].trame, index + 1);
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

                Portique = new CPortique[this.NbrPortique]; // tableau de portique
                Automate = new CAutomate[this.NbrPortique];
                m_serialPortRfid = new SerialPort[this.NbrPortique];
                m_Portique = new TPortique[this.NbrPortique]; //fenetre portique
                BaseDeDonnees.CreePortique(this.NbrPortique);

                this.m_BoxNumPortique.Items.Clear();
                for (int i = NbrPortique; i > 0; i--)
                {
                    this.m_BoxNumPortique.Items.Add(i);
                    this.m_TxtBoxSecteurPortique.Items.Add(i);

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
            this.m_TxtSerialPort.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Port).ToString());
            this.m_TxtSerialBaud.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Baud).ToString());
            this.m_BpConnecterSerialPort.Text = m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee ? "Se Déconnecter" : "Se Connecter";
        }

        private void m_TxtTpOuverturePortique_TextChanged(object sender, EventArgs e)
        {
            if(m_TxtTpOuverturePortique.Text == "")
            {
                return;
            }
            try
            {
                this.DureeOuverture = int.Parse(this.m_TxtTpOuverturePortique.Text);
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
            }
        }

        private void m_TxtTpMinPassage_TextChanged(object sender, EventArgs e)
        {
            if (m_TxtTpMinPassage.Text == "")
            {
                return;
            }
            try
            {
                this.TempMinPassage = int.Parse(this.m_TxtTpMinPassage.Text);
                BaseDeDonnees = new CBaseDeDonnees(this.TempMinPassage);

                AfficherNumCarte();
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
                return;
            }

        }

        public void AfficherNumCarte()
        {
            this.m_TxtBoxListesCartes.Items.Clear();
            for (int i = 0; i < BaseDeDonnees.NbrForfaitUtilisee; i++)
            {
                if(BaseDeDonnees.m_forfaits[i].m_NumeroCarte == 0)
                {

                }
                else
                {
                    this.m_TxtBoxListesCartes.Items.Add(BaseDeDonnees.m_forfaits[i].m_NumeroCarte);
                }
            }
        }

        private void m_BpSimulateurRfid_Click(object sender, EventArgs e)
        {
            if (this.m_TxtTpOuverturePortique.Text == "")
            {
                MessageBox.Show("Veuillez renseigner une duree d'ouverture du portique avant simulation");
                return;
            }
            else if (this.m_TxtTpMinPassage.Text == "")
            {
                MessageBox.Show("Veuillez renseigner un temp de passage minimum entre 2 forfait");
                return;
            }
            else if(this.m_BoxNumPortique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
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

            bool Autorisée = BaseDeDonnees.EstAutorise(NumCarte, index + 1);
            if (Autorisée)
            {
                Automate[index].Evolue(TEvent.AUTORISEE, NumCarte);
            }
            else
            {
                Automate[index].Evolue(TEvent.NON_AUTORISEE, NumCarte);
            }
        }

        private void m_BpAjouterNumCarte_Click(object sender, EventArgs e)
        {
            if(this.m_BpAjouterNumCarte.Text == "")
            {
                MessageBox.Show("Veuillez mettre un numero de forfait");
                return;
            }
            else if(BaseDeDonnees == null)
            {
                MessageBox.Show("Veuillez ajouter le temps de passage entre 2 forfait");
                return;
            }
            try
            {
                BaseDeDonnees.AjouterForfait(int.Parse(this.m_TxtAjouterCarte.Text.ToString()));
                AfficherNumCarte();
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
                return;
            }
        }

        private void m_TxtBoxOutils_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = BaseDeDonnees.IndexForfait(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()));
            if (this.m_TxtBoxOutils.Text == "Duree Validitee")
            {
                this.m_TxtBoxListOutils.Items.Clear();
                this.m_TxtBoxListOutils.Items.Add("1j");
                this.m_TxtBoxListOutils.Items.Add("5j");
                this.m_TxtBoxListOutils.Items.Add("7j");
                this.m_TxtBoxListOutils.Items.Add("14j");

                this.m_TxtBoxListOutils.Text = BaseDeDonnees.m_forfaits[index].m_DureeValidite.ToString();
            }
            else if(this.m_TxtBoxOutils.Text == "Autorisée")
            {
                this.m_TxtBoxListOutils.Items.Clear();
                this.m_TxtBoxListOutils.Items.Add("True");
                this.m_TxtBoxListOutils.Items.Add("False");

                this.m_TxtBoxListOutils.Text = BaseDeDonnees.m_forfaits[index].m_estAutorise.ToString();
            }
            else if(this.m_TxtBoxOutils.Text == "Supprimer")
            {
                this.m_TxtBoxListOutils.Items.Clear();
                this.m_TxtBoxListOutils.Items.Add("True");
                this.m_TxtBoxListOutils.Items.Add("False");

                this.m_TxtBoxListOutils.Text = "False";
            }
            else if (this.m_TxtBoxOutils.Text == "Secteur")
            {
                this.m_TxtBoxListOutils.Items.Clear();
                this.m_TxtBoxListOutils.Items.Add("Chatel");
                this.m_TxtBoxListOutils.Items.Add("Avoriaz");
                this.m_TxtBoxListOutils.Items.Add("Portes du Soleil");

                this.m_TxtBoxListOutils.Text = BaseDeDonnees.m_forfaits[index].m_secteur.ToString();
            }
        }

        private void m_BpModifierCarte_Click(object sender, EventArgs e)
        {
            if(this.m_TxtBoxListOutils.Text == "")
            {
                MessageBox.Show("Veuillez mettre un numero de forfait ou selectionner un forfait");
                return;
            }
            else if (this.m_TxtBoxOutils.SelectedItem == "Duree Validitee")
            {
                BaseDeDonnees.ChangerValiditee(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()), this.m_TxtBoxListOutils.SelectedItem.ToString());
            }
            else if(this.m_TxtBoxOutils.SelectedItem == "Autorisée")
            {
                bool Autorisation;
                if(this.m_TxtBoxListOutils.SelectedItem == "True")
                {
                    Autorisation = true;
                }
                else
                {
                    Autorisation = false;
                }
                if(BaseDeDonnees == null)
                {
                    MessageBox.Show("Veuillez ajouter le temps de passage entre 2 forfait");
                    return;
                }
                BaseDeDonnees.ChangerAutorisation(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()), Autorisation);
            }
            else if (this.m_TxtBoxOutils.SelectedItem == "Supprimer")
            {
                bool Supprimer;
                if (this.m_TxtBoxListOutils.SelectedItem == "True")
                {
                    Supprimer = true;
                }
                else
                {
                    Supprimer = false;
                }
                BaseDeDonnees.SupprimerForfait(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()));
                AfficherNumCarte();
            }
            else if (this.m_TxtBoxOutils.SelectedItem == "Secteur")
            {
                BaseDeDonnees.ChangerSecteur(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()), this.m_TxtBoxListOutils.SelectedItem.ToString());
            }
        }

        private void m_TxtBoxSecteurPortique_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_TxtBoxSecteur.Text = BaseDeDonnees.SecteurPortique[int.Parse(this.m_TxtBoxSecteurPortique.SelectedItem.ToString())-1].ToString();
        }

        private void m_BpSecteurModifier_Click(object sender, EventArgs e)
        {
            BaseDeDonnees.ModifierSecteur(int.Parse(this.m_TxtBoxSecteurPortique.SelectedItem.ToString()), this.m_TxtBoxSecteur.SelectedItem.ToString());
        }
    }
}