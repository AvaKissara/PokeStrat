using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public interface IPokemonTalent
    {
        int IdPokemon { get; }
        string CheminImgPokemon { get; set; }
        string NomFraPokemon { get; set; }
        string NomEngPokemon { get; set; }
        string NumPokemon { get; set; }
        double TaillePokemon { get; set; }
        double PoidsPokemon { get; set; }
        int Niveau { get; set; }
        int BaseXp { get; set; }
        int Pv { get; set; }
        int Attaque { get; set; }
        int Defense { get; set; }
        int AttSpe { get; set; }
        int DefSpe { get; set; }
        int Vitesse { get; set; }
        bool Legendaire { get; set; }
        bool Shiny { get; set; }
        int NivEvolution { get; set; }
        List<MType> TypesPokemon { get; set; }
        MPokemon Evolution { get; set; }
        MVersion Gen { get; set; }
        int IdTalent { get; }
        string NomTalent { get; set; }
        MVersion VersionTalent { get; set; }
        string DescTalent { get; set; }
    }
}
