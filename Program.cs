using System;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace GestionnaireCompteBancaire
{
    class Program
    {
        
    static void Main(string[] args)
        {
             
            string[] files = { "titulaire.txt", "numero_compte.txt", "solde_courant.txt", "solde_epargne.txt", "transactions_courant.txt", "transactions_epargne.txt" };
            string nom;
            string numeroCompte;

            Console.WriteLine("entré votre nom");
            nom = Console.ReadLine();
            Console.WriteLine("entré votre numéro de compte");
            numeroCompte = Console.ReadLine();

            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    if (file.StartsWith("solde_courant"))
                    {
                        File.WriteAllText(file, "0");
                    }
                    else if (file.StartsWith("solde_epargne"))
                    {
                        File.WriteAllText(file, "0");
                    }
                    else if (file.StartsWith("titulaire"))
                    {
                        File.WriteAllText(file, nom);
                    }
                    else if (file.StartsWith("numero_compte"))
                    {
                        File.WriteAllText(file, numeroCompte);
                    }
                    else
                    {
                        File.WriteAllText(file,"");
                    }
                }    
            }
            

            if (nom == File.ReadAllText("titulaire.txt") && numeroCompte == File.ReadAllText("numero_compte.txt")){
                Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                Console.ReadLine();
                AfficherMenu();
            }
            else
            {
                Console.WriteLine("nom ou numéro de compte incorrect");
            }
            
            
        }
        
        static void AfficherMenu()
        {
            string choix;
            do
            {
                Console.WriteLine("Veuillez sélectionner une option ci-dessous :");
                Console.WriteLine("[I] Voir les informations sur le titulaire du compte");
                Console.WriteLine("[CS] Compte courant - Consulter le solde");
                Console.WriteLine("[CD] Compte courant - Déposer des fonds");
                Console.WriteLine("[CR] Compte courant - Retirer des fonds");
                Console.WriteLine("[ES] Compte épargne - Consulter le solde");
                Console.WriteLine("[ED] Compte épargne - Déposer des fonds");
                Console.WriteLine("[ER] Compte épargne - Retirer des fonds");
                Console.WriteLine("[X] Quitter");

                choix = Console.ReadLine();

                switch (choix.ToUpper())
                {
                    case "I":
                        AfficherInformationsTitulaire();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "CS":
                        ConsulterSoldeCompteCourant();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "CD":
                        DeposerFondsCompteCourant();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "CR":
                        RetirerFondsCompteCourant();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "ES":
                        ConsulterSoldeCompteEpargne();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "ED":
                        DeposerFondsCompteEpargne();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "ER":
                        RetirerFondsCompteEpargne();
                        Console.WriteLine("Appuyez sur Entrée pour afficher le menu.");
                        Console.ReadLine();
                        break;
                    case "X":
                        Quitter();
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }
            } while (choix.ToUpper() != "X");
        }

        static void AfficherInformationsTitulaire()
        {
            string nomTitulaire = File.ReadAllText("titulaire.txt");
            string numeroCompte = File.ReadAllText("numero_compte.txt");

            Console.WriteLine("Informations sur le titulaire du compte :");
            Console.WriteLine("Nom du titulaire : " + nomTitulaire);
            Console.WriteLine("Numéro de compte : " + numeroCompte);
        }

        static void ConsulterSoldeCompteCourant()
        {
            string soldeCourant = File.ReadAllText("solde_courant.txt");

            Console.WriteLine("Solde du compte courant : " + soldeCourant + " €");
        }

        static void ConsulterSoldeCompteEpargne()
        {
            string soldeEpargne = File.ReadAllText("solde_epargne.txt");

            Console.WriteLine("Solde du compte épargne : " + soldeEpargne + " €");
        }

        static void DeposerFondsCompteCourant()
        {
            Console.WriteLine("Quel montant souhaitez-vous déposer sur le compte courant ?");
            string montantDepot = Console.ReadLine();

            string soldeCourant = File.ReadAllText("solde_courant.txt");
            decimal nouveauSolde = decimal.Parse(soldeCourant) + decimal.Parse(montantDepot);
            File.WriteAllText("solde_courant.txt", nouveauSolde.ToString());

            Console.WriteLine("Vous avez déposé : " + montantDepot + " €.");
        }

        static void DeposerFondsCompteEpargne()
        {
            Console.WriteLine("Quel montant souhaitez-vous déposer sur le compte épargne ?");
            string montantDepot = Console.ReadLine();

            string soldeEpargne = File.ReadAllText("solde_epargne.txt");
            decimal nouveauSolde = decimal.Parse(soldeEpargne) + decimal.Parse(montantDepot);
            File.WriteAllText("solde_epargne.txt", nouveauSolde.ToString());

            Console.WriteLine("Vous avez déposé : " + montantDepot + " €.");
        }

        static void RetirerFondsCompteCourant()
        {
            Console.WriteLine("Quel montant souhaitez-vous retirer du compte courant ?");
            string montantRetrait = Console.ReadLine();

            string soldeCourant = File.ReadAllText("solde_courant.txt");
            decimal solde = decimal.Parse(soldeCourant);
            decimal montant = decimal.Parse(montantRetrait);

            if (solde >= montant)
            {
                decimal nouveauSolde = solde - montant;
                File.WriteAllText("solde_courant.txt", nouveauSolde.ToString());
                Console.WriteLine("Vous avez retiré : " + montantRetrait + " €.");
            }
            else
            {
                Console.WriteLine("Solde insuffisant pour effectuer le retrait.");
            }
        }

        static void RetirerFondsCompteEpargne()
        {
            Console.WriteLine("Quel montant souhaitez-vous retirer du compte épargne ?");
            string montantRetrait = Console.ReadLine();

            string soldeEpargne = File.ReadAllText("solde_epargne.txt");
            decimal solde = decimal.Parse(soldeEpargne);
            decimal montant = decimal.Parse(montantRetrait);

            if (solde >= montant)
            {
                decimal nouveauSolde = solde - montant;
                File.WriteAllText("solde_epargne.txt", nouveauSolde.ToString());
                Console.WriteLine("Vous avez retiré : " + montantRetrait + " €.");
            }
            else
            {
                Console.WriteLine("Solde insuffisant pour effectuer le retrait.");
            }
        }

        static void Quitter()
        {
            string transactions = "Transactions du compte courant : \n" + File.ReadAllText("transactions_courant.txt") + "\n\n" +
                                  "Transactions du compte épargne : \n" + File.ReadAllText("transactions_epargne.txt");
            File.WriteAllText("transactions.txt", transactions);

            Console.WriteLine("Merci d'avoir utilisé notre application. Les transactions ont été enregistrées dans un fichier.");
        }
    }
}
