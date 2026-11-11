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
    public partial class CSimulateurRfid : Form
    {
        public event EventHandler BadgeScannee; //evement pour simuler le badge scanné d'un lecteur
        public int NumeroCarte; //numero de carte scannée par le simulateur
        public CSimulateurRfid(int index)
        {
            InitializeComponent();
            this.Show(); //affiche la fenetre du simulateur du portique
            this.Text = "Simulateur Portique " + (index); //titre de la fenetre avec le num de l'index du portique

            this.m_TxtBoxNumCarte.Text = "Veuillez choisir un Numéro";
        }

        private void m_BpAjouterNumCarte_Click(object sender, EventArgs e) //si ajout d'un nouveau numero de carte, ajoute le dans la liste des numeros de cartes
        {
            if(this.m_TxtAjouterNumCarte.Text == "")
            {
                MessageBox.Show("Veuillez ajouter le numéro de carte");
                return;
            }
            this.m_TxtBoxNumCarte.Items.Add(this.m_TxtAjouterNumCarte.Text);
            this.m_TxtAjouterNumCarte.Text = "";
        }

        private void m_BpBadger_Click(object sender, EventArgs e) //envoie l'evenement de badge scanné avec le numero de carte selectionné
        {
            if(m_TxtBoxNumCarte.SelectedItem == null)
            {
                MessageBox.Show("Veuillez Séléctionner un numéro de carte");
                return;
            }
            this.NumeroCarte = Int32.Parse(m_TxtBoxNumCarte.SelectedItem.ToString());  //conditionnement du numero de badge comme celui d'un badge scanné par un lecteur

            this.BadgeScannee(this, EventArgs.Empty); //evenement badge scannée
        }
    }
}
