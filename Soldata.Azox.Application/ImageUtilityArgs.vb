Imports System.Drawing

''' <summary>
''' Предоставляет аргументы для методов класса <see cref="ImageUtility" />.
''' </summary>
Public Class ImageUtilityArgs
    ''' <summary>
    ''' Ширина изображения.
    ''' </summary>
    Property Width As Integer
    ''' <summary>
    ''' Высота изображения.
    ''' </summary>
    Property Height As Integer
    ''' <summary>
    ''' Тип преобразования изображения.
    ''' </summary>
    Property TransformMode As ImageTransformMode
    ''' <summary>
    ''' Цвет фона.
    ''' </summary>
    Property BackgroundColor As Color
    ''' <summary>
    ''' Инициализирует экземпляр класса <see cref="ImageUtilityArgs" />.
    ''' </summary>
    ''' <param name="width">Ширина изображения.</param>
    ''' <param name="height">Высота изображения.</param>
    ''' <param name="transformMode">Тип преобразования изображения.</param>
    ''' <param name="backgroundColor">Цвет фона.</param>
    Sub New(width As Integer, height As Integer, transformMode As ImageTransformMode, backgroundColor As Color)
        _Width = width
        _Height = height
        _TransformMode = transformMode
        _BackgroundColor = backgroundColor
    End Sub
    ''' <summary>
    ''' Инициализирует экземпляр класса <see cref="ImageUtilityArgs" />.
    ''' </summary>
    ''' <param name="width">Ширина изображения.</param>
    ''' <param name="height">Высота изображения.</param>
    ''' <param name="transformMode">Тип преобразования изображения.</param>
    Sub New(width As Integer, height As Integer, transformMode As ImageTransformMode)
        Me.New(width, height, transformMode, Color.White)
    End Sub
    ''' <summary>
    ''' Инициализирует экземпляр класса <see cref="ImageUtilityArgs" />.
    ''' </summary>
    ''' <param name="width">Ширина изображения.</param>
    ''' <param name="height">Высота изображения.</param>
    Sub New(width As Integer, height As Integer)
        Me.New(width, height, ImageTransformMode.Default, Color.White)
    End Sub
End Class

''' <summary>
''' Тип преобразования изображения.
''' </summary>
Public Enum ImageTransformMode
    ''' <summary>
    ''' По умолчанию. Изображение трансформируется только по ширине; высота трансформируется пропорционально.
    ''' </summary>
    [Default] = 0
    ''' <summary>
    ''' Изображение вписывается шириной и высотой в заданный размер, не изменяя пропорций.
    ''' </summary>
    Fill = 1
    ''' <summary>
    ''' Изображение вписывается шириной и высотой в заданный размер, не изменяя пропорций, но заполняя пространство цветом.
    ''' </summary>
    FillWithBackground = 2
    ''' <summary>
    ''' Изображение вписывается в заданный размер, подрезая выходы за края.
    ''' </summary>
    Cut = 3
End Enum
