Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Controllers
    Public Class ProductsController
        Inherits Controller

        Private ReadOnly ProductManager As ProductManager(Of Product)

        Public Sub New(productManager As ProductManager(Of Product))
            Me.ProductManager = productManager
        End Sub

        Public Async Function Index(filter As ProductFilter, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
            Dim entities = ProductManager.Products.Include(Function(x) x.Category)

            ' Поиск.
            If Not String.IsNullOrEmpty(filter.SearchText) Then
                Dim s = filter.SearchText.Trim.ToLower.Replace("ё", "е")
                entities = entities.Where(Function(x) x.Title.Trim.ToLower.Replace("ё", "е").Contains(s) Or x.Sku.Contains(s) Or x.BrandName.Trim.ToLower.Replace("ё", "е").Contains(s))
            End If

            ' Производитель.
            If Not String.IsNullOrEmpty(filter.Vendor) Then
                entities = entities.Where(Function(x) x.BrandName = filter.Vendor)
            End If

            ' Категория.
            If Not IsNothing(filter.CategoryId) Then
                entities = entities.Where(Function(x) x.CategoryId = filter.CategoryId)
            End If

            ' Сортировка.
            entities = entities.OrderByDescending(Function(x) x.LastUpdateDate)

            ' Фильтр.
            Dim parameters = Await entities.Select(Function(x) New With {x.Category, x.Brand}).ToListAsync
            ViewBag.CategoryId = New SelectList(parameters.Select(Function(x) x.Category).Where(Function(x) Not IsNothing(x)).GroupBy(Function(x) x).Select(Function(x) New With {x.Key.Id, .Name = x.Key.GetPath}).OrderBy(Function(x) x.Name), "Id", "Name")
            ViewBag.BrandId = New SelectList(parameters.Select(Function(x) x.Brand).Where(Function(x) Not IsNothing(x)).GroupBy(Function(x) x).OrderBy(Function(x) x.Key.Title).Select(Function(x) New With {x.Key.Id, x.Key.Title}), "Id", "Title")
            ViewBag.Filter = filter

            ' Количество и пагинация.
            ViewBag.TotalCount = Await entities.CountAsync
            ViewBag.PageIndex = pageIndex
            ViewBag.PageCount = CInt(Math.Ceiling(ViewBag.TotalCount / pageSize))

            Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
        End Function

        Public Async Function Details(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim model = Await ProductManager.FindByIdAsync(id)
            If IsNothing(model) Then
                Return HttpNotFound()
            End If
            Return View(model)
        End Function
    End Class
End Namespace