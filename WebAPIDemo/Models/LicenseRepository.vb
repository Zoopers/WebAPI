Imports FCClassLibrary
Imports FCClassLibrary.Security
Imports SpatialDimensionLibrary.Database
Imports System.Threading

Public Class LicenseRepository
    Implements IRepository(Of License)

    Private DBCon As DBConnection
    Private licenses As List(Of License)
    Private Const DELETE_PROCESS As String = "DeleteProcess"

    Public Sub New()
        modDBConnectionFactory.DefaultConnectionString = ConfigurationManager.ConnectionStrings("DBCS").ConnectionString
    End Sub

    Private Function ILicenseRepository_GetAll() As IEnumerable(Of License) Implements IRepository(Of License).GetAll
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

    Private Function ILicenseRepository_GetLicense(guidLicense As Guid) As License Implements IRepository(Of License).GetObject
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

    Private Function ILicenseRepository_Add(License As License) As License Implements IRepository(Of License).Add
        Throw New NotImplementedException()
    End Function

    Private Function ILicenseRepository_Remove(guidLicenses As Guid) As Boolean Implements IRepository(Of License).Remove
        'Dim strWhere As String = ""

        'strWhere += WhereLicense_LicenseID(guidLicense)

        Try
            DeleteLicenses(guidLicenses)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ILicenseRepository_Update(guidLicense As Guid, updatedLicense As License) As Boolean Implements IRepository(Of License).Update
        Dim license As FCClassLibrary.License
        Dim objLookupManager As LookupManager

        objLookupManager = New LookupManagerManager()

        Try
            DBCon = NewConnection()

            license = New FCClassLibrary.License(DBCon, guidLicense, objLookupManager, Nothing, Asset.enumLoadFlags.Licenses, False)

            license = WebAPIDemo.License.GetFCClassLibraryLicenseInstance(DBCon, updatedLicense, license)

            license.Save(DBCon, objLookupManager, "en", "English")

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub DeleteObjectsProcess()
        'Dim objProcessMonitor As ProcessMonitor
        'Dim strThreadName As String
        'Dim thread As SpatialDimensionLibrary.Threading.SDThread
        'Dim objParameterThread As ParameterizedThreadStart
        'Dim pDicThreadParameters As IDictionary
        'Dim objRandomNumber As New Random()
        'Dim intTimerInterval As Integer

        'Dim objDeletableObject As DeletableObject
        ''Dim objAdditionalOptionsControl As IPluginUserInterface(Of Object)
        'Dim objAdditionalOptions As Object
        ''Dim lstAdditionalOptionsDynamicControls As IList(Of System.Web.UI.Control)

        '''When the additional options control has been loaded ...
        ''lstAdditionalOptionsDynamicControls = Me.DynamicControlManager.GetDynamicChildControls(Me.Page, pnlAdditionalOptionsHost.ClientID)

        ''If lstAdditionalOptionsDynamicControls.Count > 0 Then

        ''    'Get the additional options dynamic control ...
        ''    objAdditionalOptionsControl = lstAdditionalOptionsDynamicControls.Item(0)

        ''    'Contruct the additional options from the interface ...
        ''    objAdditionalOptions = objAdditionalOptionsControl.ConstructFromInterface()
        ''Else

        ''    'No additinal options ...
        ''    objAdditionalOptions = Nothing
        ''End If

        '''show correct panel
        ''pnlConfirm.Visible = False
        ''pnlProcessMonitor.Visible = True

        'strThreadName = DELETE_PROCESS & objRandomNumber.Next

        ''objProcessMonitor = New ProcessMonitor(Me.Page, strThreadName)
        ''objProcessMonitor.MaxCount = c_alstObjectID.Count
        ''objProcessMonitor.ProcessStarted = True

        ''get count message 
        'objDeletableObject = DeletableObjectFactory.NewDeletableObject(Me.DBCon, GetLookupManager(Me.Page), GetUserLanguage(Me.Page), c_intObjectType)
        ''objProcessMonitor.Message = GetObjectTypeName & " have been deleted."
        'objProcessMonitor.Message = TranslateControl("DeleteObject.aspx", "strMessage", enumControlProperty.Text, "DeleteMessage", "", GetUserLanguage(Me.Page))

        ''add parameters
        'pDicThreadParameters = New Hashtable
        'pDicThreadParameters.Add(ProcessMonitor.THREAD_PARAMETER_PROCESS_MONITOR, objProcessMonitor)
        'pDicThreadParameters.Add(ProcessMonitor.THREAD_PARAMETER_LOOKUP_MANAGER, GetLookupManager(Me.Page))
        'pDicThreadParameters.Add(ProcessMonitor.THREAD_PARAMETER_LANGUAGE, GetUserLanguage(Me.Page))
        'pDicThreadParameters.Add(THREAD_PARAMETER_OBJECT_TYPE, c_intObjectType)
        'pDicThreadParameters.Add(THREAD_PARAMETER_OBJECT_IDS, c_alstObjectID)
        'pDicThreadParameters.Add(THREAD_ADDITIONAL_OPTIONS, objAdditionalOptions)

        'objParameterThread = New ParameterizedThreadStart(AddressOf DeleteLicenses)

        ''calculate web timer interval 
        ''varies the time based on the number of objects it must delete this an upper limit of 10 seconds
        'intTimerInterval = 500 + (50 * c_alstObjectID.Count)
        'If intTimerInterval > 10000 Then
        '    intTimerInterval = 10000
        'End If

        ''ProcessMonitorControl_DeleteObject.WebTimerInterval = intTimerInterval
        ''ProcessMonitorControl_DeleteObject.DisplayWarnings = False
        ''ProcessMonitorControl_DeleteObject.PromptToContinueWhenWarnings = False
        ''ProcessMonitorControl_DeleteObject.ThreadName = strThreadName

        ''Create the thread and start it
        'thread = New SpatialDimensionLibrary.Threading.SDThread(objParameterThread)
        'thread.Priority = ThreadPriority.Normal
        'thread.Start(pDicThreadParameters)
        'thread.Name = objProcessMonitor.ThreadName

    End Sub

    Private Sub DeleteLicenses(guidLicenses As Guid)
        'Loops arraylist and deletes all objects
        Dim DBcon As DBConnection
        Dim intCountDeleted As Integer
        Dim intCountProcessed As Integer
        Dim blnCanceProcessRequested As Boolean

        'parameters
        Dim objLookupmanager As LookupManager
        Dim objProcessMonitor As ProcessMonitor
        Dim strLanguage As String
        Dim alstObjects As ArrayList
        Dim intObjectType As enumDeletableObjectType

        Dim objDeletableObject As DeletableObject
        Dim objAdditionalOptions As Object

        DBcon = NewConnection()

        Try

            'get parameters
            'objLookupmanager = pDicThreadParameters(ProcessMonitor.THREAD_PARAMETER_LOOKUP_MANAGER)
            'objProcessMonitor = pDicThreadParameters(ProcessMonitor.THREAD_PARAMETER_PROCESS_MONITOR)
            'strLanguage = pDicThreadParameters(ProcessMonitor.THREAD_PARAMETER_LANGUAGE)
            'intObjectType = pDicThreadParameters(THREAD_PARAMETER_OBJECT_TYPE)
            'alstObjects = pDicThreadParameters(THREAD_PARAMETER_OBJECT_IDS)
            'objAdditionalOptions = pDicThreadParameters(THREAD_ADDITIONAL_OPTIONS)

            'create connection object and the open the connection
            'DBcon = CreateDBConForThread(Me.Page)

            'DBcon = NewConnection()
            DBcon.OpenDBConnection()

            'set the boolean to false by default
            'blnCanceProcessRequested = False

            'loop through all objects to delete unless the process was canceled
            'Do While intCountProcessed <= guidLicenses.Count - 1

            objLookupmanager = New LookupManagerManager()

            objDeletableObject = DeletableObjectFactory.NewDeletableObject(DBcon, objLookupmanager, "English", enumDeletableObjectType.License, guidLicenses)

            'objDeletableObject.ExecuteAsynchronousProcessOnDelete = False

            DBcon.BeginTransaction()

                If objDeletableObject.Delete("English", Nothing) Then
                    intCountDeleted += 1
                    DBcon.Commit()

                Else
                    DBcon.Rollback()

                    'failed to delete object - add message to process monitor
                    'objProcessMonitor.AddWarningMessage(objDeletableObject.DeleteErrorMessage)
                    'objProcessMonitor.ErrorOccuredDontStopProcess = True
                End If
                intCountProcessed += 1

            'objProcessMonitor.CurrentCount = intCountDeleted
            'blnCanceProcessRequested = objProcessMonitor.CancelRequest

            'Loop

            'objDeletableObject.PostDelete(DBcon, objLookupmanager, strLanguage, objAdditionalOptions)

            'Refresh lookup tables 
            'DeletableObject.RefreshlookupTable(DBcon, objLookupmanager, intObjectType)

            'If blnCanceProcessRequested Then
            '    objProcessMonitor.ProcessCancelled = True
            'Else
            '    objProcessMonitor.ProcessComplete = True
            'End If

            'close db connection
            DBcon.CloseDBConnection()


        Catch ex As Exception

            'rollback potential transaction
            If DBcon.TransactionExists Then
                DBcon.Rollback()
            End If

            'close db connection
            DBcon.CloseDBConnection()
            'objProcessMonitor.Exception = ex
        End Try

    End Sub
End Class
