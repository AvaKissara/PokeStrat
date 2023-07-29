﻿using PokeStat.Modeles;
using PokeStat.Utilitaires;
using PokeStat.Vues.CrudUser;
using PokeStat.Vues;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;
using PokeStat.Repositories;
using System.Security;
using System.Windows;

namespace PokeStat.VuesModeles
{
    public class GestionUserVueModele : IVueModele<MUser, DataTable>
    {
        // Déclaration des commandes utilisées dans la classe
        public ICommand CreeCommand { get; set; }
        public ICommand AjouteCommand { get; set; }
        public ICommand ModifieCommand { get; set; }
        public ICommand MajCommand { get; set; }
        public ICommand EffaceCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        public ICommand GestionCommand { get; set; }
        public ICommand CloseCommand { get; }

        private readonly RepUser repUser;

        //Propriété de type MUser corrrespondant à l'élément actuellement sélectionné dans la liste des types.
        private MUser _ligneSelection;
        public MUser LigneSelection
        {
            get { return _ligneSelection; }
            set
            {
                if (_ligneSelection != value)
                {
                    _ligneSelection = value;
                    OnPropertyChanged(nameof(LigneSelection));
                    // indique si un élément est sélectionné ou non
                    OnPropertyChanged(nameof(IsSelectionne));
                }
            }
        }
        public bool IsSelectionne => LigneSelection != null;

        //Propriété de type booléen utilisée pour contrôler la saisie d'une version
        private bool isSaisieValide;
        public bool IsSaisieValide
        {
            get { return isSaisieValide; }
            set
            {
                isSaisieValide = value;
                OnPropertyChanged(nameof(IsSaisieValide));
            }
        }

        //Propriété de type string utilisée pour stocker un message d'erreur relatif à la saisie d'un type
        private string erreurSaisie;
        public string ErreurSaisie
        {
            get { return erreurSaisie; }
            set
            {
                erreurSaisie = value;
                OnPropertyChanged(nameof(ErreurSaisie));
            }
        }

        private DataTable dtData;

        public DataTable DtData
        {
            get { return dtData; }
            set
            {
                if (dtData != value)
                {
                    dtData = value;
                    OnPropertyChanged(nameof(DtData));
                }
            }
        }

        private string nomUser;
        public string NomUser
        {
            get { return nomUser; }
            set
            {
                nomUser = value;
                OnPropertyChanged(nameof(NomUser));
            }
        }

        private string prenomUser;
        public string PrenomUser
        {
            get { return prenomUser; }
            set
            {
                prenomUser = value;
                OnPropertyChanged(nameof(PrenomUser));
            }
        }

        private string pseudoUser;
        public string PseudoUser
        {
            get { return pseudoUser; }
            set
            {
                pseudoUser = value;
                OnPropertyChanged(nameof(PseudoUser));
            }
        }

        private string mailUser;
        public string MailUser
        {
            get { return mailUser; }
            set
            {
                mailUser = value;
                OnPropertyChanged(nameof(MailUser));
            }
        }

        private SecureString mdpUser;
        public SecureString MdpUser
        {
            get { return mdpUser; }
            set
            {
                mdpUser = value;
                OnPropertyChanged(nameof(MdpUser));
            }
        }

        private SecureString confirmMdp;
        public SecureString ConfirmMdp
        {
            get { return confirmMdp; }
            set
            {
                confirmMdp = value;
                OnPropertyChanged(nameof(ConfirmMdp));
            }
        }

        public int idUser;

        private DateTime actualise;
        public DateTime Actualise
        {
            get { return actualise; }
            set 
            { 
                actualise = value;
                OnPropertyChanged(nameof(Actualise));
            }
        }

        private DateTime cree;
        public DateTime Cree
        {
            get { return cree; }
            set
            {
                cree = value;
                OnPropertyChanged(nameof(Cree));
            }
        }

        public GestionUserVueModele() 
        {
            CreeCommand = new RelayCommand(CreeUser);
            AjouteCommand = new RelayCommand(AjouteUser);
            ModifieCommand = new RelayCommand(ModifieUser);
            EffaceCommand = new RelayCommand(EffaceUser);
            GestionCommand = new RelayCommand(GestionUser);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            CloseCommand = new RelayCommand(Close);

            repUser = new RepUser();
            List<MUser> users = repUser.GetAll();
            DtData = DataTableTool.ConvertListToDataTable(users);
        }

        private void CreeUser()
        {
            NavigationServices.NavigateToPage(new CreeUser());
        }

        private void AjouteUser() 
        {
            MDate cree = new MDate(Cree);

            MUser nouvelUser = new MUser(idUser, NomUser, PrenomUser, PseudoUser, MailUser, MdpUser, Actualise, Cree);
            List<MUser> users = repUser.GetAll();

            bool userExiste = users.Any(v => v.mailUser.Equals(nouvelUser.mailUser, StringComparison.OrdinalIgnoreCase));

            if(userExiste)
            {
                MessageBox.Show("Cet utilisateur existe déjà!");
            }

        }

        private void ModifieUser() 
        {

        }

        private void EffaceUser() 
        {

        }

        private void GestionUser() 
        {

        }

        private void AccueilPage()
        {
            // Vers la page d'accueil
            Page accueilPage = new AccueilPage();
            NavigationServices.NavigateToPage(accueilPage);
        }

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
