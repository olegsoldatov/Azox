@Imports System.Reflection

@ModelType ProductAvailability

@Model.GetType.GetField(Model.ToString).GetCustomAttribute(Of DisplayAttribute).Name
