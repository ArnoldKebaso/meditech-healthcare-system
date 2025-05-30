meditech-healthcare-system
==========================

Project Overview
----------------

meditech-healthcare-system is a desktop application and cloud-based MySQL database solution designed to centralize patient records, treatment plans, personnel information, and billing for MediTech Innovations. The system streamlines data entry and retrieval for doctors, nurses, administrative staff, and billing personnel through a Windows Forms C# application.

Key Features
------------

*   **Patient Management**: Create, Read, Update, Delete (CRUD) operations on patient profiles, including demographic details and contact information.
    
*   **Personnel Management**: CRUD operations for Doctors, Nurses, and Admin Staff.
    
*   **Treatment Tracking**: Assign treatments to patients with notes, date, and responsible medical staff.
    
*   **File Uploads**: Secure storage of patient-related documents (e.g., lab reports, imaging files) with upload history.
    
*   **Billing & Payments**: Issue invoices, record payments (cash, card, insurance), track invoice status, and generate reports.
    
*   **Insurance Integration**: Manage insurance providers and link patient coverage policies.
    
*   **Diagrams & Documentation**: Full set of UML (Use Case, Class, Sequence) and ER (Conceptual, Logical, Physical) diagrams to support design and discussions.
    

Installation & Setup
--------------------

1.  git clone https://github.com/your-org/meditech-healthcare-system.gitcd meditech-healthcare-system
    
2.  **Database Setup**:
    
    *   Install MySQL Server and Workbench.
        
    *   Execute schema/meditech\_schema.sql to create the meditech database and all tables.
        
    *   Confirm tables are in 3NF and populated with sample data.
        
3.  **Application Setup**:
    
    *   Install [Visual Studio Community Edition](https://visualstudio.microsoft.com/).
        
    *   Open MeditechApp.sln in Visual Studio.
        
    *   Restore NuGet packages (Connector/NET for MySQL).
        
    *   Build and run the solution.
        
4.  **Credentials**:
    
    *   Default admin user: admin / Admin@123 (change on first login).
        
    *   Configure database connection string in appsettings.json.
        

Milestones & Timeframe
----------------------

MilestoneDurationOwner(s)Status1. Database Schema Design & SetupDay 1 – Day 3Student 1✅ Completed (SQL Schema Done)2. UML & ER Diagram CreationDay 2 – Day 4Both✅ Partial (Use Case & ERD Done)3. C# Application Skeleton & SetupDay 4 – Day 6Student 2⏳ In Progress4. CRUD & Stored ProceduresDay 6 – Day 8Both⏳ Upcoming5. File Upload & Billing ModuleDay 8 – Day 10Both⏳ Upcoming6. Testing & Bug FixingDay 10 – Day 12Both⏳ Upcoming7. Report & Presentation PrepDay 12 – Day 14Both⏳ Upcoming

> **Note:** This schedule assumes 6 hours of dedicated work each evening per student. Adjustments will be updated daily based on progress.

Current Progress (as of Day 3)
------------------------------

*   ✅ Database schema fully implemented in MySQL (3NF compliant)
    
*   ✅ Conceptual, Logical, and Physical ER diagrams created
    
*   ✅ Initial Use Case diagrams drafted
    
*   🕓 C# application project created; initial connection test to DB scheduled for Day 4
    

How to Contribute
-----------------

*   Fork the repository and create a feature branch: git checkout -b feature/my-feature
    
*   Commit your changes: \`git commit -m "Add new feature"
    
*   Push to the branch: git push origin feature/my-feature
    
*   Open a Pull Request for review.
    

Documentation
-------------

*   **ER Diagrams**: /docs/ERD\_conceptual.png, /docs/ERD\_logical.png, /docs/ERD\_physical.png
    
*   **UML Diagrams**: /docs/UseCaseDiagram.png, /docs/ClassDiagram.png, /docs/SequenceDiagram.png
    
*   **Test Logs**: /docs/test\_log.xlsx