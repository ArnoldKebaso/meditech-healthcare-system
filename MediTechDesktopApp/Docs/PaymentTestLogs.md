# Payment Form – Test Log

---

### PAY-TC01 — Load Invoice Dropdown & Grid

**Feature:** Load Invoice Dropdown & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **Payments** in left nav.  
3. Observe:  
   - `cbInvoice` shows all invoices (e.g. by InvoiceDate or DisplayMember).  
   - DataGrid lists payments with columns: ID, PatientFullName, InvoiceTotal, PaymentDate, AmountPaid.
</details>

<details>
<summary>✅ Expected Result</summary>

- Dropdown is populated with invoices.  
- DataGrid shows all existing payments with correct joined fields.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAY-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select an invoice from `cbInvoice` (e.g. the topmost one).  
3. Pick today’s date in `dpPaymentDate`.  
4. Enter “50.00” in `txtAmountPaid`.  
5. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New payment added.”  
- After “OK,” DataGrid reloads. Top row shows correct Patient, InvoiceTotal, PaymentDate, AmountPaid ($50.00).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAY-TC03 — Edit (Modify Amount Only)

**Feature:** Edit (Modify Amount Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in PAY-TC02.  
2. Click **Edit**.  
3. Change `txtAmountPaid` to “75.25.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Payment updated.”  
- DataGrid reloads; that row’s AmountPaid changes to $75.25.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAY-TC04 — Delete Payment

**Feature:** Delete Payment  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from PAY-TC02/03.  
2. Click **Delete**.  
3. Confirm **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Payment deleted.”  
- After “OK,” that row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAY-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current payments (no duplicates).
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAY-TC06 — Validation: Required Fields & Format

**Feature:** Validation: Required Fields & Format  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave **Invoice** blank; fill other fields; click **Save**.  
3. Repeat leaving **Payment Date** blank or **Amount Paid** blank.  
4. Enter invalid string (e.g. “abc”) into `txtAmountPaid` and click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “All fields (Invoice, Payment Date, Amount Paid) are required.”  
- If invalid: “Amount Paid must be a valid decimal number.”  
- No record is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### PAY-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing payment row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** remain disabled while editing.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- While editing, **New** and **Refresh** remain disabled; after **Save**, both become enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
