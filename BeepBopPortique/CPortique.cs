using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    public class CPortique:Form
    {
        public CPortique()
        {
            this.Text = "Portique";
            this.BackColor = System.Drawing.Color.Orange;
            this.Show();
        }
        public void Ouvrir()
        {
            //passer le fond en vert
            this.Show();
            this.BackColor = System.Drawing.Color.Green;
        }

        public void Fermer()
        {
            //passer le fond en orange
            this.Show();
            this.BackColor = System.Drawing.Color.Orange;
        }

        public void Erreur()
        {
            //passer le fond en rouge
            this.Show();
            this.BackColor = System.Drawing.Color.Red;
        }
    }
}
