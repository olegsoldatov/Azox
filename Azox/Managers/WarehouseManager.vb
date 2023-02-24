Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class WarehouseManager
	Inherits EntityManager(Of Warehouse)

	Public Sub New(store As IEntityStore(Of Warehouse))
		MyBase.New(store)
	End Sub

	Public ReadOnly Property Warehouses As IQueryable(Of Warehouse)
		Get
			Return Store.Entities
		End Get
	End Property

	Public Async Function GetListAsync(query As WarehouseQuery, limit As Integer, offset As Integer) As Task(Of (Items As IEnumerable(Of Warehouse), TotalCount As Integer, PageCount As Integer))
		Dim entities = Store.Entities.AsNoTracking

		' Поиск.
		If Not String.IsNullOrEmpty(query.SearchText) Then
			Dim s = query.SearchText.Trim.ToLower.Replace("ё", "е")
			entities = entities.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(s) Or x.Title.ToLower.Replace("ё", "е").Contains(s))
		End If

		' Компания.
		If Not String.IsNullOrEmpty(query.Company) Then
			Dim c = query.Company.ToLower.Replace("ё", "е")
			entities = entities.Where(Function(x) x.Company.ToLower.Replace("ё", "е") = c)
		End If

		Dim items = Await entities.OrderBy(Function(x) x.Title).ThenBy(Function(x) x.Name).ThenBy(Function(x) x.Order).Skip(limit * offset).Take(limit).ToListAsync
		Dim totalCount = Await entities.CountAsync
		Dim pageCount = CInt(Math.Ceiling(totalCount / limit))
		Return (items, totalCount, pageCount)
	End Function
End Class

Public Class WarehouseQuery
	<Display(Name:="Поиск")>
	Public Property SearchText As String

	<Display(Name:="Компания")>
	Public Property Company As String
End Class
