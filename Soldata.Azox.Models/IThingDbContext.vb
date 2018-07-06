Imports System.Data.Entity

''' <summary>
''' Определяет контекст данных с поддержкой сущностей.
''' </summary>
Public Interface IThingDbContext
	Inherits IDisposable

	Property Things As DbSet(Of Thing)
End Interface
