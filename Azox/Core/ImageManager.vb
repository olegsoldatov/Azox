Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class ImageManager
	Inherits EntityManager(Of Image)

	Public Sub New()
		MyClass.New(New ApplicationDbContext)
	End Sub

	Public Sub New(context As DbContext)
		MyBase.New(context)
	End Sub

	Public ReadOnly Property Images As IQueryable(Of Image)
		Get
			Return Context.Set(Of Image)
		End Get
	End Property

	Public Async Function DeleteRangeAsync(images As IEnumerable(Of Image)) As Task
		Context.Set(Of Image).RemoveRange(images)
		Await Context.SaveChangesAsync
	End Function
End Class
