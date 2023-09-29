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
        public string SurnomEquipier { get; set; }
        public int NiveauEquipier { get; set; }
        public int EsquiveEquipier { get; set; }
        public int Ev { get; set; }
        public int Iv { get; set; }
        public int NiveauBonheur { get; set; }
        public MNature Nature { get; set; }
        public MTalent TalentEquipier { get; set; }
        public List<MCapacite> SetCapacites { get; set; }
        public MObjet ObjetEquipier { get; set; }
        public MEquipe EquipeEquipier { get; set; }

        public MEquipier(int idPokemon, string cheminImgPokemon, string nomFraPokemon, int basePV, int baseAttaque, int baseDefense, int baseAttSpe, int baseDefSpe, int baseVit, bool legendaire, bool shiny, bool mega, bool giga, bool fab, string surnomEquipier, int niveauEquipier, int esquiveEquipier, int niveauBonheur, int ev, int iv, MNature nature, MTalent talentEquipier, MObjet objetEquipier)
            : base(idPokemon, cheminImgPokemon, nomFraPokemon, basePV, baseAttaque, baseDefense, baseAttSpe, baseDefSpe, baseVit, legendaire, shiny, mega, giga, fab)
        {
            SurnomEquipier = surnomEquipier;
            NiveauEquipier = niveauEquipier;
            EsquiveEquipier = esquiveEquipier;
            NiveauBonheur = niveauBonheur;
            Ev = ev;
            Iv = iv;
            Nature = nature;
            SetCapacites = new List<MCapacite>();
            TalentEquipier = talentEquipier;
            ObjetEquipier = objetEquipier;
        }

        public MEquipier(int idPokemon, string cheminImgPokemon, string nomFraPokemon, string nomEngPokemon, string numPokemon, decimal taillePokemon, decimal poidsPokemon, int basePV, int baseAttaque, int baseDefense, int baseAttSpe, int baseDefSpe, int baseVit, bool legendaire, bool shiny, bool mega, bool giga, bool fab, MPokemon evolution, MGeneration gen, string surnomEquipier, int niveauEquipier)
            : base(idPokemon, cheminImgPokemon, nomFraPokemon, nomEngPokemon, numPokemon, taillePokemon, poidsPokemon, basePV, baseAttaque, baseDefense, baseAttSpe, baseDefSpe, baseVit, legendaire, shiny, mega, giga, fab, evolution, gen)
        {
            SurnomEquipier = surnomEquipier;
            NiveauEquipier = niveauEquipier;
        }

        public MEquipier(int idPokemon, string cheminImgPokemon, string nomFraPokemon)
            : base(idPokemon, cheminImgPokemon, nomFraPokemon)
        {
        }
    }
}
