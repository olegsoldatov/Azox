Imports System.ComponentModel.DataAnnotations

''' <summary>
''' Сущность приложения.
''' </summary>
Public Class ApplicationEntity
    Inherits Entity
    Implements IDatedEntity

    <DataType(DataType.Date)>
    <Display(Name:="Дата изменения")>
    Public Property LastUpdateDate As Date = Date.Now Implements IDatedEntity.LastUpdateDate
End Class
