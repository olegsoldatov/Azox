Imports System.Threading.Tasks

''' <summary>
''' Предоставляет определение хранилища содержания.
''' </summary>
''' <typeparam name="TContent">Тип элемента.</typeparam>
Public Interface IContentStore(Of TContent As {Class, IContent})
    Inherits IDisposable

    Function CreateAsync(entity As TContent) As Task(Of Integer)
    Function DeleteAsync(entity As TContent) As Task(Of Integer)
    Function FindByIdAsync(id As Guid) As Task(Of TContent)
    Function FindByNameAsync(name As String) As Task(Of TContent)
    Function UpdateAsync(entity As TContent) As Task(Of Integer)
End Interface

Public Interface IQueryableContentStore(Of TContent As {Class, IContent})
    Inherits IDisposable, IContentStore(Of TContent)

    ReadOnly Property Contents As IQueryable(Of TContent)
End Interface

