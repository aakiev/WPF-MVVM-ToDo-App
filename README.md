# WPF Todo Application (MVVM)

## Overview
This repository hosts a WPF-based Todo application developed to gain hands-on experience with the **MVVM (Model-View-ViewModel) pattern** in C#. The project demonstrates how to separate the UI logic from business logic, implement **data binding**, and manage **user interactions using commands** instead of traditional event handlers.

Additionally, the application includes **persistent data storage** using **JSON serialization**, allowing tasks to be saved and loaded automatically.

## Key Objectives
- Understand and implement the **MVVM** pattern in a WPF application.
- Separate **UI logic** (View), **data models** (Model), and **business logic** (ViewModel).
- Utilize **data binding** and **ICommand-based commands** for UI interactions.
- Enable **persistent task storage** using JSON files.

## Core Features
### **1. Task Management**
   - Users can **add new tasks** using a simple input field.
   - Tasks can be **marked as completed** using a checkbox.
   - Tasks can be **removed** from the list using a delete button.

### **2. MVVM Architecture**
   - **Model (`TodoItem.cs`)** – Defines the structure of a task with properties like `Title` and `IsDone`.
   - **ViewModel (`MainViewModel.cs`)** – Manages the task list, handles user interactions, and updates the UI using `INotifyPropertyChanged`.
   - **View (`MainWindow.xaml`)** – Implements a modern WPF UI with **data binding** to the ViewModel.

### **3. Data Persistence with JSON**
   - The application automatically **saves all tasks** to a `todos.json` file.
   - When restarted, the application **loads the previous tasks** from the JSON file.
   - Storage is handled via the `JsonStorage.cs` helper class, using **System.Text.Json**.

### **4. Modern UI Design**
   - The application features a **modern dark theme** for better readability.
   - **Smooth buttons & UI elements** with hover effects for better user interaction.
   - Tasks are displayed in a **styled ListBox** with rounded corners.

## Technologies Used
- **Programming Language**: C#
- **Framework**: .NET Framework (WPF)
- **Development Environment**: Visual Studio 2019 or later
- **Version Control**: Git & GitHub
- **Libraries**:
  - `System.Text.Json` (for JSON serialization)
  - `INotifyPropertyChanged` (for UI updates in MVVM)

## Getting Started

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later installed on your machine

### Installation
1. Clone the repository using the following command:
   ```bash
   git clone https://github.com/YOUR_GITHUB_USERNAME/WPF-Todo-App.git
2. Open the solution file (.sln) in Visual Studio.
3. Build the solution and run the application.

## Usage
### Adding Tasks
  - Enter a task title in the input field.
  - Click the "Hinzufügen" button to add the task.
### Completing Tasks
  - Click the checkbox next to a task to mark it as completed.
### Deleting Tasks
  - Click the red ❌ button to remove a task from the list.
### Data Persistence
  - Tasks are automatically saved when added or removed.
  - When reopening the app, all previously saved tasks will be restored.
