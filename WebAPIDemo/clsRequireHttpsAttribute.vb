Imports System.Web.Http.Controllers
Imports System.Web.Http.Filters
Imports System.Net.Http

Public Class RequireHttpsAttribute
    Inherits AuthorizationFilterAttribute
    'Implements IFilter

    Public Overrides Sub OnAuthorization(actionContext As HttpActionContext)

        If actionContext.Request.RequestUri.Scheme <> Uri.UriSchemeHttps Then
            Dim objURIBuilder As UriBuilder

            actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found)
            actionContext.Response.Content = New StringContent("<p>Use HTTPS instead of HTTP</p>", Encoding.UTF8, "text/html")

            objURIBuilder = New UriBuilder(actionContext.Request.RequestUri)
            objURIBuilder.Scheme = Uri.UriSchemeHttps
            objURIBuilder.Port = 44346

            actionContext.Response.Headers.Location = objURIBuilder.Uri
        Else
            MyBase.OnAuthorization(actionContext)
        End If

    End Sub

End Class
