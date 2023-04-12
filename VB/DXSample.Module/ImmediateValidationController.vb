Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Validation
Imports System.Collections
Imports DevExpress.Persistent.Validation

Namespace DXSample.Module

    Public Class ImmediateValidationTargetObjectsSelector
        Inherits ValidationTargetObjectSelector

        Protected Overrides Function NeedToValidateObject(ByVal objectSpace As IObjectSpace, ByVal targetObject As Object) As Boolean
            Return True
        End Function
    End Class

    Public Class ImmediateValidationController
        Inherits ViewController

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            AddHandler ObjectSpace.ObjectChanged, AddressOf ObjectSpace_ObjectChanged
            AddHandler ObjectSpace.ObjectReloaded, AddressOf ObjectSpace_ObjectReloaded
            AddHandler View.CurrentObjectChanged, AddressOf View_CurrentObjectChanged
        End Sub

        Protected Overrides Sub OnViewControlsCreated()
            MyBase.OnViewControlsCreated()
            ValidateViewObjects()
        End Sub

        Private Sub View_CurrentObjectChanged(ByVal sender As Object, ByVal e As EventArgs)
            ValidateViewObjects()
        End Sub

        Private Sub ObjectSpace_ObjectChanged(ByVal sender As Object, ByVal e As ObjectChangedEventArgs)
            ValidateViewObjects()
        End Sub

        Private Sub ObjectSpace_ObjectReloaded(ByVal sender As Object, ByVal e As ObjectManipulatingEventArgs)
            ValidateViewObjects()
        End Sub

        Private Sub ValidateViewObjects()
            If TypeOf View Is ListView Then
                If Not CType(View, ListView).CollectionSource.IsServerMode Then
                    ValidateObjects(CType(View, ListView).CollectionSource.List)
                End If
            ElseIf TypeOf View Is DetailView Then
                Dim objectsSelector As ImmediateValidationTargetObjectsSelector = New ImmediateValidationTargetObjectsSelector()
                ValidateObjects(objectsSelector.GetObjectsToValidate(View.ObjectSpace, View.CurrentObject))
            End If
        End Sub

        Private Sub ValidateObjects(ByVal targets As IEnumerable)
            If targets Is Nothing Then Return
            Dim resultsHighlightController As ResultsHighlightController = Frame.GetController(Of ResultsHighlightController)()
            If resultsHighlightController IsNot Nothing Then
                Dim result As RuleSetValidationResult = Validator.RuleSet.ValidateAllTargets(ObjectSpace, targets, DefaultContexts.Save)
                If result.ValidationOutcome = ValidationOutcome.Error OrElse result.ValidationOutcome = ValidationOutcome.Warning OrElse result.ValidationOutcome = ValidationOutcome.Information Then
                    resultsHighlightController.HighlightResults(result)
                Else
                    resultsHighlightController.ClearHighlighting()
                End If
            End If
        End Sub

        Protected Overrides Sub OnDeactivated()
            MyBase.OnDeactivated()
            RemoveHandler ObjectSpace.ObjectChanged, AddressOf ObjectSpace_ObjectChanged
            RemoveHandler ObjectSpace.ObjectReloaded, AddressOf ObjectSpace_ObjectReloaded
            RemoveHandler View.CurrentObjectChanged, AddressOf View_CurrentObjectChanged
        End Sub
    End Class
End Namespace
