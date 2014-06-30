Option Explicit On
Option Strict On

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class WinHookExKb

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


	Private Const WH_KEYBOARD_LL As UInteger = 13
	
	Public Enum Keys
		WM_KEYDOWN = &H100
		WM_KEYUP = &H101
		WM_SYSKEYDOWN = &H104
		WM_SYSKEYUP = &H105
	End Enum

	Public Enum VirtualKeys
		VK_LBUTTON = &H1
		VK_RBUTTON = &H2
		VK_CANCEL = &H3
		VK_MBUTTON = &H4
		VK_XBUTTON1 = &H5
		VK_BACK = &H8
		VK_TAB = &H9
		VK_CLEAR = &HC
		VK_RETURN = &HD
		VK_SHIFT = &H10
		VK_CONTROL = &H11
		VK_MENU = &H12
		VK_PAUSE = &H13
		VK_CAPITAL = &H14
		VK_KANA = &H15
		VK_JUNJA = &H17
		VK_FINAL = &H18
		VK_ESCAPE = &H1B
		VK_CONVERT = &H1C
		VK_NONCONVERT = &H1D
		VK_ACCEPT = &H1E
		VK_MODECHANGE = &H1F
		VK_SPACE = &H20
		VK_PRIOR = &H21
		VK_NEXT = &H22
		VK_END = &H23
		VK_HOME = &H24
		VK_LEFT = &H25
		VK_UP = &H26
		VK_RIGHT = &H27
		VK_DOWN = &H28
		VK_SELECT = &H29
		VK_PRINT = &H2A
		VK_EXECUTE = &H2B
		VK_SNAPSHOT = &H2C
		VK_INSERT = &H2D
		VK_DELETE = &H2E
		VK_HELP = &H2F
		VM_KEY_0 = &H30
		VM_KEY_1 = &H31
		VM_KEY_2 = &H32
		VM_KEY_3 = &H33
		VM_KEY_4 = &H34
		VM_KEY_5 = &H35
		VM_KEY_6 = &H36
		VM_KEY_7 = &H37
		VM_KEY_8 = &H38
		VM_KEY_9 = &H39

		VM_KEY_A = &H41
		VM_KEY_B = &H42
		VM_KEY_C = &H43
		VM_KEY_D = &H44
		VM_KEY_E = &H45
		VM_KEY_F = &H46
		VM_KEY_G = &H47
		VM_KEY_H = &H48
		VM_KEY_I = &H49
		VM_KEY_J = &H4A
		VM_KEY_K = &H4B
		VM_KEY_L = &H4C
		VM_KEY_M = &H4D
		VM_KEY_N = &H4E
		VM_KEY_O = &H4F
		VM_KEY_P = &H50
		VM_KEY_Q = &H51
		VM_KEY_R = &H52
		VM_KEY_S = &H53
		VM_KEY_T = &H54
		VM_KEY_U = &H55
		VM_KEY_V = &H56
		VM_KEY_W = &H57
		VM_KEY_X = &H58
		VM_KEY_Y = &H59
		VM_KEY_Z = &H5A

		VK_LWIN = &H5B
		VK_RWIN = &H5C
		VK_APPS = &H5D
		VK_SLEEP = &H5F
		VK_NUMPAD0 = &H60
		VK_NUMPAD1 = &H61
		VK_NUMPAD2 = &H62
		VK_NUMPAD3 = &H63
		VK_NUMPAD4 = &H64
		VK_NUMPAD5 = &H65
		VK_NUMPAD6 = &H66
		VK_NUMPAD7 = &H67
		VK_NUMPAD8 = &H68
		VK_NUMPAD9 = &H69
		VK_MULTIPLY = &H6A
		VK_ADD = &H6B
		VK_SEPARATOR = &H6C
		VK_SUBTRACT = &H6D
		VK_DECIMAL = &H6E
		VK_DIVIDE = &H6F
		VK_F1 = &H70
		VK_F2 = &H71
		VK_F3 = &H72
		VK_F4 = &H73
		VK_F5 = &H74
		VK_F6 = &H75
		VK_F7 = &H76
		VK_F8 = &H77
		VK_F9 = &H78
		VK_F10 = &H79
		VK_F11 = &H7A
		VK_F12 = &H7B
		VK_F13 = &H7C
		VK_F14 = &H7D
		VK_F15 = &H7E
		VK_F16 = &H7F
		VK_F17 = &H80
		VK_F18 = &H81
		VK_F19 = &H82
		VK_F20 = &H83
		VK_F21 = &H84
		VK_F22 = &H85
		VK_F23 = &H86
		VK_F24 = &H87
		VK_NUMLOCK = &H90
		VK_SCROLL = &H91
		VK_LSHIFT = &HA0
		VK_RSHIFT = &HA1
		VK_LCONTROL = &HA2
		VK_RCONTROL = &HA3
		VK_LMENU = &HA4
		VK_RMENU = &HA5
		VK_BROWSER_BACK = &HA6
		VK_BROWSER_FORWARD = &HA7
		VK_BROWSER_REFRESH = &HA8
		VK_BROWSER_STOP = &HA9
		VK_BROWSER_SEARCH = &HAA
		VK_BROWSER_HOME = &HAC
		VK_VOLUME_MUTE = &HAD
		VK_VOLUME_DOWN = &HAE
		VK_VOLUME_UP = &HAF
		VK_MEDIA_NEXT_TRACK = &HB0
		VK_MEDIA_PREV_TRACK = &HB1
		VK_MEDIA_STOP = &HB2
		VK_MEDIA_PLAY_PAUSE = &HB3
		VK_LAUNCH_MAIL = &HB4
		VK_LAUNCH_MEDIA_SELECT = &HB5
		VK_LAUNCH_APP1 = &HB6
		VK_LAUNCH_APP2 = &HB7
		VK_OEM_1 = &HBA
		VK_OEM_PLUS = &HBB
		VK_OEM_COMMA = &HBC
		VK_OEM_MINUS = &HBD
		VK_OEM_PERIOD = &HBE
		VK_OEM2 = &HBF
		VK_OEM3 = &HC0
		VK_OEM4 = &HDB
		VK_OEM5 = &HDC
		VK_OEM6 = &HDD
		VK_OEM7 = &HDE
		VK_OEM8 = &HDF

		VK_OEM102 = &HE2
		VK_PROCESSKEY = &HE5
		VK_PACKET = &HE7
		VK_ATTN = &HF6&
		VK_CRSEL = &HF7&
		VK_EXSEL = &HF8&
		VK_EREOF = &HF9&
		VK_PLAY = &HFA&
		VK_NONAME = &HFC&
		VK_PA1 = &HFD&
		VK_OEM_CLEAR = &HFE
	End Enum

	<Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
	Public Class KBDLLHOOKSTRUCT
		Public vkCode As UInt32
		Public scanCode As UInt32
		Public flags As UInt32
		Public time As UInt32
		Public dwExtraInfo As UIntPtr
	End Class

	Private Declare Auto Function SetWindowsHookEx Lib "user32.dll" (ByVal idHook As UInteger, ByVal lpfn As WinExDelegate, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
	Private Declare Auto Function UnhookWindowsHookEx Lib "user32.dll" (handle As IntPtr) As Boolean

	Private Delegate Function WinExDelegate(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As KBDLLHOOKSTRUCT) As Integer

	Public Event ExEventKeyboard(ByVal k As Keys, ByVal kV As VirtualKeys, kTime As UInteger)

	Private m_delegate As WinExDelegate = Nothing
	Private m_hook As IntPtr = IntPtr.Zero

	''' <summary>
    ''' Install a hook  for the Keyboard Low Level
	''' </summary>
	''' <remarks></remarks>
	Public Sub New()
		m_delegate = New WinExDelegate(AddressOf WinExCallbackKB)
        GC.KeepAlive(m_delegate) 'desactivate Garbage Collector
		Try
			m_hook = SetWindowsHookEx( _
			 WH_KEYBOARD_LL, _
			 m_delegate, _
			 IntPtr.Zero, _
			 0UI)
		Catch ex As Exception
			Debug.Assert(False, ex.Message)
		End Try
	End Sub

	''' <summary>
    ''' Free allocated resources 
	''' </summary>
	''' <remarks></remarks>
	Public Sub Close()
		Dim r As Boolean = True
		Try
			If m_hook <> IntPtr.Zero Then r = UnhookWindowsHookEx(m_hook)
			m_hook = IntPtr.Zero
			m_delegate = Nothing
		Catch ex As Exception
			r = False
		End Try
		Debug.Assert(r)
	End Sub

	''' <summary>
	''' The CallWndProc hook procedure is an application-defined or library-defined callback 
	''' function used with the SetWindowsHookEx function. The HOOKPROC type defines a pointer 
	''' to this callback function. CallWndProc is a placeholder for the application-defined 
	''' or library-defined function name.
	''' </summary>
	''' <param name="nCode">
	''' [in] Specifies whether the hook procedure must process the message. 
	''' If nCode is HC_ACTION, the hook procedure must process the message. 
	''' If nCode is less than zero, the hook procedure must pass the message to the 
	''' CallNextHookEx function without further processing and must return the 
	''' value returned by CallNextHookEx.
	''' </param>
	''' <param name="wParam">
	''' [in] Specifies whether the message was sent by the current thread. 
	''' If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
	''' </param>
	''' <param name="lParam">
	''' [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
	''' </param>
	''' <returns>
	''' If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
	''' If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
	''' and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
	''' hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
	''' procedure does not call CallNextHookEx, the return value should be zero. 
	''' </returns>
	''' <remarks>
	''' http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/callwndproc.asp
	''' </remarks>
	Private Function WinExCallbackKB(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As KBDLLHOOKSTRUCT) As Integer
        If nCode <> 0 Then Return 0 ' ncode should be equal to HC_ACTION (0)
		If ExEventKeyboardEvent Is Nothing Then Return 0
        ' If no handler, no need to lose time here
		If ExEventKeyboardEvent.GetInvocationList.Length <= 0 Then Return 0

		' we can convert the 2nd parameter (the key code) to a System.Windows.Forms.Keys enum constant
		Dim keyPressed As Keys = CType(wParam, Keys)
		Dim Vk As VirtualKeys = CType(lParam.vkCode, VirtualKeys)

			RaiseEvent ExEventKeyboard(keyPressed, Vk, lParam.time)

			Return 0
	End Function

End Class
