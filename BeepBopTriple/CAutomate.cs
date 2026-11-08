using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepBop
{
    enum TEtat { INIT = 0, FERMEE, OUVERT, ERREUR}
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
        public CAutomate(CRfid x_formMain, int x_index)
        {
            this.Rfid = x_formMain;
            this.index = x_index;
        }

        public void Evolue(TEvent x_Event, int x_numForfait)
        {
            this.NumForfait = x_numForfait;
            switch (this.m_etat)
            {
                case TEtat.INIT:
                    break;
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
                    else if (x_Event == TEvent.NON_AUTORISEE)
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
                    Rfid.FermerPortique(index);
                    break;

                case TEtat.OUVERT:
                    Rfid.OuvrirPortique(index);
                    break;

                case TEtat.ERREUR:
                    Rfid.ErreurPortique(index);
                    break;
            }
        }
    }
}
