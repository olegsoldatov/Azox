Imports Soldata.Azox

Public Class CustomerManager
    Inherits EntityManager(Of Customer)

    Public Sub New(store As IEntityStore(Of Customer))
        MyBase.New(store)
    End Sub

    Public ReadOnly Property Customers As IQueryable(Of Customer)
        Get
            Return Store.Entities
        End Get
    End Property
End Class
