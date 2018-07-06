''' <summary>
''' Предоставляет методы транслитерации.
''' </summary>
Public Class Translit
	''' <summary>
	''' Транслитерирует кириллические символы в латинские.
	''' </summary>
	''' <remarks>
	''' Транслитерация по ГОСТ Р 52535.1-2006.
	''' </remarks>
	Public Shared Function RusToLat(source As String) As String
		Dim result = source.Replace("А", "A").Replace("а", "a") _
				.Replace("Б", "B").Replace("б", "b") _
				.Replace("В", "V").Replace("в", "v") _
				.Replace("Г", "G").Replace("г", "g") _
				.Replace("Д", "D").Replace("д", "d") _
				.Replace("Е", "E").Replace("е", "e") _
				.Replace("Ё", "E").Replace("ё", "e") _
				.Replace("Ж", "Zh").Replace("ж", "zh") _
				.Replace("З", "Z").Replace("з", "z") _
				.Replace("И", "I").Replace("и", "i") _
				.Replace("Й", "I").Replace("й", "i") _
				.Replace("К", "K").Replace("к", "k") _
				.Replace("Л", "L").Replace("л", "l") _
				.Replace("М", "M").Replace("м", "m") _
				.Replace("Н", "N").Replace("н", "n") _
				.Replace("О", "O").Replace("о", "o") _
				.Replace("П", "P").Replace("п", "p") _
				.Replace("Р", "R").Replace("р", "r") _
				.Replace("С", "S").Replace("с", "s") _
				.Replace("Т", "T").Replace("т", "t") _
				.Replace("У", "U").Replace("у", "u") _
				.Replace("Ф", "F").Replace("ф", "f") _
				.Replace("Х", "Kh").Replace("х", "kh") _
				.Replace("Ц", "Ts").Replace("ц", "ts") _
				.Replace("Ч", "Ch").Replace("ч", "ch") _
				.Replace("Ш", "Sh").Replace("ш", "sh") _
				.Replace("Щ", "Shch").Replace("щ", "shch") _
				.Replace("Ъ", "").Replace("ъ", "") _
				.Replace("Ы", "Y").Replace("ы", "y") _
				.Replace("Ь", "").Replace("ь", "") _
				.Replace("Э", "E").Replace("э", "e") _
				.Replace("Ю", "Iu").Replace("ю", "iu") _
				.Replace("Я", "Ia").Replace("я", "ia")
		Return result
	End Function

	''' <summary>
	''' Заменяет русские буквы на схожие по начертанию латинские буквы.
	''' </summary>
	''' <param name="source">Исходная строка.</param>
	''' <returns>Строка, в которой латинские буквы заменяют похожие по начертанию русские буквы.</returns>
	Public Shared Function RusToPseudoLat(source As String) As String
		Return source.Replace("А", "A").Replace("а", "a") _
				.Replace("В", "B").Replace("в", "b") _
				.Replace("Е", "E").Replace("е", "e") _
				.Replace("И", "U").Replace("и", "u") _
				.Replace("К", "K").Replace("к", "k") _
				.Replace("М", "M").Replace("м", "m") _
				.Replace("Н", "H").Replace("н", "h") _
				.Replace("О", "O").Replace("о", "o") _
				.Replace("Р", "P").Replace("р", "p") _
				.Replace("С", "C").Replace("с", "c") _
				.Replace("Т", "T").Replace("т", "t") _
				.Replace("У", "Y").Replace("у", "y") _
				.Replace("Х", "X").Replace("х", "x")
	End Function
End Class
