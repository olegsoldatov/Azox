Imports System.ComponentModel.DataAnnotations
Imports Soldata.Azox

''' <summary>
''' Сущность.
''' </summary>
Public MustInherit Class Entity
    Implements IEntity

    ''' <summary>
    ''' Устанавливает или возвращает идентификатор.
    ''' </summary>
    <Key>
    <HiddenInput(DisplayValue:=False)>
    Public Overridable Property Id As Guid = Guid.NewGuid Implements IEntity.Id
End Class
