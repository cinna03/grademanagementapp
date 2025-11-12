# Student Grade Management System

## Overview
This repository delivers a hybrid student-grade management experience that combines a feature-rich C# console application with an optional Windows Forms desktop interface. Both clients share a central domain library, ensuring consistent business rules, robust validation, and LINQ-powered analytics.

## Solution Structure
| Project | Description |
| --- | --- |
| `GradeManagement.Core` | Domain library containing grade categories, student models, statistics, and the `StudentGradeService`. |
| `GradeManagement.ConsoleApp` | Keyboard-driven console experience with colored prompts, validation feedback, and a launcher for the desktop client. |
| `GradeManagement.Desktop` | Windows Forms UI featuring guided input fields, interactive grid, statistical dashboards, and category summaries. |

## Key Features
- **Dictionary-backed storage** for O(1) student lookups keyed by name.
- **LINQ analytics** (`Average`, `Max`, `Min`, `GroupBy`) for insights and trend reporting.
- **Grade categories** via strongly-typed enums (`Failing`, `Passing`, `Good`, `Excellent`).
- **Robust validation & exception handling** preventing invalid or duplicate entries.
- **Hybrid input system** allowing users to switch between console commands and an on-screen UI.
- **Summary statistics** including averages, highest/lowest performers (with tie handling), and category totals.
- **Extensible architecture** with shared services and models, enabling future integrations or persistence layers.

## Getting Started
1. **Install prerequisites**
   - [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
   - Windows 10 build 17763 or later (required for WinForms).
2. **Restore & build**
   ```bash
   dotnet restore GradeManagementApp.sln
   dotnet build GradeManagementApp.sln
   ```

## Running the Applications
- **Console experience**
  ```bash
  dotnet run --project GradeManagement.ConsoleApp
  ```
  Navigate the menu to add, update, remove, search, summarize, or launch the desktop UI.

- **Windows Forms UI**
  ```bash
  dotnet run --project GradeManagement.Desktop
  ```
  Use the on-screen controls to manage records. Data is kept in-memory within the session.

### Console ↔ Desktop Bridge
From the console menu, choose the `UI` command to launch the WinForms executable (after building the desktop project). Both interfaces can be used independently; each maintains its own in-memory session.

## Demonstration Resources
- `docs/demo-script.md` (included) outlines a step-by-step audiovisual walkthrough: narrate architecture, demonstrate console flows, showcase the desktop dashboard, and justify design trade-offs.
- Suggested talking points:
  - Emphasize dictionary-backed storage and enum-based categorization.
  - Highlight LINQ usage for analytics (average, max/min, grouping).
  - Explain validation strategy and exception handling guardrails.
  - Compare keyboard vs. on-screen workflows for accessibility.

## Extending the System
- Integrate persistence by replacing the in-memory dictionary with a repository pattern.
- Add automated tests (xUnit or MSTest) targeting `GradeManagement.Core`.
- Expand the UI with filtering, import/export, or live charting.

## Contributing
1. Fork or clone the repository.
2. Create a feature branch (`git checkout -b feature/my-improvement`).
3. Submit a pull request describing the enhancement and testing steps.

