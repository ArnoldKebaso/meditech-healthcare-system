-- =============================================================================
-- DATABASE: meditech (MediTech Innovations Healthcare System)
-- =============================================================================

DROP DATABASE IF EXISTS meditech;
CREATE DATABASE meditech
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
USE meditech;

-- ============================================================================ 
-- 1) Patients
--    Stores patient demographic and contact information.
--    3NF compliant: separate phone, email, and address fields.
-- ============================================================================ 
CREATE TABLE Patients (
  patient_id        INT AUTO_INCREMENT,
  first_name        VARCHAR(100) NOT NULL,
  last_name         VARCHAR(100) NOT NULL,
  date_of_birth     DATE NOT NULL,
  gender            ENUM('Male','Female','Other') NOT NULL,
  contact_phone     VARCHAR(20),
  contact_email     VARCHAR(150),
  address           VARCHAR(255),
  PRIMARY KEY (patient_id),
  UNIQUE KEY uq_pat_email (contact_email)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 2) Specializations (NEW Lookup Table)
--    Predefined list of doctor specializations for dropdown.
-- ============================================================================ 
CREATE TABLE Specializations (
  specialization_id INT AUTO_INCREMENT,
  name              VARCHAR(150) NOT NULL UNIQUE,
  PRIMARY KEY (specialization_id)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 3) Doctors (modified)
--    Now references Specializations(specialization_id).
-- ============================================================================ 
CREATE TABLE Doctors (
  doctor_id         INT AUTO_INCREMENT,
  first_name        VARCHAR(100) NOT NULL,
  last_name         VARCHAR(100) NOT NULL,
  specialization_id INT NOT NULL,
  contact_phone     VARCHAR(20),
  contact_email     VARCHAR(150),
  PRIMARY KEY (doctor_id),
  UNIQUE KEY uq_doc_email (contact_email),
  FOREIGN KEY (specialization_id)
    REFERENCES Specializations(specialization_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 4) Departments (NEW Lookup Table)
--    Predefined list of nursing departments for dropdown.
-- ============================================================================ 
CREATE TABLE Departments (
  department_id INT AUTO_INCREMENT,
  name          VARCHAR(100) NOT NULL UNIQUE,
  PRIMARY KEY (department_id)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 5) Nurses (modified)
--    Now references Departments(department_id).
-- ============================================================================ 
CREATE TABLE Nurses (
  nurse_id          INT AUTO_INCREMENT,
  first_name        VARCHAR(100) NOT NULL,
  last_name         VARCHAR(100) NOT NULL,
  department_id     INT NOT NULL,
  contact_phone     VARCHAR(20),
  contact_email     VARCHAR(150),
  PRIMARY KEY (nurse_id),
  UNIQUE KEY uq_nurse_email (contact_email),
  FOREIGN KEY (department_id)
    REFERENCES Departments(department_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 6) StaffRoles (NEW Lookup Table)
--    Predefined list of roles for AdminStaff dropdown.
-- ============================================================================ 
CREATE TABLE StaffRoles (
  role_id   INT AUTO_INCREMENT,
  name      VARCHAR(100) NOT NULL UNIQUE,
  PRIMARY KEY (role_id)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 7) AdminStaff (modified)
--    Now references StaffRoles(role_id).
-- ============================================================================ 
CREATE TABLE AdminStaff (
  staff_id       INT AUTO_INCREMENT,
  first_name     VARCHAR(100) NOT NULL,
  last_name      VARCHAR(100) NOT NULL,
  role_id        INT NOT NULL,
  contact_phone  VARCHAR(20),
  contact_email  VARCHAR(150),
  PRIMARY KEY (staff_id),
  UNIQUE KEY uq_staff_email (contact_email),
  FOREIGN KEY (role_id)
    REFERENCES StaffRoles(role_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 8) Treatments
--    Catalog of treatment types (used as a dropdown itself).
-- ============================================================================ 
CREATE TABLE Treatments (
  treatment_id      INT AUTO_INCREMENT,
  name              VARCHAR(150) NOT NULL UNIQUE,
  description       TEXT,
  created_at        DATETIME DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (treatment_id)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 9) TreatmentAssignments
--    Links patients to treatments and assigned staff.
--    Composite PK ensures no duplicate assignment for same datetime.
-- ============================================================================ 
CREATE TABLE TreatmentAssignments (
  patient_id          INT NOT NULL,
  treatment_id        INT NOT NULL,
  assignment_date     DATETIME NOT NULL,
  assigned_doctor_id  INT NOT NULL,
  assigned_nurse_id   INT NOT NULL,
  notes               TEXT,
  PRIMARY KEY (patient_id, treatment_id, assignment_date),
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


-- ============================================================================ 
-- 10) PatientFiles
--     Stores uploaded patient documents (e.g., scans, reports).
-- ============================================================================ 
CREATE TABLE PatientFiles (
  file_id            INT AUTO_INCREMENT,
  patient_id         INT NOT NULL,
  file_name          VARCHAR(255) NOT NULL,
  file_type          VARCHAR(100),
  file_size_bytes    INT,
  upload_timestamp   DATETIME DEFAULT CURRENT_TIMESTAMP,
  file_data          LONGBLOB,
  PRIMARY KEY (file_id),
  FOREIGN KEY (patient_id)
    REFERENCES Patients(patient_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE
) ENGINE=InnoDB;


-- ============================================================================ 
-- 11) InsuranceProviders
--      Stores insurance company details.
-- ============================================================================ 
CREATE TABLE InsuranceProviders (
  provider_id        INT AUTO_INCREMENT,
  name               VARCHAR(150) NOT NULL UNIQUE,
  contact_phone      VARCHAR(20),
  contact_email      VARCHAR(150),
  PRIMARY KEY (provider_id)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 12) InsurancePolicies (NEW Lookup Table)
--      Predefined policies, tied to a provider.
-- ============================================================================ 
CREATE TABLE InsurancePolicies (
  policy_id          INT AUTO_INCREMENT,
  provider_id        INT NOT NULL,
  policy_number      VARCHAR(100) NOT NULL UNIQUE,
  coverage_details   TEXT,
  start_date         DATE,
  end_date           DATE,
  PRIMARY KEY (policy_id),
  FOREIGN KEY (provider_id)
    REFERENCES InsuranceProviders(provider_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 13) PatientInsurance (modified)
--      Links patients to a specific policy (instead of free-text).
-- ============================================================================ 
CREATE TABLE PatientInsurance (
  patient_id    INT NOT NULL,
  policy_id     INT NOT NULL,
  PRIMARY KEY (patient_id, policy_id),
  FOREIGN KEY (patient_id)
    REFERENCES Patients(patient_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
  FOREIGN KEY (policy_id)
    REFERENCES InsurancePolicies(policy_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 14) Invoices
--      Billing invoices issued to patients.
-- ============================================================================ 
CREATE TABLE Invoices (
  invoice_id         INT AUTO_INCREMENT,
  patient_id         INT NOT NULL,
  invoice_date       DATETIME NOT NULL,
  total_amount       DECIMAL(10,2) NOT NULL,
  status             ENUM('Pending','Paid','Cancelled') NOT NULL DEFAULT 'Pending',
  PRIMARY KEY (invoice_id),
  FOREIGN KEY (patient_id)
    REFERENCES Patients(patient_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 15) Payments
--      Records payment transactions against invoices.
-- ============================================================================ 
CREATE TABLE Payments (
  payment_id         INT AUTO_INCREMENT,
  invoice_id         INT NOT NULL,
  payment_date       DATETIME NOT NULL,
  amount_paid        DECIMAL(10,2) NOT NULL,
  method             ENUM('Cash','Card','Insurance','Other') NOT NULL,
  transaction_ref    VARCHAR(100),
  PRIMARY KEY (payment_id),
  FOREIGN KEY (invoice_id)
    REFERENCES Invoices(invoice_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 16) Appointments
--      Tracks patient‐doctor appointments (new).
-- ============================================================================ 
CREATE TABLE Appointments (
  appointment_id     INT AUTO_INCREMENT,
  patient_id         INT NOT NULL,
  doctor_id          INT NOT NULL,
  appointment_date   DATETIME NOT NULL,
  status             ENUM('Pending','Confirmed','Completed','Canceled','Rescheduled','No Show') 
                        DEFAULT 'Pending',
  notes              TEXT,
  PRIMARY KEY (appointment_id),
  FOREIGN KEY (patient_id)
    REFERENCES Patients(patient_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT,
  FOREIGN KEY (doctor_id)
    REFERENCES Doctors(doctor_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 17) MedicalRecords
--      Capture diagnosis + visit info for attended appointments.
-- ============================================================================ 
CREATE TABLE MedicalRecords (
  record_id       INT AUTO_INCREMENT,
  appointment_id  INT NOT NULL,
  diagnosis       TEXT NOT NULL,
  visit_summary   TEXT,
  doctor_notes    TEXT,
  created_at      DATETIME DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (record_id),
  FOREIGN KEY (appointment_id)
    REFERENCES Appointments(appointment_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE
) ENGINE=InnoDB;


-- ============================================================================ 
-- 18) FrequencyTypes (NEW Lookup Table)
--      Predefined frequency options for prescriptions.
-- ============================================================================ 
CREATE TABLE FrequencyTypes (
  frequency_id   INT AUTO_INCREMENT,
  name           VARCHAR(100) NOT NULL UNIQUE,
  PRIMARY KEY (frequency_id)
) ENGINE=InnoDB;


-- ============================================================================ 
-- 19) Prescriptions (modified)
--      Now references MedicalRecords(record_id) and FrequencyTypes(frequency_id).
-- ============================================================================ 
CREATE TABLE Prescriptions (
  prescription_id   INT AUTO_INCREMENT,
  record_id         INT NOT NULL UNIQUE,
  medication_name   VARCHAR(255) NOT NULL,
  dosage            VARCHAR(100),
  frequency_id      INT NOT NULL,
  start_date        DATE,
  end_date          DATE,
  instructions      TEXT,
  PRIMARY KEY (prescription_id),
  FOREIGN KEY (record_id)
    REFERENCES MedicalRecords(record_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
  FOREIGN KEY (frequency_id)
    REFERENCES FrequencyTypes(frequency_id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB;


-- ============================================================================ 
-- 20) VIEWS: Simplify common lookups or filters
-- ============================================================================ 

-- 20.1 Upcoming Appointments (all appointments >= NOW())
CREATE VIEW v_UpcomingAppointments AS
SELECT
  a.appointment_id,
  a.patient_id,
  CONCAT(p.first_name,' ',p.last_name) AS patient_name,
  a.doctor_id,
  CONCAT(d.first_name,' ',d.last_name)   AS doctor_name,
  a.appointment_date,
  a.status
FROM Appointments a
JOIN Patients p ON a.patient_id = p.patient_id
JOIN Doctors  d ON a.doctor_id  = d.doctor_id
WHERE a.appointment_date >= NOW();

-- 20.2 Patient Treatment History (joins across 4 tables)
CREATE VIEW v_PatientTreatmentHistory AS
SELECT
  p.patient_id,
  CONCAT(p.first_name,' ',p.last_name) AS patient_name,
  t.name           AS treatment_name,
  ta.assignment_date,
  CONCAT(d.first_name,' ',d.last_name) AS doctor_name,
  CONCAT(n.first_name,' ',n.last_name) AS nurse_name,
  ta.notes
FROM TreatmentAssignments ta
JOIN Patients   p ON ta.patient_id          = p.patient_id
JOIN Treatments t ON ta.treatment_id        = t.treatment_id
JOIN Doctors    d ON ta.assigned_doctor_id  = d.doctor_id
JOIN Nurses     n ON ta.assigned_nurse_id   = n.nurse_id;

-- 20.3 Patient Billing (aggregate invoice & payments)
CREATE VIEW v_PatientBilling AS
SELECT
  p.patient_id,
  CONCAT(p.first_name,' ',p.last_name)         AS patient_name,
  i.invoice_id,
  i.invoice_date,
  i.total_amount,
  i.status,
  COALESCE(SUM(pay.amount_paid),0)             AS total_paid
FROM Invoices i
JOIN Patients p ON i.patient_id = p.patient_id
LEFT JOIN Payments pay ON pay.invoice_id = i.invoice_id
GROUP BY i.invoice_id;


-- ============================================================================ 
-- 21) STORED PROCEDURES
--     For brevity, only show one example of each “modified” procedure.
--     (You will need to update all existing SPs to match the new FK columns!)
-- ============================================================================ 
DELIMITER $$

-- ====================
-- 21.1 Patients: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddPatient(
  IN p_first VARCHAR(100),
  IN p_last  VARCHAR(100),
  IN p_dob   DATE,
  IN p_gender ENUM('Male','Female','Other'),
  IN p_phone VARCHAR(20),
  IN p_email VARCHAR(150),
  IN p_addr  VARCHAR(255)
)
BEGIN
  INSERT INTO Patients
    (first_name, last_name, date_of_birth, gender, contact_phone, contact_email, address)
  VALUES
    (p_first,   p_last,   p_dob,          p_gender,     p_phone,     p_email,    p_addr);
END$$

CREATE PROCEDURE sp_GetPatients()
BEGIN
  SELECT * FROM Patients;
END$$

CREATE PROCEDURE sp_UpdatePatient(
  IN p_id    INT,
  IN p_dob   DATE,
  IN p_gender ENUM('Male','Female','Other'),
  IN p_phone VARCHAR(20),
  IN p_email VARCHAR(150),
  IN p_addr  VARCHAR(255)
)
BEGIN
  UPDATE Patients
    SET date_of_birth = p_dob,
        gender        = p_gender,
        contact_phone = p_phone,
        contact_email = p_email,
        address       = p_addr
  WHERE patient_id = p_id;
END$$

CREATE PROCEDURE sp_DeletePatient(IN p_id INT)
BEGIN
  DELETE FROM Patients WHERE patient_id = p_id;
END$$


-- ====================
-- 21.2 Doctors: CRUD (modified to use specialization_id)
-- ====================
CREATE PROCEDURE sp_AddDoctor(
  IN d_first VARCHAR(100),
  IN d_last  VARCHAR(100),
  IN d_spec_id INT,
  IN d_phone VARCHAR(20),
  IN d_email VARCHAR(150)
)
BEGIN
  INSERT INTO Doctors
    (first_name, last_name, specialization_id, contact_phone, contact_email)
  VALUES
    (d_first,   d_last,   d_spec_id,         d_phone,      d_email);
END$$

CREATE PROCEDURE sp_GetDoctors()
BEGIN
  SELECT d.doctor_id,
         d.first_name,
         d.last_name,
         s.name       AS specialization_name,
         d.contact_phone,
         d.contact_email
  FROM Doctors d
  JOIN Specializations s ON d.specialization_id = s.specialization_id;
END$$

CREATE PROCEDURE sp_UpdateDoctor(
  IN d_id    INT,
  IN d_spec_id INT,
  IN d_phone VARCHAR(20),
  IN d_email VARCHAR(150)
)
BEGIN
  UPDATE Doctors
    SET specialization_id = d_spec_id,
        contact_phone    = d_phone,
        contact_email    = d_email
  WHERE doctor_id = d_id;
END$$

CREATE PROCEDURE sp_DeleteDoctor(IN d_id INT)
BEGIN
  DELETE FROM Doctors WHERE doctor_id = d_id;
END$$


-- ====================
-- 21.3 Nurses: CRUD (modified to use department_id)
-- ====================
CREATE PROCEDURE sp_AddNurse(
  IN n_first VARCHAR(100),
  IN n_last  VARCHAR(100),
  IN n_dept_id INT,
  IN n_phone VARCHAR(20),
  IN n_email VARCHAR(150)
)
BEGIN
  INSERT INTO Nurses
    (first_name, last_name, department_id, contact_phone, contact_email)
  VALUES
    (n_first,   n_last,   n_dept_id,     n_phone,        n_email);
END$$

CREATE PROCEDURE sp_GetNurses()
BEGIN
  SELECT n.nurse_id,
         n.first_name,
         n.last_name,
         d.name      AS department_name,
         n.contact_phone,
         n.contact_email
  FROM Nurses n
  JOIN Departments d ON n.department_id = d.department_id;
END$$

CREATE PROCEDURE sp_UpdateNurse(
  IN n_id    INT,
  IN n_dept_id INT,
  IN n_phone VARCHAR(20),
  IN n_email VARCHAR(150)
)
BEGIN
  UPDATE Nurses
    SET department_id  = n_dept_id,
        contact_phone  = n_phone,
        contact_email  = n_email
  WHERE nurse_id = n_id;
END$$

CREATE PROCEDURE sp_DeleteNurse(IN n_id INT)
BEGIN
  DELETE FROM Nurses WHERE nurse_id = n_id;
END$$


-- ====================
-- 21.4 AdminStaff: CRUD (modified to use role_id)
-- ====================
CREATE PROCEDURE sp_AddAdmin(
  IN a_first VARCHAR(100),
  IN a_last  VARCHAR(100),
  IN a_role_id INT,
  IN a_phone VARCHAR(20),
  IN a_email VARCHAR(150)
)
BEGIN
  INSERT INTO AdminStaff
    (first_name, last_name, role_id, contact_phone, contact_email)
  VALUES
    (a_first,   a_last,   a_role_id, a_phone,        a_email);
END$$

CREATE PROCEDURE sp_GetAdmins()
BEGIN
  SELECT a.staff_id,
         a.first_name,
         a.last_name,
         r.name         AS role_name,
         a.contact_phone,
         a.contact_email
  FROM AdminStaff a
  JOIN StaffRoles r ON a.role_id = r.role_id;
END$$

CREATE PROCEDURE sp_UpdateAdmin(
  IN a_id      INT,
  IN a_role_id INT,
  IN a_phone   VARCHAR(20),
  IN a_email   VARCHAR(150)
)
BEGIN
  UPDATE AdminStaff
    SET role_id       = a_role_id,
        contact_phone = a_phone,
        contact_email = a_email
  WHERE staff_id = a_id;
END$$

CREATE PROCEDURE sp_DeleteAdmin(IN a_id INT)
BEGIN
  DELETE FROM AdminStaff WHERE staff_id = a_id;
END$$


-- ====================
-- 21.5 Treatments: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddTreatment(
  IN t_name VARCHAR(150),
  IN t_desc TEXT
)
BEGIN
  INSERT INTO Treatments(name, description)
  VALUES (t_name, t_desc);
END$$

CREATE PROCEDURE sp_GetTreatments()
BEGIN
  SELECT * FROM Treatments;
END$$

CREATE PROCEDURE sp_UpdateTreatment(
  IN t_id   INT,
  IN t_name VARCHAR(150),
  IN t_desc TEXT
)
BEGIN
  UPDATE Treatments
    SET name        = t_name,
        description = t_desc
  WHERE treatment_id = t_id;
END$$

CREATE PROCEDURE sp_DeleteTreatment(IN t_id INT)
BEGIN
  DELETE FROM Treatments WHERE treatment_id = t_id;
END$$


-- ====================
-- 21.6 TreatmentAssignments: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddAssignment(
  IN p_id INT, IN t_id INT, IN a_date DATETIME,
  IN doc_id INT, IN nurse_id INT, IN a_notes TEXT
)
BEGIN
  INSERT INTO TreatmentAssignments
    (patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes)
  VALUES (p_id, t_id, a_date, doc_id, nurse_id, a_notes);
END$$

CREATE PROCEDURE sp_GetAssignments()
BEGIN
  SELECT * FROM TreatmentAssignments;
END$$

CREATE PROCEDURE sp_UpdateAssignment(
  IN p_id INT, IN t_id INT, IN a_date DATETIME, IN new_notes TEXT
)
BEGIN
  UPDATE TreatmentAssignments
    SET notes = new_notes
  WHERE patient_id     = p_id
    AND treatment_id   = t_id
    AND assignment_date = a_date;
END$$

CREATE PROCEDURE sp_DeleteAssignment(
  IN p_id INT, IN t_id INT, IN a_date DATETIME
)
BEGIN
  DELETE FROM TreatmentAssignments
  WHERE patient_id     = p_id
    AND treatment_id   = t_id
    AND assignment_date = a_date;
END$$


-- ====================
-- 21.7 PatientFiles: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddPatientFile(
  IN p_id   INT,
  IN f_name VARCHAR(255),
  IN f_type VARCHAR(100),
  IN f_size INT,
  IN f_data LONGBLOB
)
BEGIN
  INSERT INTO PatientFiles(patient_id, file_name, file_type, file_size_bytes, file_data)
  VALUES (p_id, f_name, f_type, f_size, f_data);
END$$

CREATE PROCEDURE sp_GetPatientFiles()
BEGIN
  SELECT * FROM PatientFiles;
END$$

CREATE PROCEDURE sp_UpdatePatientFile(
  IN f_id   INT,
  IN f_name VARCHAR(255),
  IN f_type VARCHAR(100),
  IN f_size INT
)
BEGIN
  UPDATE PatientFiles
    SET file_name       = f_name,
        file_type       = f_type,
        file_size_bytes = f_size
  WHERE file_id = f_id;
END$$

CREATE PROCEDURE sp_DeletePatientFile(IN f_id INT)
BEGIN
  DELETE FROM PatientFiles WHERE file_id = f_id;
END$$


-- ====================
-- 21.8 InsuranceProviders: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddProvider(
  IN prov_name  VARCHAR(150),
  IN prov_phone VARCHAR(20),
  IN prov_email VARCHAR(150)
)
BEGIN
  INSERT INTO InsuranceProviders(name, contact_phone, contact_email)
  VALUES (prov_name, prov_phone, prov_email);
END$$

CREATE PROCEDURE sp_GetProviders()
BEGIN
  SELECT * FROM InsuranceProviders;
END$$

CREATE PROCEDURE sp_UpdateProvider(
  IN prov_id   INT,
  IN prov_phone VARCHAR(20),
  IN prov_email VARCHAR(150)
)
BEGIN
  UPDATE InsuranceProviders
    SET contact_phone = prov_phone,
        contact_email = prov_email
  WHERE provider_id = prov_id;
END$$

CREATE PROCEDURE sp_DeleteProvider(IN prov_id INT)
BEGIN
  DELETE FROM InsuranceProviders WHERE provider_id = prov_id;
END$$


-- ====================
-- 21.9 InsurancePolicies: CRUD (NEW)
-- ====================
CREATE PROCEDURE sp_AddInsurancePolicy(
  IN ip_prov_id      INT,
  IN ip_policy_num   VARCHAR(100),
  IN ip_cov_details  TEXT,
  IN ip_start_date   DATE,
  IN ip_end_date     DATE
)
BEGIN
  INSERT INTO InsurancePolicies(
    provider_id, policy_number, coverage_details, start_date, end_date
  )
  VALUES(ip_prov_id, ip_policy_num, ip_cov_details, ip_start_date, ip_end_date);
END$$

CREATE PROCEDURE sp_GetInsurancePolicies()
BEGIN
  SELECT 
    ip.policy_id,
    ip.provider_id,
    p.name           AS provider_name,
    ip.policy_number,
    ip.coverage_details,
    ip.start_date,
    ip.end_date
  FROM InsurancePolicies ip
  JOIN InsuranceProviders p ON ip.provider_id = p.provider_id;
END$$

CREATE PROCEDURE sp_UpdateInsurancePolicy(
  IN ip_id           INT,
  IN ip_policy_num   VARCHAR(100),
  IN ip_cov_details  TEXT,
  IN ip_start_date   DATE,
  IN ip_end_date     DATE
)
BEGIN
  UPDATE InsurancePolicies
    SET policy_number    = ip_policy_num,
        coverage_details = ip_cov_details,
        start_date       = ip_start_date,
        end_date         = ip_end_date
  WHERE policy_id = ip_id;
END$$

CREATE PROCEDURE sp_DeleteInsurancePolicy(IN ip_id INT)
BEGIN
  DELETE FROM InsurancePolicies WHERE policy_id = ip_id;
END$$


-- ====================
-- 21.10 PatientInsurance: CRUD (modified)
-- ====================
CREATE PROCEDURE sp_AddPatientInsurance(
  IN p_id     INT,
  IN pol_id   INT
)
BEGIN
  INSERT INTO PatientInsurance (patient_id, policy_id)
  VALUES (p_id, pol_id);
END$$

CREATE PROCEDURE sp_GetPatientInsurance()
BEGIN
  SELECT
    pi.patient_id,
    CONCAT(p.first_name,' ',p.last_name) AS patient_name,
    pi.policy_id,
    ip.policy_number,
    ip.coverage_details,
    ip.start_date,
    ip.end_date
  FROM PatientInsurance pi
  JOIN Patients p ON pi.patient_id   = p.patient_id
  JOIN InsurancePolicies ip ON pi.policy_id = ip.policy_id;
END$$

CREATE PROCEDURE sp_DeletePatientInsurance(
  IN p_id   INT,
  IN pol_id INT
)
BEGIN
  DELETE FROM PatientInsurance
  WHERE patient_id = p_id
    AND policy_id  = pol_id;
END$$


-- ====================
-- 21.11 Invoices: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddInvoice(
  IN p_id     INT,
  IN inv_date DATETIME,
  IN tot_amt  DECIMAL(10,2),
  IN st       ENUM('Pending','Paid','Cancelled')
)
BEGIN
  INSERT INTO Invoices(patient_id, invoice_date, total_amount, status)
  VALUES(p_id, inv_date, tot_amt, st);
END$$

CREATE PROCEDURE sp_GetInvoices()
BEGIN
  SELECT * FROM Invoices;
END$$

CREATE PROCEDURE sp_UpdateInvoiceStatus(
  IN inv_id     INT,
  IN new_status ENUM('Pending','Paid','Cancelled')
)
BEGIN
  UPDATE Invoices
    SET status = new_status
  WHERE invoice_id = inv_id;
END$$

CREATE PROCEDURE sp_DeleteInvoice(IN inv_id INT)
BEGIN
  DELETE FROM Invoices WHERE invoice_id = inv_id;
END$$


-- ====================
-- 21.12 Payments: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddPayment(
  IN inv_id  INT,
  IN pay_date DATETIME,
  IN amt_paid DECIMAL(10,2),
  IN method  ENUM('Cash','Card','Insurance','Other'),
  IN tx_ref  VARCHAR(100)
)
BEGIN
  INSERT INTO Payments(invoice_id, payment_date, amount_paid, method, transaction_ref)
  VALUES(inv_id, pay_date, amt_paid, method, tx_ref);
END$$

CREATE PROCEDURE sp_GetPayments()
BEGIN
  SELECT * FROM Payments;
END$$

CREATE PROCEDURE sp_UpdatePayment(
  IN pay_id   INT,
  IN amt_paid DECIMAL(10,2),
  IN method   ENUM('Cash','Card','Insurance','Other'),
  IN tx_ref   VARCHAR(100)
)
BEGIN
  UPDATE Payments
    SET amount_paid     = amt_paid,
        method          = method,
        transaction_ref = tx_ref
  WHERE payment_id = pay_id;
END$$

CREATE PROCEDURE sp_DeletePayment(IN pay_id INT)
BEGIN
  DELETE FROM Payments WHERE payment_id = pay_id;
END$$


-- ====================
-- 21.13 Appointments: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddAppointment(
  IN p_patient_id INT,
  IN p_doctor_id  INT,
  IN p_date       DATETIME,
  IN p_status     ENUM('Pending','Confirmed','Completed','Canceled','Rescheduled','No Show'),
  IN p_notes      TEXT
)
BEGIN
  INSERT INTO Appointments(patient_id, doctor_id, appointment_date, status, notes)
  VALUES (p_patient_id, p_doctor_id, p_date, p_status, p_notes);
END$$

CREATE PROCEDURE sp_GetAppointments()
BEGIN
  SELECT * FROM Appointments;
END$$

CREATE PROCEDURE sp_UpdateAppointment(
  IN p_appointment_id INT,
  IN p_patient_id     INT,
  IN p_doctor_id      INT,
  IN p_date           DATETIME,
  IN p_status         ENUM('Pending','Confirmed','Completed','Canceled','Rescheduled','No Show'),
  IN p_notes          TEXT
)
BEGIN
  UPDATE Appointments
    SET patient_id       = p_patient_id,
        doctor_id        = p_doctor_id,
        appointment_date = p_date,
        status           = p_status,
        notes            = p_notes
  WHERE appointment_id = p_appointment_id;
END$$

CREATE PROCEDURE sp_DeleteAppointment(IN p_appointment_id INT)
BEGIN
  DELETE FROM Appointments WHERE appointment_id = p_appointment_id;
END$$


-- ====================
-- 21.14 MedicalRecords: CRUD (unchanged)
-- ====================
CREATE PROCEDURE sp_AddMedicalRecord(
  IN p_appointment_id  INT,
  IN p_diagnosis       TEXT,
  IN p_visit_summary   TEXT,
  IN p_doctor_notes    TEXT
)
BEGIN
  INSERT INTO MedicalRecords(appointment_id, diagnosis, visit_summary, doctor_notes)
  VALUES (p_appointment_id, p_diagnosis, p_visit_summary, p_doctor_notes);
END$$

CREATE PROCEDURE sp_GetMedicalRecords()
BEGIN
  SELECT * FROM MedicalRecords;
END$$

CREATE PROCEDURE sp_UpdateMedicalRecord(
  IN p_record_id     INT,
  IN p_diagnosis     TEXT,
  IN p_visit_summary TEXT,
  IN p_doctor_notes  TEXT
)
BEGIN
  UPDATE MedicalRecords
    SET diagnosis     = p_diagnosis,
        visit_summary = p_visit_summary,
        doctor_notes  = p_doctor_notes
  WHERE record_id = p_record_id;
END$$

CREATE PROCEDURE sp_DeleteMedicalRecord(IN p_record_id INT)
BEGIN
  DELETE FROM MedicalRecords WHERE record_id = p_record_id;
END$$


-- ====================
-- 21.15 Prescriptions: CRUD (modified to use frequency_id)
-- ====================
CREATE PROCEDURE sp_AddPrescription(
  IN p_record_id       INT,
  IN p_medication_name VARCHAR(255),
  IN p_dosage          VARCHAR(100),
  IN p_frequency_id    INT,
  IN p_start_date      DATE,
  IN p_end_date        DATE,
  IN p_instructions    TEXT
)
BEGIN
  INSERT INTO Prescriptions(
    record_id, medication_name, dosage, frequency_id, start_date, end_date, instructions
  )
  VALUES (
    p_record_id, p_medication_name, p_dosage, p_frequency_id, p_start_date, p_end_date, p_instructions
  );
END$$

CREATE PROCEDURE sp_GetPrescriptions()
BEGIN
  SELECT 
    pr.prescription_id,
    pr.record_id,
    mr.appointment_id,
    pr.medication_name,
    pr.dosage,
    f.name            AS frequency,
    pr.start_date,
    pr.end_date,
    pr.instructions
  FROM Prescriptions pr
  JOIN FrequencyTypes f ON pr.frequency_id = f.frequency_id
  JOIN MedicalRecords mr ON pr.record_id = mr.record_id;
END$$

CREATE PROCEDURE sp_UpdatePrescription(
  IN p_prescription_id INT,
  IN p_medication_name VARCHAR(255),
  IN p_dosage          VARCHAR(100),
  IN p_frequency_id    INT,
  IN p_start_date      DATE,
  IN p_end_date        DATE,
  IN p_instructions    TEXT
)
BEGIN
  UPDATE Prescriptions
    SET medication_name = p_medication_name,
        dosage          = p_dosage,
        frequency_id    = p_frequency_id,
        start_date      = p_start_date,
        end_date        = p_end_date,
        instructions    = p_instructions
  WHERE prescription_id = p_prescription_id;
END$$

CREATE PROCEDURE sp_DeletePrescription(IN p_prescription_id INT)
BEGIN
  DELETE FROM Prescriptions WHERE prescription_id = p_prescription_id;
END$$

DELIMITER ;


-- ============================================================================ 
-- 22) INDEXES FOR COMMON LOOKUPS
--    Keep the same indexes plus any new ones for our lookup tables.
-- ============================================================================ 
-- Patients last_name lookup
CREATE INDEX idx_pat_lastname ON Patients(last_name);

-- Doctors specialization lookup
CREATE INDEX idx_doc_specialization ON Doctors(specialization_id);

-- Invoices status lookup
CREATE INDEX idx_invoice_status ON Invoices(status);

-- Departments name lookup
CREATE INDEX idx_dept_name ON Departments(name);

-- Specializations name lookup
CREATE INDEX idx_spec_name ON Specializations(name);

-- StaffRoles name lookup
CREATE INDEX idx_role_name ON StaffRoles(name);

-- Policies group by provider
CREATE INDEX idx_policy_provider ON InsurancePolicies(provider_id);

-- FrequencyTypes name lookup
CREATE INDEX idx_freq_name ON FrequencyTypes(name);

