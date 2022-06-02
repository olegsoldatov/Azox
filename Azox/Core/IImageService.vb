Imports System.IO
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Interface IImageService
    Function UploadAsync(entity As IPictorial, imageFile As (InputStream As Stream, ContentType As String)) As Task
    Function DeleteAsync(entity As IPictorial) As Task
    Function DeleteRangeAsync(entities As IEnumerable(Of IPictorial)) As Task
End Interface
