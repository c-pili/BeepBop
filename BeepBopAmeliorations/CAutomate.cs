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
        CRfid BeepBop;
        int index;
        int NumForfait;
        private System.Timers.Timer m_Timer;

        public CAutomate(int x_DureeOuverture, int x_index)
        {
            this.index = x_index;

            this.m_Timer = new System.Timers.Timer();
            this.m_Timer.Interval = (x_DureeOuverture * 1000);
            this.m_Timer.AutoReset = false;
            this.m_Timer.Elapsed += M_Timer_Elapsed;
        }

        public void Evolue(TEvent x_Event, int x_numforfait)
        {
            this.NumForfait = x_numforfait;
            switch (this.m_etat)
            {
                case TEtat.FERMEE:
                    if(x_Event == TEvent.AUTORISEE)
                    {
                        this.m_etat = TEtat.OUVERT;
                    }
                    else if(x_Event == TEvent.NON_AUTORISEE)
                    {
                        this.m_etat = TEtat.ERREUR;
                    }
                    break;

                case TEtat.OUVERT:
                    if (x_Event == TEvent.TIMER2S) 
                    {
                        this.m_etat = TEtat.FERMEE;
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
                    BeepBop.FermerPortique(index);
                    break;

                case TEtat.OUVERT:
                    BeepBop.OuvrirPortique(index, NumForfait);
                    break;

                case TEtat.ERREUR:
                    BeepBop.ErreurPortique(index, NumForfait);
                    break;
            }
        }
        private void M_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Evolue(TEvent.TIMER2S, 0);
            this.m_Timer.Stop();
        }
    }
}
