using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Repositories
{
    public interface IRepository<T> where T : class
    {
        void DbConnecter();
        void CheckConnexion();
        List<T> GetAll();
        void Add(T MModele);
        void Update(T MModele);
        void Delete(int idSuppr);
    }
}
