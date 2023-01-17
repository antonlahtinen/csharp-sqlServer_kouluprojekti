using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace OO_NeljäsPalautus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            bool jatkuu;
            List<Laakari> laakarit = new List<Laakari>();
            
            string yhteysMerkkijono = @"Data source=(localdb)\mssqllocaldb;Initial Catalog=Hammaslaakari;Integrated Security=true;";
            using (SqlConnection sqlConnection = new SqlConnection(yhteysMerkkijono))
            {
                do
                {

                    Console.Write("Tervetuloa muokkaamaan tietokantaa Hammaslääkäri. Valitse vaihtoehdoista 1-5.\n \n" +
                                  "Syötä 1, jos haluat nähdä kaikki hammaslääkärit.\n" +
                                  "Syötä 2, jos haluat lisätä uuden hammaslääkärin.\n" +
                                  "Syötä 3, jos haluat muuttaa hammaslääkärin tietoja\n" +
                                  "Syötä 4, jos haluat poistaa lääkärin.\n" +
                                  "Syötä 5, jos et halua tehdä mitään. " +
                                  "\nValinta: ");

                    string annettu = Console.ReadLine();
                    int valittuNumero;
                    while (!Int32.TryParse(annettu, out valittuNumero) && valittuNumero > 0 && valittuNumero <= 5)
                    {
                        Console.Write("Väärä valinta yritä uudelleen 1-5.");
                        annettu = Console.ReadLine();
                    }
                    Console.WriteLine();

                    if (valittuNumero == 1)
                    {
                        Laakari.TulostaTiedot(sqlConnection);
                    }
                    else if (valittuNumero == 2)
                    {
                        Laakari.LisaaLaakari(sqlConnection);
                    }
                    else if (valittuNumero == 3)
                    {

                        Laakari.MuutaTietoja(sqlConnection);
                    }
                    else if (valittuNumero == 4)
                    {

                        Laakari.PoistaLaakari(sqlConnection);
                    }
                    else if (valittuNumero == 5)
                    {
                        Console.WriteLine("Et tee mitään.");
                    }
                    else          
                        Console.Write("Väärä valinta, yritä uudelleen.");
                    Console.WriteLine();


                    Console.Write("Jatketaanko K/E?");

                    string valinta = Console.ReadLine().ToUpper();
                    if (valinta.StartsWith("K"))
                        jatkuu = true;
                    else
                        jatkuu = false;
                    Console.WriteLine();
                } while (jatkuu);
            }
        }
    }
}
