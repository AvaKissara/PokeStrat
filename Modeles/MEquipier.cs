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
        public string surnomEquipier { get; set; }
        public int niveauEquipier { get; set; }
        public int esquiveEquipier { get; set; }
        public int ev { get; set; }
        public int iv { get; set; }
        public int niveauBonheur { get; set; }
        public string nature { get; set; }
        public MTalent talentEquipier { get; set; }
        public List<MCapacite> setCapacites { get; set; }
        public MObjet objetEquipier { get; set; }
        public MEquipe equipeEquipier { get; set; } 

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, bool Mega, bool Giga, bool Fab, MPokemon Evolution, MGeneration Gen, int IdEquipier, string SurnomEquipier, int NiveauEquipier)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon, nomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Mega, Giga, Fab, Evolution, Gen)
        {
            this.idEquipier = IdEquipier;
            this.surnomEquipier = SurnomEquipier;
            this.niveauEquipier = NiveauEquipier;
        }

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon)
        {
           
        }

    }

}
