using Avalonia.Controls;

namespace LOG.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎访问 23号 终端!";

    public string[] Options { get; } =
    {
        "[a]",
        "[b]",
        "[c]"
    };
    
    
    
    
}