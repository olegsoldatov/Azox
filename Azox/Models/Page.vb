﻿Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Page
	Implements IPage

	<Key>
	Public Property Id As Guid Implements IPage.Id

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String Implements IPage.Title

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String Implements IPage.Content

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<RegularExpression("\/[-\w/~%.]+", ErrorMessage:="Используется недопустимый формат.")>
	<Remote("Exists", "Pages", "Admin", AdditionalFields:="Id", ErrorMessage:="Такой ярлык уже существует.")>
	<Display(Name:="Ярлык")>
	Public Property Slug As String

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date

	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Public Property Description As String Implements IPage.Description

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String Implements IPage.Keywords
End Class

Partial Public Class ApplicationDbContext
	Public Property Pages As DbSet(Of Page)
End Class
