Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class EntityStore(Of TEntity As {Class, IEntity})
    Implements IEntityStore(Of TEntity)

    Private disposedValue As Boolean

    Public ReadOnly Property Context As ApplicationDbContext

    Public Sub New(context As ApplicationDbContext)
        Me.Context = context
    End Sub

    Public ReadOnly Property Entities As IQueryable(Of TEntity) Implements IEntityStore(Of TEntity).Entities
        Get
            Return Context.Set(Of TEntity)
        End Get
    End Property

    Public Function FindByIdAsync(entityId As Guid?) As Task(Of TEntity) Implements IEntityStore(Of TEntity).FindByIdAsync
        Return Context.Set(Of TEntity).FindAsync(entityId)
    End Function

    Public Async Function CreateAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity).CreateAsync
        Context.Set(Of TEntity).Add(entity)
        Await Context.SaveChangesAsync
    End Function

    Public Async Function CreateRangeAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity).CreateRangeAsync
        Context.Set(Of TEntity).AddRange(entities)
        Await Context.SaveChangesAsync
    End Function

    Public Async Function UpdateAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity).UpdateAsync
        Context.Entry(entity).State = EntityState.Modified
        Await Context.SaveChangesAsync
    End Function

    Public Async Function UpdateRangeAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity).UpdateRangeAsync
        For Each item In entities
            Context.Entry(item).State = EntityState.Modified
        Next
        Await Context.SaveChangesAsync
    End Function

    Public Async Function DeleteAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity).DeleteAsync
        Context.Set(Of TEntity).Remove(entity)
        Await Context.SaveChangesAsync
    End Function

    Public Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity).DeleteRangeAsync
        Context.Set(Of TEntity).RemoveRange(entities)
        Await Context.SaveChangesAsync
    End Function

#Region "IDisposable"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: освободить управляемое состояние (управляемые объекты)
            End If

            ' TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
            ' TODO: установить значение NULL для больших полей
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
