# Patient Form – Test Log

---

### PAT-TC01 — Add Patient

**Feature:** Add Patient  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Patients** in left nav.  
3. Click **New**.  
4. Enter:  
   - First Name = “John”  
   - Last Name  = “Doe”  
   - DOB        = “1980-05-15”  
   - Gender     = “Male”  
   - Phone      = “555-4444”  
   - Email      = “jdoe@example.com”  
   - Address    = “123 Main St.”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

A new row appears in the grid with:  
- ID auto-generated  
- Name = “John Doe”  
- DOB = “1980-05-15”  
- Gender = “Male”  
- Phone = “555-4444”  
- Email = “jdoe@example.com”  
- Address = “123 Main St.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAT-TC02 — Edit Patient

**Feature:** Edit Patient  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in PAT-TC01.  
2. Click **Edit**.  
3. Change Address to “456 Elm St.”  
4. Change Phone to “555-5555.”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Address changes to “456 Elm St.”  
- Phone changes to “555-5555”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAT-TC03 — Delete Patient

**Feature:** Delete Patient  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from PAT-TC01/PAT-TC02.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- The selected patient disappears from the grid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAT-TC04 — Refresh Patients

**Feature:** Refresh Patients  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (after no data changes).
</details>

<details>
<summary>✅ Expected Result</summary>

- The grid reloads; showing the same set of patients (no duplicates).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAT-TC05 — Validation: Required Fields

**Feature:** Validation: Required Fields  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave one of the fields blank (First Name, Last Name, DOB, Gender, Phone, Email, Address).  
3. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Please fill in all fields (First Name, Last Name, DOB, Gender, Phone, Email, Address).”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAT-TC06 — Edit/Save Lock

**Feature:** Edit/Save Lock  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing patient row.  
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
