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
        ICommand GestionCommand { get; set; }
        ICommand CreeCommand { get; set; }
        ICommand AjouteCommand { get; set; }
        ICommand ModifieCommand { get; set; }
        ICommand MajCommand { get; set; }
        ICommand EffaceCommand { get; set; }
        ICommand AccueilPageCommand { get; set; }
        ICommand CloseCommand { get; }

        TElement LigneSelection { get; set; }

        TData DtData { get; set; }

        bool IsSaisieValide { get; set; }
        string ErreurSaisie { get; set; }
    }
}
