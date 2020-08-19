Imports System.Globalization

''' <summary>
''' Связыватель модели денежного формата.
''' </summary>
''' <remarks>
''' Для нормальной работы этого связывателя необходимо установить jQuery-плагин InputMask (https://github.com/RobinHerbots/Inputmask) и настроить формат полей ввода данных.
''' </remarks>
Public Class DecimalModelBinder
    Implements IModelBinder

    Public Function BindModel(controllerContext As ControllerContext, bindingContext As ModelBindingContext) As Object Implements IModelBinder.BindModel
        Dim valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName)
        Dim modelState = New ModelState With {.Value = valueResult}
        Dim actualValue = Nothing

        Try
            actualValue = Convert.ToDecimal(valueResult.AttemptedValue, New CultureInfo(CultureInfo.CurrentCulture.Name) With {.NumberFormat = New NumberFormatInfo With {.NumberDecimalSeparator = ",", .NumberGroupSeparator = " "}})
        Catch ex As FormatException
            modelState.Errors.Add(ex)
        End Try

        bindingContext.ModelState.Add(bindingContext.ModelName, modelState)
        Return actualValue
    End Function
End Class
