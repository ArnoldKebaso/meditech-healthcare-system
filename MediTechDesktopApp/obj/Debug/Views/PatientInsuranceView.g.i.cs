﻿#pragma checksum "..\..\..\Views\PatientInsuranceView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "03A7AE9417EAD0E70E1D78FD227D337122C591C2D27F45122B2BABA7C31A908E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MediTechDesktopApp.Views {
    
    
    /// <summary>
    /// PatientInsuranceView
    /// </summary>
    public partial class PatientInsuranceView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPatient;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPolicy;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNew;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefresh;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Views\PatientInsuranceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgPatientInsurance;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MediTechDesktopApp;component/views/patientinsuranceview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\PatientInsuranceView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\Views\PatientInsuranceView.xaml"
            ((MediTechDesktopApp.Views.PatientInsuranceView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbPatient = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cbPolicy = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.btnNew = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Views\PatientInsuranceView.xaml"
            this.btnNew.Click += new System.Windows.RoutedEventHandler(this.btnNew_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Views\PatientInsuranceView.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\Views\PatientInsuranceView.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\Views\PatientInsuranceView.xaml"
            this.btnRefresh.Click += new System.Windows.RoutedEventHandler(this.btnRefresh_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dgPatientInsurance = ((System.Windows.Controls.DataGrid)(target));
            
            #line 54 "..\..\..\Views\PatientInsuranceView.xaml"
            this.dgPatientInsurance.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgPatientInsurance_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

