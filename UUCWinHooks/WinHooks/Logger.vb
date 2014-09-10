Module Logger
    Dim PrevUsername As String = GetUserName()
    Dim PrevTs As System.DateTime = System.DateTime.Now
    Dim PrevProcessName As String = "START"
    Dim PrevProgramName As String = "START"
    Dim PrevServiceName As String = "START"
    Dim PrevWindowName As String = "START"
    Dim PrevFullName As String = "START"
    Dim PrevCategory As String = "Other"
    Dim PrevTimeWaste As String = "Undefined"
    Dim PrevEnoughData As Boolean = False

    Dim dictCategory As New Dictionary(Of String, List(Of String))



    Public Sub LogEvent(Buffer As Dictionary(Of System.DateTime, Tuple(Of Integer, String)), FilePath As String, Ts As System.DateTime, ProcessName As String, WindowHandle As IntPtr, MainWindow As String, Window As String, EnoughData As Boolean)
        Dim dictCategory As New Dictionary(Of String, List(Of String))

        ' --- Creating keywords to categorize 
        If dictCategory.Count() = 0 Then
            dictCategory.Add("References", New List(Of String)({"wikipedia", "bing", "google", "yahoo"}))
            dictCategory.Add("Social Networking", New List(Of String)({"facebook", "linkedin", "flickr", "pinterest", "foursquare", "instagram", "google plus", "google+", "twitter"}))
            dictCategory.Add("Emailing", New List(Of String)({"gmail", "outlook", "hotmail", "webmail"}))
            dictCategory.Add("Scheduling", New List(Of String)({"google agenda", "agenda", "google keep", "evernote", "timetable"}))
            dictCategory.Add("Entertainment", New List(Of String)({"youtube", "netflix", "buzz", "streaming", "series", "film", "video", "dropbox", "deezer", "grooveshark", "9gag", "porn", "imdb", "the lad bible"}))
            dictCategory.Add("Shopping", New List(Of String)({"deals", "shopping", "argos", "gumtree", "ebay", "paypal", "supermarket", "argos"}))
            dictCategory.Add("Software Development", New List(Of String)({"php", "java", "c++", "c#", "programming", "html", "code", ".js", "javascript", "python", "vb", "software", "stack overflow"}))
            dictCategory.Add("News", New List(Of String)({"reddit", "telegraph", "cnn", "bbc", "mail online", "news"}))
            dictCategory.Add("Travel", New List(Of String)({"trip", "travel", "holiday", "hotel", "lastminute"}))
            dictCategory.Add("Utilities", New List(Of String)({"bank", "banking"}))
            dictCategory.Add("Office", New List(Of String)({"google docs", "google drive", "google sheets"}))
        End If

        Dim dictPair As KeyValuePair(Of String, List(Of String))
        Dim keyword As String


        ' --- If App is starting
        If ProcessName = "Start" Then
            PrevEnoughData = EnoughData
        End If

        ' --- Calculate duration of previous event
        Dim duration As TimeSpan
        duration = Ts - PrevTs

        ' --- Write last event to Log file
        If Not System.IO.File.Exists(FilePath) Then
            System.IO.File.Create(FilePath).Dispose()
        End If
        If PrevUsername <> "" Then
            Using ObjWriter As New System.IO.StreamWriter(FilePath, True)
                ObjWriter.WriteLine(PrevUsername & "," & PrevTs & "," & Ts & "," & duration.Seconds.ToString & "," & PrevProcessName & "," & PrevProgramName & "," & PrevServiceName & "," & PrevFullName & "," & PrevCategory & "," & PrevTimeWaste & "," & PrevEnoughData.ToString())
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
        If Not Buffer.ContainsKey(PrevTs) Then
            Buffer.Add(PrevTs, tuple)
        End If


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
            Case "notepad", "notepad++", "notes2", "nlnotes"
                ProgramName = "Notes"
                ServiceName = "Word Processing"
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
                Category = "Office"
            Case "EXCEL"
                ProgramName = "Excel"
                ServiceName = "Spreadsheet"
                Category = "Office"
            Case "POWERPNT"
                ProgramName = "PowerPoint"
                ServiceName = "Presentation"
                Category = "Office"
            Case "MSPUB"
                ProgramName = "Publisher"
                ServiceName = "Presentation"
                Category = "Office"


                ' --- Communication ---
            Case "OUTLOOK"
                ProgramName = "Outlook"
                ServiceName = "Emailing"
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
                For Each dictPair In dictCategory
                    For Each keyword In dictPair.Value
                        If Window.Contains(keyword) Then
                            Category = dictPair.Key
                        End If
                    Next
                Next

                ' --- Dev ---
            Case "devenv", "eclipse", "netbeans", "pycharm"
                If Window.Contains("Visual Studio") Then
                    ProgramName = "Visual Studio"
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
        PrevEnoughData = EnoughData


    End Sub

End Module
