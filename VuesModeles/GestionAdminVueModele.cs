using PokeStat.Modeles;
using PokeStat.Repositories;
using PokeStat.Utilitaires;
using PokeStat.Vues;
using PokeStat.Vues.CrudUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public class GestionAdminVueModele : BaseVueModele
    {
        public ICommand AjouteCommand { get; set; }
        public ICommand AccueilPageCommand { get; set; }
        private readonly RepAdmin repAdmin;
        private readonly RepDate repDate;

        private string nomAdmin;
        public string NomAdmin
        {
            get { return nomAdmin; }
            set
            {
                nomAdmin = value;
                OnPropertyChanged(nameof(NomAdmin));
            }
        }

        private string prenomAdmin;
        public string PrenomAdmin
        {
            get { return prenomAdmin; }
            set
            {
                prenomAdmin = value;
                OnPropertyChanged(nameof(PrenomAdmin));
            }
        }

        private string pseudoAdmin;
        public string PseudoAdmin
        {
            get { return pseudoAdmin; }
            set
            {
                pseudoAdmin = value;
                OnPropertyChanged(nameof(PseudoAdmin));
            }
        }

        private string mailAdmin;
        public string MailAdmin
        {
            get { return mailAdmin; }
            set
            {
                mailAdmin = value;
                OnPropertyChanged(nameof(MailAdmin));
            }
        }

        private SecureString mdpAdmin = new SecureString();
        public SecureString MdpAdmin
        {
            get { return mdpAdmin; }
            set
            {
                mdpAdmin = value;
                OnPropertyChanged(nameof(MdpAdmin));
            }
        }

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

        public int idAdmin;


        public GestionAdminVueModele()
        {
            AjouteCommand = new RelayCommand(AjouteAdmin);
            AccueilPageCommand = new RelayCommand(AccueilPage);
            repAdmin = new RepAdmin();
            repDate = new RepDate();
        }

        public void AjouteAdmin()
        {
            Cree = DateTime.Now;
            MDate cree = new MDate(Cree);
            MAdmin nouvelAdmin = new MAdmin(idAdmin, NomAdmin, PrenomAdmin, PseudoAdmin, MailAdmin, MdpAdmin, Actualise, cree);
            List<MAdmin> Admins = repAdmin.GetAll();

            bool AdminExiste = Admins.Any(u => u.MailPersonne.Equals(nouvelAdmin.MailPersonne, StringComparison.OrdinalIgnoreCase));

            if (AdminExiste)
            {
                MessageBox.Show("Cet admin existe déjà!");
            }
            else
            {
                repDate.Add(cree);
                repAdmin.Add(nouvelAdmin);
                MessageBox.Show("L'inscription est réussie !");
            }          
        }
        private void AccueilPage()
        {
            NavigationServices.NavigateToPage(new AccueilPage());
        }
    }
}
