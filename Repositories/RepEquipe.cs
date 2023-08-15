using PokeStat.Utilitaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public class RepEquipe
    {
        public BddTool bddTool;

        public RepEquipe()
        {
            bddTool = new BddTool();

            try
            {
                this.bddTool.DbConnecter();
            }
            catch (Exception ex)
            {
                // Gestion de l'exception
                Console.WriteLine("Erreur lors de la connexion à la base de données : " + ex.Message);
            }

        }
    }
}
