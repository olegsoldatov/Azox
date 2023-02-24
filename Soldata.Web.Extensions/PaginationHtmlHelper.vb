Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для постраничной навигации.
''' </summary>
Public Module PaginationHtmlHelper
	''' <summary>
	''' Отображает постраничную навигацию.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку постраничной навигации.</returns>
	''' <remarks></remarks>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper) As MvcHtmlString
		Return Pagination(helper, Nothing)
	End Function

	''' <summary>
	''' Отображает постраничную навигацию.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку постраничной навигации.</returns>
	''' <remarks></remarks>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, htmlAttributes As Object) As MvcHtmlString
		Return Pagination(helper, New PaginationOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Отображает постраничную навигацию.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="paginationOptions">Объект, предоставляющий параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку постраничной навигации.</returns>
	''' <remarks></remarks>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, paginationOptions As PaginationOptions, htmlAttributes As Object) As MvcHtmlString
		Return Pagination(helper, helper.ViewBag.TotalCount, helper.ViewBag.PageIndex, helper.ViewBag.PageSize, paginationOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Отображает постраничную навигацию.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="pageIndex">Индекс страницы.</param>
	''' <param name="pageCount">Количество страниц.</param>
	''' <param name="paginationOptions">Объект, предоставляющий параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку постраничной навигации.</returns>
	<Obsolete>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, pageIndex As Integer, pageCount As Integer, paginationOptions As PaginationOptions, htmlAttributes As Object) As MvcHtmlString
		Return Pagination(helper, helper.ViewBag.TotalCount, pageIndex, helper.ViewBag.PageSize, paginationOptions, htmlAttributes)
	End Function

	''' <summary>
	''' Отображает постраничную навигацию.
	''' </summary>
	''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
	''' <param name="totalCount">Общее количество элементов в списке.</param>
	''' <param name="pageIndex">Индекс страницы.</param>
	''' <param name="pageSize">Размер списка на странице.</param>
	''' <param name="paginationOptions">Объект, предоставляющий параметры визуализации.</param>
	''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
	''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку постраничной навигации.</returns>
	<Extension()>
	Public Function Pagination(helper As HtmlHelper, totalCount As Integer, pageIndex As Integer, pageSize As Integer, paginationOptions As PaginationOptions, htmlAttributes As Object) As MvcHtmlString
		If totalCount < 0 Then
			Return MvcHtmlString.Empty
		End If

		If pageIndex < 0 Then
			Return MvcHtmlString.Empty
		End If

		If pageSize < 1 Then
			Return MvcHtmlString.Empty
		End If

		Return MvcHtmlString.Create(BuildUl(If(helper.ViewContext.IsChildAction, helper.ViewContext.ParentActionViewContext, helper.ViewContext), totalCount, pageIndex, pageSize, paginationOptions, htmlAttributes))
	End Function

	Private Function BuildUl(viewContext As ViewContext, totalCount As Integer, pageIndex As Integer, pageSize As Integer, options As PaginationOptions, htmlAttributes As Object) As String
		Dim pageCount = CInt(Math.Ceiling(totalCount / pageSize))
		If pageCount <= 1 Then
			Return String.Empty
		End If

		Dim ul As New TagBuilder("ul")
		With ul
			.MergeAttributes(New RouteValueDictionary(htmlAttributes))

			' Ссылка на предыдущую страницу.
			If Not options.HidePreviousNext Then
				If pageIndex > 0 And Not String.IsNullOrEmpty(options.PreviousText) Then
					.InnerHtml += BuildPageItem(viewContext, pageIndex - 1, options.PreviousText, PageItemState.Normal, options)
				End If
			End If

			' Начальная страница.
			.InnerHtml += BuildPageItem(viewContext, 0, "1", If(pageIndex = 0, PageItemState.Active, PageItemState.Normal), options)

			Dim startIndex = If(pageIndex - 4 < 1, 1, pageIndex - 4)
			Dim count = If(pageIndex + 4 >= pageCount - 2, pageCount - 2, pageIndex + 4)

			' Разделитель в начале.
			If startIndex > 1 Then
				.InnerHtml += BuildPageItem(viewContext, 0, "…", PageItemState.Disabled, options)
			End If

			For i = startIndex To count
				.InnerHtml += BuildPageItem(viewContext, i, (i + 1).ToString, If(pageIndex = i, PageItemState.Active, PageItemState.Normal), options)
			Next

			' Разделитель в конце.
			If pageCount - count > 2 Then
				.InnerHtml += BuildPageItem(viewContext, 0, "…", PageItemState.Disabled, options)
			End If

			' Последняя страница.
			.InnerHtml += BuildPageItem(viewContext, pageCount - 1, pageCount.ToString, If(pageIndex = pageCount - 1, PageItemState.Active, PageItemState.Normal), options)

			' Ссылка на следующую страницу.
			If Not options.HidePreviousNext Then
				If pageIndex < (pageCount - 1) And Not String.IsNullOrEmpty(options.NextText) Then
					.InnerHtml += BuildPageItem(viewContext, pageIndex + 1, options.NextText, PageItemState.Normal, options)
				End If
			End If
		End With
		Return ul.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildPageItem(viewContext As ViewContext, pageIndex As Integer, innerText As String, state As PageItemState, options As PaginationOptions) As String
		Dim list = viewContext.HttpContext.Request.QueryString.ToString.Split({"&"}, StringSplitOptions.RemoveEmptyEntries).ToList
		Dim pageIndexListItem = list.SingleOrDefault(Function(x) x.Contains($"{options.PageIndexName}="))
		If Not String.IsNullOrEmpty(pageIndexListItem) Then
			list.Remove(pageIndexListItem)
		End If
		If pageIndex > 0 Then
			list.Add($"{options.PageIndexName}={pageIndex}")
		End If

		Dim queryString = String.Join("&", list)
		If queryString.Length > 0 Then
			queryString = $"?{queryString}"
		End If

		Dim result As New TagBuilder("li") With {.InnerHtml = BuildPageLink(innerText, state, $"{New UrlHelper(viewContext.RequestContext).RouteUrl(viewContext.RouteData.Values)}{queryString}", options)}

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
