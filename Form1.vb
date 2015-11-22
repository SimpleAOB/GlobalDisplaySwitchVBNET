Imports System.Runtime.InteropServices

Public Class Form1
    <DllImport("User32.dll")> _
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function

    <DllImport("User32.dll")> _
    Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function
    Public Const MOD_CTRL As Integer = &H2 'Ctrl key
    Public Const WM_HOTKEY As Integer = &H312
    Public currentMode As Integer = 0
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 100, MOD_CTRL, Keys.NumPad1)
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select (id.ToString)
                Case "100"
                    If currentMode = 0 Then
                        monitor2Display()
                        currentMode = 1
                    ElseIf currentMode = 1 Then
                        extendDisplay()
                        currentMode = 0
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Public Sub extendDisplay()
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = "C:\Windows\System32\DisplaySwitch.exe"
        startInfo.Arguments = "/extend"
        Process.Start(startInfo)
    End Sub
    Public Sub monitor2Display()
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = "C:\Windows\System32\DisplaySwitch.exe"
        startInfo.Arguments = "/external"
        Process.Start(startInfo)
    End Sub
    Public Sub monitor1Display()
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = "C:\Windows\System32\DisplaySwitch.exe"
        startInfo.Arguments = "/internal"
        Process.Start(startInfo)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 100)
    End Sub
End Class
