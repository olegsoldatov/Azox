'=============================
'Последняя редакция 09.08.2014
'=============================

Imports System.Data.Entity

''' <summary>
''' Управляет источником данных.
''' </summary>
<ObsoleteAttribute>
Public Class ContentStore
    Inherits ContentStore(Of Content, Category, ContentCategory)

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class

''' <summary>
''' Управляет источником данных.
''' </summary>
<ObsoleteAttribute>
Public Class ContentStore(Of TContent As Content, TCategory As Category(Of TContentCategory), TContentCategory As {New, ContentCategory})
    Inherits Store(Of TContent)
    Implements IDisposable, IQueryableContentStore(Of TContent), IContentCategoryStore(Of TContent), IContentStore(Of TContent)

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub

    Public ReadOnly Property Contents As IQueryable(Of TContent) Implements IQueryableContentStore(Of TContent).Contents
        Get
            Return MyBase.Context.Set(Of TContent).Include(Function(m) m.Categories)
        End Get
    End Property

    Public Function AddToCategoryAsync(content As TContent, categoryName As String) As Threading.Tasks.Task Implements IContentCategoryStore(Of TContent).AddToCategoryAsync
        Throw New NotImplementedException
    End Function

    Public Function GetCategoriesAsync(content As TContent) As Threading.Tasks.Task(Of IList(Of String)) Implements IContentCategoryStore(Of TContent).GetCategoriesAsync
        Throw New NotImplementedException
    End Function

    Public Function IsInCategory(content As TContent, categoryName As String) As Threading.Tasks.Task(Of Boolean) Implements IContentCategoryStore(Of TContent).IsInCategory
        Throw New NotImplementedException
    End Function

    Public Function RemoveFromCategory(content As TContent, categoryName As String) As Threading.Tasks.Task Implements IContentCategoryStore(Of TContent).RemoveFromCategory
        Throw New NotImplementedException
    End Function
End Class
