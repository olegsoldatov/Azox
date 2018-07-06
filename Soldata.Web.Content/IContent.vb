''' <summary>
''' Предоставляет определение модели данных содержания.
''' </summary>
Public Interface IContent
    Property Id As Guid
    Property Title As String
    Property Description As String
    Property Keywords As String
    Property Heading As String
    Property Text As String
    Property ActionName As String
    Property ControllerName As String
End Interface
