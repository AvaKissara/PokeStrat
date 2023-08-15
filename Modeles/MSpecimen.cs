using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    /// <summary>
    /// Classe dérivée de MPokemon correpondant à un specimen de pokémon dans le Pokedex
    /// </summary>
    public class MSpecimen : MPokemon
    {     
        public string desc { get; set; }

        public MSpecimen(int IdPokemon, string CheminImgPokemon, string NomFraPokemon) : base(IdPokemon, CheminImgPokemon, NomFraPokemon)
        {
            
        }

        public MSpecimen()
        {

        }

        public MSpecimen(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BaseXp, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, MPokemon Evolution, int NivEvolution, MGeneration Gen) : base(IdPokemon, CheminImgPokemon, NomFraPokemon, nomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, BaseXp, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Evolution, NivEvolution, Gen)
        {

        }
    }
}
