# Insurance Provider Form – Test Log

---

### IP-TC01 — Load Grid

**Feature:** Load Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **InsProviders** in left nav.  
3. Observe: DataGrid lists existing providers (ID, Name, Phone, Email, Address).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid shows all providers from the database with correct fields.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IP-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Enter:  
   - Name = “HealthGuard”  
   - Phone = “555-1234”  
   - Email = “contact@healthguard.com”  
   - Address = “123 Wellness Blvd.”  
3. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New provider added.”  
- After “OK,” DataGrid reloads. Top row shows “HealthGuard” / “555-1234” / “contact@healthguard.com” / “123 Wellness Blvd.”
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IP-TC03 — Edit (Modify Address Only)

**Feature:** Edit (Modify Address Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in IP-TC02.  
2. Click **Edit**.  
3. Change Address to “456 Healing Way.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Provider updated.”  
- DataGrid reloads, and that row’s Address changes accordingly.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IP-TC04 — Delete Provider

**Feature:** Delete Provider  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from IP-TC02/03.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Provider deleted.”  
- After “OK,” the row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IP-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current providers.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IP-TC06 — Validation (Required Fields)

**Feature:** Validation (Required Fields)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave **Name** blank; fill other fields.  
3. Click **Save**.  
4. Repeat leaving **Phone**, **Email**, or **Address** blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “All fields (Name, Phone, Email, Address) are required.”  
- No provider is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IP-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing provider row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** are disabled while editing.  
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
