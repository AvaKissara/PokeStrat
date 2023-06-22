using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.VuesModeles
{
    public class GestionVersionVueModel : INotifyPropertyChanged
    {
        private string nomVers;
        public string NomVers
        {
            get { return nomVers; }
            set
            {
                if (nomVers != value)
                {
                    nomVers = value;
                    OnPropertyChanged(nameof(NomVers));
                }
            }
        }

        public int IdVers;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
