# Employee Record Management

A C# learning project focused on object-oriented programming, interfaces, abstract classes, inheritance, polymorphism, and file handling.

## 🎯 Purpose

The main purpose of this project is to learn and practice C# concepts by building an employee record management system.

The application manages employee details such as Name, Age, and Salary. It supports adding employee records and saving or restoring them using TXT, CSV, and JSON file formats.

## 🛠️ Tech Stack

- **Language:** C#
- **Framework:** .NET / WPF
- **Concepts:** Object-Oriented Programming (OOP)
- **Data Storage:** TXT, CSV, JSON files
- **Serialization:** `System.Text.Json`
- **IDE:** Visual Studio
- **Version Control:** Git and GitHub

## ✨ Features

- Store employee details: Name, Age, and Salary.
- Maintain employee records using `List<emp>`.
- Initialize sample employee records using `Init()`.
- Add new employees using `Add()`.
- Save employee records in TXT, CSV, or JSON format.
- Restore employee records from saved files.
- Validate file paths and extensions.
- Use separate storage classes for each file format.

## 🏗️ Project Architecture

```text
EMP_record_management/
│
├── Models/
│   └── emp.cs
│
├── Interfaces/
│   └── Iemp.cs
│
├── Services/
│   ├── EmployeeManager.cs
│   └── EmployeeService.cs
│
├── Storage/
│   ├── EmployeeStorageBase.cs
│   ├── txtfile.cs
│   ├── csvfile.cs
│   └── jsonfile.cs
│
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── App.xaml
├── App.xaml.cs
└── EMP_record_management.csproj


# Employee Record Management --- UML Class Diagram

This class diagram describes the core architecture of
`EMP_record_management`.

## UML Class Diagram

``` mermaid
classDiagram
    direction TB

    class emp {
        +string Name
        +int Age
        +decimal Salary
    }

    class EmployeeManager {
        -List~emp~ employees
        -EmployeeService employeeService
        +Init() void
        +Add(employee: emp) void
        +Save(filePath: string, format: string) void
        +Restore(filePath: string) void
    }

    class EmployeeService {
        +SaveEmployees(employees, filePath, format) void
        +RestoreEmployees(filePath: string) List~emp~
    }

    class Iemp {
        <<interface>>
        +save(employees: List~emp~, filePath: string) void
        +restore(filePath: string) List~emp~
    }

    class EmployeeStorageBase {
        <<abstract>>
        #ValidatePath(filePath: string, extension: string, requireExistingFile: bool) void
        +save(employees: List~emp~, filePath: string)*
        +restore(filePath: string) List~emp~*
    }

    class txtfile {
        +save(employees: List~emp~, filePath: string) void
        +restore(filePath: string) List~emp~
    }

    class csvfile {
        +save(employees: List~emp~, filePath: string) void
        +restore(filePath: string) List~emp~
        -ParseCsvLine(line: string) string[]
    }

    class jsonfile {
        +save(employees: List~emp~, filePath: string) void
        +restore(filePath: string) List~emp~
    }

    EmployeeManager "1" o-- "*" emp : manages employee records
    EmployeeManager --> EmployeeService : delegates file operations
    EmployeeService ..> Iemp : calls through interface
    EmployeeStorageBase ..|> Iemp : implements
    txtfile --|> EmployeeStorageBase : inherits
    csvfile --|> EmployeeStorageBase : inherits
    jsonfile --|> EmployeeStorageBase : inherits
```

## How to read the diagram

-   **`emp`** represents one employee and contains `Name`, `Age`, and
    `Salary`.
-   **`EmployeeManager`** owns the in-memory `List<emp>` and provides
    `Init()`, `Add()`, `Save()`, and `Restore()`.
-   **`EmployeeService`** chooses the appropriate storage class for the
    selected format or file extension.
-   **`Iemp`** defines the common `save()` and `restore()` storage
    contract.
-   **`EmployeeStorageBase`** implements `Iemp`, shares
    `ValidatePath()`, and declares abstract storage methods.
-   **`txtfile`, `csvfile`, and `jsonfile`** inherit from
    `EmployeeStorageBase` and implement format-specific file handling.

## Relationship symbols

  UML notation         Meaning
  -------------------- ----------------------------------------
  `--|>`               Inheritance
  `..|>`               Interface implementation (realization)
  `-->`                Uses / dependency
  `o--`                Aggregation
  `+`                  Public member
  `-`                  Private member
  `#`                  Protected member
  `*` after a method   Abstract method

## Save flow

1.  `EmployeeManager.Save()` passes the employee records to
    `EmployeeService`.
2.  `EmployeeService` chooses `txtfile`, `csvfile`, or `jsonfile`.
3.  The selected object is referenced through `Iemp`.
4.  The storage implementation validates the path and writes the data in
    its format.

## Restore flow

1.  `EmployeeManager.Restore()` calls `EmployeeService`.
2.  `EmployeeService` chooses the storage implementation based on the
    file extension.
3.  The storage class validates and reads the file.
4.  The storage class converts the file data into `List<emp>` and
    returns it.
5.  `EmployeeManager` replaces its current in-memory records with the
    restored records.

> **Note:** Keep class names and method signatures in this diagram
> aligned with the actual source code. Mermaid diagrams render on GitHub
> when placed in a Markdown (`.md`) file.

## 🚀 Learning Outcome

Through this project, I practiced applying C# fundamentals and OOP concepts to a practical application. I learned how to organize classes, define interfaces, reuse common functionality through inheritance, implement polymorphism, and handle employee data using multiple file formats.

This project is a foundation for learning more advanced C# concepts and building larger applications with maintainable code.
