using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpellingChunkWPF
{
    public class Alphabet
    {
        static readonly Int16 ColumnWidth = 40;
        const int hiddennumber = 2;
        readonly int level;
        static int[] values;

        public Alphabet(int Level)
        {
            level = Level;
            populateHiddenColumn();
        }

        public WrapPanel AddAlphabet()
        {
            WrapPanel canvas1 = new WrapPanel();
            for (int i = 1; i <= 26; i++)
            {
                TextBox tb = Common.GetTextBox();
                tb.Name = Common.IndexToColumn(i);
                tb.Width = ColumnWidth;
                if (ishidden(i) == false)
                {
                    tb.IsReadOnly = false;
                    tb.Text = "";
                    tb.Background = Brushes.CadetBlue;
                    tb.MaxLength = 1;
                    tb.TextChanged += (object sender, TextChangedEventArgs e) =>
                    {
                        TextBox t1 = (TextBox)sender;
                        if (t1.Text.Trim().Length > 0)
                        {
                            if (t1.Text.ToUpper() == t1.Name)
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
                    tb.Text = Common.IndexToColumn(i);
                }
                canvas1.Children.Add(tb);
            }
            return canvas1;
        }

        private void populateHiddenColumn()
        {
            int hiddencolumns = level * hiddennumber;
            values = new int[hiddencolumns];
            int newVal = 0;

            for (int i = 0; i < hiddencolumns; i++)
            {
                bool isExists = false;

                do
                {
                    newVal = Common.GetRandomNumber(1, 26);
                    isExists = values.Contains(newVal);
                } while (isExists == true); // continue loop if newVal exists in array;
                
                values[i] = newVal;
            }
        }

        private static bool ishidden(int index)
        {
            return !values.Contains(index);
        }
    }
}
