﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public abstract class MPokemon 
    {
        public int IdPokemon { get; set; }
        public string CheminImgPokemon { get; set; }
        public string NomFraPokemon { get; set; }
        public string NomEngPokemon { get; set; }
        public string NumPokemon { get; set; }
        public decimal TaillePokemon { get; set; }
        public decimal PoidsPokemon { get; set; }
        public int BasePV { get; set; }
        public int BaseAttaque { get; set; }
        public int BaseDefense { get; set; }
        public int BaseAttSpe { get; set; }
        public int BaseDefSpe { get; set; }
        public int BaseVit { get; set; }
        public bool Legendaire { get; set; }
        public bool Shiny { get; set; }
        public bool Mega { get; set; }
        public bool Giga { get; set; }
        public bool Fab { get; set; }
        public List<MType> TypesPokemon { get; set; }
        public MPokemon Evolution { get; set; }
        public MGeneration Gen { get; set; }
        public List<MCapacite> PoolCapacites { get; set; }
        public List<MTalent> TalentPokemon { get; set; }
        public string CheminImgPokemonAbsolu
        {
            get
            {
                if (!string.IsNullOrEmpty(CheminImgPokemon))
                {
                    // Obtenez le chemin du répertoire de l'exécutable de l'application
                    string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    // Composez le chemin absolu en concaténant le chemin du répertoire de l'application avec le chemin relatif
                    return Path.Combine(appDirectory, CheminImgPokemon);
                }

                return string.Empty;
            }
        }

        public MPokemon(int idPokemon, string cheminImgPokemon, string nomFraPokemon, string nomEngPokemon, string numPokemon, decimal taillePokemon, decimal poidsPokemon, int basePV, int baseAttaque, int baseDefense, int baseAttSpe, int baseDefSpe, int baseVit, bool legendaire, bool shiny, bool mega, bool giga, bool fab, MPokemon evolution, MGeneration gen)
        {
            IdPokemon = idPokemon;
            CheminImgPokemon = cheminImgPokemon;
            NomFraPokemon = nomFraPokemon;
            NomEngPokemon = nomEngPokemon;
            NumPokemon = numPokemon;
            TaillePokemon = taillePokemon;
            PoidsPokemon = poidsPokemon;
            BasePV = basePV;
            BaseAttaque = baseAttaque;
            BaseDefense = baseDefense;
            BaseAttSpe = baseAttSpe;
            BaseDefSpe = baseDefSpe;
            BaseVit = baseVit;
            Legendaire = legendaire;
            Shiny = shiny;
            Mega = mega;
            Giga = giga;
            Fab = fab;
            Evolution = evolution;
            Gen = gen;
        }

        public MPokemon(int idPokemon, string cheminImgPokemon, string nomFraPokemon)
        {
            IdPokemon = idPokemon;
            CheminImgPokemon = cheminImgPokemon;
            NomFraPokemon = nomFraPokemon;
        }

        public MPokemon(int idPokemon)
        {
            IdPokemon = idPokemon;
        }

        public MPokemon(int idPokemon, string cheminImgPokemon, string nomFraPokemon, int basePV, int baseAttaque, int baseDefense, int baseAttSpe, int baseDefSpe, int baseVit, bool legendaire, bool shiny, bool mega, bool giga, bool fab)
        {
            IdPokemon = idPokemon;
            CheminImgPokemon = cheminImgPokemon;
            NomFraPokemon = nomFraPokemon;
            BasePV = basePV;
            BaseAttaque = baseAttaque;
            BaseDefense = baseDefense;
            BaseAttSpe = baseAttSpe;
            BaseDefSpe = baseDefSpe;
            BaseVit = baseVit;
            Legendaire = legendaire;
            Shiny = shiny;
            Mega = mega;
            Giga = giga;
            Fab = fab;
            TypesPokemon = new List<MType>();
        }

        public MPokemon(int IdPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit)
        {
            this.IdPokemon = IdPokemon;
            this.BasePV = BasePV;
            this.BaseAttaque = BaseAttaque;
            this.BaseDefense = BaseDefense;
            this.BaseAttSpe = BaseAttSpe;
            this.BaseDefSpe = BaseDefSpe;
            this.BaseVit = BaseVit;
        }


        public MPokemon() { }

     
    }
}
