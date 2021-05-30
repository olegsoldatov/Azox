' © Софт Бизнес, ООО. Все права защищены.

''' <summary>
''' Представляет базовое API сущности.
''' </summary>
''' <typeparam name="TEntity"></typeparam>
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
	''' <param name="entity"></param>
	''' <returns></returns>
	Function CreateAsync(entity As TEntity) As Task

	''' <summary>
	''' Обновляет сущность.
	''' </summary>
	''' <param name="entity"></param>
	''' <returns></returns>
	Function UpdateAsync(entity As TEntity) As Task

	''' <summary>
	''' Удаляет сущность.
	''' </summary>
	''' <param name="entity"></param>
	''' <returns></returns>
	Function DeleteAsync(entity As TEntity) As Task

	''' <summary>
	''' Находит сущность.
	''' </summary>
	''' <param name="entityId"></param>
	''' <returns></returns>
	Function FindByIdAsync(entityId As Guid) As Task(Of TEntity)
End Interface
