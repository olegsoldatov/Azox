Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
    <Authorize>
    Public Class ArticlesController
        Inherits Controller

        Private ReadOnly Db As New ApplicationDbContext

        Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
            Dim entities = Db.Articles.AsQueryable

            ' Поиск.
            If Not String.IsNullOrEmpty(filter.SearchText) Then
                Dim s = filter.SearchText.ToLower.Replace("ё", "е")
                entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s) Or x.Slug.ToLower.Contains(s))
            End If

            ' Сортировка.
            entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Name)

            ' Фильтр.
            ViewBag.Filter = filter

            ' Количество и пагинация.
            ViewBag.Count = Await entities.CountAsync
            ViewBag.PageIndex = pageIndex
            ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

            Dim result = Await entities _
                .Skip(pageIndex * pageSize) _
                .Take(pageSize) _
                .ToListAsync

            Return View(result _
                .Select(Function(x) New ArticleAdminViewModel With {
                    .Id = x.Id,
                    .Name = x.Name,
                    .Slug = x.Slug}))
        End Function

        <HttpGet>
        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(<Bind(Include:="Name,Content,Title,Description,Keywords,Slug")> model As ArticleCreateViewModel) As Task(Of ActionResult)
            If Not String.IsNullOrEmpty(model.Slug) AndAlso Await Db.Articles.AnyAsync(Function(x) x.Slug.ToLower = model.Slug.Trim.ToLower) Then
                ModelState.AddModelError("Slug", "Такой путь уже существует.")
            End If

            If ModelState.IsValid Then
                Db.Articles.Add(New Article With {
                    .Id = Guid.NewGuid,
                    .Name = model.Name,
                    .Content = model.Content,
                    .Title = model.Title,
                    .Description = model.Description,
                    .Keywords = model.Keywords,
                    .Slug = model.Slug,
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
            Return View(New ArticleEditViewModel With {.Id = entity.Id, .Name = entity.Name, .Content = entity.Content, .Title = entity.Title, .Description = entity.Description, .Keywords = entity.Keywords, .Slug = entity.Slug})
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(<Bind(Include:="Id,Name,Content,Title,Description,Keywords,Slug")> model As ArticleEditViewModel, returnUrl As String) As Task(Of ActionResult)
            If Not String.IsNullOrEmpty(model.Slug) AndAlso Await Db.Articles.AnyAsync(Function(x) x.Slug.ToLower = model.Slug.Trim.ToLower And Not x.Id = model.Id) Then
                ModelState.AddModelError("Slug", "Такой путь уже существует.")
            End If

            If ModelState.IsValid Then
                Dim entity = Await Db.Articles.FindAsync(model.Id)
                Response.RemoveOutputCacheItem(Url.Action("details", "articles", New With {entity.Id, .area = ""}))
                If Not String.IsNullOrEmpty(entity.Slug) Then
                    Response.RemoveOutputCacheItem(entity.Slug)
                End If
                entity.Name = model.Name
                entity.Content = model.Content
                entity.Title = model.Title
                entity.Description = model.Description
                entity.Keywords = model.Keywords
                entity.Slug = model.Slug
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

        <HttpGet>
        Public Function SlugValid(path As String, id As Guid?) As ActionResult
            If String.IsNullOrEmpty(path) Then
                Return Json(True, JsonRequestBehavior.AllowGet)
            End If
            Return Json(Not Db.Articles.Any(Function(x) x.Slug.ToLower = path.Trim.ToLower And Not x.Id = id), JsonRequestBehavior.AllowGet)
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