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
        private BattleGym battleGym;
        private int restTime = 3;       // Time to rest after an attack has been made
        private bool switchingPokemon;

        private int restcount;
        private int[] skillcount;
        private int[] skilltime = new int[3] { 1, 20, 50 };     // Time(s) to active the Skill 0,1,2
        private DispatcherTimer skill0Timer = new DispatcherTimer();
        private DispatcherTimer skill1Timer = new DispatcherTimer();
        private DispatcherTimer skill2Timer = new DispatcherTimer();
        private DispatcherTimer restTimer = new DispatcherTimer();

        private int bossRestcount;
        private int[] bossSkillcount;
        private int[] bossSkilltime = new int[3] { 2, 25, 55 };     // Time(s) for boss to active the Skill 0,1,2, to fair, boss should need more time to active the skill
        private DispatcherTimer bossSkill0Timer = new DispatcherTimer();
        private DispatcherTimer bossSkill1Timer = new DispatcherTimer();
        private DispatcherTimer bossSkill2Timer = new DispatcherTimer();
        private DispatcherTimer bossRestTimer = new DispatcherTimer();

        public Battle(Player p)
        {
            InitializeComponent();
            switchingPokemon = false;
            p1 = p;
            List<Pokemon> playerPokemon = p1.GetPokemons();
            battleGym = new BattleGym(playerPokemon[0], playerPokemon[1], Win, Lose); //generateOneRandomBoss(); --TODO
            setBoss();
            usePokemon();
        }
        private void setBoss()
        {
            Pokemon op = battleGym.GetOpponentPokemon;
            opponentPokemonName.Text = op.Name;
            opponentPokemonCP.Text = op.GetCP.ToString();
            opponentPokemonHP.Width = ((double)op.GetHP / (double)op.MaxHP) * 280;
            opponentPokemonHPAfterAttack.Width = ((double)op.GetHP / (double)op.MaxHP) * 280;

            var opImage = new BitmapImage();
            opImage.BeginInit();
            opImage.UriSource = new Uri(@"Images/pokemon/" + op.TypeName + ".gif", UriKind.Relative);
            opImage.EndInit();
            ImageBehavior.SetAnimatedSource(opponentPokemonImage, opImage);

            skillcount = new int[3] { bossSkilltime[0], bossSkilltime[1], bossSkilltime[2] };
            bossSkill0Timer.Tick += skill0Timer_Tick;
            bossSkill0Timer.Interval = TimeSpan.FromSeconds(1);
            bossSkill0Timer.Start();

            bossSkill1Timer.Tick += skill1Timer_Tick;
            bossSkill1Timer.Interval = TimeSpan.FromSeconds(1);
            bossSkill1Timer.Start();

            bossSkill2Timer.Tick += skill2Timer_Tick;
            bossSkill2Timer.Interval = TimeSpan.FromSeconds(1);
            bossSkill2Timer.Start();

            bossRestcount = 0;
            bossRestTimer.Tick += restTimer_Tick;
            bossRestTimer.Interval = TimeSpan.FromSeconds(1);
            bossRestTimer.Start();
        }
        private void usePokemon()
        {
            playerCurrentPokemon = battleGym.GetPlayerPokemon;
            playerPokemonName.Text = playerCurrentPokemon.Name;
            playerPokemonCP.Text = playerCurrentPokemon.GetCP.ToString();
            playerPokemonHP.Width = ((double)playerCurrentPokemon.GetHP / (double)playerCurrentPokemon.MaxHP) * 280;
            playerPokemonHPAfterAttack.Width = ((double)playerCurrentPokemon.GetHP / (double)playerCurrentPokemon.MaxHP) * 280;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"Images/pokemon/back/" + playerCurrentPokemon.TypeName + ".gif", UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(playerPokemonImage, image);

            playerPokemonSkill0Name.Text = playerCurrentPokemon.Moveslist[0].name + " (" + playerCurrentPokemon.Moveslist[0].attackPoints.ToString() + ")";
            playerPokemonSkill1Name.Text = playerCurrentPokemon.Moveslist[1].name + " (" + playerCurrentPokemon.Moveslist[1].attackPoints.ToString() + ")";
            playerPokemonSkill2Name.Text = playerCurrentPokemon.Moveslist[2].name + " (" + playerCurrentPokemon.Moveslist[2].attackPoints.ToString() + ")";

            skillcount = new int[3] { skilltime[0], skilltime[1], skilltime[2] };
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
        private void skill0Timer_Tick(object sender, EventArgs e)
        {
            if (skillcount[0] > 0)
            {
                skillcount[0]--;
                timerBlock0.Text = skillcount[0] + "s";
                playerPokemonSkill0.Opacity = 0.5;
            }
            else
            {
                playerPokemonSkill0.Opacity = 1;
            }
        }
        private void skill1Timer_Tick(object sender, EventArgs e)
        {
            if (skillcount[1] > 0)
            {
                skillcount[1]--;
                timerBlock1.Text = skillcount[1] + "s";
                playerPokemonSkill1.Opacity = 0.5;
            }
            else
            {
                playerPokemonSkill1.Opacity = 1;
            }
        }
        private void skill2Timer_Tick(object sender, EventArgs e)
        {
            if(skillcount[2] > 0)
            {
                skillcount[2]--;
                timerBlock2.Text = skillcount[2] + "s";
                playerPokemonSkill2.Opacity = 0.5;
            }
            else
            {
                playerPokemonSkill2.Opacity = 1;
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
            if (skillcount[moveID] == 0) // The skill is actived(allowed) to use after count down to zero
            {
                skillButtonGroup.Visibility = Visibility.Collapsed;
                opponentPokemonImageAttack.Visibility = Visibility.Visible;
                restcount = 0;
                if (battleGym.PlayerMove(moveID))
                {
                    MessageBox.Show("Critical attack!");
                }
                opponentPokemonCP.Text = battleGym.GetOpponentPokemon.GetCP.ToString();
                opponentPokemonHP.Width = 280 * (double) battleGym.GetOpponentPokemon.GetHP / battleGym.GetOpponentPokemon.MaxHP;
                opponentPokemonHPAfterAttack.Width = 280 * (double) battleGym.GetOpponentPokemon.GetHP / battleGym.GetOpponentPokemon.MaxHP;

                skillcount[moveID] = skilltime[moveID];
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
            NavigationService.GoBack();
        }
        public void Lose(Pokemon _PlayerPokemon, Pokemon _OpponentPokemon)
        {
            MessageBox.Show("You lose the game, try to train your pokemon.");
            Program.status = 0;
            NavigationService.GoBack();
        }
        private void RunAway(object sender, RoutedEventArgs e)
        {
            Program.status = 0;
            this.NavigationService.GoBack();
        }
    }
}