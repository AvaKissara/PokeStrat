﻿using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.User.GestionEquipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class GestionEquipeVueModele : BaseVueModele
    {
        public ICommand CreeCommand { get; set; }
        public ICommand AjouteCommand { get; set; }
        public ICommand ModifieCommand { get; set; }
        public ICommand MajCommand { get; set; }
        public ICommand EffaceCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand GestionCommand { get; set; }
        public ICommand CloseCommand { get; }

        private readonly RepEquipe repEquipe;
        private readonly ObservableCollection<MEquipe> _equipes;
        public IEnumerable<MEquipe> Equipes => _equipes;

        private int idEquipe;
        public int IdEquipe
        {
            get { return idEquipe; }
            set
            {
                idEquipe = value;
                OnPropertyChanged(nameof(IdEquipe));
            }
        }

        private string nomEquipe;
        public string NomEquipe
        {
            get { return nomEquipe; }
            set
            {
                nomEquipe = value;
                OnPropertyChanged(nameof(NomEquipe));
            }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        public GestionEquipeVueModele()
        {
            GestionCommand = new RelayCommand(GestionEquipe);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);
            repEquipe = new RepEquipe();
            _equipes = repEquipe.GetAllEquipes();
        }

        private void GestionEquipe()
        {
            NavigationServices.NavigateToPage(new GestionEquipe());
        }

        private void AccueilPage()
        {
            NavigationServices.NavigateToPage(new AccueilPage());
        }

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
