using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class EquipeTreeViewNode : INotifyPropertyChanged, IDisposable
    {
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
        public MEquipe Equipe { get; }
        public ObservableCollection<EquipierTreeViewNode> Equipiers { get; } = new ObservableCollection<EquipierTreeViewNode>();

        public EquipeTreeViewNode(MEquipe Equipe)
        {
            this.Equipe = Equipe;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }
    }
}
