Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для визуализации элементов навигации.
''' </summary>
Public Module NavigationHtmlHelper

	Private _ParentUrl As String
	Private _VirtualParentUrl As String
	Private _DividerCssClass As String

#Region " Навигация "

	''' <summary>
	''' Предоставляет разметку для немаркированного списка, отображающего навигационную структуру сайта.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="excludedUrls">Строковый массив, содержащий URL исключаемых узлов.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
	<Extension()>
	Public Function Navigation(helper As HtmlHelper, ParamArray excludedUrls() As String) As MvcHtmlString
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, True, 0, False, Nothing, excludedUrls))
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
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, True, 0, False, htmlAttributes, excludedUrls))
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
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, True, childLevels, False, Nothing, excludedUrls))
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
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, True, childLevels, False, htmlAttributes, excludedUrls))
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
		Return MvcHtmlString.Create(BuildUl(SiteMap.RootNode, True, childLevels, useDropDown, htmlAttributes, excludedUrls))
	End Function

	Private Function BuildUl(node As SiteMapNode, isRoot As Boolean, childLevels As Integer, useDropDown As Boolean, htmlAttributes As Object, ParamArray excludedUrls() As String) As String
		Dim ulTag As New TagBuilder("ul")

		With ulTag
			.MergeAttributes(New RouteValueDictionary(htmlAttributes), True)

			If isRoot Then
				.InnerHtml += BuildLi(node, False, childLevels, useDropDown, excludedUrls)
			ElseIf useDropDown Then
				.AddCssClass("dropdown-menu")
			End If

			For Each item In node.ChildNodes
				.InnerHtml += BuildLi(item, True, childLevels, useDropDown, excludedUrls)
			Next
		End With

		Return ulTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildLi(node As SiteMapNode, isChild As Boolean, childLevels As Integer, useDropDown As Boolean, ParamArray excludedUrls() As String) As String
		If excludedUrls.Contains(node.Url) Then Return String.Empty

		Dim liTag As New TagBuilder("li")

		With liTag
			If isChild And childLevels > 0 And node.HasChildNodes Then
				If useDropDown Then .AddCssClass("dropdown")
				.InnerHtml += BuildA(node, useDropDown)
				.InnerHtml += BuildUl(node, False, childLevels - 1, useDropDown, New With {.role = "menu"}, excludedUrls)
			Else
				.InnerHtml += BuildA(node, False)
			End If

			If node.Equals(SiteMap.CurrentNode) Then .AddCssClass("active")
		End With

		Return liTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildA(node As SiteMapNode, useDropDown As Boolean) As String
		Dim aTag As New TagBuilder("a")

		With aTag
			.Attributes.Add("href", If(String.IsNullOrEmpty(node.Url), "#", If(Not String.IsNullOrEmpty(node.Url) AndAlso VirtualPathUtility.IsAppRelative(node.Url), VirtualPathUtility.ToAbsolute(node.Url), node.Url)))
			If useDropDown Then
				.AddCssClass("dropdown-toggle")
				.Attributes.Add("data-toggle", "dropdown")
				.InnerHtml += BuildCaret(node.Title)
			Else
				.InnerHtml += node.Title
			End If
			'Если узел имеет описание, то добавим к тэгу аттрибут title.
			If Not String.IsNullOrEmpty(node.Description) Then .Attributes.Add("title", node.Description)
		End With

		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildCaret(title As String) As String
		Dim spanTag As New TagBuilder("span")
		spanTag.AddCssClass("caret")
		Return title & " " & spanTag.ToString(TagRenderMode.Normal)
	End Function

#End Region

#Region " Вспомогательные методы "

	Private Function BuildIcon(className As String) As TagBuilder
		Dim iTag As New TagBuilder("i")
		iTag.AddCssClass(className)
		Return iTag
	End Function

	Private Function GetLinkInnerHtml(linkText As String, virtualImageUrl As String) As String
		If String.IsNullOrEmpty(virtualImageUrl) Then
			Return linkText
		Else
			Dim imgTag As New TagBuilder("img")

			imgTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(virtualImageUrl))
			imgTag.Attributes.Add("alt", linkText)

			Return imgTag.ToString(TagRenderMode.SelfClosing)
		End If
	End Function

#End Region

End Module
