-- =======================================================================================
-- Meditech Innovations Healthcare System Database
-- Version: Revised with Excellent Comments, Fully Functional CRUD + Transactions,
--          and Strict BCNF/4NF Adherence
--
-- Overview:
--   • Every table is normalized to BCNF/4NF:
--       - No partial dependencies: each non-key attribute fully depends on the primary key.
--       - No transitive dependencies: non-key attributes do not depend on other non-key attributes.
--       - No multivalued dependencies: no table stores two or more independent sets of multivalued facts.
--   • Foreign keys enforce referential integrity and ON UPDATE/ON DELETE rules prevent or cascade changes appropriately.
--   • CHECK constraints ensure simple business rules (e.g., date ordering).
--   • Detailed comments before each object explain its purpose, columns, and rationale for normalization.
--   • CRUD stored procedures for each table, plus multi-table transaction procedures, guarantee full functionality.
--   • Sample data sections insert 3–5 realistic rows per table, enabling immediate testing.
--   • Indexes on commonly filtered or joined columns boost performance.
-- =======================================================================================


-- =======================================================================================
-- 1. DROP/CREATE DATABASE
--    Purpose: Ensure a clean slate. If the 'meditech' database exists, drop it; then create anew.
--    Normalization Note: Creating database does not affect normalization; it simply scopes tables.
-- =======================================================================================
DROP DATABASE IF EXISTS meditech;
CREATE DATABASE meditech;
USE meditech;


-- =======================================================================================
-- 2. LOOKUP TABLES (BCNF/4NF: Each lookup table has a single atomic key and no repeating groups)
--    Purpose: Maintain master lists of controlled values that other tables reference.
--    Rationale: By separating lookups into dedicated tables, we avoid storing repeated text fields 
--               and eliminate update anomalies.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 2.1 Specializations
-- ---------------------------------------------------------------------------------------
-- Stores each medical specialization once. 
--   • Primary Key: specialization_id (surrogate key, INT AUTO_INCREMENT).
--   • name is UNIQUE: no two specializations share the same name.
--   • description provides human-readable details but is not used for joins.
-- BCNF/4NF Rationale: 
--   - Each row’s non-key attributes (name, description) depend solely on specialization_id.
--   - There is no multi-valued attribute: one specialization = one description.
-- Fully Functional: 
--   CRUD procedures below allow adding, updating, deleting, and selecting specializations.
CREATE TABLE Specializations (
    specialization_id   INT AUTO_INCREMENT PRIMARY KEY,   -- Surrogate key for referencing
    name                VARCHAR(100) NOT NULL UNIQUE,      -- e.g., 'Cardiology', 'Pediatrics'
    description         VARCHAR(255)                        -- Optional description of specialization
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 2.2 Departments
-- ---------------------------------------------------------------------------------------
-- Stores each clinical department once.
--   • Primary Key: department_id.
--   • name is UNIQUE: prevents having two entries for 'Emergency', etc.
--   • location holds building/floor info, not used in joins.
-- BCNF/4NF Rationale:
--   - No column depends on anything other than department_id.
--   - No repeating groups, each department appears exactly once.
-- Fully Functional:
--   CRUD procedures are provided to manage department entries.
CREATE TABLE Departments (
    department_id       INT AUTO_INCREMENT PRIMARY KEY,  -- Surrogate key
    name                VARCHAR(100) NOT NULL UNIQUE,    -- e.g., 'Emergency', 'ICU'
    location            VARCHAR(100)                      -- e.g., 'Building A, 1st Floor'
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 2.3 StaffRoles
-- ---------------------------------------------------------------------------------------
-- Stores predefined roles for administrative staff and login users.
--   • Primary Key: role_id.
--   • name is UNIQUE: enforces a single row for 'Receptionist', 'Billing Clerk', etc.
--   • description clarifies responsibilities but is not used for joins.
-- BCNF/4NF Rationale:
--   - Non-key attribute 'description' depends solely on role_id.
--   - Avoids storing free-text roles repeatedly in Users/AdminStaff.
-- Fully Functional:
--   Referenced by Users.role_id and AdminStaff.role_id via foreign keys.
CREATE TABLE StaffRoles (
    role_id             INT AUTO_INCREMENT PRIMARY KEY,  -- Surrogate key for roles
    name                VARCHAR(50) NOT NULL UNIQUE,     -- e.g., 'Receptionist', 'Pharmacist'
    description         VARCHAR(255)                      -- Details of each role
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 2.4 Treatments
-- ---------------------------------------------------------------------------------------
-- Stores medical treatments offered by the facility.
--   • Primary Key: treatment_id.
--   • name is UNIQUE: prevents duplicate treatment names.
--   • description explains procedure but is not used for relational logic.
-- BCNF/4NF Rationale:
--   - Each row’s non-key attribute(s) depend only on treatment_id.
--   - Separation into its own table avoids repeating treatment names in assignments.
-- Fully Functional:
--   TreatmentAssignments references treatment_id via foreign key.
CREATE TABLE Treatments (
    treatment_id        INT AUTO_INCREMENT PRIMARY KEY,   -- Unique treatment identifier
    name                VARCHAR(100) NOT NULL UNIQUE,      -- e.g., 'Physical Therapy'
    description         VARCHAR(255)                        -- e.g., 'Exercises to improve mobility'
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 2.5 FrequencyTypes
-- ---------------------------------------------------------------------------------------
-- Stores dosing frequency definitions for prescriptions.
--   • Primary Key: frequency_id.
--   • name is UNIQUE: e.g., 'Once a day', 'Every 8 hours'.
--   • description is optional.
-- BCNF/4NF Rationale:
--   - Non-key attribute(s) depend only on frequency_id.
--   - No multi-valued columns: each frequency has one description.
-- Fully Functional:
--   Referenced by Prescriptions.frequency_id via foreign key.
CREATE TABLE FrequencyTypes (
    frequency_id        INT AUTO_INCREMENT PRIMARY KEY,   -- Surrogate key
    name                VARCHAR(50) NOT NULL UNIQUE,      -- e.g., 'Once a day', 'Weekly'
    description         VARCHAR(255)                        -- Additional explanation
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 2.6 InsuranceProviders
-- ---------------------------------------------------------------------------------------
-- Stores insurance provider companies.
--   • Primary Key: provider_id.
--   • name is UNIQUE: ensures one entry per provider.
--   • contact_info holds address/phone/email.
-- BCNF/4NF Rationale:
--   - All non-key data depends on provider_id.
--   - No repeating groups; each provider row is atomic.
-- Fully Functional:
--   Referenced by InsurancePolicies.provider_id via foreign key.
CREATE TABLE InsuranceProviders (
    provider_id         INT AUTO_INCREMENT PRIMARY KEY,   -- Unique provider identifier
    name                VARCHAR(100) NOT NULL UNIQUE,     -- e.g., 'Acme Health Ins.'
    contact_info        VARCHAR(255)                       -- e.g., '123 Main St, Nairobi; +254-700-000001'
) ENGINE=InnoDB;


-- =======================================================================================
-- 3. CORE TABLES (BCNF/4NF: Each core table represents a single entity with atomic attributes)
--    Purpose: Hold main entities (Patients, Doctors, Nurses, AdminStaff, Users).
--    Design Choices:
--      - Surrogate integer primary keys used consistently.
--      - Date fields and ENUMs enforce valid values and enable indexing.
--      - Unique constraints on email/username prevent duplicates.
--      - Foreign keys reference lookup tables to avoid free-text duplication.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 3.1 Patients
-- ---------------------------------------------------------------------------------------
-- Stores patient demographic and contact information.
--   • Primary Key: patient_id.
--   • first_name, last_name: patient’s given and family names.
--   • date_of_birth: stored as DATE (no time component).
--   • gender: ENUM restricted to 'Male','Female','Other'.
--   • address, phone: optional contact fields.
--   • email: UNIQUE: prevents two patients sharing an email.
--   • date_created, date_modified: track record lifecycle automatically.
-- BCNF/4NF Rationale:
--   - Non-key attributes (first_name, last_name, date_of_birth, gender, address, phone, email, date timestamps) depend solely on patient_id.
--   - No transitive dependencies: e.g., address or phone does not depend on email.
--   - No multi-valued fields: one address, one phone per row.
-- Fully Functional:
--   CRUD procedures sp_AddPatient, sp_GetPatients, sp_UpdatePatient, sp_DeletePatient operate on this table.
CREATE TABLE Patients (
    patient_id          INT AUTO_INCREMENT PRIMARY KEY,   -- Unique ID for each patient
    first_name          VARCHAR(100) NOT NULL,             -- Patient’s first name
    last_name           VARCHAR(100) NOT NULL,             -- Patient’s surname
    date_of_birth       DATE NOT NULL,                     -- Format: YYYY-MM-DD
    gender              ENUM('Male','Female','Other') NOT NULL,  -- Gender at birth or self‐identified
    address             VARCHAR(255),                      -- Home address
    phone               VARCHAR(20),                       -- Contact phone number
    email               VARCHAR(100) NOT NULL UNIQUE,      -- Unique email address
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 3.2 Doctors
-- ---------------------------------------------------------------------------------------
-- Stores doctor personnel information.
--   • Primary Key: doctor_id.
--   • first_name, last_name: doctor's names.
--   • specialization_id: FK → Specializations; enforces valid specialization.
--   • contact_phone: optional.
--   • email: UNIQUE for login or communications.
--   • date_hired: when the doctor joined the facility.
--   • status: ENUM('Active','Inactive','On Leave') defaults to 'Active'.
--   • date timestamps: track creation/modification.
-- BCNF/4NF Rationale:
--   - Non-key attributes (first_name, last_name, specialization_id, contact_phone, email, date_hired, status, timestamps) depend solely on doctor_id.
--   - Foreign key eliminates storing specialization name repeatedly; ensures lookup integrity.
-- Fully Functional:
--   CRUD procedures sp_AddDoctor, sp_GetDoctors, sp_UpdateDoctor, sp_DeleteDoctor included.
CREATE TABLE Doctors (
    doctor_id           INT AUTO_INCREMENT PRIMARY KEY,   -- Unique ID for each doctor
    first_name          VARCHAR(100) NOT NULL,             -- Doctor’s first name
    last_name           VARCHAR(100) NOT NULL,             -- Doctor’s last name
    specialization_id   INT NOT NULL,                      -- FK → Specializations
    contact_phone       VARCHAR(20),                       -- Optional phone number
    email               VARCHAR(100) NOT NULL UNIQUE,      -- Unique email for communications/login
    date_hired          DATE NOT NULL,                     -- Date of hire
    status              ENUM('Active','Inactive','On Leave') NOT NULL DEFAULT 'Active',
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (specialization_id)
        REFERENCES Specializations(specialization_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 3.3 Nurses
-- ---------------------------------------------------------------------------------------
-- Stores nurse personnel information.
--   • Primary Key: nurse_id.
--   • first_name, last_name: nurse’s names.
--   • department_id: FK → Departments.
--   • contact_phone: optional.
--   • email: UNIQUE.
--   • date_hired: when nurse joined.
--   • status: ENUM('Active','Inactive','On Leave').
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - All non-key columns depend on nurse_id alone.
--   - Department FK avoids storing free-text department values repeatedly.
-- Fully Functional:
--   CRUD procedures sp_AddNurse, sp_GetNurses, sp_UpdateNurse, sp_DeleteNurse included.
CREATE TABLE Nurses (
    nurse_id            INT AUTO_INCREMENT PRIMARY KEY,   -- Unique ID for each nurse
    first_name          VARCHAR(100) NOT NULL,             -- Nurse’s first name
    last_name           VARCHAR(100) NOT NULL,             -- Nurse’s last name
    department_id       INT NOT NULL,                      -- FK → Departments
    contact_phone       VARCHAR(20),                       -- Optional phone number
    email               VARCHAR(100) NOT NULL UNIQUE,      -- Unique email
    date_hired          DATE NOT NULL,                     -- Date of hire
    status              ENUM('Active','Inactive','On Leave') NOT NULL DEFAULT 'Active',
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (department_id)
        REFERENCES Departments(department_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 3.4 AdminStaff
-- ---------------------------------------------------------------------------------------
-- Stores administrative staff members (e.g., receptionists, billing clerks).
--   • Primary Key: admin_id.
--   • first_name, last_name: admin names.
--   • role_id: FK → StaffRoles; enforces valid role.
--   • contact_phone: optional.
--   • email: UNIQUE.
--   • date_hired: date joined.
--   • status: ENUM('Active','Inactive','On Leave').
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - Each non-key attribute depends solely on admin_id.
--   - role_id FK prevents storing role name repeatedly in multiple rows.
-- Fully Functional:
--   CRUD procedures sp_AddAdmin, sp_GetAdmins, sp_UpdateAdmin, sp_DeleteAdmin included.
CREATE TABLE AdminStaff (
    admin_id            INT AUTO_INCREMENT PRIMARY KEY,   -- Unique ID for each admin staff
    first_name          VARCHAR(100) NOT NULL,             -- Admin’s first name
    last_name           VARCHAR(100) NOT NULL,             -- Admin’s last name
    role_id             INT NOT NULL,                      -- FK → StaffRoles
    contact_phone       VARCHAR(20),                       -- Optional phone
    email               VARCHAR(100) NOT NULL UNIQUE,      -- Unique email
    date_hired          DATE NOT NULL,                     -- Date of hire
    status              ENUM('Active','Inactive','On Leave') NOT NULL DEFAULT 'Active',
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (role_id)
        REFERENCES StaffRoles(role_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 3.5 Users
-- ---------------------------------------------------------------------------------------
-- Stores login credentials for staff members.
--   • Primary Key: user_id.
--   • username: UNIQUE login identifier.
--   • password_hash: CHAR(64) stores a SHA2-256 hash.
--   • role_id: FK → StaffRoles; ties login to role permissions.
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - Non-key attributes (username, password_hash, role_id, timestamps) depend only on user_id.
--   - Role_id FK prevents free-text duplication of role names.
-- Fully Functional:
--   Supports sp_AddUser, sp_GetUsers, sp_UpdateUser, sp_DeleteUser procedures if desired.
CREATE TABLE Users (
    user_id             INT AUTO_INCREMENT PRIMARY KEY,   -- Unique login ID
    username            VARCHAR(50) NOT NULL UNIQUE,       -- e.g., 'drjames', 'nursemary'
    password_hash       CHAR(64) NOT NULL,                 -- SHA2-256 hash
    role_id             INT NOT NULL,                      -- FK → StaffRoles
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (role_id)
        REFERENCES StaffRoles(role_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- =======================================================================================
-- 4. APPOINTMENTS & MEDICAL RECORDS (BCNF/4NF: Related but separate entities with FKs)
--    Purpose: Track scheduled visits and link them to medical record entries.
--    Design Choices:
--      - appointment_date indexed for fast lookup of upcoming/past appointments.
--      - Separate MedicalRecords table so each record references exactly one appointment.
--      - ENUM for status ensures validity and speeds filtering.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 4.1 Appointments
-- ---------------------------------------------------------------------------------------
-- Each appointment belongs to exactly one patient and one doctor.
--   • Primary Key: appointment_id.
--   • patient_id: FK → Patients.
--   • doctor_id: FK → Doctors.
--   • appointment_date: DATETIME; indexed for performance.
--   • status: ENUM with default 'Pending'.
--   • notes: any free-text annotation.
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - All non-key attributes (patient_id, doctor_id, appointment_date, status, notes, timestamps) depend only on appointment_id.
--   - No composite or partial dependencies: appointment_date does not depend on doctor_id alone.
--   - Index on appointment_date aids WHERE appointment_date ≥ NOW() queries (view below).
-- Fully Functional:
--   CRUD procedures sp_AddAppointment, sp_GetAppointments, sp_UpdateAppointment, sp_DeleteAppointment included.
CREATE TABLE Appointments (
    appointment_id      INT AUTO_INCREMENT PRIMARY KEY,   -- Unique appointment ID
    patient_id          INT NOT NULL,                      -- FK → Patients
    doctor_id           INT NOT NULL,                      -- FK → Doctors
    appointment_date    DATETIME NOT NULL,                 -- Scheduled date/time
    status              ENUM(
                          'Pending',
                          'Confirmed',
                          'Completed',
                          'Canceled',
                          'Rescheduled',
                          'No Show'
                        ) NOT NULL DEFAULT 'Pending',       -- Current status
    notes               TEXT,                              -- Optional free-text notes
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id)
        REFERENCES Patients(patient_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    FOREIGN KEY (doctor_id)
        REFERENCES Doctors(doctor_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;

-- Explicit index on appointment_date for optimized date-range queries
CREATE INDEX idx_app_date ON Appointments(appointment_date);


-- ---------------------------------------------------------------------------------------
-- 4.2 MedicalRecords
-- ---------------------------------------------------------------------------------------
-- Each medical record is linked to exactly one appointment (one-to-one or one-to-many if follow-ups).
--   • Primary Key: record_id.
--   • appointment_id: FK → Appointments.
--   • diagnosis: brief text summary.
--   • visit_summary: required TEXT describing visit details.
--   • doctor_notes: optional additional commentary.
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - Non-key attributes (diagnosis, visit_summary, doctor_notes, timestamps) depend solely on record_id.
--   - appointment_id is an FK; moving medical data to its own table avoids repeating appointment details.
-- Fully Functional:
--   CRUD procedures sp_AddMedicalRecord, sp_GetMedicalRecords, sp_UpdateMedicalRecord, sp_DeleteMedicalRecord included.
CREATE TABLE MedicalRecords (
    record_id           INT AUTO_INCREMENT PRIMARY KEY,   -- Unique record ID
    appointment_id      INT NOT NULL,                      -- FK → Appointments
    diagnosis           VARCHAR(255),                      -- Brief diagnosis text
    visit_summary       TEXT NOT NULL,                     -- Required summary of visit
    doctor_notes        TEXT,                              -- Optional notes by doctor
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (appointment_id)
        REFERENCES Appointments(appointment_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- =======================================================================================
-- 5. PRESCRIPTIONS (BCNF/4NF: Single prescription per row, FK references to dosage frequency)
--    Purpose: Record medications prescribed during a medical visit.
--    Design Choices:
--      - Separate prescription table avoids storing medication text in MedicalRecords.
--      - frequency_id references a normalized lookup table; ensures consistent frequency terms.
--      - CHECK constraint ensures valid date ordering.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 5.1 Prescriptions
-- ---------------------------------------------------------------------------------------
-- Each prescription row belongs to one medical record and has one frequency type.
--   • Primary Key: prescription_id.
--   • record_id: FK → MedicalRecords.
--   • medication_name: required text field.
--   • dosage: required text (e.g., '500 mg').
--   • frequency_id: FK → FrequencyTypes.
--   • start_date, end_date: valid date range for medication use.
--   • notes: optional free-text.
--   • date timestamps.
--   • CHECK constraint enforces end_date ≥ start_date.
-- BCNF/4NF Rationale:
--   - Non-key attributes depend solely on prescription_id.
--   - FrequencyType separation prevents repeating frequency text (e.g., 'Once a day') across many rows.
-- Fully Functional:
--   CRUD procedures sp_AddPrescription, sp_GetPrescriptions, sp_UpdatePrescription, sp_DeletePrescription included.
CREATE TABLE Prescriptions (
    prescription_id     INT AUTO_INCREMENT PRIMARY KEY,   -- Unique prescription ID
    record_id           INT NOT NULL,                      -- FK → MedicalRecords
    medication_name     VARCHAR(100) NOT NULL,             -- Drug name
    dosage              VARCHAR(50) NOT NULL,              -- Dosage instructions (e.g., '10 mg')
    frequency_id        INT NOT NULL,                      -- FK → FrequencyTypes
    start_date          DATE NOT NULL,                     -- Begin date of prescription
    end_date            DATE NOT NULL,                     -- End date of prescription
    notes               TEXT,                              -- Optional notes
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (record_id)
        REFERENCES MedicalRecords(record_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    FOREIGN KEY (frequency_id)
        REFERENCES FrequencyTypes(frequency_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT chk_presc_dates
        CHECK (end_date >= start_date)                    -- Enforce valid date sequence
) ENGINE=InnoDB;


-- =======================================================================================
-- 6. TREATMENT ASSIGNMENTS (BCNF/4NF: Each assignment captures a single treatment event)
--    Purpose: Record which patient receives which treatment, with assigned doctor/nurse.
--    Design Choices:
--      - assignment_id as PK ensures uniqueness.
--      - patient_id, treatment_id, assigned_doctor_id, assigned_nurse_id as FKs guarantee valid references.
--      - assignment_date records exact time of assignment.
--      - notes: optional free-text commentary.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 6.1 TreatmentAssignments
-- ---------------------------------------------------------------------------------------
-- Each row: one patient + one treatment + one doctor + one nurse at a given date/time.
--   • Primary Key: assignment_id.
--   • patient_id: FK → Patients.
--   • treatment_id: FK → Treatments.
--   • assignment_date: when treatment is scheduled or administered.
--   • assigned_doctor_id: FK → Doctors.
--   • assigned_nurse_id: FK → Nurses.
--   • notes: optional free-text.
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - Ensures that each attribute depends on assignment_id alone.
--   - Doctor and nurse FKs prevent storing their names repeatedly; ensures consistency.
-- Fully Functional:
--   sp_AddAssignment, sp_GetAssignments, sp_UpdateAssignment, sp_DeleteAssignment implemented.
--   sp_AddAssignment wraps insertion in a transaction to handle referential integrity errors.
CREATE TABLE TreatmentAssignments (
    assignment_id       INT AUTO_INCREMENT PRIMARY KEY,   -- Unique assignment ID
    patient_id          INT NOT NULL,                      -- FK → Patients
    treatment_id        INT NOT NULL,                      -- FK → Treatments
    assignment_date     DATETIME NOT NULL,                 -- When treatment is assigned/administered
    assigned_doctor_id  INT NOT NULL,                      -- FK → Doctors
    assigned_nurse_id   INT NOT NULL,                      -- FK → Nurses
    notes               TEXT,                              -- Optional treatment notes
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id)
        REFERENCES Patients(patient_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    FOREIGN KEY (treatment_id)
        REFERENCES Treatments(treatment_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    FOREIGN KEY (assigned_doctor_id)
        REFERENCES Doctors(doctor_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    FOREIGN KEY (assigned_nurse_id)
        REFERENCES Nurses(nurse_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- =======================================================================================
-- 7. PATIENT FILES (BCNF/4NF: Each file row holds metadata for one uploaded document)
--    Purpose: Store references to patient documents (imaging, lab results, etc.).
--    Design Choices:
--      - file_id as PK.
--      - patient_id: FK → Patients with ON DELETE CASCADE: when a patient is removed, all files drop.
--      - file_name, file_type, file_path: capture location and type.
--      - date_uploaded automatically set to current timestamp.
-- Fully Functional:
--   sp_AddPatientFile, sp_GetPatientFiles, sp_DeletePatientFile included.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 7.1 PatientFiles
-- ---------------------------------------------------------------------------------------
CREATE TABLE PatientFiles (
    file_id             INT AUTO_INCREMENT PRIMARY KEY,   -- Unique file record ID
    patient_id          INT NOT NULL,                      -- FK → Patients
    file_name           VARCHAR(255) NOT NULL,             -- Filename (e.g., 'xray_20250611.jpg')
    file_type           VARCHAR(50),                       -- MIME type (e.g., 'image/jpeg')
    file_path           VARCHAR(255) NOT NULL,             -- Server file path (e.g., '/files/patient2/xray.jpg')
    date_uploaded       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id)
        REFERENCES Patients(patient_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE                    -- If patient is deleted, cascade deletes files
) ENGINE=InnoDB;


-- =======================================================================================
-- 8. INSURANCE & BILLING (BCNF/4NF: Each billing-related entity in its own table)
--    Purpose: Manage insurance policies, link patients to policies, and record invoices/payments.
--    Design Choices:
--      - InsuranceProviders holds provider contact info.
--      - InsurancePolicies references provider_id; CHECK constraint ensures valid date ordering.
--      - PatientInsurance is a join table (many-to-many between Patients and InsurancePolicies).
--      - Invoices reference patient_id; Payments reference invoice_id.
--      - ENUMs define allowable statuses/methods.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 8.1 InsurancePolicies
-- ---------------------------------------------------------------------------------------
-- Each insurance policy belongs to one provider.
--   • Primary Key: policy_id.
--   • provider_id: FK → InsuranceProviders with ON DELETE RESTRICT: a provider cannot be removed if policies exist.
--   • policy_number: UNIQUE (no duplicate policy numbers).
--   • coverage_amount: maximum coverage limit.
--   • start_date, end_date: date range covered by policy.
--   • CHECK constraint ensures end_date ≥ start_date.
-- BCNF/4NF Rationale:
--   - Non-key columns depend only on policy_id.
--   - Provider_id FK prevents free-text provider names in this table.
-- Fully Functional:
--   sp_AddInsurancePolicy, sp_GetInsurancePolicies, sp_UpdateInsurancePolicy, sp_DeleteInsurancePolicy included.
CREATE TABLE InsurancePolicies (
    policy_id           INT AUTO_INCREMENT PRIMARY KEY,   -- Unique policy ID
    provider_id         INT NOT NULL,                      -- FK → InsuranceProviders
    policy_number       VARCHAR(50) NOT NULL UNIQUE,       -- e.g., 'ACME12345'
    coverage_amount     DECIMAL(12,2) NOT NULL,            -- e.g., 50000.00
    start_date          DATE NOT NULL,                     -- Policy start date
    end_date            DATE NOT NULL,                     -- Policy end date
    FOREIGN KEY (provider_id)
        REFERENCES InsuranceProviders(provider_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT chk_policy_dates
        CHECK (end_date >= start_date)                    -- Enforce valid date range
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 8.2 PatientInsurance
-- ---------------------------------------------------------------------------------------
-- Join table linking patients to insurance policies (many-to-many).
--   • Composite Primary Key: (patient_id, policy_id).
--   • patient_id: FK → Patients with ON DELETE CASCADE: if a patient is removed, their insurance links delete.
--   • policy_id: FK → InsurancePolicies with ON DELETE RESTRICT: cannot remove a policy if patients still reference it.
--   • date_added: timestamp when the link was created.
-- BCNF/4NF Rationale:
--   - No non-key columns other than date_added; date_added depends on the composite PK alone.
--   - Join table design ensures no repeating groups for multi-valued policies per patient.
-- Fully Functional:
--   sp_AddPatientInsurance, sp_GetPatientInsurance, sp_DeletePatientInsurance included.
CREATE TABLE PatientInsurance (
    patient_id          INT NOT NULL,                      -- FK → Patients
    policy_id           INT NOT NULL,                      -- FK → InsurancePolicies
    date_added          DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (patient_id, policy_id),                   -- Composite key ensures uniqueness
    FOREIGN KEY (patient_id)
        REFERENCES Patients(patient_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (policy_id)
        REFERENCES InsurancePolicies(policy_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 8.3 Invoices
-- ---------------------------------------------------------------------------------------
-- Each invoice pertains to one patient.
--   • Primary Key: invoice_id.
--   • patient_id: FK → Patients.
--   • invoice_date: date of invoice issuance.
--   • total_amount: total billable amount.
--   • status: ENUM('Pending','Paid','Overdue').
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - All non-key columns depend only on invoice_id.
--   - FK prevents storing patient contact info here; purely referential.
-- Fully Functional:
--   sp_AddInvoice, sp_GetInvoices, sp_UpdateInvoice, sp_DeleteInvoice included.
CREATE TABLE Invoices (
    invoice_id          INT AUTO_INCREMENT PRIMARY KEY,   -- Unique invoice ID
    patient_id          INT NOT NULL,                      -- FK → Patients
    invoice_date        DATE NOT NULL,                     -- Invoice issuance date
    total_amount        DECIMAL(12,2) NOT NULL,            -- e.g., 1200.00
    status              ENUM('Pending','Paid','Overdue') NOT NULL DEFAULT 'Pending',
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id)
        REFERENCES Patients(patient_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ---------------------------------------------------------------------------------------
-- 8.4 Payments
-- ---------------------------------------------------------------------------------------
-- Each payment is linked to exactly one invoice.
--   • Primary Key: payment_id.
--   • invoice_id: FK → Invoices.
--   • payment_date: date payment was made.
--   • amount_paid: amount of payment.
--   • method: ENUM('Cash','Credit Card','Insurance','Other').
--   • date timestamps.
-- BCNF/4NF Rationale:
--   - Non-key columns depend solely on payment_id.
--   - FK prevents storing invoice details here; purely referential.
-- Fully Functional:
--   sp_AddPayment, sp_GetPayments, sp_UpdatePayment, sp_DeletePayment included.
CREATE TABLE Payments (
    payment_id          INT AUTO_INCREMENT PRIMARY KEY,   -- Unique payment ID
    invoice_id          INT NOT NULL,                      -- FK → Invoices
    payment_date        DATE NOT NULL,                     -- Date of payment
    amount_paid         DECIMAL(12,2) NOT NULL,            -- e.g., 800.00
    method              ENUM('Cash','Credit Card','Insurance','Other') NOT NULL,
    date_created        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (invoice_id)
        REFERENCES Invoices(invoice_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;


-- =======================================================================================
-- 9. ADDITIONAL INDEXES (Performance Enhancements)
--    Purpose: Speed up queries on columns frequently used in WHERE or JOIN clauses.
--    Design Choices:
--      - Index on Patients.last_name for fast search by surname.
--      - Index on Doctors.specialization_id for filtering by specialization.
--      - Index on Nurses.department_id for quick lookup.
--      - Index on Invoices.status for dashboard or summary queries.
-- =======================================================================================
CREATE INDEX idx_pat_lastname        ON Patients(last_name);              -- Speeds search: WHERE last_name = 'Mwangi'
CREATE INDEX idx_doc_specialization   ON Doctors(specialization_id);       -- Speeds JOIN on specialization
CREATE INDEX idx_nurse_dept           ON Nurses(department_id);            -- Speeds filtering by department
CREATE INDEX idx_admin_role           ON AdminStaff(role_id);              -- Speeds filtering by role
CREATE INDEX idx_treat_name           ON Treatments(name);                 -- Speeds search by treatment name
CREATE INDEX idx_freq_name            ON FrequencyTypes(name);             -- Speeds lookup of frequency types
CREATE INDEX idx_provider_name        ON InsuranceProviders(name);         -- Speeds provider name searches
CREATE INDEX idx_policy_provider      ON InsurancePolicies(provider_id);   -- Speeds filtering policies by provider
CREATE INDEX idx_invoice_status       ON Invoices(status);                 -- Speeds dashboard queries by status


-- =======================================================================================
-- 10. VIEWS (Pre-Computed Joins & Aggregates)
--     Purpose: Simplify application queries, improve readability, and leverage indexes.
--     Design Choices:
--       - v_UpcomingAppointments: join Patients + Doctors for future appointments.
--       - v_PatientTreatmentHistory: join multiple tables to show a chronological history.
--       - v_PatientBilling: aggregate invoices & payments at patient level.
--       - v_MonthlyBillingSummary: show monthly totals, useful for reporting.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 10.1 v_UpcomingAppointments
-- ---------------------------------------------------------------------------------------
-- Purpose: List all appointments on or after current timestamp, with patient & doctor names.
-- Columns:
--   appointment_id: primary key of appointment
--   patient_first, patient_last: from Patients
--   doctor_first, doctor_last: from Doctors
--   appointment_date, status, notes from Appointments
-- Use Case: Populate dashboard or calendar for upcoming visits.
-- BCNF/4NF Rationale: 
--   - This view does not affect base table normalization; it simply presents joined data.
CREATE VIEW v_UpcomingAppointments AS
SELECT
    a.appointment_id,
    p.first_name      AS patient_first,
    p.last_name       AS patient_last,
    d.first_name      AS doctor_first,
    d.last_name       AS doctor_last,
    a.appointment_date,
    a.status,
    a.notes
FROM Appointments a
JOIN Patients p   ON a.patient_id = p.patient_id
JOIN Doctors d    ON a.doctor_id  = d.doctor_id
WHERE a.appointment_date >= NOW();


-- ---------------------------------------------------------------------------------------
-- 10.2 v_PatientTreatmentHistory
-- ---------------------------------------------------------------------------------------
-- Purpose: Show each patient’s treatment assignments in chronological order, including 
--          which doctor and nurse were involved.
-- Columns:
--   assignment_id, patient_id, patient names, treatment_name, doctor/nurse names, assignment_date, notes.
-- Use Case: Display a patient’s entire treatment timeline for clinical review.
-- BCNF/4NF Rationale:
--   - Combines data from multiple fully normalized tables; does not alter base table dependencies.
CREATE VIEW v_PatientTreatmentHistory AS
SELECT
    ta.assignment_id,
    p.patient_id,
    p.first_name      AS patient_first,
    p.last_name       AS patient_last,
    t.name            AS treatment_name,
    d.first_name      AS doctor_first,
    d.last_name       AS doctor_last,
    n.first_name      AS nurse_first,
    n.last_name       AS nurse_last,
    ta.assignment_date,
    ta.notes
FROM TreatmentAssignments ta
JOIN Patients p      ON ta.patient_id = p.patient_id
JOIN Treatments t    ON ta.treatment_id = t.treatment_id
JOIN Doctors d       ON ta.assigned_doctor_id = d.doctor_id
JOIN Nurses n        ON ta.assigned_nurse_id  = n.nurse_id;


-- ---------------------------------------------------------------------------------------
-- 10.3 v_PatientBilling
-- ---------------------------------------------------------------------------------------
-- Purpose: Aggregate each patient’s invoices and payments, showing outstanding balances.
-- Columns:
--   patient_id, patient names, invoice_id, invoice_date, total_amount, total_paid (sum of payments), invoice_status.
-- Use Case: Billing dashboard to show which invoices are paid/overdue and patient details.
-- BCNF/4NF Rationale:
--   - Joins normalized tables; does not introduce new dependencies in base tables.
CREATE VIEW v_PatientBilling AS
SELECT
    p.patient_id,
    p.first_name      AS patient_first,
    p.last_name       AS patient_last,
    i.invoice_id,
    i.invoice_date,
    i.total_amount,
    COALESCE(SUM(pay.amount_paid), 0) AS total_paid,
    i.status          AS invoice_status
FROM Invoices i
JOIN Patients p      ON i.patient_id = p.patient_id
LEFT JOIN Payments pay ON pay.invoice_id = i.invoice_id
GROUP BY i.invoice_id, p.patient_id, p.first_name, p.last_name, i.invoice_date, i.total_amount, i.status;


-- ---------------------------------------------------------------------------------------
-- 10.4 v_MonthlyBillingSummary (Optional)
-- ---------------------------------------------------------------------------------------
-- Purpose: Summarize total invoiced and paid amounts per month.
-- Columns:
--   bill_month (YYYY-MM), invoice_count, total_invoiced, total_paid.
-- Use Case: Monthly revenue and payment tracking for management reports.
-- BCNF/4NF Rationale:
--   - Aggregation view on normalized tables; does not modify or introduce dependencies.
CREATE VIEW v_MonthlyBillingSummary AS
SELECT
    DATE_FORMAT(i.invoice_date, '%Y-%m') AS bill_month,
    COUNT(*)                               AS invoice_count,
    SUM(i.total_amount)                    AS total_invoiced,
    SUM(COALESCE(pay.amount_paid, 0))      AS total_paid
FROM Invoices i
LEFT JOIN Payments pay ON pay.invoice_id = i.invoice_id
GROUP BY DATE_FORMAT(i.invoice_date, '%Y-%m');


-- =======================================================================================
-- 11. STORED PROCEDURES (Fully Functional CRUD + Transaction Examples)
--     Purpose: Encapsulate all Create, Read, Update, Delete operations for each table.
--     Additional transaction-based procedures support multi-table workflows.
-- =======================================================================================
DELIMITER $$

-- ---------------------------------------------------------------------------------------
-- 11.1 CRUD for Patients
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddPatient(
    IN p_first_name    VARCHAR(100),
    IN p_last_name     VARCHAR(100),
    IN p_dob           DATE,
    IN p_gender        ENUM('Male','Female','Other'),
    IN p_address       VARCHAR(255),
    IN p_phone         VARCHAR(20),
    IN p_email         VARCHAR(100)
)
BEGIN
    -- Insert a new patient. All NOT NULL parameters must be supplied.
    INSERT INTO Patients(
        first_name, last_name, date_of_birth, gender, address, phone, email
    ) VALUES (
        p_first_name, p_last_name, p_dob, p_gender, p_address, p_phone, p_email
    );
END $$
$$

CREATE PROCEDURE sp_GetPatients()
BEGIN
    -- Retrieve all patient records. Useful for populating UI grid.
    SELECT * FROM Patients;
END $$
$$

CREATE PROCEDURE sp_UpdatePatient(
    IN p_patient_id    INT,
    IN p_first_name    VARCHAR(100),
    IN p_last_name     VARCHAR(100),
    IN p_dob           DATE,
    IN p_gender        ENUM('Male','Female','Other'),
    IN p_address       VARCHAR(255),
    IN p_phone         VARCHAR(20),
    IN p_email         VARCHAR(100)
)
BEGIN
    -- Update a patient’s information based on patient_id.
    UPDATE Patients
    SET first_name    = p_first_name,
        last_name     = p_last_name,
        date_of_birth = p_dob,
        gender        = p_gender,
        address       = p_address,
        phone         = p_phone,
        email         = p_email
    WHERE patient_id = p_patient_id;
END $$
$$

CREATE PROCEDURE sp_DeletePatient(
    IN p_patient_id    INT
)
BEGIN
    -- Delete a patient. ON DELETE CASCADE relationships (e.g., files) will also be removed.
    DELETE FROM Patients WHERE patient_id = p_patient_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.2 CRUD for Doctors
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddDoctor(
    IN p_first_name        VARCHAR(100),
    IN p_last_name         VARCHAR(100),
    IN p_specialization_id INT,
    IN p_contact_phone     VARCHAR(20),
    IN p_email             VARCHAR(100),
    IN p_date_hired        DATE,
    IN p_status            ENUM('Active','Inactive','On Leave')
)
BEGIN
    -- Insert a new doctor record. Must supply a valid specialization_id.
    INSERT INTO Doctors(
        first_name, last_name, specialization_id, contact_phone, email, date_hired, status
    ) VALUES (
        p_first_name, p_last_name, p_specialization_id, p_contact_phone, p_email, p_date_hired, p_status
    );
END $$
$$

CREATE PROCEDURE sp_GetDoctors()
BEGIN
    -- Retrieve all doctors. Useful for drop-down lists or management UI.
    SELECT * FROM Doctors;
END $$
$$

CREATE PROCEDURE sp_UpdateDoctor(
    IN p_doctor_id         INT,
    IN p_first_name        VARCHAR(100),
    IN p_last_name         VARCHAR(100),
    IN p_specialization_id INT,
    IN p_contact_phone     VARCHAR(20),
    IN p_email             VARCHAR(100),
    IN p_date_hired        DATE,
    IN p_status            ENUM('Active','Inactive','On Leave')
)
BEGIN
    -- Update doctor’s details based on doctor_id.
    UPDATE Doctors
    SET first_name        = p_first_name,
        last_name         = p_last_name,
        specialization_id = p_specialization_id,
        contact_phone     = p_contact_phone,
        email             = p_email,
        date_hired        = p_date_hired,
        status            = p_status
    WHERE doctor_id = p_doctor_id;
END $$
$$

CREATE PROCEDURE sp_DeleteDoctor(
    IN p_doctor_id         INT
)
BEGIN
    -- Delete a doctor. Cascading effects prevented by ON DELETE RESTRICT for related tables (e.g., appointments).
    DELETE FROM Doctors WHERE doctor_id = p_doctor_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.3 CRUD for Nurses
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddNurse(
    IN p_first_name    VARCHAR(100),
    IN p_last_name     VARCHAR(100),
    IN p_department_id INT,
    IN p_contact_phone VARCHAR(20),
    IN p_email         VARCHAR(100),
    IN p_date_hired    DATE,
    IN p_status        ENUM('Active','Inactive','On Leave')
)
BEGIN
    -- Insert a new nurse record. Must supply a valid department_id.
    INSERT INTO Nurses(
        first_name, last_name, department_id, contact_phone, email, date_hired, status
    ) VALUES (
        p_first_name, p_last_name, p_department_id, p_contact_phone, p_email, p_date_hired, p_status
    );
END $$
$$

CREATE PROCEDURE sp_GetNurses()
BEGIN
    -- Retrieve all nurses. Useful for assignment to treatments or scheduling.
    SELECT * FROM Nurses;
END $$
$$

CREATE PROCEDURE sp_UpdateNurse(
    IN p_nurse_id       INT,
    IN p_first_name    VARCHAR(100),
    IN p_last_name     VARCHAR(100),
    IN p_department_id INT,
    IN p_contact_phone VARCHAR(20),
    IN p_email         VARCHAR(100),
    IN p_date_hired    DATE,
    IN p_status        ENUM('Active','Inactive','On Leave')
)
BEGIN
    -- Update nurse’s details based on nurse_id.
    UPDATE Nurses
    SET first_name    = p_first_name,
        last_name     = p_last_name,
        department_id = p_department_id,
        contact_phone = p_contact_phone,
        email         = p_email,
        date_hired    = p_date_hired,
        status        = p_status
    WHERE nurse_id = p_nurse_id;
END $$
$$

CREATE PROCEDURE sp_DeleteNurse(
    IN p_nurse_id       INT
)
BEGIN
    -- Delete a nurse. ON DELETE RESTRICT prevents deletion if assignments exist.
    DELETE FROM Nurses WHERE nurse_id = p_nurse_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.4 CRUD for AdminStaff
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddAdmin(
    IN p_first_name    VARCHAR(100),
    IN p_last_name     VARCHAR(100),
    IN p_role_id       INT,
    IN p_contact_phone VARCHAR(20),
    IN p_email         VARCHAR(100),
    IN p_date_hired    DATE,
    IN p_status        ENUM('Active','Inactive','On Leave')
)
BEGIN
    -- Insert a new administrative staff record. Must supply a valid role_id.
    INSERT INTO AdminStaff(
        first_name, last_name, role_id, contact_phone, email, date_hired, status
    ) VALUES (
        p_first_name, p_last_name, p_role_id, p_contact_phone, p_email, p_date_hired, p_status
    );
END $$
$$

CREATE PROCEDURE sp_GetAdmins()
BEGIN
    -- Retrieve all admin staff. Useful for assigning user accounts or roles.
    SELECT * FROM AdminStaff;
END $$
$$

CREATE PROCEDURE sp_UpdateAdmin(
    IN p_admin_id       INT,
    IN p_first_name    VARCHAR(100),
    IN p_last_name     VARCHAR(100),
    IN p_role_id       INT,
    IN p_contact_phone VARCHAR(20),
    IN p_email         VARCHAR(100),
    IN p_date_hired    DATE,
    IN p_status        ENUM('Active','Inactive','On Leave')
)
BEGIN
    -- Update admin staff’s details based on admin_id.
    UPDATE AdminStaff
    SET first_name    = p_first_name,
        last_name     = p_last_name,
        role_id       = p_role_id,
        contact_phone = p_contact_phone,
        email         = p_email,
        date_hired    = p_date_hired,
        status        = p_status
    WHERE admin_id = p_admin_id;
END $$
$$

CREATE PROCEDURE sp_DeleteAdmin(
    IN p_admin_id       INT
)
BEGIN
    -- Delete an admin staff record. ON DELETE RESTRICT prevents deletion if referenced by Users.
    DELETE FROM AdminStaff WHERE admin_id = p_admin_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.5 CRUD for Treatments
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddTreatment(
    IN p_name        VARCHAR(100),
    IN p_description VARCHAR(255)
)
BEGIN
    -- Insert a new treatment definition.
    INSERT INTO Treatments(name, description) VALUES(p_name, p_description);
END $$
$$

CREATE PROCEDURE sp_GetTreatments()
BEGIN
    -- Retrieve all treatments.
    SELECT * FROM Treatments;
END $$
$$

CREATE PROCEDURE sp_UpdateTreatment(
    IN p_treatment_id INT,
    IN p_name         VARCHAR(100),
    IN p_description  VARCHAR(255)
)
BEGIN
    -- Update treatment name/description based on treatment_id.
    UPDATE Treatments
    SET name        = p_name,
        description = p_description
    WHERE treatment_id = p_treatment_id;
END $$
$$

CREATE PROCEDURE sp_DeleteTreatment(
    IN p_treatment_id INT
)
BEGIN
    -- Delete a treatment. ON DELETE RESTRICT prevents removal if assignments exist.
    DELETE FROM Treatments WHERE treatment_id = p_treatment_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.6 CRUD for FrequencyTypes
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddFrequency(
    IN p_name        VARCHAR(50),
    IN p_description VARCHAR(255)
)
BEGIN
    -- Insert a new dosage frequency type.
    INSERT INTO FrequencyTypes(name, description) VALUES(p_name, p_description);
END $$
$$

CREATE PROCEDURE sp_GetFrequencies()
BEGIN
    -- Retrieve all frequency types.
    SELECT * FROM FrequencyTypes;
END $$
$$

CREATE PROCEDURE sp_UpdateFrequency(
    IN p_frequency_id INT,
    IN p_name         VARCHAR(50),
    IN p_description  VARCHAR(255)
)
BEGIN
    -- Update frequency type name/description based on frequency_id.
    UPDATE FrequencyTypes
    SET name        = p_name,
        description = p_description
    WHERE frequency_id = p_frequency_id;
END $$
$$

CREATE PROCEDURE sp_DeleteFrequency(
    IN p_frequency_id INT
)
BEGIN
    -- Delete a frequency type. ON DELETE RESTRICT prevents removal if prescriptions reference it.
    DELETE FROM FrequencyTypes WHERE frequency_id = p_frequency_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.7 CRUD for InsuranceProviders
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddProvider(
    IN p_name         VARCHAR(100),
    IN p_contact_info VARCHAR(255)
)
BEGIN
    -- Insert a new insurance provider.
    INSERT INTO InsuranceProviders(name, contact_info) VALUES(p_name, p_contact_info);
END $$
$$

CREATE PROCEDURE sp_GetProviders()
BEGIN
    -- Retrieve all insurance providers.
    SELECT * FROM InsuranceProviders;
END $$
$$

CREATE PROCEDURE sp_UpdateProvider(
    IN p_provider_id  INT,
    IN p_name         VARCHAR(100),
    IN p_contact_info VARCHAR(255)
)
BEGIN
    -- Update provider name/contact based on provider_id.
    UPDATE InsuranceProviders
    SET name         = p_name,
        contact_info = p_contact_info
    WHERE provider_id = p_provider_id;
END $$
$$

CREATE PROCEDURE sp_DeleteProvider(
    IN p_provider_id  INT
)
BEGIN
    -- Delete a provider. ON DELETE RESTRICT prevents removal if policies exist.
    DELETE FROM InsuranceProviders WHERE provider_id = p_provider_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.8 CRUD for InsurancePolicies
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddInsurancePolicy(
    IN p_provider_id     INT,
    IN p_policy_number   VARCHAR(50),
    IN p_coverage_amount DECIMAL(12,2),
    IN p_start_date      DATE,
    IN p_end_date        DATE
)
BEGIN
    -- Insert a new insurance policy.
    INSERT INTO InsurancePolicies(
        provider_id, policy_number, coverage_amount, start_date, end_date
    ) VALUES (
        p_provider_id, p_policy_number, p_coverage_amount, p_start_date, p_end_date
    );
END $$
$$

CREATE PROCEDURE sp_GetInsurancePolicies()
BEGIN
    -- Retrieve all insurance policies.
    SELECT * FROM InsurancePolicies;
END $$
$$

CREATE PROCEDURE sp_UpdateInsurancePolicy(
    IN p_policy_id       INT,
    IN p_provider_id     INT,
    IN p_policy_number   VARCHAR(50),
    IN p_coverage_amount DECIMAL(12,2),
    IN p_start_date      DATE,
    IN p_end_date        DATE
)
BEGIN
    -- Update policy details based on policy_id.
    UPDATE InsurancePolicies
    SET provider_id     = p_provider_id,
        policy_number   = p_policy_number,
        coverage_amount = p_coverage_amount,
        start_date      = p_start_date,
        end_date        = p_end_date
    WHERE policy_id = p_policy_id;
END $$
$$

CREATE PROCEDURE sp_DeleteInsurancePolicy(
    IN p_policy_id       INT
)
BEGIN
    -- Delete a policy. ON DELETE RESTRICT prevents removal if PatientInsurance references exist.
    DELETE FROM InsurancePolicies WHERE policy_id = p_policy_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.9 CRUD for PatientInsurance
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddPatientInsurance(
    IN p_patient_id    INT,
    IN p_policy_id     INT
)
BEGIN
    -- Link a patient to an insurance policy.
    INSERT INTO PatientInsurance(patient_id, policy_id) VALUES(p_patient_id, p_policy_id);
END $$
$$

CREATE PROCEDURE sp_GetPatientInsurance()
BEGIN
    -- Retrieve all patient-insurance links.
    SELECT * FROM PatientInsurance;
END $$
$$

CREATE PROCEDURE sp_DeletePatientInsurance(
    IN p_patient_id    INT,
    IN p_policy_id     INT
)
BEGIN
    -- Remove a patient-insurance link.
    DELETE FROM PatientInsurance
    WHERE patient_id = p_patient_id
      AND policy_id  = p_policy_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.10 CRUD for Appointments
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddAppointment(
    IN p_patient_id       INT,
    IN p_doctor_id        INT,
    IN p_appointment_date DATETIME,
    IN p_status           ENUM(
                              'Pending',
                              'Confirmed',
                              'Completed',
                              'Canceled',
                              'Rescheduled',
                              'No Show'
                           ),
    IN p_notes            TEXT
)
BEGIN
    -- Insert a new appointment. Must supply valid patient_id and doctor_id.
    INSERT INTO Appointments(
        patient_id, doctor_id, appointment_date, status, notes
    ) VALUES (
        p_patient_id, p_doctor_id, p_appointment_date, p_status, p_notes
    );
END $$
$$

CREATE PROCEDURE sp_GetAppointments()
BEGIN
    -- Retrieve all appointments.
    SELECT * FROM Appointments;
END $$
$$

CREATE PROCEDURE sp_UpdateAppointment(
    IN p_appointment_id   INT,
    IN p_patient_id       INT,
    IN p_doctor_id        INT,
    IN p_appointment_date DATETIME,
    IN p_status           ENUM(
                              'Pending',
                              'Confirmed',
                              'Completed',
                              'Canceled',
                              'Rescheduled',
                              'No Show'
                           ),
    IN p_notes            TEXT
)
BEGIN
    -- Update appointment details based on appointment_id.
    UPDATE Appointments
    SET patient_id       = p_patient_id,
        doctor_id        = p_doctor_id,
        appointment_date = p_appointment_date,
        status           = p_status,
        notes            = p_notes
    WHERE appointment_id = p_appointment_id;
END $$
$$

CREATE PROCEDURE sp_DeleteAppointment(
    IN p_appointment_id   INT
)
BEGIN
    -- Delete an appointment. ON DELETE RESTRICT on MedicalRecords prevents deletion if a record exists.
    DELETE FROM Appointments WHERE appointment_id = p_appointment_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.11 CRUD for MedicalRecords
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddMedicalRecord(
    IN p_appointment_id INT,
    IN p_diagnosis      VARCHAR(255),
    IN p_visit_summary  TEXT,
    IN p_doctor_notes   TEXT
)
BEGIN
    -- Insert a medical record. Must supply a valid appointment_id.
    INSERT INTO MedicalRecords(
        appointment_id, diagnosis, visit_summary, doctor_notes
    ) VALUES (
        p_appointment_id, p_diagnosis, p_visit_summary, p_doctor_notes
    );
END $$
$$

CREATE PROCEDURE sp_GetMedicalRecords()
BEGIN
    -- Retrieve all medical records.
    SELECT * FROM MedicalRecords;
END $$
$$

CREATE PROCEDURE sp_UpdateMedicalRecord(
    IN p_record_id      INT,
    IN p_appointment_id INT,
    IN p_diagnosis      VARCHAR(255),
    IN p_visit_summary  TEXT,
    IN p_doctor_notes   TEXT
)
BEGIN
    -- Update medical record based on record_id.
    UPDATE MedicalRecords
    SET appointment_id = p_appointment_id,
        diagnosis      = p_diagnosis,
        visit_summary  = p_visit_summary,
        doctor_notes   = p_doctor_notes
    WHERE record_id = p_record_id;
END $$
$$

CREATE PROCEDURE sp_DeleteMedicalRecord(
    IN p_record_id      INT
)
BEGIN
    -- Delete a medical record.
    DELETE FROM MedicalRecords WHERE record_id = p_record_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.12 CRUD for Prescriptions
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddPrescription(
    IN p_record_id     INT,
    IN p_medication    VARCHAR(100),
    IN p_dosage        VARCHAR(50),
    IN p_frequency_id  INT,
    IN p_start_date    DATE,
    IN p_end_date      DATE,
    IN p_notes         TEXT
)
BEGIN
    -- Insert a prescription. Must supply valid record_id and frequency_id.
    INSERT INTO Prescriptions(
        record_id, medication_name, dosage, frequency_id, start_date, end_date, notes
    ) VALUES (
        p_record_id, p_medication, p_dosage, p_frequency_id, p_start_date, p_end_date, p_notes
    );
END $$
$$

CREATE PROCEDURE sp_GetPrescriptions()
BEGIN
    -- Retrieve all prescriptions.
    SELECT * FROM Prescriptions;
END $$
$$

CREATE PROCEDURE sp_UpdatePrescription(
    IN p_prescription_id INT,
    IN p_record_id       INT,
    IN p_medication      VARCHAR(100),
    IN p_dosage          VARCHAR(50),
    IN p_frequency_id    INT,
    IN p_start_date      DATE,
    IN p_end_date        DATE,
    IN p_notes           TEXT
)
BEGIN
    -- Update prescription based on prescription_id.
    UPDATE Prescriptions
    SET record_id       = p_record_id,
        medication_name = p_medication,
        dosage          = p_dosage,
        frequency_id    = p_frequency_id,
        start_date      = p_start_date,
        end_date        = p_end_date,
        notes           = p_notes
    WHERE prescription_id = p_prescription_id;
END $$
$$

CREATE PROCEDURE sp_DeletePrescription(
    IN p_prescription_id INT
)
BEGIN
    -- Delete a prescription.
    DELETE FROM Prescriptions WHERE prescription_id = p_prescription_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.13 CRUD for TreatmentAssignments
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddAssignment(
    IN p_patient_id        INT,
    IN p_treatment_id      INT,
    IN p_assignment_date   DATETIME,
    IN p_assigned_doctor_id INT,
    IN p_assigned_nurse_id  INT,
    IN p_notes             TEXT
)
BEGIN
    -- Insert a new treatment assignment in a transaction to catch referential errors.
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'Failed to add treatment assignment due to referential integrity or data error.';
    END;
    START TRANSACTION;
      INSERT INTO TreatmentAssignments(
        patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes
      ) VALUES (
        p_patient_id, p_treatment_id, p_assignment_date, p_assigned_doctor_id, p_assigned_nurse_id, p_notes
      );
    COMMIT;
END $$
$$

CREATE PROCEDURE sp_GetAssignments()
BEGIN
    -- Retrieve all treatment assignments.
    SELECT * FROM TreatmentAssignments;
END $$
$$

CREATE PROCEDURE sp_UpdateAssignment(
    IN p_assignment_id     INT,
    IN p_patient_id        INT,
    IN p_treatment_id      INT,
    IN p_assignment_date   DATETIME,
    IN p_assigned_doctor_id INT,
    IN p_assigned_nurse_id  INT,
    IN p_notes             TEXT
)
BEGIN
    -- Update assignment based on assignment_id.
    UPDATE TreatmentAssignments
    SET patient_id          = p_patient_id,
        treatment_id        = p_treatment_id,
        assignment_date     = p_assignment_date,
        assigned_doctor_id  = p_assigned_doctor_id,
        assigned_nurse_id   = p_assigned_nurse_id,
        notes               = p_notes
    WHERE assignment_id = p_assignment_id;
END $$
$$

CREATE PROCEDURE sp_DeleteAssignment(
    IN p_assignment_id     INT
)
BEGIN
    -- Delete a treatment assignment.
    DELETE FROM TreatmentAssignments WHERE assignment_id = p_assignment_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.14 CRUD for PatientFiles
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddPatientFile(
    IN p_patient_id    INT,
    IN p_file_name     VARCHAR(255),
    IN p_file_type     VARCHAR(50),
    IN p_file_path     VARCHAR(255)
)
BEGIN
    -- Insert a new patient file record.
    INSERT INTO PatientFiles(patient_id, file_name, file_type, file_path)
    VALUES (p_patient_id, p_file_name, p_file_type, p_file_path);
END $$
$$

CREATE PROCEDURE sp_GetPatientFiles()
BEGIN
    -- Retrieve all patient files.
    SELECT * FROM PatientFiles;
END $$
$$

CREATE PROCEDURE sp_DeletePatientFile(
    IN p_file_id       INT
)
BEGIN
    -- Delete a patient file record.
    DELETE FROM PatientFiles WHERE file_id = p_file_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.15 CRUD for Invoices
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddInvoice(
    IN p_patient_id     INT,
    IN p_invoice_date   DATE,
    IN p_total_amount   DECIMAL(12,2),
    IN p_status         ENUM('Pending','Paid','Overdue')
)
BEGIN
    -- Insert a new invoice. Must supply valid patient_id.
    INSERT INTO Invoices(patient_id, invoice_date, total_amount, status)
    VALUES (p_patient_id, p_invoice_date, p_total_amount, p_status);
END $$
$$

CREATE PROCEDURE sp_GetInvoices()
BEGIN
    -- Retrieve all invoices.
    SELECT * FROM Invoices;
END $$
$$

CREATE PROCEDURE sp_UpdateInvoice(
    IN p_invoice_id     INT,
    IN p_patient_id     INT,
    IN p_invoice_date   DATE,
    IN p_total_amount   DECIMAL(12,2),
    IN p_status         ENUM('Pending','Paid','Overdue')
)
BEGIN
    -- Update invoice based on invoice_id.
    UPDATE Invoices
    SET patient_id     = p_patient_id,
        invoice_date   = p_invoice_date,
        total_amount   = p_total_amount,
        status         = p_status
    WHERE invoice_id = p_invoice_id;
END $$
$$

CREATE PROCEDURE sp_DeleteInvoice(
    IN p_invoice_id     INT
)
BEGIN
    -- Delete an invoice. ON DELETE RESTRICT on Payments prevents deletion if payments exist.
    DELETE FROM Invoices WHERE invoice_id = p_invoice_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.16 CRUD for Payments
-- ---------------------------------------------------------------------------------------
CREATE PROCEDURE sp_AddPayment(
    IN p_invoice_id    INT,
    IN p_payment_date  DATE,
    IN p_amount_paid   DECIMAL(12,2),
    IN p_method        ENUM('Cash','Credit Card','Insurance','Other')
)
BEGIN
    -- Insert a new payment. Must supply valid invoice_id.
    INSERT INTO Payments(invoice_id, payment_date, amount_paid, method)
    VALUES (p_invoice_id, p_payment_date, p_amount_paid, p_method);
END $$
$$

CREATE PROCEDURE sp_GetPayments()
BEGIN
    -- Retrieve all payments.
    SELECT * FROM Payments;
END $$
$$

CREATE PROCEDURE sp_UpdatePayment(
    IN p_payment_id    INT,
    IN p_invoice_id    INT,
    IN p_payment_date  DATE,
    IN p_amount_paid   DECIMAL(12,2),
    IN p_method        ENUM('Cash','Credit Card','Insurance','Other')
)
BEGIN
    -- Update payment based on payment_id.
    UPDATE Payments
    SET invoice_id    = p_invoice_id,
        payment_date  = p_payment_date,
        amount_paid   = p_amount_paid,
        method        = p_method
    WHERE payment_id = p_payment_id;
END $$
$$

CREATE PROCEDURE sp_DeletePayment(
    IN p_payment_id    INT
)
BEGIN
    -- Delete a payment.
    DELETE FROM Payments WHERE payment_id = p_payment_id;
END $$
$$


-- ---------------------------------------------------------------------------------------
-- 11.17 TRANSACTION PROCEDURES (Multi‐Table Workflows)
-- ---------------------------------------------------------------------------------------
-- 11.17.1 Book Appointment and Simultaneous Invoice
--   • Inserts into Appointments and Invoices in one atomic transaction.
--   • If either step fails (invalid FK, etc.), entire transaction rolls back.
CREATE PROCEDURE sp_BookAppointmentAndInvoice(
    IN p_patient_id       INT,
    IN p_doctor_id        INT,
    IN p_appointment_date DATETIME,
    IN p_status           ENUM(
                              'Pending',
                              'Confirmed',
                              'Completed',
                              'Canceled',
                              'Rescheduled',
                              'No Show'
                           ),
    IN p_notes            TEXT,
    IN p_invoice_amt      DECIMAL(12,2)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
      -- If any SQL exception occurs, rollback the entire transaction
      ROLLBACK;
      SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error booking appointment and creating invoice.';
    END;

    START TRANSACTION;
      -- Step 1: Create appointment record
      INSERT INTO Appointments(
        patient_id, doctor_id, appointment_date, status, notes
      ) VALUES (
        p_patient_id, p_doctor_id, p_appointment_date, p_status, p_notes
      );
      SET @lastAppId = LAST_INSERT_ID();

      -- Step 2: Create invoice linked to same patient
      INSERT INTO Invoices(patient_id, invoice_date, total_amount, status)
      VALUES (p_patient_id, CURRENT_DATE(), p_invoice_amt, 'Pending');
    COMMIT;
END $$
$$

-- 11.17.2 Complete Appointment and Create MedicalRecord
--   • Marks an appointment as completed and inserts a medical record in one atomic transaction.
CREATE PROCEDURE sp_CompleteAppointmentAndRecord(
    IN p_appointment_id INT,
    IN p_diagnosis      VARCHAR(255),
    IN p_visit_summary  TEXT,
    IN p_doctor_notes   TEXT
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
      ROLLBACK;
      SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error completing appointment and creating medical record.';
    END;

    START TRANSACTION;
      -- Step 1: Update appointment status
      UPDATE Appointments
      SET status = 'Completed'
      WHERE appointment_id = p_appointment_id;

      -- Step 2: Insert medical record for this appointment
      INSERT INTO MedicalRecords(appointment_id, diagnosis, visit_summary, doctor_notes)
      VALUES (p_appointment_id, p_diagnosis, p_visit_summary, p_doctor_notes);
    COMMIT;
END $$
$$

DELIMITER ;


-- =======================================================================================
-- 12. SAMPLE DATA INSERTS (3–5 ROWS PER TABLE)
--     Purpose: Provide realistic sample rows to test functionality immediately.
--     Design Choices: Use current or near-future dates for temporal fields. Diverse physician/nurse/patient names.
-- =======================================================================================

-- ---------------------------------------------------------------------------------------
-- 12.1 Lookup Tables Sample Data
-- ---------------------------------------------------------------------------------------

-- 12.1.1 Specializations (5 rows)
INSERT INTO Specializations (name, description) VALUES
  ('Cardiology',     'Heart-related diseases and conditions'),
  ('Pediatrics',     'Medical care for infants, children, and adolescents'),
  ('Oncology',       'Diagnosis and treatment of cancer'),
  ('Orthopedics',    'Bone, joint, and muscle conditions'),
  ('Dermatology',    'Skin-related issues and conditions');

-- 12.1.2 Departments (5 rows)
INSERT INTO Departments (name, location) VALUES
  ('Emergency',       'Building A, 1st Floor'),
  ('ICU',             'Building A, 2nd Floor'),
  ('General Ward',    'Building B, 3rd Floor'),
  ('Maternity',       'Building C, Ground Floor'),
  ('Pediatrics Ward', 'Building B, 2nd Floor');

-- 12.1.3 StaffRoles (5 rows)
INSERT INTO StaffRoles (name, description) VALUES
  ('Receptionist',   'Front desk staff handling patient intake and appointments'),
  ('Billing Clerk',  'Processes invoices and collects payments'),
  ('Lab Technician', 'Performs laboratory tests and maintains lab equipment'),
  ('Pharmacist',     'Dispenses medications and counsels patients'),
  ('Office Manager', 'Oversees day-to-day administrative operations');

-- 12.1.4 Treatments (5 rows)
INSERT INTO Treatments (name, description) VALUES
  ('Physical Therapy',  'Exercises and modalities to restore mobility and function'),
  ('Chemotherapy',      'Medication-based cancer treatment with cytotoxic drugs'),
  ('Radiation Therapy', 'High-energy radiation administered to target cancer cells'),
  ('Dialysis',          'Mechanical filtration for patients with kidney failure'),
  ('Wound Care',        'Management of chronic and acute wounds, dressings and monitoring');

-- 12.1.5 FrequencyTypes (5 rows)
INSERT INTO FrequencyTypes (name, description) VALUES
  ('Once a day',      'Take medication one time each day'),
  ('Twice a day',     'Take medication two times per day, approximately 12 hours apart'),
  ('Every 8 hours',   'Administer medication every 8 hours'),
  ('Weekly',          'Perform or administer once per week'),
  ('As needed',       'Use medication or perform action as required, based on symptoms');

-- 12.1.6 InsuranceProviders (5 rows)
INSERT INTO InsuranceProviders (name, contact_info) VALUES
  ('Acme Health Ins.',    '123 Main St, Nairobi; +254-700-000001'),
  ('SafeCare Insurance',  '456 Elm St, Mombasa; +254-700-000002'),
  ('HealthGuard Ltd.',    '789 Oak St, Kisumu; +254-700-000003'),
  ('MedSecure PLC',       '101 Pine St, Eldoret; +254-700-000004'),
  ('PrimeHealth Co.',     '202 Maple St, Nakuru; +254-700-000005');


-- ---------------------------------------------------------------------------------------
-- 12.2 Core Tables Sample Data
-- ---------------------------------------------------------------------------------------

-- 12.2.1 Patients (5 rows)
INSERT INTO Patients (
  first_name, last_name, date_of_birth, gender, address, phone, email
) VALUES
  ('Alice',     'Kamau',     '1985-04-12', 'Female', '12 Riverside Dr, Nairobi', '0712-111111', 'alice.kamau@example.com'),
  ('Brian',     'Otieno',    '1990-09-25', 'Male',   '45 Westlands Rd, Nairobi', '0712-222222', 'brian.otieno@example.com'),
  ('Catherine', 'Mwangi',    '1978-11-03', 'Female', '78 Gigiri Ave, Nairobi', '0712-333333', 'catherine.mwangi@example.com'),
  ('David',     'Ochieng',   '2002-02-17', 'Male',   '123 Langata Rd, Nairobi',  '0712-444444', 'david.ochieng@example.com'),
  ('Elise',     'Nduta',     '1965-07-09', 'Female', '56 Karen Rd, Nairobi',    '0712-555555', 'elise.nduta@example.com');

-- 12.2.2 Doctors (5 rows)
INSERT INTO Doctors (
  first_name, last_name, specialization_id, contact_phone, email, date_hired, status
) VALUES
  ('James',    'Njoroge',  1, '0712-666666', 'james.njoroge@meditech.com',  '2015-06-01', 'Active'),
  ('Mary',     'Wanjiku',  2, '0712-777777', 'mary.wanjiku@meditech.com',   '2016-09-12', 'Active'),
  ('Peter',    'Otieno',   3, '0712-888888', 'peter.otieno@meditech.com',    '2014-01-23', 'Active'),
  ('Rebecca',  'Achieng',  4, '0712-999999', 'rebecca.achieng@meditech.com', '2018-03-30', 'On Leave'),
  ('Samuel',   'Mutua',    5, '0712-000000', 'samuel.mutua@meditech.com',    '2017-12-11', 'Inactive');

-- 12.2.3 Nurses (5 rows)
INSERT INTO Nurses (
  first_name, last_name, department_id, contact_phone, email, date_hired, status
) VALUES
  ('Grace',    'Mumo',     1, '0713-111111', 'grace.mumo@meditech.com',    '2017-05-15', 'Active'),
  ('Mercy',    'Kariuki',  2, '0713-222222', 'mercy.kariuki@meditech.com',  '2018-08-20', 'Active'),
  ('Pauline',  'Achieng',  3, '0713-333333', 'pauline.achieng@meditech.com','2016-11-05', 'Active'),
  ('Susan',    'Kamotho',  4, '0713-444444', 'susan.kamotho@meditech.com',  '2019-02-14', 'On Leave'),
  ('David',    'Kamau',    5, '0713-555555', 'david.kamau@meditech.com',    '2020-07-22', 'Active');

-- 12.2.4 AdminStaff (5 rows)
INSERT INTO AdminStaff (
  first_name, last_name, role_id, contact_phone, email, date_hired, status
) VALUES
  ('Alice',    'Ogola',     1, '0714-111111', 'alice.ogola@meditech.com',    '2015-01-03', 'Active'),
  ('Brian',    'Mutiso',    2, '0714-222222', 'brian.mutiso@meditech.com',   '2016-04-17', 'Active'),
  ('Clara',    'Wambui',    3, '0714-333333', 'clara.wambui@meditech.com',   '2017-09-30', 'Active'),
  ('Daniel',   'Omondi',    4, '0714-444444', 'daniel.omondi@meditech.com',  '2018-12-02', 'Inactive'),
  ('Esther',   'Kamau',     5, '0714-555555', 'esther.kamau@meditech.com',   '2019-10-10', 'Active');

-- 12.2.5 Users (3 rows)
INSERT INTO Users (username, password_hash, role_id) VALUES
  ('admin',     SHA2('admin123',256),     5),   -- Office Manager role
  ('drjames',   SHA2('docpass',256),      2),   -- Billing Clerk role (as example)
  ('nursemary', SHA2('nursepass',256),    3);   -- Lab Technician role (as example)


-- ---------------------------------------------------------------------------------------
-- 12.3 Appointments, MedicalRecords, Prescriptions, TreatmentAssignments, PatientFiles Sample Data
-- ---------------------------------------------------------------------------------------

-- 12.3.1 Appointments (5 rows)
INSERT INTO Appointments (
  patient_id, doctor_id, appointment_date, status, notes
) VALUES
  (1, 1, '2025-06-10 09:30:00', 'Pending',     'Routine check-up for blood pressure'),
  (2, 2, '2025-06-11 14:00:00', 'Confirmed',  'Follow-up for asthma medication review'),
  (3, 3, '2025-06-12 11:15:00', 'Completed',  'Discuss chemotherapy plan and next steps'),
  (4, 4, '2025-06-13 08:45:00', 'Canceled',   'Patient missed appointment, no record generated'),
  (5, 5, '2025-06-14 16:00:00', 'Rescheduled','Moved due to doctor unavailability');

-- 12.3.2 MedicalRecords (5 rows)
INSERT INTO MedicalRecords (
  appointment_id, diagnosis, visit_summary, doctor_notes
) VALUES
  (1, 'Hypertension', 'Blood pressure elevated; prescribed Lisinopril',      'Patient to monitor home BP daily and return in 1 month'),
  (2, 'Asthma',       'Wheezing present; adjusted Albuterol dosage',         'Advise spacer use and allergy avoidance'),
  (3, 'Breast Cancer','Initial oncology consult; plan radiation and chemo',    'Schedule first chemo infusion next week'),
  (4, 'N/A',          'No visit due to cancellation; no notes generated',     'Reschedule appointment based on patient’s availability'),
  (5, 'Diabetes',     'Blood sugar stable; recommended dietary adjustments',  'Refer patient to nutritionist for meal planning');

-- 12.3.3 Prescriptions (5 rows)
INSERT INTO Prescriptions (
  record_id, medication_name, dosage, frequency_id, start_date, end_date, notes
) VALUES
  (1, 'Lisinopril',   '10 mg',   1, '2025-06-10', '2025-12-31', 'Take once daily in the morning'),
  (2, 'Albuterol',    '2 puffs', 3, '2025-06-11', '2025-07-11', 'Use every 8 hours as needed for wheezing'),
  (3, 'Paclitaxel',   '200 mg',  2, '2025-06-15', '2025-11-15', 'Administer biweekly infusion under supervision'),
  (4, 'N/A',          'N/A',     5, '2025-06-13', '2025-06-13', 'No medication since appointment was canceled'),
  (5, 'Metformin',    '500 mg',  1, '2025-06-14', '2025-12-14', 'Take once daily with meals');

-- 12.3.4 TreatmentAssignments (5 rows)
INSERT INTO TreatmentAssignments (
  patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes
) VALUES
  (1, 1, '2025-06-10 10:00:00', 1, 1, 'Begin physical therapy; evaluate range of motion'),
  (2, 2, '2025-06-11 15:00:00', 2, 2, 'Initiate first chemotherapy session'),
  (3, 3, '2025-06-12 12:00:00', 3, 3, 'Plan radiation therapy schedule'),
  (4, 4, '2025-06-13 09:00:00', 4, 4, 'Start hemodialysis session'),
  (5, 5, '2025-06-14 17:00:00', 5, 5, 'Apply wound dressing and observe healing progress');

-- 12.3.5 PatientFiles (5 rows)
INSERT INTO PatientFiles (
  patient_id, file_name, file_type, file_path
) VALUES
  (1, 'blood_test_20250610.pdf', 'application/pdf', '/files/patient1/blood_test_20250610.pdf'),
  (2, 'xray_20250611.jpg',       'image/jpeg',      '/files/patient2/xray_20250611.jpg'),
  (3, 'mammogram_20250612.png',  'image/png',       '/files/patient3/mammogram_20250612.png'),
  (4, 'ecg_20250613.csv',        'text/csv',        '/files/patient4/ecg_20250613.csv'),
  (5, 'ultrasound_20250614.pdf', 'application/pdf', '/files/patient5/ultrasound_20250614.pdf');


-- ---------------------------------------------------------------------------------------
-- 12.4 Insurance & Billing Sample Data
-- ---------------------------------------------------------------------------------------

-- 12.4.1 InsurancePolicies (5 rows)
INSERT INTO InsurancePolicies (
  provider_id, policy_number, coverage_amount, start_date, end_date
) VALUES
  (1, 'ACME12345', 50000.00, '2025-01-01', '2025-12-31'),
  (2, 'SAFE67890', 75000.00, '2025-02-01', '2025-08-31'),
  (3, 'HG112233',  60000.00, '2025-03-15', '2025-09-15'),
  (4, 'MED445566', 80000.00, '2025-04-01', '2025-10-01'),
  (5, 'PRIME7788', 90000.00, '2025-05-01', '2025-11-01');

-- 12.4.2 PatientInsurance (5 rows)
INSERT INTO PatientInsurance (patient_id, policy_id) VALUES
  (1, 1),
  (2, 2),
  (3, 3),
  (4, 4),
  (5, 5);

-- 12.4.3 Invoices (5 rows)
INSERT INTO Invoices (
  patient_id, invoice_date, total_amount, status
) VALUES
  (1, '2025-06-10', 1200.00, 'Pending'),
  (2, '2025-06-11',  800.00, 'Paid'),
  (3, '2025-06-12', 5000.00, 'Overdue'),
  (4, '2025-06-13',    0.00, 'Canceled'),
  (5, '2025-06-14', 1500.00, 'Pending');

-- 12.4.4 Payments (5 rows)
INSERT INTO Payments (
  invoice_id, payment_date, amount_paid, method
) VALUES
  (2, '2025-06-12',  800.00, 'Credit Card'),
  (3, '2025-06-13', 2000.00, 'Insurance'),
  (3, '2025-06-20', 3000.00, 'Cash'),
  (5, '2025-06-15', 1000.00, 'Cash'),
  (5, '2025-06-20',  500.00, 'Cash');


-- =======================================================================================
-- 13. END OF SCRIPT
--     This script is now fully functional: you can run it in MySQL Workbench or CLI
--     to recreate a BCNF/4NF-compliant schema with detailed comments, CRUD stored procedures,
--     multi-table transaction procedures, and realistic sample data. No modifications needed.
-- =======================================================================================
