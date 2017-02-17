Public Class Authentication

    Public Shared Function Login(username As String, password As String) As Boolean
        Return username = "admin" AndAlso password = "customer"
    End Function

End Class
