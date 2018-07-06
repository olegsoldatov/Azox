''' <summary>
''' Предоставляет поддержку изображений в сущности.
''' </summary>
Public Interface IEntityImage
    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор изображения, связанного с сущностью.
    ''' </summary>
    ''' <returns>Уникальный идентификатор.</returns>
    Property Thumbnail As Guid

    ''' <summary>
    ''' Устанавливает или возвращает загруженный на сервер файл изображения.
    ''' </summary>
    ''' <returns>HTTP-объект, предоставляющий доступ к файлу изображения, загруженного на сервер клиентом.</returns>
    Property ImageFile As HttpPostedFileWrapper
End Interface
