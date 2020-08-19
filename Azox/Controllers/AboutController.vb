Imports System.Data.Entity
Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

		Private ReadOnly Db As New ApplicationDbContext

		<HttpGet>
		<OutputCache(Duration:=1200, VaryByParam:="none")>
		Public Async Function Index() As Task(Of ActionResult)
			Dim article = Await Db.Articles.SingleOrDefaultAsync(Function(x) x.Slug = "/about")
			If IsNothing(article) Then
				Return HttpNotFound()
			End If
			Return View("Article", article)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				Db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace