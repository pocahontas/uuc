

Module DataAnalysis
    Sub Analyze(FilePath As String, TimeWastedFile As String, ReportName As String)
        ' --- Create New Text File for Reporting
        MsgBox("Data Analysis")
        Dim file As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(ReportName, False)
        file.WriteLine("User Productivity Report")
        file.Close()
        AnalyzeTimeWasted(TimeWastedFile, ReportName)
        AnalyzeCat(FilePath, ReportName)


    End Sub

    Sub AnalyzeTimeWasted(TimeWastedFile As String, ReportName As String)
        ' --- Analyze the time series of percentage of time wasted
        Dim TextLine As String
        Dim sumValuesBefore, sumValuesAfter As Double
        Dim sumSquareValuesBefore, sumSquareValuesAfter As Double
        Dim nbElemsBefore, nbElemsAfter As Integer
        Dim longestFocusBefore, longestFocusAfter As Double 'in minutes
        Dim longestWasteBefore, longestWasteAfter As Double
        Dim percentageOfProductiveTimeBefore, percentageOfProductiveTimeAfter As Double
        Dim prev As Double
        Dim wasteBefore, wasteAfter As Double
        Dim focusBefore, focusAfter As Double
        Dim totalTimeBefore, totalTimeAfter As Double
        Dim StartTs, EndTs As System.DateTime


        If System.IO.File.Exists(TimeWastedFile) Then
            Dim ObjReader As New System.IO.StreamReader(TimeWastedFile)
            Dim i As Integer = 0

            Do While ObjReader.Peek() <> -1
                i += 1

                TextLine = ObjReader.ReadLine()
                Dim parts As String() = TextLine.Split(New Char() {","c})
                Dim StringTs As String = parts(0)

                Dim Ts As System.DateTime = New DateTime()
                Ts = Convert.ToDateTime(StringTs)
                If i = 1 Then
                    StartTs = Ts
                End If

                Dim TimeWasted As Double = Convert.ToDouble(parts(1))
                Dim EnoughData As String = parts(2)

                If EnoughData = "False" Then
                    sumValuesBefore += TimeWasted
                    sumSquareValuesBefore += TimeWasted * TimeWasted
                    nbElemsBefore += 1
                    If prev > 50 And TimeWasted > 50 Then
                        wasteBefore += 5
                        If wasteBefore >= longestWasteBefore Then
                            longestWasteBefore = wasteBefore
                        End If
                    ElseIf prev < 20 And TimeWasted < 20 Then
                        focusBefore += 5
                        If focusBefore >= longestFocusBefore Then
                            longestFocusBefore = focusBefore
                        End If
                    End If

                    If TimeWasted < 20 Then
                        percentageOfProductiveTimeBefore += 5
                    End If
                    totalTimeBefore += 5


                End If
                If EnoughData = "True" Then
                    sumValuesAfter += TimeWasted
                    sumSquareValuesAfter += TimeWasted * TimeWasted
                    nbElemsAfter += 1
                    If prev > 50 And TimeWasted > 50 Then
                        wasteAfter += 5
                        If wasteAfter >= longestWasteAfter Then
                            longestWasteAfter = wasteAfter
                        End If
                    ElseIf prev < 20 And TimeWasted < 20 Then
                        focusAfter += 5
                        If focusAfter >= longestFocusAfter Then
                            longestFocusAfter = focusAfter
                        End If
                    End If

                    If TimeWasted < 20 Then
                        percentageOfProductiveTimeAfter += 5
                    End If
                    totalTimeAfter += 5
                End If
                prev = TimeWasted
                EndTs = Ts
            Loop
        End If

        Dim durationRecording As System.TimeSpan
        durationRecording = EndTs - StartTs

        percentageOfProductiveTimeBefore = percentageOfProductiveTimeBefore * 100 / totalTimeBefore
        percentageOfProductiveTimeAfter = percentageOfProductiveTimeAfter * 100 / totalTimeAfter

        Dim meanBefore As Double = sumValuesBefore / nbElemsBefore
        Dim meanAfter As Double = sumValuesAfter / nbElemsAfter
        Dim varBefore As Double = sumSquareValuesBefore / nbElemsBefore - meanBefore * meanBefore
        Dim varAfter As Double = sumSquareValuesAfter / nbElemsAfter - meanAfter * meanAfter

        Dim file As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(ReportName, True)
        file.WriteLine("Start of the recording: " & StartTs.ToString)
        file.WriteLine("End of the recording: " & EndTs.ToString)
        file.WriteLine("Total duration of the experiment: " & durationRecording.TotalHours.ToString)
        file.WriteLine("Total duration recorded without the tool (in min):" & totalTimeBefore)
        file.WriteLine("Total duration recorded using the tool (in min): " & totalTimeAfter)
        file.WriteLine()
        file.WriteLine("Average Percentage Of Time Wasted without the tool: " & meanBefore.ToString)
        file.WriteLine("Average Percentage Of Time Wasted using the tool: " & meanAfter.ToString)
        file.WriteLine()
        file.WriteLine("Variance of Time Wasted without the tool: " & varBefore.ToString)
        file.WriteLine("Variance of Time Wasted using the tool: " & varAfter.ToString)
        file.WriteLine()
        file.WriteLine("Longest period, in minutes, of 'focus' without the tool: " & longestFocusBefore)
        file.WriteLine("Longest period, in minutes, of 'focus' using the tool: " & longestFocusAfter)
        file.WriteLine()
        file.WriteLine("Percentage of time of productive usage without the tool: " & percentageOfProductiveTimeBefore)
        file.WriteLine("Percentage of time of productive usage using the tool: " & percentageOfProductiveTimeAfter)
        file.WriteLine()
        file.WriteLine("Longest period, in minutes, of 'waste period' without the tool: " & longestWasteBefore)
        file.WriteLine("Longest period, in minutes, of 'waste period' using the tool: " & longestWasteAfter)
        file.Close()

    End Sub

    Sub AnalyzeCat(FilePath As String, ReportName As String)
        ' --- Analyze the full user log
        Dim percentageSocialNetworkBefore As Double
        Dim percentageSocialNetworkAfter As Double
        Dim percentageEntertainmentBefore As Double
        Dim percentageEntertainmentAfter As Double
        Dim percentageCommunicationBefore As Double
        Dim percentageCommunicationAfter As Double
        Dim totalDurationBefore As Double
        Dim totalDurationAfter As Double

        Dim TextLine As String
        If System.IO.File.Exists(FilePath) Then
            Dim ObjReader As New System.IO.StreamReader(FilePath)
            Do While ObjReader.Peek() <> -1
                TextLine = ObjReader.ReadLine()
                Dim parts As String() = TextLine.Split(New Char() {","c})
                Dim duration As Double = Convert.ToDouble(parts(3))
                Dim category As String = parts(8)
                Dim toolOn As String = parts(10)

                If toolOn = "False" Then
                    totalDurationBefore += duration
                    Select Case category
                        Case "Social Networking"
                            percentageSocialNetworkBefore += duration
                        Case "Entertainment"
                            percentageEntertainmentBefore += duration
                        Case "Communication"
                            percentageCommunicationBefore += duration
                    End Select
                ElseIf toolOn = "True" Then
                    totalDurationAfter += duration
                    Select Case category
                        Case "Social Networking"
                            percentageSocialNetworkAfter += duration
                        Case "Entertainment"
                            percentageEntertainmentAfter += duration
                        Case "Communication"
                            percentageCommunicationAfter += duration
                    End Select
                End If
            Loop
            percentageCommunicationBefore /= totalDurationBefore
            percentageSocialNetworkBefore /= totalDurationBefore
            percentageEntertainmentBefore /= totalDurationBefore
            percentageCommunicationAfter /= totalDurationAfter
            percentageSocialNetworkAfter /= totalDurationAfter
            percentageEntertainmentAfter /= totalDurationAfter
        End If
        Dim file As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(ReportName, True)
        file.WriteLine()
        file.WriteLine("percentage of time concerning Communication with the tool off: " & percentageCommunicationBefore)
        file.WriteLine("percentage of time concerning Communication with the tool On: " & percentageCommunicationAfter)
        file.WriteLine()
        file.WriteLine("percentage of time concerning Social Networking with the tool off: " & percentageSocialNetworkBefore)
        file.WriteLine("percentage of time concerning Social Networking with the tool on: " & percentageSocialNetworkAfter)
        file.WriteLine()
        file.WriteLine("percentage of time concerning Entertainment with the tool off: " & percentageEntertainmentBefore)
        file.WriteLine("percentage of time concerning Entertainment with the tool on: " & percentageEntertainmentAfter)
        file.Close()
    End Sub



End Module
