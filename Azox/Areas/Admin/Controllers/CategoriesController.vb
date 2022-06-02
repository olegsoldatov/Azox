Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
    Public Class CategoriesController
        Inherits AdminController

        Private ReadOnly Property CategoryManager As CategoryManager

        Public Sub New(categoryManager As CategoryManager)
            Me.CategoryManager = categoryManager
        End Sub

        Public Async Function Index(filter As EntityFilter, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
            Dim entities = CategoryManager.Categories.Include(Function(x) x.Products).AsNoTracking

            ' Поиск.
            If Not String.IsNullOrEmpty(filter.SearchText) Then
                Dim s = filter.SearchText.ToLower.Replace("ё", "е")
                entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s))
            End If

            ' Сортировка.
            entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Title)

            ' Фильтр.
            ViewBag.Filter = filter

            Pagination(Await entities.CountAsync, pageIndex, pageSize)

            Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Index(id As Guid(), model As CategoryChangeViewModel, returnUrl As String, Optional delete As Boolean = False, Optional change As Boolean = False) As Task(Of ActionResult)
            If Not IsNothing(id) Then
                Dim entities = Await CategoryManager.Categories.Where(Function(x) id.Contains(x.Id)).ToListAsync
                If delete Then
                    Await CategoryManager.DeleteRangeAsync(entities)
                    Alert(String.Format("Удалено: {0}.", entities.Count.ToString("категория", "категории", "категорий")))
                ElseIf change Then
                    If Not IsNothing(model.Draft) Then
                        entities.ForEach(Sub(x) x.Draft = model.Draft)
                    End If
                    Await CategoryManager.UpdateRangeAsync(entities)
                    Alert(String.Format("Изменено: {0}.", entities.Count.ToString("категория", "категории", "категорий")))
                End If
            End If
            Return Redirect(returnUrl)
        End Function

        <HttpGet>
        Public Function Create() As ActionResult
            ViewBag.ParentId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title")
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(category As Category, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
            If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
                ModelState.AddModelError(NameOf(imageFile), My.Resources.FileIsNotImage)
            End If
            If ModelState.IsValid Then
                Await CategoryManager.UploadImageAsync(category, (imageFile?.InputStream, imageFile?.ContentType))
                Await CategoryManager.CreateAsync(category)
                Alert("Категория добавлена.")
                Return RedirectToAction("index")
            End If
            ViewBag.ParentId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title", category.ParentId)
            Return View(category)
        End Function

        Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim category = Await CategoryManager.FindByIdAsync(id)
            If IsNothing(category) Then
                Return HttpNotFound()
            End If
            ViewBag.ParentId = New SelectList(CategoryManager.Categories.Where(Function(x) Not x.Id = category.Id).OrderBy(Function(x) x.Title), "Id", "Title", category.ParentId)
            Return View(category)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(category As Category, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
            If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
                ModelState.AddModelError(NameOf(imageFile), My.Resources.FileIsNotImage)
            End If
            If ModelState.IsValid Then
                Await CategoryManager.UploadImageAsync(category, (imageFile?.InputStream, imageFile?.ContentType))
                Await CategoryManager.UpdateAsync(category)
                Alert("Категория изменена.")
                Return RedirectToReturnUrl(returnUrl)
            End If
            ViewBag.ParentId = New SelectList(CategoryManager.Categories.Where(Function(x) Not x.Id = category.Id).OrderBy(Function(x) x.Title), "Id", "Title", category.ParentId)
            Return View(category)
        End Function

        Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim category = Await CategoryManager.FindByIdAsync(id)
            If IsNothing(category) Then
                Return HttpNotFound()
            End If
            Return View(category)
        End Function

        <HttpPost>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
            Dim category = Await CategoryManager.FindByIdAsync(id)
            Await CategoryManager.DeleteAsync(category)
            Alert("Категория удалена.")
            Return RedirectToReturnUrl(returnUrl)
        End Function
    End Class
End Namespace