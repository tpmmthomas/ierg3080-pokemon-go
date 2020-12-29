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
        private Player p1;
        private BattleGym battleGym;
        private int restTime = 3;       // Time to rest after an attack has been made
        private bool switchingPokemon;

        private int restcount;
        private DispatcherTimer restTimer = new DispatcherTimer();

        public Battle(Player p)
        {
            InitializeComponent();

            switchingPokemon = false;
            p1 = p;
            List<Pokemon> playerPokemon = p1.GetPokemons();
            battleGym = new BattleGym(playerPokemon[0], playerPokemon[1], Win, Lose); //generateOneRandomBoss(); --TODO

            setBoss();
            usePokemon();

            restcount = 0;
            restTimer.Tick += restTimer_Tick;
            restTimer.Interval = TimeSpan.FromSeconds(1);
            restTimer.Start();
        }
        private void setBoss()
        {
            Pokemon op = battleGym.GetOpponentPokemon;
            opName.Text = op.Name;
            opCP.Text = op.GetCP.ToString();
            opHP.Width = ((double)op.GetHP / (double)op.MaxHP) * 280;
            opHPAfterAttack.Width = ((double)op.GetHP / (double)op.MaxHP) * 280;

            var opBitImage = new BitmapImage();
            opBitImage.BeginInit();
            opBitImage.UriSource = new Uri(@"Images/pokemon/" + op.TypeName + ".gif", UriKind.Relative);
            opBitImage.EndInit();
            ImageBehavior.SetAnimatedSource(opImage, opBitImage);
        }
        private void usePokemon()
        {
            Pokemon pp = battleGym.GetPlayerPokemon;
            ppName.Text = pp.Name;
            ppCP.Text = pp.GetCP.ToString();
            ppHP.Width = ((double)pp.GetHP / (double)pp.MaxHP) * 280;
            ppHPAfterAttack.Width = ((double)pp.GetHP / (double)pp.MaxHP) * 280;

            var ppBitimage = new BitmapImage();
            ppBitimage.BeginInit();
            ppBitimage.UriSource = new Uri(@"Images/pokemon/back/" + pp.TypeName + ".gif", UriKind.Relative);
            ppBitimage.EndInit();
            ImageBehavior.SetAnimatedSource(ppImage, ppBitimage);

            InitializeAttackButton(0, ppSkill0Name, ppSkill0, timerBlock0);
            InitializeAttackButton(1, ppSkill1Name, ppSkill1, timerBlock1);
            InitializeAttackButton(2, ppSkill2Name, ppSkill2, timerBlock2);
        }
        private void restTimer_Tick(object sender, EventArgs e)
        {
            restcount++;
            restBlock.Text = restcount + "s";
            if (restcount >= restTime)
            {
                opponentPokemonImageAttack.Visibility = Visibility.Collapsed;
                skillButtonGroup.Visibility = Visibility.Visible;
            }
        }
        private void InitializeAttackButton(int MoveID, TextBlock skillNameBlock, Button skillButton, TextBlock skillCapicityBlock)
        {
            Pokemon pp = battleGym.GetPlayerPokemon;
            skillNameBlock.Text = pp.Moveslist[MoveID].name + " (" + pp.Moveslist[MoveID].attackPoints.ToString() + ")";
            skillButton.Opacity = battleGym.GetSkillTime[MoveID] > 0 ? 1 : 0.5;
            skillCapicityBlock.Text = battleGym.GetSkillTime[MoveID] + " left";
        }
        private void Attack0(object sender, RoutedEventArgs e)
        {
            ConfirmAttack(0, timerBlock0, ppSkill0);
        }
        private void Attack1(object sender, RoutedEventArgs e)
        {
            ConfirmAttack(1, timerBlock1, ppSkill1);
        }
        private void Attack2(object sender, RoutedEventArgs e)
        {
            ConfirmAttack(2, timerBlock2, ppSkill2);
        }
        private void ConfirmAttack(int moveID, TextBlock skillTextBlock, Button skillButton)
        {
            if (battleGym.GetSkillTime[moveID] > 0)
            {
                skillButtonGroup.Visibility = Visibility.Collapsed;
                opponentPokemonImageAttack.Visibility = Visibility.Visible;
                restcount = 0;
                if (battleGym.PlayerMove(moveID))
                {
                    MessageBox.Show("Critical attack!");
                }
                opCP.Text = battleGym.GetOpponentPokemon.GetCP.ToString();
                opHP.Width = 280 * (double) battleGym.GetOpponentPokemon.GetHP / battleGym.GetOpponentPokemon.MaxHP;
                opHPAfterAttack.Width = 280 * (double) battleGym.GetOpponentPokemon.GetHP / battleGym.GetOpponentPokemon.MaxHP;
                skillTextBlock.Text = battleGym.GetSkillTime[moveID] + " left";
                skillButton.Opacity = (battleGym.GetSkillTime[moveID] > 0) ? 1 : 0.5;
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
            }
            else
            {
                ta.From = new Thickness(0, 0, 0, -10);
                ta.To = new Thickness(0, 0, 0, -300);
            }
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            switchingPokemon = !switchingPokemon;
            sb.Children.Add(ta);
            sb.Begin(this);
        }
        private void ConfirmSwitchPokemon(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developing!");
        }
        public void Win(Pokemon _PlayerPokemon, Pokemon _OpponentPokemon)
        {
            MessageBox.Show("End Game! Obtained 1000 Stardust as reward.");
            p1.AddStardust(1000);
            battleGym.GetOpponentPokemon.Heal();
            Program.status = 0;
            this.NavigationService.GoBack();
        }
        public void Lose(Pokemon _PlayerPokemon, Pokemon _OpponentPokemon)
        {
            MessageBox.Show("You lose the game, try to train your pokemon.");
            Program.status = 0;
            this.NavigationService.GoBack();
        }
        private void RunAway(object sender, RoutedEventArgs e)
        {
            Program.status = 0;
            this.NavigationService.GoBack();
        }
    }
}