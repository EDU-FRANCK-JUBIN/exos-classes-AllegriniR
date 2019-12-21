using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exos_classes
{
    class CompteBancaire
    {
        string titulaire, devise;
        int solde;

        public static int NombreDeCompte = 0;

        public string Titulaire { get => titulaire; set => titulaire = value; }
        public string Devise { get => devise; set => devise = value; }
        public int Solde { get => solde; set => solde = value; }

        public CompteBancaire(string titulaire, string devise, int solde)
        {
            this.titulaire = titulaire;
            this.devise = devise;
            this.solde = solde;
            // Incrémenté seulement si toutes les valeurs précédantes ont été bien affectées
            NombreDeCompte++;
        }

        public void Crediter(int valeur)
        {
            this.solde += valeur;
        }

        public void Debiter(int valeur)
        {
            this.solde -= valeur;
        }

        public string Decrire()
        {
            return "Le titulaire est " + this.titulaire + " et le solde est de " + this.solde + "" + this.devise;
        }


    }

    class Client
    {
        string cin, nom, prenom, tel;

        public string Cin { get => cin; set => cin = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Tel { get => tel; set => tel = value; }

        public Client(string cin, string nom, string prenom, string tel)
        {
            this.cin = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.tel = tel;
        }

        public Client(string cin, string nom, string prenom)
        {
            this.cin = cin;
            this.nom = nom;
            this.prenom = prenom;
        }

        public void Afficher()
        {
            string str = "Le client s'appelle " + this.prenom + " " + this.nom + ". Son CIN est " + this.cin + ".";
            if (this.tel != null)
            {
                str += " Son numéro de téléphone est " + this.Tel + ".";
            }
            Console.WriteLine(str);
        }
    }

    class Compte
    {
        Client proprietaire;
        readonly int code;
        int solde = 0;

        static int CodeCourant = 0;

        public Client Proprietaire { get => proprietaire; set => proprietaire = value; }
        public int Solde { get => solde; }

        public int Code { get => code; }

        public Compte(Client proprietaire)
        {
            this.code = CodeCourant;
            CodeCourant++;
            this.proprietaire = proprietaire;
        }

        public void Crediter(int somme)
        {
            this.solde += somme;
        }

        public void Debiter(int somme)
        {
            this.solde -= somme;
        }

        public void Crediter(int somme, Compte compteADebiter)
        {
            // On débite avant de créditer le compte, si jamais on lève une exeption, on ne crédite pas un compte sans débiter un autre
            compteADebiter.Debiter(somme);
            this.solde += somme;
        }

        public void Debiter(int somme, Compte compteACrediter)
        {
            // On débite avant de créditer le compte, si jamais on lève une exeption, on ne débite pas un compte sans en créditer un autre
            compteACrediter.Crediter(somme);
            this.solde -= somme;
        }

        public void AfficherDetail()
        {
            Console.WriteLine("Le propriétaire est " + this.proprietaire.Prenom + " " + this.proprietaire.Nom + ", le solde du compte est de " + this.solde + " et le code du compte est " + this.code + ".");
        }

        public static void AfficherNbComptes()
        {
            string compte = "comptes.";
            if (CodeCourant < 2)
            {
                compte = "compte.";
            }
            Console.WriteLine("Il y a " + CodeCourant + " " + compte);
        }
    }

    class Article
    {
        string reference, designation;
        float prixHT;
        const float TAUX_TVA = 20F;

        public string Reference { get => reference; set => reference = value; }
        public string Designation { get => designation; set => designation = value; }
        public float PrixHT { get => prixHT; set => prixHT = value; }

        public Article()
        {
            this.reference = "defaut";
            this.designation = "defaut";
            this.prixHT = 0F;
        }
        public Article(string reference, string designation, float prixHT)
        {
            this.reference = reference;
            this.designation = designation;
            this.prixHT = prixHT;
        }

        public Article(string reference, string designation)
        {
            this.reference = reference;
            this.designation = designation;
        }

        public Article(Article articleACopier)
        {
            this.reference = articleACopier.reference;
            this.designation = articleACopier.designation;
            this.prixHT = articleACopier.prixHT;
        }

        public float CalculPrixTTC()
        {
            return this.prixHT + (this.prixHT * TAUX_TVA / 100);
        }

        public void AfficherArticle()
        {
            Console.WriteLine("L'article a pour référence " + this.reference + ", pour désignation " + this.designation + ", pour prix HT " + this.prixHT + " et un taux de TVA de " + TAUX_TVA + "%.Son prix est donc de " + this.CalculPrixTTC());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Exo 1
            CompteBancaire compteBancaire1 = new CompteBancaire("Michel", "£", 100);
            CompteBancaire compteBancaire2 = new CompteBancaire("Robert", "$", 100);
            Console.WriteLine(compteBancaire1.Decrire());
            Console.WriteLine(compteBancaire2.Decrire());
            compteBancaire1.Crediter(20);
            Console.WriteLine("On credite le compte de " + compteBancaire1.Titulaire + " avec 20" + compteBancaire1.Devise);
            compteBancaire2.Debiter(30);
            Console.WriteLine("On debite le compte de " + compteBancaire2.Titulaire + " de 30" + compteBancaire2.Devise);
            Console.WriteLine(compteBancaire1.Decrire());
            Console.WriteLine(compteBancaire2.Decrire());
            Console.WriteLine("Il y a " + CompteBancaire.NombreDeCompte + " comptes bancaires.");
            Console.WriteLine();

            // Exo 2
            Client client1 = new Client("cin1", "Dupont", "Robert", "0123456789");
            Client client2 = new Client("cin2", "Dupuis", "Michel");
            client1.Afficher();
            client2.Afficher();
            Compte.AfficherNbComptes();
            Compte compte1 = new Compte(client1);
            Compte compte2 = new Compte(client2);
            compte1.AfficherDetail();
            Console.WriteLine("On crédite le compte de " + compte1.Proprietaire.Prenom + " " + compte1.Proprietaire.Nom + " de 100.");
            compte1.Crediter(100);
            compte1.AfficherDetail();
            Console.WriteLine("On débite le compte de " + compte1.Proprietaire.Prenom + " " + compte1.Proprietaire.Nom + " de 30.");
            compte1.Debiter(30);
            compte1.AfficherDetail();
            compte2.AfficherDetail();
            Console.WriteLine("On débite le compte de " + compte1.Proprietaire.Prenom + " " + compte1.Proprietaire.Nom + " de 20 pour créditer le compte de " + compte2.Proprietaire.Prenom + " " + compte2.Proprietaire.Nom + ".");
            compte1.Debiter(20,compte2);
            compte1.AfficherDetail();
            compte2.AfficherDetail();
            Console.WriteLine("On débite le compte de " + compte1.Proprietaire.Prenom + " " + compte1.Proprietaire.Nom + " de 10 pour créditer le compte de " + compte2.Proprietaire.Prenom + " " + compte2.Proprietaire.Nom + ".");
            compte2.Crediter(10, compte1);
            compte1.AfficherDetail();
            compte2.AfficherDetail();
            Compte.AfficherNbComptes();

            // Exo 3
            Article article1 = new Article();
            Article article2 = new Article("001","Jouet",19.99F);
            Article article3 = new Article("002","Meuble");
            Article article4 = new Article(article3);
            article1.AfficherArticle();
            article2.AfficherArticle();
            article3.AfficherArticle();
            article3.PrixHT = 10F;
            article3.AfficherArticle();
            article4.AfficherArticle();

            Console.ReadKey();
        }


    }
}
