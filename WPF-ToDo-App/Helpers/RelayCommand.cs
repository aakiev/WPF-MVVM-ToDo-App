using System;  // Enthält grundlegende Typen wie `Action` und `Func`
using System.Windows.Input;  // Definiert das `ICommand`-Interface für MVVM

/// <summary>
/// `RelayCommand` implementiert das `ICommand`-Interface und ermöglicht
/// das Binden von Methoden an UI-Elemente wie Buttons in einer MVVM-Architektur.
/// Es unterstützt Methoden mit und ohne Parameter.
/// </summary>
public class RelayCommand : ICommand
{
    // Speichert die Aktion, die ausgeführt wird, wenn der Command ohne Parameter aufgerufen wird.
    private readonly Action _executeWithoutParam;

    // Speichert die Aktion, die ausgeführt wird, wenn der Command mit einem Parameter aufgerufen wird.
    private readonly Action<object> _executeWithParam;

    // Funktion zur Steuerung, ob der Command gerade ausführbar ist (true/false).
    private readonly Func<object, bool> _canExecute;

    /// <summary>
    /// Konstruktor für Commands ohne Parameter.
    /// </summary>
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        if (execute == null) throw new ArgumentNullException(nameof(execute));

        _executeWithoutParam = execute;

        // Falls `canExecute` null ist, wird standardmäßig `true` zurückgegeben (Command ist immer ausführbar).
        _canExecute = canExecute == null ? (p => true) : (p => canExecute());
    }

    /// <summary>
    /// Konstruktor für Commands mit Parameter.
    /// </summary>
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        if (execute == null) throw new ArgumentNullException(nameof(execute));

        _executeWithParam = execute;
        _canExecute = canExecute;
    }

    /// <summary>
    /// Prüft, ob der Command ausgeführt werden kann.
    /// </summary>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    /// Führt die mit dem Command verknüpfte Methode aus.
    /// </summary>
    public void Execute(object parameter)
    {
        if (_executeWithoutParam != null)
            _executeWithoutParam();  // Falls keine Parameter benötigt werden, diese Methode aufrufen
        else
            _executeWithParam(parameter);  // Falls ein Parameter benötigt wird, diese Methode aufrufen
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich die Ausführbarkeit des Commands ändert.
    /// Ermöglicht eine automatische UI-Aktualisierung.
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
