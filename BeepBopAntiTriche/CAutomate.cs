using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepBop
{
    enum TEtat { FERMEE = 0, OUVERT, ERREUR}
    enum TEvent {AUTORISEE, NON_AUTORISEE, TIMER2S, APPUIACQ }
    internal class CAutomate
    {
        TEtat m_etat;
        CRfid Rfid;
        private System.Timers.Timer m_Timer;
        /*
        Graph Etat
        etat init = fermee
        autorisee = ouvert 2s
        non auto = erreur 2s -> durant 2s on peut acquitter avec un bouton
        */

        public CAutomate(CRfid x_FormMain)
        {
            this.Rfid = x_FormMain;

            this.m_Timer = new System.Timers.Timer();
            this.m_Timer.Interval = 2000;
            this.m_Timer.AutoReset = false;
            this.m_Timer.Elapsed += M_Timer_Elapsed;
        }

        public void Evolue(TEvent x_Event)
        {
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
                    Rfid.FermerPortique();
                    break;

                case TEtat.OUVERT:
                    Rfid.OuvrirPortique();
                    break;

                case TEtat.ERREUR:
                    Rfid.ErreurPortique();
                    break;
            }
        }

        private void M_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Evolue(TEvent.TIMER2S);
            this.m_Timer.Stop();
        }
    }
}
