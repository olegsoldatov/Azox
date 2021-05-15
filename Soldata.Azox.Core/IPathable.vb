''' <summary>
''' Предоставляет поддержку абсолютного пути.
''' </summary>
Public Interface IPathable
	Inherits IEntity

	''' <summary>
	''' Абсолютный путь.
	''' </summary>
	Property AbsolutePath As String
End Interface
