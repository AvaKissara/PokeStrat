using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class PokemonCapacite : IPokemonCapacite
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
        public int IdCapacite { get; }
        public string NomCapacite { get; set; }
        public MType TypeCapacite { get; set; }
        public List<MCategorie> CatCapacite { get; set; }

        public PokemonCapacite(MPokemon pokemon, MCapacite capacite)
        {
            IdPokemon = pokemon.idPokemon;
            CheminImgPokemon = pokemon.cheminImgPokemon;
            NomFraPokemon = pokemon.nomFraPokemon;
            NumPokemon = pokemon.numPokemon;
            Niveau = pokemon.niveau;
            Pv = pokemon.pv;
            Attaque = pokemon.attaque;
            Defense = pokemon.defense;
            AttSpe = pokemon.attSpe;
            DefSpe = pokemon.defSpe;
            Vitesse = pokemon.vitesse;
            IdCapacite = capacite.idCapacite;
            NomCapacite = capacite.nomCapacite;
            TypeCapacite = capacite.typeCapacite;
            CatCapacite = capacite.catCapacite;
        }
    }

}
