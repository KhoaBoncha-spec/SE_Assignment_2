================================================================================
SOFTWARE ENGINEERING LAB - ASSIGNMENT 2 (SEM 1_2627)
B2B WHOLESALE ORDER MANAGEMENT SYSTEM (WinForms Back-Office + ASP.NET Core MVC)
================================================================================

1. GROUP & MEMBER CONTRIBUTION (Total = 100%)
--------------------------------------------------------------------------------
- Member 1: Tran Nguyen Dang Khoa (524H0058) - 50%
  Tasks: Database schema & seed data (Task 2), Shared Core Library, WinForms
  Login & Dashboard (Tasks 1.1, 1.4), Web Login & Transactional Order Placement
  (Tasks 3.1, 3.2), xUnit Testing (Task 5.1), Git Workflow & Configuration.
- Member 2: Nguyen Tat Dung (524H0045) - 50%
  Tasks: WinForms Item/Agent/Order CRUD & Shipping Manifest Report (Tasks 1.2,
  1.3), Web My Orders, Printable Invoice & Responsive Bootstrap UI (Tasks 3.3,
  3.4), Testing Verification & Documentation.

2. LINKS
--------------------------------------------------------------------------------
- GitHub Repository: https://github.com/KhoaBoncha-spec/SE_Assignment_2
- Video Demo (3-5 minutes): https://drive.google.com/drive/folders/1BYfQTQhgtETOYV4RqYfXlSBC0WYKd_1f?usp=drive_link

3. ENVIRONMENT REQUIREMENTS
--------------------------------------------------------------------------------
- Visual Studio 2026 (or Visual Studio 2022/2026 supporting .NET 10.0 SDK)
- .NET 10.0 SDK
- Microsoft SQL Server Express (instance: .\SQLEXPRESS) & SSMS

4. DATABASE SETUP INSTRUCTIONS
--------------------------------------------------------------------------------
Step 1: Open SQL Server Management Studio (SSMS) and connect to .\SQLEXPRESS.
Step 2: Open the script file "B2BWholesaleDB.sql" located in the root folder
        of SourceCode.zip.
Step 3: Execute the entire script (F5). It automatically creates the database
        "B2BWholesaleDB", all 5 tables with PK/FK/CHECK/UNIQUE constraints,
        and seeds 20-25 realistic sample rows per table.

5. CONFIGURATION & HOW TO RUN
--------------------------------------------------------------------------------
Step 1: Extract SourceCode.zip and open "B2BWholesale.Core.slnx" in Visual Studio.
Step 2: Verify/update the SQL Server connection string if your SQL Server
        instance name differs from ".\SQLEXPRESS":
        - WinForms App: B2BWholesale.WinForms\App.config (key: "B2BDatabase")
        - Web App:      B2BWholesale.Web\appsettings.json (key: "DefaultConnection")
Step 3: Restore NuGet packages and Build Solution (Ctrl + Shift + B).
Step 4: Run Back-Office Desktop Application (WinForms):
        - Right-click "B2BWholesale.WinForms" -> Set as Startup Project -> F5.
Step 5: Run Agent Portal Web Application (ASP.NET Core MVC - Option C):
        - Right-click "B2BWholesale.Web" -> Set as Startup Project -> F5.
        - Default URL: https://localhost:7168
Step 6: Run Unit Tests (xUnit - 8 test cases):
        - Open Menu: Test -> Test Explorer -> Click "Run All Tests".
        - Uses SQLite In-Memory; does not require modifying SQL Server data.

6. DEMO ACCOUNTS (DUMMY PASSWORDS ONLY)
--------------------------------------------------------------------------------
- Admin Account (WinForms Back-Office ONLY):
  Username: admin          | Password: 123456   (Active Admin)
  Username: admin_locked   | Password: 123456   (Locked Admin - for lockout test)

- Agent Account (Web Agent Portal ONLY):
  Username: agent1         | Password: 123456   (Cong ty TNHH Phan Phoi Minh Phat)
  Username: agent2         | Password: 123456   (Dai ly Si Hoang Gia)
  Username: agent_locked   | Password: 123456   (Locked Agent - for lockout test)

7. SOLUTION STRUCTURE
--------------------------------------------------------------------------------
- B2BWholesaleDB.sql       : SQL Server schema, constraints & seed data script
- B2BWholesale.Core/       : Shared Entities, B2BContext, AuthService, OrderService
- B2BWholesale.WinForms/   : Exercise 1 - Back-Office Desktop Application
- B2BWholesale.Web/        : Option C (Exer 4) - ASP.NET Core MVC Agent Portal
- B2BWholesale.Tests/      : Exercise 5 - xUnit Automated Unit Test Project
================================================================================