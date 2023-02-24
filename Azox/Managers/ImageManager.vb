Imports Soldata.Azox

Public Class ImageManager
    Inherits Soldata.Azox.EntityManager(Of Image)

    Public Sub New(store As IEntityStore(Of Image))
        MyBase.New(store)
    End Sub

    Public ReadOnly Property Images As IQueryable(Of Image)
        Get
            Return Store.Entities
        End Get
    End Property
End Class
