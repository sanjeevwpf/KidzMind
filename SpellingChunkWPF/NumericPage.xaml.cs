using System;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpellingChunkWPF
{
    /// <summary>
    /// Interaction logic for NumericPage.xaml
    /// </summary>
    public partial class NumericPage : Page
    {
        int level = 1;
        static int[] values;

        public NumericPage()
        {
            InitializeComponent();
            AddHeader();
            AddTemplate();
        }

        public NumericPage(int Level)
        {
            level = Level;
            InitializeComponent();
            AddHeader();
            AddTemplate();
        }



        public Int16 GetTotalColumnNumber()
        {
            return (short)((level / App.hiddennumber) + (level % App.hiddennumber));
            //return 3;
        }

        public void AddTemplate()
        {
            short i = 1;
            while (i <= GetTotalColumnNumber())
            {
                WrapPanel wp = AddNumber(i);

                wp.Orientation = Orientation.Vertical;
                //wp.Background =  ;

                Grid.SetRow(wp, 1);
                Grid.SetColumn(wp, i);
                Grid.SetColumnSpan(wp, 10 / GetTotalColumnNumber());


                CountingGrid.Children.Add(wp);
                i++;
            }
        }

        public void AddHeader()
        {
            txtHeader.Text = "Counting (Level - " + level.ToString() + ")";
            this.Title = txtHeader.Text;
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (level <= 19)
            {
                this.NavigationService.Navigate(new NumericPage(level + 1));
            }
            else
            {
                this.NavigationService.Navigate(new MainPage());
            }

            
            
        }

        private int GetHiddenBox(Int16 ColumnNumber)
        {
            return (App.hiddennumber * (level / ColumnNumber) > 4) ? 4 : (App.hiddennumber * (level / ColumnNumber)) ;
        }

        private void populateHiddenColumn(Int16 ColumnNumber)
        {
            int hiddencolumns = GetHiddenBox(ColumnNumber);
            values = new int[hiddencolumns];
            int newVal = 0;

            for (int i = 0; i < hiddencolumns; i++)
            {
                bool isExists = false;

                do
                {
                    newVal = Common.GetRandomNumber(GetStartNumber(ColumnNumber), GetLastNumber(ColumnNumber));
                    isExists = values.Contains(newVal);
                } while (isExists == true); // continue loop if newVal exists in array;

                values[i] = newVal;
            }
        }

        private int GetStartNumber(Int16 ColumnNumber)
        {
            return GetLastNumber(ColumnNumber) - 9;
        }

        private int GetLastNumber(Int16 ColumnNumber)
        {
            return ColumnNumber * 10;
        }

        public WrapPanel AddNumber(Int16 ColumnNumber)
        {
            populateHiddenColumn(ColumnNumber);
            WrapPanel canvas1 = new WrapPanel();
            for (int i = GetStartNumber(ColumnNumber); i <= GetLastNumber(ColumnNumber); i++)
            {
                TextBox tb = Common.GetTextBox();
                tb.Name = "TEXT" + i.ToString();
                if (i > 99)
                    tb.Width = App.ColumnWidth + 10;
                else
                    tb.Width = App.ColumnWidth;
                if (ishidden(i) == false)
                {
                    tb.IsReadOnly = false;
                    tb.Text = "";
                    tb.Background = Brushes.CadetBlue;
                    tb.MaxLength = 3;
                    tb.TextChanged += (object sender, TextChangedEventArgs e) =>
                    {
                        TextBox t1 = (TextBox)sender;
                        if (t1.Text.Trim().Length > 0)
                        {
                            if (t1.Text.ToUpper() == t1.Name.Replace("TEXT", ""))
                            {
                                t1.Background = Brushes.LightGreen;
                                t1.Foreground = Brushes.Black;
                                SystemSounds.Hand.Play();
                                //SoundPlayer wowSound = new SoundPlayer(@"soundEffect/Wow.wav");
                                //wowSound.PlaySync();
                                //System.Windows.MessageBox.Show("Correct!!!");
                            }
                            else
                            {
                                t1.Background = Brushes.Red;
                                t1.Foreground = Brushes.GreenYellow;
                                //SoundPlayer wowSound = new SoundPlayer(@"SoundEffects/ding.wav");
                                //wowSound.PlaySync();
                                //SystemSounds.Exclamation.Play();
                                //System.Windows.MessageBox.Show("Try again");
                            }
                        }
                    };
                }
                else
                {
                    tb.IsReadOnly = true;
                    tb.Text = i.ToString();
                }
                canvas1.Children.Add(tb);
            }
            return canvas1;
        }

        private static bool ishidden(int index)
        {
            return !values.Contains(index);
        }
    }
}
