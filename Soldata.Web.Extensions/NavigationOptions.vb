''' <summary>
''' Представляет значения параметров для методов визуализации элементов навигации.
''' </summary>
Public Class NavigationOptions
	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="NavigationOptions"/>.
	''' </summary>
	Public Sub New()
		CssClass = "navbar-nav"
		ItemCssClass = "nav-item"
		ActiveItemCssClass = "active"
		LinkCssClass = "nav-link"
		DropDownMenuCssClass = "dropdown-menu"
		DisabledCssClass = "disabled"
		SrOnlyCssClass = "sr-only"
		ChildLevels = 0
		UseDropDown = False
	End Sub

	Public Sub New(options As NavigationOptions)
		CssClass = options.CssClass
		ItemCssClass = options.ItemCssClass
		ActiveItemCssClass = options.ActiveItemCssClass
		LinkCssClass = options.LinkCssClass
		DropDownMenuCssClass = options.DropDownMenuCssClass
		DisabledCssClass = options.DisabledCssClass
		SrOnlyCssClass = options.SrOnlyCssClass
		ChildLevels = options.ChildLevels
		UseDropDown = options.UseDropDown
	End Sub

	''' <summary>
	''' Устанавливает или возвращает класс стиля для контейнера элемента навигации. По умолчанию <code>navbar-nav</code>.
	''' </summary>
	Public Property CssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для пункта элемента навигации. По умолчанию <code>nav-item</code>.
	''' </summary>
	Public Property ItemCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для активного пункта элемента навигации. По умолчанию <code>active</code>.
	''' </summary>
	Public Property ActiveItemCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для ссылки. По умолчанию <code>nav-link</code>.
	''' </summary>
	Public Property LinkCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля выпадающего меню. По умолчанию <code>dropdown-menu</code>.
	''' </summary>
	Public Property DropDownMenuCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля для заблокированного пункта. По умолчанию <code>disabled</code>.
	''' </summary>
	Public Property DisabledCssClass As String

	''' <summary>
	''' Устанавливает или возвращает класс стиля элементам для экранного браузера. По умолчанию <code>sr-only</code>.
	''' </summary>
	Public Property SrOnlyCssClass As String

	''' <summary>
	''' Устанавливает или возвращает количество дочерних уровней. По умолчанию <code>0</code>.
	''' </summary>
	Public Property ChildLevels As Integer

	''' <summary>
	''' Устанавливает или возвращает разрешение использовать выпадающее меню. По умолчанию <code>False</code>.
	''' </summary>
	Public Property UseDropDown As Boolean
End Class
