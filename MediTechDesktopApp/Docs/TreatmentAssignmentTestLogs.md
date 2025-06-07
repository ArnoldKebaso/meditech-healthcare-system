# Treatment Assignment Form – Test Log

---

### ASG-TC01 — Load Lookups & Grid

**Feature:** Load Lookups & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Assignments** in left nav.  
3. Observe that:  
   - `cbPatient` dropdown is populated.  
   - `cbTreatment` dropdown is populated.  
   - `cbDoctor` dropdown is populated.  
   - `cbNurse` dropdown is populated.  
   - DataGrid shows existing assignments.
</details>

<details>
<summary>✅ Expected Result</summary>

- All four combo boxes contain at least one item (if DB has entries).  
- DataGrid lists rows with columns: PatientFullName, TreatmentName, DoctorFullName, NurseFullName, Date, Notes.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### ASG-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Choose a patient from `cbPatient` (e.g. “John Doe”).  
3. Choose a treatment from `cbTreatment` (e.g. “Physiotherapy”).  
4. Choose a doctor from `cbDoctor`.  
5. Choose a nurse from `cbNurse`.  
6. Pick today’s date in `dpDate`.  
7. Enter “Test assignment” in `txtNotes`.  
8. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New assignment added.”  
- After clicking “OK,” DataGrid reloads and shows the new row at top with correct Patient, Treatment, Doctor, Nurse, Date, Notes.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### ASG-TC03 — Edit (Modify Notes)

**Feature:** Edit (Modify Notes)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select row created in ASG-TC02.  
2. Click **Edit**.  
3. Change Notes to “Updated note.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Assignment updated.”  
- DataGrid reloads; selected row now shows “Updated note.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### ASG-TC04 — Delete Assignment

**Feature:** Delete Assignment  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select row from ASG-TC02/03.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Assignment deleted.”  
- After “OK,” the row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### ASG-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current assignments.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### ASG-TC06 — Validation (Required)

**Feature:** Validation (Required)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave “Patient” blank; choose other fields.  
3. Click **Save**.  
4. Repeat leaving “Treatment”, “Doctor”, “Nurse”, or “Date” blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Patient, Treatment, Doctor, Nurse, and Date are required.”  
- No new row is inserted/updated.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### ASG-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing assignment row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** are disabled while editing.  
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
