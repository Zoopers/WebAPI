Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Collections.Generic
Imports System.Threading
Imports FCClassLibrary
Imports SpatialDimensionLibrary.Database
Imports SpatialDimensionLibrary.Strings

'<Authorize>
<RoutePrefix("api/values")>
Public Class ValuesController
    Inherits ApiController

    Private DBCon As DBConnection
    Private Shared strStrings As List(Of String) = New List(Of String)({"value0", "value1", "value2"})
    Private ReadOnly licenseRepository As IRepository(Of License) = New LicenseRepository()
    Private ReadOnly licenseTypeRepository As IRepository(Of LicenseType) = New LicenseTypeRepository()
    Private ReadOnly licenseStatusRepository As IRepository(Of LicenseStatus) = New LicenseStatusRepository()

    ' GET api/values
    <Route("")>
    <HttpGet>
    Public Function GetValues() As HttpResponseMessage
        Dim licenses As IEnumerable(Of License) = Nothing
        'Dim strUsername As String
        Dim objHttpRequestMessage As HttpResponseMessage

        'strUsername = Thread.CurrentPrincipal.Identity.Name

        'If strUsername = "admin" Then
        '    licenses = repository.GetAll()

        '    If licenses Is Nothing Then
        '        Throw New HttpResponseException(HttpStatusCode.NotFound)
        '    End If

        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK, licenses)
        'Else
        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.Unauthorized)
        'End If

        licenses = licenseRepository.GetAll()

        If licenses Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK, licenses)

        Return objHttpRequestMessage
    End Function

    ' GET api/values/5
    <Route("{id:Guid}")>
    <HttpGet>
    Public Function GetValue(id As Guid) As HttpResponseMessage
        Dim license As License
        'Dim strUsername As String
        Dim objHttpRequestMessage As HttpResponseMessage

        'strUsername = Thread.CurrentPrincipal.Identity.Name

        'If strUsername = "admin" Then
        '    license = repository.GetLicense(id)

        '    If license Is Nothing Then
        '        Throw New HttpResponseException(HttpStatusCode.NotFound)
        '    End If

        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK, license)
        'Else
        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.Unauthorized)
        'End If

        license = licenseRepository.GetObject(id)

        If license Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK, license)

        Return objHttpRequestMessage
    End Function

    ' POST api/values
    <HttpPost>
    Public Function PostValue(<FromBody> value As License) As HttpResponseMessage
        Dim objHttpRequestMessage As HttpResponseMessage

        objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.Created)

        Return objHttpRequestMessage

        'strStrings.Add(value)
    End Function

    ' PUT api/values/5
    <HttpPut>
    Public Function PutValue(id As Guid, <FromBody> value As License) As HttpResponseMessage
        Dim blnUpdated As Boolean
        Dim objHttpRequestMessage As HttpResponseMessage

        'If id <> Nothing Then
        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK)
        'Else
        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.NotFound)
        'End If

        blnUpdated = licenseRepository.Update(id, value)

        If blnUpdated = False Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        Else
            objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK)
        End If

        Return objHttpRequestMessage
        'strStrings.Insert(id, value)
    End Function

    '[FromUri] int[] ids
    'id() As Guid
    ' DELETE api/values/5
    <HttpDelete>
    Public Function DeleteValue(id As Guid) As HttpResponseMessage
        Dim blnSuccessful As Boolean
        'Dim strUsername As String
        Dim objHttpRequestMessage As HttpResponseMessage

        'strUsername = Thread.CurrentPrincipal.Identity.Name

        'If strUsername = "admin" Then
        '    Try
        '        blnSuccessful = repository.Remove(id)

        '        If Not blnSuccessful Then
        '            objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.NotFound)
        '        Else
        '            objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK)
        '        End If
        '    Catch ex As Exception
        '        objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.BadRequest)
        '    End Try
        'Else
        '    objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.Unauthorized)
        'End If

        Try
            blnSuccessful = licenseRepository.Remove(id)

            If Not blnSuccessful Then
                objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.NotFound)
            Else
                objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK)
            End If
        Catch ex As Exception
            objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.BadRequest)
        End Try

        Return objHttpRequestMessage
    End Function

    <Route("types")>
    <HttpGet>
    Public Function GetLicenseTypes() As HttpResponseMessage
        Dim licenseTypes As IEnumerable(Of LicenseType) = Nothing
        Dim objHttpRequestMessage As HttpResponseMessage

        licenseTypes = licenseTypeRepository.GetAll()

        If licenseTypes Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK, licenseTypes)

        Return objHttpRequestMessage
    End Function

    <Route("statuses")>
    <HttpGet>
    Public Function GetLicenseStatuses() As HttpResponseMessage
        Dim licenseStatuses As IEnumerable(Of LicenseStatus) = Nothing
        Dim objHttpRequestMessage As HttpResponseMessage

        licenseStatuses = licenseStatusRepository.GetAll()

        If licenseStatuses Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        objHttpRequestMessage = Request.CreateResponse(HttpStatusCode.OK, licenseStatuses)

        Return objHttpRequestMessage
    End Function
End Class
