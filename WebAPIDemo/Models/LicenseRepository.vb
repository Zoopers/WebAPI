Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database

Public Class LicenseRepository
    Implements ILicenseRepository

    Private DBCon As DBConnection
    Private licenses As List(Of License)

    Public Sub New()
        modDBConnectionFactory.DefaultConnectionString = ConfigurationManager.ConnectionStrings("DBCS").ConnectionString
    End Sub

    Private Function ILicenseRepository_GetAll() As IEnumerable(Of License) Implements ILicenseRepository.GetAll
        Dim dtLicenses As DataTable
        Dim objColumns As TableFieldList(Of enumTableField_License)
        Dim license As License

        objColumns = New TableFieldList(Of enumTableField_License)
        objColumns.AddField(enumTableField_License.ID)
        objColumns.AddField(enumTableField_License.Name)
        objColumns.AddField(enumTableField_License.Type)
        objColumns.AddField(enumTableField_License.Status)
        objColumns.AddField(enumTableField_License.DateApplication)
        objColumns.AddField(enumTableField_License.RelatedLicenseCount)

        Try
            DBCon = NewConnection()

            dtLicenses = GetTable_License(DBCon, "", Nothing, LookupManagerEnum.enumAuthorisation.NONE, Nothing, enumIsolationLevel.NOTSET, NULL_NUMBER, objColumns)

            licenses = New List(Of License)

            For Each drlicense As DataRow In dtLicenses.Rows
                license = License.GetLicenseInstance(drlicense)

                licenses.Add(license)
            Next
        Catch ex As Exception
            Return Nothing
        End Try

        Return licenses
    End Function

    Private Function ILicenseRepository_GetLicense(guidLicense As Guid) As License Implements ILicenseRepository.GetLicense
        Dim license As License
        Dim objLookupManager As LookupManager

        objLookupManager = New LookupManagerManager()

        Try
            DBCon = NewConnection()

            license = License.GetLicenseInstance(DBCon, guidLicense, objLookupManager, Nothing, Asset.enumLoadFlags.Licenses, False)
        Catch ex As Exception
            Return Nothing
        End Try

        Return license
    End Function

    Private Function ILicenseRepository_Add(License As License) As License Implements ILicenseRepository.Add
        Throw New NotImplementedException()
    End Function

    Private Function ILicenseRepository_Remove(guidLicense As Guid) As Boolean Implements ILicenseRepository.Remove
        'Dim strWhere As String = ""

        'strWhere += WhereLicense_LicenseID(guidLicense)

        Try
            DBCon = NewConnection()

            'DBCon.ExecuteSQL("DELETE FROM tblLicense " + strWhere)

            'Dim objLookupManager As LookupManager

            'objLookupManager = New LookupManager()

            Dim license As FCClassLibrary.License = New FCClassLibrary.License(DBCon, guidLicense, Nothing, Nothing, Asset.enumLoadFlags.Licenses, False)

            Dim objLookupManager As LookupManager = New LookupManagerManager()

            FCClassLibrary.License.Delete(DBCon, objLookupManager, guidLicense, enumHistoryContextType.Unknown, "License Deleted", "English")

            'DBCon.DeleteDataRows("tblLicense", FieldName_License(enumTableField_License.ID), guidLicense)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ILicenseRepository_Update(license As License) As Boolean Implements ILicenseRepository.Update
        Throw New NotImplementedException()
    End Function
End Class
