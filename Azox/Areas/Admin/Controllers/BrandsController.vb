Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	Public Class BrandsController
		Inherits AdminController

        Private ReadOnly BrandManager As BrandManager

        Public Sub New(brandManager As BrandManager)
			Me.BrandManager = brandManager
		End Sub

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = BrandManager.Brands

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Сортировка.
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Title)

			Pagination(Await entities.CountAsync, pageIndex, pageSize)

			ViewBag.Filter = filter
			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), returnUrl As String, changeModel As ChangeModel, Optional change As Boolean = False, Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim brands = Await BrandManager.Brands.Where(Function(x) id.Contains(x.Id)).ToListAsync

				If change And Not IsNothing(changeModel.IsPublished) Then

					If Not IsNothing(changeModel.IsPublished) Then
						brands.ForEach(Sub(x) x.IsPublished = changeModel.IsPublished)
					End If

					Await BrandManager.UpdateRangeAsync(brands)
					Alert(String.Format("Изменено: {0}.", id.Length.ToString("бренд", "бренда", "брендов")))
				ElseIf delete Then
					Await BrandManager.DeleteRangeAsync(brands)
					Alert(String.Format("Удалено: {0}.", id.Length.ToString("бренд", "бренда", "брендов")))
				End If
			End If

			Return Redirect(returnUrl)
		End Function

		<HttpGet>
		Public Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Create(brand As Brand, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
			If Await BrandManager.ExistsAsync(brand) Then
				ModelState.AddModelError(NameOf(brand.Title), My.Resources.BrandExists)
			End If
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError(NameOf(brand.ImageId), My.Resources.FileIsNotImage)
			End If
			If ModelState.IsValid Then
                Await BrandManager.UploadImageAsync(brand, (imageFile?.InputStream, imageFile?.ContentType))
                Await BrandManager.CreateAsync(brand)
                Alert("Бренд добавлен.")
				Return RedirectToAction("index")
			End If
			Return View(brand)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand = Await BrandManager.FindByIdAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(brand As Brand, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
			If Await BrandManager.ExistsAsync(brand) Then
				ModelState.AddModelError(NameOf(brand.Title), My.Resources.BrandExists)
			End If
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError(NameOf(brand.ImageId), My.Resources.FileIsNotImage)
			End If
			If ModelState.IsValid Then
                Await BrandManager.UploadImageAsync(brand, (imageFile?.InputStream, imageFile?.ContentType))
                Await BrandManager.UpdateAsync(brand)
                Alert("Бренд изменен.")
                Return RedirectToReturnUrl(returnUrl)
			End If
			Return View(brand)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand = Await BrandManager.FindByIdAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim brand = Await BrandManager.FindByIdAsync(id)
			Await BrandManager.DeleteAsync(brand)
			Alert("Бренд удален.")
			Return RedirectToReturnUrl(returnUrl)
		End Function

		<HttpGet>
		Public Function Exists(brand As Brand) As ActionResult
			If Task.Run(Function() BrandManager.ExistsAsync(brand)).Result Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function
	End Class
End Namespace