''' <summary>
''' Предоставляет параметры для методов визуализации элементов навигации.
''' </summary>
Public Class NavOptions
	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="NavOptions"/>.
	''' </summary>
	Public Sub New()
		HideRoot = False
		CssClass = "nav"
		ItemCssClass = "nav-item"
		LinkCssClass = "nav-link"
		ActiveLinkCssClass = "active"
		DropDownMenuCssClass = "dropdown-menu"
		DropDownItemCssClass = "dropdown-item"
		HideDropDown = False
	End Sub

	''' <summary>
	''' Устанавливает или возвращает скрытие пункта корневого узла. По умолчанию <code>False</code>.
	''' </summary>
	Public Property HideRoot As Boolean

	''' <summary>
	''' Устанавливает или возвращает класс стиля для контейнера элемента навигации. По умолчанию <code>nav</code>.
	''' </summary>
	Public Property CssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для пункта элемента навигации. По умолчанию <code>nav-item</code>.
	''' </summary>
	Public Property ItemCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для ссылки. По умолчанию <code>nav-link</code>.
	''' </summary>
	Public Property LinkCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для активной ссылки. По умолчанию <code>active</code>.
	''' </summary>
	Public Property ActiveLinkCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля выпадающего меню. По умолчанию <code>dropdown-menu</code>.
	''' </summary>
	Public Property DropDownMenuCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля пункта выпадающего меню. По умолчанию <code>dropdown-item</code>.
	''' </summary>
	Public Property DropDownItemCssClass As String

	''' <summary>
	''' Устанавливает или возвращает скрытие выпадающего меню. По умолчанию <code>False</code>.
	''' </summary>
	Public Property HideDropDown As Boolean
End Class
