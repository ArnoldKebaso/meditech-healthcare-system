# Medical Record Form – Test Log

---

### MR-TC01 — Load Patient Dropdown & Grid

**Feature:** Load Patient Dropdown & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **MedRecords** in left nav.  
3. Observe:  
   - `cbPatient` dropdown is populated.  
   - DataGrid lists existing records with columns: ID, PatientFullName, MedName, Dosage, Frequency, StartDate, EndDate, Instructions.
</details>

<details>
<summary>✅ Expected Result</summary>

- Dropdown contains at least one patient.  
- DataGrid shows all medical records with correct columns.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### MR-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select a patient (e.g. “John Doe”).  
3. Enter:  
   - MedName = “Aspirin”  
   - Dosage = “100 mg”  
   - Frequency = “Once a day”  
   - StartDate = today’s date  
   - EndDate = one week from today  
   - Instructions = “Take with water.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New medical record added.”  
- After “OK,” DataGrid reloads. Top row shows correct values.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### MR-TC03 — Edit (Modify Instructions Only)

**Feature:** Edit (Modify Instructions Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in MR-TC02.  
2. Click **Edit**.  
3. Change `txtInstructions` to “Take with water after meal.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Medical record updated.”  
- DataGrid reloads, and that record’s Instructions updates accordingly.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### MR-TC04 — Delete Medical Record

**Feature:** Delete Medical Record  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from MR-TC02/03.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Medical record deleted.”  
- After “OK,” that row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### MR-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current medical records.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### MR-TC06 — Validation: Required Fields

**Feature:** Validation: Required Fields  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave any one of the fields blank (Patient, MedName, Dosage, Frequency, StartDate, EndDate, Instructions).  
3. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “All fields (Patient, MedName, Dosage, Frequency, Start Date, End Date, Instructions) are required.”  
- No record is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### MR-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing record.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** remain disabled while editing.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- While editing, **New** and **Refresh** remain disabled; after **Save**, both re-enable.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
