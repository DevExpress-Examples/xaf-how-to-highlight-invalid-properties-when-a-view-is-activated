using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Validation;
using System.Collections;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;

namespace DXSample.Module {
    public class ImmediateValidationTargetObjectsSelector : ValidationTargetObjectSelector {
        protected override bool NeedToValidateObject(IObjectSpace objectSpace, object targetObject) {
            return true;
        }
    }
    public class ImmediateValidationController : ViewController {
        protected override void OnActivated() {
            base.OnActivated();
            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            ObjectSpace.ObjectReloaded += ObjectSpace_ObjectReloaded;
            View.CurrentObjectChanged += View_CurrentObjectChanged;            
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            ValidateViewObjects();
        }
        void View_CurrentObjectChanged(object sender, EventArgs e) {
            ValidateViewObjects();
        }
        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e) {
            ValidateViewObjects();
        }
        private void ObjectSpace_ObjectReloaded(object sender, ObjectManipulatingEventArgs e) {
            ValidateViewObjects();
        }
        private void ValidateViewObjects() {
            if (View is ListView) {
                if (!((ListView)View).CollectionSource.IsServerMode) {
                    ValidateObjects(((ListView)View).CollectionSource.List);
                }
            } else if (View is DetailView) {
                ImmediateValidationTargetObjectsSelector objectsSelector = new ImmediateValidationTargetObjectsSelector();
                ValidateObjects(objectsSelector.GetObjectsToValidate(View.ObjectSpace, View.CurrentObject));
            }
        }
        private void ValidateObjects(IEnumerable targets) {
            if (targets == null) return;
            ResultsHighlightController resultsHighlightController = Frame.GetController<ResultsHighlightController>();
            if (resultsHighlightController != null) {
                RuleSetValidationResult result = Validator.RuleSet.ValidateAllTargets(ObjectSpace, targets, DefaultContexts.Save);
                if (result.ValidationOutcome == ValidationOutcome.Error || result.ValidationOutcome == ValidationOutcome.Warning || result.ValidationOutcome == ValidationOutcome.Information) {
                    resultsHighlightController.HighlightResults(result);
                } else {
                    resultsHighlightController.ClearHighlighting();
                }
            }
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
            ObjectSpace.ObjectReloaded -= ObjectSpace_ObjectReloaded;
            View.CurrentObjectChanged -= View_CurrentObjectChanged;
        }
    }
}
