Imports FCClassLibrary
Imports SpatialDimensionLibrary.Database

Public Class LicenseType
    Private c_idLicenseType As Guid
    Private c_guidJurisdiction As Guid
    Private c_strType As String
    Private c_strTypeCode As String

    Public Sub New(idLicense As Guid, guidJurisdiction As Guid, strType As String, strTypeCode As String)
        c_idLicenseType = idLicense
        c_guidJurisdiction = guidJurisdiction
        c_strType = strType
        c_strTypeCode = strTypeCode
    End Sub

    Public Property License_Type_ID() As Guid
        Get
            Return c_idLicenseType
        End Get
        Set(Value As Guid)
            c_idLicenseType = Value
        End Set
    End Property

    Public Property License_Type_Jurisdiction() As Guid
        Get
            Return c_guidJurisdiction
        End Get
        Set(Value As Guid)
            c_guidJurisdiction = Value
        End Set
    End Property

    Public Property License_Type() As String
        Get
            Return c_strType
        End Get
        Set(Value As String)
            c_strType = Value
        End Set
    End Property

    Public Property License_Type_Code() As String
        Get
            Return c_strTypeCode
        End Get
        Set(Value As String)
            c_strTypeCode = Value
        End Set
    End Property

    Public Shared Function GetLicenseTypeInstance(drLicense As DataRow) As LicenseType
        Dim idLicenseType As Guid
        Dim guidJurisdiction As Guid
        Dim strType As String = ""
        Dim strTypeCode As String = ""

        LoadDBValue(drLicense.Item(FieldName_LicenseType(enumTableField_LicenseType.LicenseTypeID)), idLicenseType)
        LoadDBValue(drLicense.Item(FieldName_LicenseType(enumTableField_LicenseType.JurisdictionID)), guidJurisdiction)
        LoadDBValue(drLicense.Item(FieldName_LicenseType(enumTableField_LicenseType.LicenseType)), strType)
        LoadDBValue(drLicense.Item(FieldName_LicenseType(enumTableField_LicenseType.LicenseTypeGroup)), strTypeCode)

        Return New LicenseType(idLicenseType, guidJurisdiction, strType, strTypeCode)
    End Function

    Public Shared Function GetFCClassLibraryLicenseTypeInstance(DBCon As DBConnection, licenseType As LicenseType, FCClassLibraryLicenseType As FCClassLibrary.LicenseType) As FCClassLibrary.LicenseType
        Dim objLookupManager As LookupManager

        objLookupManager = New LookupManagerManager()

        FCClassLibraryLicenseType.LicenseType_GUID = licenseType.License_Type_ID
        FCClassLibraryLicenseType.Jurisdiction_GUID = licenseType.License_Type_Jurisdiction
        FCClassLibraryLicenseType.Type = licenseType.License_Type
        FCClassLibraryLicenseType.TypeCode = licenseType.License_Type_Code

        Return FCClassLibraryLicenseType
    End Function

    'Public Shared Function GetLicenseTypeInstance(ByRef DBCon As DBConnection, idLicenseType As Guid, ByRef objLookupManager As LookupManager, objAuthorisation As AuthorisationController, intLicenseTypeLoadFlags As Asset.enumLoadFlags, blnIsOldCopy As Boolean, Optional strLanguage As String = "") As LicenseType
    '    Dim guidJurisdiction As Guid
    '    Dim strType As String = ""
    '    Dim strTypeCode As String = ""
    '    Dim objColumns As TableFieldList(Of enumTableField_LicenseType)
    '    Dim dtLicense As DataTable
    '    Dim strWhere As String = ""

    '    objColumns = New TableFieldList(Of enumTableField_LicenseType)
    '    objColumns.AddField(enumTableField_LicenseType.LicenseTypeID)
    '    objColumns.AddField(enumTableField_LicenseType.JurisdictionID)
    '    objColumns.AddField(enumTableField_LicenseType.LicenseType)
    '    objColumns.AddField(enumTableField_LicenseType.LicenseTypeCode)

    '    strWhere += WhereLicense_LicenseID(guidLicense)

    '    dtLicense = GetTable_License(DBCon, strWhere, objAuthorisation, LookupManagerEnum.enumAuthorisation.NONE, "", enumIsolationLevel.NOTSET, 1, objColumns)

    '    Return GetLicenseInstance(dtLicense.Rows(0))
    'End Function

End Class
