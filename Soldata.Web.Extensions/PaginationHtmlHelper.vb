Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для визуализации элементов постраничной навигации.
''' </summary>
Public Module PaginationHtmlHelper
	Private _PageIndex As Integer
	Private _PageCount As Integer
	Private _ViewContext As ViewContext

	''' <summary>
	''' Отображает немаркированный список постраничной навигации.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
	''' <remarks></remarks>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper) As MvcHtmlString
		Return Pagination(helper, Nothing)
	End Function

	''' <summary>
	''' Отображает немаркированный список постраничной навигации.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
	''' <remarks></remarks>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, htmlAttributes As Object) As MvcHtmlString
		Return Pagination(helper, New PaginationOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Отображает немаркированный список постраничной навигации.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="paginationOptions">Объект, предоставляющий параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
	''' <remarks></remarks>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, paginationOptions As PaginationOptions, htmlAttributes As Object) As MvcHtmlString
		Return Pagination(helper, helper.ViewBag.PageIndex, helper.ViewBag.PageCount, paginationOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Отображает немаркированный список постраничной навигации.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="pageIndex">Индекс страницы.</param>
	''' <param name="pageCount">Количество страниц.</param>
	''' <param name="paginationOptions">Объект, предоставляющий параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, pageIndex As Integer, pageCount As Integer, paginationOptions As PaginationOptions, htmlAttributes As Object) As MvcHtmlString
		If pageCount <= 1 Then Return MvcHtmlString.Empty

		_PageIndex = pageIndex
		_PageCount = pageCount
		_ViewContext = If(helper.ViewContext.IsChildAction, helper.ViewContext.ParentActionViewContext, helper.ViewContext)

		Return MvcHtmlString.Create(BuildUl(paginationOptions, htmlAttributes))
	End Function

	Private Function BuildUl(options As PaginationOptions, htmlAttributes As Object) As String
		Dim ul As New TagBuilder("ul")

		With ul
			.MergeAttributes(New RouteValueDictionary(htmlAttributes))

			' Ссылка на предыдущую страницу.
			If Not options.HidePreviousNext Then
				If _PageIndex > 0 And Not String.IsNullOrEmpty(options.PreviousText) Then
					.InnerHtml += BuildPageItem(_PageIndex - 1, options.PreviousText, PageItemState.Normal, options)
				End If
			End If

			' Начальная страница.
			.InnerHtml += BuildPageItem(0, "1", If(_PageIndex = 0, PageItemState.Active, PageItemState.Normal), options)

			Dim startIndex = If(_PageIndex - 4 < 1, 1, _PageIndex - 4)
			Dim count = If(_PageIndex + 4 >= _PageCount - 2, _PageCount - 2, _PageIndex + 4)

			' Разделитель в начале.
			If startIndex > 1 Then
				.InnerHtml += BuildPageItem(0, "…", PageItemState.Disabled, options)
			End If

			For i = startIndex To count
				.InnerHtml += BuildPageItem(i, (i + 1).ToString, If(_PageIndex = i, PageItemState.Active, PageItemState.Normal), options)
			Next

			' Разделитель в конце.
			If _PageCount - count > 2 Then
				.InnerHtml += BuildPageItem(0, "…", PageItemState.Disabled, options)
			End If

			' Последняя страница.
			.InnerHtml += BuildPageItem(_PageCount - 1, _PageCount.ToString, If(_PageIndex = _PageCount - 1, PageItemState.Active, PageItemState.Normal), options)

			' Ссылка на следующую страницу.
			If Not options.HidePreviousNext Then
				If _PageIndex < (_PageCount - 1) And Not String.IsNullOrEmpty(options.NextText) Then
					.InnerHtml += BuildPageItem(_PageIndex + 1, options.NextText, PageItemState.Normal, options)
				End If
			End If
		End With

		Return ul.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildPageItem(pageIndex As Integer, innerText As String, state As PageItemState, options As PaginationOptions) As String
		Dim routeValues As New RouteValueDictionary(_ViewContext.RouteData.Values)
		Dim queryString = _ViewContext.HttpContext.Request.QueryString

		For Each key In queryString.AllKeys
			If Not key = options.PageIndexName Then
				routeValues.Add(key, queryString(key))
			End If
		Next
		If pageIndex > 0 Then
			routeValues.Add(options.PageIndexName, pageIndex)
		End If

		Dim result As New TagBuilder("li") With {.InnerHtml = BuildPageLink(innerText, state, New UrlHelper(_ViewContext.RequestContext).RouteUrl(routeValues), options)}

		Select Case state
			Case PageItemState.Disabled
				result.AddCssClass(options.DisabledCssClass)
			Case PageItemState.Active
				result.AddCssClass(options.ActiveCssClass)
		End Select

		If Not String.IsNullOrEmpty(options.PageItemCssClass) Then
			result.AddCssClass(options.PageItemCssClass)
		End If

		Return result.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildPageLink(innerText As String, state As PageItemState, href As String, options As PaginationOptions) As String
		Dim result As TagBuilder

		Select Case state
			Case PageItemState.Disabled
				result = New TagBuilder("span") With {.InnerHtml = innerText}
			Case PageItemState.Active
				result = New TagBuilder("span") With {.InnerHtml = innerText}
				result.InnerHtml += BuildCurrent("(current)", options)
			Case Else
				result = New TagBuilder("a") With {.InnerHtml = innerText}
				result.Attributes.Add("href", href)
		End Select

		If Not String.IsNullOrEmpty(options.PageLinkCssClass) Then
			result.AddCssClass(options.PageLinkCssClass)
		End If

		Return result.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildCurrent(innerText As String, options As PaginationOptions) As String
		Dim result As New TagBuilder("span") With {.InnerHtml = innerText}

		If Not String.IsNullOrEmpty(options.SrOnlyCssClass) Then
			result.AddCssClass(options.SrOnlyCssClass)
		End If

		Return result.ToString(TagRenderMode.Normal)
	End Function
End Module

Enum PageItemState
	Normal
	Active
	Disabled
End Enum
