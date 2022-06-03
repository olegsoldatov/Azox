Imports System.ComponentModel.DataAnnotations
Imports Soldata.Azox

''' <summary>
''' Иллюстрированная базовая сущность.
''' </summary>
Public MustInherit Class PictorialEntity
    Inherits Entity
    Implements IPictorial

    ''' <summary>
    ''' Устанавлиавет или возвращает идентификатор изображения.
    ''' </summary>
    <Display(Name:="Изображение")>
    Public Property ImageId As Guid? Implements IPictorial.ImageId
End Class
