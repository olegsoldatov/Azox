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

		Public Async Function Index(query As BrandQuery, Optional pageSize As Integer = 50, Optional pageIndex As Integer = 0) As Task(Of ActionResult)
			Dim result = Await BrandManager.GetListAsync(query, pageSize, pageIndex)

			ViewBag.TotalCount = result.TotalCount
			ViewBag.PageCount = result.PageCount
			ViewBag.PageIndex = pageIndex
			ViewBag.PageSize = pageSize

			Return View(result.Items)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), returnUrl As String, Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim brands = Await BrandManager.FindByIdRangeAsync(id)
				If delete Then
					Await BrandManager.DeleteRangeAsync(brands)
					Alert($"Удалено: {id.Length.ToString("бренд", "бренда", "брендов")}.")
				End If
			End If

			Return RedirectToReturnUrl(returnUrl)
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
				Dim result = Await BrandManager.CreateAsync(brand)
				If result.Succeeded Then
					Alert("Бренд добавлен.")
					Return RedirectToAction("index")
				End If
				AddErrors(result)
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
				Dim result = Await BrandManager.UpdateAsync(brand)
				If result.Succeeded Then
					Alert("Бренд изменен.")
					Return RedirectToReturnUrl(returnUrl)
				End If
				AddErrors(result)
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