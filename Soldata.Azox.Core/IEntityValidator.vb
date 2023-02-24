Public Interface IEntityValidator(Of T)
    Function ValidateAsync(item As T) As Task(Of EntityResult)
End Interface
