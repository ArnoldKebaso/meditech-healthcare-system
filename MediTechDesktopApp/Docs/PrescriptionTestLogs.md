# Prescription Form – Test Log

---

### PR-TC01 — Load Patient & Doctor Dropdowns & Grid

**Feature:** Load Patient & Doctor Dropdowns & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Prescriptions** in left nav.  
3. Observe:  
   - `cbPatient` shows all patients.  
   - `cbDoctor` shows all doctors.  
   - DataGrid lists prescriptions with columns: ID, PatientFullName, DoctorFullName, PrescriptionDate, Medication, Dosage, Frequency, Duration, Notes.
</details>

<details>
<summary>✅ Expected Result</summary>

- Both dropdowns are populated.  
- DataGrid shows all prescriptions with correct joined fields.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PR-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select:  
   - Patient = “Jane Smith”  
   - Doctor  = “Dr. Jones”  
3. Pick today’s date in `dpPrescriptionDate`.  
4. Enter:  
   - Medication = “Ibuprofen”  
   - Dosage     = “200 mg”  
   - Frequency  = “Twice a day”  
   - Duration   = “5 days”  
   - Notes      = “Take with food.”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New prescription added.”  
- After “OK,” DataGrid reloads. Top row shows correct Patient, Doctor, Date, Medication, Dosage, Frequency, Duration, Notes.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PR-TC03 — Edit (Modify Notes Only)

**Feature:** Edit (Modify Notes Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in PR-TC02.  
2. Click **Edit**.  
3. Change **Notes** to “Take with food thrice daily.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Prescription updated.”  
- After reload, that row’s Notes changes accordingly.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PR-TC04 — Delete Prescription

**Feature:** Delete Prescription  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from PR-TC02/03.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Prescription deleted.”  
- After “OK,” that row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PR-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current prescriptions.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PR-TC06 — Validation: Required Fields

**Feature:** Validation: Required Fields  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave required fields blank (Patient, Doctor, Date, Medication, Dosage, Frequency, Duration).  
3. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Please fill in all required fields (Patient, Doctor, Date, Medication, Dosage, Frequency, Duration).”  
- No new prescription is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PR-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing prescription row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** remain disabled while editing.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- While editing, **New** and **Refresh** remain disabled; after **Save**, both become enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
