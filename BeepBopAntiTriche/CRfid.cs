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
    public partial class CRfid : Form
    {
        CAutomate Automate;
        CBaseDeDonnees BaseDeDonnees;
        CPortique Portique;
        private DateTime DateDernierPassage;
        private readonly int TempDetection = 400; //en millisecondes
        public CRfid()
        {
            InitializeComponent();
            BaseDeDonnees = new CBaseDeDonnees();
            Automate = new CAutomate(this);
            Portique = new CPortique();
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

        private void m_serialPortRfid_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (this.DateDernierPassage.AddMilliseconds(this.TempDetection) > DateTime.Now) //si le dernier passage est trop proche (spam console portique)
            {
                return;
            }
            this.DateDernierPassage = DateTime.Now;
            this.Invoke(new Action(() => //permet de faire appel à l'interface graphique depuis un thread
            {
                try
                {
                    int NumCarte = LectureNumeroCarte();
                    m_lblNumeroCarte.Text = NumCarte.ToString();

                    if (BaseDeDonnees.EstAutorise(NumCarte))
                    {
                        label3.BackColor = Color.LightGreen;
                        Automate.Evolue(TEvent.AUTORISEE);
                    }
                    else
                    {
                        label3.BackColor = Color.LightCoral;
                        Automate.Evolue(TEvent.NON_AUTORISEE);
                    }
                }
                catch (Exception x_exception)
                {
                    MessageBox.Show(x_exception.Message);
                }
            }));
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

        private void m_BpAcquittement_Click(object sender, EventArgs e)
        {
            Automate.Evolue(TEvent.APPUIACQ);
        }

        public void FermerPortique()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => FermerPortique())); //permet de faire appel à l'interface graphique depuis un thread
                return;
            }
            Portique.Fermer();
            Portique.Show();
        }

        public void OuvrirPortique()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OuvrirPortique())); //permet de faire appel à l'interface graphique depuis un thread
                return;
            }
                Portique.Ouvrir();
            Portique.Show();
        }
        public void ErreurPortique()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OuvrirPortique())); //permet de faire appel à l'interface graphique depuis un thread
                return;
            }
            Portique.Erreur();
            Portique.Show();
        }
    }
}