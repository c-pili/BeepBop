using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    internal class CPortique:Form
    {
        private CheckBox checkBox1;
        private ListBox listBox1;
        private RichTextBox m_TxtBoxConsole;

        public CPortique()
        {
            this.m_TxtBoxConsole = new System.Windows.Forms.RichTextBox();

            this.m_TxtBoxConsole.Location = new System.Drawing.Point(0, 187);
            this.m_TxtBoxConsole.Name = "richTextBox1";
            this.m_TxtBoxConsole.Size = new System.Drawing.Size(282, 68);
            this.m_TxtBoxConsole.TabIndex = 0;
            this.m_TxtBoxConsole.Text = "";

            this.Controls.Add(this.m_TxtBoxConsole);
        }

        public void Ouvrir(int index, int NumForfait)
        {
            //passer le fond en vert
            this.Text = "Portique " + (index + 1).ToString();
            this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Ouverture forfait :" + (NumForfait) + Environment.NewLine);
            this.BackColor = System.Drawing.Color.Green;
        }

        public void Fermer(int index)
        {
            //passer le fond en orange
            this.Text = "Portique " + (index + 1).ToString();
            this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Fermeture" + Environment.NewLine);
            this.BackColor = System.Drawing.Color.Orange;
        }

        public void Erreur(int index, int NumForfait)
        {
            //passer le fond en rouge
            this.Text = "Portique " + (index + 1).ToString();
            this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Erreur forfait :" + (NumForfait) + Environment.NewLine);
            this.BackColor = System.Drawing.Color.Red;
        }
    }
}
