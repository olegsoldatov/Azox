Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class BrandValidator
    Implements IEntityValidator(Of Brand)

    Private Property Manager As BrandManager

    Public Sub New(manager As BrandManager)
        If manager Is Nothing Then
            Throw New ArgumentNullException(NameOf(manager))
        End If

        Me.Manager = manager
    End Sub

    Public Async Function ValidateAsync(brand As Brand) As Task(Of EntityResult) Implements IEntityValidator(Of Brand).ValidateAsync
        If brand Is Nothing Then
            Throw New ArgumentNullException(NameOf(brand))
        End If

        Dim errors As New List(Of String)
        Await ValidateTitle(brand, errors)
        If errors.Count > 0 Then
            Return EntityResult.Failed(errors.ToArray)
        End If

        Return EntityResult.Success
    End Function

    Private Async Function ValidateTitle(brand As Brand, errors As List(Of String)) As Task
        Dim exists = Await Manager.ExistsAsync(brand)
        If exists Then
            errors.Add(My.Resources.BrandExists)
        End If
    End Function
End Class
