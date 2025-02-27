using System;  // Enthält grundlegende .NET-Funktionalitäten wie Exception-Handling
using System.Collections.Generic;  // Erlaubt die Verwendung von `List<T>`
using System.IO;  // Erlaubt Dateioperationen (Lesen/Schreiben von JSON-Dateien)
using System.Text.Json;  // Ermöglicht das Serialisieren und Deserialisieren von JSON
using MVVM.Models;  // Importiert das `TodoItem`-Modell

namespace MVVM.Helpers
{
    /// <summary>
    /// Statische Hilfsklasse zum Speichern und Laden von To-Do-Listen als JSON-Datei.
    /// </summary>
    public static class JsonStorage
    {
        // Dateipfad, unter dem die JSON-Datei gespeichert wird
        private static readonly string FilePath = "todos.json";

        /// <summary>
        /// Speichert die Liste der Aufgaben in einer JSON-Datei.
        /// </summary>
        /// <param name="todos">Liste der zu speichernden `TodoItem`-Objekte.</param>
        public static void SaveTasks(List<TodoItem> todos)
        {
            try
            {
                // Serialisiert die Liste als JSON-String (mit Einrückungen für bessere Lesbarkeit)
                var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });

                // Schreibt den JSON-String in die Datei
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                // Falls ein Fehler auftritt, wird dieser in der Konsole ausgegeben
                Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
            }
        }

        /// <summary>
        /// Lädt die gespeicherten Aufgaben aus der JSON-Datei.
        /// </summary>
        /// <returns>Eine Liste von `TodoItem`-Objekten. Falls die Datei nicht existiert oder ein Fehler auftritt, wird eine leere Liste zurückgegeben.</returns>
        public static List<TodoItem> LoadTasks()
        {
            try
            {
                // Prüft, ob die Datei existiert
                if (File.Exists(FilePath))
                {
                    // Liest den JSON-Inhalt aus der Datei
                    var json = File.ReadAllText(FilePath);

                    // Deserialisiert den JSON-String in eine `List<TodoItem>`
                    return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
                }
            }
            catch (Exception ex)
            {
                // Falls ein Fehler auftritt, wird dieser in der Konsole ausgegeben
                Console.WriteLine($"Fehler beim Laden: {ex.Message}");
            }

            // Falls die Datei nicht existiert oder ein Fehler auftritt, wird eine leere Liste zurückgegeben
            return new List<TodoItem>();
        }
    }
}
