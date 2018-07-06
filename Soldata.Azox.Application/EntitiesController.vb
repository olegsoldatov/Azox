Imports System.Data.Entity
Imports System.Web.Mvc
Imports Soldata.Azox.EntityFramework

''' <summary>
''' Предоставляет базовый контроллер для управления и вывода в представление сущностей.
''' </summary>
Public MustInherit Class EntitiesController(Of TContext As {DbContext, IEntityDbContext(Of TEntity)}, TEntity As {Class, IEntity})
	Inherits Controller

	Private _Db As TContext

	''' <summary>
	''' Создает новый экземпляр контроллера для вывода изображений.
	''' </summary>
	''' <param name="context">Контекст данных.</param>
	Protected Sub New(context As TContext)
		_Db = context
	End Sub

	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If (disposing) Then
			If _Db IsNot Nothing Then
				_Db.Dispose()
				_Db = Nothing
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	''' <summary>
	''' Возвращает экземпляр контекста данных.
	''' </summary>
	''' <returns>Экземпляр контекста данных.</returns>
	Public ReadOnly Property Db As TContext
		Get
			Return _Db
		End Get
	End Property

#Region "Вспомогательные методы"

	''' <summary>
	''' Проверяет ярлык сущности на совпадение. Если ярлык совпадает с существующим, то ему добавляется числовой суффикс.
	''' </summary>
	''' <param name="slug">Ярлык.</param>
	''' <param name="id">Идентификатор сущности.</param>
	''' <param name="suffix">Числовой суффикс. По умолчанию 0.</param>
	''' <returns>Строка, содержащая ярлык.</returns>
	Protected Friend Function ValidateSlug(slug As String, id As Guid, Optional suffix As Integer = 0) As String
		Dim result = If(suffix = 0, slug, String.Join("-", slug, suffix))

		If Db.Set(Of TEntity).AsNoTracking.Any(Function(m) m.Slug.Equals(result) And Not m.Id.Equals(id)) Then
			Return ValidateSlug(slug, id, suffix + 1)
		End If

		Return result
	End Function

#End Region

End Class
