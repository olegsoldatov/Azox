Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Imaging
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class CategoriesController
		Inherits Controller

		Private ReadOnly manager As New CategoryManager
		Private ReadOnly imageService As New ImageService With {
			.SmallConfiguration = New ImageConfiguration With {.Width = 320, .Height = 180, .Mode = StretchMode.UniformToFill},
			.MediumConfiguration = New ImageConfiguration With {.Width = 544, .Height = 306, .Mode = StretchMode.UniformToFill},
			.LargeConfiguration = New ImageConfiguration With {.Width = 1200, .Height = 255, .Mode = StretchMode.UniformToFill}}

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = manager.Categories

			' Фильтр.
			ViewBag.Filter = filter

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s) Or x.Name.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Количество и пагинация.
			Dim count = Await entities.AsNoTracking().CountAsync()
			ViewBag.Count = count
			ViewBag.PageIndex = pageIndex
			ViewBag.PageSize = pageSize
			ViewBag.PageCount = CInt(Math.Ceiling(count / pageSize))

			' Сортировка.
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Title)

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync())
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), model As CategoryChangeViewModel, Optional delete As Boolean = False, Optional change As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim entities = Await manager.Categories.Where(Function(x) id.Contains(x.Id)).ToListAsync()

				If delete Then
					Await imageService.DeleteAsync(entities)
					Await manager.DeleteAsync(entities)
					TempData("Message") = String.Format("Удалено: {0}.", id.Length.ToString("категория", "категории", "категорий"))
				ElseIf change Then
					If Not IsNothing(model.Draft) Then
						entities.ForEach(Sub(x) x.Draft = model.Draft)
					End If
					Await manager.UpdateAsync(entities)
					TempData("Message") = String.Format("Изменено: {0}.", id.Length.ToString("категория", "категории", "категорий"))
				End If
			End If

			Return Redirect(Request.UrlReferrer.PathAndQuery)
		End Function

		<HttpGet>
		Public Function Create() As ActionResult
			ViewBag.ParentId = New SelectList(manager.Categories.OrderBy(Function(x) x.Title), "Id", "Title")
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Create(model As Category, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
			If Await manager.NameExistsAsync(model) Then
				ModelState.AddModelError("Name", "Такое имя уже существует.")
			End If
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError("ImageId", "Файл не является изображением.")
			End If
			If ModelState.IsValid Then
				If Not IsNothing(imageFile) Then
					Await imageService.UploadAsync(model, imageFile.InputStream, imageFile.ContentType)
				End If
				Await manager.AddAsync(model)
				TempData("Message") = "Категория добавлена."
				Return RedirectToAction("index")
			End If
			ViewBag.ParentId = New SelectList(manager.Categories.OrderBy(Function(x) x.Title), "Id", "Title", model.ParentId)
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await manager.GetByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.ParentId = New SelectList(manager.Categories.Where(Function(x) Not x.Id = model.Id).OrderBy(Function(x) x.Title), "Id", "Title", model.ParentId)
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Category, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
			If Await manager.NameExistsAsync(model) Then
				ModelState.AddModelError("Name", "Такое имя уже существует.")
			End If
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError("ImageId", "Файл не является изображением.")
			End If
			If ModelState.IsValid Then
				If Not IsNothing(imageFile) Then
					Await imageService.UploadAsync(model, imageFile.InputStream, imageFile.ContentType)
				End If
				Await manager.UpdateAsync(model)
				TempData("Message") = "Категория изменена."
				If String.IsNullOrEmpty(returnUrl) Then
					Return RedirectToAction("index")
				Else
					Return Redirect(returnUrl)
				End If
			End If
			ViewBag.ParentId = New SelectList(manager.Categories.Where(Function(x) Not x.Id = model.Id).OrderBy(Function(x) x.Title), "Id", "Title", model.ParentId)
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await manager.GetByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Dim entity = Await manager.GetByIdAsync(id)
			If Not IsNothing(entity.ImageId) Then
				Await imageService.DeleteAsync(entity)
			End If
			Await manager.DeleteAsync(entity)
			TempData("Message") = "Категория удалена."
			Return RedirectToAction("index")
		End Function

		<HttpGet>
		Public Function Exists(id As Guid?, name As String) As ActionResult
			If manager.Categories.AsNoTracking().Any(Function(x) Not x.Id = id And x.Name = name) Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				manager.Dispose()
				imageService.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace