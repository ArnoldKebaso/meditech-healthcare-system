# Appointment Form – Test Log

---

### APP-TC01 — Load Lookups & Grid

**Feature:** Load Lookups & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Appointments** in left nav.  
3. Observe:  
   - `cbPatient` dropdown is populated.  
   - `cbDoctor` dropdown is populated.  
   - DataGrid lists existing appointments with columns: ID, PatientFullName, DoctorFullName, Date, Reason.
</details>

<details>
<summary>✅ Expected Result</summary>

- Both combo boxes contain at least one entry (if the DB has patients/doctors).  
- DataGrid shows all existing appointments with correct data.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### APP-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select a patient from `cbPatient` (e.g. “Jane Smith”).  
3. Select a doctor from `cbDoctor` (e.g. “Dr. Jones”).  
4. Pick today’s date in `dpAppointmentDate`.  
5. Enter “Routine checkup” in `txtReason`.  
6. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message box: “New appointment added.”  
- After clicking “OK,” DataGrid reloads.  
- The top row shows the new appointment with correct Patient, Doctor, Date, and Reason.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### APP-TC03 — Edit (Modify Reason Only)

**Feature:** Edit (Modify Reason Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in APP-TC02.  
2. Click **Edit**.  
3. Change **Reason** to “Follow‐up visit.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message box: “Appointment updated.”  
- DataGrid reloads, and that row’s Reason changes to “Follow‐up visit.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### APP-TC04 — Delete Appointment

**Feature:** Delete Appointment  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from APP-TC02/03.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message box: “Appointment deleted.”  
- After clicking “OK,” the row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### APP-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current appointments.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### APP-TC06 — Validation (Missing Fields)

**Feature:** Validation (Missing Fields)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave `cbPatient` blank; fill other fields.  
3. Click **Save**.  
4. Repeat leaving `cbDoctor` blank, `dpAppointmentDate` blank, or `txtReason` blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- A warning appears: “Patient, Doctor, Date, and Reason are required.”  
- No appointment is inserted/updated.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### APP-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing appointment row.  
2. Click **Edit**.  
3. Verify that **New** and **Refresh** are disabled while editing.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- While editing, **New** and **Refresh** remain disabled.  
- After **Save**, **New** and **Refresh** become enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
