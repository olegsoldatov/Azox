Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Controllers
	Public Class ImagesController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Thumbnail(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Images.Select(Function(x) New With {x.Id, x.Thumbnail, x.ContentType}).AsNoTracking.SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Thumbnail, model.ContentType)
		End Function

		Public Async Function Original(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Images.Select(Function(x) New With {x.Id, x.Original, x.ContentType}).AsNoTracking.SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Original, model.ContentType)
		End Function

		<OutputCache(CacheProfile:="Image")>
		Public Async Function Large(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Images.Select(Function(x) New With {x.Id, x.Large, x.ContentType}).AsNoTracking.SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Large, model.ContentType)
		End Function

		<OutputCache(CacheProfile:="Image")>
		Public Async Function Medium(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Images.Select(Function(x) New With {x.Id, x.Medium, x.ContentType}).AsNoTracking.SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Medium, model.ContentType)
		End Function

		<OutputCache(CacheProfile:="Image")>
		Public Async Function Small(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Images.Select(Function(x) New With {x.Id, x.Small, x.ContentType}).AsNoTracking.SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return File(model.Small, model.ContentType)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
