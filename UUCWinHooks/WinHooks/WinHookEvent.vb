Option Explicit On
Option Strict On

' inspired from:
' http://www.codeproject.com/Tips/559023/Directly-Hook-to-System-Events-using-managed-code
'

Friend Class WinHookEvent
	Implements IDisposable

	Private disposed As Boolean = False
	' Implement IDisposable.
	' Do not make this method virtual.
	' A derived class should not be able to override this method.
	Public Overloads Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		' This object will be cleaned up by the Dispose method.
		' Therefore, you should call GC.SupressFinalize to
		' take this object off the finalization queue 
		' and prevent finalization code for this object
		' from executing a second time.
		GC.SuppressFinalize(Me)
	End Sub

	' Dispose(bool disposing) executes in two distinct scenarios.
	' If disposing equals true, the method has been called directly
	' or indirectly by a user's code. Managed and unmanaged resources
	' can be disposed.
	' If disposing equals false, the method has been called by the 
	' runtime from inside the finalizer and you should not reference 
	' other objects. Only unmanaged resources can be disposed.
	Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
		' Check to see if Dispose has already been called.
		If Not Me.disposed Then
			' If disposing equals true, dispose all managed 
			' and unmanaged resources.
			If disposing Then
				' Dispose managed resources.
				Me.Close()
			End If

			' Call the appropriate methods to clean up 
			' unmanaged resources here.
			' If disposing is false, 
			' only the following code is executed.

			' Note disposing has been done.
			disposed = True
		End If
	End Sub

	' Error codes
	' Assign these to the AccessibilityImplementation.errno property to indicate errors.
	Private Enum ErrorCode
		NOERROR = &H0	' No error 
		FAIL = &H80004005	' Generic or unexpected failure 
		MEMBERNOTFOUND = &H80020003	' This property is inapplicable to this object 
		INVALIDARG = &H80070057	' One or more arguments were invalid
	End Enum

	' Object role constants
	' See Microsoft's Accessibility Object Roles for documentation.

	Private Enum RoleSystem
		ALERT = &H8
		ANIMATION = &H36
		APPLICATION = &HE
		BORDER = &H13
		BUTTONDROPDOWN = &H38
		BUTTONDROPDOWNGRID = &H3A
		BUTTONMENU = &H39
		CARET = &H7
		CELL = &H1D
		CHARACTER = &H20
		CHART = &H11
		CHECKBUTTON = &H2C
		CLIENT = &HA
		CLOCK = &H3D
		COLUMN = &H1B
		COLUMNHEADER = &H19
		COMBOBOX = &H2E
		CURSOR = &H6
		DIAGRAM = &H35
		DIAL = &H31
		DIALOG = &H12
		DOCUMENT = &HF
		DROPLIST = &H2F
		EQUATION = &H37
		GRAPHIC = &H28
		GRIP = &H4
		GROUPING = &H14
		HELPBALLOON = &H1F
		HOTKEYFIELD = &H32
		INDICATOR = &H27
		LINK = &H1E
		LIST = &H21
		LISTITEM = &H22
		MENUBAR = &H2
		MENUITEM = &HC
		MENUPOPUP = &HB
		OUTLINE = &H23
		OUTLINEITEM = &H24
		PAGETAB = &H25
		PAGETABLIST = &H3C
		PANE = &H10
		PROGRESSBAR = &H30
		PROPERTYPAGE = &H26
		PUSHBUTTON = &H2B
		RADIOBUTTON = &H2D
		ROW = &H1C
		ROWHEADER = &H1A
		SCROLLBAR = &H3
		SEPARATOR = &H15
		SLIDER = &H33
		SOUND = &H5
		SPINBUTTON = &H34
		SPLITBUTTON = &H3E
		STATICTEXT = &H29
		STATUSBAR = &H17
		TABLE = &H18
		TEXT = &H2A
		TITLEBAR = &H1
		TOOLBAR = &H16
		TOOLTIP = &HD
		WHITESPACE = &H3B
		WINDOW = &H9
	End Enum

	' Object state constants
	' See Microsoft's Accessibility Object State Constants for documentation.

	Public Enum StateSystem
		ALERT_HIGH = &H10000000
		ALERT_LOW = &H4000000
		ALERT_MEDIUM = &H8000000
		ANIMATED = &H4000
		BUSY = &H800
		CHECKED = &H10
		COLLAPSED = &H400
		[DEFAULT] = &H100
		EXPANDED = &H200
		EXTSELECTABLE = &H2000000
		FLOATING = &H1000
		FOCUSABLE = &H100000
		FOCUSED = &H4
		HOTTRACKED = &H80
		INDETERMINATE = &H20
		INVISIBLE = &H8000
		LINKED = &H400000
		MARQUEED = &H2000
		MIXED = &H20
		MOVEABLE = &H40000
		MULTISELECTABLE = &H1000000
		NORMAL = &H0
		OFFSCREEN = &H10000
		PRESSED = &H8
		[PROTECTED] = &H20000000
		[READONLY] = &H40
		SELECTABLE = &H200000
		SELECTED = &H2
		SELFVOICING = &H80000
		SIZEABLE = &H20000
		TRAVERSED = &H800000
		UNAVAILABLE = &H1
	End Enum

	' Selection flags
	' The following constants are described in the AccessibilityImplementation class documentation under the accSelect() method.
	Private Enum SelectionFlags
		TAKESELECTION = &H2
		EXTENDSELECTION = &H4
		ADDSELECTION = &H8
		REMOVESELECTION = &H10
	End Enum

	' Event constants()
	' See Microsoft's Accessibility Event Constants for documentation.
	' http://msdn.microsoft.com/en-us/library/windows/desktop/dd318066(v=vs.85).aspx
	'
	Private Const event_object_mask As Long = &H8000
	Public Enum WinEvents
		unknown = 0
		Event_System_SOUND = 1
		Event_System_ALERT = 2
		Event_System_FOREGROUND = 3
		Event_System_MENUSTART = 4
		Event_System_MENUEND = 5
		Event_System_MENUPOPUPSTART = 6
		Event_System_MENUPOPUPEND = 7
		Event_System_CAPTURESTART = 8
		Event_System_CAPTUREEND = 9
		Event_System_MOVESIZESTART = 10
		Event_System_MOVESIZEEND = 11
		Event_System_CONTEXTHELPSTART = 12
		Event_System_CONTEXTHELPEND = 13
		Event_System_DRAGDROPSTART = 14
		Event_System_DRAGDROPEND = 15
		Event_System_DIALOGSTART = 16
		Event_System_DIALOGEND = 17
		Event_System_SCROLLINGSTART = 18
		Event_System_SCROLLINGEND = 19
		Event_System_SWITCHSTART = 20
		Event_System_SWITCHEND = 21
		Event_System_MINIMIZESTART = 22
		Event_System_MINIMIZEEND = 23

		event_object_CREATE = event_object_mask Or 0
		event_object_DESTROY = event_object_mask Or 1
		event_object_SHOW = event_object_mask Or 2
		event_object_HIDE = event_object_mask Or 3
		event_object_REORDER = event_object_mask Or 4
		event_object_FOCUS = event_object_mask Or 5
		event_object_SELECTION = event_object_mask Or 6
		event_object_SELECTIONADD = event_object_mask Or 7
		event_object_SELECTIONREMOVE = event_object_mask Or 8
		event_object_SELECTIONWITHIN = event_object_mask Or 9
		event_object_STATECHANGE = event_object_mask Or 10
		event_object_LOCATIONCHANGE = event_object_mask Or 11
		event_object_NAMECHANGE = event_object_mask Or 12
		event_object_DESCRIPTIONCHANGE = event_object_mask Or 13
		event_object_VALUECHANGE = event_object_mask Or 14
		event_object_PARENTCHANGE = event_object_mask Or 15
		event_object_HELPCHANGE = event_object_mask Or 16
		event_object_DEFACTIONCHANGE = event_object_mask Or 17
		event_object_ACCELERATORCHANGE = event_object_mask Or 18
	End Enum

	Private Enum HookFlags As Integer
		OUTOFCONTEXT = &H0	 'Events are ASYNC
		SKIPOWNTHREAD = &H1	 'Don't call back for events on installer's thread
		SKIPOWNPROCESS = &H2  'Don't call back for events on installer's process
		INCONTEXT = &H4	 'Events are SYNC, this causes your dll to be injected into every process
	End Enum

	Private Declare Auto Function SetWinEventHook Lib "user32.dll" (ByVal eventMin As UInteger, ByVal eventMax As UInteger, ByVal hmodWineventProc As IntPtr, ByVal lpfnWineventProc As WineventDelegate, ByVal idProcess As UInteger, ByVal idThread As UInteger, ByVal dwFlags As UInteger) As IntPtr
	Private Declare Auto Function UnhookWinEvent Lib "user32.dll" (handle As IntPtr) As Boolean

	Private Delegate Sub WineventDelegate(ByVal hWineventHook As IntPtr, ByVal eventType As UInteger, ByVal hwnd As IntPtr, ByVal idObject As Integer, ByVal idChild As Integer, ByVal dwEventThread As UInteger, ByVal dwmsEventTime As UInteger)
	Public Event SystemEvent(ByVal hWineventHook As IntPtr, ByVal eventType As WinEvents, ByVal hwnd As IntPtr, ByVal idObject As Integer, ByVal idChild As Integer, ByVal dwEventThread As UInteger, ByVal dwmsEventTime As UInteger)

	Private m_event(-1) As WinEvents
	Private m_delegate As WineventDelegate = Nothing
	Private m_hook As IntPtr = IntPtr.Zero

	Public Sub New(ByVal e() As WinEvents)
		m_event = e
        ' We position m_eventMin (resp m_eventMax) to the max (resp to the min) possible
		Dim m_eventMin As UInteger = UInteger.MaxValue
		Dim m_eventMax As UInteger = UInteger.MinValue
        ' We go through the enumeration of asked events
		For Each we As WinEvents In e
            ' We update min and max to follow
			m_eventMin = Math.Min(m_eventMin, CUInt(we))
			m_eventMax = Math.Max(m_eventMax, CUInt(we))
		Next

		m_delegate = New WineventDelegate(AddressOf WineventCallBack)
        GC.KeepAlive(m_delegate) ' desactivate the Garbage Collector
		Try
			m_hook = SetWinEventHook( _
			 m_eventMin, _
			 m_eventMax, _
			 IntPtr.Zero, _
			 m_delegate, _
			 0UI, _
			 0UI, _
			 CUInt(HookFlags.OUTOFCONTEXT Or HookFlags.SKIPOWNPROCESS))
		Catch ex As Exception
			Debug.Assert(False, ex.Message)
		End Try
	End Sub

	''' <summary>
    ''' Free resources
	''' </summary>
	''' <remarks></remarks>
	Public Sub Close()
		Dim r As Boolean = True
		Try
			If m_hook <> IntPtr.Zero Then r = UnhookWinEvent(m_hook)
			m_hook = IntPtr.Zero
			m_delegate = Nothing
		Catch ex As Exception
			r = False
		End Try
		Debug.Assert(r)
	End Sub

	''' <summary>
    ''' Callback on an event
	''' </summary>
	''' <param name="hWineventHook"></param>
	''' <param name="eventType"></param>
	''' <param name="hwnd"></param>
	''' <param name="idObject"></param>
	''' <param name="idChild"></param>
	''' <param name="dwEventThread"></param>
	''' <param name="dwmsEventTime"></param>
	''' <remarks></remarks>
	Private Sub WineventCallBack(ByVal hWineventHook As IntPtr, ByVal eventType As UInteger, ByVal hwnd As IntPtr, ByVal idObject As Integer, ByVal idChild As Integer, ByVal dwEventThread As UInteger, ByVal dwmsEventTime As UInteger)
		If SystemEventEvent Is Nothing Then Exit Sub ' 
        ' If no handler, no need to lose time here
		If SystemEventEvent.GetInvocationList.Length <= 0 Then Exit Sub

		Dim thisEvent As WinEvents
		Try
			thisEvent = CType(eventType, WinEvents)
		Catch ex As Exception
			thisEvent = WinEvents.unknown
		End Try
        If Not m_event.Contains(thisEvent) Then Exit Sub ' don't belong to followed events
		RaiseEvent SystemEvent(hWineventHook, thisEvent, hwnd, idObject, idChild, dwEventThread, dwmsEventTime)
	End Sub

End Class

