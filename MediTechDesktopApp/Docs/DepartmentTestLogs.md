# Department Form – Test Log

---

### DEP-TC01 — Load Grid

**Feature:** Load Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Departments** in left nav.  
3. Observe:  
   - DataGrid lists existing departments (columns: ID, DepartmentName, Location).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid populates with all departments from the database.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DEP-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Enter “Radiology” in `txtDeptName`.  
3. Enter “2nd Floor” in `txtLocation`.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message box: “New department added.”  
- After “OK,” DataGrid reloads. The top row shows “Radiology” / “2nd Floor.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DEP-TC03 — Edit (Modify Location Only)

**Feature:** Edit (Modify Location Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in DEP-TC02.  
2. Click **Edit**.  
3. Change `txtLocation` to “3rd Floor.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message box: “Department updated.”  
- DataGrid reloads, and that row’s Location changes to “3rd Floor.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DEP-TC04 — Delete Department

**Feature:** Delete Department  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from DEP-TC02/03.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message box: “Department deleted.”  
- After “OK,” that row is removed from the DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DEP-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current departments.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DEP-TC06 — Validation (Required Fields)

**Feature:** Validation (Required Fields)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave `txtDeptName` blank; fill `txtLocation`.  
3. Click **Save**.  
4. Repeat leaving `txtLocation` blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning appears: “Department Name and Location are required.”  
- No department is inserted/updated.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DEP-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing department row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** are disabled while editing.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- **New** and **Refresh** remain disabled while editing; after **Save**, they become enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
