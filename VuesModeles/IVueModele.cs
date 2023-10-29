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
        // Commande pour gérer des éléments
        ICommand GestionCommand { get; set; }

        // Commande pour créer un nouvel élément
        ICommand CreeCommand { get; set; }

        // Commande pour ajouter un élément
        ICommand AjouteCommand { get; set; }

        // Commande pour modifier un élément
        ICommand ModifieCommand { get; set; }

        // Commande pour mettre à jour les données
        ICommand MajCommand { get; set; }

        // Commande pour supprimer un élément
        ICommand EffaceCommand { get; set; }

        // Commande pour naviguer vers la page d'accueil
        ICommand AccueilPageCommand { get; set; }

        // Commande pour fermer l'interface
        ICommand CloseCommand { get; }

        // Élément sélectionné, généralement dans une liste ou une grille
        TElement LigneSelection { get; set; }

        // Données associées à l'interface
        TData DtData { get; set; }

        // Indique si les saisies faites dans l'interface sont valides
        bool IsSaisieValide { get; set; }

        // Message d'erreur en cas de saisie invalide
        string ErreurSaisie { get; set; }
    }

}
