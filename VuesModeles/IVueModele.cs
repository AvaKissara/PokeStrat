using PokeStat.Vues;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeStat.VuesModeles
{
    public interface IVueModele<TElement, TData>
    {
        // Commandes
        ICommand GestionCommand { get; set; }
        ICommand CreeCommand { get; set; }
        ICommand AjouteCommand { get; set; }
        ICommand ModifieCommand { get; set; }
        ICommand MajCommand { get; set; }
        ICommand EffaceCommand { get; set; }
        ICommand AccueilPageCommand { get; set; }
        ICommand CloseCommand { get; }

        // Propriété de sélection de ligne
        TElement LigneSelection { get; set; }

        // Propriété de DataTable
        TData DtData { get; set; }

        // Propriétés de contrôle de saisie
        bool IsSaisieValide { get; set; }
        string ErreurSaisie { get; set; }
    }
}
