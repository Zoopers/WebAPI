Public Class Authentication

    Public Shared Function Login(username As String, password As String) As Boolean
        Return username = "admin" AndAlso password = "customer"

        'If username = "admin" AndAlso password = "customer" Then

        'End If
    End Function

End Class
