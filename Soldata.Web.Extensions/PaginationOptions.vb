''' <summary>
''' Представляет значения параметров для методов визуализации постраничной навигации.
''' </summary>
Public Class PaginationOptions
	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="PaginationOptions"/>.
	''' </summary>
	Public Sub New()
		_PreviousText = "Пред."
		_NextText = "След."
		_ActiveCssClass = "active"
		_PageItemCssClass = "page-item"
		_PageLinkCssClass = "page-link"
		_DisabledCssClass = "disabled"
		_SrOnlyCssClass = "sr-only"
		_HidePreviousNext = True
		_PageIndexName = "pageIndex"
	End Sub

	''' <summary>
	''' Устанавливает или возвращает текст ссылки на предыдущую страницу. Допускается использовать HTML. По умолчанию <code>Пред.</code>.
	''' </summary>
	Public Property PreviousText As String

	''' <summary>
	''' Устанавливает или возвращает текст ссылки на следующую страницу. Допускается использовать HTML. По умолчанию <code>След.</code>.
	''' </summary>
	Public Property NextText As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для активной ссылки. По умолчанию <code>active</code>.
	''' </summary>
	Public Property ActiveCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для пункта в списке. По умолчанию <code>page-item</code>.
	''' </summary>
	Public Property PageItemCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для ссылки. По умолчанию <code>page-link</code>.
	''' </summary>
	Public Property PageLinkCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для заблокированного пункта в списке. По умолчанию <code>disabled</code>.
	''' </summary>
	Public Property DisabledCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля элементам для экранного браузера. По умолчанию <code>sr-only</code>.
	''' </summary>
	Public Property SrOnlyCssClass As String

	''' <summary>
	''' Устанавливает или возвращает указатель, скрывающий ссылки на предыдущую и следующую страницы. По умолчанию <code>True</code>.
	''' </summary>
	Public Property HidePreviousNext As Boolean

	''' <summary>
	''' Устанавливает или возвращает имя параметра индекса страницы в  строке запроса. По умолчанию <code>pageIndex</code>.
	''' </summary>
	Public Property PageIndexName As String
End Class
