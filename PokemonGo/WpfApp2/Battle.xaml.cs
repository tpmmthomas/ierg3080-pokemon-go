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

        private int[] skillcount;
        private DispatcherTimer skill1Timer = new DispatcherTimer();
        private DispatcherTimer skill2Timer = new DispatcherTimer();
        private DispatcherTimer skill3Timer = new DispatcherTimer();

        public Battle(Player p1)
        {
            InitializeComponent();
            List<Pokemon> playerPokemon = p1.GetPokemons();

            // Generate and show the boss detail -- todo
            opponentCurrentPokemon = playerPokemon[1];
            opponentPokemonName.Text = opponentCurrentPokemon.Name;
            opponentPokemonCP.Text = opponentCurrentPokemon.GetCP.ToString();
            opponentPokemonHP.Width = ((double)opponentCurrentPokemon.GetHP / (double) opponentCurrentPokemon.MaxHP) * 280;
            opponentPokemonHPAfterAttack.Width = ((double) opponentCurrentPokemon.GetHP / (double) opponentCurrentPokemon.MaxHP) * 280;
            if(opponentPokemonHPAfterAttack.Width > 60){
                opponentPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.LightGreen);
            } else if (opponentPokemonHPAfterAttack.Width > 30){
                opponentPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Yellow);
            } else {
                opponentPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Red);
            }
            var bossImage = new BitmapImage();
            bossImage.BeginInit();
            bossImage.UriSource = new Uri(@"Images/pokemon/" + opponentCurrentPokemon.TypeName + ".gif", UriKind.Relative);
            bossImage.EndInit();
            ImageBehavior.SetAnimatedSource(opponentPokemonImage, bossImage);

            // Select three pokemon to attend the gym battle -- todo

            // Generate and show my battle pokemon detail
            playerCurrentPokemon = playerPokemon[0];
            playerPokemonName.Text = playerCurrentPokemon.Name;
            playerPokemonCP.Text = playerCurrentPokemon.GetCP.ToString();
            playerPokemonHP.Width = ((double) playerCurrentPokemon.GetHP / (double) playerCurrentPokemon.MaxHP) * 280;
            playerPokemonHPAfterAttack.Width = ((double) playerCurrentPokemon.GetHP / (double) playerCurrentPokemon.MaxHP) * 280;
            if (playerPokemonHPAfterAttack.Width > 60){
                playerPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.LightGreen);
            }else if (playerPokemonHPAfterAttack.Width > 30){
                playerPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Yellow);
            }else{
                playerPokemonHPAfterAttack.Fill = new SolidColorBrush(Colors.Red);
            }
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"Images/pokemon/back/" + playerCurrentPokemon.TypeName + ".gif", UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(playerPokemonImage, image);

            // Start battle

            skillcount = new int[3] { 0, 0, 0 };
            skill1Timer.Tick += skill1Timer_Tick;
            skill1Timer.Interval = TimeSpan.FromSeconds(1);
            skill1Timer.Start();

            skill2Timer.Tick += skill2Timer_Tick;
            skill2Timer.Interval = TimeSpan.FromSeconds(1);
            skill2Timer.Start();

            skill3Timer.Tick += skill3Timer_Tick;
            skill3Timer.Interval = TimeSpan.FromSeconds(1);
            skill3Timer.Start();

            playerPokemonSkill1Name.Text = opponentCurrentPokemon.Moveslist[0].name + "(" + opponentCurrentPokemon.Moveslist[0].attackPoints.ToString() + ")";
            playerPokemonSkill2Name.Text = opponentCurrentPokemon.Moveslist[1].name + "(" + opponentCurrentPokemon.Moveslist[0].attackPoints.ToString() + ")";
            playerPokemonSkill3Name.Text = opponentCurrentPokemon.Moveslist[2].name + "(" + opponentCurrentPokemon.Moveslist[0].attackPoints.ToString() + ")";

            GridChangePokemon.Visibility = Visibility.Collapsed;
        }

        private void skill1Timer_Tick(object sender, EventArgs e)
        {
            skillcount[0]++;
            timerBlock1.Text = skillcount[0] + "s";
            playerPokemonSkill1.Opacity = (skillcount[0] > 1) ? 1 : 0.5;
        }
        private void skill2Timer_Tick(object sender, EventArgs e)
        {
            skillcount[1]++;
            timerBlock2.Text = skillcount[1] + "s";
            playerPokemonSkill2.Opacity = (skillcount[1] > 10) ? 1 : 0.5;
        }
        private void skill3Timer_Tick(object sender, EventArgs e)
        {
            skillcount[2]++;
            timerBlock3.Text = skillcount[2] + "s";
            playerPokemonSkill3.Opacity = (skillcount[2] > 30) ? 1 : 0.5;
        }
        private void ClickAttack1(object sender, RoutedEventArgs e)
        {
            MakeAttackWithSkill(0);
        }
        private void ClickAttack2(object sender, RoutedEventArgs e)
        {
            MakeAttackWithSkill(1);
        }
        private void ClickAttack3(object sender, RoutedEventArgs e)
        {
            MakeAttackWithSkill(2);
        }
        private void MakeAttackWithSkill(int moveID)
        {
            opponentCurrentPokemon.Hit(playerCurrentPokemon.Moveslist[moveID].attackPoints);
            opponentPokemonCP.Text = opponentCurrentPokemon.GetCP.ToString();
            opponentPokemonHP.Width = ((double)opponentCurrentPokemon.GetHP / (double)opponentCurrentPokemon.MaxHP) * 280;
            opponentPokemonHPAfterAttack.Width = ((double)opponentCurrentPokemon.GetHP / (double)opponentCurrentPokemon.MaxHP) * 280;

            MessageBox.Show(opponentCurrentPokemon.Moveslist[moveID].name);
            MessageBox.Show(opponentCurrentPokemon.Moveslist[moveID].attackPoints.ToString());
            skillcount[moveID] = 0;
            MessageBox.Show("Attack "+ skillcount[moveID]+ "!");
        }





        private void AttackMyTurn()
        {

        }
        private void ButtonConfirmSwitchPokemon(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developing!");
        }
        private void ButtonSwitchPokemon(object sender, RoutedEventArgs e)
        {
            GridChangePokemon.Visibility = Visibility.Visible;

            // Animation to show the pokemon list
            var sb = new Storyboard();
            var ta = new ThicknessAnimation();
            ta.BeginTime = new TimeSpan(0);
            ta.SetValue(Storyboard.TargetNameProperty, "GridChangePokemon");
            Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

            ta.From = new Thickness(0, 0, 0, -300);
            ta.To = new Thickness(0, 0, 0, -10);
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            sb.Children.Add(ta);
            sb.Begin(this);
        }
        private void ButtonCloseSwitchPokemon(object sender, RoutedEventArgs e)
        {
            // Animation to hide the pokemon list
            var sb = new Storyboard();
            var ta = new ThicknessAnimation();
            ta.BeginTime = new TimeSpan(0);
            ta.SetValue(Storyboard.TargetNameProperty, "GridChangePokemon");
            Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

            ta.From = new Thickness(0, 0, 0, -10);
            ta.To = new Thickness(0, 0, 0, -300);
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            sb.Children.Add(ta);
            sb.Begin(this);
        }
        private void ButtonRunAway(object sender, RoutedEventArgs e)
        {
            Program.status = 0;
            this.NavigationService.GoBack();
        }
    }
}