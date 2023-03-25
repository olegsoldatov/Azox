Imports System.IO
Imports System.Text

''' <summary>
''' CSV-документ.
''' </summary>
Public Class CsvDocument
    ''' <summary>
    ''' Заголовки колонок.
    ''' </summary>
    Public Property ColumnHeaders As IList(Of String)

    ''' <summary>
    ''' Строки.
    ''' </summary>
    Public Property Rows As IList(Of String())

    Public Sub New()
        ColumnHeaders = New List(Of String)
        Rows = New List(Of String())
    End Sub

    Public Shared Async Function ParseAsync(stream As Stream, encoding As Encoding, Optional separator As String = ";") As Task(Of CsvDocument)
        Dim document As New CsvDocument

        Using reader As New StreamReader(stream, encoding)
            ' Получим заголовок, являющийся первой строкой в документе.
            document.ColumnHeaders = (Await reader.ReadLineAsync).Split(separator).ToList

            ' Получим строки.
            Dim columnCount = document.ColumnHeaders.Count
            Do Until reader.EndOfStream
                Dim line = reader.ReadLine.Split(separator).ToList
                Dim diff = line.Count - columnCount

                ' Если в линии меньше ячеек, чем столбцов, то добавим пустые; Если в линии больше ячеек, чем столбцов, то удалим лишние.
                If diff < 0 Then
                    Do Until line.Count = columnCount
                        line.Add(String.Empty)
                    Loop
                ElseIf diff > 0 Then
                    line.RemoveRange(columnCount, diff)
                End If

                document.Rows.Add(line.ToArray)
            Loop
        End Using

        Return document
    End Function
End Class
