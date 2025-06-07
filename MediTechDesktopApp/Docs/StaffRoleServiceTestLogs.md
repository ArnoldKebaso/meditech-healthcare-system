# StaffRoleService – Test Log

---

### SR-TC01 — Get All Roles

**Feature:** Get All Roles  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Launch app or test runner.  
2. Invoke `GetAllRoles()`.
</details>

<details>
<summary>✅ Expected Result</summary>

- A non-null list of `StaffRole` objects is returned.  
- List count ≥ 1.  
- Known role (e.g. “Receptionist”) exists in the list.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### SR-TC02 — Add New Role

**Feature:** Add New Role  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Invoke `AddRole(new StaffRole { Name = "TestRole" })`.  
2. Invoke `GetAllRoles()` again.
</details>

<details>
<summary>✅ Expected Result</summary>

- The returned list contains a role with `Name = "TestRole"`.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### SR-TC03 — Update Existing Role

**Feature:** Update Existing Role  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Identify existing role (e.g. “TestRole”).  
2. Invoke `UpdateRole(id, new StaffRole { Name = "UpdatedRole" })`.  
3. Invoke `GetAllRoles()`.
</details>

<details>
<summary>✅ Expected Result</summary>

- List contains a role with `Name = "UpdatedRole"`.  
- No role named “TestRole” remains.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### SR-TC04 — Delete Role

**Feature:** Delete Role  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Identify role to delete (e.g. “UpdatedRole”).  
2. Invoke `DeleteRole(id)`.  
3. Invoke `GetAllRoles()`.
</details>

<details>
<summary>✅ Expected Result</summary>

- List no longer contains a role with `Name = "UpdatedRole"`.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### SR-TC05 — Error Handling – DB Error

**Feature:** Error Handling – DB Error  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Configure mock or test DB helper to throw on any call.  
2. Invoke `GetAllRoles()`.
</details>

<details>
<summary>✅ Expected Result</summary>

- A `DatabaseUnavailableException` (or similar) is thrown.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
