   M S B A M L   `   `   `  ����  MediTechDesktopApp2    ,MediTechDesktopApp.Views.InsurancePolicyView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5      �� 5      $��2* �6   �� 5      $��3* �6   .+�5      �� $��0��6   $��0,0,10,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�6)   .+�5      �� $��	Provider:��6   $	��100��6    $��Center=�6,   �� 5      -   5         ��Name$ cbProviders��5      $	��200��6$   5      5      �� 5      $ 
Horizontal=�6   $	��0,5q�6)   .+�5      �� $��Policy Num:��6   $	��100��6"   $��Center=�6.   �� 5      -   5         ��Name$ txtPolicyNumber��5      $	��200��6'   {�.  63   5      �� 5"      $ 
Horizontal=�6   $	��0,5q�6)   .+�5#      �� $��	Coverage:��6   $	��100��6    $	��Top=�6,   �� 5$      -   5        $ txtCoverage��5$      $	��200��6#   $��60��6/    �� TextWrapping$
 Wrap=�6;    �� AcceptsReturn . {�.  6d   5%      �� 5(      $ 
Horizontal=�6   $	��0,5q�6)   .+�5)      �� $��Start Date:��6   $	��100��6"   $��Center=�6.   (  "System.Windows.Controls.DatePicker  5*      -   5          Name$ dpStartDate��5*      $	��200��6&   {�.  62   5+      �� 5.      $ 
Horizontal=�6   $	��0,5q�6)   .+�5/      �� $��	End Date:��6   $	��100��6    $��Center=�6,     50      -   5        $ 	dpEndDate��50      $	��200��6$   {�.  60   51      �� 54      $ 
Horizontal=�6   $
��0,15q�6)   $
��Left=�67   .+�55      �� -   5         ��Name$ btnNew��55      $	��New��6!   $��60��63   $��0,0,5,0q�6>   6O   �� 56      -   5        $ btnSave��56      $
��Save��6!   $��60��63   $��0,0,5,0q�6>   {�.  6h   �� 57      -	   5        $ btnEdit��57      $
��Edit��6!   $��60��63   $��0,0,5,0q�6>   {�.  6h   �� 58      -
   5        $ 	btnDelete��58      $��Delete��6!   $��60��63   $��0,0,5,0q�6>   {�.  6g   �� 59      -   5        $ 
btnRefresh��59      $��Refresh��6!   $��60��63   6J   5:      5;      &   System.Windows.Controls.DataGrid  5>      -   5          Name$ 
dgPolicies��5>      $��1��5?      	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    CanUserAddRows .     CanUserDeleteRows .     SelectionUnit$ FullRow=�5D         SelectionMode$ Single=�5E      $��0,0,0,10q�5G         Columns 5H      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6.   �� *
PolicyId+6R     5J      $ Provider��6   $ 2* 6R    6.   �� *ProviderName+6R     5K      $ 
Policy Num��6   $ 2* 6R    6.   �� *PolicyNumber+6R     5L      $ Coverage��6   $ 3* 6R    6.   �� *CoverageDetails+6R     5M      $ 
Start Date��6   $ * 6k    6.   �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��6k     5N      $ End Date��6   $ * 6k    6.   �� *	EndDate+$ {0:yyyy-MM-dd}��6k   5P      5Q      5R      