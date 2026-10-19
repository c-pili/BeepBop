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
    }
    internal class CBaseDeDonnees
    {
        private Forfait[] m_forfaits;
        private const int N_FORFAITS = 5;
        CBaseDeDonnees()
        {
            m_forfaits = new Forfait[N_FORFAITS];
            this.m_forfaits[0].m_NumeroCarte = 24393;
            this.m_forfaits[0].m_estAutorise = true;
            this.m_forfaits[1].m_NumeroCarte = 22033;
            this.m_forfaits[1].m_estAutorise = true;
            this.m_forfaits[2].m_NumeroCarte = 19599;
            this.m_forfaits[2].m_estAutorise = false;
            this.m_forfaits[3].m_NumeroCarte = 18424;
            this.m_forfaits[3].m_estAutorise = true;
            this.m_forfaits[4].m_NumeroCarte = 25132;
            this.m_forfaits[4].m_estAutorise = true;
            this.m_forfaits[5].m_NumeroCarte = 19140;
            this.m_forfaits[5].m_estAutorise = false;
        }
        public bool EstAutorise(int x_numeroCarte)
        {
            for (int i = 0; i < N_FORFAITS; i++)
            {
                if (m_forfaits[i].m_NumeroCarte == x_numeroCarte)
                {
                    return m_forfaits[i].m_estAutorise;
                }

            }
            return false;
        }
    }
}
