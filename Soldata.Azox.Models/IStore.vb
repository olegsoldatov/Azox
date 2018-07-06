Imports System.Threading.Tasks

''' <summary>
''' Предоставляет определение хранилища.
''' </summary>
''' <typeparam name="TEntity">Тип элемента.</typeparam>
Public Interface IStore(Of TEntity As {Class, IEntity})
    Inherits IDisposable

    Function CreateAsync(entity As TEntity) As Task(Of Integer)
    Function DeleteAsync(entity As TEntity) As Task(Of Integer)
    Function FindByIdAsync(id As Guid) As Task(Of TEntity)
    Function FindByNameAsync(name As String) As Task(Of TEntity)
    Function UpdateAsync(entity As TEntity) As Task(Of Integer)
End Interface
