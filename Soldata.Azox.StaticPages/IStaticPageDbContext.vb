Imports System.Data.Entity

''' <summary>
''' Определяет контекст данных с поддержкой статичных страниц.
''' </summary>
Public Interface IStaticPageDbContext
    Inherits IDisposable

    Property StaticPages As DbSet(Of StaticPage)
End Interface
