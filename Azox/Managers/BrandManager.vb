Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class BrandManager
    Inherits PictorialEntityManager(Of Brand)

    Private ReadOnly _brandValidator As IEntityValidator(Of Brand)

    Public Sub New(store As IEntityStore(Of Brand), imageService As BrandImageService)
        MyBase.New(store, imageService)
        _brandValidator = New BrandValidator(Me)
    End Sub

    Public ReadOnly Property Brands As IQueryable(Of Brand)
        Get
            Return Store.Entities
        End Get
    End Property

    Public ReadOnly Property Published As IQueryable(Of Brand)
        Get
            Return Store.Entities.Where(Function(x) x.IsPublished)
        End Get
    End Property

    Public Async Function GetListAsync(query As BrandQuery, limit As Integer, offset As Integer) As Task(Of (Items As IList(Of Brand), TotalCount As Integer, PageCount As Integer))
        Dim entities = Store.Entities.AsNoTracking.Include(Function(x) x.Products)

        ' Поиск.
        If Not String.IsNullOrEmpty(query.SearchText) Then
            Dim s = query.SearchText.ToLower.Replace("ё", "е")
            entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s))
        End If

        ' Количество и пагинация.
        Dim items = Await entities.OrderBy(Function(x) x.Title).Skip(limit * offset).Take(limit).ToListAsync
        Dim totalCount = Await entities.CountAsync
        Dim pageCount = CInt(Math.Ceiling(totalCount / limit))
        Return (items, totalCount, pageCount)
    End Function

    Public Async Function ExistsAsync(entity As Brand) As Task(Of Boolean)
        Dim title = entity.Title.ToLower.Replace("ё", "е")
        Return Await Store.Entities.AsNoTracking.AnyAsync(Function(x) Not x.Id = entity.Id And x.Title.ToLower.Replace("ё", "е") = title)
    End Function
End Class

Public Class BrandQuery
    <Display(Name:="Поиск")>
    Public Property SearchText As String
End Class
