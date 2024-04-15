using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EncontreParesGame
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Stopwatch stopwatch = new Stopwatch();
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeTextBlock.Text = stopwatch.Elapsed.ToString("ss\\.f");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text += " - Jogar Novamente?";
            }
        }

        private void SetUpGame()
        {
            List<string> emojis = new List<string>()
            {
                "🍙","🍙",
                "🤠","🤠",
                "👾","👾",
                "💀","💀",
                "😺","😺",
                "🦉","🦉",
                "👁️","👁️",
                "⚠️","⚠️",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(emojis.Count);
                    string nextEmoji = emojis[index];
                    textBlock.Text = nextEmoji;
                    emojis.RemoveAt(index);
                    textBlock.Visibility = Visibility.Visible;
                }
            }

            stopwatch.Reset();
            stopwatch.Start();
            matchesFound = 0;
            timer.Start();
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
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
