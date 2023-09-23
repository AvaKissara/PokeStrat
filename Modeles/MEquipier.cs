using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MEquipier : MPokemon
    {
        public string surnomEquipier { get; set; }
        public int niveauEquipier { get; set; }
        public int esquiveEquipier { get; set; }
        public int ev { get; set; }
        public int iv { get; set; }
        public int niveauBonheur { get; set; }
        public MNature nature { get; set; }
        public MTalent talentEquipier { get; set; }
        public List<MCapacite> setCapacites { get; set; }
        public MObjet objetEquipier { get; set; }
        public MEquipe equipeEquipier { get; set; }


        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, bool Mega, bool Giga, bool Fab, string SurnomEquipier, int NiveauEquipier, int EsquiveEquipier, int NiveauBonheur, int Ev, int Iv, MNature Nature, MTalent TalentEquipier, MObjet ObjetEquipier)
        : base(IdPokemon, CheminImgPokemon, NomFraPokemon, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Mega, Giga, Fab)
        {
            this.surnomEquipier = SurnomEquipier;
            this.niveauEquipier = NiveauEquipier;
            this.esquiveEquipier = EsquiveEquipier;
            this.ev = Ev;
            this.iv = Iv;
            this.niveauBonheur = NiveauBonheur;
            this.nature = Nature;
            this.setCapacites = new List<MCapacite>();
            this.talentEquipier = TalentEquipier;
            this.objetEquipier = ObjetEquipier;

        }
        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, bool Mega, bool Giga, bool Fab, MPokemon Evolution, MGeneration Gen, string SurnomEquipier, int NiveauEquipier)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon, nomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Mega, Giga, Fab, Evolution, Gen)
        {
            this.surnomEquipier = SurnomEquipier;
            this.niveauEquipier = NiveauEquipier;
        }

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon)
        {
           
        }
    

    }

}
