using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepBop
{
    enum TEtat { FERMEE = 0, OUVERT, ERREUR}
    enum TEvent {FERMER = 0, OUVERT, ERREUR, ACQUITTER }
    internal class CAutomate
    {
        /*
        Graph Etat
        etat init = fermee
        autorisee = ouvert 2s
        non auto = erreur 2s -> durant 2s on peut acquitter avec un bouton
        */
        private void Evolue(TEvent x_Event)
        {
            switch (this.m_event)
            {
                case TEtat.FERMEE:
                    break;

                case TEtat.OUVERT:
                    break;

                case TEtat.ERREUR:
                    break;
            }
        }
    }
}
