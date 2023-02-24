Imports Soldata.Azox

''' <summary>
''' Предоставляет определение бренда.
''' </summary>
Public Interface IBrand
    Inherits IEntity

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    Property Title As String
End Interface
