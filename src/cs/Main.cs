using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Control
{
    public override async void _Ready()
    {
        _errorLabel ??= GetNode<Label>("Error");
        var logger = new GameLogger();
        OS.AddLogger(logger);
        
        logger.Connect(
            GameLogger.SignalName.ErrorCaught,
            Callable.From<string>(ShowError),
            (uint)ConnectFlags.Deferred);

        await Task.Delay(1000);

        throw new NullReferenceException("Test exception from Main");
        
    }
    private Label _errorLabel;
    private void ShowError(string errorMessage)
    {
        _errorLabel.Text = errorMessage;
        _errorLabel.Visible = true;
    }

}
