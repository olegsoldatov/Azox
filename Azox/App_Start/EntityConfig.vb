Imports Soldata.Azox

''' <summary>
''' Предоставляет базовые API управления сущностями в источнике данных.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As EntityFramework.Entity)
	Inherits Soldata.Azox.EntityManager(Of TEntity)

	Public Sub New(store As IEntityStore(Of TEntity, Guid))
		MyBase.New(New EntityFramework.EntityStore(Of TEntity)(New ApplicationDbContext))
	End Sub
End Class
