# Treatment Form – Test Log

| Test Case ID | Feature                           | Steps                                                                                                                                                                                                         | Expected Result                                                                                                                                                                                                     | Actual Result                                                                                                             | Pass/Fail |
|--------------|-----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------|-----------|
| TR-TC01      | Load Grid                         | 1. Launch app, log in.  
2. Click “Treatments” in left nav.  
3. Observe:  
   • DataGrid lists all treatments (TreatmentId, Name, Description, Cost).                                               | • DataGrid populates with every treatment record from the database.                                                                                                                                                 |                                                                                                                           |           |
| TR-TC02      | New + Save (Insert)               | 1. Click “New.”  
2. Enter:  
   • Name = “X-Ray”  
   • Description = “Diagnostic imaging”  
   • Cost = “100.00”  
3. Click “Save.”                                                                                                                                       | • Message: “New treatment added.”  
• After “OK,” DataGrid reloads. Top row shows “X-Ray,” “Diagnostic imaging,” and $100.00.                                                                                    |                                                                                                                           |           |
| TR-TC03      | Edit (Modify Cost Only)           | 1. Select the row added in TR-TC02.  
2. Click “Edit.”  
3. Change Cost to “120.50.”  
4. Click “Save.”                                                                                                                                       | • Message: “Treatment updated.”  
• After reload, that row’s Cost changes to $120.50.                                                                                                                               |                                                                                                                           |           |
| TR-TC04      | Delete Treatment                  | 1. Select the row from TR-TC02/03.  
2. Click “Delete.”  
3. In confirmation dialog, click “Yes.”                                                                                                        | • Message: “Treatment deleted.”  
• After “OK,” that row disappears from DataGrid.                                                                                                                          |                                                                                                                           |           |
| TR-TC05      | Refresh Grid                      | 1. Click “Refresh” (no row selected).                                                                                                                                                                                                                | • DataGrid reloads, showing all current treatments with no duplicates.                                                                                                                |                                                                                                                           |           |
| TR-TC06      | Validation: Required Fields & Format | 1. Click “New.”  
2. Leave “Name” blank, fill other fields, click “Save.”  
3. Repeat leaving “Description” blank, or “Cost” blank.  
4. Enter invalid “Cost” (e.g. “abc”) and click “Save.”                                                       | • Warning: “Please fill in all required fields (Name, Description, Cost).” (or)  
• “Cost must be a valid decimal number.”  
• No treatment is inserted.                                                                                                                        |                                                                                                                           |           |
| TR-TC07      | Edit/Save Lock Behavior            | 1. Select an existing treatment row.  
2. Click “Edit.”  
3. Verify “New” and “Refresh” remain disabled while editing.  
4. Click “Save.”                                                                                                                                        | • While editing, “New” and “Refresh” are disabled; after “Save,” both become enabled again.                                                                |                                                                                                                           |           |
