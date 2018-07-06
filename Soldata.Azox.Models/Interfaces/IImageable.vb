''' <summary>
''' Предоставляет в моделях данных поддержку связанных изображений (миниатюр).
''' </summary>
Public Interface IImageable
    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор связанного изображения.
    ''' </summary>
    Property ImageId As Guid
End Interface
