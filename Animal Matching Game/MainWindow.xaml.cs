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

namespace Animal_Matching_Game
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = "Finished in " + timeTextBlock.Text + ". Play Again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()  // creates a list of emojis
            { 
                "🐉", "🐉",
                "🐍", "🐍",
                "🦌", "🦌",
                "🦙", "🦙",
                "🐄", "🐄",
                "🐐", "🐐",
                "🐁", "🐁",
                "🐇", "🐇",
            };

            Random random = new Random(); // creates a random number generator

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())  //goes through each textblock in the grid
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);            //creates a random number within the list count
                    string nextEmoji = animalEmoji[index];          //selects the emoji from the list at the index of the random number 
                    textBlock.Text = nextEmoji;                     // replaces the textblock text with the randomly indexed emoji
                    animalEmoji.RemoveAt(index);    // removes the emoji from the list.
                }
            
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;


        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) // when TextBlock is clicekd
        {
            TextBlock textBlock = sender as TextBlock;

            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)  //if current TextBlcok content matches the last
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;

            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
            
        }
    }
}
