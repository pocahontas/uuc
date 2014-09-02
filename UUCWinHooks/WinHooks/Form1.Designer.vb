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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLogger))
        Me.TimerPolling = New System.Windows.Forms.Timer(Me.components)
        Me.TimerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.Chart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.RichTextBoxInfo = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonViewReport = New System.Windows.Forms.Button()
        CType(Me.Chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.Chart.Location = New System.Drawing.Point(207, 392)
        Me.Chart.Name = "Chart"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series1.Name = "Prod"
        Me.Chart.Series.Add(Series1)
        Me.Chart.Size = New System.Drawing.Size(49, 49)
        Me.Chart.TabIndex = 1
        Me.Chart.Text = "Chart1"
        Me.Chart.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(125, 297)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(110, 13)
        Me.LinkLabel1.TabIndex = 5
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "http://goo.gl/XE72Fn"
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(12, 260)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(37, 13)
        Me.LabelStatus.TabIndex = 6
        Me.LabelStatus.Text = "Status"
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.AcceptsReturn = True
        Me.TextBoxStatus.Location = New System.Drawing.Point(128, 257)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(345, 20)
        Me.TextBoxStatus.TabIndex = 7
        '
        'RichTextBoxInfo
        '
        Me.RichTextBoxInfo.AcceptsTab = True
        Me.RichTextBoxInfo.Location = New System.Drawing.Point(15, 12)
        Me.RichTextBoxInfo.Name = "RichTextBoxInfo"
        Me.RichTextBoxInfo.Size = New System.Drawing.Size(458, 222)
        Me.RichTextBoxInfo.TabIndex = 8
        Me.RichTextBoxInfo.Text = resources.GetString("RichTextBoxInfo.Text")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 297)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Link to the survey"
        '
        'ButtonViewReport
        '
        Me.ButtonViewReport.Location = New System.Drawing.Point(107, 327)
        Me.ButtonViewReport.Name = "ButtonViewReport"
        Me.ButtonViewReport.Size = New System.Drawing.Size(232, 49)
        Me.ButtonViewReport.TabIndex = 10
        Me.ButtonViewReport.Text = "View Report"
        Me.ButtonViewReport.UseVisualStyleBackColor = True
        '
        'FormLogger
        '
        Me.AccessibleDescription = "User Logger Application"
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(485, 453)
        Me.Controls.Add(Me.ButtonViewReport)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RichTextBoxInfo)
        Me.Controls.Add(Me.TextBoxStatus)
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Chart)
        Me.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormLogger"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "User Logger"
        CType(Me.Chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerPolling As System.Windows.Forms.Timer
    Friend WithEvents TimerRefresh As System.Windows.Forms.Timer
    Friend WithEvents Chart As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LabelStatus As System.Windows.Forms.Label
    Friend WithEvents TextBoxStatus As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBoxInfo As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonViewReport As System.Windows.Forms.Button

End Class
