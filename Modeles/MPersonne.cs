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
        public int IdPersonne { get; set; }
        public string NomPersonne { get; set; }
        public string PrenomPersonne { get; set; }
        public string PseudoPersonne { get; set; }
        public string MailPersonne { get; set; }
        public SecureString MdpPersonne { get; set; }
        public DateTime Actualise { get; set; }
        public MDate Cree { get; set; }

        public MPersonne(int idPersonne, string nomPersonne, string prenomPersonne, string pseudoPersonne, string mailPersonne, SecureString mdpPersonne, DateTime actualise, MDate cree)
        {
            IdPersonne = idPersonne;
            NomPersonne = nomPersonne;
            PrenomPersonne = prenomPersonne;
            PseudoPersonne = pseudoPersonne;
            MailPersonne = mailPersonne;
            MdpPersonne = mdpPersonne;
            Actualise = actualise;
            Cree = cree;
        }

        public MPersonne(int idPersonne, string mailPersonne)
        {
            IdPersonne = idPersonne;
            MailPersonne = MailPersonne;
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
