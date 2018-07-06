Namespace Models

    ''' <summary>
    ''' Предоставляет модель страницы.
    ''' </summary>
    <Table("Pages")>
    Public Class Page
        Inherits Entity

        ''' <summary>
        ''' Устанавливает или возвращает имя действия.
        ''' </summary>
        <Required>
        <StringLength(255)>
        Public Property ActionName As String

        ''' <summary>
        ''' Устанавливает или возвращает имя контроллера.
        ''' </summary>
        <Required>
        <StringLength(255)>
        Public Property ControllerName As String
    End Class

End Namespace
