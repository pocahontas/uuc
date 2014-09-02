Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.OleDb
Imports System.IO




Public Class FormLogger

    Private Declare Function GetForegroundWindow Lib "user32.dll" () As IntPtr
    Private Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessID As Integer) As Integer
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hWnd As IntPtr, ByVal WinTitle As String, ByVal MaxLength As Integer) As Integer
    Private Declare Function GetWindowTextLength Lib "user32.dll" Alias "GetWindowTextLengthA" (ByVal hwnd As Integer) As Integer

    Public BlnLoggerActive As Boolean = False

    Public LogFilePath As String = "UserLog.txt"
    Public TimeWastedFilePath As String = "TimeWasted.txt"
    Public ReportFilePath As String = "Report_UserProductivity.txt"

    Public Const MinLength As Integer = 100
    Public Const MaxLength As Integer = 200
    Public Buffer As New Dictionary(Of System.DateTime, Tuple(Of Integer, String))
    Public EnoughData As Boolean = False 'Indicates if the graphical interface has been started or not


    Private Sub FormLogger_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TimerPolling.Interval = 2000
        TimerRefresh.Interval = 180000 'Updating productivity every 3 minutes = 180000
        Logger.LogEvent(Buffer, LogFilePath, System.DateTime.Now, "Start", Nothing, "", "", Nothing)
        TimerPolling.Start()
        TimerRefresh.Start()
        TextBoxStatus.Text = "Recording Initial Data"
    End Sub


    Public CurrentActiveWindow As String = ""
    Private Sub TimerPolling_Tick(sender As Object, e As EventArgs) Handles TimerPolling.Tick
        '----- Get the Handle to the Current Foreground Window ----- 
        Dim hWnd As IntPtr = GetForegroundWindow()
        If hWnd = IntPtr.Zero Then Exit Sub

        '----- Find the Length of the Window's Title -----
        Dim TitleLength As Integer
        TitleLength = GetWindowTextLength(CInt(hWnd))

        '----- Find the Window's Title ----- 
        Dim WindowTitle As String = StrDup(TitleLength + 1, "*")
        GetWindowText(hWnd, WindowTitle, TitleLength + 1)

        '----- Find the PID of the Application that Owns the Window ----- 
        Dim pid As Integer = 0
        GetWindowThreadProcessId(hWnd, pid)
        If pid = 0 Then Exit Sub

        '----- Get the actual PROCESS from the process ID ----- 
        Dim proc As Process = Process.GetProcessById(pid)
        If proc Is Nothing Then Exit Sub

        If String.Compare(CurrentActiveWindow, "") = 0 Then
            CurrentActiveWindow = WindowTitle
            Logger.LogEvent(Buffer, LogFilePath, System.DateTime.Now, proc.ProcessName, proc.MainWindowHandle, proc.MainWindowTitle, WindowTitle, EnoughData)
        Else
            If CurrentActiveWindow <> WindowTitle Then
                Logger.LogEvent(Buffer, LogFilePath, System.DateTime.Now, proc.ProcessName, proc.MainWindowHandle, proc.MainWindowTitle, WindowTitle, EnoughData)
                CurrentActiveWindow = WindowTitle
            End If
        End If
    End Sub


    Private Sub TimerRefresh_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        ' Checking if we have enough data before making the user aware of its productivity level
        Dim FileLength As Integer = -1
        If EnoughData = False Then
            If System.IO.File.Exists(LogFilePath) Then
                FileLength = NumberLines(LogFilePath)
                If FileLength >= MinLength Then
                    EnoughData = True
                    TextBoxStatus.Text = "Experimenting"
                End If
            End If
        End If
        ' Making the user aware of its new productivity
        Dim currentBuffer As New Dictionary(Of System.DateTime, Tuple(Of Integer, String))
        currentBuffer = Buffer
        Productivity.Update(currentBuffer, EnoughData, System.DateTime.Now)
        Debug.Print("Productivity has been Updated")
        If FileLength > MaxLength Then
            TimerPolling.Stop()
            TimerRefresh.Stop()
            TextBoxStatus.Text = "Analyzing Data"
            DataAnalysis.Analyze(LogFilePath, TimeWastedFilePath, ReportFilePath)
        End If
        TextBoxStatus.Text = "Report Generated"

    End Sub

    Private Function NumberLines(filepath As String) As Integer
        Dim myReader As StreamReader
        Try
            myReader = File.OpenText(filepath)
        Catch e As IOException
            NumberLines = -1
            Exit Function
        End Try
        Dim lineCounter As Integer = 0
        Dim currentLine As String = Nothing, currentData As String = Nothing
        Do
            Try
                currentLine = myReader.ReadLine
                lineCounter = lineCounter + 1
            Catch e As EndOfStreamException
            Finally
                currentData = currentData & currentLine & vbCrLf
            End Try
        Loop While currentLine <> Nothing
        NumberLines = lineCounter - 1
        myReader.Close()
        myReader = Nothing
    End Function


    Private Sub ButtonViewReport_Click(sender As Object, e As EventArgs) Handles ButtonViewReport.Click
        If File.Exists(ReportFilePath) Then
            Process.Start(ReportFilePath)
        Else
            MsgBox("The report has not been generated yet. Please wait until the status becomes 'Report Generated'. Thanks!")
        End If
    End Sub

End Class