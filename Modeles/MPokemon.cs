using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public class MPokemon
    {  public int idPokemon { get; }
        public string cheminImgPokemon { get; set; }
        public string nomPokemon { get; set; }
        public string numPokemon { get; set; }
        public double taillePokemon { get; set; }
        public double poidsPokemon { get; set; }
        public int baseXp { get; set; }
        public int pv { get; set; }
        public int attaque { get; set; }
        public int defense { get; set; }
        public int attSpe { get; set; }
        public int defSpe { get; set; }
        public int vitesse { get; set; }
        public bool legendaire { get; set; }
        public bool shiny { get; set; }
        public int nivEvolution { get; set; }
        public MPokemon evolution { get; set; }

        public MPokemon(int IdPokemon, string CheminImgPokemon, string NomPokemon, string NumPokemon, double TaillePokemon, int PoidsPokemon, int BaseXp, int PV, int Attaque, int Defense, int AttSpe, int DefSpe, int Vitesse, bool Legendaire, bool Shiny, int NivEvolution, MPokemon Evolution)
        {
            this.idPokemon = IdPokemon;
            this.cheminImgPokemon = CheminImgPokemon;
            this.nomPokemon = NomPokemon;
            this.numPokemon = NumPokemon;
            this.taillePokemon = TaillePokemon;
            this.poidsPokemon = PoidsPokemon;
            this.baseXp = BaseXp;
            this.pv = PV;
            this.attaque = Attaque;
            this.defense = Defense;
            this.attSpe = AttSpe;
            this.defSpe = DefSpe;
            this.vitesse = Vitesse;
            this.legendaire = Legendaire;
            this.shiny = Shiny;
            this.nivEvolution = NivEvolution;
            this.evolution = Evolution;
        }
    }
}
