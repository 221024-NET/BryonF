using PokeApp.Logic;
using System.Data.SqlClient;

namespace PokeApp.Data
{
    public class SqlRepository : IRepository
    {
        // Fields
        string ConnectionString;

        // Constructors
        public SqlRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // Methods

        public List<Pokemon> GetAllPokemon()
        {
            List<Pokemon> Pokemons = new List<Pokemon>();

            using SqlConnection connection = new SqlConnection(this.ConnectionString);
            connection.Open();
            //      0       1       2           3     4     5       6
            string cmdText = "SELECT Name, DexNumber, Health, Level, Exp, Type, PokemonId" +
                "  FROM Pokemon.Pokemons;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Id = reader.GetInt32(6);
                string Name = reader.GetString(0);
                int Type = reader.GetInt32(5);
                int Health = reader.GetInt32(2);
                int DexNumber = reader.GetInt32(1);
                int Level = reader.GetInt32(3);
                int Exp = reader.GetInt32(4);

                Pokemons.Add(new Pokemon(Name, Level, Exp, Health, Type, Id, DexNumber));

                //Pokemons.Add(new Pokemon(reader.GetString(0), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(2), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(1)));
            }
            connection.Close();

            return Pokemons;
        }

        public string UpdatePokemonName(string newName, int Id)
        {
            using SqlConnection connection = new SqlConnection(this.ConnectionString);
            connection.Open();
            //      0       1       2           3     4     5       6
            string cmdText = @"UPDATE Pokemon.Pokemons SET Name = @newName" +
                              "WHERE PokemonId = @Id;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@newName", newName);
            cmd.Parameters.AddWithValue("@Id", Id);

            cmd.ExecuteNonQuery();

            string cmdText2 = "SELECT Name FROM Pokemon.Pokemons " +
                                "WHERE PokemonId = @Id;";
            using SqlCommand cmd2 = new SqlCommand(cmdText2, connection);
            using SqlDataReader reader = cmd2.ExecuteReader();

            string? updatedName;
            while (reader.Read())
            {
                updatedName = reader.GetString(0);
            }

            connection.Close();

            if (updatedName != null)
            {
                return updatedName;
            }
            else
            {
                return null;
            }
        }

    }
}