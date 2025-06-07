# Nurse Form – Test Log

---

### NUR-TC01 — Add Nurse

**Feature:** Add Nurse  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Nurses** in left nav.  
3. Click **New**.  
4. Enter:  
   - First Name = “Amy”  
   - Last Name  = “Watson”  
   - Department = “Oncology”  
   - Phone      = “555-2222”  
   - Email      = “awatson@meditech.com”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

A new row appears in the grid with:  
- ID auto-generated  
- Name = “Amy Watson”  
- Department = “Oncology”  
- Phone = “555-2222”  
- Email = “awatson@meditech.com”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### NUR-TC02 — Edit Nurse

**Feature:** Edit Nurse  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in NUR-TC01.  
2. Click **Edit**.  
3. Change Department to “Pediatrics.”  
4. Change Phone to “555-3333.”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Department changes to “Pediatrics”  
- Phone changes to “555-3333”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### NUR-TC03 — Delete Nurse

**Feature:** Delete Nurse  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from NUR-TC01/NUR-TC02.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- The selected nurse disappears from the grid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### NUR-TC04 — Refresh Nurses

**Feature:** Refresh Nurses  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (even if no changes were made).
</details>

<details>
<summary>✅ Expected Result</summary>

- The grid reloads and shows the same set of nurses (no duplicates).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### NUR-TC05 — Validation: Required Fields

**Feature:** Validation: Required Fields  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave “First Name” blank; fill other fields.  
3. Click **Save**.  
4. Repeat leaving Last Name, Department, Phone, or Email blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Please fill in all fields (First Name, Last Name, Department, Phone, Email).”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### NUR-TC06 — Edit/Save Lock

**Feature:** Edit/Save Lock  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing nurse row.  
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
