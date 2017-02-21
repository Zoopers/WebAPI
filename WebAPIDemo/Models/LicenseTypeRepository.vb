Imports WebAPIDemo
Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database
Imports System.Threading

Public Class LicenseTypeRepository
    Implements IRepository(Of LicenseType)

    Private DBCon As DBConnection
    Private licenseTypes As List(Of LicenseType)
    Private Const DELETE_PROCESS As String = "DeleteProcess"

    Public Sub New()
        modDBConnectionFactory.DefaultConnectionString = ConfigurationManager.ConnectionStrings("DBCS").ConnectionString
    End Sub

    Public Function Add(obj As LicenseType) As LicenseType Implements IRepository(Of LicenseType).Add
        Throw New NotImplementedException()
    End Function

    Public Function GetAll() As IEnumerable(Of LicenseType) Implements IRepository(Of LicenseType).GetAll
        Dim dtLicenseTypes As DataTable
        Dim licenseType As LicenseType

        Try
            DBCon = NewConnection()

            dtLicenseTypes = GetTable_LicenseType(DBCon, "")

            licenseTypes = New List(Of LicenseType)

            For Each drlicenseType As DataRow In dtLicenseTypes.Rows
                licenseType = licenseType.GetLicenseTypeInstance(drlicenseType)

                licenseTypes.Add(licenseType)
            Next
        Catch ex As Exception
            Return Nothing
        End Try

        Return licenseTypes
    End Function

    Public Function GetObject(guidObject As Guid) As LicenseType Implements IRepository(Of LicenseType).GetObject
        Throw New NotImplementedException()
    End Function

    Public Function Remove(guidObject As Guid) As Boolean Implements IRepository(Of LicenseType).Remove
        Throw New NotImplementedException()
    End Function

    Public Function Update(guidObject As Guid, obj As LicenseType) As Boolean Implements IRepository(Of LicenseType).Update
        Throw New NotImplementedException()
    End Function
End Class
