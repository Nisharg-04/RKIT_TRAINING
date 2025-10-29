# Reasoning for Abstract vs Sealed

## Abstract Class

An **abstract class** in C# is a class that **cannot be instantiated directly** and is intended to be a base class for other classes.  
It can contain abstract methods (without implementation) as well as implemented methods.  
Derived classes are required to provide implementation for abstract methods.

**example:**
```csharp
public abstract class FileImporter<T>
```

* `FileImporter<T>` is marked as **abstract** because it defines a **generic blueprint** for importing files.
* It is designed to be extended by other specific importer classes.
* This allows different formats (CSV, XML, JSON, etc.) to have their own implementations while sharing common logic defined in the abstract base.
* Abstract enforces that you cannot create an instance of `FileImporter<T>` directly — only through a derived class.

---

## Sealed Class

A **sealed class** in C# is a class that **cannot be inherited**.  
It is used when the class is complete and should not be extended further.

**example:**
```csharp
public sealed class CsvBookImporter : FileImporter<Book>
```

* `CsvBookImporter` is **sealed** because it is a **final concrete implementation** of `FileImporter<Book>`.
* It means no other class can inherit from `CsvBookImporter`.
* This can improve performance slightly and enforce **design constraints** where the class logic should remain fixed.
* It ensures that the CSV-specific import logic cannot be modified or extended through inheritance.

---

## Summary of Reasoning

* **Abstract class** → Used to define a **base contract** and share common logic, allowing inheritance and polymorphism.
* **Sealed class** → Used to **finalize** a class implementation so no further extension is possible.
* In your design:
  * `FileImporter<T>` is abstract → to define a generic import strategy.
  * `CsvBookImporter` is sealed → to provide a **fixed CSV import implementation**.
