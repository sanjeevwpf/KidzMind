using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SpellingChunkWPF
{
    /// <summary>
    /// Interaction logic for AlphabetPage.xaml
    /// </summary>
    public partial class AlphabetPage : Page
    {
        int level = 1;

        public AlphabetPage()
        {
            InitializeComponent();

            AddHeader();
            AddTemplate();
        }

        public AlphabetPage(int Level)
        {
            InitializeComponent();
            level = Level;
            AddHeader();
            AddTemplate();
        }

        public void AddTemplate()
        {
            Alphabet obj = new Alphabet(level);
            WrapPanel wp = obj.AddAlphabet();

            wp.Orientation = Orientation.Horizontal;
            //wp.Background =  ;

            Grid.SetRow(wp, 1);
            Grid.SetColumnSpan(wp, 4);


            MainGrid.Children.Add(wp);
        }

        public void AddHeader()
        {
            txtHeader.Text = "Aplhabet (Level - " + level.ToString() + ")";
            this.Title = txtHeader.Text;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (level < 12)
            {
                this.NavigationService.Navigate(new AlphabetPage(level + 1));
            }
            else
            {
                this.NavigationService.Navigate(new MainPage());
            }
        }
    }
}
