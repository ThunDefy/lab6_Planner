using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace lab6_Planner.Views
{
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
