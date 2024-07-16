Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=TSUNAMI;Initial Catalog=LibraryDB;Integrated Security=True;Encrypt=False"
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()
        Disp_Data()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Insert into Books values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "')"
        cmd.ExecuteNonQuery()
        MsgBox("Record Inserted Succesfully")
        Disp_Data()
    End Sub
    Public Sub Disp_Data()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Books"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt



    End Sub

    Private Sub dgvBooks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub dgvBooks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
            con.Open()
            i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Books where id=" & i & ""
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                TextBox1.Text = dr.GetString(1).ToString()
                TextBox2.Text = dr.GetString(2).ToString()
                TextBox3.Text = dr.GetInt32(3).ToString()
                TextBox4.Text = dr.GetString(4).ToString()
            End While
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()

        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update Books set Title='" + TextBox1.Text + "',Author='" + TextBox2.Text + "',YearPublished='" + TextBox3.Text + "',Genre='" + TextBox4.Text + "'where id=" & i & ""
        cmd.ExecuteNonQuery()

        Disp_Data()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("Only Type In Title when using delete")
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()

        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from Books where Title='" + TextBox1.Text + "'"
        cmd.ExecuteNonQuery()
        Disp_Data()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()

        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from Books"
        cmd.ExecuteNonQuery()
        Disp_Data()
    End Sub
End Class