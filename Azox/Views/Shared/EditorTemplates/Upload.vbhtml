@Code
	If IsNothing(ViewBag.Length) Then
		ViewBag.Length = 4096
	End If
End Code
@Html.TextBox("", Nothing, New With {.type = "file", .accept = ViewBag.Accept})
<p><small>@String.Format("Размер файла не более {0} МБ.", ViewBag.Length / 1024)</small></p>


