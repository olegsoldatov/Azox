Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class CustomerManager
	Inherits EntityManager(Of Customer)

	Public Sub New(context As DbContext)
		MyBase.New(context)
	End Sub

	Public ReadOnly Property Customers As IQueryable(Of Customer)
		Get
			Return Context.Set(Of Customer)
		End Get
	End Property

	Public Overrides Function CreateAsync(entity As Customer) As Task(Of ManagerResult)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.LastUpdateDate = Now
		Return MyBase.CreateAsync(entity)
	End Function

	Public Overrides Function UpdateAsync(entity As Customer) As Task(Of ManagerResult)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.LastUpdateDate = Now
		Return MyBase.UpdateAsync(entity)
	End Function
End Class
