using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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
        private int NbrPortique; // Nombre de portiques
        private int DureeOuverture; // Temps d'ouverture du portique en secondes
        private float TempMinPassage; // Temps minimum entre 2 passage d'un forfait en secondes
        private DateTime DateDernierPassage; //Time du dernier passage d'un forfait pour eviter le spam console du portique
        private readonly int TempDetection = 200; //Millisecondes
        public struct TPortique // Structure pour les portiques
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
             * -> Ajout TxtBox(console) pour chaque portique
             * -> Ajout Simulateur pour chaque portique
             * -> Ajout Modification Forfait
             * -> Ajout Secteur Forfait
             * -> Changement dynamique des portiques (faut-il libérer la mememoire ? faut-il realloc en c# ?)
            */

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = true;

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

            this.m_TxtBoxSecteur.Items.Add("CHATEL");
            this.m_TxtBoxSecteur.Items.Add("AVORIAZ");
        }

        private int LectureNumeroCarte(int index)
        {
            // Récupération des 12 octets avec ReadExisting :
            string l_trame = this.m_serialPortRfid[index].ReadExisting();
            if (l_trame.Length < 12)
            {
                return 0;
            }
            string l_derniersOctets = l_trame.Substring(7, 4);
            int l_numeroCarte = Int32.Parse(l_derniersOctets,
            System.Globalization.NumberStyles.HexNumber);
            return l_numeroCarte;
        }

        private void m_serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort spEmetteur = (SerialPort)sender;
            Thread.Sleep(50);
            int index = Array.IndexOf(this.m_serialPortRfid, spEmetteur);

            m_Portique[index].trame = LectureNumeroCarte(index);

            if (m_Portique[index].trame == 0) //si erreur de lecture -> arrete la l
            {
                return;
            }
            else if(this.DateDernierPassage.AddMilliseconds(this.TempDetection) > DateTime.Now) //si le dernier passage est trop proche (spam console portique)
            {
                return;
            }

            this.Invoke(new Action(() => //permet de faire appel à l'interface graphique depuis un thread
            {
                try
                {
                    bool Autorisée = BaseDeDonnees.EstAutorise(m_Portique[index].trame, index); //verifie si le forfait est: secteur, passage, validitee,..
                    this.m_LblCarteNum.Text = "Carte N° : " + m_Portique[index].trame.ToString(); //Met a jour le label

                    if (Autorisée)
                    {
                        Automate[index].Evolue(TEvent.AUTORISEE, m_Portique[index].trame); //Autorisee Graph Etat
                        this.m_LblCarteNum.BackColor = Color.Green;
                    }
                    else
                    {
                        Automate[index].Evolue(TEvent.NON_AUTORISEE, m_Portique[index].trame); //Non autorisée Graph Etat
                        this.m_LblCarteNum.BackColor = Color.Red;
                    }
                    this.DateDernierPassage = DateTime.Now; //enregistre la date du dernier passage pour eviter le spam console du portique
                }
                catch (Exception x_exception) //si erreur 
                {
                    MessageBox.Show(x_exception.Message);
                }
            }));
        }

        private void m_BpConnecterSerialPort_Click(object sender, EventArgs e)
        {
            if(this.m_BoxNumPortique.SelectedItem == null) //si rien selectionnee -> erreur
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            int index = int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1; //recupere l'index du portique selectionné

            if (!this.m_Portique[index].Connectee) //si le portique n'est pas connectee
            {
                if (this.m_TxtSerialPort.Text.ToString() == "") //erreur si rien a été choisi
                {
                    MessageBox.Show("Veuillez choisir un port série");
                    return;
                }
                if (int.Parse(this.m_TxtSerialBaud.Text) == 0) //erreur si rien a été choisi
                {
                    MessageBox.Show("Veuillez choisir une vitesse de transmission en bauds");
                    return;
                }
                this.m_Portique[index].Port = int.Parse(this.m_TxtSerialPort.Text); //enrigistre le port et le baud du portique selectionné
                this.m_Portique[index].Baud = int.Parse(this.m_TxtSerialBaud.Text);

                try
                {
                    this.m_serialPortRfid[index].PortName = "COM" + this.m_TxtSerialPort.Text.ToString(); //connexion au port com
                    this.m_serialPortRfid[index].BaudRate = int.Parse(this.m_TxtSerialBaud.Text);
                    this.m_serialPortRfid[index].Open();
                    this.m_Portique[index].Connectee = true; //enregistre la connexion
                    this.m_BpConnecterSerialPort.Text = "Se Déconnecter";
                }
                catch (Exception x_exception) //si erreur
                {
                    MessageBox.Show(x_exception.Message);
                }
            }
            else // si deconnexion
            {
                try
                {
                    this.m_serialPortRfid[index].Close();
                    this.m_Portique[index].Connectee = false; //enregistre que le port s'est deconnectee
                    this.m_BpConnecterSerialPort.Text = "Se Connecter";
                }
                catch (Exception x_exception) //si erreur
                {
                    MessageBox.Show(x_exception.Message);
                }
            }
        }

        private void m_BpAcquittement_Click(object sender, EventArgs e) //Bp pour acquitter un portique en erreur
        {
            if(this.m_BoxNumPortique.SelectedItem == null) //si aucun portique selectionnée
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            Automate[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Evolue(TEvent.APPUIACQ, 0); //envoie l'evenement d'acquittement au portique selectionné
        }

        public void FermerPortique(int index) //permet de fermer le portique selectionné
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => FermerPortique(index)));
                return;
            }
            Portique[index].Fermer(index);
            Portique[index].Show(); //ouvre la fenetre du portique selectionné
        }

        public void OuvrirPortique(int index, int NumForfait) //permet d'ouvrir le portique selectionné avec le numero de forfait
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OuvrirPortique(index, NumForfait))); //permet de faire appel à l'interface graphique depuis un thread
                return;
            }
            Portique[index].Ouvrir(index, NumForfait);
            Portique[index].Show();
        }
        public void ErreurPortique(int index, int NumForfait) //permet de mettre le portique selectionné en erreur avec le numero de forfait
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ErreurPortique(index, NumForfait)));
                return;
            }
            Portique[index].Erreur(index, NumForfait);
            Portique[index].Show();
        }

        private void m_TxtNbrPortique_TextChanged(object sender, EventArgs e) //permet de changer le nombre de portique
        {
            if(this.m_TxtNbrPortique.Text == null) //si rien ecrit en nbr portique
            {
                return;
            }
            else if(this.m_TxtTpOuverturePortique == null) //si rien ecrit en duree ouverture portique
            {
                MessageBox.Show("Veuillez choisir une durée de temps pour l'ouverture du portique");
                return;
            }
            else if(BaseDeDonnees == null) //si erreur lors de la creation de la base de données
            {
                MessageBox.Show("Veuillez remplir le temps entre 2 passage de forfait");
                return;
            }

            try
            {
                this.NbrPortique = int.Parse(this.m_TxtNbrPortique.Text);

                Portique = new CPortique[this.NbrPortique]; // tableau de portique
                Automate = new CAutomate[this.NbrPortique]; //tableau d'automate pour chaque portique
                m_serialPortRfid = new SerialPort[this.NbrPortique]; //tableau de ports série pour chaque portique
                m_Portique = new TPortique[this.NbrPortique]; //fenetre portique
                BaseDeDonnees.CreePortique(this.NbrPortique); //cree le nombre de portique dans la base de données

                this.m_BoxNumPortique.Items.Clear(); //vide le combo box de selection du portique
                this.m_TxtBoxSecteurPortique.Items.Clear(); //vide le combo box de selection du secteur du portique
                for (int i = NbrPortique; i > 0; i--) //boucle pour chaque portique
                {
                    this.m_BoxNumPortique.Items.Add(i); //ajoute le numero du portique dans le combo box
                    this.m_TxtBoxSecteurPortique.Items.Add(i); //ajoute le numero du portique dans le combo box

                    Portique[i - 1] = new CPortique(); // Instanciation de la fenêtre
                    Automate[i - 1] = new CAutomate(this, this.DureeOuverture, i); // Instanciation de l'automate pour chaque portique

                    m_serialPortRfid[i - 1] = new SerialPort //instancie serial port pour chaque portique
                    {
                        DtrEnable = true //active le dtr pour chaque serial port
                    };

                    m_Portique[i - 1] = new TPortique(); //instancie la structure portique pour chaque portique
                    m_serialPortRfid[i - 1].DataReceived += m_serialPortRfid_DataReceived; //ajoute l'evenement de reception de données pour chaque serial port
                }
                this.m_BoxNumPortique.Text = "Selectionne le numero du portique";
            }
            catch (Exception x_exception) //si erreur
            {
                MessageBox.Show(x_exception.Message);
            }
        }

        private void m_BoxNumPortique_SelectedIndexChanged(object sender, EventArgs e) //permet de changer le port et le baud du portique selectionné
        {
            this.m_TxtSerialPort.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Port).ToString()); //met les valeur de port enregistyrer pour le portique selectionné
            this.m_TxtSerialBaud.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Baud).ToString()); //met les valeur de baud enregistyrer pour le portique selectionné
            this.m_BpConnecterSerialPort.Text = m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee ? "Se Déconnecter" : "Se Connecter";
            this.m_LblCarteNum.Text = "Carte N° : ";
        }

        private void m_TxtTpOuverturePortique_TextChanged(object sender, EventArgs e)
        {
            if(m_TxtTpOuverturePortique.Text == "") //si rien ecrit en duree ouverture portique
            {
                return;
            }
            try
            {
                this.DureeOuverture = int.Parse(this.m_TxtTpOuverturePortique.Text); //recupere la duree d'ouverture du portique
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
            }
        }

        private void m_TxtTpMinPassage_TextChanged(object sender, EventArgs e)
        {
            if (m_TxtTpMinPassage.Text == "") //si rien ecrit
            {
                return;
            }
            try
            {
                this.TempMinPassage = int.Parse(this.m_TxtTpMinPassage.Text); //recupere le temps minimum entre 2 passage d'un forfait
                BaseDeDonnees = new CBaseDeDonnees(this.TempMinPassage); //cree la base de données avec le temps minimum entre 2 passage d'un forfait

                AfficherNumCarte(); //affiche les numéros de forfait dans la liste combo box
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
                return;
            }

        }

        public void AfficherNumCarte() //affiche les numéros de forfait dans la liste combo box
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

        private void m_BpSimulateurRfid_Click(object sender, EventArgs e) //Lance le simulateur pour le portique selectionné
        {
            if (this.m_TxtTpOuverturePortique.Text == "") //si rien ecrit en duree ouverture portique
            {
                MessageBox.Show("Veuillez renseigner une duree d'ouverture du portique avant simulation");
                return;
            }
            else if (this.m_TxtTpMinPassage.Text == "") //si rien ecrit en duree ouverture portique
            {
                MessageBox.Show("Veuillez renseigner un temp de passage minimum entre 2 forfait");
                return;
            }
            else if(this.m_BoxNumPortique.SelectedItem == null) //si aucun portique selectionnée
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            SimulateurRfid = new CSimulateurRfid[NbrPortique]; // Objet simulateur pour chaque portique
            SimulateurRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1] = new CSimulateurRfid(int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())); // Instanciation de la fenêtre du simulateur pour le portique selectionné
            SimulateurRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].BadgeScannee += SimulateurRfid_BadgeScannee; // Ajout de l'événement BadgeScannee pour le portique selectionné
        }

        private void SimulateurRfid_BadgeScannee(object sender, EventArgs e) //evenement qui permet de simuler le passage d'un forfait dans le portique selectionné
        {
            try
            {
                CSimulateurRfid spEmetteur = (CSimulateurRfid)sender;
                int NumCarte = spEmetteur.NumeroCarte;
                int index = Array.IndexOf(SimulateurRfid, spEmetteur); //recup l'index du portique selectionné
                //try { }
                bool Autorisée = BaseDeDonnees.EstAutorise(NumCarte, index + 1); //verifie si la carte est autorisee
                if (Autorisée)
                {
                    Automate[index].Evolue(TEvent.AUTORISEE, NumCarte); //graph etat autorisee
                }
                else
                {
                    Automate[index].Evolue(TEvent.NON_AUTORISEE, NumCarte); //graph etat non autorisee
                }
            }
            catch (System.IndexOutOfRangeException) //si la valeur du nombre de portique a changé -> hor limite tableau
            {
                MessageBox.Show("Le portique n'existe plus !");
                return;
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
                return;
            }
        }

        private void m_BpAjouterNumCarte_Click(object sender, EventArgs e)
        {
            if(this.m_BpAjouterNumCarte.Text == "") //si rien ecrit en duree ouverture portique
            {
                MessageBox.Show("Veuillez mettre un numero de forfait");
                return;
            }
            else if(BaseDeDonnees == null) //si la base de données n'est pas initialisée
            {
                MessageBox.Show("Veuillez ajouter le temps de passage entre 2 forfait");
                return;
            }
            try
            {
                BaseDeDonnees.AjouterForfait(int.Parse(this.m_TxtAjouterCarte.Text.ToString())); //ajoute le forfait dans la base de données
                m_TxtAjouterCarte.Text = "";
                AfficherNumCarte(); //re-affiche les numéros de forfait dans la liste combo box
            }
            catch (Exception x_exception)
            {
                MessageBox.Show(x_exception.Message);
                return;
            }
        }

        private void m_TxtBoxOutils_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(BaseDeDonnees == null) //si la base de données n'est pas initialisée
            {
                MessageBox.Show("Veuillez ajouter le temps de passage entre 2 forfait");
                return;
            }
            else if(this.m_TxtBoxListesCartes.SelectedItem == null) //si aucun portique selectionnée
            {
                MessageBox.Show("Veuillez selectionner un forfait");
                return;
            }
            int index = BaseDeDonnees.IndexForfait(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString())); //recup l'index du forfait selectionné dans la base de données
            if (this.m_TxtBoxOutils.Text == "Duree Validitee")
            {
                this.m_TxtBoxListOutils.Items.Clear();//ajoute les durees de validitee dans le combo box
                this.m_TxtBoxListOutils.Items.Add("1j");
                this.m_TxtBoxListOutils.Items.Add("5j");
                this.m_TxtBoxListOutils.Items.Add("7j");
                this.m_TxtBoxListOutils.Items.Add("14j");

                this.m_TxtBoxListOutils.Text = BaseDeDonnees.m_forfaits[index].m_DureeValidite.ToString();
            }
            else if(this.m_TxtBoxOutils.Text == "Autorisée") //AUTORISATION FORFAIT
            {
                this.m_TxtBoxListOutils.Items.Clear(); //ajoute les valeurs autorisée dans le combo box
                this.m_TxtBoxListOutils.Items.Add("True");
                this.m_TxtBoxListOutils.Items.Add("False");

                this.m_TxtBoxListOutils.Text = BaseDeDonnees.m_forfaits[index].m_estAutorise.ToString(); //affiche l'autorisation du forfait selectionné
            }
            else if(this.m_TxtBoxOutils.Text == "Supprimer") //SUPPRIMER FORFAIT
            {
                this.m_TxtBoxListOutils.Items.Clear(); //ajoute les valeurs autorisée dans le combo box
                this.m_TxtBoxListOutils.Items.Add("True");
                this.m_TxtBoxListOutils.Items.Add("False");
                this.m_TxtBoxListOutils.Text = "False";
            }
            else if (this.m_TxtBoxOutils.Text == "Secteur") //SECTEUR FORFAIT
            {
                this.m_TxtBoxListOutils.Items.Clear();
                this.m_TxtBoxListOutils.Items.Add("Chatel"); //ajoute les secteurs dans le combo box
                this.m_TxtBoxListOutils.Items.Add("Avoriaz");
                this.m_TxtBoxListOutils.Items.Add("Portes du Soleil");

                this.m_TxtBoxListOutils.Text = BaseDeDonnees.m_forfaits[index].m_secteur.ToString(); //affiche le secteur du forfait selectionné
            }
        }

        private void m_BpModifierCarte_Click(object sender, EventArgs e) //Envoie les modifications du forfait selectionné dans la base de données
        {
            if(this.m_TxtBoxListOutils.Text == "" || this.m_TxtBoxListesCartes.SelectedItem == null) //si champ nulle ou rien selectionnée
            {
                MessageBox.Show("Veuillez mettre un numero de forfait ou selectionner un forfait");
                return;
            }
            else if (this.m_TxtBoxOutils.SelectedItem == "Duree Validitee") //DUREE VALIDITEE FORFAIT
            {
                BaseDeDonnees.ChangerValiditee(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()), this.m_TxtBoxListOutils.SelectedItem.ToString()); //change la validité du forfait
            }
            else if(this.m_TxtBoxOutils.SelectedItem == "Autorisée") //AUTORISATION FORFAIT
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
                if(BaseDeDonnees == null) //si la base de données n'est pas initialisée
                {
                    MessageBox.Show("Veuillez ajouter le temps de passage entre 2 forfait");
                    return;
                }
                BaseDeDonnees.ChangerAutorisation(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()), Autorisation); //change autorisation dans la base de données
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
                BaseDeDonnees.SupprimerForfait(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString())); //supprime le forfait dans la base de données
                this.m_TxtBoxListesCartes.Text = "";
                AfficherNumCarte(); //re-affiche les numéros de forfait dans la liste combo box
            }
            else if (this.m_TxtBoxOutils.SelectedItem == "Secteur")
            {
                BaseDeDonnees.ChangerSecteur(int.Parse(this.m_TxtBoxListesCartes.SelectedItem.ToString()), this.m_TxtBoxListOutils.SelectedItem.ToString()); //change le secteur du forfait dans la base de données
            }
        }

        private void m_TxtBoxSecteurPortique_SelectedIndexChanged(object sender, EventArgs e) //permet de changer le secteur du portique selectionné
        {
            this.m_TxtBoxSecteur.Text = BaseDeDonnees.SecteurPortique[int.Parse(this.m_TxtBoxSecteurPortique.SelectedItem.ToString())-1].ToString(); //affiche le secteur du portique selectionné
        }

        private void m_BpSecteurModifier_Click(object sender, EventArgs e) //permet de changer le secteur du portique selectionné
        {
            if(this.m_TxtBoxSecteurPortique.SelectedItem == null) //si aucun portique selectionnée
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            else if (this.m_TxtBoxSecteur.SelectedItem == null) //si aucun secteur selectionnée
            {
                MessageBox.Show("Veuillez choisir un secteur");
                return;
            }   
            BaseDeDonnees.ModifierSecteur(int.Parse(this.m_TxtBoxSecteurPortique.SelectedItem.ToString()), this.m_TxtBoxSecteur.SelectedItem.ToString());
        }

        private void m_TxtBoxSecteur_SelectedIndexChanged(object sender, EventArgs e) //permet de changer le secteur du portique selectionné
        {
            if (this.m_TxtBoxSecteurPortique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;

            }
        }
    }
}