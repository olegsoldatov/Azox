Imports System.Data.Entity
Imports System.Threading.Tasks

Namespace Controllers
	Public Class ImagesController
		Inherits Controller

		Public Sub New()
			_ImageManager = New ImageManager
		End Sub

		Public ReadOnly Property ImageManager As ImageManager

		Public Async Function Thumbnail(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ImageManager.Entities.Where(Function(x) x.Id = id).Select(Function(x) New With {x.Thumbnail, x.ContentType}).SingleOrDefaultAsync
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Thumbnail, model.ContentType)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Original(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ImageManager.Entities.Where(Function(x) x.Id = id).Select(Function(x) New With {x.Original, x.ContentType}).SingleOrDefaultAsync
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Original, model.ContentType)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Large(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ImageManager.Entities.Where(Function(x) x.Id = id).Select(Function(x) New With {x.Large, x.ContentType}).SingleOrDefaultAsync
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Large, model.ContentType)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Medium(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ImageManager.Entities.Where(Function(x) x.Id = id).Select(Function(x) New With {x.Medium, x.ContentType}).SingleOrDefaultAsync
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Medium, model.ContentType)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Small(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ImageManager.Entities.Where(Function(x) x.Id = id).Select(Function(x) New With {x.Small, x.ContentType}).SingleOrDefaultAsync
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Small, model.ContentType)
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				If _ImageManager IsNot Nothing Then
					_ImageManager.Dispose()
					_ImageManager = Nothing
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
