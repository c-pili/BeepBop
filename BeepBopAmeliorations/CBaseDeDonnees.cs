using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BeepBop
{
    public struct Forfait //structure qui contient les informations d'un forfait
    {
        public int m_NumeroCarte; //Num forfait
        public bool m_estAutorise; //true si autorisee
        public TSecteur m_secteur; //secteur forfait
        public DateTime m_DernierPassage; //date du dernier passage type DateTime
        public DateTime m_DureeValidite; //date de validité du forfait type DateTime
    }

    public enum TSecteur { CHATEL = 0, AVORIAZ, PORTESDUSOLEIL}; //enumération qui contient les différents secteurs possibles pour un forfait ou un portique
    public class CBaseDeDonnees
    {
        public Forfait[] m_forfaits;
        TSecteur Secteur;
        public TSecteur[] SecteurPortique; //tableau qui contient les secteurs des portiques
        public const int N_FORFAITS_MAX = 15; //constante fixe du nombre de forfaits dans la base de données (faut realloc en c# ?) nombre max de forfait qu'on peut cree
        public int NbrForfaitUtilisee = 6; //nombre de forfait reelement utilisee
        private float temp_passage; //tempe de passage d'ouverture du portique (en secondes) entre 2 passages d'un même forfait
        public CBaseDeDonnees(float x_temp_passage)
        {
            m_forfaits = new Forfait[N_FORFAITS_MAX];

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
            this.m_forfaits[4].m_secteur = TSecteur.PORTESDUSOLEIL;
            this.m_forfaits[4].m_DureeValidite = DateTime.Now.AddDays(30);

            this.m_forfaits[5].m_NumeroCarte = 19140;
            this.m_forfaits[5].m_estAutorise = false;
            this.m_forfaits[5].m_secteur = TSecteur.AVORIAZ;
            this.m_forfaits[5].m_DureeValidite = DateTime.Now.AddDays(30);

            this.temp_passage = x_temp_passage;
        }
        public bool EstAutorise(int x_numeroCarte, int x_indexPortique) //Verification si le forfait est autorisee, conditions détaillées
        {
            for (int i = 0; i < NbrForfaitUtilisee; i++) //regarde tout les forfaits utilisés
            {
                if (m_forfaits[i].m_NumeroCarte == x_numeroCarte) //si le numéro de carte est dans la base de données
                {
                    if(m_forfaits[i].m_DernierPassage.AddSeconds(temp_passage) < DateTime.Now) //si le dernier passage est plus vieux que le temps minimum entre 2 passages
                    {
                        if(this.m_forfaits[i].m_DureeValidite > DateTime.Now) //si le forfait est encore valide (duree d'un forfait)
                        {
                            if (VerifSecteur(m_forfaits[i].m_secteur, this.SecteurPortique[x_indexPortique-1])) //si le secteur du forfait est compatible avec le secteur du portique
                            {
                                if(m_forfaits[i].m_estAutorise) //forfait autorisée
                                {
                                    m_forfaits[i].m_DernierPassage = DateTime.Now; //met a jour le dernier passage du forfait
                                    return true; //autorisee
                                }
                            }
                        }
                    }
                }

            }
            return false; //pas autorisée
        }

        public int IndexForfait(int NumForfait) //Permet de connaitre l'index du forfait dans le tableau de forfaits à partir du numéro de carte du forfait
        {
            for(int i = 0; i < N_FORFAITS_MAX; i++)
            {
                if(NumForfait == this.m_forfaits[i].m_NumeroCarte)
                {
                    return i; //retourne l'index du forfait
                }
            }
            return 0;
        }

        public void ChangerAutorisation(int NumForfait, bool Autorisee) //Permet de changer l'autorisation d'un forfait à partir du numéro de carte du forfait
        {
            int index = IndexForfait(NumForfait); //cherche l'index du forfait dans le tableau de forfaits
            this.m_forfaits[index].m_estAutorise = Autorisee; //autorise ou non le forfait
        }

        public void ChangerValiditee(int NumForfait, string JourForfait) //Permet de changer la validite d'un forfait à partir du numéro de carte du forfait et du nombre de jours de validité
        {
            int index = IndexForfait(NumForfait); //cherche l'index du forfait dans le tableau de forfaits
            int NumJour = 0;
            switch (JourForfait)
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
            this.m_forfaits[index].m_DureeValidite = DateTime.Now.AddDays(NumJour); //met a jour la validité du forfait avec le nombre de jours choisi
        }

        public void ChangerSecteur(int NumForfait, string secteur) //Permet de changer le secteur d'un forfait à partir du numéro de carte du forfait et du secteur choisi
        {
            int index = IndexForfait(NumForfait); //cherche l'index du forfait dans le tableau de forfaits
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
            this.m_forfaits[index].m_secteur = (TSecteur)NumSecteur; //Change le secteur du forfait avec le secteur choisi
        }

        public void SupprimerForfait(int NumForfait) //Supprime un forfait de la base de données à partir du numéro de carte du forfait
        {
            int index = IndexForfait(NumForfait); //cherche l'index du forfait dans le tableau de forfaits
            this.m_forfaits[index].m_NumeroCarte = 0; //met le numéro de carte du forfait à 0 pour le supprimer
        }
        public void AjouterForfait(int NumForfait) //Ajoute un forfait à la base de données à partir du numéro de carte du forfait
        {
            if(this.NbrForfaitUtilisee >= N_FORFAITS_MAX) //verifie si le nombre maximum de forfaits est atteint
            {
                throw new Exception("Nombre maximum de forfaits atteint");
            }
            this.m_forfaits[NbrForfaitUtilisee].m_NumeroCarte = NumForfait; //ajoute le numéro de carte du forfait à la base de données
            this.NbrForfaitUtilisee++; //incrémente le nombre de forfaits utilisés
        }

        public void CreePortique(int NbrPortique) //Cree un tableau de portiques avec le nombre de portiques choisi
        {
            SecteurPortique = new TSecteur[NbrPortique];
        }

        public void ModifierSecteur(int x_indexPortique, string x_secteur) //Permet de changer le secteur d'un portique à partir de l'index du portique et du secteur choisi
        {
            int NumSecteur = 0;
            switch (x_secteur)
            {
                case "CHATEL":
                    NumSecteur = 0;
                    break;
                case "AVORIAZ":
                    NumSecteur = 1;
                    break;
            }
            this.SecteurPortique[x_indexPortique - 1] = (TSecteur)NumSecteur;
        }

        private bool VerifSecteur(TSecteur x_secteurForfait, TSecteur x_secteurPortique) //Permet de vérifier si le secteur du forfait est compatible avec le secteur du portique
        {
            if((x_secteurForfait == x_secteurPortique) || (x_secteurForfait == TSecteur.PORTESDUSOLEIL && x_secteurPortique == TSecteur.CHATEL)
                || (x_secteurForfait == TSecteur.PORTESDUSOLEIL && x_secteurPortique == TSecteur.AVORIAZ))
            {
                return true; //vrai si le portique et le forfait sont dans le même secteur ou si le forfait est de secteur PORTESDUSOLEIL
            }
            else
            {
                return false; //faux sinon
            }
        }
    }
}
