using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace PokemonGo
{
    public partial class Battle : Page
    {
        private Pokemon playerCurrentPokemon;
        private Pokemon opponentCurrentPokemon;

        private Player p1;
        private int[] skillcount;
        private int[] skilltime = new int[3] { 1, 20, 50 };     // Time(s) to active the Skill 0
        private int restcount;
        private int bossRestcount;
        private int restTime = 5;       // Time to rest after an attack has been made
        private DispatcherTimer skill0Timer = new DispatcherTimer();
        private DispatcherTimer skill1Timer = new DispatcherTimer();
        private DispatcherTimer skill2Timer = new DispatcherTimer();
        private DispatcherTimer restTimer = new DispatcherTimer();
        private DispatcherTimer bossRestTimer = new DispatcherTimer();
        private bool switchingPokemon;

        public Battle(Player p)
        {
            InitializeComponent();
            switchingPokemon = false;
            p1 = p;
            List<Pokemon> playerPokemon = p1.GetPokemons();
            //generateOneRandomBoss(); --todo
            setBoss(playerPokemon[1]);    // Generate and show the boss detail -- todo
            //selectThreePokemonToAttend(); -- todo
            setPlayerCurrentPokemon(playerPokemon[0]);      // Generate and show my battle pokemon detail
        }
        private void setPlayerCurrentPokemon(Pokemon pokemon)
        {
            playerCurrentPokemon = pokemon;
            playerPokemonName.Text = playerCurrentPokemon.Name;
            playerPokemonCP.Text = playerCurrentPokemon.GetCP.ToString();
            playerPokemonHP.Width = ((double)playerCurrentPokemon.GetHP / (double)playerCurrentPokemon.MaxHP) * 280;
            playerPokemonHPAfterAttack.Width = ((double)playerCurrentPokemon.GetHP / (double)playerCurrentPokemon.MaxHP) * 280;
            if (playerPokemonHPAfterAttack.Width > 60)
            {
                playerPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.LightGreen);
            }
            else if (playerPokemonHPAfterAttack.Width > 30)
            {
                playerPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                playerPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Red);
            }
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"Images/pokemon/back/" + playerCurrentPokemon.TypeName + ".gif", UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(playerPokemonImage, image);

            playerPokemonSkill0Name.Text = playerCurrentPokemon.Moveslist[0].name + " (" + playerCurrentPokemon.Moveslist[0].attackPoints.ToString() + ")";
            playerPokemonSkill1Name.Text = playerCurrentPokemon.Moveslist[1].name + " (" + playerCurrentPokemon.Moveslist[1].attackPoints.ToString() + ")";
            playerPokemonSkill2Name.Text = playerCurrentPokemon.Moveslist[2].name + " (" + playerCurrentPokemon.Moveslist[2].attackPoints.ToString() + ")";

            skillcount = skilltime;
            skill0Timer.Tick += skill0Timer_Tick;
            skill0Timer.Interval = TimeSpan.FromSeconds(1);
            skill0Timer.Start();

            skill1Timer.Tick += skill1Timer_Tick;
            skill1Timer.Interval = TimeSpan.FromSeconds(1);
            skill1Timer.Start();

            skill2Timer.Tick += skill2Timer_Tick;
            skill2Timer.Interval = TimeSpan.FromSeconds(1);
            skill2Timer.Start();

            restcount = 0;
            restTimer.Tick += restTimer_Tick;
            restTimer.Interval = TimeSpan.FromSeconds(1);
            restTimer.Start();
        }
        private void setBoss(Pokemon pokemon)
        {
            opponentCurrentPokemon = pokemon;
            opponentPokemonName.Text = opponentCurrentPokemon.Name;
            opponentPokemonCP.Text = opponentCurrentPokemon.GetCP.ToString();
            opponentPokemonHP.Width = ((double)opponentCurrentPokemon.GetHP / (double)opponentCurrentPokemon.MaxHP) * 280;
            opponentPokemonHPAfterAttack.Width = ((double)opponentCurrentPokemon.GetHP / (double)opponentCurrentPokemon.MaxHP) * 280;
            // Has bug, color the HP　bar currently not work
            if (opponentPokemonHPAfterAttack.Width > 60)
            {
                opponentPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.LightGreen);
            }
            else if (opponentPokemonHPAfterAttack.Width > 30)
            {
                opponentPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                opponentPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Red);
            }
            var bossImage = new BitmapImage();
            bossImage.BeginInit();
            bossImage.UriSource = new Uri(@"Images/pokemon/" + opponentCurrentPokemon.TypeName + ".gif", UriKind.Relative);
            bossImage.EndInit();
            ImageBehavior.SetAnimatedSource(opponentPokemonImage, bossImage);

            bossRestcount = 0;
        }
        private void restTimer_Tick(object sender, EventArgs e)
        {
            restcount++;
            restBlock.Text = restcount + "s";
            if (restcount >= 2)
            {
                opponentPokemonImageAttack.Visibility = Visibility.Collapsed;
                skillButtonGroup.Visibility = Visibility.Visible;
            }
        }
        private void skill0Timer_Tick(object sender, EventArgs e)
        {
            if (skillcount[0] > 0)
            {
                skillcount[0]--;
                timerBlock0.Text = skillcount[0] + "s";
                playerPokemonSkill0.Opacity = (skillcount[0] > skilltime[0]) ? 1 : 0.5;
            }
        }
        private void skill1Timer_Tick(object sender, EventArgs e)
        {
            if (skillcount[1] > 0)
            {
                skillcount[1]--;
                timerBlock1.Text = skillcount[1] + "s";
                playerPokemonSkill1.Opacity = (skillcount[1] > skilltime[1]) ? 1 : 0.5;
            }
        }
        private void skill2Timer_Tick(object sender, EventArgs e)
        {
            if(skillcount[2] > 0)
            {
                skillcount[2]--;
                timerBlock2.Text = skillcount[2] + "s";
                playerPokemonSkill2.Opacity = (skillcount[2] > skilltime[2]) ? 1 : 0.5;
            }
        }
        private void Attack0(object sender, RoutedEventArgs e)
        {
            ConfirmAttack(0);
        }
        private void Attack1(object sender, RoutedEventArgs e)
        {
            ConfirmAttack(1);
        }
        private void Attack2(object sender, RoutedEventArgs e)
        {
            ConfirmAttack(2);
        }
        private void ConfirmAttack(int moveID)
        {
            if (skillcount[moveID] == 0)
            {
                skillButtonGroup.Visibility = Visibility.Collapsed;
                opponentPokemonImageAttack.Visibility = Visibility.Visible;
                restcount = 0;

                opponentCurrentPokemon.Hit(playerCurrentPokemon.Moveslist[moveID].attackPoints);
                opponentPokemonCP.Text = opponentCurrentPokemon.GetCP.ToString();
                opponentPokemonHP.Width = ((double)opponentCurrentPokemon.GetHP / (double)opponentCurrentPokemon.MaxHP) * 280;
                opponentPokemonHPAfterAttack.Width = ((double)opponentCurrentPokemon.GetHP / (double)opponentCurrentPokemon.MaxHP) * 280;

                skillcount[moveID] = skilltime[moveID];
                //MessageBox.Show("Used" + opponentCurrentPokemon.Moveslist[moveID].name + " to attack with power "+ opponentCurrentPokemon.Moveslist[moveID].attackPoints.ToString()+ " !");

                if (opponentCurrentPokemon.GetHP <= 0)
                {
                    MessageBox.Show("End Game! Obtained 1000 Stardust as reward.");
                    p1.AddStardust(1000);
                    opponentCurrentPokemon.Heal();
                    Program.status = 0;
                    this.NavigationService.GoBack();
                }
            }
        }
        private void SwitchPokemon(object sender, RoutedEventArgs e)
        {
            GridChangePokemon.Visibility = Visibility.Visible;

            // Animation of Switch Box
            var sb = new Storyboard();
            var ta = new ThicknessAnimation();
            ta.BeginTime = new TimeSpan(0);
            ta.SetValue(Storyboard.TargetNameProperty, "GridChangePokemon");
            Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));
            if (!switchingPokemon)
            {
                ta.From = new Thickness(0, 0, 0, -300);
                ta.To = new Thickness(0, 0, 0, -10);
                ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            }
            else
            {
                ta.From = new Thickness(0, 0, 0, -10);
                ta.To = new Thickness(0, 0, 0, -300);
                ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            }
            switchingPokemon = !switchingPokemon;
            sb.Children.Add(ta);
            sb.Begin(this);
        }
        private void ConfirmSwitchPokemon(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developing!");
        }
        private void RunAway(object sender, RoutedEventArgs e)
        {
            Program.status = 0;
            this.NavigationService.GoBack();
        }
    }
}