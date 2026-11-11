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
        public CPortique(int x_index)
        {
            this.Text = "Portique" + (x_index = 1).ToString();
        }
        public void Ouvrir()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Ouvrir()));
                return;
            }
            //passer le fond en vert
            this.Show();
            this.BackColor = System.Drawing.Color.Green;
        }

        public void Fermer()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Fermer()));
                return;
            }
            //passer le fond en orange
            this.Show();
            this.BackColor = System.Drawing.Color.Orange;
        }

        public void Erreur()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Erreur()));
                return;
            }
            //passer le fond en rouge
            this.Show();
            this.BackColor = System.Drawing.Color.Red;
        }
    }
}
