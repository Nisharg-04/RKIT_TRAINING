# Pluggable Import Pipeline

## Project Overview

The **Pluggable Import Pipeline** is designed as a flexible and extensible system to import, process, and report on data from various file formats.  
It leverages abstraction, generics, and dependency inversion to allow easy addition of new file formats and report types.

---

## Project Structure

```
PluggableImportPipeline
│
├── IngestionCLI (Console Application)
│   └── Entry point for the application
│       - Parses command-line arguments
│       - Initiates file scanning
│       - Coordinates import and reporting
│
├── IngestionPipeline (Class Library)
│   ├── Abstract Class: FileImporter<T>
│   │   - Defines the blueprint for file importers
│   │
│   ├── Concrete Importers:
│   │   ├── CsvBookImporter : FileImporter<Book>
|   │   ├── JsonBookImporter : FileImporter<Book>
|   │
│   ├── Interface: IReportWriter<T>
|   │   │   - Defines contract for report writers
|   │
│   ├── Concrete Report Writers:
│   │   ├── TextReportWriter : IReportWriter<Report>
|   │   ├── XmlReportWriter : IReportWriter<Report>
|   │
│   ├── Models:
│   │   ├── Report
│   │   ├── SerializableKeyValue
│   │   └── SummaryReport
|   │
│   └── Extension Methods:
│       - BookExtensions
│         - TopBy<TValue>(this IEnumerable<Book>, Func<Book,TValue>, int n)
│         - ToConditionCounts(this IEnumerable<Book>)
│
└── LibraryCheckInDomain (Class Library)
    └── Domain Models (Book, BookConditionEnum,etc)
```

---

## Data Flow

1. **Initialization**:  
   - `IngestionCLI` starts and parses CLI arguments (`--in`, `--out`, `--dry-run`).  
   - Determines input/output directories.

2. **File Discovery**:  
   - Recursively scans input directory for supported files (`.csv`, `.json`).

3. **Importer Selection**:  
   - Chooses appropriate `FileImporter<T>` implementation (`CsvBookImporter` or `JsonBookImporter`) based on file type.

4. **Data Ingestion**:  
   - Reads files and converts them into collections of `Book` objects.

5. **Aggregation & Processing**:  
   - Aggregates all imported books.  
   - Uses **extension methods**:
     - `TopBy<TValue>`: Returns top N books ordered by a key.
     - `ToConditionCounts`: Counts books grouped by their condition.

6. **Reporting**:  
   - Uses `IReportWriter<T>` implementations (`TextReportWriter`, `XmlReportWriter`) to serialize reports.

7. **Output**:  
   - Writes reports (`.txt`, `.xml`) to the output directory.

---

## Key Design Principles

- **Abstraction**:  
  - `FileImporter<T>` and `IReportWriter<T>` define contracts without specifying concrete behavior.

- **Generics**:  
  - Makes importers and report writers reusable for different data models.

- **Separation of Concerns**:  
  - Import logic, processing logic, and reporting logic are separated into different components.

- **Extension Methods**:  
  - Encapsulate reusable data processing logic for `Book` collections.

- **Extensibility**:  
  - New importers or report writers can be added without modifying existing code.
