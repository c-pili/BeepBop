using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    enum TEtat { FERMEE = 0, OUVERT, ERREUR } //enumération qui contient les différents états possibles pour un portique
    enum TEvent {AUTORISEE, NON_AUTORISEE, TIMER2S, APPUIACQ } //enumération qui contient les différents événements possibles pour un portique
    internal class CAutomate
    {
        /*
        Graph Etat
        etat init = fermee
        autorisee = ouvert 2s
        non auto = erreur 2s -> durant 2s on peut acquitter avec un bouton
        */
        TEtat m_etat;
        CRfid Rfid;
        int index; //index portique
        int NumForfait; //num du forfait qui passe dans le portique
        private System.Windows.Forms.Timer m_Timer; //Timer duree ouverture portique

        public CAutomate(CRfid x_formMain, int x_DureeOuverture, int x_index)
        {
            this.index = x_index - 1;
            this.Rfid = x_formMain;

            this.m_Timer = new System.Windows.Forms.Timer();
            this.m_Timer.Interval = (x_DureeOuverture * 1000); //converti en millisecondes la duree du timer
            this.m_Timer.Tick += M_Timer_Tick;
        }

        public void Evolue(TEvent x_Event, int x_numforfait)
        {
            this.NumForfait = x_numforfait;
            switch (this.m_etat)
            {
                case TEtat.FERMEE:
                    if(x_Event == TEvent.AUTORISEE)
                    {
                        this.m_Timer.Start(); //démarre le timer de 2 secondes pour la fermeture
                        this.m_etat = TEtat.OUVERT;
                    }
                    else if(x_Event == TEvent.NON_AUTORISEE)
                    {
                        this.m_Timer.Start(); //démarre le timer de 2 secondes pour l'acquitement de l'erreur
                        this.m_etat = TEtat.ERREUR;
                    }
                    break;

                case TEtat.OUVERT:
                    if (x_Event == TEvent.TIMER2S) 
                    {
                        this.m_etat = TEtat.FERMEE;
                    }
                    if (x_Event == TEvent.NON_AUTORISEE)
                    {
                        this.m_etat = TEtat.ERREUR;
                    }
                    break;

                case TEtat.ERREUR:
                    if ((x_Event == TEvent.APPUIACQ) || (x_Event == TEvent.TIMER2S)) // appui acquit + timer 2s
                    {
                        this.m_etat = TEtat.FERMEE;
                    }
                    break;
            }
            this.Actions(); //met a jour les actions 
        }

        public void Actions()
        {
            switch (this.m_etat)
            {
                case TEtat.FERMEE:
                    Rfid.Portique[index].Fermer();
                    break;

                case TEtat.OUVERT:
                    Rfid.Portique[index].Ouvrir(NumForfait);
                    break;

                case TEtat.ERREUR:
                    Rfid.Portique[index].Erreur(NumForfait);
                    break;
            }
        }
        private void M_Timer_Tick(object sender, EventArgs e)
        {
            this.m_Timer.Stop(); //stop le timer jusqu'à la prochaine ouverture du portique
            this.Evolue(TEvent.TIMER2S, 0); //fait evoluer le graph etat
        }
    }
}
