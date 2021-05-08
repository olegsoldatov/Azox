''' <summary>
''' Предоставляет интерфейс страницы.
''' </summary>
Public Interface IPage
	Inherits IEntity
	''' <summary>
	''' Устанавливает или возвращает название.
	''' </summary>
	Property Title As String
	''' <summary>
	''' Устанавливает или возвращает содержание.
	''' </summary>
	Property Content As String
	''' <summary>
	''' Устанавливает или возвращает описание.
	''' </summary>
	Property Description As String
	''' <summary>
	''' Устанавливает или возвращает ключевые слова.
	''' </summary>
	Property Keywords As String
End Interface
