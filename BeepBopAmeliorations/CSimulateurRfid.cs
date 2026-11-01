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
        public event EventHandler BadgeScannee;
        public int NumeroCarte;
        public CSimulateurRfid(int index)
        {
            InitializeComponent();
            this.Show();
            this.Text = "Simulateur Portique " + (index);

            this.m_TxtBoxNumCarte.Text = "Veuillez choisir un Numéro";
        }

        private void m_BpAjouterNumCarte_Click(object sender, EventArgs e)
        {
            if(this.m_TxtAjouterNumCarte.Text == "")
            {
                MessageBox.Show("Veuillez ajouter le numéro de carte");
                return;
            }
            this.m_TxtBoxNumCarte.Items.Add(this.m_TxtAjouterNumCarte.Text);
            this.m_TxtAjouterNumCarte.Text = "";
        }

        private void m_BpBadger_Click(object sender, EventArgs e)
        {
            if(m_TxtBoxNumCarte.Text == null)
            {
                MessageBox.Show("Veuillez Séléctionner un numéro de carte");
                return;
            }
            this.NumeroCarte = Int32.Parse(m_TxtBoxNumCarte.Text);

            this.BadgeScannee(this, EventArgs.Empty);
        }
    }
}
