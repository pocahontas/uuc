Option Explicit On
Option Strict On

Public Class WinHookExMouse

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

	Private Const WH_MOUSE_LL As UInteger = 14
	 

	Public Enum Buttons
		WM_LBUTTONDOWN = &H201
		WM_LBUTTONUP = &H202
		WM_MOUSEMOVE = &H200
		WM_MOUSEWHEEL = &H20A
		WM_MOUSEHWHEEL = &H20E
		WM_RBUTTONDOWN = &H204
		WM_RBUTTONUP = &H205
	End Enum

	<System.Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
	Public Structure POINT
		Public X As Integer
		Public Y As Integer
	End Structure
	
	<Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
	Public Class MSLLHOOKSTRUCT
		Public pt As Point
		Public mouseData As Int32
		Public flags As MSLLHOOKSTRUCTFlags
		Public time As Int32
		Public dwExtraInfo As UIntPtr
	End Class

	<Flags()> _
	Public Enum MSLLHOOKSTRUCTFlags As Int32
		LLMHF_INJECTED = 1
	End Enum


	Private Declare Auto Function SetWindowsHookEx Lib "user32.dll" (ByVal idHook As UInteger, ByVal lpfn As WinExDelegate, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
	Private Declare Auto Function UnhookWindowsHookEx Lib "user32.dll" (handle As IntPtr) As Boolean

	Private Delegate Function WinExDelegate(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As MSLLHOOKSTRUCT) As Integer

	Public Event ExEventMouse(button As Buttons, pt As POINT, mousedata As Int32, time As Int32)

	Private m_delegate As WinExDelegate = Nothing
	Private m_hook As IntPtr = IntPtr.Zero

	''' <summary>
    ''' Install a hook which intercepts low level mouse events
	''' </summary>
	''' <remarks></remarks>
	Public Sub New()
		m_delegate = New WinExDelegate(AddressOf WinExCallBackMouse)
        GC.KeepAlive(m_delegate) ' desactivates the Garbage Collector on the delegate (not sure if really useful..) 
		Try
			m_hook = SetWindowsHookEx( _
			 WH_MOUSE_LL, _
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
	Private Function WinExCallBackMouse(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As MSLLHOOKSTRUCT) As Integer
        If nCode <> 0 Then Return 0 ' ncode should be equal to HC_ACTION (0)
		If ExEventMouseEvent Is Nothing Then Return 0
        ' If no handler, no need to lose time here...
		If ExEventMouseEvent.GetInvocationList.Length <= 0 Then Return 0

		Dim button As Buttons = CType(wParam, Buttons)
		RaiseEvent ExEventMouse(button, lParam.pt, lParam.mouseData, lParam.time)

		'return the value returned by CallNextHookEx
		Return 0
	End Function

End Class
