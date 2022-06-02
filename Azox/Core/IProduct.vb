Imports Soldata.Azox

''' <summary>
''' Минимальный интерфейс товара.
''' </summary>
Public Interface IProduct
    Inherits IEntity

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    Property Title As String

    ''' <summary>
    ''' Устанавливает или возвращает содержание.
    ''' </summary>
    Property Content As String

    ''' <summary>
    ''' Устанавливает или возвращает артикул.
    ''' </summary>
    Property Sku As String

    ''' <summary>
    ''' Устанавливает или возвращает поставщика.
    ''' </summary>
    Property Vendor As String

    ''' <summary>
    ''' Устанавливает или возвращает имя бренда.
    ''' </summary>
    Property BrandName As String

    ''' <summary>
    ''' Устанавливает или возвращает имя модели.
    ''' </summary>
    Property ModelName As String

    ''' <summary>
    ''' Устанавливает или возвращает имя категории.
    ''' </summary>
    Property CategoryName As String

    ''' <summary>
    ''' Устанавливает или возвращает цену.
    ''' </summary>
    Property Price As Decimal

    ''' <summary>
    ''' Устанавливает или возвращает количество.
    ''' </summary>
    Property Quantity As Integer

    ''' <summary>
    ''' Устанавливает или возвращает отметку "Распродажа".
    ''' </summary>
    Property IsSale As Boolean

    ''' <summary>
    ''' Устанавливает или возвращает отметку "Популярный товар".
    ''' </summary>
    Property IsPopular As Boolean

    ''' <summary>
    ''' Устанавливает или возвращает идентификатор бренда.
    ''' </summary>
    Property BrandId As Guid?
End Interface
