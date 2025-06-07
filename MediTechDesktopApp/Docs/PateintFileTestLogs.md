# Patient File Form – Test Log

---

### PTF-TC01 — Load Patients & Grid

**Feature:** Load Patients & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **PatientFiles** in left nav.  
3. Observe:  
   - `cbPatients` dropdown is populated.  
   - DataGrid shows existing files with columns: FileId, PatientFullName, FileName, FileType, FileSizeBytes, UploadTimestamp.
</details>

<details>
<summary>✅ Expected Result</summary>

- `cbPatients` has at least one entry (if DB has patients).  
- DataGrid lists all patient files with correct metadata (no blank rows).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PTF-TC02 — New + Browse + Save

**Feature:** New + Browse + Save  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Enable `cbPatients`; select “John Doe.”  
3. Click **Browse…**, pick “test.txt.”  
4. Verify `txtFileName` shows “test.txt”; `txtFilePath` shows full path.  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New patient file added.”  
- After “OK,” DataGrid reloads showing new row with FileName=“test.txt,” correct PatientFullName, FileType, FileSizeBytes, UploadTimestamp.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PTF-TC03 — Delete Patient File

**Feature:** Delete Patient File  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from PTF-TC02.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Patient file deleted.”  
- After “OK,” that row is removed from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PTF-TC04 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current patient files.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PTF-TC05 — Validation (Missing Fields)

**Feature:** Validation (Missing Fields)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Do not select a patient or browse for a file.  
3. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Please select a patient and a file first.”  
- No file is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PTF-TC06 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing patient-file row (if any).  
2. Click **Edit** (should be disabled).
</details>

<details>
<summary>✅ Expected Result</summary>

- **Edit** remains disabled (no edit support).  
- **New** and **Refresh** remain enabled.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
