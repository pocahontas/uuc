Option Explicit On
Option Strict On

Imports System
Imports System.Threading
Imports System.Linq

''' <summary>
''' Groups function about system info
''' </summary>
''' <remarks></remarks>
Public Module GetInfos

	Private Declare Function GetWindowThreadProcessId Lib "user32" _
	   (ByVal hwnd As IntPtr, ByRef lpdwprocessid As Long) As Long

	''' <summary>
    ''' Return the process corresponding to the handle
	''' </summary>
    ''' <param name="Hwnd">App Handle</param>
	''' <returns>Process</returns>
    ''' <remarks>Nothing if no match</remarks>
	Public Function GetProcessByHwnd(Hwnd As IntPtr) As Process
		Dim p As Process = Nothing
		Dim lpdwprocessid As Long = 0
		If Hwnd <> IntPtr.Zero Then
			Try
				GetWindowThreadProcessId(Hwnd, lpdwprocessid)
				p = GetProcessByPid(lpdwprocessid)
			Catch ex As Exception
				p = Nothing
			End Try
		End If
		Return p
	End Function

	''' <summary>
    ''' Returns the process corresponding to the PID
	''' </summary>
    ''' <param name="Pid">Process ID</param>
	''' <returns>Process</returns>
    ''' <remarks>Nothing if no match</remarks>
	Public Function GetProcessByPid(Pid As Long) As Process
		Dim myProcess As Process = Nothing
		Try
			myProcess = Process.GetProcessById(CInt(Pid))
		Catch
		End Try
		Return myProcess
	End Function

	''' <summary>
    ''' Returns the process name 
	''' </summary>
    ''' <param name="p">Process Name</param>
	''' <returns>String</returns>
	''' <remarks></remarks>
	Public Function GetProcessName(p As Process) As String
		Dim s As String
		Try
			s = p.ProcessName
		Catch ex As Exception
			s = "ex:" + ex.Message
		End Try
		Return s
	End Function

	''' <summary>
    ''' Returns the exe associated to the first module of a process 
	''' </summary>
	''' <param name="p">Process</param>
	''' <returns>String</returns>
	''' <remarks></remarks>
	Public Function GetProcessExe(p As Process) As String
		Dim s As String
		Try
			s = p.Modules(0).FileName
		Catch ex As Exception
			s = "ex:" + ex.Message
		End Try
		Return s
	End Function

End Module
