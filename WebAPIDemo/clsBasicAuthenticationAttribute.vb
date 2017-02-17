Imports System.Web.Http.Controllers
Imports System.Web.Http.Filters
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Security.Principal

Public Class BasicAuthenticationAttribute
    Inherits AuthorizationFilterAttribute

    Public Overrides Sub OnAuthorization(actionContext As HttpActionContext)
        Dim strAuthenticationToken As String
        Dim strDecodedAuthenticationToken As String
        Dim arrUsernameAndPassword() As String
        Dim username As String
        Dim password As String

        If actionContext.Request.Headers.Authorization Is Nothing Then
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
        Else
            strAuthenticationToken = actionContext.Request.Headers.Authorization.Parameter
            strDecodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(strAuthenticationToken))
            arrUsernameAndPassword = strDecodedAuthenticationToken.Split(":")
            username = arrUsernameAndPassword(0)
            password = arrUsernameAndPassword(1)

            If Authentication.Login(username, password) Then
                Thread.CurrentPrincipal = New GenericPrincipal(New GenericIdentity(username), Nothing)
            Else
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If

        End If

    End Sub

End Class
