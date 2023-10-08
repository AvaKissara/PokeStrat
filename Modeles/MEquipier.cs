﻿
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues.User.GestionEquipe;
using PokeStat.VuesModeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokeStat.Modeles
{
    public class MEquipier : MPokemon, INotifyPropertyChanged
    {
        public string SurnomEquipier { get; set; }
        public int NiveauEquipier { get; set; }
        public int EsquiveEquipier { get; set; }
        public int Ev { get; set; }
        public int Iv { get; set; }
        public int NiveauBonheur { get; set; }
        public MNature Nature { get; set; }
        public MTalent TalentEquipier { get; set; }
        public ObservableCollection<MCapacite> SetCapacites { get; set; }
        public MObjet ObjetEquipier { get; set; }
        public MEquipe EquipeEquipier { get; set; }
        public int EquipeId { get; set; }
       
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }



        private readonly RepPokemon repPokemon;
        private readonly RepNature repNature;
        private readonly RepObjet repObjet; 
        private readonly RepCapacite repCapacite;

        public List<MSpecimen> pokemons { get; set; }
        public List<MNature> natures { get; set; }
        public List<MObjet> objets { get; set; }
        public List<MCapacite> capacites { get; set; }

        public ICommand DetailPopupCommand { get; set; }


        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, bool Mega, bool Giga, bool Fab, string SurnomEquipier, int NiveauEquipier, int EsquiveEquipier, int NiveauBonheur, int Ev, int Iv, MNature Nature, MTalent TalentEquipier, MObjet ObjetEquipier, ObservableCollection<MCapacite> SetCapacites, int EquipeId)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Mega, Giga, Fab)
        {
            this.SurnomEquipier = SurnomEquipier;
            this.NiveauEquipier = NiveauEquipier;
            this.EsquiveEquipier = EsquiveEquipier;
            this.NiveauBonheur = NiveauBonheur;
            this.Ev = Ev;
            this.Iv = Iv;
            this.Nature = Nature;          
            this.TalentEquipier = TalentEquipier;
            this.ObjetEquipier = ObjetEquipier;
            this.EquipeId = EquipeId;
            this.SetCapacites = new ObservableCollection<MCapacite>();
            this.SetCapacites = SetCapacites;
            repPokemon = new RepPokemon();
            repNature = new RepNature();
            repObjet = new RepObjet();
            repCapacite = new RepCapacite();
            this.pokemons = repPokemon.GetAll();
            this.natures = repNature.GetAll();
            this.objets = repObjet.GetAll();
            this.PoolCapacites = repPokemon.GetPoolCapacite(IdPokemon);
            this.TalentPokemon = repPokemon.GetPoolTalent(IdPokemon);
            this.PoolCapacites = repCapacite.GetCapacite(IdPokemon);
        }

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon, string NomEngPokemon, string NumPokemon, decimal TaillePokemon, decimal PoidsPokemon, int BasePV, int BaseAttaque, int BaseDefense, int BaseAttSpe, int BaseDefSpe, int BaseVit, bool Legendaire, bool Shiny, bool Mega, bool Giga, bool Fab, MPokemon Evolution, MGeneration Gen, string SurnomEquipier, int NiveauEquipier)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon, NomEngPokemon, NumPokemon, TaillePokemon, PoidsPokemon, BasePV, BaseAttaque, BaseDefense, BaseAttSpe, BaseDefSpe, BaseVit, Legendaire, Shiny, Mega, Giga, Fab, Evolution, Gen)
        {
            this.SurnomEquipier = SurnomEquipier;
            this.NiveauEquipier = NiveauEquipier;
        }

        public MEquipier(int IdPokemon, string CheminImgPokemon, string NomFraPokemon)
            : base(IdPokemon, CheminImgPokemon, NomFraPokemon)
        {

        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
