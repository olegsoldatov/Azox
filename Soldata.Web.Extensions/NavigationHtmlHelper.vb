Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для визуализации элементов навигации.
''' </summary>
Public Module NavigationHtmlHelper
	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, New NavigationOptions, Nothing, excludedUrls))
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, htmlAttributes As Object, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, New NavigationOptions, htmlAttributes, excludedUrls))
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="childLevels">Количество дочерних уровней.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, childLevels As Integer, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, New NavigationOptions With {.ChildLevels = childLevels}, Nothing, excludedUrls))
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="childLevels">Количество дочерних уровней.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, childLevels As Integer, htmlAttributes As Object, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, New NavigationOptions With {.ChildLevels = childLevels}, htmlAttributes, excludedUrls))
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="childLevels">Количество дочерних уровней.</param>
	''' <param name="useDropDown">Разрешает использовать выпадающее меню.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, childLevels As Integer, useDropDown As Boolean, htmlAttributes As Object, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, New NavigationOptions With {.ChildLevels = childLevels, .UseDropDown = useDropDown}, htmlAttributes, excludedUrls))
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="options">Экземпляр класса параметров навигации.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, options As NavigationOptions, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, options, Nothing, excludedUrls))
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="options">Экземпляр класса параметров навигации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, options As NavigationOptions, htmlAttributes As Object, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, options, htmlAttributes, excludedUrls))
	End Function

	Private Function BuildUl(node As SiteMapNode, options As NavigationOptions, htmlAttributes As Object, ParamArray excludedUrls() As String) As String
		Dim ulTag As New TagBuilder("ul")

		With ulTag
			.MergeAttributes(New RouteValueDictionary(htmlAttributes), True)

			If IsNothing(node.ParentNode) Then
				.AddCssClass(options.CssClass)
				.InnerHtml += BuildLi(node, False, options, excludedUrls)
			ElseIf options.UseDropDown Then
				.AddCssClass(options.DropDownMenuCssClass)
			End If

			For Each item In node.ChildNodes
				.InnerHtml += BuildLi(item, True, options, excludedUrls)
			Next
		End With

		Return ulTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildLi(node As SiteMapNode, isChild As Boolean, options As NavigationOptions, ParamArray excludedUrls() As String) As String
		If excludedUrls.Contains(node.Url) Then Return String.Empty

		Dim liTag As New TagBuilder("li")

		With liTag
			.AddCssClass(options.ItemCssClass)
			If isChild And options.ChildLevels > 0 And node.HasChildNodes Then
				If options.UseDropDown Then .AddCssClass("dropdown")
				.InnerHtml += BuildA(node, options)
				.InnerHtml += BuildUl(node, New NavigationOptions(options) With {.ChildLevels = options.ChildLevels - 1}, New With {.role = "menu"}, excludedUrls)
			Else
				.InnerHtml += BuildA(node, New NavigationOptions(options) With {.UseDropDown = False})
			End If

			If node.Equals(SiteMap.CurrentNode) Then .AddCssClass("active")
		End With

		Return liTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildA(node As SiteMapNode, options As NavigationOptions) As String
		Dim aTag As New TagBuilder("a")

		With aTag
			.AddCssClass("nav-link")
			.Attributes.Add("href", If(String.IsNullOrEmpty(node.Url), "#", If(Not String.IsNullOrEmpty(node.Url) AndAlso VirtualPathUtility.IsAppRelative(node.Url), VirtualPathUtility.ToAbsolute(node.Url), node.Url)))
			If options.UseDropDown Then
				.AddCssClass("dropdown-toggle")
				.Attributes.Add("role", "button")
				.Attributes.Add("data-toggle", "dropdown")
				.Attributes.Add("aria-haspopup", "true")
				.Attributes.Add("aria-expanded", "false")
			End If
			.InnerHtml += node.Title
			'Если узел имеет описание, то добавим к тэгу аттрибут title.
			If Not String.IsNullOrEmpty(node.Description) Then .Attributes.Add("title", node.Description)
		End With

		Return aTag.ToString(TagRenderMode.Normal)
	End Function
End Module
