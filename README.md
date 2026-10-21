
classDiagram
    class emp {
        +string Name
        +int Age
        +decimal Salary
    }

    class EmployeeManager {
        -List~emp~ employees
        -EmployeeService employeeService
        +Init()
        +Add(emp employee)
        +Save(string filePath, string format)
        +Restore(string filePath)
    }

    class EmployeeService {
        +SaveEmployees(employees, filePath, format)
        +RestoreEmployees(string filePath) List~emp~
    }

    class Iemp {
        <<interface>>
        +save(List~emp~, string filePath)
        +restore(string filePath) List~emp~
    }

    class EmployeeStorageBase {
        <<abstract>>
        #ValidatePath(filePath, extension, requireExistingFile)
        +save(List~emp~, string filePath)*
        +restore(string filePath)*
    }

    class txtfile {
        +save(List~emp~, string filePath)
        +restore(string filePath) List~emp~
    }

    class csvfile {
        +save(List~emp~, string filePath)
        +restore(string filePath) List~emp~
        -ParseCsvLine(string line) string[]
    }

    class jsonfile {
        +save(List~emp~, string filePath)
        +restore(string filePath) List~emp~
    }

    EmployeeManager "1" o-- "*" emp : manages
    EmployeeManager --> EmployeeService : calls
    EmployeeService ..> Iemp : uses
    EmployeeStorageBase ..|> Iemp : implements
    txtfile --|> EmployeeStorageBase : inherits
    csvfile --|> EmployeeStorageBase : inherits
    jsonfile --|> EmployeeStorageBase : inherits
