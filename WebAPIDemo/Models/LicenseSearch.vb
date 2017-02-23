Imports FCClassLibrary

Public Class LicenseSearch

    Private c_strName As String
    Private c_strCode As String
    Private c_guidType As Guid
    Private c_guidStatus As Integer
    Private c_dteApplicationDateFrom As Date
    Private c_dteApplicationDateTo As Date
    Private c_objLicenseSearch = New SearchLicense()
    Private c_objLicenseSearchPart = New SearchPartLicense()

    Public Sub New()
        Me.c_strName = Nothing
        Me.c_strCode = Nothing
        Me.c_guidType = Nothing
        Me.c_guidStatus = Nothing
        Me.c_dteApplicationDateFrom = Nothing
        Me.c_dteApplicationDateTo = Nothing
    End Sub

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

    Public Property Application_Date_From() As Date
        Get
            Return c_dteApplicationDateFrom
        End Get
        Set(Value As Date)
            c_dteApplicationDateFrom = Value
        End Set
    End Property

    Public Property Application_Date_To() As Date
        Get
            Return c_dteApplicationDateTo
        End Get
        Set(Value As Date)
            c_dteApplicationDateTo = Value
        End Set
    End Property

    Public Function SearchQueryString()
        Dim dtActions As DataTable
        Dim objActionColumns As TableFieldList(Of enumTableField_License)

        If txtActionDateSelected.Text <> "" Then
            c_objActionSearch = New SearchAction()
            objSearchPart = New SearchPartAction()

            objSearchPart.AddParameterGuid(SearchPartAction.enumSearchParameter_Guid.ActionType, StringToGUID(cmbActionTypes.SelectedValue))

            objSearchPart.AddParameter_ExplicitDateRange(SearchPartAction.enumSearchParameter_Date.DueDate, calActionDue.SelectedDate, SessionManager.Item(GS_DATE_FORMAT), ISearchPart.enumSearchDateOperator.Minimum, True)
            objSearchPart.AddParameter_ExplicitDateRange(SearchPartAction.enumSearchParameter_Date.DueDate, calActionDue.SelectedDate.AddYears(1), SessionManager.Item(GS_DATE_FORMAT), ISearchPart.enumSearchDateOperator.Maximum, True)

            c_objActionSearch.AddSearchPart(objSearchPart)

            c_objActionSearch.MaximumRecordCount = 1000

            objActionColumns = New TableFieldList(Of enumTableField_License)
            objActionColumns.AddField(enumTableField_Action.ID)
            objActionColumns.AddField(enumTableField_Action.Name)
            objActionColumns.AddField(enumTableField_Action.Code)
            objActionColumns.AddField(enumTableField_Action.TypeID)
            objActionColumns.AddField(enumTableField_Action.Summary)
            objActionColumns.AddField(enumTableField_Action.DateDue)

            c_objActionSearch.FieldList = objActionColumns
            dtActions = c_objActionSearch.Search(DBCon, LookupManager, UserLanguage, enumIsolationLevel.NOTSET)

            grdActions.DataSource = dtActions
            grdActions.DataBind()
            btnSave.Enabled = True
            'ViewState.Add(Me.ClientID + S_SEARCH_ACTION, c_objActionSearch)


        'Dim strWhere As String = ""
        'Dim p As SearchLicense

        'If c_strName <> Nothing Then
        '    strWhere += WhereLicense_LicenseName(c_strName)
        'End If

        'If c_strCode <> Nothing Then
        '    If strWhere <> "" Then
        '        strWhere += " AND " + WhereLicense_LicenseCode(c_strCode)
        '    Else
        '        strWhere += WhereLicense_LicenseCode(c_strCode)
        '    End If
        'End If

        'If c_guidType <> Nothing Then
        '    If strWhere <> "" Then
        '        strWhere += " AND " + WhereLicense_LicenseTypeID(c_guidType)
        '    Else
        '        strWhere += WhereLicense_LicenseTypeID(c_guidType)
        '    End If
        'End If

        'If c_guidStatus <> Nothing Then
        '    If strWhere <> "" Then
        '        strWhere += " AND " + WhereLicense_StatusID(c_guidStatus)
        '    Else
        '        strWhere += WhereLicense_StatusID(c_guidStatus)
        '    End If
        'End If

        'If c_dteApplicationDateFrom <> Nothing Then
        '    strWhere += WhereLicense_Ap
        'End If

        'If c_dteApplicationDateTo <> Nothing Then
        '    strWhere += WhereLicense_LicenseName(c_strName)
        'End If

        'Return strWhere
    End Function

End Class
