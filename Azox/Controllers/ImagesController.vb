Imports System.Data.Entity
Imports System.Threading.Tasks

Namespace Controllers
	Public Class ImagesController
		Inherits DbController

		Public Async Function Thumbnail(id As Guid) As Task(Of ActionResult)
			Dim image = Await Db.Images.Select(Function(x) New With {x.Id, x.ContentType, .Content = x.Thumbnail}).SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(image) Then Return HttpNotFound("Изображение не найдено.")
			Return New ImageResult(image.ContentType, image.Content)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Original(id As Guid) As Task(Of ActionResult)
			Dim image = Await Db.Images.Select(Function(x) New With {x.Id, x.ContentType, .Content = x.Original}).SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(image) Then Return HttpNotFound("Изображение не найдено.")
			Return New ImageResult(image.ContentType, image.Content)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Large(id As Guid) As Task(Of ActionResult)
			Dim image = Await Db.Images.Select(Function(x) New With {x.Id, x.ContentType, .Content = x.Large}).SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(image) Then Return HttpNotFound("Изображение не найдено.")
			Return New ImageResult(image.ContentType, image.Content)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Medium(id As Guid) As Task(Of ActionResult)
			Dim image = Await Db.Images.Select(Function(x) New With {x.Id, x.ContentType, .Content = x.Medium}).SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(image) Then Return HttpNotFound("Изображение не найдено.")
			Return New ImageResult(image.ContentType, image.Content)
		End Function

		<OutputCache(Duration:=30, VaryByParam:="none")>
		Public Async Function Small(id As Guid) As Task(Of ActionResult)
			Dim image = Await Db.Images.Select(Function(x) New With {x.Id, x.ContentType, .Content = x.Small}).SingleOrDefaultAsync(Function(x) x.Id = id)
			If IsNothing(image) Then Return HttpNotFound("Изображение не найдено.")
			Return New ImageResult(image.ContentType, image.Content)
		End Function

		Private Class ImageResult
			Inherits ActionResult

			Private _ContentType As String
			Private _Content As Byte()

			Public Sub New(contentType As String, content As Byte())
				_ContentType = contentType
				_Content = content
			End Sub

			Public Overrides Sub ExecuteResult(context As ControllerContext)
				If IsNothing(_Content) Then
					context.HttpContext.Response.StatusCode = 404
				Else
					context.HttpContext.Response.ContentType = _ContentType
					context.HttpContext.Response.BinaryWrite(_Content)
				End If
			End Sub
		End Class
	End Class
End Namespace
