   M S B A M L   `   `   `  ����  MediTechDesktopApp-    'MediTechDesktopApp.Views.AssignmentView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5	      � $��10q�6     � ColumnDefinitions  5
   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Patient:��6   $	��100��6+   $��Center=�67   �� 5      -   5         ��Name$ 	cbPatient��5      $	��200��6.   {�.   �� DisplayMemberPath$ FullName��5       �� SelectedValuePath$ 	PatientId��68   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��
Treatment:��6   $	��100��6-   $��Center=�69   �� 5      -   5        $ cbTreatment��5      $	��200��60   {�.  $
 Name��5      $ TreatmentId��64   5      �� 5"      $ 
Horizontal=�6   $	��0,5q�62   .+�5#      �� $��Doctor:��6   $	��100��6*   $��Center=�66   �� 5$      -   5        $ cbDoctor��5$      $	��200��6-   {�.  $ FullName��5%      $ DoctorId��68   5&      �� 5)      $ 
Horizontal=�6   $	��0,5q�62   .+�5*      �� $��Nurse:��6   $	��100��6)   $��Center=�65   �� 5+      -   5        $ cbNurse��5+      $	��200��6,   {�.  $ FullName��5,      $ NurseId��68   5-      �� 50      $ 
Horizontal=�6   $	��0,5q�62   .+�51      �� $��Date:��6   $	��100��6(   $��Center=�64   (  "System.Windows.Controls.DatePicker  52      -   5          Name$ dpDate��52      $	��200��6-   {�.  69   53      �� 56      $ Vertical=�6   $	��0,5q�60   .+�57      �� $��Notes:��6   $	��100��6)   $��Center=�65   �� 58      -   5         ��Name$ txtNotes��58      $	��300��6,   $��80��68    �� TextWrapping$
 Wrap=�59       �� AcceptsReturn . {�.  6C   5:      �� 5=      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5>      �� -   5        	 ��Name$	 btnNew��5>      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5?      -	   5        $	 btnSave��5?      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5@      -
   5        $	 btnEdit��5@      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5A      -   5        $	 	btnDelete��5A      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5B      -   5        $	 
btnRefresh��5B      $��Refresh��6-   $��60��6?   6X   5C      5D      &   System.Windows.Controls.DataGrid  5G   
   -   5        
  Name$
 dgAssignments��5G      $��1��5H         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .     SelectionMode$ Single=�5M      $��0,0,0,10q�5O         Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ Patient��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6_   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *PatientFullName+6_     5R      $ 	Treatment��6%   $ 2* 6_    68   �� *TreatmentName+6_     5S      $
 Date��6%   $ * 6z    68   �� *AssignmentDate+ �� StringFormat$ {0:yyyy-MM-dd}��6z     5T      $ Doctor��6%   $ 2* 6^    68   �� *DoctorFullName+6^     5U      $ Nurse��6%   $ 2* 6^    68   �� *NurseFullName+6^     5V      $ Notes��6%   $ 3* 6^    68   �� *Notes+6^   5X      5Y      5Z      