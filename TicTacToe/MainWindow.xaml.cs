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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarkType[] mResults;

        private bool mPlayer1Turn;

        private bool mGameEnded;

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            // create empty array based on free cells
            mResults = new MarkType[9];

            for(var i = 0; i<mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            // player 1 starts the game
            mPlayer1Turn = true;

            // button controller
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // restart game after completed
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            var button = (Button)sender;

            // get button position
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            // do nothing if cell has value
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            if (mPlayer1Turn)
            {
                mResults[index] = MarkType.Cross;
                button.Content = "X";
                button.Foreground = Brushes.Red;

                //stop player 1 turn
                mPlayer1Turn = false;
            }
            else
            {
                mResults[index] = MarkType.Nought;
                button.Content = "0";

                //player 1 turn
                mPlayer1Turn = true;
            }

            CheckWinner();
        }

        private void CheckWinner()
        {
            #region Horizontal win condition
            //win condition 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                button0_0.Background = button1_0.Background = button2_0.Background = Brushes.Green;
            }
            //win condition 2
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                button0_1.Background = button1_1.Background = button2_1.Background = Brushes.Green;
            }
            //win condition 3
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                button0_2.Background = button1_2.Background = button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical win condition
            //win condition 4
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                button0_0.Background = button0_1.Background = button0_2.Background = Brushes.Green;
            }
            //win condition 5
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                button1_0.Background = button1_1.Background = button1_2.Background = Brushes.Green;
            }
            //win condition 6
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                button2_0.Background = button2_1.Background = button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal win condition
            //win condition 7
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                button0_0.Background = button1_1.Background = button2_2.Background = Brushes.Green;
            }
            //win condition 8
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                button2_0.Background = button1_1.Background = button0_2.Background = Brushes.Green;
            }

            #endregion

            #region Nobody won
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Gray;
                });
            }
            #endregion
        }
    }
}
