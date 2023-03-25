''' <summary>
''' Предоставляет определение товара.
''' </summary>
Public Interface IProduct
    Inherits IEntity

    ''' <summary>
    ''' Название.
    ''' </summary>
    Property Title As String
End Interface
