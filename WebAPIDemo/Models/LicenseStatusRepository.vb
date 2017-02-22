Imports WebAPIDemo
Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database
Imports System.Threading

Public Class LicenseStatusRepository
    Implements IRepository(Of LicenseStatus)

    Private DBCon As DBConnection
    Private licenseStatuses As List(Of LicenseStatus)
    Private Const DELETE_PROCESS As String = "DeleteProcess"

    Public Sub New()
        modDBConnectionFactory.DefaultConnectionString = ConfigurationManager.ConnectionStrings("DBCS").ConnectionString
    End Sub

    Public Function Add(obj As LicenseStatus) As LicenseStatus Implements IRepository(Of LicenseStatus).Add
        'Dim dtLicenseTypes As DataTable
        'Dim objColumns As TableFieldList(Of enumTableField_LicenseStatus)
        'Dim licenseType As LicenseType

        'objColumns = New TableFieldList(Of enumTableField_LicenseStatus)
        'objColumns.AddField(enumTableField_LicenseStatus.ID)
        'objColumns.AddField(enumTableField_LicenseStatus.LicenseStatus)

        'Try
        '    DBCon = NewConnection()

        '    dtLicenseTypes = GetTable_LicenseType(DBCon, "")

        '    licenseStatuses = New List(Of LicenseType)

        '    For Each drlicenseType As DataRow In dtLicenseTypes.Rows
        '        licenseType = LicenseType.GetLicenseTypeInstance(drlicenseType)

        '        licenseStatuses.Add(licenseType)
        '    Next
        'Catch ex As Exception
        '    Return Nothing
        'End Try

        'Return licenseStatuses
    End Function

    Public Function GetAll() As IEnumerable(Of LicenseStatus) Implements IRepository(Of LicenseStatus).GetAll

    End Function

    Public Function GetObject(guidObject As Guid) As LicenseStatus Implements IRepository(Of LicenseStatus).GetObject
        Throw New NotImplementedException()
    End Function

    Public Function Remove(guidObject As Guid) As Boolean Implements IRepository(Of LicenseStatus).Remove
        Throw New NotImplementedException()
    End Function

    Public Function Update(guidObject As Guid, obj As LicenseStatus) As Boolean Implements IRepository(Of LicenseStatus).Update
        Throw New NotImplementedException()
    End Function
End Class
