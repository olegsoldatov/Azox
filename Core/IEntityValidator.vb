''' <summary>
''' Валидатор сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Interface IEntityValidator(Of TEntity As {Class, IEntity})
    ''' <summary>
    ''' Производит валидацию сущности.
    ''' </summary>
    ''' <param name="entity">Сущность.</param>
    Function ValidateAsync(entity As TEntity) As Task(Of EntityResult)
End Interface
