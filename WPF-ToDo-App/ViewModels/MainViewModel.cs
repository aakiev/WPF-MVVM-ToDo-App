using System.Collections.ObjectModel;  // Ermöglicht die Verwendung von ObservableCollection<T>
using System.ComponentModel;  // Enthält INotifyPropertyChanged für UI-Updates
using System.Linq;  // Wird für Listenumwandlungen (z. B. .ToList()) genutzt
using System.Windows.Input;  // Ermöglicht die Nutzung von ICommand für Buttons
using MVVM.Models;  // Importiert das Modell (TodoItem)
using MVVM.Helpers;  // Importiert die Speicherklasse (JsonStorage)

namespace MVVM.ViewModels
{
    /// <summary>
    /// Das ViewModel für die To-Do-App.
    /// Verwaltet die Aufgabenliste und verarbeitet Benutzerinteraktionen.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        // Privates Feld zur Speicherung des Textes der neuen Aufgabe
        private string _newTaskTitle;

        /// <summary>
        /// Öffentliche Eigenschaft für den Titel der neuen Aufgabe.
        /// Änderungen lösen ein UI-Update aus.
        /// </summary>
        public string NewTaskTitle
        {
            get { return _newTaskTitle; }  // Gibt den aktuellen Wert zurück
            set
            {
                _newTaskTitle = value;  // Speichert den neuen Wert
                OnPropertyChanged(nameof(NewTaskTitle));  // Informiert die UI über die Änderung
            }
        }

        /// <summary>
        /// Liste aller To-Do-Elemente.
        /// Nutzt eine ObservableCollection, damit die UI automatisch aktualisiert wird.
        /// </summary>
        public ObservableCollection<TodoItem> Todos { get; set; }

        /// <summary>
        /// Command zum Hinzufügen einer Aufgabe.
        /// Wird im XAML mit dem "Hinzufügen"-Button verknüpft.
        /// </summary>
        public ICommand AddTaskCommand { get; }

        /// <summary>
        /// Command zum Entfernen einer Aufgabe.
        /// Wird im XAML mit dem "❌"-Button jeder Aufgabe verknüpft.
        /// </summary>
        public ICommand RemoveTaskCommand { get; }

        /// <summary>
        /// Konstruktor des ViewModels.
        /// Lädt beim Start gespeicherte Aufgaben und initialisiert die Commands.
        /// </summary>
        public MainViewModel()
        {
            // Lade gespeicherte Aufgaben beim Start aus der JSON-Datei
            var loadedTasks = JsonStorage.LoadTasks();
            Todos = new ObservableCollection<TodoItem>(loadedTasks);

            // Commands für Buttons definieren
            AddTaskCommand = new RelayCommand(AddTask);  // Hinzufügen einer Aufgabe
            RemoveTaskCommand = new RelayCommand(RemoveTask);  // Entfernen einer Aufgabe
        }

        /// <summary>
        /// Methode zum Hinzufügen einer neuen Aufgabe.
        /// Wird von AddTaskCommand aufgerufen, wenn der Nutzer den "Hinzufügen"-Button klickt.
        /// </summary>
        private void AddTask()
        {
            // Überprüft, ob das Eingabefeld nicht leer ist
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                // Erstellt ein neues To-Do-Element
                var newTask = new TodoItem { Title = NewTaskTitle, IsDone = false };

                // Fügt die neue Aufgabe zur Liste hinzu (UI wird automatisch aktualisiert)
                Todos.Add(newTask);

                // Speichert die geänderte Liste in die JSON-Datei
                SaveTasks();

                // Leert das Eingabefeld nach dem Hinzufügen
                NewTaskTitle = "";
            }
        }

        /// <summary>
        /// Methode zum Entfernen einer Aufgabe.
        /// Wird von RemoveTaskCommand aufgerufen, wenn der Nutzer auf "❌" klickt.
        /// </summary>
        /// <param name="task">Die zu entfernende Aufgabe.</param>
        private void RemoveTask(object task)
        {
            // Prüft, ob das übergebene Objekt eine TodoItem-Instanz ist
            if (task is TodoItem todo)
            {
                // Entfernt die Aufgabe aus der Liste
                Todos.Remove(todo);

                // Speichert die geänderte Liste in die JSON-Datei
                SaveTasks();
            }
        }

        /// <summary>
        /// Speichert die aktuelle To-Do-Liste in der JSON-Datei.
        /// Wird nach jeder Änderung (Hinzufügen/Entfernen) aufgerufen.
        /// </summary>
        private void SaveTasks()
        {
            JsonStorage.SaveTasks(Todos.ToList());  // Speichert die Liste als JSON
        }

        /// <summary>
        /// Event zur Benachrichtigung der UI über Property-Änderungen.
        /// Wird benötigt, damit WPF automatisch die Oberfläche aktualisiert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Methode zum manuellen Auslösen von Property-Änderungen.
        /// Wird in den Settern verwendet, um UI-Updates zu ermöglichen.
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
