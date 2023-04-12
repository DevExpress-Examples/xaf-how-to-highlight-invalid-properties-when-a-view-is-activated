using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;

namespace ValidateHighlight.Module {
    [DefaultClassOptions]
    public class TestObject : BaseObject {
        public TestObject(Session session) : base(session) { }
        private string _Error;
        [RuleRequiredField(ResultType = ValidationResultType.Error)]
        public string Error {
            get { return _Error; }
            set { SetPropertyValue(nameof(Error), ref _Error, value); }
        }
        private string _Warning;
        [RuleRequiredField(ResultType = ValidationResultType.Warning)]
        public string Warning {
            get { return _Warning; }
            set { SetPropertyValue(nameof(Warning), ref _Warning, value); }
        }
        private string _Information;
        [RuleRequiredField(ResultType = ValidationResultType.Information)]
        public string Information {
            get { return _Information; }
            set { SetPropertyValue(nameof(Information), ref _Information, value); }
        }
    }
}
