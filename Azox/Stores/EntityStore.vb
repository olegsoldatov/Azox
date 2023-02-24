Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class EntityStore(Of TEntity As {Class, IEntity})
    Implements IEntityStore(Of TEntity)

    Private disposedValue As Boolean
    Private ReadOnly _Context As ApplicationDbContext

    Public Overridable ReadOnly Property Entities As IQueryable(Of TEntity) Implements IEntityStore(Of TEntity, Guid).Entities
        Get
            Return _Context.Set(Of TEntity)
        End Get
    End Property

    Public Sub New(context As ApplicationDbContext)
        _Context = context
    End Sub

    Public Overridable Function FindByIdAsync(id As Guid) As Task(Of TEntity) Implements IEntityStore(Of TEntity, Guid).FindByIdAsync
        Return _Context.Set(Of TEntity).FindAsync(id)
    End Function

    Public Overridable Async Function FindByIdRangeAsync(id As IEnumerable(Of Guid)) As Task(Of IEnumerable(Of TEntity)) Implements IEntityStore(Of TEntity, Guid).FindByIdRangeAsync
        Return Await _Context.Set(Of TEntity).Where(Function(x) id.Contains(x.Id)).ToListAsync
    End Function

    Public Overridable Async Function CreateAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity).CreateAsync
        _Context.Set(Of TEntity).Add(entity)
        Await _Context.SaveChangesAsync
    End Function

    Public Overridable Async Function CreateRangeAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity, Guid).CreateRangeAsync
        _Context.Set(Of TEntity).AddRange(entities)
        Await _Context.SaveChangesAsync
    End Function

    Public Overridable Async Function UpdateAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity, Guid).UpdateAsync
        _Context.Entry(entity).State = EntityState.Modified
        Await _Context.SaveChangesAsync
    End Function

    Public Overridable Async Function UpdateRangeAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity, Guid).UpdateRangeAsync
        For Each item In entities
            _Context.Entry(item).State = EntityState.Modified
        Next
        Await _Context.SaveChangesAsync
    End Function

    Public Overridable Async Function DeleteAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity, Guid).DeleteAsync
        _Context.Set(Of TEntity).Remove(entity)
        Await _Context.SaveChangesAsync
    End Function

    Public Overridable Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity, Guid).DeleteRangeAsync
        _Context.Set(Of TEntity).RemoveRange(entities)
        Await _Context.SaveChangesAsync
    End Function

#Region "IDisposable"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _Context?.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Не изменяйте этот код. Разместите код очистки в методе "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
