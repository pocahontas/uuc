Imports System.IO
Module Productivity
    Public Sub Update(CurrentBuffer As Dictionary(Of System.DateTime, Tuple(Of Integer, String)), EnoughData As Boolean, CurrentTs As System.DateTime)
        Dim pair As KeyValuePair(Of System.DateTime, Tuple(Of Integer, String))
        Dim TotalTime As Integer = 0
        Dim TotalTimeWasted As Integer = 0
        Dim AvgTimeWasted As Double

        ' Calculate Current Productivity
        For Each pair In CurrentBuffer
            If pair.Value.Item2 = "True" Then
                TotalTimeWasted += pair.Value.Item1
            End If
            TotalTime += pair.Value.Item1
        Next


        AvgTimeWasted = TotalTimeWasted * 100 / TotalTime
        Debug.Print("Avg Time Wasted: " & AvgTimeWasted.ToString())
        ' Store Current Percentage of Time Wasted Into File for further analysis
        Dim ProductivityFilePath As String = "TimeWasted.txt"
        If Not System.IO.File.Exists(ProductivityFilePath) Then
            System.IO.File.Create(ProductivityFilePath).Dispose()
        End If

        Using ObjWriter As New System.IO.StreamWriter(ProductivityFilePath, True)
            ObjWriter.WriteLine(CurrentTs & "," & Math.Truncate(AvgTimeWasted) & "," & EnoughData.ToString())
        End Using

        ' Update Icon
        If EnoughData = True Then
            UpdateIcon(AvgTimeWasted) 'Can be replaced by update icon pie to replace color scale by pie graph
        End If

    End Sub

    Public Sub UpdateIcon(AvgTimeWasted As Double)
        Dim newIcon As System.Drawing.Icon
        Dim AppData As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)

        If AvgTimeWasted >= 75 Then
            ' Color = Red
            newIcon = New Icon(Path.Combine(AppData, "Icons/red_icon.ico"))
        ElseIf AvgTimeWasted >= 50 And AvgTimeWasted < 75 Then
            'Color = Orange
            newIcon = New Icon(Path.Combine(AppData, "Icons/orange_icon.ico"))
        ElseIf AvgTimeWasted >= 25 And AvgTimeWasted < 50 Then
            'Color = Yellow
            newIcon = New Icon(Path.Combine(AppData, "Icons/yellow_icon.ico"))
        Else
            'Color = Green
            newIcon = New Icon(Path.Combine(AppData, "Icons/green_icon.ico"))
        End If
        FormLogger.Icon = newIcon
    End Sub



    Public Sub UpdateIconPie(AvgTimeWasted As Double)

        ' Draw Graphic
        FormLogger.Chart.Series("Prod").Points.Clear()
        FormLogger.Chart.Series("Prod").Points.AddXY("Waste", AvgTimeWasted)
        FormLogger.Chart.Series("Prod").Points.AddXY("Productivity", 100 - AvgTimeWasted)

        ' Save Grahic

        FormLogger.Chart.SaveImage("pie.png", System.Drawing.Imaging.ImageFormat.Png)

        ' Update Icon
        Using image As New Bitmap("pie.png")
            Dim HIcon As System.IntPtr = image.GetHicon()
            Dim newIcon As System.Drawing.Icon = System.Drawing.Icon.FromHandle(HIcon)
            FormLogger.Icon = newIcon
        End Using

    End Sub

End Module
