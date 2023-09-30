using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class EquipeTreeViewNode
    {
        public MEquipe Equipe { get; }
        public ObservableCollection<EquipierTreeViewNode> Equipiers { get; } = new ObservableCollection<EquipierTreeViewNode>();

        public EquipeTreeViewNode(MEquipe equipe)
        {
            Equipe = equipe;
        }
    }

}
