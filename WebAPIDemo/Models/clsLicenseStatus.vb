Public Class LicenseStatus

    Private c_idLicenseStatus As Guid
    Private c_strLicenseStatus As String

    Public Sub New(idLicenseStaus As Guid, strLicenseStatus As String)
        c_idLicenseStatus = idLicenseStaus
        c_strLicenseStatus = strLicenseStatus
    End Sub

    Public Property License_Status_ID() As Guid
        Get
            Return c_idLicenseStatus
        End Get
        Set(Value As Guid)
            c_idLicenseStatus = Value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return c_strLicenseStatus
        End Get
        Set(Value As String)
            c_strLicenseStatus = Value
        End Set
    End Property

End Class
