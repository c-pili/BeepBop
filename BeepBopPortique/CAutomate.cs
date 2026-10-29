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
        /*
        Graph Etat
        etat init = fermee
        autorisee = ouvert 2s
        non auto = erreur 2s -> durant 2s on peut acquitter avec un bouton
        */
        TEtat m_etat;
        CBeepBop BeepBop;

        public void Evolue(TEvent x_Event)
        {
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
                    BeepBop.FermerPortique();
                    break;

                case TEtat.OUVERT:
                    BeepBop.OuvrirPortique();
                    break;

                case TEtat.ERREUR:
                    BeepBop.ErreurPortique();
                    break;
            }
        }
    }
}
