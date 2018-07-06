''' <summary>
''' Представляет значения параметров для методов визуализации постраничной навигации.
''' </summary>
Public Class PaginationOptions
	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="PaginationOptions"/>.
	''' </summary>
	Public Sub New()
		_PreviousText = "&laquo;"
		_NextText = "&raquo;"
		_ActiveCssClass = "active"
	End Sub

	''' <summary>
	''' Устанавливает или возвращает текст ссылки на предыдущую страницу. Допускается использовать HTML. По умолчанию <code>«</code>.
	''' </summary>
	Public Property PreviousText As String

	''' <summary>
	''' Устанавливает или возвращает текст ссылки на следующую страницу. Допускается использовать HTML. По умолчанию <code>»</code>.
	''' </summary>
	Public Property NextText As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для активной ссылки. По умолчанию <code>active</code>.
	''' </summary>
	Public Property ActiveCssClass As String
End Class
