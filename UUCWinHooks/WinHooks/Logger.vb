Module Logger
    Dim PrevUsername As String = ""
    Dim PrevTs As System.DateTime
    Dim PrevProcessName As String
    Dim PrevProgramName As String
    Dim PrevServiceName As String
    Dim PrevWindowName As String
    Dim PrevFullName As String
    Dim PrevCategory As String
    Dim PrevTimeWaste As String


    Public Sub LogEvent(Buffer As Dictionary(Of System.DateTime, Tuple(Of Integer, String)), FilePath As String, Ts As System.DateTime, ProcessName As String, WindowHandle As IntPtr, MainWindow As String, Window As String)
        ' --- Calculate duration of previous event
        Dim duration As TimeSpan
        duration = Ts - PrevTs

        ' --- Write last event to Log file
        If Not System.IO.File.Exists(FilePath) Then
            System.IO.File.Create(FilePath).Dispose()
        End If
        If PrevUsername <> "" Then
            Using ObjWriter As New System.IO.StreamWriter(FilePath, True)
                ObjWriter.WriteLine(PrevUsername & "," & PrevTs & "," & Ts & "," & duration.Seconds & "," & PrevProcessName & "," & PrevProgramName & "," & PrevServiceName & "," & PrevFullName & "," & PrevCategory & "," & PrevTimeWaste)
            End Using
        End If

        ' --- Update buffer

        ' Removing Old event(s)
        Dim pair As KeyValuePair(Of System.DateTime, Tuple(Of Integer, String))
        Dim keysToRemove As New List(Of System.DateTime)
        For Each pair In Buffer
            If pair.Key.AddMinutes(15) < Ts Then
                ' Event happened more than 5 minutes ago, so lets delete it
                keysToRemove.Add(pair.Key)
            End If
        Next
        Dim key As System.DateTime
        For Each key In keysToRemove
            Buffer.Remove(key)
        Next
        ' Adding new event
        Dim tuple As Tuple(Of Integer, String) = New Tuple(Of Integer, String)(duration.Seconds, PrevTimeWaste)
        Buffer.Add(PrevTs, tuple)

        ' --- Analyse new event
        Dim Username As String = Tools.GetUserName()
        Dim ProgramName As String = ""
        Dim ServiceName As String = ""
        Dim FullName As String = ""
        Dim Category As String = "Other"
        Dim TimeWaste As String = "Undefined"

        Select Case ProcessName
            ' --- Utilities ---
            Case "explorer", "SndVol"
                ProgramName = "Explorer"
                ServiceName = MainWindow
                Category = "Utilities"
            Case "calc"
                ProgramName = "Calculator"
                ServiceName = "Calculator"
                FullName = "Calculator"
                Category = "Utilities"
            Case "7zFM"
                ProgramName = "7zip"
                ServiceName = "Compression"
                FullName = "7zip"
                Category = "Utilities"
            Case "notepad", "notepad++"
                ProgramName = "NotePad"
                ServiceName = "Word Processing"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Utilities"
            Case "jgs"
                ProgramName = "Java Platform"
                ServiceName = "VM"
                Category = "Utilities"
            Case "Foxit Reader", "Acrobat", "Acrobat Elements", "AcrobatInfo", "PDFSaver"
                ProgramName = ProcessName
                ServiceName = "PDF"
                Category = "Utilities"
            Case "DropBox"
                ProgramName = "Dropbox"
                ServiceName = "Storage"
                Category = "Utilities"
            Case "nero"
                ProgramName = "Nero Burning ROM"
                ServiceName = "Storage"
                Category = "Utilities"
            Case "RescueTime"
                ProgramName = "Rescue Time"
                ServiceName = "Time Management"
                Category = "Utilities"

                ' --- Office ---
            Case "WINWORD"
                ProgramName = "Word"
                ServiceName = "Word Processing"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Office"
            Case "EXCEL"
                ProgramName = "Excel"
                ServiceName = "Spreadsheet"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Office"
            Case "POWERPNT"
                ProgramName = "PowerPoint"
                ServiceName = "Presentation"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Office"
            Case "MSPUB"
                ProgramName = "Publisher"
                ServiceName = "Presentation"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Office"


                ' --- Communication ---
            Case "OUTLOOK"
                ProgramName = "Outlook"
                ServiceName = "Emailing"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Emailing"
            Case "Thunderbird"
                ProgramName = "Thunderbird"
                ServiceName = "Emailing"
                Category = "Emailing"
            Case "Skype"
                ProgramName = "Skype"
                ServiceName = "Chat"
                Category = "Communication"
            Case "mric", "irc", "mIRC"
                ProgramName = "IRC"
                ServiceName = "Chat"
                Category = "Communication"
            Case "aim"
                ProgramName = "AOL Instant Messenger"
                ServiceName = "Chat"
                Category = "Communication"
            Case "ts3client_win64"
                ProgramName = "Team Speak"
                ServiceName = "Chat"
                Category = "Communication"
            Case "mumble"
                ProgramName = "Mumble"
                ServiceName = "Chat"
                Category = "Communication"

                ' --- Browsing ---
            Case "iexplore", "chrome", "firefox"
                If ProcessName.Contains("chrome") Then
                    ProgramName = "Chrome"
                ElseIf ProcessName.Contains("ie") Then
                    ProgramName = "Internet Explorer"
                Else
                    ProgramName = "Firefox"
                End If
                ServiceName = "Browsing"
                ' No Need to store URL, as it is not going to be used (and tends to bug depending on the platform)
                'FullName = GetCurrentUrl(WindowHandle, "Windows Internet Explorer", "Edit", Nothing)

                Window = Window.ToLower()
                If Window.Contains("facebook") Or Window.Contains("twitter") Or Window.Contains("flickr") Or Window.Contains("linkedIn") Or Window.Contains("pinterest") Or Window.Contains("instagram") Then
                    Category = "Social Networking"
                ElseIf Window.Contains("gmail") Or Window.Contains("outlook") Or Window.Contains("webmail") Then
                    Category = "Emailing"
                ElseIf Window.Contains("google agenda") Or Window.Contains("google keep") Then
                    Category = "Scheduling"
                ElseIf Window.Contains("youtube") Or Window.Contains("netflix") Or Window.Contains("buzz") Or Window.Contains("9gag") Or Window.Contains("video") Or Window.Contains("imdb") Or Window.Contains("entertainment") Or Window.Contains("streaming") Or Window.Contains("series") Or Window.Contains("film") Or Window.Contains("porn") Or Window.Contains("the lad bible") Then
                    Category = "Entertainment"
                ElseIf Window.Contains("amazon") Or Window.Contains("supermarket") Or Window.Contains("deals") Or Window.Contains("shopping") Or Window.Contains("argos") Or Window.Contains("gumtree") Or Window.Contains("ebay") Or Window.Contains("paypal") Then
                    Category = "Shopping"
                ElseIf Window.Contains("reddit") Or Window.Contains("telegraph") Or Window.Contains("cnn") Or Window.Contains("bbc") Or Window.Contains("mail online") Or Window.Contains("news") Then
                    Category = "News"
                ElseIf Window.Contains("stack overflow") Or Window.Contains("software") Or Window.Contains("vb") Or Window.Contains("python") Or Window.Contains("java") Or Window.Contains("php") Or Window.Contains("c++") Or Window.Contains("c#") Or Window.Contains("html") Or Window.Contains("javascript") Or Window.Contains("programming") Or Window.Contains("code") Then
                    Category = "Software Development"
                ElseIf Window.Contains("google docs") Or Window.Contains("google drive") Or Window.Contains("google sheets") Then
                    Category = "Office"
                ElseIf Window.Contains("bing") Or Window.Contains("live") Or Window.Contains("google") Or Window.Contains("wikipedia") Then
                    Category = "References"
                ElseIf Window.Contains("bank") Or Window.Contains("banking") Then
                    Category = "Utilities"
                ElseIf Window.Contains("holiday") Or Window.Contains("hotel") Or Window.Contains("lastminute") Then
                    Category = "Travel"
                End If

                ' --- Dev ---
            Case "devenv", "eclipse", "netbeans", "pycharm"
                If Window.Contains("Visual Studio") Then
                    ProgramName = "Visual Studio"
                    FullName = Window.Substring(0, InStr(Window, "-") - 2)
                ElseIf ProcessName = "netbeans" Then
                    ProgramName = "Netbeans"
                ElseIf ProcessName = "pycharm" Then
                    ProgramName = "Pycharm"
                Else
                    ProgramName = "Eclipse"
                End If
                ServiceName = "IDE"
                Category = "Software Development"
            Case "GitHub"
                ProgramName = "GitHub"
                ServiceName = "VCS"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Software Development"
            Case "MATLAB"
                ProgramName = ProcessName
                ServiceName = "Data Analysis"
                Category = "Software Development"
            Case "putty"
                ProgramName = "Putty"
                ServiceName = "Remote Connection"
                Category = "Software Development"
            Case "vmconnect"
                ProgramName = "Virtual Machine Connection"
                ServiceName = "Remote Connection"
                Category = "Software Development"
            Case "mmc"
                ProgramName = "Microsoft Management Console"
                ServiceName = "Remote Connection"
                Category = "Software Development"
            Case "MSACCESS"
                ProgramName = "Access"
                ServiceName = "Database"
                Category = "Software Develpment"

                ' --- Design ---
            Case "mspaint"
                ProgramName = "Paint"
                ServiceName = "Drawing"
                FullName = Window.Substring(0, InStr(Window, "-") - 2)
                Category = "Design"
            Case "Photoshop", "PhotoshopElements", "PhotoshopElementsFileAgent", "PhotoshopElementsOrganizer"
                ProgramName = "Adobe Photoshop"
                ServiceName = "Drawing"
                Category = "Design"
            Case "InDesign"
                ProgramName = "Adobe InDesign"
                ServiceName = "Presentation"
                Category = "Design"
            Case "WLXPhotoGallery"
                ProgramName = "Windows Live Photo Gallery"
                ServiceName = "Photo Editor"
                Category = "Design"
            Case "OIS"
                ProgramName = "Microsoft Office Picture Manager"
                ServiceName = "Photo Editor"
                Category = "Design"
            Case "Premiere"
                ProgramName = "Adobe Premiere"
                ServiceName = "Video Editor"
                Category = "Design"
            Case "Picasa2", "Picasa3"
                ProgramName = "Picasa"
                ServiceName = "Photo Editor"
                Category = "Design"
            Case "PhotosApp"
                ProgramName = "Photos"
                ServiceName = "Photo Editor"
                Category = "Design"

                ' --- Entertainment"
            Case "League of Legends", "LoLLauncher", "LoLClient"
                ProgramName = "League of Legends"
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "Game"
                ProgramName = "Duel of Champions"
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "CivilizationV_DX11", "CivilizationV_DX9"
                ProgramName = "Civilization V"
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "WoW", "Steam", "HiRezLauncherUI", "Steam", "Uplay", "RomStation"
                ProgramName = ProcessName
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "GFWLClient"
                ProgramName = "Games for Windows Marketplace"
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "OriginClientService", "Origin"
                ProgramName = "EA Origin"
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "TS3W", "TS1W", "TS2W"
                ProgramName = "Sims"
                ServiceName = "Gaming"
                Category = "Entertainment"
            Case "vlc"
                ProgramName = "VLC"
                ServiceName = "Video"
                Category = "Entertainment"
            Case "mplayer", "mplayer2", "wmplayer"
                ProgramName = "Windows Media Player"
                ServiceName = "Video"
                Category = "Entertainment"
            Case "winamp", "WINAMP"
                ProgramName = "Winamp"
                ServiceName = "Video"
                Category = "Entertainment"
            Case "QuickTimePlayer"
                ProgramName = "QuickTime"
                ServiceName = "Video"
                Category = "Entertainment"
            Case "M6"
                ProgramName = "6Play"
                ServiceName = "Video"
                Category = "Entertainment"

                ' --- Shopping ---
            Case "WinStore"
                ProgramName = "Windows Store"
                ServiceName = "Store"
                Category = "Shopping"

        End Select
        ' --- Time Waste depending on the cateogry
        Select Case Category
            Case "Entertainment", "Shopping", "Communication", "Travel", "News", "Social Networking"
                TimeWaste = "True"
            Case "Utilities", "Office", "Emailing", "Scheduling", "Design", "Software Development", "References"
                TimeWaste = "False"

        End Select

        ' --- Store new event
        PrevTs = Ts
        PrevUsername = Username
        PrevProcessName = ProcessName
        PrevProgramName = ProgramName
        PrevServiceName = ServiceName
        If FullName <> "" Then
            PrevFullName = FullName
        Else
            PrevFullName = Window
        End If
        PrevCategory = Category
        PrevTimeWaste = TimeWaste


    End Sub

End Module
