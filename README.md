# Credit Referencing Console Application

This is a .NET Framework console application designed for credit referencing, focusing on parsing transaction and property data from CSV files and performing affordability checks based on financial transactions

To submit bug reports, feature suggestions, or track changes, visit the GitHub repository at [https://github.com/your-username/credit-referencing-console-application](https://github.com/JayMackay/CreditReferencingConsoleApplication)

## Contents Of This File

- Requirements
- Features
- Installation
- Problem

## Requirements

This project requires .NET Framework 4.6.2. Ensure you have the necessary version installed to build and run the application, which can be found [HERE](https://dotnet.microsoft.com/en-us/download/dotnet-framework).

Git link for cloning the repository: `https://github.com/JayMackay/CreditReferencingConsoleApplication.git`

## Features

This application showcases modern C# features and best practices for handling CSV files and other standerd design patterns:

- **CSV File Handling:** Utilizes `CsvReaderService` to parse transaction and property data from CSV files, demonstrating file handling and data parsing techniques
- **Error Handling and Validation:** Implements error handling within `CsvReaderService` to manage exceptions during file operations and ensure data integrity
- **Affordability Check:** Integrates `AffordabilityService` to perform credit referencing checks based on financial transactions, determining affordable properties for tenants
- **Dependency Injection:** Utilizes dependency injection to promote loose coupling and enhance testability by injecting dependencies into services like `CsvReaderService` and `AffordabilityService`
- **Configuration Management:** Utilizes `appsettings.json` for dynamic configuration of file paths, enhancing maintainability and flexibility in managing application settings

### Installation

**Step 1: Clone the Repository**

Clone the repository to your local machine using Git:

```
git clone <repository-url>
```

_Replace <repository-url> with the actual URL of the Git repository_


**Step 2: Navigate to the Project Directory**

Open a terminal or command prompt and navigate into the project directory:

```
cd CreditReferencingConsoleApplication
```


**Step 3: Build the Solution**

Build the solution using Visual Studio or .Net CLI:

Using Visual Studio:
Open the solution file (CreditReferencingConsoleApplication.sln) in Visual Studio and build the solution

Using .Net CLI
Run the following command in your terminal or command prompt:

```
dotnet build
```

**Step 4: Place CSV Files**

Navigate to the `bin\Debug\Files` directory of the project and place your bank statement (`transactions.csv`) and properties (`properties.csv`) CSV files

**Step 5: Configure File Names**

Open `appsettings.json` located in the root directory of the project and update the file paths for `TransactionsFilePath` and `PropertiesFilePath` under `CsvFiles` section to match the file names you placed in Step 4

Example `appsettings.json` snippet:

```json
{
  "CsvFiles": {
    "TransactionsFilePath": "transactions.csv",
    "PropertiesFilePath": "properties.csv"
  }
}
```

## Problem

Given financial transaction data and property listings from CSV files, this application calculates affordable properties for tenants based on their financial transactions

Examples:

```csharp
// Given
// Transactions - date, type description, money out, money in, balance
"1st January 2020", "Direct Debit", "Gas & Electricity", "£95.06", "", "£1200.04"
"2nd January 2020", "ATM", "HSBC Holborn", "£20.00", "", "£1180.04"
"3rd January 2020", "Standing Order", "London Room", "£500.00", "", "£680.04"
"4th January 2020", "Bank Credit", "Awesome Job Ltd", "", "£1254.23", "£1934.27"
"1st February 2020", "Direct Debit", "Gas & Electricity", "£95.06", "", "£1839.21"
"2nd February 2020", "ATM", "@Random", "£50.00", "", "£1789.21"
"3rd February 2020", "Standing Order", "London Room", "£500.00", "", "£1289.21"
"4th February 2020", "Bank Credit", "Awesome Job Ltd", "", "£1254.23", "£2543.44"

// Properties - id, address, rent per month
1, "1, Oxford Street", 300
2, "12, St John Avenue", 750
3, "Flat 43, Expensive Block", 1200
4, "Flat 44, Expensive Block", 1150
           
// When
affordableProperties = affordabilityService.Check(transactions, properties);
        
// Then
Assert.AreEqual(1, affordableProperties.Count)
Assert.AreEqual("1, Oxford Street", affordableProperties[0].address)
```


