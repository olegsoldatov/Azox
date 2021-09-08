Imports System.Runtime.CompilerServices
Imports System.Web.Mvc
Imports System.Web.Routing
Imports Soldata.Web.Extensions

''' <summary>
''' Предоставляет методы расширения визаулизации заголовка старницы.
''' </summary>
Public Module TitleHtmlHelper
	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper) As MvcHtmlString
		Return Title(helper, helper.ViewBag.Title, String.Empty, String.Empty, String.Empty, Nothing)
	End Function

	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper, htmlAttributes As Object) As MvcHtmlString
		Return Title(helper, helper.ViewBag.Title, String.Empty, String.Empty, String.Empty, htmlAttributes)
	End Function

	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="text">Текст заголовка.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper, text As String) As MvcHtmlString
		Return Title(helper, text, String.Empty, String.Empty, String.Empty, Nothing)
	End Function

	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="nominativ">Единица измерения в именительном падеже.</param>
	''' <param name="genetiv">Единица измерения в родительном падеже.</param>
	''' <param name="plural">Единица измерения во множественном числе.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper, nominativ As String, genetiv As String, plural As String) As MvcHtmlString
		Return Title(helper, helper.ViewBag.Title, nominativ, genetiv, plural, Nothing)
	End Function

	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="text">Текст заголовка.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper, text As String, htmlAttributes As Object) As MvcHtmlString
		Return Title(helper, text, String.Empty, String.Empty, String.Empty, htmlAttributes)
	End Function

	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="nominativ">Единица измерения в именительном падеже.</param>
	''' <param name="genetiv">Единица измерения в родительном падеже.</param>
	''' <param name="plural">Единица измерения во множественном числе.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper, nominativ As String, genetiv As String, plural As String, htmlAttributes As Object) As MvcHtmlString
		Return Title(helper, helper.ViewBag.Title, nominativ, genetiv, plural, htmlAttributes)
	End Function

	''' <summary>
	''' Заголовок страницы.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="text">Текст заголовка.</param>
	''' <param name="nominativ">Единица измерения в именительном падеже.</param>
	''' <param name="genetiv">Единица измерения в родительном падеже.</param>
	''' <param name="plural">Единица измерения во множественном числе.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	<Extension()>
	Public Function Title(helper As HtmlHelper, text As String, nominativ As String, genetiv As String, plural As String, htmlAttributes As Object) As MvcHtmlString
		Dim h1 As New TagBuilder("h1") With {.InnerHtml = text}

		h1.MergeAttributes(New RouteValueDictionary(htmlAttributes))

		Dim totalCount As Integer
		If Integer.TryParse(helper.ViewBag.TotalCount, totalCount) Then
			h1.InnerHtml += " " & New TagBuilder("sup") With {.InnerHtml = totalCount.ToString(nominativ, genetiv, plural)}.ToString(TagRenderMode.Normal)
		End If

		Return MvcHtmlString.Create(h1.ToString(TagRenderMode.Normal))
	End Function
End Module
