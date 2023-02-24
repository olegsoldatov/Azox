' © Софт Бизнес. Все права защищены.

''' <summary>
''' Представляет определение хранилища сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Interface IEntityStore(Of TEntity As {Class, IEntity})
	Inherits IEntityStore(Of TEntity, Guid)
End Interface

''' <summary>
''' Представляет определение хранилища сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
''' <typeparam name="TKey">Тип ключевого поля.</typeparam>
Public Interface IEntityStore(Of TEntity As {Class, IEntity(Of TKey)}, TKey)
	Inherits IDisposable

	''' <summary>
	''' Возвращает перечисление сущностей с возможностью запросов.
	''' </summary>
	ReadOnly Property Entities As IQueryable(Of TEntity)

	''' <summary>
	''' Находит сущность.
	''' </summary>
	''' <param name="id">Идентификатор сущности.</param>
	Function FindByIdAsync(id As TKey) As Task(Of TEntity)

	''' <summary>
	''' Находит перечисление сущностей.
	''' </summary>
	''' <param name="id">Перечисление идентификаторов.</param>
	Function FindByIdRangeAsync(id As IEnumerable(Of TKey)) As Task(Of IEnumerable(Of TEntity))

	''' <summary>
	''' Добавляет новую сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Function CreateAsync(entity As TEntity) As Task

	''' <summary>
	''' Добавляет перечисление сущностей.
	''' </summary>
	''' <param name="entities">Перечисление сущностей.</param>
	Function CreateRangeAsync(entities As IEnumerable(Of TEntity)) As Task

	''' <summary>
	''' Обновляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Function UpdateAsync(entity As TEntity) As Task

	''' <summary>
	''' Обновляет перечисление сущностей.
	''' </summary>
	''' <param name="entities">Перечисление сущностей.</param>
	Function UpdateRangeAsync(entities As IEnumerable(Of TEntity)) As Task

	''' <summary>
	''' Удаляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Function DeleteAsync(entity As TEntity) As Task

	''' <summary>
	''' Удаляет перечисление сущностей.
	''' </summary>
	''' <param name="entities">Перечисление сущностей.</param>
	Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task
End Interface
