Option Explicit On

Imports System.Text
Imports System.Runtime.InteropServices.Marshal

Module CurrentUrl

#Region " Overview & References "
    'Overview:
    'Function GetCurrentUrl returns the URL of the selected browser (IE or Chrome). 
    'Most of the code is based on the references listed below, but this function starts with
    'the browser's main window handle and returns only 1 URL. 
    'It also builds a simple "treeview" of the windows up to the target window's classname.

    'References: 
    'http://www.xtremevbtalk.com/archive/index.php/t-129988.html
    'http://social.msdn.microsoft.com/forums/en-us/vbgeneral/thread/321D0EAD-CD50-4517-BC43-29190542DCE0
    'http://social.msdn.microsoft.com/Forums/en/vbgeneral/thread/02a67f3a-4a26-4d9a-9c67-0fdff1428a66

#End Region

#Region " Declares, Constants, and Variables"
    Private Delegate Function EnumProcDelegate(ByVal hwnd As IntPtr, ByVal lParam As IntPtr) As Boolean 'Delegate added
    Private Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As EnumProcDelegate, ByVal lParam As IntPtr) As Boolean
    Private Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As IntPtr, ByVal lpString As StringBuilder, ByVal nMaxCount As Integer) As Integer
    Private Declare Function GetWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal wCmd As IntPtr) As IntPtr
    Private Declare Function GetClassName Lib "user32" Alias "GetClassNameA" (ByVal hwnd As IntPtr, ByVal lpClassName As String, ByVal nMaxCount As Integer) As Integer
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As IntPtr, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer

    Private Const WM_GETTEXT As Integer = &HD
    Private Const WM_GETTEXTLENGTH As Integer = &HE

    Private Const GW_CHILD As Integer = 5
    Private Const GW_HWNDNEXT As Integer = 2
    Private Const GW_HWNDFIRST As Integer = 0

    Private sURL As String 'String that will contain the URL
    Private cbWindows As ComboBox 'Treeview" 
    Private sIndent As String 'Spaces
    Private sBrowser As String 'Starting window (IE or Chrome)
    Private sClassName As String = "Edit" 'Default
#End Region

    Public Function GetCurrentUrl(ByVal hwnd As IntPtr, ByVal browser As String, ByVal classname As String, ByVal combo As ComboBox) As String
        sBrowser = browser
        sClassName = classname
        cbWindows = combo
        If cbWindows IsNot Nothing Then
            If cbWindows.GetType.Name = "ComboBox" Then
                cbWindows.Items.Clear()
            Else
                cbWindows = Nothing
            End If
        End If
        sURL = ""
        sIndent = ""
        EnumWindows(AddressOf EnumProc, hwnd) 'hwnd - originally IntPtr.Zero
        Return sURL
    End Function

    ' Enumerate the windows
    ' Find the URL in the browser window 
    Private Function EnumProc(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean
        Dim buf As StringBuilder = New StringBuilder(256) 'String * 1024
        Dim title As String
        Dim length As Integer

        ' Get the window's title.
        length = GetWindowText(hWnd, buf, buf.Capacity)
        title = Left(buf.ToString, length)

        ' See if the title ends with the browser name 
        Dim s As String = sBrowser
        Dim inprivate As String = sBrowser & " - [InPrivate]"  'IE adds this to the window title
        If title <> "" Then
            If (Right(title, s.Length) = s) Or (Right(title, inprivate.Length) = inprivate) Then
                ' This is it. Find the URL information.
                sURL = EditInfo(hWnd, cbWindows)
                Return False
            End If
        End If
        ' Continue searching
        Return True

    End Function

    ' If this window is of the Edit class (IE) or Chrome_AutocompleteEditView (Google), return its contents. 
    ' Otherwise search its children for such an object.
    Private Function EditInfo(ByVal window_hwnd As IntPtr, ByRef cbWindows As ComboBox) As String
        Dim txt As String = ""
        Dim buf As String
        Dim buflen As Integer
        Dim child_hwnd As IntPtr
        Dim children() As IntPtr = {}
        Dim num_children As Integer
        Dim i As Integer

        'Get the class name.
        buflen = 256
        buf = Space(buflen - 1)
        buflen = GetClassName(window_hwnd, buf, buflen)
        buf = Left(buf, buflen)

        'Add an item to the window list combo, indent as required
        If cbWindows IsNot Nothing Then
            cbWindows.Items.Add(sIndent & buf)
        End If
        ' See if we found an Edit/AutocompleteEditView object.
        If buf = sClassName Then
            Return WindowText(window_hwnd)
        End If

        ' It's not an Edit/AutocompleteEditView object. Search the children.
        ' Make a list of the child windows.
        num_children = 0
        child_hwnd = GetWindow(window_hwnd, CType(GW_CHILD, IntPtr))
        While child_hwnd <> CType(0, IntPtr)
            num_children = num_children + 1
            ReDim Preserve children(0 To num_children) 'was 1 to ..
            children(num_children) = child_hwnd
            child_hwnd = GetWindow(child_hwnd, CType(GW_HWNDNEXT, IntPtr))
        End While

        ' Get information on the child windows.
        sIndent &= "    "
        For i = 1 To num_children
            txt = EditInfo(children(i), cbWindows)
            If txt <> "" Then Exit For
        Next i
        sIndent = Left(sIndent, sIndent.Length - 4)

        Return txt
    End Function

    ' ************************************************
    ' Return the text associated with the window.
    ' ************************************************
    Private Function WindowText(ByVal window_hwnd As IntPtr) As String
        Dim txtlen As Integer
        Dim txt As String

        txt = "" 'WindowText = ""
        If window_hwnd = CType(0, IntPtr) Then Return "" 'Exit Function

        'Get the size of the window text
        txtlen = SendMessage(window_hwnd, CType(WM_GETTEXTLENGTH, IntPtr), CType(0, IntPtr), CType(0, IntPtr))
        If txtlen = 0 Then Return "" 'Exit Function

        'Extra for terminating char
        txtlen = txtlen + 1

        'Alloc memory for the buffer that recieves the text
        Dim buffer As IntPtr = AllocHGlobal(txtlen)

        'Send The WM_GETTEXT Message
        txtlen = SendMessage(window_hwnd, CType(WM_GETTEXT, IntPtr), CType(txtlen, IntPtr), buffer) 'byval txt 

        'Copy the characters from the unmanaged memory to a managed string
        txt = PtrToStringAnsi(buffer)
        Return Left(txt, txtlen)
    End Function

End Module
