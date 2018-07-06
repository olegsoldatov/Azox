''' <summary>
''' Определяет модель данных сущности.
''' </summary>
Public Interface IEntity
	Property Id As Guid
	Property Title As String
	Property Content As String
	Property Slug As String
	Property Order As Integer
	Property Draft As Boolean
	Property ImageId As Guid
End Interface

''' <summary>
''' Предоставляет базовую модель данных сущности.
''' </summary>
Public MustInherit Class Entity
	Implements IEntity

	<Key>
	<HiddenInput(DisplayValue:=False)>
	<Display(Order:=0)>
	Public Property Id As Guid = Guid.NewGuid Implements IEntity.Id

	<Required(ErrorMessage:="Укажите название.")>
	<Display(Name:="Название", Order:=10)>
	Public Property Title As String Implements IEntity.Title

	<AllowHtml>
	<UIHint("WYSIWYG")>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание", Order:=20)>
	Public Property Content As String Implements IEntity.Content

	<Display(Name:="Ярлык", Order:=30)>
	Public Property Slug As String Implements IEntity.Slug

	<UIHint("Order")>
	<Display(Name:="Порядок", Order:=40)>
	Public Property Order As Integer = 0 Implements IEntity.Order

	<UIHint("Draft")>
	<Display(Name:="Черновик", Order:=50)>
	Public Property Draft As Boolean = False Implements IEntity.Draft

	<Display(Name:="Изображение", Order:=60)>
	Public Property ImageId As Guid = Guid.Empty Implements IEntity.ImageId
End Class
