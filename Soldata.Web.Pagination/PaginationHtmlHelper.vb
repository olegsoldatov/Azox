Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для визуализации элементов постраничной навигации.
''' </summary>
Public Module PaginationHtmlHelper
	Private _PageIndex As Integer
	Private _PageCount As Integer
	Private _StartPageIndex As Integer
	Private _ViewContext As ViewContext
	Private _pageIndexName As String = "pageIndex"

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

			'Ссылка на предыдущую страницу.
			If _PageIndex > 0 And Not String.IsNullOrEmpty(options.PreviousText) Then
				.InnerHtml += BuildPreviousLi(_PageIndex - 1, BuildASpan(options.PreviousText))
			End If

			'Ссылки на страницы.
			Dim limit = 10
			If _PageCount < limit Then limit = _PageCount

			Dim mid = CInt(Math.Floor(limit / 2))

			If _PageIndex > (_StartPageIndex + mid) And (_StartPageIndex + limit) < _PageCount Then
				_StartPageIndex += 1
			ElseIf _PageIndex < (_StartPageIndex + mid) And _StartPageIndex > 0 Then
				_StartPageIndex -= 1
			End If

			For i = _StartPageIndex To (_StartPageIndex + limit) - 1
				.InnerHtml += BuildLi(i, (i + 1).ToString(), options.ActiveCssClass)
			Next

			'Ссылка на следующую страницу.
			If _PageIndex < (_PageCount - 1) And Not String.IsNullOrEmpty(options.NextText) Then
				.InnerHtml += BuildNextLi(_PageIndex + 1, BuildASpan(options.NextText))
			End If
		End With

		Return ul.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildPreviousLi(pageIndex As Integer, text As String) As String
		Dim li As New TagBuilder("li")

		Dim routeValues As New RouteValueDictionary(_ViewContext.RouteData.Values)
		Dim queryString = _ViewContext.HttpContext.Request.QueryString
		For Each key In queryString.AllKeys
			If Not key = _pageIndexName Then
				Dim value = queryString(key)
				routeValues.Add(key, value)
			End If
		Next
		If pageIndex > 0 Then
			routeValues.Add(_pageIndexName, pageIndex)
		End If

		Dim urlHelper As New UrlHelper(_ViewContext.RequestContext)
		li.InnerHtml = BuildPreviousA(urlHelper.RouteUrl(routeValues), text)

		Return li.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildNextLi(pageIndex As Integer, text As String) As String
		Dim li As New TagBuilder("li")

		Dim routeValues As New RouteValueDictionary(_ViewContext.RouteData.Values)
		Dim queryString = _ViewContext.HttpContext.Request.QueryString
		For Each key In queryString.AllKeys
			If Not key = _pageIndexName Then
				Dim value = queryString(key)
				routeValues.Add(key, value)
			End If
		Next
		If pageIndex > 0 Then
			routeValues.Add(_pageIndexName, pageIndex)
		End If

		Dim urlHelper As New UrlHelper(_ViewContext.RequestContext)
		li.InnerHtml = BuildNextA(urlHelper.RouteUrl(routeValues), text)

		Return li.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildLi(pageIndex As Integer, text As String, activeCssClass As String) As String
		Dim li As New TagBuilder("li")

		Dim routeValues As New RouteValueDictionary(_ViewContext.RouteData.Values)
		Dim queryString = _ViewContext.HttpContext.Request.QueryString
		For Each key In queryString.AllKeys
			If Not key = _pageIndexName Then
				Dim value = queryString(key)
				routeValues.Add(key, value)
			End If
		Next
		If pageIndex > 0 Then
			routeValues.Add(_pageIndexName, pageIndex)
		End If

		With li
			If pageIndex = _PageIndex Then
				.AddCssClass(activeCssClass)
				.InnerHtml = BuildDisabledSpan(text & " " & BuildSpan("(current)"))
			Else
				Dim urlHelper As New UrlHelper(_ViewContext.RequestContext)
				.InnerHtml = BuildA(urlHelper.RouteUrl(routeValues), text)
			End If
		End With

		Return li.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildPreviousA(href As String, text As String) As String
		Dim aTag As New TagBuilder("a") With {
			.InnerHtml = text
		}
		aTag.Attributes.Add("href", href)
		aTag.Attributes.Add("area-label", "Previous")

		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildNextA(href As String, text As String) As String
		Dim aTag As New TagBuilder("a") With {
			.InnerHtml = text
		}
		aTag.Attributes.Add("href", href)
		aTag.Attributes.Add("area-label", "Next")

		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildA(href As String, text As String) As String
		Dim aTag As New TagBuilder("a") With {
			.InnerHtml = text
		}
		aTag.Attributes.Add("href", href)

		Return aTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildASpan(text As String) As String
		Dim spanTag As New TagBuilder("span")

		spanTag.Attributes.Add("aria-hidden", "true")
		spanTag.InnerHtml = text

		Return spanTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildDisabledSpan(text As String) As String
		Dim spanTag As New TagBuilder("span") With {
			.InnerHtml = text
		}

		Return spanTag.ToString(TagRenderMode.Normal)
	End Function

	Private Function BuildSpan(text As String) As String
		Dim spanTag As New TagBuilder("span")

		spanTag.AddCssClass("sr-only")
		spanTag.InnerHtml = text

		Return spanTag.ToString(TagRenderMode.Normal)
	End Function
End Module
