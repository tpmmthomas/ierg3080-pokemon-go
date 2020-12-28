using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PokemonGo
{
    public static class WindowNavigation //code copied from https://stackoverflow.com/questions/30860276/return-to-previous-window-on-wpf
    {
        private static readonly Stack<Window> NavigationStack = new Stack<Window>();
        static WindowNavigation()
        {
            NavigationStack.Push(Application.Current.MainWindow);
        }
        public static void NavigateTo(Window win)
        {
            if(! (win is Navigation))
            {
                Program.Status = 1;
                Console.WriteLine(1);
            }
            else
            {
                Program.Status = 0;
                Console.WriteLine(0);
            }
            if (NavigationStack.Count > 0)
                NavigationStack.Peek().Hide();
            NavigationStack.Push(win);
            win.Show();
        }
        public static bool NavigateBack()
        {
            if (NavigationStack.Count <= 1)
                return false;
            NavigationStack.Pop().Hide();
            if (!(NavigationStack.Peek() is Navigation))
            {
                Program.Status = 1;
                Console.WriteLine(1);
            }
            else
            {
                Program.Status = 0;
                Console.WriteLine(0);
            }
            NavigationStack.Peek().Show();
            return true;
        }
        public static bool CanNavigateBack()
        {
            return NavigationStack.Count > 1;
        }
    }
}
