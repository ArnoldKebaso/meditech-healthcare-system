- =======================================================================================
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
