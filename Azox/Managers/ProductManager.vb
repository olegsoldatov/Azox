Imports Soldata.Azox

Public Class ProductManager(Of TProduct As {Class, IEntity, IPictorial, New})
    Inherits PictorialEntityManager(Of TProduct)

    Public Sub New(store As IEntityStore(Of TProduct), imageService As ProductImageService)
        MyBase.New(store, imageService)
    End Sub

    Public ReadOnly Property Products As IQueryable(Of TProduct)
        Get
            Return Store.Entities
        End Get
    End Property
End Class
