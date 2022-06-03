Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Image
    Inherits Entity

    Public Property ContentType As String

    <Column(TypeName:="Image")>
    Public Property Original As Byte()

    <Column(TypeName:="Image")>
    Public Property Thumbnail As Byte()

    <Column(TypeName:="Image")>
    Public Property Large As Byte()

    <Column(TypeName:="Image")>
    Public Property Medium As Byte()

    <Column(TypeName:="Image")>
    Public Property Small As Byte()
End Class

Partial Public Class ApplicationDbContext
    Public Property Images As DbSet(Of Image)
End Class
