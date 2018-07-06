Imports System.Data.Entity

''' <summary>
''' Определяет контекст данных с поддержкой изображений.
''' </summary>
Public Interface IImageDbContext
    Inherits IDisposable

    Property Images As DbSet(Of Image)
End Interface
