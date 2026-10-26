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
        public void Ouvir()
        {
            //passer le fond en vert
            this.BackColor = System.Drawing.Color.Green;
        }

        public void Fermer()
        {
            //passer le fond en orange
            this.BackColor = System.Drawing.Color.Orange;
        }

        public void Erreur()
        {
            //passer le fond en rouge
            this.BackColor = System.Drawing.Color.Red;
        }
    }
}
