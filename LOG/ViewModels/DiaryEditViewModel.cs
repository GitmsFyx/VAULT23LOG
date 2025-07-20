using Avalonia.Controls;

namespace LOG.ViewModels;

public class DiaryEditViewModel :ViewModelBase
{
    public string Greeting { get; } = "日志编辑中...";
    
    private Button _selectedOption;
    public Button SelectedOption
    {
        get=>_selectedOption;
        set=>SetProperty(ref _selectedOption, value);
    }
    public Button[] Options { get; } =
    {
        new Button(){Content = "[添加日志]"},
        new Button(){Content = "[1.]"},
        new Button(){Content = "[2.]"},
        new Button(){Content = "[3.]"}
        ,
    };
}