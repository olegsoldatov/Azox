' © Софт Бизнес, ООО. Все права защищены.

''' <summary>
''' Представляет определение хранилища сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Interface IEntityStore(Of TEntity As {Class, IEntity})
	Inherits IDisposable

	''' <summary>
	''' Предоставляет возможность создания запросов к источнику данных сущности.
	''' </summary>
	''' <returns></returns>
	ReadOnly Property Entities As IQueryable(Of TEntity)

	''' <summary>
	''' Добавляет новую сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Function CreateAsync(entity As TEntity) As Task

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

	''' <summary>
	''' Находит сущность.
	''' </summary>
	''' <param name="entityId">Идентификатор сущности.</param>
	Function FindByIdAsync(entityId As Guid) As Task(Of TEntity)
End Interface
