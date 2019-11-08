'Imports System.ComponentModel.DataAnnotations
'Imports System.Data.Entity
'Imports System.Threading.Tasks
'Imports Soldata.Azox

'Public Class Picture
'	Inherits Entity

'	<Required(ErrorMessage:="Укажите название."), Display(Name:="Название")>
'	Public Property Name As String

'	<Display(Name:="Изображение")>
'	Public Property ImageId As Guid?

'	<Required(ErrorMessage:="Укажите порядок."), Display(Name:="Порядок"), UIHint("Order")>
'	Public Property Order As Integer

'	<Display(Name:="Продукт")>
'	Public Overridable Property Product As Product

'	<Display(Name:="Продукт")>
'	Public Overridable Property ProductId As Guid
'End Class

'Partial Public Class ApplicationDbContext
'	Public Property Pictures As DbSet(Of Picture)
'End Class

'Public Class PictureManager
'	Inherits EntityManager(Of Picture)

'	Public Sub New()
'		MyClass.New(New ApplicationDbContext)
'	End Sub

'	Public Sub New(context As DbContext)
'		MyBase.New(context)
'		_ImageManager = New ImageManager(context)
'	End Sub

'	Public ReadOnly Property ImageManager As ImageManager

'	Public Overrides Function Delete(entity As Picture) As ManagerResult
'		If Not IsNothing(entity.ImageId) Then
'			ImageManager.Delete(ImageManager.FindById(entity.ImageId))
'		End If
'		Return MyBase.Delete(entity)
'	End Function

'	Public Overrides Async Function DeleteAsync(entity As Picture) As Task(Of ManagerResult)
'		If Not IsNothing(entity.ImageId) Then
'			Await ImageManager.DeleteAsync(Await ImageManager.FindByIdAsync(entity.ImageId))
'		End If
'		Return Await MyBase.DeleteAsync(entity)
'	End Function

'	Protected Overrides Sub Dispose(disposing As Boolean)
'		If disposing Then
'			If _ImageManager IsNot Nothing Then
'				_ImageManager.Dispose()
'				_ImageManager = Nothing
'			End If
'		End If
'		MyBase.Dispose(disposing)
'	End Sub
'End Class

'Public Class PictureCreateViewModel
'	<HiddenInput(DisplayValue:=False)>
'	Public Property ProductId As Guid

'	<Display(Name:="Файл изображения")>
'	Public Property ImageFile As IEnumerable(Of HttpPostedFileWrapper)
'End Class
