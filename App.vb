Imports System.IO
Public Class App
    Private Sub App_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = Application.StartupPath
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim x As New Form
        Dim y As New Panel
        Dim z As New TextBox

        x.Text = "File List"
        y.Dock = DockStyle.Fill
        z.Dock = DockStyle.Fill
        z.Multiline = True


        If Directory.Exists(TextBox1.Text) Then
            For Each FName As String In Directory.GetFiles(TextBox1.Text)
                If Not FName.Contains("desktop.ini") And Not FName.Contains("ReNamer.exe") Then
                    z.AppendText(FName & vbNewLine)
                End If
            Next

            y.Controls.Add(z)
            x.Controls.Add(y)
            x.ShowDialog()
        Else
            MsgBox("This directory no exists!", MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim x As New FolderBrowserDialog
        If x.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = x.SelectedPath
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.Text = "" Then
            MsgBox("Don't leave empty!", MsgBoxStyle.Critical, Me.Text)
        Else
            If Directory.Exists(TextBox1.Text) Then
                If Not Directory.Exists(TextBox1.Text & "/Backup/") Then
                    Directory.CreateDirectory(TextBox1.Text & "/Backup/")
                End If

                Label5.Text = "Wait"
                For Each FName As String In Directory.GetFiles(TextBox1.Text)
                    If Not FName.Contains("desktop.ini") And Not FName.Contains("ReNamer.exe") Then
                        Dim BackupName = FName.Split("\")(FName.Split("\").Count - 1)
                        My.Computer.FileSystem.CopyFile(FName, TextBox1.Text & "\Backup\" & BackupName)

                        If (FName.Contains(TextBox2.Text)) Then
                            My.Computer.FileSystem.RenameFile(FName, BackupName.Replace(TextBox2.Text, TextBox3.Text))
                        End If
                    End If
                Next
                Label5.Text = "Finish"
            Else
                MsgBox("This directory no exists!", MsgBoxStyle.Critical, Me.Text)
            End If
        End If
    End Sub
End Class
