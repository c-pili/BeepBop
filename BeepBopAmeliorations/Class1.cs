using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepBop
{
    public struct Forfait
    {
        public int m_NumeroCarte;
        public bool m_estAutorise;
        public DateTime m_DernierPassage;
        public DateTime DureeValidite;
    }
    public class CBaseDeDonnees
    {
        private Forfait[] m_forfaits;
        private const int N_FORFAITS = 6;
        private float temp_passage;
        public CBaseDeDonnees(float x_temp_passage)
        {
            m_forfaits = new Forfait[N_FORFAITS];

            this.m_forfaits[0].m_NumeroCarte = 24393;
            this.m_forfaits[0].m_estAutorise = true;
            this.m_forfaits[0].DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[1].m_NumeroCarte = 22033;
            this.m_forfaits[1].m_estAutorise = true;
            this.m_forfaits[1].DureeValidite = DateTime.Now.AddDays(1);

            this.m_forfaits[2].m_NumeroCarte = 19599;
            this.m_forfaits[2].m_estAutorise = false;
            this.m_forfaits[2].DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[3].m_NumeroCarte = 18424;
            this.m_forfaits[3].m_estAutorise = true;
            this.m_forfaits[3].DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[4].m_NumeroCarte = 25132;
            this.m_forfaits[4].m_estAutorise = true;
            this.m_forfaits[4].DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[5].m_NumeroCarte = 19140;
            this.m_forfaits[5].m_estAutorise = false;
            this.m_forfaits[5].DureeValidite = DateTime.Now.AddDays(30);

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
                        if(this.m_forfaits[i].DureeValidite > DateTime.Now)
                        {
                            m_forfaits[i].m_DernierPassage = DateTime.Now;
                            return m_forfaits[i].m_estAutorise;
                        }
                    }
                }

            }
            return false;
        }
    }
}
