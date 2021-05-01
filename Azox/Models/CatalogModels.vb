Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class CategoryFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String
End Class

Namespace Catalog.Models
	Public Class Warehouse
		Public Property Id As Guid
		Public Property Name As String
		Public Property Title As String
	End Class

	Public Class Product
		Public Property Id As Guid
		Public Property Sku As String
		Public Property Title As String
		Public Property Brand As String
		Public Property Model As String
		Public Property CategoryId As Guid?
		Public Property CreateDate As Date
		Public Property LastUpdateDate As Date
	End Class

	Public Class Offer
		Public Property Price As Decimal
		Public Property OldPrice As Decimal?
		Public Property ProductId As Guid
		Public Property LastUpdateDate As Date
	End Class

	Public Class Attribute
		Public Property Id As Guid
		Public Property Name As String
		Public Property Order As Integer
	End Class

	Public Class Parameter
		Public Property Value As String
		Public Property AttributeId As Guid
		Public Property ProductId As Guid
	End Class

	Public Class Picture
		Public Property ImageUrl As String
		Public Property Order As Integer
		Public Property ProductId As Guid
	End Class
End Namespace
