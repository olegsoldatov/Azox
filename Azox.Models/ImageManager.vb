Imports System.Data.Entity

''' <summary>
''' Предоставляет API для управления изображеними.
''' </summary>
''' <typeparam name="TImage">Тип изображения.</typeparam>
Public Class ImageManager(Of TImage As {Class, IImage})
	Inherits ImageManager(Of TImage, Guid)

	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="ImageManager(Of TImage)"/>.
	''' </summary>
	''' <param name="context">Экземпляр контекста данных.</param>
	Public Sub New(context As DbContext)
		MyBase.New(context)
	End Sub
End Class

Public Class ImageManager(Of TImage As {Class, IImage(Of TKey)}, TKey As IEquatable(Of TKey))
	Implements IDisposable

	''' <summary>
	''' Возвращает контекст данных.
	''' </summary>
	''' <value>
	''' Контекст данных.
	''' </value>
	Protected Friend ReadOnly Property Context As DbContext

	''' <summary>
	''' Возвращает перечисление изображений с возможностью расчета запроса.
	''' </summary>
	Public ReadOnly Property Images As IQueryable(Of TImage)

	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="ImageManager(Of TImage, TKey)"/>.
	''' </summary>
	''' <param name="context">Экземпляр контекста данных.</param>
	Public Sub New(context As DbContext)
		_Context = context
		_Images = context.Set(Of TImage)
	End Sub

	''' <summary>
	''' Находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	Public Overridable Function FindById(id As Guid) As TImage
		Return Context.Set(Of TImage).Find(id)
	End Function

	''' <summary>
	''' Находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	Public Overridable Async Function FindByIdAsync(id As Guid) As Task(Of TImage)
		Return Await Context.Set(Of TImage).FindAsync(id)
	End Function

	''' <summary>
	''' Добавляет экземпляр сущности в источник данных.
	''' </summary>
	''' <param name="image">Экземпляр сущности, добавляемый в источник данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Function Create(image As TImage) As ManagerResult
		If IsNothing(image) Then Throw New ArgumentNullException(NameOf(Entity))
		Context.Set(Of TImage).Add(image)
		Context.SaveChanges()
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Добавляет экземпляр сущности в источник данных.
	''' </summary>
	''' <param name="image">Экземпляр сущности, добавляемый в источник данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function CreateAsync(image As TImage) As Task(Of ManagerResult)
		If IsNothing(image) Then Throw New ArgumentNullException(NameOf(image))
		Context.Set(Of TImage).Add(image)
		Await Context.SaveChangesAsync()
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Обновляет сущность в источнике данных.
	''' </summary>
	''' <param name="image">Экземпляр сущности, обновляемый в источнике данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Function Update(image As TImage) As ManagerResult
		If IsNothing(image) Then Throw New ArgumentNullException(NameOf(image))
		Context.Entry(image).State = EntityState.Modified
		Context.SaveChanges()
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Обновляет сущность в источнике данных.
	''' </summary>
	''' <param name="image">Экземпляр сущности, обновляемый в источнике данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function UpdateAsync(image As TImage) As Task(Of ManagerResult)
		If IsNothing(image) Then Throw New ArgumentNullException(NameOf(image))
		Context.Entry(image).State = EntityState.Modified
		Await Context.SaveChangesAsync()
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет экземпляр сущности из источника данных.
	''' </summary>
	''' <param name="image">Экземпляр сущности, удаляемый из источника данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Function Delete(image As TImage) As ManagerResult
		If IsNothing(image) Then Throw New ArgumentNullException(NameOf(image))
		Context.Set(Of TImage).Remove(image)
		Context.SaveChanges()
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет экземпляр сущности из источника данных.
	''' </summary>
	''' <param name="image">Экземпляр сущности, удаляемый из источника данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function DeleteAsync(image As TImage) As Task(Of ManagerResult)
		If IsNothing(image) Then Throw New ArgumentNullException(NameOf(image))
		Context.Set(Of TImage).Remove(image)
		Await Context.SaveChangesAsync()
		Return ManagerResult.Success
	End Function

#Region "IDisposable Support"
	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				_Context.Dispose()
			End If
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
