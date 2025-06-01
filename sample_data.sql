USE meditech;

-- ───────────────────────────────────────────────────────────────────────────────
-- 1) Patients (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Patients (patient_id, first_name, last_name, date_of_birth, gender, contact_phone, contact_email, address)
VALUES
  (1, 'Alice',   'Johnson',   '1980-08-15', 'Female', '555-1001', 'alice.johnson@example.com',   '123 Maple Street'),
  (2, 'Bob',     'Smith',     '1975-02-20', 'Male',   '555-1002', 'bob.smith@example.com',       '456 Oak Avenue'),
  (3, 'Carol',   'Anderson',  '1990-12-05', 'Female', '555-1003', 'carol.anderson@example.com',  '789 Pine Road'),
  (4, 'David',   'Lee',       '1965-07-30', 'Male',   '555-1004', 'david.lee@example.com',       '321 Elm Boulevard'),
  (5, 'Evelyn',  'Nguyen',    '2000-03-18', 'Female', '555-1005', 'evelyn.nguyen@example.com',   '654 Cedar Lane');


-- ───────────────────────────────────────────────────────────────────────────────
-- 2) Doctors (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Doctors (doctor_id, first_name, last_name, specialization, contact_phone, contact_email)
VALUES
  (1, 'Robert',  'Smith',    'Cardiology',        '555-2001', 'robert.smith@hospital.com'),
  (2, 'Linda',   'Martinez', 'Pediatrics',        '555-2002', 'linda.martinez@hospital.com'),
  (3, 'Michael', 'Brown',    'Orthopedics',       '555-2003', 'michael.brown@hospital.com'),
  (4, 'Jessica', 'Williams', 'Dermatology',       '555-2004', 'jessica.williams@hospital.com'),
  (5, 'Thomas',  'Garcia',   'Neurology',         '555-2005', 'thomas.garcia@hospital.com');


-- ───────────────────────────────────────────────────────────────────────────────
-- 3) Nurses (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Nurses (nurse_id, first_name, last_name, department, contact_phone, contact_email)
VALUES
  (1, 'Maria',  'Lopez',    'Emergency',      '555-3001', 'maria.lopez@hospital.com'),
  (2, 'David',  'Kim',      'ICU',            '555-3002', 'david.kim@hospital.com'),
  (3, 'Angela', 'Patel',    'Pediatrics',     '555-3003', 'angela.patel@hospital.com'),
  (4, 'Jason',  'Wong',     'Surgery',        '555-3004', 'jason.wong@hospital.com'),
  (5, 'Susan',  'Garcia',   'Dermatology',    '555-3005', 'susan.garcia@hospital.com');


-- ───────────────────────────────────────────────────────────────────────────────
-- 4) AdminStaff (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO AdminStaff (staff_id, first_name, last_name, role, contact_phone, contact_email)
VALUES
  (1, 'Karen',   'Davis',    'Receptionist',    '555-4001', 'karen.davis@hospital.com'),
  (2, 'George',  'Hernandez','Billing Clerk',   '555-4002', 'george.hernandez@hospital.com'),
  (3, 'Patricia','Lopez',    'HR Manager',      '555-4003', 'patricia.lopez@hospital.com'),
  (4, 'Steven',  'Clark',    'IT Support',      '555-4004', 'steven.clark@hospital.com'),
  (5, 'Michelle','Adams',    'Office Manager',  '555-4005', 'michelle.adams@hospital.com');


-- ───────────────────────────────────────────────────────────────────────────────
-- 5) Treatments (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Treatments (treatment_id, name, description, created_at)
VALUES
  (1, 'Physical Therapy', 'Rehabilitation sessions to improve mobility and strength.',        '2025-01-15 08:00:00'),
  (2, 'Chemotherapy',     'Cancer treatment using chemical substances to target tumor cells.', '2025-02-01 09:30:00'),
  (3, 'Radiology Scan',   'Diagnostic imaging (X-ray, CT, MRI) to view internal structures.',   '2025-03-10 10:15:00'),
  (4, 'Vaccination',      'Administration of vaccine to provide immunity against a disease.',    '2025-04-05 11:00:00'),
  (5, 'Cardiac Rehab',    'Supervised exercise and education for heart disease patients.',       '2025-05-20 14:45:00');


-- ───────────────────────────────────────────────────────────────────────────────
-- 6) Appointments (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Appointments (appointment_id, patient_id, doctor_id, appointment_date, status, notes)
VALUES
  (1,  1, 1, '2025-06-01 09:00:00', 'Confirmed',  'Annual cardiology check‐up'),
  (2,  2, 2, '2025-06-02 10:30:00', 'Pending',    'Routine pediatric visit'),
  (3,  3, 3, '2025-06-03 11:15:00', 'Completed',  'Orthopedic follow‐up'),
  (4,  4, 4, '2025-06-04 14:00:00', 'Confirmed',  'Dermatology skin rash evaluation'),
  (5,  5, 5, '2025-06-05 15:45:00', 'No Show',    'Neurology consultation');


-- ───────────────────────────────────────────────────────────────────────────────
-- 7) MedicalRecords (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO MedicalRecords (record_id, appointment_id, diagnosis, visit_summary, doctor_notes, created_at)
VALUES
  (1,  1, 'Mild Arrhythmia',       'Patient complained of palpitations and shortness of breath.', 'Recommend lifestyle changes; follow-up in 3 months.', '2025-06-01 09:45:00'),
  (2,  2, 'Seasonal Allergies',    'Sneezing, itchy eyes; mild congestion.',                     'Prescribed antihistamine spray; monitor for asthma symptoms.',  '2025-06-02 11:00:00'),
  (3,  3, 'Knee Meniscus Tear',    'Pain and swelling in left knee after soccer injury.',       'Ordered MRI; start physical therapy upon confirmation.',         '2025-06-03 12:00:00'),
  (4,  4, 'Eczema Flare',          'Red, itchy patches on forearms and behind knees.',          'Advise topical steroid cream twice daily; re‐evaluate in 2 weeks.', '2025-06-04 14:30:00'),
  (5,  5, 'Migraines',             'Severe headache with nausea; occasional aura.',             'Prescribed migraine prophylaxis; lifestyle modifications suggested.', '2025-06-05 16:00:00');


-- ───────────────────────────────────────────────────────────────────────────────
-- 8) Prescriptions (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Prescriptions (prescription_id, record_id, medication_name, dosage, frequency, start_date, end_date, instructions)
VALUES
  (1,  1, 'Metoprolol',   '50mg',   'Once daily',   '2025-06-01', '2025-12-01', 'Take with food in the morning.'),
  (2,  2, 'Cetirizine',   '10mg',   'Once daily',   '2025-06-02', '2025-06-30', 'Take at bedtime if symptoms persist.'),
  (3,  3, 'Ibuprofen',    '400mg',  'Every 6 hours','2025-06-03', '2025-06-10', 'Take with a meal to avoid stomach upset.'),
  (4,  4, 'Hydrocortisone Cream', 'Apply thin layer', 'Twice daily', '2025-06-04', '2025-06-18', 'Apply to affected areas after washing.'),
  (5,  5, 'Topiramate',   '25mg',   'Once daily',   '2025-06-05', '2025-12-05', 'Take at bedtime to minimize dizziness.');


-- ───────────────────────────────────────────────────────────────────────────────
-- 9) TreatmentAssignments (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO TreatmentAssignments (patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes)
VALUES
  (1,  1, '2025-06-06 08:00:00', 1, 1, 'Begin physical therapy exercises.'),
  (2,  2, '2025-06-07 09:30:00', 2, 3, 'First chemo session – monitor vitals closely.'),
  (3,  3, '2025-06-08 11:00:00', 3, 4, 'Radiology scan scheduled for knee.'),
  (4,  4, '2025-06-09 14:15:00', 4, 5, 'Topical treatment demonstration; patient education.'),
  (5,  5, '2025-06-10 15:45:00', 5, 2, 'Cardiac rehab orientation; monitor blood pressure.');


-- ───────────────────────────────────────────────────────────────────────────────
-- 10) PatientFiles (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
-- NOTE: We store dummy values for file_data; set file_size_bytes = 0 and file_data = NULL
INSERT INTO PatientFiles (file_id, patient_id, file_name, file_type, file_size_bytes, upload_timestamp, file_data)
VALUES
  (1,  1, 'alice_ecg_report.pdf',   'application/pdf', 0, '2025-06-01 10:00:00', NULL),
  (2,  2, 'bob_allergy_test.jpg',   'image/jpeg',      0, '2025-06-02 11:30:00', NULL),
  (3,  3, 'carol_knee_mri.dcm',     'application/dicom',0, '2025-06-03 12:30:00', NULL),
  (4,  4, 'david_skin_biopsy.png',  'image/png',       0, '2025-06-04 15:00:00', NULL),
  (5,  5, 'evelyn_migraine_ct.pdf', 'application/pdf', 0, '2025-06-05 16:30:00', NULL);


-- ───────────────────────────────────────────────────────────────────────────────
-- 11) InsuranceProviders (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO InsuranceProviders (provider_id, name, contact_phone, contact_email)
VALUES
  (1, 'HealthSafe Inc',   '555-5001', 'contact@healthsafe.com'),
  (2, 'WellnessCare',     '555-5002', 'info@wellnesscare.com'),
  (3, 'PrimeHealth',      '555-5003', 'support@primehealth.com'),
  (4, 'LifeGuard Insurance', '555-5004', 'service@lifeguard.com'),
  (5, 'SecureMed Coverage','555-5005','help@securemed.com');


-- ───────────────────────────────────────────────────────────────────────────────
-- 12) PatientInsurance (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO PatientInsurance (patient_id, provider_id, policy_number, coverage_details, start_date, end_date)
VALUES
  (1, 1, 'HS-0001', 'Covers 90% of cardiology treatments; 80% of tests', '2025-01-01', '2026-01-01'),
  (2, 2, 'WC-12345','Covers routine pediatric care; vaccines fully covered',     '2025-03-01', '2026-03-01'),
  (3, 3, 'PH-98765','Covers orthopedic surgeries at 70%; imaging at 60%',       '2025-05-01', '2026-05-01'),
  (4, 4, 'LG-55555','Covers dermatology consults and prescriptions fully',       '2025-06-01', '2026-06-01'),
  (5, 5, 'SM-24680','Covers neurology diagnostics; partial coverage for meds',    '2025-07-01', '2026-07-01');


-- ───────────────────────────────────────────────────────────────────────────────
-- 13) Invoices (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Invoices (invoice_id, patient_id, invoice_date, total_amount, status)
VALUES
  (1,  1, '2025-06-07 08:30:00', 250.00, 'Pending'),
  (2,  2, '2025-06-08 09:45:00', 150.00, 'Paid'),
  (3,  3, '2025-06-09 10:15:00', 350.00, 'Pending'),
  (4,  4, '2025-06-10 11:00:00', 100.00, 'Cancelled'),
  (5,  5, '2025-06-11 12:00:00', 500.00, 'Pending');


-- ───────────────────────────────────────────────────────────────────────────────
-- 14) Payments (5 rows)
-- ───────────────────────────────────────────────────────────────────────────────
INSERT INTO Payments (payment_id, invoice_id, payment_date, amount_paid, method, transaction_ref)
VALUES
  (1,  2, '2025-06-08 10:00:00', 150.00, 'Card',      'TXN-1001'),
  (2,  1, '2025-06-07 09:00:00', 100.00, 'Insurance', 'INS-2001'),
  (3,  3, '2025-06-09 11:00:00', 200.00, 'Cash',      'CASH-3001'),
  (4,  5, '2025-06-11 13:00:00', 300.00, 'Card',      'TXN-5001'),
  (5,  5, '2025-06-11 14:00:00', 200.00, 'Card',      'TXN-5002';
  
-- Note: Invoice #5 has two partial payments: $300 + $200 = $500 total.


-- ───────────────────────────────────────────────────────────────────────────────
-- 15) (Optional) If you need to reset AUTO_INCREMENT counters to match above IDs:
-- ───────────────────────────────────────────────────────────────────────────────
ALTER TABLE Patients         AUTO_INCREMENT = 6;
ALTER TABLE Doctors          AUTO_INCREMENT = 6;
ALTER TABLE Nurses           AUTO_INCREMENT = 6;
ALTER TABLE AdminStaff       AUTO_INCREMENT = 6;
ALTER TABLE Treatments       AUTO_INCREMENT = 6;
ALTER TABLE Appointments     AUTO_INCREMENT = 6;
ALTER TABLE MedicalRecords   AUTO_INCREMENT = 6;
ALTER TABLE Prescriptions    AUTO_INCREMENT = 6;
ALTER TABLE PatientFiles     AUTO_INCREMENT = 6;
ALTER TABLE InsuranceProviders AUTO_INCREMENT = 6;
ALTER TABLE PatientInsurance AUTO_INCREMENT = 1;  -- composite PK, no single auto‐inc
ALTER TABLE Invoices         AUTO_INCREMENT = 6;
ALTER TABLE Payments         AUTO_INCREMENT = 6;
-- TreatmentAssignments has a composite primary key; no AUTO_INCREMENT needed.
