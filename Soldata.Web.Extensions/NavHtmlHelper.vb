Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для визуализации элементов навигации.
''' </summary>
Public Module NavHtmlHelper
	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper) As MvcHtmlString
		Return Nav(helper, SiteMap.RootNode, New NavOptions, Nothing)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="node">Экземпляр узла карты сайта.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, node As SiteMapNode) As MvcHtmlString
		Return Nav(helper, node, New NavOptions, Nothing)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="options">Параметры визуализации.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, options As NavOptions) As MvcHtmlString
		Return Nav(helper, SiteMap.RootNode, options, Nothing)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, htmlAttributes As Object) As MvcHtmlString
		Return Nav(helper, SiteMap.RootNode, New NavOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="node">Экземпляр узла карты сайта.</param>
	''' <param name="options">Параметры визуализации.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, node As SiteMapNode, options As NavOptions) As MvcHtmlString
		Return Nav(helper, node, options, Nothing)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="node">Экземпляр узла карты сайта.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, node As SiteMapNode, htmlAttributes As Object) As MvcHtmlString
		Return Nav(helper, node, New NavOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="options">Параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, options As NavOptions, htmlAttributes As Object) As MvcHtmlString
		Return Nav(helper, SiteMap.RootNode, options, htmlAttributes)
	End Function

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="node">Экземпляр узла карты сайта.</param>
	''' <param name="options">Параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension>
	Public Function Nav(helper As HtmlHelper, node As SiteMapNode, options As NavOptions, htmlAttributes As Object) As MvcHtmlString
		Dim ulTag As New TagBuilder("ul")

		With ulTag
			.AddCssClass(options.CssClass)
			.MergeAttributes(New RouteValueDictionary(htmlAttributes), True)
			.InnerHtml += RootItem(node, options)
			For Each item As SiteMapNode In node.ChildNodes
				.InnerHtml += NavItem(item, options)
			Next
		End With

		Return MvcHtmlString.Create(ulTag.ToString(TagRenderMode.Normal))
	End Function

	Private Function RootItem(node As SiteMapNode, options As NavOptions) As String
		If options.HideRoot Then
			Return String.Empty
		End If

		Dim liTag As New TagBuilder("li")

		With liTag
			.AddCssClass(options.ItemCssClass)
			.InnerHtml += RootLink(node, options)
		End With

		Return liTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function RootLink(node As SiteMapNode, options As NavOptions) As String
		Dim aTag As New TagBuilder("a")
		With aTag
			.AddCssClass(options.LinkCssClass)
			.Attributes.Add("href", Href(node.Url))
			.InnerHtml += node.Title
			If Not String.IsNullOrEmpty(node.Description) Then .Attributes.Add("title", node.Description)
			If node.Equals(SiteMap.CurrentNode) Then .AddCssClass(options.ActiveLinkCssClass)
		End With
		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function NavItem(node As SiteMapNode, options As NavOptions) As String
		Dim liTag As New TagBuilder("li")

		With liTag
			.AddCssClass(options.ItemCssClass)
			.InnerHtml += NavLink(node, options)
			If node.HasChildNodes And Not IsNothing(node.ParentNode) And Not options.HideDropDown Then
				.AddCssClass("dropdown")
				.InnerHtml += DropdownMenu(node, options)
			End If
		End With

		Return liTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function NavLink(node As SiteMapNode, options As NavOptions) As String
		Dim aTag As New TagBuilder("a")

		With aTag
			.AddCssClass(options.LinkCssClass)
			.Attributes.Add("href", Href(node.Url))
			.InnerHtml += node.Title
			If Not String.IsNullOrEmpty(node.Description) Then .Attributes.Add("title", node.Description)
			If node.Equals(SiteMap.CurrentNode) Then .AddCssClass(options.ActiveLinkCssClass)
			If node.HasChildNodes And Not IsNothing(node.ParentNode) And Not options.HideDropDown Then
				.AddCssClass("dropdown-toggle")
				.Attributes.Add("data-toggle", "dropdown")
				.Attributes.Add("role", "button")
				.Attributes.Add("aria-haspopup", "true")
				.Attributes.Add("aria-expanded", "false")
			End If
		End With

		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function DropdownMenu(node As SiteMapNode, options As NavOptions) As String
		Dim divTag As New TagBuilder("div")

		With divTag
			.AddCssClass(options.DropDownMenuCssClass)
			For Each item As SiteMapNode In node.ChildNodes
				.InnerHtml += DropdownItem(item, options)
			Next
		End With

		Return divTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function DropdownItem(node As SiteMapNode, options As NavOptions) As String
		Dim aTag As New TagBuilder("a")

		With aTag
			.AddCssClass(options.DropDownItemCssClass)
			.Attributes.Add("href", Href(node.Url))
			.InnerHtml += node.Title
			If Not String.IsNullOrEmpty(node.Description) Then .Attributes.Add("title", node.Description)
			If node.Equals(SiteMap.CurrentNode) Then .AddCssClass(options.ActiveLinkCssClass)
		End With

		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function Href(url As String) As String
		Return If(String.IsNullOrEmpty(url), "#", If(VirtualPathUtility.IsAppRelative(url), VirtualPathUtility.ToAbsolute(url), url))
	End Function
End Module
