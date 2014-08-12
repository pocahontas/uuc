<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLogger
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.TimerPolling = New System.Windows.Forms.Timer(Me.components)
        Me.TimerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.Chart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.Chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnStart
        '
        Me.BtnStart.ForeColor = System.Drawing.Color.DarkCyan
        Me.BtnStart.Location = New System.Drawing.Point(76, 12)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(111, 30)
        Me.BtnStart.TabIndex = 0
        Me.BtnStart.Text = "Start User Logger"
        Me.BtnStart.UseVisualStyleBackColor = True
        '
        'TimerPolling
        '
        '
        'TimerRefresh
        '
        Me.TimerRefresh.Interval = 600000
        '
        'Chart
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart.ChartAreas.Add(ChartArea1)
        Me.Chart.Location = New System.Drawing.Point(12, 63)
        Me.Chart.Name = "Chart"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series1.Name = "Prod"
        Me.Chart.Series.Add(Series1)
        Me.Chart.Size = New System.Drawing.Size(239, 213)
        Me.Chart.TabIndex = 1
        Me.Chart.Text = "Chart1"
        '
        'FormLogger
        '
        Me.AccessibleDescription = "User Logger Application"
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(263, 288)
        Me.Controls.Add(Me.Chart)
        Me.Controls.Add(Me.BtnStart)
        Me.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.Name = "FormLogger"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "User Logger"
        CType(Me.Chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnStart As System.Windows.Forms.Button
    Friend WithEvents TimerPolling As System.Windows.Forms.Timer
    Friend WithEvents TimerRefresh As System.Windows.Forms.Timer
    Friend WithEvents Chart As System.Windows.Forms.DataVisualization.Charting.Chart

End Class
