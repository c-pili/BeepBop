using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    public class CPortique:Form
    {
        private RichTextBox m_TxtBoxConsole;

        public CPortique(int x_index)
        {
            this.Text = "Portique " + (x_index).ToString();

            this.m_TxtBoxConsole = new System.Windows.Forms.RichTextBox();
            this.m_TxtBoxConsole.Location = new System.Drawing.Point(0, 187);
            this.m_TxtBoxConsole.Name = "richTextBox1";
            this.m_TxtBoxConsole.Size = new System.Drawing.Size(282, 68);
            this.m_TxtBoxConsole.TabIndex = 0;
            this.m_TxtBoxConsole.Text = "";
            this.Controls.Add(this.m_TxtBoxConsole);
        }

        public void Ouvrir(int NumForfait)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Ouvrir(NumForfait)));
                return;
            }
            //passer le fond en vert
            try
            {
                this.Show();
                this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Ouverture forfait :" + (NumForfait) + Environment.NewLine);
                this.BackColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ouverture du portique : " + ex.Message);
            }
        }

        public void Fermer()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Fermer()));
                return;
            }
            //passer le fond en orange
            try
            {
                this.Show();
                this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Fermeture" + Environment.NewLine);
                this.BackColor = System.Drawing.Color.Orange;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture du portique : " + ex.Message);
            }
        }

        public void Erreur(int NumForfait)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Erreur(NumForfait)));
                return;
            }
            //passer le fond en rouge
            try
            {
                this.Show();
                this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Erreur forfait :" + (NumForfait) + Environment.NewLine);
                this.BackColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'erreur du portique : " + ex.Message);
            }
        }
    }
}
