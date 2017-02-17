Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Collections.Generic
Imports System.Threading
Imports FCClassLibrary
Imports SpatialDimensionLibrary.Database
Imports SpatialDimensionLibrary.Strings

'<Authorize>
Public Class ValuesController
    Inherits ApiController

    Private DBCon As DBConnection
    Private strStrings As List(Of String) = New List(Of String)({"value0", "value1", "value2"})
    Private ReadOnly repository As ILicenseRepository = New LicenseRepository()

    '<BasicAuthentication>
    ' GET api/values
    <HttpGet>
    Public Function GetValues() As HttpResponseMessage
        Dim licenses As IEnumerable(Of License) = Nothing
        'Dim strUsername As String

        'strUsername = Thread.CurrentPrincipal.Identity.Name

        'If strUsername = "admin" Then
        '    licenses = repository.GetAll()

        '    If licenses Is Nothing Then
        '        Throw New HttpResponseException(HttpStatusCode.NotFound)
        '    End If

        '    Return Request.CreateResponse(HttpStatusCode.OK, licenses)
        'Else
        '    Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorised access")
        'End If

        licenses = repository.GetAll()

        If licenses Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return Request.CreateResponse(HttpStatusCode.OK, licenses)
    End Function

    ' GET api/values/5
    <HttpGet>
    Public Function GetValue(id As Guid) As HttpResponseMessage
        Dim license As License

        license = repository.GetLicense(id)

        If license Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return Request.CreateResponse(HttpStatusCode.OK, license)
    End Function

    ' POST api/values
    <HttpPost>
    Public Sub PostValue(<FromBody> value As String)
        strStrings.Add(value)
    End Sub

    ' PUT api/values/5
    <HttpPut>
    Public Sub PutValue(id As Integer, <FromBody> value As String)
        strStrings.Insert(id, value)
    End Sub

    ' DELETE api/values/5
    <HttpDelete>
    Public Function DeleteValue(id As Guid) As HttpResponseMessage
        Dim blnSuccessful As Boolean

        Try
            blnSuccessful = repository.Remove(id)

            If Not blnSuccessful Then
                Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "License not found")
            Else
                Return Request.CreateErrorResponse(HttpStatusCode.OK, "")
            End If
        Catch ex As Exception
            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex)
        End Try
    End Function
End Class
