# ToDoApp - WPF Desktop Application

A desktop task management application built with C# using WPF and the MVVM pattern. This project was created as part of learning .NET and C# programming.

## 📋 Project Overview

ToDoApp is a full-featured personal task management application with user system, categories, and secure data storage. The application allows users to create, edit, mark as completed, and delete tasks organized by categories.

## 🛠 Technologies & Tools

- **Platform**: .NET 8.0 (Windows)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Architectural Pattern**: MVVM (Model-View-ViewModel)
- **Database**: SQLite
- **Languages**: C#, XAML
- **Security**: PBKDF2 password hashing with salt

## 📁 Project Structure

```
ToDoApp/
├── Models/                 # Data models
│   ├── User.cs            # User model
│   ├── Category.cs        # Category model
│   └── TaskItem.cs        # Task model
├── Services/              # Data access services
│   ├── DatabaseHelper.cs  # Database initialization
│   ├── UserService.cs     # User management
│   ├── CategoryService.cs # Category management
│   ├── TaskService.cs     # Task management
│   └── SecurityHelper.cs  # Password hashing
├── ViewModels/            # ViewModels for MVVM
│   ├── BaseViewModel.cs   # Base ViewModel class
│   ├── LoginViewModel.cs  # Login window ViewModel
│   ├── TaskViewModel.cs   # Main window ViewModel
│   └── RelayCommand.cs    # ICommand implementation
└── Views/                 # User interface
    ├── LoginWindow.xaml   # Authentication window
    ├── MainWindow.xaml    # Main application window
    └── InputDialog.xaml   # Text input dialog
```

## 🏗 Architectural Decisions

### MVVM Pattern
The application is built using the MVVM pattern to separate presentation logic from business logic:

- **Models**: Simple POCO classes for data representation (User, Category, TaskItem)
- **Views**: XAML files defining the user interface
- **ViewModels**: Binding layer between Views and Models, containing presentation logic

### Data Binding & Commands
- Two-way data binding for automatic UI updates
- Command pattern implementation through `RelayCommand` for handling user actions
- `ObservableCollection` for automatic change notifications in collections

### Database
**SQLite** was chosen for the following reasons:
- Local data storage without external server requirements
- Simple deployment (single database file)
- Sufficient functionality for educational project
- ACID transaction support

### Security
Modern security system implementation:
- **PBKDF2** with salt for password hashing
- 100,000 iterations for brute-force attack protection
- Unique salt for each password

## 🔧 Features

### User System
- New user registration
- Authentication with credential verification
- Secure password storage

### Category Management
- Create personal categories for task organization
- Edit category names
- Delete categories (with warning about deleting all related tasks)

### Task Management
- Add new tasks to selected category
- Mark tasks as completed
- Edit task names
- Delete unwanted tasks

### User Interface
- Modern dark design with gradient backgrounds
- Intuitive two-panel layout
- Personalized user greeting
- Logout functionality

## 🎯 Why These Solutions?

### WPF + MVVM
- **WPF** provides rich capabilities for creating desktop applications with modern UI
- **MVVM** ensures clear separation of concerns and simplifies testing
- Pattern is well-suited for WPF's data-binding capabilities

### SQLite
- No separate database server installation required
- Perfect for single-user desktop applications
- Portability - entire database in one file

### Async/Await
- All database operations are performed asynchronously
- Prevents UI blocking during data operations
- Improves application responsiveness

### Modular Service Architecture
- Each service handles its own domain (UserService, TaskService, CategoryService)
- Simplifies maintenance and feature extension
- Follows Single Responsibility Principle (SRP)

## 🚀 How to Run

1. Clone the repository
2. Open `ToDoApp.sln` in Visual Studio
3. Restore NuGet packages
4. Run the project (F5)

The database file `todoapp.db` is automatically created on first run.

## 📝 Learning Outcomes

This educational project demonstrates:
- Working with WPF and XAML
- Applying MVVM pattern
- Database operations through ADO.NET
- Information security fundamentals
- Asynchronous programming
- Clean architecture principles
