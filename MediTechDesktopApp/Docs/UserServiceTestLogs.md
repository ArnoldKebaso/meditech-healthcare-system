# UserService – Test Log

---

### US-TC01 — ValidateUser – Correct Credentials

**Feature:** ValidateUser – Correct Credentials  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Ensure user “testuser” with password “Password123” exists in test DB or mock.  
2. Invoke `ValidateUser("testuser", "Password123")`.
</details>

<details>
<summary>✅ Expected Result</summary>

- Returns `true`.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### US-TC02 — ValidateUser – Wrong Password

**Feature:** ValidateUser – Wrong Password  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Ensure user “testuser” exists.  
2. Invoke `ValidateUser("testuser", "WrongPassword")`.
</details>

<details>
<summary>✅ Expected Result</summary>

- Returns `false`.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### US-TC03 — ValidateUser – Non-existent User

**Feature:** ValidateUser – Non-existent User  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Invoke `ValidateUser("noSuchUser", "Password123")` without creating that user.
</details>

<details>
<summary>✅ Expected Result</summary>

- Returns `false`.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### US-TC04 — Create New User

**Feature:** Create New User  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Invoke `CreateUser(new User { Username = "newuser", Password = "Pwd!23" })`.  
2. Invoke `ValidateUser("newuser", "Pwd!23")`.
</details>

<details>
<summary>✅ Expected Result</summary>

- Returns `true`; new user can log in.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>

---

### US-TC05 — Error Handling – DB Error

**Feature:** Error Handling – DB Error  
**Status:** ☐

<details>
<summary>🔍 Steps</summary>

1. Configure mock or test DB helper to throw on any call.  
2. Invoke `ValidateUser("testuser", "Password123")`.
</details>

<details>
<summary>✅ Expected Result</summary>

- A `DatabaseUnavailableException` (or similar) is thrown.
</details>

<details>
<summary>⚠️ Actual Result</summary>

*Fill after test run…*  
</details>
