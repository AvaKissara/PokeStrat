﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public abstract class MPokemon
    {  
        public int idPokemon { get; }
        public string cheminImgPokemon { get; set; }
        public string nomFraPokemon { get; set; }
        public string nomEngPokemon { get; set; }
        public string numPokemon { get; set; }
        public decimal taillePokemon { get; set; }
        public decimal poidsPokemon { get; set; }
        public int baseXp { get; set; }
        public int basePv { get; set; }
        public int baseAttaque { get; set; }
        public int baseDefense { get; set; }
        public int baseAttSpe { get; set; }
        public int baseDefSpe { get; set; }
        public int baseVit { get; set; }
        public bool legendaire { get; set; }
        public bool shiny { get; set; }
        public int nivEvolution { get; set; }
        public List<MType> typesPokemon { get; set; }
        public MPokemon evolution { get; set; }
        public MGeneration gen { get; set; }
        public List<MCapacite> poolCapacites { get; set; }
        public List<MTalent> talentPokemon { get; set; }
        public MPokemon(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BaseXp, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, MPokemon Evolution, int NivEvolution, MGeneration Gen)
        {
            this.idPokemon = IdPokemon;
            this.cheminImgPokemon = CheminImgPokemon;
            this.nomFraPokemon = NomFraPokemon;
            this.nomEngPokemon = nomEngPokemon;
            this.numPokemon = NumPokemon;
            this.taillePokemon = TaillePokemon;
            this.poidsPokemon = PoidsPokemon;
            this.baseXp = BaseXp;
            this.basePv = BasePV;
            this.baseAttaque = BaseAttaque;
            this.baseDefense = BaseDefense;
            this.baseAttSpe = BaseAttSpe;
            this.baseDefSpe = BaseDefSpe;
            this.baseVit = BaseVit;
            this.legendaire = Legendaire;
            this.shiny = Shiny;
            this.evolution = Evolution;
            this.nivEvolution = NivEvolution;
            this.gen = Gen;
        }

        public MPokemon(int IdPokemon, string CheminImgPokemon, string NomFraPokemon)
        { 
            this.idPokemon = IdPokemon;
            this.cheminImgPokemon = CheminImgPokemon;
            this.nomFraPokemon = NomFraPokemon;
        }

        public MPokemon()
        { }
    }
}
