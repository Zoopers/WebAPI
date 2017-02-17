Imports System.Web.Http.Controllers
Imports System.Web.Http.Filters
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Security.Principal

Public Class BasicAuthenticationAttribute
    Inherits AuthorizationFilterAttribute

    Public Overrides Sub OnAuthorization(actionContext As HttpActionContext)

        If actionContext.Request.Headers.Authorization Is Nothing Then
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
        Else
            Dim strAuthenticationToken As String = actionContext.Request.Headers.Authorization.Parameter
            Dim strDecodedAuthenticationToken As String = Encoding.UTF8.GetString(Convert.FromBase64String(strAuthenticationToken))
            Dim arrUsernameAndPassword() As String = strDecodedAuthenticationToken.Split(":")
            Dim username As String = arrUsernameAndPassword(0)
            Dim password As String = arrUsernameAndPassword(1)

            If Authentication.Login(username, password) Then
                Thread.CurrentPrincipal = New GenericPrincipal(New GenericIdentity(username), Nothing)
            Else
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If

        End If

        MyBase.OnAuthorization(actionContext)
    End Sub

End Class
