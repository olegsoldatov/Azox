'Namespace Areas.Admin.Controllers
'	<Authorize>
'	Public Class ServicesController
'		Inherits Controller

'		Private Manager As New ServiceManager

'		Protected Overrides Sub Dispose(disposing As Boolean)
'			If disposing Then
'				If Manager IsNot Nothing Then
'					Manager.Dispose()
'					Manager = Nothing
'				End If
'			End If
'			MyBase.Dispose(disposing)
'		End Sub

'		Public Async Function Index(searchString As String, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
'			Dim items = Manager.Entities.AsNoTracking

'			'Поиск.
'			If Not String.IsNullOrEmpty(searchString) Then
'				items = items.Where(Function(m) m.Title.Contains(searchString))
'			End If

'			'Пагинация.
'			ViewBag.PageIndex = pageIndex
'			ViewBag.PageCount = CInt(Math.Ceiling(Await items.CountAsync / pageSize))

'			Return View(Await items.OrderByDescending(Function(m) m.LastUpdateDate).ThenBy(Function(m) m.Title).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync())
'		End Function

'		Public Function Create() As ActionResult
'			Return View()
'		End Function

'		<HttpPost>
'		<ValidateAntiForgeryToken>
'		Public Async Function Create(model As Service, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
'			If ModelState.IsValid Then
'				If imageFile IsNot Nothing AndAlso Not imageFile.ContentType.Contains("image") Then
'					ModelState.AddModelError("Image", "Загружаемый файл не является изображением.")
'				Else
'					Await Manager.CreateAsync(model)
'					TempData("Message") = "Добавлено."

'					'Создадим новое изображение.
'					If imageFile IsNot Nothing Then
'						Dim image = New ApplicationImage

'						With image
'							.ContentType = imageFile.ContentType
'							.Content = ImageUtility.ToBytes(imageFile)
'							.Thumbnail = ImageUtility.ToBytes(imageFile, 200, 200, ImageTransformMode.Cut)
'							.Small = ImageUtility.ToBytes(imageFile, 272)
'							.Medium = ImageUtility.ToBytes(imageFile, 600)
'							.Large = ImageUtility.ToBytes(imageFile, 1200)
'						End With

'						Await Manager.AddImageAsync(model.Id, image)
'					End If

'					Return RedirectToAction("index")
'				End If
'			End If
'			Return View(model)
'		End Function

'		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
'			If IsNothing(id) Then
'				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'			End If
'			Dim model = Await Manager.FindByIdAsync(id)
'			If IsNothing(model) Then
'				Return HttpNotFound()
'			End If
'			Return View(model)
'		End Function

'		<HttpPost>
'		<ValidateAntiForgeryToken>
'		Public Async Function Edit(model As Service, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
'			If ModelState.IsValid Then
'				If imageFile IsNot Nothing AndAlso Not imageFile.ContentType.Contains("image") Then
'					ModelState.AddModelError("Image", "Загружаемый файл не является изображением.")
'				Else
'					Await Manager.UpdateAsync(model)
'					TempData("Message") = "Изменено."

'					'Получим или создадим новое изображение.
'					If imageFile IsNot Nothing Then
'						Dim image = If((Await Manager.GetImageAsync(model.Id)), New ApplicationImage)

'						With image
'							.ContentType = imageFile.ContentType
'							.Content = ImageUtility.ToBytes(imageFile)
'							.Thumbnail = ImageUtility.ToBytes(imageFile, 200, 200, ImageTransformMode.Cut)
'							.Small = ImageUtility.ToBytes(imageFile, 272)
'							.Medium = ImageUtility.ToBytes(imageFile, 600)
'							.Large = ImageUtility.ToBytes(imageFile, 1200)
'						End With

'						Await Manager.AddImageAsync(model.Id, image)
'					End If

'					Return RedirectToAction("index")
'				End If
'			End If
'			Return View(model)
'		End Function

'		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
'			If IsNothing(id) Then
'				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'			End If
'			Dim model = Await Manager.FindByIdAsync(id)
'			If IsNothing(model) Then
'				Return HttpNotFound()
'			End If
'			Return View(model)
'		End Function

'		<HttpPost>
'		<ActionName("Delete")>
'		<ValidateAntiForgeryToken>
'		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
'			Await Manager.DeleteAsync(Await Manager.FindByIdAsync(id))
'			TempData("Message") = "Удалено."
'			Return RedirectToAction("index")
'		End Function

'		<HttpPost>
'		Public Async Function DeleteChecked(id As Guid()) As Task(Of ActionResult)
'			For Each item In id
'				Await Manager.DeleteAsync(Await Manager.FindByIdAsync(item))
'			Next
'			TempData("Message") = "Удалено."
'			Return Json(New With {.redirect = Url.Action("index")})
'		End Function

'		'Public Async Function Rebuild() As Task(Of ActionResult)
'		'	Dim entities = Await Manager.Entities.ToListAsync()
'		'	For Each item In entities
'		'		If Not IsNothing(item.Image) Then
'		'			Dim image = item.Image
'		'			image.Thumbnail = ImageUtility.Regenerate(image.Content, image.ContentType, 200, 200, ImageTransformMode.Cut)
'		'			image.Small = ImageUtility.Regenerate(image.Content, image.ContentType, 272, 272, ImageTransformMode.Default)
'		'			image.Medium = Nothing
'		'			image.Large = Nothing
'		'			Await Manager.ImageManager.UpdateAsync(image)
'		'		End If
'		'	Next
'		'	TempData("Message") = "Процесс завершен."
'		'	Return RedirectToAction("index")
'		'End Function
'	End Class
'End Namespace