Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database

Public Class AgreementModel

    Private c_idLicense As Guid
    Private c_strName As String
    Private c_guidType As Guid
    Private c_guidStatus As Integer
    Private c_dteEndDate As Date

    Public Sub New(idLicense As Guid, strName As String, guidType As Guid, guidStatus As Integer, dteEndDate As Date)
        Me.c_idLicense = idLicense
        Me.c_strName = strName
        Me.c_guidType = guidType
        Me.c_guidStatus = guidStatus
        Me.c_dteEndDate = dteEndDate

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

    Public Property End_Date() As Date
        Get
            Return c_dteEndDate
        End Get
        Set(Value As Date)
            c_dteEndDate = Value
        End Set
    End Property

End Class
