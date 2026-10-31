using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    internal class CPortique:Form
    {
        public void Ouvrir(int index)
        {
            //passer le fond en vert
            this.Text = "Portique" + (index).ToString();
            this.BackColor = System.Drawing.Color.Green;
        }

        public void Fermer(int index)
        {
            //passer le fond en orange
            this.Text = "Portique" + (index).ToString();
            this.BackColor = System.Drawing.Color.Orange;
        }

        public void Erreur(int index)
        {
            //passer le fond en rouge
            this.Text = "Portique" + (index).ToString();
            this.BackColor = System.Drawing.Color.Red;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CPortique
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "CPortique";
            this.ResumeLayout(false);

        }
    }
}
