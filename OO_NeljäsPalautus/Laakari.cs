using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OO_NeljäsPalautus
{
    internal class Laakari
    {
        public Laakari(int id, string nimi, string puhelinnumero)
        {
            Id = id;
            Nimi = nimi;
            Puhelinnumero = puhelinnumero;
        }

        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Puhelinnumero { get; set; }

        public static void TulostaTiedot(SqlConnection munYhteys)
        {
            try
            {
                string kysely = "SELECT * FROM Laakari";
                SqlCommand sqlCommand = new SqlCommand(kysely, munYhteys);
                munYhteys.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}, {2}",
                                reader[0], reader[1], reader[2]));
                        }
                    }
                    else
                        Console.WriteLine("Nyt oli tyhjä taulu.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            munYhteys.Close();
        }
        public static void LisaaLaakari(SqlConnection munYhteys)
        {
            Console.WriteLine("Lisää uusi lääkäri tiimiisi.");
            try
            {
                string lisäys = "INSERT INTO Laakari (Nimi, Puhelinnumero) VALUES (@LaaNimi, @LaaPuhelin)";
                SqlCommand sqlCommand = new SqlCommand(lisäys, munYhteys);
                munYhteys.Open();

                Console.Write("Anna nimi: ");
                string nimi = Console.ReadLine();

                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = "@LaaNimi",
                    Value = nimi,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };
                sqlCommand.Parameters.Add(sqlParameter);

                Console.Write("Anna puhelinnumero: ");
                string puhelinNumero = Console.ReadLine();

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@LaaPuhelin",
                    Value = puhelinNumero,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();

                //Menikö mitään?
                Console.WriteLine("Päivitetty taulu:");

                string kysely = "SELECT * FROM Laakari";
                sqlCommand = new SqlCommand(kysely, munYhteys);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}, {2}",
                                reader[0], reader[1], reader[2]));
                        }
                    }
                    else
                        Console.WriteLine("Nyt oli tyhjä taulu.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            munYhteys.Close();
        }
       
        public static void MuutaTietoja(SqlConnection munYhteys)
        {
            Console.WriteLine("Vaihda hammaslääkärin puhelinnumero.");
            Console.WriteLine();
            try
            {
                
                string muutos = "UPDATE Laakari SET Puhelinnumero=@1PuhNro WHERE NIMI LIKE @lNimi+'%'";
                SqlCommand sqlCommand = new SqlCommand(muutos, munYhteys);
                munYhteys.Open();

                Console.Write("Anna lääkärin nimi, jonka puhelinnumero tulee vaihtaa: ");
                string nimi = Console.ReadLine();

                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = "@lNimi",
                    Value = nimi,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };
                sqlCommand.Parameters.Add(sqlParameter);

                Console.Write("Anna valitsemasi lääkärin uusi puhelinnumero: ", nimi);
                string puhelinNumero = Console.ReadLine();

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@1PuhNro",
                    Value = puhelinNumero,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();

                //Päivittyikö informaatio?

                Console.WriteLine("Katsotaan taulun sisältö päivityskomennon jälkeen.");

                string kysely = "SELECT * FROM Laakari";
                sqlCommand = new SqlCommand(kysely, munYhteys);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}, {2}",
                                reader[0], reader[1], reader[2]));
                        }
                    }
                    else
                        Console.WriteLine("Nyt oli tyhjä taulu.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            munYhteys.Close();
        }

        public static void PoistaLaakari(SqlConnection munYhteys)
        {
            try
            {
                string poisto = "DELETE FROM Laakari WHERE Nimi LIKE @lNimi+'%'";
                SqlCommand sqlCommand = new SqlCommand(poisto, munYhteys);

                Console.Write("Anna lääkärin nimi, jonka mahdollisesti poistat: ");
                string annettuNimi = Console.ReadLine();

                SqlParameter sp = new SqlParameter
                {
                    ParameterName = "@lNimi",
                    Value = annettuNimi,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };
                sqlCommand.Parameters.Add(sp);
                munYhteys.Open();

                int rivienLkm = sqlCommand.ExecuteNonQuery();
                if (rivienLkm != 0)
                {

                    Console.WriteLine("Poistettiin {0} riviä.", rivienLkm);
                    Console.WriteLine("Taulun sisältö poiston jälkeen.");

                    sqlCommand = new SqlCommand("SELECT * FROM Laakari", munYhteys);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0}, {1}, {2}",
                                    reader[0], reader[1], reader[2]));
                            }
                        }
                        else
                            Console.WriteLine("Henkilöä ei löytynyt.");
                    }
                }
                else
                    Console.WriteLine("Henkilöä ei löytynyt");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            munYhteys.Close();
        }

    }

}
