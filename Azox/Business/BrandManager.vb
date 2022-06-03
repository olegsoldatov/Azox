Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class BrandManager
    Inherits PictorialEntityManager(Of Brand)

    Public Sub New(store As Soldata.Azox.IEntityStore(Of Brand), imageService As BrandImageService)
        MyBase.New(store, imageService)
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

    Public Function ExistsAsync(entity As Brand) As Task(Of Boolean)
        Return Store.Entities.AnyAsync(Function(x) Not x.Id = entity.Id And x.Title = entity.Title)
    End Function
End Class
