Imports System

Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.Editors

Namespace DXSample.Module
	<DefaultClassOptions>
	Public Class TestObject
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _Error As String
		<RuleRequiredField(ResultType := ValidationResultType.Error)>
		Public Property [Error]() As String
			Get
				Return _Error
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Error", _Error, value)
			End Set
		End Property
		Private _Warning As String
		<RuleRequiredField(ResultType := ValidationResultType.Warning)>
		Public Property Warning() As String
			Get
				Return _Warning
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Warning", _Warning, value)
			End Set
		End Property
		Private _Information As String
		<RuleRequiredField(ResultType := ValidationResultType.Information)>
		Public Property Information() As String
			Get
				Return _Information
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Information", _Information, value)
			End Set
		End Property
	End Class
End Namespace
