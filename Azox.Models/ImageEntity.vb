Imports System.ComponentModel.DataAnnotations

Public Class ImageEntity
	Inherits ImageEntity(Of Image)
	Implements IEntity, IEntity(Of Guid), IImageEntity(Of Image), IImageEntity(Of Image, Guid, Guid)
End Class

Public Class ImageEntity(Of TImage As {Class, IImage})
	Inherits ImageEntity(Of TImage, Guid, Guid)
	Implements IEntity, IEntity(Of Guid), IImageEntity(Of TImage), IImageEntity(Of TImage, Guid, Guid)

	Public Sub New()
		Id = Guid.NewGuid
	End Sub

	<Display(Name:="Изображение", Order:=41)>
	Public Overridable Property ImageId As Guid? Implements IImageEntity(Of TImage).ImageId
End Class

Public Class ImageEntity(Of TImage As {Class, IImage(Of TImageKey)}, TImageKey, TKey)
	Inherits Entity(Of TKey)
	Implements IEntity(Of TKey), IImageEntity(Of TImage, TImageKey, TKey)

	<DataType(DataType.ImageUrl)>
	<Display(Name:="Изображение", Order:=40)>
	Public Overridable Property Image As TImage Implements IImageEntity(Of TImage, TImageKey, TKey).Image
End Class
