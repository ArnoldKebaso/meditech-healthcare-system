# Insurance Policy Form – Test Log

---

### IPOL-TC01 — Load Provider Dropdown & Grid

**Feature:** Load Provider Dropdown & Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app, log in.  
2. Click **InsPolicies** in left nav.  
3. Observe:  
   - `cbProvider` dropdown shows all providers.  
   - DataGrid lists existing policies with columns: ID, ProviderName, PolicyName, CoverageDetails, StartDate, EndDate.
</details>

<details>
<summary>✅ Expected Result</summary>

- Dropdown contains provider names (if any exist).  
- DataGrid displays all policies with correct fields.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IPOL-TC02 — New + Save (Insert)

**Feature:** New + Save (Insert)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Select a provider from `cbProvider` (e.g. “HealthGuard”).  
3. Enter:  
   - PolicyName = “Gold Plan”  
   - CoverageDetails = “Up to $1M hospitalization”  
   - StartDate = today’s date  
   - EndDate = one year from today  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “New policy added.”  
- After “OK,” DataGrid reloads.  
- Top row shows ProviderName / “Gold Plan” / “Up to $1M hospitalization” / correct StartDate / EndDate.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IPOL-TC03 — Edit (Modify Coverage Only)

**Feature:** Edit (Modify Coverage Only)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row added in IPOL-TC02.  
2. Click **Edit**.  
3. Change CoverageDetails to “Up to $2M hospitalization.”  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Policy updated.”  
- DataGrid reloads, and that row’s CoverageDetails updates accordingly.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IPOL-TC04 — Delete Policy

**Feature:** Delete Policy  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select the row from IPOL-TC02/03.  
2. Click **Delete**.  
3. In confirmation dialog, click **Yes**.
</details>

<details>
<summary>✅ Expected Result</summary>

- Message: “Policy deleted.”  
- After “OK,” that row disappears from DataGrid.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IPOL-TC05 — Refresh Grid

**Feature:** Refresh Grid  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **Refresh** (no row selected).
</details>

<details>
<summary>✅ Expected Result</summary>

- DataGrid reloads, showing all current policies.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IPOL-TC06 — Validation (Required Fields)

**Feature:** Validation (Required Fields)  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Click **New**.  
2. Leave **Provider** blank; fill other fields.  
3. Click **Save**.  
4. Repeat leaving **PolicyName**, **CoverageDetails**, **StartDate**, or **EndDate** blank.
</details>

<details>
<summary>✅ Expected Result</summary>

- Warning: “All fields (Provider, Policy Name, Coverage, Start Date, End Date) are required.”  
- No policy is inserted.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### IPOL-TC07 — Edit/Save Lock Behavior

**Feature:** Edit/Save Lock Behavior  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Select an existing policy row.  
2. Click **Edit**.  
3. Verify **New** and **Refresh** are disabled while editing.  
4. Click **Save**.
</details>

<details>
<summary>✅ Expected Result</summary>

- **New** and **Refresh** remain disabled while editing.  
- After **Save**, they become enabled again.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
