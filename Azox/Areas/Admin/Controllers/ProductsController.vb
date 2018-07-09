Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Imaging

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class ProductsController
		Inherits Controller

		Private ReadOnly Property ProductManager As New ProductManager

		Async Function Index(searchString As String, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
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

		Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Async Function Create(model As Product) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await ProductManager.CreateAsync(model)
				TempData("Message") = "Продукт добавлен."
				Return RedirectToAction("Edit", New With {model.Id})
			End If
			Return View(model)
		End Function

		Async Function Edit(id As Guid?) As Task(Of ActionResult)
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
		Async Function Edit(model As Product) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await ProductManager.UpdateAsync(model)
				TempData("Message") = "Продукт изменен."
				Return RedirectToAction("Edit", New With {model.Id})
			End If
			Return View(model)
		End Function

		Async Function Delete(id As Guid?) As Task(Of ActionResult)
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
		Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Await ProductManager.DeleteAsync(Await ProductManager.FindByIdAsync(id))
			'Dim entity = Await Db.Products.FindAsync(id)
			'If Not IsNothing(entity.ImageId) Then
			'	Db.Images.Remove(Await Db.Images.FindAsync(entity.ImageId))
			'End If
			'Db.Products.Remove(entity)
			'Await Db.SaveChangesAsync
			TempData("Message") = "Продукт удален."
			Return RedirectToAction("Index")
		End Function

		Function ChangeImage(id As Guid) As ActionResult
			ViewBag.Id = id
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Async Function ChangeImage(model As ChangeImageViewModel) As Task(Of ActionResult)
			If Not IsNothing(model.ImageFile) AndAlso model.ImageFile.ContentType.Contains("image") Then
				'Dim entity = Await Db.Products.FindAsync(model.Id)

				'If Not IsNothing(entity.ImageId) Then
				'	Db.Images.Remove(Await Db.Images.FindAsync(entity.ImageId))
				'End If

				'Dim image As New Image With {
				'		.Original = ImageUtility.FileToBytes(model.ImageFile.InputStream, model.ImageFile.ContentType),
				'		.Thumbnail = ImageUtility.FileToBytes(model.ImageFile.InputStream, model.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill),
				'		.Large = ImageUtility.FileToBytes(model.ImageFile.InputStream, model.ImageFile.ContentType, 555, 555, StretchMode.UniformToFill),
				'		.Medium = ImageUtility.FileToBytes(model.ImageFile.InputStream, model.ImageFile.ContentType, 260, 260, StretchMode.UniformToFill),
				'		.Small = ImageUtility.FileToBytes(model.ImageFile.InputStream, model.ImageFile.ContentType, 160, 160, StretchMode.Uniform, Drawing.Color.White)
				'	}

				'Db.Images.Add(image)
				'entity.ImageId = image.Id
				'Await Db.SaveChangesAsync

				TempData("Message") = "Изображение изменено."
			End If

			Return RedirectToAction("Edit", New With {model.Id})
		End Function

		Public Async Function DeleteImage(id As Guid) As Task(Of ActionResult)
			'Dim entity = Await Db.Products.FindAsync(id)

			'If Not IsNothing(entity.ImageId) Then
			'	Db.Images.Remove(Await Db.Images.FindAsync(entity.ImageId))
			'End If

			'entity.ImageId = Nothing
			'Await Db.SaveChangesAsync
			'TempData("Message") = "Изображение удалено."

			Return RedirectToAction("Edit", New With {id})
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If _ProductManager IsNot Nothing Then
					_ProductManager.Dispose()
					_ProductManager = Nothing
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace