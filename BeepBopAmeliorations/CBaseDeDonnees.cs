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
        public TSecteur m_secteur;
        public DateTime m_DernierPassage;
        public DateTime m_DureeValidite;
    }

    public enum TSecteur { CHATEL = 0, AVORIAZ, PORTESDUSOLEIL};
    public class CBaseDeDonnees
    {
        public Forfait[] m_forfaits;
        TSecteur Secteur;
        public TSecteur[] SecteurPortique;
        public const int N_FORFAITS = 15;
        public int NbrForfaitUtilisee = 6;
        private float temp_passage;
        public CBaseDeDonnees(float x_temp_passage)
        {
            m_forfaits = new Forfait[N_FORFAITS];

            this.m_forfaits[0].m_NumeroCarte = 24393;
            this.m_forfaits[0].m_estAutorise = true;
            this.m_forfaits[0].m_secteur = TSecteur.AVORIAZ;
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
        public bool EstAutorise(int x_numeroCarte, int x_indexPortique)
        {
            for (int i = 0; i < NbrForfaitUtilisee; i++)
            {
                if (m_forfaits[i].m_NumeroCarte == x_numeroCarte)
                {
                    if(m_forfaits[i].m_DernierPassage.AddSeconds(temp_passage) < DateTime.Now)
                    {
                        if(this.m_forfaits[i].m_DureeValidite > DateTime.Now)
                        {
                            if (this.m_forfaits[i].m_secteur == this.SecteurPortique[x_indexPortique-1])
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

        public void ChangerSecteur(int NumForfait, string secteur)
        {
            int index = IndexForfait(NumForfait);
            int NumSecteur = 0;
            switch (secteur)
            {
                case "Chatel":
                    NumSecteur = 0;
                    break;

                case "Avoriaz":
                    NumSecteur = 1;
                    break;

                case "Portes du Soleil":
                    NumSecteur = 2;
                    break;
            }
            this.m_forfaits[index].m_secteur = (TSecteur)NumSecteur;
        }

        public void SupprimerForfait(int NumForfait)
        {
            int index = IndexForfait(NumForfait);
            this.m_forfaits[index].m_NumeroCarte = 0;
        }
        public void AjouterForfait(int NumForfait)
        {
            this.m_forfaits[NbrForfaitUtilisee].m_NumeroCarte = NumForfait;
            this.NbrForfaitUtilisee++;
        }

        public void CreePortique(int NbrPortique)
        {
            SecteurPortique = new TSecteur[NbrPortique];
        }

        public void ModifierSecteur(int x_indexPortique, string x_secteur)
        {
            int NumSecteur = 0;
            switch (x_secteur)
            {
                case "Chatel":
                    NumSecteur = 0;
                    break;
                case "Avoriaz":
                    NumSecteur = 1;
                    break;
                case "Portes du Soleil":
                    NumSecteur = 2;
                    break;
            }
            this.SecteurPortique[x_indexPortique - 1] = (TSecteur)NumSecteur;
        }
    }
}
