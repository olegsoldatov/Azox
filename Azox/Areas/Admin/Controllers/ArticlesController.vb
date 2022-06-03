Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
	Public Class ArticlesController
		Inherits AdminController

		Private ReadOnly Db As New ApplicationDbContext

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = Db.Articles.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
                entities = entities.Where(Function(x) x.Heading.ToLower.Replace("ё", "е").Contains(s))
            End If

            ' Сортировка.
            entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Heading)

            ' Фильтр.
            ViewBag.Filter = filter

            Pagination(Await entities.CountAsync, pageIndex, pageSize)

            Dim result = Await entities _
                .Skip(pageIndex * pageSize) _
                .Take(pageSize) _
                .ToListAsync

            Return View(result _
                .Select(Function(x) New ArticleAdminViewModel With {
                    .Id = x.Id,
                    .Name = x.Heading}))
        End Function

        <HttpGet>
        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As ArticleCreateViewModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Db.Articles.Add(New Article With {
                    .Id = Guid.NewGuid,
                    .Heading = model.Name,
                    .Content = model.Content,
                    .LastUpdateDate = Now
                })
                Await Db.SaveChangesAsync
                TempData("Message") = "Статья добавлена."
                Return RedirectToAction("index")
            End If

            Return View(model)
        End Function

        Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity = Await Db.Articles.FindAsync(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(New ArticleEditViewModel With {.Id = entity.Id, .Name = entity.Heading, .Content = entity.Content})
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As ArticleEditViewModel, returnUrl As String) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim entity = Await Db.Articles.FindAsync(model.Id)
                Response.RemoveOutputCacheItem(Url.Action("details", "articles", New With {entity.Id, .area = ""}))
                entity.Heading = model.Name
                entity.Content = model.Content
                entity.LastUpdateDate = Now
                Db.Entry(entity).State = EntityState.Modified
                Await Db.SaveChangesAsync
                TempData("Message") = "Статья изменена."
                Return RedirectToLocal(returnUrl)
            End If

            Return View(model)
        End Function

        Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity = Await Db.Articles.FindAsync(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(entity)
        End Function

        <HttpPost>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
            Dim entity = Await Db.Articles.FindAsync(id)
            Db.Articles.Remove(entity)
            Await Db.SaveChangesAsync
            TempData("Message") = "Статья удалена."
            Return RedirectToLocal(returnUrl)
        End Function

        Private Function RedirectToLocal(returnUrl As String) As ActionResult
			If Url.IsLocalUrl(returnUrl) Then
				Return Redirect(returnUrl)
			End If
			Return RedirectToAction("index")
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				Db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace