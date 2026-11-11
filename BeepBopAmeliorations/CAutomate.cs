using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    enum TEtat { FERMEE = 0, OUVERT, ERREUR}
    enum TEvent {AUTORISEE, NON_AUTORISEE, TIMER2S, APPUIACQ }
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
        int index;
        int NumForfait;
        private System.Windows.Forms.Timer m_Timer;

        public CAutomate(CRfid x_formMain, int x_DureeOuverture, int x_index)
        {
            this.index = x_index - 1;
            this.Rfid = x_formMain;

            this.m_Timer = new System.Windows.Forms.Timer();
            this.m_Timer.Interval = (x_DureeOuverture * 1000);
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
                        this.m_Timer.Start();
                        this.m_etat = TEtat.OUVERT;
                    }
                    else if(x_Event == TEvent.NON_AUTORISEE)
                    {
                        this.m_Timer.Start();
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
            this.Actions();
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
            this.m_Timer.Stop();
            this.Evolue(TEvent.TIMER2S, 0);
        }
    }
}
