using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PokeStrat.Modeles
{
    public class MPokemon
    {  public int idPokemon { get; }
        public string cheminImgPokemon { get; set; }
        public string nomFraPokemon { get; set; }
        public string nomEngPokemon { get; set; }
        public string numPokemon { get; set; }
        public double taillePokemon { get; set; }
        public double poidsPokemon { get; set; }
        public int niveau { get; set; }
        public int ev { get; set; }
        public int iv { get; set; }
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
        public List<MType> typesPokemon { get; set; }
        public MPokemon evolution { get; set; }
        public MVersion gen { get; set; }
        public string nature { get; set; }

        public MPokemon(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string nomEngPokemon, string NumPokemon, double TaillePokemon, int PoidsPokemon, int BaseXp, int PV, int Attaque, int Defense, int AttSpe, int DefSpe, int Vitesse, bool Legendaire, bool Shiny, MPokemon Evolution, int NivEvolution, MVersion Gen)
        {
            this.idPokemon = IdPokemon;
            this.cheminImgPokemon = CheminImgPokemon;
            this.nomFraPokemon = NomFraPokemon;
            this.nomEngPokemon = nomEngPokemon;
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
    }
}
