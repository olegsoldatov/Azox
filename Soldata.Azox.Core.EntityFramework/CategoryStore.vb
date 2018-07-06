'=============================
'Последняя редакция 09.08.2014
'=============================

Imports System.Data.Entity

<ObsoleteAttribute>
Public Class CategoryStore
    Inherits CategoryStore(Of Category, ContentCategory)

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class

<ObsoleteAttribute>
Public Class CategoryStore(Of TCategory As {New, Category}, TContentCategory As {New, ContentCategory})
    Inherits Store(Of TCategory)
    Implements IDisposable, IQueryableCategoryStore(Of TCategory), ICategoryStore(Of TCategory)

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub

    Public ReadOnly Property Categories As IQueryable(Of TCategory) Implements IQueryableCategoryStore(Of TCategory).Categories
        Get
            Return MyBase.Context.Set(Of TCategory)()
        End Get
    End Property
End Class
