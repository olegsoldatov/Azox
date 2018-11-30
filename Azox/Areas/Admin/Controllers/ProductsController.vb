Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Web.Configuration
Imports Soldata.Imaging

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class ProductsController
		Inherits Controller

		Public ReadOnly Property ImageManager As ImageManager
		Public ReadOnly Property ProductManager As ProductManager

		Public Sub New()
			_ImageManager = New ImageManager
			_ProductManager = New ProductManager
		End Sub

		Public Async Function Index(searchString As String, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
			Dim entities = ProductManager.Entities

			' Поиск.
			If Not String.IsNullOrEmpty(searchString) Then
				entities = entities.Where(Function(x) x.Title.Contains(searchString))
			End If

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

			Return View(Await entities.OrderBy(Function(x) x.Id).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Public Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Create(model As Product) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await ProductManager.CreateAsync(model)
				TempData("Message") = "Продукт добавлен."
				Return RedirectToAction("Edit", New With {model.Id})
			End If
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ProductManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Edit(model As Product) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await ProductManager.UpdateAsync(model)
				TempData("Message") = "Продукт изменен."
				Return RedirectToAction("Edit", New With {model.Id})
			End If
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await ProductManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<ActionName("Delete"), HttpPost, ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Await ProductManager.DeleteAsync(Await ProductManager.FindByIdAsync(id))
			TempData("Message") = "Продукт удален."
			Return RedirectToAction("Index")
		End Function

		Public Function ChangeImage(id As Guid) As ActionResult
			ViewBag.Id = id
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function ChangeImage(model As ChangeImageViewModel) As Task(Of ActionResult)
			If ModelState.IsValid Then
				If Not IsNothing(model.ImageFile) AndAlso (model.ImageFile.ContentType.Contains("image") And model.ImageFile.ContentLength > 0) Then
					Dim entity = Await ProductManager.FindByIdAsync(model.Id)

					If Not IsNothing(entity.ImageId) Then
						Await ImageManager.DeleteAsync(Await ImageManager.FindByIdAsync(entity.ImageId))
					End If

					Dim image As New Image With {
						.ContentType = model.ImageFile.ContentType,
						.Original = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType),
						.Thumbnail = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill),
						.Small = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 80, 80, StretchMode.UniformToFill),
						.Medium = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 240, 160, StretchMode.UniformToFill),
						.Large = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 848, 318, StretchMode.UniformToFill)
					}

					Await ImageManager.CreateAsync(image)

					entity.ImageId = image.Id

					Await ProductManager.UpdateAsync(entity)

					TempData("Message") = "Изображение изменено."
					Return RedirectToAction("edit", New With {model.Id})
				End If
			End If
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function DeleteImage(id As Guid) As Task(Of ActionResult)
			Dim entity = Await ProductManager.FindByIdAsync(id)

			If Not IsNothing(entity.ImageId) Then
				Await ImageManager.DeleteAsync(Await ImageManager.FindByIdAsync(entity.ImageId))
			End If

			entity.ImageId = Nothing

			Await ProductManager.UpdateAsync(entity)

			TempData("Message") = "Изображение удалено."
			Return RedirectToAction("edit", New With {id})
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If _ProductManager IsNot Nothing Then
					_ProductManager.Dispose()
					_ProductManager = Nothing
				End If
				If _ImageManager IsNot Nothing Then
					_ImageManager.Dispose()
					_ImageManager = Nothing
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace