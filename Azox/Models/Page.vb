Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Page
	Inherits Entity

    <Required(ErrorMessage:="Укажите заголовок.")>
    <MaxLength(200, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Заголовок")>
    Public Property Heading As String

    <AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String

    <MaxLength(200, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Название")>
    Public Property Title As String

    <DataType(DataType.MultilineText)>
    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Описание")>
    Public Property Description As String
End Class

Public Class AboutPage
    Inherits Page
End Class

Public Class ContactsPage
    Inherits Page
End Class

Public Class PolicyPage
    Inherits Page
End Class

Public Class DeliveryPage
    Inherits Page
End Class

Public Class TermsPage
    Inherits Page
End Class

Partial Public Class ApplicationDbContext
	Public Property Pages As DbSet(Of Page)
End Class
