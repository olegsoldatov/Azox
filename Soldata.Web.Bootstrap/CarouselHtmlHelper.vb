Imports System.Runtime.CompilerServices

Public Module CarouselHtmlHelper

    ''' <summary>
    ''' Возвращает разметку слайдера, прокручивающего слайды по принципу карусели.
    ''' </summary>
    ''' <param name="base"></param>
    ''' <param name="name">Уникальное имя элемента.</param>
    ''' <param name="items">Перечисление элементов типа <see cref="CarouselItem" />, содержащих данные о слайде.</param>
    ''' <param name="autoplay">Разрешает автоматическую инициализацию слайдера. По умолчанию - False.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function Carousel(base As HtmlHelper, name As String, items As IEnumerable(Of CarouselItem), autoplay As Boolean) As MvcHtmlString
        Return MvcHtmlString.Create(BuildSlide(name, items) & BuildScript(autoplay))
    End Function

    Private Function BuildScript(autoplay As Boolean) As String
        If Not autoplay Then Return String.Empty

        Dim scriptTag As New TagBuilder("script")

        scriptTag.InnerHtml = "$('.carousel').carousel();"

        Return scriptTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildSlide(name As String, items As IEnumerable(Of CarouselItem)) As String
        Dim divTag As New TagBuilder("div")

        With divTag
            .AddCssClass("carousel slide")
            .GenerateId(name)
            .Attributes.Add("data-ride", "carousel")
            .InnerHtml += BuildIndicators(items, name)
            .InnerHtml += BuildInner(items)
            .InnerHtml += BuildControl(name, "left", "prev")
            .InnerHtml += BuildControl(name, "right", "next")
        End With

        Return divTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildIndicators(items As IEnumerable(Of CarouselItem), target As String) As String
        Dim olTag As New TagBuilder("ol")

        With olTag
            .AddCssClass("carousel-indicators")
            Dim index As Integer = 0
            For Each item In items
                .InnerHtml += BuildIndicatorItem(index, target)
                index += 1
            Next
        End With

        Return olTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildIndicatorItem(index As Integer, target As String) As String
        Dim liTag As New TagBuilder("li")

        With liTag
            If index = 0 Then .AddCssClass("active")
            .Attributes.Add("data-slide-to", index)
            .Attributes.Add("data-target", "#" & target)
        End With

        Return liTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildInner(items As IEnumerable(Of CarouselItem)) As String
        Dim divTag As New TagBuilder("div")

        With divTag
            .AddCssClass("carousel-inner")
            Dim index As Integer = 0
            For Each item In items
                .InnerHtml += BuildItem(index, item)
                index += 1
            Next
        End With

        Return divTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildItem(index As Integer, item As CarouselItem) As String
        Dim divTag As New TagBuilder("div")

        With divTag
            If index = 0 Then .AddCssClass("active")
            .AddCssClass("item")
            .InnerHtml += BuildImage(item.ImageUrl, "Carousel item")
            .InnerHtml += BuildCaption(item.Caption)
        End With

        Return divTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildImage(src As String, alt As String) As String
        Dim imgTag As New TagBuilder("img")

        With imgTag
            .Attributes.Add("alt", alt)
            .Attributes.Add("src", src)
        End With

        Return imgTag.ToString(TagRenderMode.SelfClosing)
    End Function

    Private Function BuildCaption(text As String) As String
        If String.IsNullOrEmpty(text) Then Return String.Empty

        Dim divTag As New TagBuilder("div")

        With divTag
            .AddCssClass("carousel-caption")
            .InnerHtml = text
        End With

        Return divTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildControl(href As String, cssClass As String, dataSlide As String) As String
        Dim aTag As New TagBuilder("a") With {.InnerHtml = BuildSpan("glyphicon glyphicon-chevron-" & cssClass)}

        With aTag
            .AddCssClass(cssClass)
            .AddCssClass("carousel-control")
            .Attributes.Add("role", "button")
            .Attributes.Add("href", "#" & href)
            .Attributes.Add("data-slide", dataSlide)
        End With

        Return aTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildSpan(cssClass As String) As String
        Dim spanTag As New TagBuilder("span")

        spanTag.AddCssClass(cssClass)

        Return spanTag.ToString(TagRenderMode.Normal)
    End Function
End Module

Public Class CarouselItem
    Public ImageUrl As String
    Public Caption As String
End Class
