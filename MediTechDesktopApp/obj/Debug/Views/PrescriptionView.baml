   M S B A M L   `   `   `  ����  MediTechDesktopApp/    )MediTechDesktopApp.Views.PrescriptionView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5	      � $��10q�6     � ColumnDefinitions  5
   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Record:��6   $	��100��6*   $��Center=�66   �� 5      -   5         ��Name$ 	cbRecords��5      $	��200��6.   {�.  6:   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Medication:��6   $	��100��6.   $��Center=�6:   �� 5      -   5         ��Name$ txtMedicationName��5      $	��200��65   {�.  6A   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Dosage:��6   $	��100��6*   $��Center=�66   �� 5       -   5        $ 	txtDosage��5       $	��200��6-   {�.  69   5!      �� 5#      $ 
Horizontal=�6   $	��0,5q�62   .+�5$      �� $��
Frequency:��6   $	��100��6-   $��Center=�69   �� 5%      -   5        $ cbFrequency��5%      $	��200��60   {�.  6<   5&      �� 5(      $ 
Horizontal=�6   $	��0,5q�62   .+�5)      �� $��Start Date:��6   $	��100��6.   $��Center=�6:   (  "System.Windows.Controls.DatePicker  5*      -   5          Name$ dpStartDate��5*      $	��200��62   {�.  6>   5+      �� 5-      $ 
Horizontal=�6   $	��0,5q�62   .+�5.      �� $��	End Date:��6   $	��100��6,   $��Center=�68     5/      -   5        $ 	dpEndDate��5/      $	��200��60   {�.  6<   50      �� 52      $ Vertical=�6   $	��0,5q�60   .+�53      �� $��Instructions:��6   $	��100��60   $��Center=�6<   �� 54      -   5        $ txtInstructions��54      $	��300��63   $��60��6?    �� TextWrapping$
 Wrap=�55       �� AcceptsReturn . {�.  6C   56      �� 59      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5:      �� -	   5         ��Name$ btnNew��5:      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5;      -
   5        $ btnSave��5;      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5<      -   5        $ btnEdit��5<      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5=      -   5        $ 	btnDelete��5=      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5>      -   5        $ 
btnRefresh��5>      $��Refresh��6-   $��60��6?   6X   5?      5@      &   System.Windows.Controls.DataGrid  5C   
   -   5          Name$ dgPrescriptions��5C      $��1��5D      	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    CanUserAddRows .     CanUserDeleteRows .     SelectionUnit$ FullRow=�5I         SelectionMode$ Single=�5J      $��0,0,0,10q�5L         Columns 5M      0  *System.Windows.Controls.DataGridTextColumn  5N      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *PrescriptionId+6[     5O      $ Record��6%   $ 2* 6]    68   �� *DisplayRecord+6]     5P      $ Med Name��6%   $ 2* 6\    68   �� *MedicationName+6\     5Q      $ Dosage��6%   $ * 6\    68   �� *Dosage+6\     5R      $ 	Frequency��6%   $ * 6\    68   �� *FrequencyName+6\     5S      $ 
Start Date��6%   $ * 6v    69   �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��6v     5T      $ End Date��6%   $ * 6u    68   �� *	EndDate+$ {0:yyyy-MM-dd}��6u     5U      $ Instructions��6%   $ 3* 6\    6;   �� *Instructions+6\   5W      5X      5Y      