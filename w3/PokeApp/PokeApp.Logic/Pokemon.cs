namespace PokeApp.Logic
{
    public class Pokemon
    {
        // Fields
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public int DexNum { get; set; }
        // dexNum

        // Constructor
        public Pokemon() { }

        public Pokemon(string name, int level, int experience, int health, int type, int id, int dexNum)
        {
            this.Name = name;
            this.Level = level;
            this.Experience = experience;
            this.Health = health;
            this.Type = type;
            this.Id = id;
            this.DexNum = dexNum;
        }

        // Methods
        public string Speak()
        {
            return this.Name;
        }

        public bool UpdatePokemonName(string newName)
        {
            try { repo.UpdatePokemon(newName); }
            catch { return false; }
            this.Name = newName;
            return true;
        }
    }
}