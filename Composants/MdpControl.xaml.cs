using PokeStat.Utilitaires;
using PokeStat.VuesModeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokeStat.Composants
{
    /// <summary>
    /// Logique d'interaction pour MdpControl.xaml
    /// </summary>
    public partial class MdpControl : UserControl
    {
        private bool mdpChange;
        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(MdpControl),
                 new FrameworkPropertyMetadata(new SecureString(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MdpControl passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public MdpControl()
        {
            InitializeComponent();
            passwordBox.GotFocus += PasswordBox_Focus;
        }

        private void PasswordBox_Focus(object sender, RoutedEventArgs e)
        {
            passwordBox.Clear();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            mdpChange = true;
            Password = passwordBox.SecurePassword;
            mdpChange = false;
        }

        private void UpdatePassword()
        {
            if (!mdpChange)
            {
                string enteredPassword = SecureStringToString(Password);

                // Utiliser PasswordManager pour hacher le mot de passe
                (string hash, string salt) = PasswordManager.HashPassword(enteredPassword);

                // Mettre à jour le champ Password avec le mot de passe haché
                passwordBox.Password = hash;
            }
        }

        private string SecureStringToString(SecureString secureString)
        {
            if (secureString == null)
            {
                return string.Empty;
            }

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                char[] chars = new char[secureString.Length];
                for (int i = 0; i < secureString.Length; i++)
                {
                    chars[i] = (char)System.Runtime.InteropServices.Marshal.ReadByte(ptr, i * 2);
                }

                return new string(chars);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }

    }
}

