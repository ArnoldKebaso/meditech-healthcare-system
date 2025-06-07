# Doctor Form – Test Log

---

### DOC-TC01 — Add Doctor

**Feature:** Add Doctor  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Doctors** in left navigation.  
3. Click **New**.  
4. Enter:  
   - First Name = “Alice”  
   - Last Name  = “Smith”  
   - Specialization = “Cardiology”  
   - Phone      = “555-4321”  
   - Email      = “asmith@hospital.com”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

A new row appears in the grid with:  
- ID (auto-generated)  
- Alice | Smith | Cardiology | 555-4321 | asmith@hospital.com
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DOC-TC02 — Edit Doctor

**Feature:** Edit Doctor  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in DOC-TC01.  
2. Click **Edit**.  
3. Change Specialization to “Interventional Cardiology.”  
4. Change Phone to “555-8765.”  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- The selected row updates:  
  - Specialization → **Interventional Cardiology**  
  - Phone → **555-8765**
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DOC-TC03 — Delete Doctor

**Feature:** Delete Doctor  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from DOC-TC01/DOC-TC02.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- The selected doctor row disappears from the grid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DOC-TC04 — Refresh Doctors

**Feature:** Refresh Doctors  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (after no data changes).
</details>

<details>
<summary>✅ Expected Result</summary>

- The grid reloads; same doctor records reappear (no duplicates).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DOC-TC05 — Validation: Required Fields

**Feature:** Validation: Required Fields  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave “First Name” blank; fill others.  
3. Click **Save**.  
4. Repeat leaving Last Name, Specialization, Phone or Email blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “Please fill in all fields: First Name, Last Name, Specialization, Phone, Email.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### DOC-TC06 — Edit/Save Lock

**Feature:** Edit/Save Lock  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing doctor row.  
2. Click **Edit**.  
3. While editing, verify **New** and **Refresh** are disabled.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- **New** and **Refresh** remain disabled during editing; after **Save**, they are enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
