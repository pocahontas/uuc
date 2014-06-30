Option Explicit On
Option Strict On

Imports WinEventHook.WinHookEvent
Imports WinEventHook.WinHookEvent.WinEvents

' http://reflector.webtropy.com/default.aspx/Dotnetfx_Vista_SP2/Dotnetfx_Vista_SP2/8@0@50727@4016/DEVDIV/depot/DevDiv/releases/Orcas/QFE/wpf/src/UIAutomation/UIAutomationClient/MS/Internal/Automation/HwndProxyElementProvider@cs/1/HwndProxyElementProvider@cs

Public Class Form1

	Shared WindowEvent As WinHookEvent = Nothing
	Shared KBEvent As WinHookExKb = Nothing
	Shared MouseEvent As WinHookExMouse = Nothing

	Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		Btn_hook.Enabled = True
		Btn_unhook.Enabled = False
		Btn_Hook_Mouse.Enabled = True
		Btn_Unhook_Mouse.Enabled = False
		Btn_Hook_KB.Enabled = True
		Btn_Unhook_KB.Enabled = False
		TextBox1.Enabled = False
	End Sub

	Private Sub Form1_UnLoad(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosing
		If WindowEvent IsNot Nothing Then WindowEvent.Close()
		If KBEvent IsNot Nothing Then KBEvent.Close()
		If MouseEvent IsNot Nothing Then MouseEvent.Close()
	End Sub


	Private Sub Btn_Hook_Click(sender As System.Object, e As System.EventArgs) Handles Btn_hook.Click
		Btn_hook.Enabled = False
		WindowEvent = New WinHookEvent(New WinEvents() _
		{
		 Event_System_DIALOGSTART, Event_System_DIALOGEND,
		 Event_System_SCROLLINGSTART, Event_System_SCROLLINGEND,
		 event_object_CREATE, event_object_FOCUS, event_object_DESTROY
		 })
		AddHandler WindowEvent.SystemEvent, AddressOf WindowEventCallBack
		Btn_unhook.Enabled = True
	End Sub

	Private Sub Btn_unhook_Click(sender As System.Object, e As System.EventArgs) Handles Btn_unhook.Click
		Btn_hook.Enabled = True
		RemoveHandler WindowEvent.SystemEvent, AddressOf WindowEventCallBack
		WindowEvent.Close()
		Btn_unhook.Enabled = False
	End Sub

	Private Sub Btn_Hook_KB_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Hook_KB.Click
		Btn_Hook_KB.Enabled = False
		KBEvent = New WinHookExKb()
		AddHandler KBEvent.ExEventKeyboard, AddressOf KBEventCallBack
		Btn_Unhook_KB.Enabled = True
	End Sub

	Private Sub Btn_unhook_KB_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Unhook_KB.Click
		Btn_Hook_KB.Enabled = True
		RemoveHandler KBEvent.ExEventKeyboard, AddressOf KBEventCallBack
		KBEvent.Close()
		Btn_Unhook_KB.Enabled = False
	End Sub

	Private Sub Btn_Hook_Mouse_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Hook_Mouse.Click
		Btn_Hook_Mouse.Enabled = False
		MouseEvent = New WinHookExMouse()
		AddHandler MouseEvent.ExEventMouse, AddressOf MouseEventCallBack
		Btn_Unhook_Mouse.Enabled = True
	End Sub

	Private Sub Btn_unhook_Mouse_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Unhook_Mouse.Click
		Btn_Hook_Mouse.Enabled = True
		RemoveHandler MouseEvent.ExEventMouse, AddressOf MouseEventCallBack
		MouseEvent.Close()
		Btn_Unhook_Mouse.Enabled = False
	End Sub

	Private Sub WindowEventCallBack(hWinEventhook As IntPtr, EventType As WinEvents, Hwnd As IntPtr, idObject As Integer, idChild As Integer, dwEventThread As UInteger, dwmsEventTime As UInteger)
		Dim s As String

		Dim p As Process = GetInfos.GetProcessByHwnd(Hwnd)
		If p Is Nothing Then Exit Sub ' pas de process associé au Hwnd
		If My.Settings.ExcludeProcess.Contains(GetInfos.GetProcessName(p)) Then Exit Sub ' process exclus

		s = String.Format( _
		 "Pid={0}" + vbTab + "Thread={1}" + vbTab + "EV={2}" + vbTab + "Name={3}" + vbTab + "exe={4},",
		 p.Id,
		 dwEventThread,
		 EventType.ToString.PadRight(30),
		 GetInfos.GetProcessName(p).PadRight(25),
		 GetInfos.GetProcessExe(p))

		AddTobox(s)

	End Sub

	Private Sub KBEventCallBack(ByVal k As WinHookExKb.Keys, ByVal kV As WinHookExKb.VirtualKeys, kTime As UInteger)

		Dim s As String
		s = String.Format("Key={0}" + vbTab + "Virtual Code={1}", k.ToString.PadRight(20), kV.ToString.PadRight(25))
		AddTobox(s)

	End Sub

	Private Sub MouseEventCallBack(button As WinHookExMouse.Buttons, pt As WinHookExMouse.POINT, mousedata As Int32, time As Int32)

		Dim s As String
		s = String.Format("Mouse={0}" + vbTab + "Point={1},{2}" + vbTab + "data={3}", button.ToString.PadRight(20), pt.X, pt.Y, mousedata)
		AddTobox(s)

	End Sub

	Private Sub AddTobox(s As String)
		Static olock As New Object
		SyncLock olock
			TextBox1.Text = s + vbCrLf + TextBox1.Text
			If TextBox1.Text.Length > 1000 Then TextBox1.Text = TextBox1.Text.Remove(1000)
		End SyncLock
	End Sub

End Class
