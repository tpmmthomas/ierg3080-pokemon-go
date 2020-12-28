using System.Windows.Controls;

namespace PokemonGo
{
    public partial class PokemonSingleViewTiny : UserControl
    {
        public PokemonSingleViewTiny()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string PokemonName { get; set; }
        public int PokemonCP { get; set; }
    }
}
