'=============================
'Последняя редакция 09.08.2014
'=============================

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<ObsoleteAttribute>
<Table("Categories")>
    Public Class Category
    Inherits Category(Of ContentCategory)
    Implements ICategory
End Class

<ObsoleteAttribute>
Public Class Category(Of TContentCategory)
    Inherits Entity
    Implements ICategory

    Private _Contents As ICollection(Of TContentCategory)

    Public Sub New()
        _Contents = New HashSet(Of TContentCategory)
    End Sub

    Public Overridable ReadOnly Property Contents As ICollection(Of TContentCategory)
        Get
            Return _Contents
        End Get
    End Property
End Class
