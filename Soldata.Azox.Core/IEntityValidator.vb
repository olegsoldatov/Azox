''' <summary>
''' Предоставляет определение валидатора сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Interface IEntityValidator(Of TEntity As {Class, IEntity})
    Inherits IEntityValidator(Of TEntity, Guid)
End Interface

''' <summary>
''' Предоставляет определение валидатора сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
''' <typeparam name="TKey">Тип ключевого поля.</typeparam>
Public Interface IEntityValidator(Of TEntity As {Class, IEntity(Of TKey)}, TKey)
    ''' <summary>
    ''' Производит валидацию сущности.
    ''' </summary>
    ''' <param name="entity">Сущность.</param>
    Function ValidateAsync(entity As TEntity) As Task(Of EntityResult)
End Interface
