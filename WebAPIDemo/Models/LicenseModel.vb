Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database
Imports FCClassLibrary

Public Class LicenseModel

    Private c_idLicense As Guid
    Private c_strName As String
    Private c_strCode As String
    Private c_guidType As Guid
    Private c_guidStatus As Integer
    Private c_dteApplicationDate As Date
    Private c_intAgreementsCount As Integer
    Private c_lstAgreements As List(Of AgreementModel)

    Public Sub New(idLicense As Guid, strName As String, strCode As String, guidType As Guid, guidStatus As Integer, dteApplicationDate As Date, intAgreementsCount As Integer)
        Me.c_idLicense = idLicense
        Me.c_strName = strName
        Me.c_strCode = strCode
        Me.c_guidType = guidType
        Me.c_guidStatus = guidStatus
        Me.c_dteApplicationDate = dteApplicationDate
        Me.c_intAgreementsCount = intAgreementsCount
    End Sub

    Public Property ID() As Guid
        Get
            Return c_idLicense
        End Get
        Set(Value As Guid)
            c_idLicense = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return c_strName
        End Get
        Set(Value As String)
            c_strName = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return c_strCode
        End Get
        Set(Value As String)
            c_strCode = Value
        End Set
    End Property

    Public Property Type() As Guid
        Get
            Return c_guidType
        End Get
        Set(Value As Guid)
            c_guidType = Value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return c_guidStatus
        End Get
        Set(Value As Integer)
            c_guidStatus = Value
        End Set
    End Property

    Public Property Application_Date() As Date
        Get
            Return c_dteApplicationDate
        End Get
        Set(Value As Date)
            c_dteApplicationDate = Value
        End Set
    End Property

    Public Property Agreements_Count() As Integer
        Get
            Return c_intAgreementsCount
        End Get
        Set(Value As Integer)
            c_intAgreementsCount = Value
        End Set
    End Property

    Public Shared Function ConvertFCClassLibraryLicenseToAPILicense(drLicense As DataRow) As LicenseModel
        'Dim guidLicense As Guid
        'Dim strName As String = ""
        'Dim guidType As Guid
        'Dim guidStatus As Integer
        'Dim dteApplicationDate As Date = NULL_DATE
        'Dim intAgreementsCount As Integer

        'LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.ID)), guidLicense)
        'LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.Name)), strName)
        'LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.Type)), guidType)
        'LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.Status)), guidStatus)
        'LoadDBValue(drLicense.Item(FieldName_License(enumTableField_License.DateApplication)), dteApplicationDate)

        'DBCon.LookupValue(FieldName_LicenseType(enumTableField_LicenseType.LicenseType), FieldName_LicenseType(enumTableField_LicenseType.LicenseTypeID), "tblLicenseType", License.c_strLicenseType)
        'DBCon.LookupValue(FieldName_LicenseStatus(enumTableField_LicenseStatus.LicenseStatus), FieldName_LicenseStatus(enumTableField_LicenseStatus.ID), "lutLicenseStatus", License.c_strLicenseStatus_ID)

        'Return New LicenseModel(guidLicense, strName, guidType, guidStatus, dteApplicationDate)
    End Function

    Public Shared Function ConvertAPILicenseToFCClassLibraryLicense(ByVal DBCon As DBConnection, ByVal license As LicenseModel) As FCClassLibrary.License
        Dim licenseFC As FCClassLibrary.License
        Dim objLookupManager As LookupManager
        Dim objAuthorisationController As AuthorisationController
        Dim objUser As User

        objUser = New User(DBCon)

        objLookupManager = New LookupManagerManager()

        objAuthorisationController = New AuthorisationController(DBCon, objLookupManager, objUser.User_guid, "English", False)

        licenseFC = New FCClassLibrary.License(DBCon, license.License_ID, objLookupManager, objAuthorisationController, Asset.enumLoadFlags.Licenses, False)

        licenseFC.LicenseName = license.Name
        licenseFC.LicenseCode = license.Code
        licenseFC.LicenseTypeID = license.Type
        licenseFC.LicenseStatus_ID = license.Status
        licenseFC.DateApplication = license.Application_Date

        Return licenseFC
    End Function

End Class
