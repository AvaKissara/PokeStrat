using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MEquipier : MPokemon
    {
        public int idEquipier { get; set; }
        public string nomEquipier { get; set; }
        public int niveauEquipier { get; set; }
        public int esquiveEquipier { get; set; }
        public int ev { get; set; }
        public int iv { get; set; }
        public int niveauBonheur { get; set; }
        public string nature { get; set; }
        public List<MCapacite> setCapacites { get; set; }
        public MTalent talentEquipier { get; set; }
        public MEquipe equipeEquipier { get; set; } 

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BaseXp, int PV, int Attaque, int Defense, int AttSpe, int DefSpe, int Vitesse, bool Legendaire, bool Shiny, MPokemon Evolution, int NivEvolution, MGeneration Gen, int IdEquipier, string NomEquipier, int NiveauEquipier)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon, nomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, BaseXp, PV, Attaque, Defense, AttSpe, DefSpe, Vitesse, Legendaire, Shiny, Evolution, NivEvolution, Gen)
        {
            this.idEquipier = IdEquipier;
            this.nomEquipier = NomEquipier;
            this.niveauEquipier = NiveauEquipier;
        }

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon)
        {
           
        }

    }

}
