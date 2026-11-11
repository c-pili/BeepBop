using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
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
        public CPortique[] Portique;
        SerialPort[] m_serialPortRfid;
        private int NbrPortique;
        private DateTime DateDernierPassage;
        private readonly int TempDetection = 600; //en millisecondes
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
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            BaseDeDonnees = new CBaseDeDonnees();
            //this.m_serialPortRfid.Open();
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
                    bool Autorisée = BaseDeDonnees.EstAutorise(m_Portique[index].trame); //verifie si le forfait est: secteur, passage, validitee,..
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
            if(this.m_BoxNumPortique.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un numéro de portique");
                return;
            }
            this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Port = int.Parse(this.m_TxtSerialPort.Text);
            this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Baud = int.Parse(this.m_TxtSerialBaud.Text);

            if (!this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee)
            {
                if (m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].PortName == "")
                {
                    MessageBox.Show("Veuillez choisir un port série");
                    return;
                }
                if (this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].BaudRate == 0)
                {
                    MessageBox.Show("Veuillez choisir une vitesse de transmission en bauds");
                    return;
                }
                try
                {
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].PortName = "COM" + this.m_TxtSerialPort.Text.ToString();
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].BaudRate = int.Parse(this.m_TxtSerialBaud.Text);
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Open();
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee = true;
                    this.m_BpConnecterSerialPort.Text = "Se Déconnecter";
                }
                catch (Exception x_exception)
                {
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee = false;
                    MessageBox.Show(x_exception.Message);
                }
            }
            else
            {
                try
                {
                    this.m_serialPortRfid[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Close();
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee = false;
                    this.m_BpConnecterSerialPort.Text = "Se Connecter";
                }
                catch (Exception x_exception)
                {
                    this.m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Connectee = true;
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
            Automate[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()) - 1].Evolue(TEvent.APPUIACQ, int.Parse(this.m_BoxNumPortique.SelectedItem.ToString()));
        }

        private void m_TxtNbrPortique_TextChanged(object sender, EventArgs e)
        {
            this.NbrPortique = int.Parse(this.m_TxtNbrPortique.Text.ToString());

            Portique = new CPortique[NbrPortique]; // tableau de portique
            Automate = new CAutomate[NbrPortique];
            m_serialPortRfid = new SerialPort[NbrPortique];
            m_Portique = new TPortique[NbrPortique]; //fenetre portique

            this.m_BoxNumPortique.Items.Clear();
            for (int i = NbrPortique; i > 0; i--)
            {
                this.m_BoxNumPortique.Items.Add(i);

                Portique[i - 1] = new CPortique(i);
                Automate[i - 1] = new CAutomate(this, i - 1);
                m_serialPortRfid[i - 1] = new SerialPort()
                {
                    DtrEnable = true,
                };
                m_Portique[i - 1] = new TPortique();
                m_serialPortRfid[i - 1].DataReceived += m_serialPortRfid_DataReceived;
            }
        }

        private void m_BoxNumPortique_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_TxtSerialPort.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Port).ToString());
            this.m_TxtSerialBaud.Text = ((m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Baud).ToString());
            this.m_BpConnecterSerialPort.Text = m_Portique[int.Parse(this.m_BoxNumPortique.SelectedItem.ToString())-1].Connectee ? "Se Déconnecter" : "Se Connecter";
        }
    }
}