using PokeStat.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PokeStat.Modeles
{
    public abstract class MPersonne
    {
        public int idPersonne { get; set; }
        public string nomPersonne { get; set; }
        public string prenomPersonne { get; set; }
        public string pseudoPersonne { get; set; }
        public string mailPersonne { get; set; }
        public SecureString mdpPersonne { get; set; }
        public DateTime actualise { get; set; }
        public MDate cree { get; set; }

        public MPersonne(int idPersonne, string nomPersonne, string prenomPersonne, string pseudoPersonne, string mailPersonne, SecureString mdpPersonne, DateTime actualise, MDate cree)
        {
            this.idPersonne = idPersonne;
            this.nomPersonne = nomPersonne;
            this.prenomPersonne = prenomPersonne;
            this.pseudoPersonne = pseudoPersonne;
            this.mailPersonne = mailPersonne;
            this.mdpPersonne = mdpPersonne;
            this.actualise = actualise;
            this.cree = cree;
        }

        public MPersonne(int idPersonne, string pseudoPersonne)
        {
            this.idPersonne = idPersonne;
            this.pseudoPersonne = pseudoPersonne;
        }

        public MPersonne() { }

        public string ToInsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return null;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
