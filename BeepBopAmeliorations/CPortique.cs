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
        private RichTextBox m_TxtBoxConsole; //Text Box pour faire la console du portique

        public CPortique(int x_index)
        {
            this.Text = "Portique " + (x_index).ToString(); //titre de la fenêtre du portique avec le numero de l'index du portique

            this.m_TxtBoxConsole = new System.Windows.Forms.RichTextBox(); //cree la text box
            this.m_TxtBoxConsole.Location = new System.Drawing.Point(0, 187); //location de la box
            this.m_TxtBoxConsole.Name = "richTextBox1"; 
            this.m_TxtBoxConsole.Size = new System.Drawing.Size(282, 68); //taille de la box
            this.m_TxtBoxConsole.TabIndex = 0;
            this.m_TxtBoxConsole.Text = ""; //init la box vide
            this.Controls.Add(this.m_TxtBoxConsole); //affiche la box
        }

        public void Ouvrir(int NumForfait)
        {
            if (this.InvokeRequired) //si besoin, invoque la methode sur le thread de l'interface suite aux erreur de threads
            {
                this.Invoke(new Action(() => Ouvrir(NumForfait))); //met en attente la methode sur le thread de l'interface
                return;
            }
            //passer le fond en vert
            try
            {
                this.Show(); //affiche la fenetre du portique
                this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Ouverture forfait :" + (NumForfait) + Environment.NewLine); //affiche dans la text box l'evenement
                this.BackColor = System.Drawing.Color.Green; //modifie la couleur du fond de la fenetre du portique en vert
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ouverture du portique : " + ex.Message); //si erreur
            }
        }

        public void Fermer()
        {
            if (this.InvokeRequired) //si besoin, invoque la methode sur le thread de l'interface
            {
                this.Invoke(new Action(() => Fermer())); //met en attente la methode sur le thread de l'interface
                return;
            }
            //passer le fond en orange
            try
            {
                this.Show(); //affiche la fenetre du portique
                this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Fermeture" + Environment.NewLine); //affiche dans la text box l'evenement
                this.BackColor = System.Drawing.Color.Orange; //modifie la couleur du fond de la fenetre du portique en orange
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture du portique : " + ex.Message);
            }
        }

        public void Erreur(int NumForfait)
        {
            if (this.InvokeRequired) //si besoin, invoque la methode sur le thread de l'interface
            {
                this.Invoke(new Action(() => Erreur(NumForfait))); //met en attente la methode sur le thread de l'interface
                return;
            }
            //passer le fond en rouge
            try
            {
                this.Show(); //affiche la fenetre du portique
                this.m_TxtBoxConsole.AppendText("[" + (DateTime.Now) + "]" + "Erreur forfait :" + (NumForfait) + Environment.NewLine); //affiche dans la text box l'evenement
                this.BackColor = System.Drawing.Color.Red; //modifie la couleur du fond de la fenetre du portique en rouge
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'erreur du portique : " + ex.Message);
            }
        }
    }
}
