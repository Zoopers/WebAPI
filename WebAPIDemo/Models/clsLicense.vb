Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database
Imports SpatialDimensionLibrary.Strings

Public Class License
    Private c_guidLicense As Guid
    Private c_strName As String = ""
    Private c_guidLicenseType As String
    Private c_intLicenseStatus_ID As String
    Private c_dteApplication As Date = NULL_DATE

    Private Sub New(guidLicense As Guid, strName As String, guidLicenseType As String, intLicenseStatus_ID As String, dteApplication As Date)
        c_guidLicense = guidLicense
        c_strName = strName
        c_guidLicenseType = guidLicenseType
        c_intLicenseStatus_ID = intLicenseStatus_ID
        c_dteApplication = dteApplication
        'c_test = test
    End Sub

    Public Property License_ID() As Guid
        Get
            Return c_guidLicense
        End Get
        Set(Value As Guid)
            c_guidLicense = Value
        End Set
    End Property

    Public Property License_Name() As String
        Get
            Return c_strName
        End Get
        Set(Value As String)
            c_strName = Value
        End Set
    End Property

    Public Property License_Type() As String
        Get
            Return c_guidLicenseType
        End Get
        Set(Value As String)
            c_guidLicenseType = Value
        End Set
    End Property

    Public Property License_Status() As String
        Get
            Return c_intLicenseStatus_ID
        End Get
        Set(Value As String)
            c_intLicenseStatus_ID = Value
        End Set
    End Property

    Public Shared Function GetLicenseInstance(drLicense As DataRow) As License
        Dim guidLicense As Guid
        Dim strName As String = ""
        Dim guidLicenseType As String = ""
        Dim intLicenseStatus_ID As String = ""
        Dim dteApplication As Date = NULL_DATE
        Dim test As Integer = Nothing

        LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.ID)), guidLicense)
        LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.Name)), strName)
        LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.Type)), guidLicenseType)
        LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.Status)), intLicenseStatus_ID)
        LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.DateApplication)), dteApplication)

        Return New License(guidLicense, strName, guidLicenseType, intLicenseStatus_ID, dteApplication)
    End Function

    Public Shared Function GetLicenseInstance(ByRef DBCon As DBConnection, guidLicense As Guid, ByRef objLookupManager As LookupManager, objAuthorisation As AuthorisationController, intLicenseLoadFlags As Asset.enumLoadFlags, blnIsOldCopy As Boolean, Optional strLanguage As String = "") As License
        Dim strName As String = ""
        Dim guidLicenseType As String = ""
        Dim intLicenseStatus_ID As String = ""
        Dim dteApplication As Date = NULL_DATE
        Dim test As Integer = Nothing
        Dim objColumns As TableFieldList(Of enumTableField_License)
        Dim dtLicense As DataTable
        Dim strWhere As String = ""

        objColumns = New TableFieldList(Of enumTableField_License)
        objColumns.AddField(enumTableField_License.ID)
        objColumns.AddField(enumTableField_License.Name)
        objColumns.AddField(enumTableField_License.Type)
        objColumns.AddField(enumTableField_License.Status)
        objColumns.AddField(enumTableField_License.DateApplication)
        objColumns.AddField(enumTableField_License.RelatedLicenseCount)

        strWhere += WhereLicense_LicenseID(guidLicense)

        dtLicense = GetTable_License(DBCon, strWhere, objAuthorisation, LookupManagerEnum.enumAuthorisation.NONE, "", enumIsolationLevel.NOTSET, 1, objColumns)

        Return GetLicenseInstance(dtLicense.Rows(0))
    End Function
End Class
