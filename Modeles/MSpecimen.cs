using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Desc { get; set; }

       

        public MSpecimen(int IdPokemon, string CheminImgPokemon, string NomFraPokemon) : base(IdPokemon, CheminImgPokemon, NomFraPokemon)
        {
            
        }

        public MSpecimen()
        {

        }

        public MSpecimen(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, bool Mega, bool Giga, bool Fab, MPokemon Evolution, MGeneration Gen) : base(IdPokemon, CheminImgPokemon, NomFraPokemon, nomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Mega, Giga, Fab, Evolution, Gen)
        {

        }

    }
}
