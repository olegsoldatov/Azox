''' <summary>
''' Предоставляет в модели данных сущности поддержку упорядочивания.
''' </summary>
Public Interface IOrderable
    ''' <summary>
    ''' Устанавливает или возвращает значение порядка.
    ''' </summary>
    ''' <returns>Число <see cref="Integer" />, указывающее порядок.</returns>
    Property Order As Integer
End Interface

#Region "Устаревшие возможности"

''' <summary>
''' Предоставляет свойство <c>Order</c> для реализации в модели данных поддержку упорядочивания.
''' </summary>
''' <remarks></remarks>
<Obsolete("Необходимо использовать интерфейс Soldata.Azox.Models.IOrderable.")>
Public Interface IOrderableEntity
    Inherits IEntity

    Property Order As Integer
End Interface


#End Region
