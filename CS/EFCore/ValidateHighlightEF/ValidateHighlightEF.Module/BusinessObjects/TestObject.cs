using System;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.BaseImpl.EF;

namespace ValidateHighlight.Module {
    [DefaultClassOptions]
    public class TestObject : BaseObject {
        [RuleRequiredField(ResultType = ValidationResultType.Error)]
        public string Error { get; set; }
        [RuleRequiredField(ResultType = ValidationResultType.Warning)]
        public string Warning { get; set; }
        [RuleRequiredField(ResultType = ValidationResultType.Information)]
        public string Information { get; set; }
    }
}
