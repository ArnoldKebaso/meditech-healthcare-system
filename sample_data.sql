
USE meditech;
-- ───────────────────────────────────────────────────────────────────────────────
-- 1) Lookup Tables First (to satisfy FK constraints)
-- ───────────────────────────────────────────────────────────────────────────────

-- 1.1 Specializations (used by Doctors):
INSERT INTO Specializations (name) VALUES
  ('Cardiology'),
  ('Pediatrics'),
  ('Orthopedics'),
  ('Dermatology'),
  ('Neurology');

-- 1.2 Departments (used by Nurses):
INSERT INTO Departments (name) VALUES
  ('Emergency'),
  ('ICU'),
  ('Pediatrics'),
  ('Surgery'),
  ('Radiology');

-- 1.3 StaffRoles (used by AdminStaff):
INSERT INTO StaffRoles (name) VALUES
  ('Receptionist'),
  ('Billing Clerk'),
  ('HR Manager'),
  ('IT Support'),
  ('Office Manager');

-- 1.4 Treatments (dropdown list for TreatmentAssignments):
INSERT INTO Treatments (name, description) VALUES
  ('Physical Therapy',            'Therapeutic exercises and manual therapy.'),
  ('Chemotherapy',                'Cancer treatment using chemical agents.'),
  ('Radiation Therapy',           'Cancer treatment using ionizing radiation.'),
  ('Cardiac Bypass Surgery',      'Surgical procedure to improve blood flow to the heart.'),
  ('Dermabrasion',                'Cosmetic procedure to remove skin imperfections.');

-- 1.5 FrequencyTypes (used by Prescriptions):
INSERT INTO FrequencyTypes (name) VALUES
  ('Once a day'),
  ('Twice a day'),
  ('Every 6 hours'),
  ('Every 8 hours'),
  ('As needed');

-- 1.6 InsuranceProviders (used by InsurancePolicies):
INSERT INTO InsuranceProviders (name, contact_phone, contact_email) VALUES
  ('HealthSafe Inc',       '555-5001', 'contact@healthsafe.com'),
  ('WellnessCare',         '555-5002', 'info@wellnesscare.com'),
  ('PrimeHealth',          '555-5003', 'support@primehealth.com'),
  ('LifeGuard Insurance',  '555-5004', 'service@lifeguard.com'),
  ('SecureMed Coverage',   '555-5005', 'help@securemed.com');

-- 1.7 InsurancePolicies (used by PatientInsurance):
INSERT INTO InsurancePolicies (provider_id, policy_number, coverage_details, start_date, end_date) VALUES
  (1, 'HS-0001', 'Covers 90% of cardiology treatments; 80% of tests',       '2025-01-01', '2026-01-01'),
  (1, 'HS-0002', 'Covers emergency visits at 100%; partial pharmacy',       '2025-02-01', '2026-02-01'),
  (2, 'WC-12345','Covers routine pediatric care; vaccines fully covered',   '2025-03-01', '2026-03-01'),
  (3, 'PH-98765','Covers orthopedic surgeries at 70%; imaging at 60%',     '2025-05-01', '2026-05-01'),
  (4, 'LG-55555','Covers dermatology consults and prescriptions fully',     '2025-06-01', '2026-06-01');

-- ───────────────────────────────────────────────────────────────────────────────
-- 2) Core Tables with No Dependencies on “Later” Tables
-- ───────────────────────────────────────────────────────────────────────────────

-- 2.1 Patients:
INSERT INTO Patients (first_name, last_name, date_of_birth, gender, contact_phone, contact_email, address) VALUES
  ('Alice',   'Anderson', '1980-05-15', 'Female', '555-1000', 'alice.anderson@example.com', '123 Maple St'),
  ('Bob',     'Brown',    '1975-09-23', 'Male',   '555-1001', 'bob.brown@example.com',     '456 Oak Ave'),
  ('Carol',   'Clark',    '1990-11-30', 'Female', '555-1002', 'carol.clark@example.com',   '789 Pine Rd'),
  ('David',   'Davis',    '1965-01-10', 'Male',   '555-1003', 'david.davis@example.com',   '321 Birch Ln'),
  ('Eve',     'Edwards',  '1985-07-08', 'Female', '555-1004', 'eve.edwards@example.com',   '654 Cedar Pl');

-- 2.2 Doctors (must reference Specializations 1–5):
INSERT INTO Doctors (first_name, last_name, specialization_id, contact_phone, contact_email) VALUES
  ('Dr. John',    'Smith',   1, '555-2000', 'john.smith@hospital.com'),
  ('Dr. Emily',   'Jones',   2, '555-2001', 'emily.jones@clinic.com'),
  ('Dr. Michael', 'Taylor',  3, '555-2002', 'michael.taylor@health.org'),
  ('Dr. Sarah',   'Wilson',  4, '555-2003', 'sarah.wilson@derma.com'),
  ('Dr. Robert',  'Martinez',5, '555-2004', 'robert.martinez@neurology.com');

-- 2.3 Nurses (must reference Departments 1–5):
INSERT INTO Nurses (first_name, last_name, department_id, contact_phone, contact_email) VALUES
  ('Nancy',  'Nelson',    1, '555-3000', 'nancy.nelson@hospital.com'),
  ('Patricia','Olsen',    2, '555-3001', 'patricia.olsen@icu.com'),
  ('Karen',  'Parker',    3, '555-3002', 'karen.parker@pediatrics.com'),
  ('Lisa',   'Quinn',     4, '555-3003', 'lisa.quinn@surgery.com'),
  ('Angela', 'Reed',      5, '555-3004', 'angela.reed@radiology.com');

-- 2.4 AdminStaff (must reference StaffRoles 1–5):
INSERT INTO AdminStaff (first_name, last_name, role_id, contact_phone, contact_email) VALUES
  ('Frank',    'Foster',   1, '555-4000', 'frank.foster@meditech.com'),
  ('George',   'Gibson',   2, '555-4001', 'george.gibson@meditech.com'),
  ('Hannah',   'Hayes',    3, '555-4002', 'hannah.hayes@meditech.com'),
  ('Ian',      'Irwin',    4, '555-4003', 'ian.irwin@meditech.com'),
  ('Jack',     'Johnson',  5, '555-4004', 'jack.johnson@meditech.com');

-- ───────────────────────────────────────────────────────────────────────────────
-- 3) Tables That Depend on the Above but Not on “Later” Tables
-- ───────────────────────────────────────────────────────────────────────────────

-- 3.1 Appointments (must reference Patients 1–5 and Doctors 1–5):
INSERT INTO Appointments (patient_id, doctor_id, appointment_date, status, notes) VALUES
  (1, 1, '2025-06-10 09:00:00', 'Pending',   'Annual checkup'),
  (2, 2, '2025-06-11 10:30:00', 'Confirmed', 'Follow‐up on blood tests'),
  (3, 3, '2025-06-12 14:00:00', 'Completed', 'Post‐surgery review'),
  (4, 4, '2025-06-13 08:45:00', 'Canceled',  'Patient requested reschedule'),
  (5, 5, '2025-06-14 11:15:00', 'Pending',   'Dermatology consultation');

-- 3.2 MedicalRecords (must reference Appointments 1–5):
INSERT INTO MedicalRecords (appointment_id, diagnosis, visit_summary, doctor_notes) VALUES
  (1, 'Hypertension',        'BP elevated, counselled on diet.',      'Recommend low‐salt diet.'),
  (2, 'Hyperlipidemia',      'Cholesterol high; started on statin.', 'Prescribed Atorvastatin.'),
  (3, 'Post‐Op Recovery',    'Healing well; no complications.',      'Continue current wound care.'),
  (5, 'Acne Vulgaris',       'Moderate acne on face and back.',      'Prescribe topical retinoids.'),
  (4, 'N/A (Canceled)',      'No exam performed.',                   'Appointment canceled by patient.');

-- 3.3 Prescriptions (must reference MedicalRecords and FrequencyTypes):
INSERT INTO Prescriptions (record_id, medication_name, dosage, frequency_id, start_date, end_date, instructions) VALUES
  (1, 'Lisinopril',         '10 mg',    1, '2025-06-11', '2026-06-10', 'Once daily in morning.'),
  (2, 'Atorvastatin',       '20 mg',    1, '2025-06-12', '2026-06-11', 'Once daily at bedtime.'),
  (3, 'Ibuprofen',          '400 mg',   3, '2025-06-13', '2025-06-20', 'Every 6 hours as needed.'),
  (5, 'Tretinoin Cream',    '0.05%',    2, '2025-06-15', '2025-12-15', 'Apply twice a day.'),
  (4, 'No Medication',      '—',        5, '2025-06-13', '2025-06-13', 'No prescription since canceled.');

-- 3.4 TreatmentAssignments (must reference Patients, Treatments, Doctors, Nurses):
INSERT INTO TreatmentAssignments (patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes) VALUES
  (1, 1, '2025-06-10 09:30:00', 1, 1, 'Therapy started.'),
  (2, 2, '2025-06-11 11:00:00', 2, 2, 'First chemo session.'),
  (3, 3, '2025-06-12 14:30:00', 3, 3, 'Radiation consult.'),
  (4, 4, '2025-06-13 09:15:00', 4, 4, 'Surgery prep.'),
  (5, 5, '2025-06-14 11:45:00', 5, 5, 'Dermabrasion scheduled.');

-- 3.5 PatientFiles (must reference Patients 1–5):
--   For demonstration, we'll insert dummy file metadata without actual BLOB data.
INSERT INTO PatientFiles (patient_id, file_name, file_type, file_size_bytes) VALUES
  (1, 'alice_report.pdf',     'application/pdf',   250000),
  (2, 'bob_xray.jpg',         'image/jpeg',        500000),
  (3, 'carol_labresults.txt', 'text/plain',         20000),
  (4, 'david_mri_scan.dcm',   'application/dicom',2000000),
  (5, 'eve_photo.png',        'image/png',         350000);

-- 3.6 PatientInsurance (must reference Patients and InsurancePolicies):
INSERT INTO PatientInsurance (patient_id, policy_id) VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (3, 4),
  (4, 5);

-- 3.7 Invoices (must reference Patients 1–5):
INSERT INTO Invoices (patient_id, invoice_date, total_amount, status) VALUES
  (1, '2025-06-10 10:00:00',  500.00, 'Pending'),
  (2, '2025-06-11 12:00:00', 1200.00, 'Paid'),
  (3, '2025-06-12 15:00:00',  750.00, 'Cancelled'),
  (4, '2025-06-13 10:15:00', 2000.00, 'Pending'),
  (5, '2025-06-14 12:30:00',  300.00, 'Pending');

-- 3.8 Payments (must reference Invoices 1–5):
INSERT INTO Payments (invoice_id, payment_date, amount_paid, method, transaction_ref) VALUES
  (1, '2025-06-11 14:00:00', 500.00, 'Card',      'TX1001'),
  (2, '2025-06-11 13:30:00',1200.00, 'Cash',      'TX1002'),
  (4, '2025-06-14 09:45:00',1000.00, 'Insurance', 'TX1003'),
  (4, '2025-06-14 10:00:00',1000.00, 'Insurance', 'TX1004'),
  (5, '2025-06-15 08:00:00', 300.00, 'Card',      'TX1005');

-- Insert a sample admin user:
--    Password is “admin123” hashed with SHA256. (See below for how to compute.)
INSERT INTO users (username, password_hash, role)
VALUES
  ('admin', SHA2('admin123', 256), 'AdminStaff')
   ('drsmith', SHA2('docpass', 256), 'Doctor'),
  ('nursejane', SHA2('nursepass', 256), 'Nurse');

-- (Optional) Add other sample users:
--    e.g. a doctor account “drsmith” with password “docpass”
-- INSERT IGNORE INTO users (username, password_hash, role)
-- VALUES
--   ('drsmith', SHA2('docpass', 256), 'Doctor'),
--   ('nursejane', SHA2('nursepass', 256), 'Nurse');

-- SELECT SHA2('admin123', 256);
-- -- Copy that 64‐character hex value into your INSERT if you prefer manual insertion.

-- ───────────────────────────────────────────────────────────────────────────────
-- 4) Remaining Tables That Depend on “After” Tables
-- ───────────────────────────────────────────────────────────────────────────────

-- 4.1 FrequencyTypes (already seeded above) – no more inserts needed.

-- ───────────────────────────────────────────────────────────────────────────────
-- 5) Verify: SELECT from each table to ensure the INSERTs worked
--    (Optional: run these manually after you paste the script)
-- ───────────────────────────────────────────────────────────────────────────────

-- SELECT * FROM Specializations;
-- SELECT * FROM Departments;
-- SELECT * FROM StaffRoles;
-- SELECT * FROM Treatments;
-- SELECT * FROM FrequencyTypes;
-- SELECT * FROM InsuranceProviders;
-- SELECT * FROM InsurancePolicies;
-- SELECT * FROM Patients;
-- SELECT * FROM Doctors;
-- SELECT * FROM Nurses;
-- SELECT * FROM AdminStaff;
-- SELECT * FROM Appointments;
-- SELECT * FROM MedicalRecords;
-- SELECT * FROM Prescriptions;
-- SELECT * FROM TreatmentAssignments;
-- SELECT * FROM PatientFiles;
-- SELECT * FROM PatientInsurance;
-- SELECT * FROM Invoices;
-- SELECT * FROM Payments;
