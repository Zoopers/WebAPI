Public Class LicenseModel

    Private c_idLicense As Guid
    Private c_strName As String
    Private c_guidType As Guid
    Private c_guidStatus As Guid
    Private c_dteApplicationDate As Date
    Private c_ As Date

    Public Sub New(idLicense As Guid, strName As String, guidType As Guid, guidStatus As Guid, dteApplicationDate As Date)
        Me.c_idLicense = idLicense
        Me.c_strName = strName
        Me.c_guidType = guidType
        Me.c_guidStatus = guidStatus
        Me.c_dteApplicationDate = dteApplicationDate
    End Sub

End Class
