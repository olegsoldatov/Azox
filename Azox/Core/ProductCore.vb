Imports Soldata.Azox

''' <summary>
''' Минимальный интерфейс продукта.
''' </summary>
Public Interface IProduct
	Inherits IEntity
	Property Sku As String
	Property SkuRaw As String
End Interface
