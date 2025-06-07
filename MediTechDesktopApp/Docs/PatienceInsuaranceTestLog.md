# Patient Insurance Form – Test Log

---

### PI-TC01 — Load Dropdowns & Grid

**Feature:** Load Dropdowns & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **PtInsurance** in left nav.  
3. Observe:  
   - `cbPatient` shows all patients.  
   - `cbProvider` shows all providers.  
   - `cbPolicy` shows all policies.  
   - DataGrid lists assignments with columns: ID, PatientFullName, ProviderName, PolicyName, AssignDate.
</details>

<details>
<summary>✅ Expected Result</summary>

- All three dropdowns populated correctly.  
- DataGrid shows every assignment with correct joined fields.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PI-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select:  
   - Patient = “John Doe”  
   - Provider = “HealthPlus”  
   - Policy = “Gold Plan”  
3. Pick today’s date in `dpAssignDate`.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New patient insurance added.”  
- After “OK,” DataGrid reloads. Top row shows John Doe, HealthPlus, Gold Plan, today’s date.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PI-TC03 — Edit (Modify Assign Date Only)

**Feature:** Edit (Modify Assign Date Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select row added in PI-TC02.  
2. Click **Edit**.  
3. Change `dpAssignDate` to 3 days from today.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Patient insurance updated.”  
- DataGrid reloads; that row’s date updates accordingly.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PI-TC04 — Delete Patient Insurance

**Feature:** Delete Patient Insurance  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from PI-TC02/03.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Patient insurance deleted.”  
- After “OK,” row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PI-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads; shows all assignments without duplicates.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PI-TC06 — Validation: Required Fields

**Feature:** Validation: Required Fields  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave Patient, Provider, Policy, or AssignDate blank; click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “All fields (Patient, Provider, Policy, Assign Date) are required.”  
- No record is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PI-TC07 — Edit/Save Lock Behavior

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

- While editing, **New** and **Refresh** remain disabled; after **Save**, they become enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
