# Treatment Form – Test Log

---

### TR-TC01 — Load Grid

**Feature:** Load Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Treatments** in left nav.  
3. Observe:  
   - DataGrid lists treatments with columns: TreatmentId, Name, Description, Cost.
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid populates with every treatment record from the database.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### TR-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Enter:  
   - Name = “X-Ray”  
   - Description = “Diagnostic imaging”  
   - Cost = “100.00”  
3. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New treatment added.”  
- After “OK,” DataGrid reloads. Top row shows “X-Ray,” “Diagnostic imaging,” $100.00.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### TR-TC03 — Edit (Modify Cost Only)

**Feature:** Edit (Modify Cost Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select row added in TR-TC02.  
2. Click **Edit**.  
3. Change Cost to “120.50.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Treatment updated.”  
- After reload, that row’s Cost changes to $120.50.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### TR-TC04 — Delete Treatment

**Feature:** Delete Treatment  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select row from TR-TC02/03.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Treatment deleted.”  
- After “OK,” that row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### TR-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current treatments (no duplicates).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### TR-TC06 — Validation: Required Fields & Format

**Feature:** Validation: Required Fields & Format  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave “Name” blank; fill others; click **Save**.  
3. Repeat leaving “Description” blank or “Cost” blank.  
4. Enter invalid “Cost” (e.g. “abc”); click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Please fill in all required fields (Name, Description, Cost).”  
- If invalid: “Cost must be a valid decimal number.”  
- No treatment is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### TR-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing treatment row.  
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
