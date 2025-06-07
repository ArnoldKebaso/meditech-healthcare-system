# Invoice Form – Test Log

---

### INV-TC01 — Load Patient Dropdown & Grid

**Feature:** Load Patient Dropdown & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Invoices** in left nav.  
3. Observe:  
   - `cbPatient` dropdown is populated.  
   - DataGrid lists existing invoices with columns: ID, PatientFullName, InvoiceDate, TotalAmount.
</details>

<details>
<summary>✅ Expected Result</summary>

- Dropdown contains at least one patient.  
- DataGrid shows all invoices with correct columns.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### INV-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select a patient (e.g. “John Doe”) from `cbPatient`.  
3. Pick today’s date in `dpInvoiceDate`.  
4. Enter “150.00” in `txtTotalAmount`.  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New invoice added.”  
- After “OK,” DataGrid reloads. Top row shows correct Patient, Date, and Total ($150.00).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### INV-TC03 — Edit (Modify Total Only)

**Feature:** Edit (Modify Total Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in INV-TC02.  
2. Click **Edit**.  
3. Change `txtTotalAmount` to “175.50.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Invoice updated.”  
- DataGrid reloads; that row’s Total changes to $175.50.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### INV-TC04 — Delete Invoice

**Feature:** Delete Invoice  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from INV-TC02/03.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Invoice deleted.”  
- After “OK,” that row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### INV-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current invoices.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### INV-TC06 — Validation: Required Fields & Format

**Feature:** Validation: Required Fields & Format  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave **Patient** blank; fill other fields; click **Save**.  
3. Repeat with blank **Invoice Date** or blank **Total Amount**.  
4. Enter invalid string (e.g. “abc”) into `txtTotalAmount`; click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “All fields (Patient, Date, Total Amount) are required.”  
- For invalid total: “Total Amount must be a valid decimal number.”  
- No invoice is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### INV-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing invoice row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** are disabled while editing.  
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
