using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BeepBop
{
    public struct Forfait
    {
        public int m_NumeroCarte;
        public bool m_estAutorise;
        public DateTime m_DernierPassage;
        public DateTime m_DureeValidite;
    }
    public class CBaseDeDonnees
    {
        public Forfait[] m_forfaits;
        public int N_FORFAITS = 6;
        private float temp_passage;
        public CBaseDeDonnees(float x_temp_passage)
        {
            m_forfaits = new Forfait[N_FORFAITS];

            this.m_forfaits[0].m_NumeroCarte = 24393;
            this.m_forfaits[0].m_estAutorise = true;
            this.m_forfaits[0].m_DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[1].m_NumeroCarte = 22033;
            this.m_forfaits[1].m_estAutorise = true;
            this.m_forfaits[1].m_DureeValidite = DateTime.Now.AddDays(1);

            this.m_forfaits[2].m_NumeroCarte = 19599;
            this.m_forfaits[2].m_estAutorise = false;
            this.m_forfaits[2].m_DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[3].m_NumeroCarte = 18424;
            this.m_forfaits[3].m_estAutorise = true;
            this.m_forfaits[3].m_DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[4].m_NumeroCarte = 25132;
            this.m_forfaits[4].m_estAutorise = true;
            this.m_forfaits[4].m_DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[5].m_NumeroCarte = 19140;
            this.m_forfaits[5].m_estAutorise = false;
            this.m_forfaits[5].m_DureeValidite = DateTime.Now.AddDays(30);

            this.temp_passage = x_temp_passage;
        }
        public bool EstAutorise(int x_numeroCarte)
        {
            for (int i = 0; i < N_FORFAITS; i++)
            {
                if (m_forfaits[i].m_NumeroCarte == x_numeroCarte)
                {
                    if(m_forfaits[i].m_DernierPassage.AddSeconds(temp_passage) < DateTime.Now)
                    {
                        if(this.m_forfaits[i].m_DureeValidite > DateTime.Now)
                        {
                            m_forfaits[i].m_DernierPassage = DateTime.Now;
                            return m_forfaits[i].m_estAutorise;
                        }
                    }
                }

            }
            return false;
        }

        public int IndexForfait(int NumForfait)
        {
            for(int i = 0; i < N_FORFAITS; i++)
            {
                if(NumForfait == this.m_forfaits[i].m_NumeroCarte)
                {
                    return i;
                }
            }
            return 0;
        }

        public void ChangerAutorisation(int NumForfait, bool Autorisee)
        {
            int index = IndexForfait(NumForfait);
            this.m_forfaits[index].m_estAutorise = Autorisee;
        }

        public void ChangerValiditee(int NumForfait, string j)
        {
            int index = IndexForfait(NumForfait);
            int NumJour = 0;
            switch (j)
            {
                case "1j":
                    NumJour = 1;
                    break;

                case "5j":
                    NumJour = 5;
                    break;

                case "7j":
                    NumJour = 7;
                    break;

                case "14j":
                    NumJour = 14;
                    break;
            }
            this.m_forfaits[index].m_DureeValidite = DateTime.Now.AddDays(NumJour);
        }

        public void SupprimerForfait(int NumForfait)
        {
            int index = IndexForfait(NumForfait);
            this.m_forfaits[index].m_NumeroCarte = 0;
        }
        public void AjouterForfait(int Num)
        {   
            this.m_forfaits
        }
    }
}
