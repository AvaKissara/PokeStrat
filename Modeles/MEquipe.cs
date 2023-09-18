﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public class MEquipe
    {
        public int idEquipe { get; set; }
        public string nomEquipe { get; set; }
        public int user { get; set; }
        public MDate dateMatch { get; }
        public ObservableCollection<MEquipier> equipiers { get; set; }

        public MEquipe(int IdEquipe, string NomEquipe, int User, MDate DateMatch)
        {
            this.idEquipe = IdEquipe;
            this.nomEquipe = NomEquipe;
            this.user = User;
            this.dateMatch = DateMatch;
            this.equipiers = new ObservableCollection<MEquipier>();
        }
    }
}
