Public Interface IEntityStore(Of TEntity As {Class, IEntity(Of Guid)})
	Inherits IEntityStore(Of TEntity, Guid)
End Interface

Public Interface IEntityStore(Of TEntity As {Class, IEntity(Of TKey)}, In TKey)
	Inherits IDisposable
	ReadOnly Property Entities As IQueryable(Of TEntity)
	Function CreateAsync(entity As TEntity) As Task
	Function CreateAsync(entities As IEnumerable(Of TEntity)) As Task
	Function FindByIdAsync(id As TKey) As Task(Of TEntity)
	Function UpdateAsync(entity As TEntity) As Task
	Function DeleteAsync(entity As TEntity) As Task
	Function DeleteAsync(entities As IEnumerable(Of TEntity)) As Task
	Sub Create(entity As TEntity)
	Sub Create(entities As IEnumerable(Of TEntity))
	Function FindById(id As Guid) As TEntity
	Sub Update(entity As TEntity)
	Sub Delete(entity As TEntity)
	Sub Delete(entitis As IEnumerable(Of TEntity))
End Interface
