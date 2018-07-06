'=============================
'Последняя редакция 09.08.2014
'=============================

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("ContentCategories")>
    Public Class ContentCategory
    Public Overridable Property ContentId As Guid
    Public Overridable Property CategoryId As Guid
End Class
