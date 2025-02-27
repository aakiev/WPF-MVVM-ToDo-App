using System;  // Enthält grundlegende .NET-Typen wie `string` und `bool`
using System.ComponentModel;  // Enthält `INotifyPropertyChanged` für die Datenbindung

namespace MVVM.Models
{
    /// <summary>
    /// Repräsentiert eine einzelne To-Do-Aufgabe in der Anwendung.
    /// Implementiert `INotifyPropertyChanged`, damit Änderungen in der UI angezeigt werden.
    /// </summary>
    public class TodoItem : INotifyPropertyChanged
    {
        // Privates Feld zur Speicherung des Titels der Aufgabe, nach Konvention mit Unterstrich
        private string _title;

        // Privates Feld zur Speicherung des Status (Erledigt oder nicht)
        private bool _isDone;

        /// <summary>
        /// Öffentliche Eigenschaft für den Titel der Aufgabe.
        /// Wenn der Wert geändert wird, wird die UI über `OnPropertyChanged` informiert.
        /// </summary>
        public string Title
        {
            get => _title; // Getter gibt den aktuellen Wert von `_title` zurück
            set
            {
                _title = value; // Setzt den neuen Wert für `_title`
                OnPropertyChanged(nameof(Title)); // Benachrichtigt die UI über die Änderung
            }
        }

        /// <summary>
        /// Öffentliche Eigenschaft, die speichert, ob die Aufgabe erledigt ist oder nicht.
        /// Wenn sich der Status ändert, wird die UI aktualisiert.
        /// </summary>
        public bool IsDone
        {
            get => _isDone; // Getter gibt den aktuellen Wert von `_isDone` zurück
            set
            {
                _isDone = value; // Setzt den neuen Wert für `_isDone`
                OnPropertyChanged(nameof(IsDone)); // Benachrichtigt die UI über die Änderung
            }
        }

        /// <summary>
        /// Event, das ausgelöst wird, wenn sich eine Eigenschaft ändert.
        /// Dieses Event wird von WPF genutzt, um die UI automatisch zu aktualisieren.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Methode zum Auslösen des `PropertyChanged`-Events.
        /// Diese Methode wird in den Settern von `Title` und `IsDone` aufgerufen.
        /// </summary>
        private void OnPropertyChanged(string propertyName)
        {
            // Falls `PropertyChanged` abonniert wurde, löst es das Event aus.
            // `?.` stellt sicher, dass das Event nur aufgerufen wird, wenn es nicht `null` ist.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
