MediTech Management Desktop Application
=======================================

Table of Contents
-----------------

1.  [Project Overview](#project-overview)
    
2.  [Features](#features)
    
3.  [Architecture & Technologies](#architecture--technologies)
    
4.  [Database Schema](#database-schema)
    
5.  [Installation & Setup](#installation--setup)
    
6.  [Usage](#usage)
    
7.  [Project Structure](#project-structure)
    
8.  [Development Highlights](#development-highlights)
    
9.  [Authors & Contributions](#authors--contributions)
    
10.  [License](#license)
    

Project Overview
----------------

**MediTech Management** is a WPF-based desktop application built on .NET Framework 4.8 and MySQL. It provides a unified, user-friendly interface for managing all aspects of a small medical practice, including patients, staff, treatments, billing, and more. A secure login gatekeeps access to the dashboard, and role-based extension points are in place for future permissions.

Features
--------

*   **Secure Login**
    
    *   SHA-2 hashed passwords stored in a users table
        
    *   Login dialog blocks dashboard access until authentication succeeds
        
    *   “Log Out” returns to Home view without closing the app
        
*   **Home / About / Contact Pages**
    
    *   Landing pages displayed at startup (no login required)
        
    *   Static content with branding and contact information
        
*   **Dashboard Navigation**
    
    *   Collapsible sidebar listing 14 management modules
        
    *   Smooth animated transitions between views
        
*   **CRUD Forms** (Master-Detail pattern) for:
    
    1.  Patients
        
    2.  Doctors
        
    3.  Nurses
        
    4.  AdminStaff
        
    5.  Departments (lookup)
        
    6.  Specializations (lookup)
        
    7.  StaffRoles (lookup)
        
    8.  Treatments
        
    9.  TreatmentAssignments
        
    10.  PatientFiles (upload/download)
        
    11.  InsuranceProviders
        
    12.  InsurancePolicies
        
    13.  PatientInsurance (many-to-many)
        
    14.  Invoices
        
    15.  Payments
        
    16.  Appointments
        
    17.  MedicalRecords
        
    18.  Prescriptions
        
    19.  FrequencyTypes (lookup)
        
    
    *   Grid loads on startup; selecting a row populates form fields
        
    *   New, Save, Edit, Delete, Refresh buttons with state-aware enabling
        
    *   Validation messages for missing/invalid data
        
    *   Foreign-key constraint errors surfaced with clear alerts
        
*   **File Management**
    
    *   Attach medical documents to patients
        
    *   Browse & upload local files (BLOB storage)
        
    *   Download and save files via SaveFileDialog
        
*   **Robust Data Access Layer**
    
    *   MySqlDbHelper for raw queries and non-queries
        
    *   Stored procedures (sp\_Add…, sp\_Update…, sp\_Delete…, sp\_Get…) for all tables
        
    *   Transactions ensure referential integrity on multi-step deletes
        
*   **Responsive UX**
    
    *   Asynchronous database operations keep UI fluid
        
    *   Modern styling via community themes (e.g. MahApps.Metro)
        
    *   Consistent iconography and typography
        

Architecture & Technologies
---------------------------

*   **Client**: C# / WPF (.NET Framework 4.8)
    
*   **Data Access**: ADO.NET with MySQL Connector/NET
    
*   **Database**: MySQL 8.x
    
*   **Design Patterns**: MVVM-inspired separation, Repository/Service layers
    
*   **Dependency Injection**: Lightweight factories for services
    
*   **Security**: SHA-2 password hashing, parameterized queries to prevent SQL injection
    

Database Schema
---------------

All tables are in the meditech schema. Major tables include:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   sqlCopyEdit-- Users  CREATE TABLE users (    user_id       INT AUTO_INCREMENT PRIMARY KEY,    username      VARCHAR(50) UNIQUE NOT NULL,    password_hash CHAR(64)    NOT NULL,    role          VARCHAR(20) NOT NULL  -- e.g. 'AdminStaff','Doctor','Nurse'  );  -- Patients  CREATE TABLE patients (    patient_id    INT AUTO_INCREMENT PRIMARY KEY,    first_name    VARCHAR(50) NOT NULL,    last_name     VARCHAR(50) NOT NULL,    date_of_birth DATE         NOT NULL,    gender        ENUM('Male','Female','Other') NOT NULL,    phone         VARCHAR(20),    email         VARCHAR(100),    address       TEXT  );  -- And similarly: doctors, nurses, adminstaff, departments, specializations, staffroles,  -- treatments, frequencytypes, insuranceproviders, insurancepolicies, appointments, medicalrecords, prescriptions,  -- invoices, payments, patientfiles, treatmentassignments, patientinsurance (all with appropriate FKs).   `

*   **Lookup Tables** (e.g. Departments, FrequencyTypes) ensure referential integrity.
    
*   **Associative Tables** (composite primary keys) for many-to-many relationships:
    
    *   TreatmentAssignments(patient\_id, treatment\_id, assignment\_date)
        
    *   PatientInsurance(patient\_id, policy\_id)
        

Stored procedures wrap every insert/update/delete, enforcing business rules and constraints at the database level.

Installation & Setup
--------------------

1.  **Database**
    
    *   Run meditech\_schema.sql against your MySQL server to create schema, tables, views, and stored procedures.
        
    *   Optionally run meditech\_seed\_data.sql to populate lookup tables and a test admin user (Username: admin | Password: admin123).
        
2.  **Application**
    
    *   Clone or download this repo.
        
    *   Open MediTechDesktopApp.sln in Visual Studio 2022+ with .NET Framework 4.8.
        
    *   xmlCopyEdit
        
    *   Restore NuGet packages and build the solution.
        
    *   Run (F5). The **Home** page appears by default. Click **Dashboard** → **Log In** to authenticate.
        

Usage
-----

1.  **Explore** the Home/About/Contact pages without logging in.
    
2.  Click **Dashboard** in the top navbar to open the Login dialog.
    
3.  Enter valid credentials (e.g. admin / admin123).
    
4.  Upon success, the left **Dashboard** sidebar appears with all modules.
    
5.  Click any module to view its grid & form.
    
6.  **New**: Clears form for new entry.
    
7.  **Save**: Validates and calls sp\_Add… or sp\_Update… accordingly.
    
8.  **Edit**: Unlocks form fields for modifications.
    
9.  **Delete**: Confirms then calls sp\_Delete…; cascades or restricts based on FKs.
    
10.  **Refresh**: Reloads the grid and resets the form.
    
11.  **Log Out**: Hides dashboard and returns to Home page (no app exit).
    

Project Structure
-----------------

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   pythonCopyEdit/MediTechDesktopApp.sln  ├─ App.config  ├─ App.xaml  ├─ MainWindow.xaml      # Hosts Navbar, ContentControl swap area  ├─ Views/  │   ├─ HomeView.xaml  │   ├─ AboutView.xaml  │   ├─ ContactView.xaml  │   ├─ LoginWindow.xaml  │   ├─ DashboardView.xaml # Contains sidebar & animated Container  │   ├─ PatientsView.xaml  │   ├─ DoctorsView.xaml  │   └─ … (all other CRUD views)  ├─ Services/  │   ├─ UserService.cs  │   ├─ PatientService.cs  │   ├─ DoctorService.cs  │   └─ … (one per entity)  ├─ DataAccess/  │   └─ MySqlDbHelper.cs   # ADO.NET helper for queries/non-queries  ├─ Models/  │   ├─ User.cs            # Matches users table  │   ├─ Patient.cs  │   └─ … (all other entities)  └─ Commands/      └─ RelayCommand.cs    # For MVVM binding (if used)   `

Development Highlights
----------------------

*   **Consistency**: Every CRUD view follows the same pattern—minimal cognitive load for users.
    
*   **Separation of Concerns**: UI in Views, data logic in Services, raw DB code in MySqlDbHelper.
    
*   **Error Handling**: Database errors (FKs, SP SIGNALs) bubble up as friendly WPF pop-ups.
    
*   **Documentation**: XML comments on all public methods; SQL comments on all objects.
    
*   **Extensibility**: Role checks in UserService enable fine-grained future permissions.
    
*   **Performance**: Asynchronous writes and optimized indices on FK columns.
    

Authors & Contributions
-----------------------

*   **Lead Developer**: \[Your Name\] – App architecture, all C# code, WPF layouts
    
*   **DB Designer**: \[Colleague\] – MySQL schema design, stored procedures, seed data
    
*   **UI/UX Consultant**: \[Colleague\] – Color palette, button styles, theme integration
    
*   **QA & Testing**: \[Colleague\] – Manual test plans, bug reports, validation scenarios
