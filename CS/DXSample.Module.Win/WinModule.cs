using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace DXSample.Module.Win {
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class DXSampleWindowsFormsModule : ModuleBase {
        public DXSampleWindowsFormsModule() {
            InitializeComponent();
        }
    }
}
