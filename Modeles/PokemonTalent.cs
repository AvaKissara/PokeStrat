using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class PokemonTalent : IPokemon, ITalent
    {
        public int IdPokemon { get; }
        public string CheminImgPokemon { get; set; }
        public string NomFraPokemon { get; set; }
        public string NomEngPokemon { get; set; }
        public string NumPokemon { get; set; }
        public double TaillePokemon { get; set; }
        public double PoidsPokemon { get; set; }
        public int Niveau { get; set; }
        public int BaseXp { get; set; }
        public int Pv { get; set; }
        public int Attaque { get; set; }
        public int Defense { get; set; }
        public int AttSpe { get; set; }
        public int DefSpe { get; set; }
        public int Vitesse { get; set; }
        public bool Legendaire { get; set; }
        public bool Shiny { get; set; }
        public int NivEvolution { get; set; }
        public List<MType> TypesPokemon { get; set; }
        public MPokemon Evolution { get; set; }
        public MVersion Gen { get; set; }
        public int IdTalent { get; }
        public string NomTalent { get; set; }
        public MVersion VersionTalent { get; set; }
        public string DescTalent { get; set; }

        public PokemonTalent(MPokemon pokemon, MTalent talent)
        {
            this.IdPokemon = pokemon.idPokemon;
            this.CheminImgPokemon = pokemon.cheminImgPokemon;
            this.NomFraPokemon = pokemon.nomFraPokemon;
            this.NumPokemon = pokemon.numPokemon;
            this.Niveau = pokemon.niveau;
            this.Pv = pokemon.pv;
            this.Attaque = pokemon.attaque;
            this.Defense = pokemon.defense;
            this.AttSpe = pokemon.attSpe;
            this.DefSpe = pokemon.defSpe;
            this.Vitesse = pokemon.vitesse;

            this.IdTalent = talent.idTalent;
            this.NomTalent = talent.nomTalent;
            this.VersionTalent = talent.versionTalent;
            this.DescTalent = talent.descTalent;
        }
    }
}
