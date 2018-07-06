''' <summary>
''' Базовая сущность.
''' </summary>
Public MustInherit Class Entity
    Implements IEntity

    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор.
    ''' </summary>
    <Key>
    <HiddenInput(DisplayValue:=False)>
    Public Property Id As Guid Implements IEntity.Id

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    <Required(ErrorMessage:="Укажите название.")>
    <Display(Name:="Название", Order:=0)>
    Public Property Title As String Implements IEntity.Title
End Class
