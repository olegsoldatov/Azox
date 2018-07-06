Public Interface IImageEntity(Of TImage As {Class, IImage(Of Guid)})
	Inherits IImageEntity(Of TImage, Guid, Guid)
	Property ImageId As Guid?
End Interface

Public Interface IImageEntity(Of TImage As {Class, IImage(Of TImageKey)}, TImageKey, Out TKey)
	Inherits IEntity(Of TKey)
	Property Image As TImage
End Interface
