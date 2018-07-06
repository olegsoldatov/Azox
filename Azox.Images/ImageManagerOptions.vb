''' <summary>
''' Предоставляет опции для настройки диспетчера сущностей.
''' </summary>
<Obsolete("Рекомендуется использовать класс EntityImageOptions.")>
Public Class ImageManagerOptions
	''' <summary>
	''' Устанавливает или возвращает параметры миниатюры изображения. По умолчанию: ширина - 200, высота - 200, с обрезом.
	''' </summary>
	Public Property Thumbnail As New ImageUtilityArgs(200, 200, ImageTransformMode.Cut)
	''' <summary>
	''' Устанавливает или возвращает параметры малого изображения. По умолчанию: ширина - 800, высота - 300, без обреза.
	''' </summary>
	Public Property Small As New ImageUtilityArgs(800, 300)
	''' <summary>
	''' Устанавливает или возвращает параметры среднего изображения. По умолчанию: ширина - 1440, высота - 540, без обреза.
	''' </summary>
	Public Property Medium As New ImageUtilityArgs(1440, 540)
	''' <summary>
	''' Устанавливает или возвращает параметры большого изображения. По умолчанию: ширина - 1920, высота - 720, без обреза.
	''' </summary>
	Public Property Large As New ImageUtilityArgs(1920, 720)

	''' <summary>
	''' Инициализирует новый экземпляр <see cref="ImageManagerOptions"/> с параметрами по умолчанию.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Инициализирует новый экземпляр <see cref="ImageManagerOptions"/>.
	''' </summary>
	Public Sub New(thumbnail As ImageUtilityArgs)
		Me.Thumbnail = thumbnail
	End Sub

	''' <summary>
	''' Инициализирует новый экземпляр <see cref="ImageManagerOptions"/>.
	''' </summary>
	Public Sub New(thumbnail As ImageUtilityArgs, small As ImageUtilityArgs)
		Me.Thumbnail = thumbnail
		Me.Small = small
	End Sub

	''' <summary>
	''' Инициализирует новый экземпляр <see cref="ImageManagerOptions"/>.
	''' </summary>
	Public Sub New(thumbnail As ImageUtilityArgs, small As ImageUtilityArgs, medium As ImageUtilityArgs)
		Me.Thumbnail = thumbnail
		Me.Small = small
		Me.Medium = medium
	End Sub

	''' <summary>
	''' Инициализирует новый экземпляр <see cref="ImageManagerOptions"/>.
	''' </summary>
	Public Sub New(thumbnail As ImageUtilityArgs, small As ImageUtilityArgs, medium As ImageUtilityArgs, large As ImageUtilityArgs)
		Me.Thumbnail = thumbnail
		Me.Small = small
		Me.Medium = medium
		Me.Large = large
	End Sub
End Class
