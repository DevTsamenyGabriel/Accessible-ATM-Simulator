Imports System.Data.SqlClient



Public Class GLOGIN


'Public Sub New()
'        ' Required for the Windows Forms Designer to initialize controls
'        InitializeComponent()

'        ' CRITICAL FIX: Enable Double Buffering to prevent startup flicker
'        Me.DoubleBuffered = True
'    End Sub


''CONNECTION STRING
   Public Const CONNECTION_STRING As String = "Data Source=ANONYMOUSKAU;Initial Catalog=NEWATMGABE;Persist Security Info=True;User ID=sa;Password=2025;TrustServerCertificate=True"



















''TO SIGNUP BUTTON
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

      Me.TXTloginform.Visible =False
      If Me.TXTSignupform.Visible = False
         Me.TXTSignupform.Visible = True
         Me.TXTloginform.SendToBack
         Me.Guna2CirclePictureBox1.Visible =False
      End If

	End Sub

''TO LOGIN
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
           
        Me.TXTloginform.Visible = True
        Me.TXTSignupform.Visible = False
        Me.Guna2CirclePictureBox1.Visible = True
       

	End Sub




''REGISTER BUTTON====================================DEEEEEEEEEEEEEEEVVVVVVVVVVVVVVVVVVVVVV




''' DEV KAU THIS FUNCTION I DEFINE WILL ALLOW ME TO CHECK IF THE ACCOUNT NUMBER EXIST
Private Function AccountNumberExists(ByVal accNum As String, ByVal Pinss As String) As Boolean

       Dim isFound = False

      ''MAKE I SET TWO CHEKINGS CHECKING1 AND CHECKING2 TO TRUE

       Dim sqlQuery As String = "SELECT COUNT(ACCOUNT_NUMBER) FROM ATMGusers WHERE ACCOUNT_NUMBER = @AccNum OR ATM_PIN = @Pin"

       Using connection As New SqlConnection(CONNECTION_STRING)
         Using Command As New SqlCommand(sqlQuery, connection)

          Command.Parameters.AddWithValue("@ACCNum", accNum)
          Command.Parameters.AddWithValue("@Pin", Pinss)

''I SEE THE MISTAKE I DUPLICATE THE QUERY PARAMS Done lets re run 


           Try
              connection.Open()

              Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

              If count > 0 Then
                   
                 isFound = True

              End If

           Catch ex As Exception
                MessageBox.Show("Validation Error: " & ex.Message)

                Return False

           End Try




         End Using

       End Using

       Return isFound



End Function
''' '








	Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

'''''MY GIFT TO YOU BABY BY DEVKAU

     Const reward As Decimal = 100.00

    If Me.txtFirstNameSp.Text = "" Or  Me.txtEmailSp.Text = "" Or Me.txtFAccountnumSp.Text = "" Or Me.txtATMsp.Text = "" Then

       MessageBox.Show("ENTER A VALID INPUT")

     


    Else
              
       If Me.txtATMsp.Text.Length = 3 And Me.txtFAccountnumSp.Text.Length = 6 Then

      
      'DATA RETRIEVE
       Dim FullName As String = Me.txtFirstNameSp.Text
       Dim Email As String = Me.txtEmailSp.Text
       Dim ACCOUNT_NUM As String = Me.txtFAccountnumSp.Text
       Dim Atm_pIN As String = Me.txtATMsp.Text


       Dim isExit = AccountNumberExists(ACCOUNT_NUM, Atm_pIN)

              If isExit Then
                   MessageBox.Show("Enter a unique Account Number And Pin")
                               Me.txtFAccountnumSp.Clear
                               Me.txtATMsp.Clear()     
                   Return


              Else



                                     ''INSERT QUERY
                  Dim sqlQuery As String = "INSERT INTO ATMGusers (ACCOUNT_NUMBER, FULL_NAME, Email, ATM_PIN, AccountBalance) " & _
                                         "VALUES (@aacountNum, @fullName, @Email, @atmPin, @accountBalance)"

                 ''command and connection

                 Using connection As New Sqlconnection(CONNECTION_STRING)

                   Using Command As New SqlCommand(sqlQuery, connection)

                    ''adding to database
                    Command.Parameters.AddWithValue("@aacountNum", ACCOUNT_NUM)
                    Command.Parameters.AddWithValue("@fullName", FullName)
                    Command.Parameters.AddWithValue("@Email", Email)
                    Command.Parameters.AddWithValue("@atmPin", Atm_pIN)
                    Command.Parameters.AddWithValue("@accountBalance", reward)


                    ''test connection

                      Try
                         connection.Open()

                         Dim rowsAffected As Integer = command.ExecuteNonQuery()

                          If rowsAffected > 0 Then
                               MessageBox.Show("Registration Successful! You have been rewarded GHC100", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                               Me.txtFirstNameSp.clear()
                               Me.txtFAccountnumSp.Clear
                               Me.txtATMsp.Clear()
                               Me.txtEmailSp.Clear()
              
                 


              
                           End If








                      Catch ex As Exception

                        MessageBox.Show(ex.Message)

                      End Try




                   End Using






                 End Using
    






            End if

                '    ''INSERT QUERY
                '    Dim sqlQuery As String = "INSERT INTO ATMGusers (ACCOUNT_NUMBER, FULL_NAME, Email, ATM_PIN, AccountBalance) " & _
                '                           "VALUES (@aacountNum, @fullName, @Email, @atmPin, @accountBalance)"

                '   ''command and connection

                '   Using connection As New Sqlconnection(CONNECTION_STRING)

                '     Using Command As New SqlCommand(sqlQuery, connection)

                '      ''adding to database
                '      Command.Parameters.AddWithValue("@aacountNum", ACCOUNT_NUM)
                '      Command.Parameters.AddWithValue("@fullName", FullName)
                '      Command.Parameters.AddWithValue("@Email", Email)
                '      Command.Parameters.AddWithValue("@atmPin", Atm_pIN)
                '      Command.Parameters.AddWithValue("@accountBalance", reward)


                '      ''test connection

                '        Try
                '           connection.Open()

                '           Dim rowsAffected As Integer = command.ExecuteNonQuery()

                '            If rowsAffected > 0 Then
                '                 MessageBox.Show("Registration Successful! You have been rewarded GHC100", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                '                 Me.txtFirstNameSp.clear()
                '                 Me.txtFAccountnumSp.Clear
                '                 Me.txtATMsp.Clear()
                '                 Me.txtEmailSp.Clear()


                '            Else 

                '               MessageBox.Show("USE A DIFFERENT ACCOUNT NUMBER AND PIN ALREADY REGISTERED")



                '             End If








                '        Catch ex As Exception

                '          MessageBox.Show(ex.Message)

                '        End Try




                '     End Using






                '   End Using


            Else

                MessageBox.Show("Enter a Three(3) Digit Pin and a Six(6) Digit Account Number")

            End If


 


     End If



 End Sub


'LOGIN FORM

	Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click

      'RETRIEVE CHECK FROM DATABASE
           Dim inputEmail As String = txtLoginEmail.Text
           Dim inputACCOUNTnum As String = txtLoginPassword.Text

      'QUERY COMMAND
          Dim sqlQuery As String = "SELECT ID, FULL_NAME, Email, ATM_PIN, ACCOUNT_NUMBER, AccountBalance  FROM ATMGusers
 WHERE Email = @Email AND ACCOUNT_NUMBER = @AccountNum"

     'test connection

        Using connection As New SqlConnection(CONNECTION_STRING)

          Using command As New Sqlcommand(sqlQuery, connection)

            command.Parameters.AddWithValue("@Email", inputEmail)
            command.Parameters.AddWithValue("@AccountNum", inputACCOUNTnum)

             Try

              connection.Open
              ' Use ExecuteScalar or ExecuteReader for retrieval '
               Dim reader As SqlDataReader = command.ExecuteReader()

                If reader.Read Then

                  ''Retrieve username

                  Dim userName As String = reader.GetString(reader.GetOrdinal("FULL_NAME"))
                  Dim UserEmail As String = reader.GetString(reader.GetOrdinal("Email"))
                  Dim UserATM_PIN As String = reader.GetString(reader.GetOrdinal("ATM_PIN"))
                  Dim UserACCOUNT_num As String = reader.GetString(reader.GetOrdinal("ACCOUNT_NUMBER"))
                  Dim UserBalance As Decimal = reader.GetDecimal(reader.GetOrdinal("AccountBalance"))
                

                    MessageBox.Show("Login Successful! Welcome. The most Secure ATM Bank In the world", "Success")
                    ' Close the login form and open the main/dashboard form '

                    'CREATING NEW FORM INSTANCE FOR PROPERTEY RETRIEVAL

                    Dim mainForm As New GATMMENU()

                    mainForm.SuspendLayout()

                    mainForm.Visible = False
                    mainForm.ResumeLayout()
                      
                    'SETTING USERNAME TO PROPERTY
                    mainForm.AccountFullName = userName

                    mainForm.AccountEmail = UserEmail

                    mainForm.AccountATMPIN = UserATM_PIN

                    mainForm.AccountNumber = UserACCOUNT_num

                    mainForm.CurrentBalance = UserBalance


                    ''FINISH SETTING TO GATMENU PROPERTY DEVKAU

                    Me.Hide
                    mainForm.Show



                Else
                   MessageBox.Show("Invalid Email or AccountNumber entry")


                End If
                reader.Close




             Catch ex As Exception

                 MessageBox.Show(ex.Message)

             End Try






          End Using





        End Using
      



	End Sub

''checkbox reveal

	Private Sub txtCheckBox_hide_CheckedChanged(sender As Object, e As EventArgs) Handles txtCheckBox_hide.CheckedChanged
      
         If Me.txtCheckBox_hide.Checked = True Then

            Me.txtLoginPassword.PasswordChar = ""

          Else

            Me.txtLoginPassword.PasswordChar = "*"

         End If
            
	End Sub

	Private Sub GLOGIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Guna2Button1.Animated =False
        Me.txtFirstNameSp.Animated = False
        Me.txtEmailSp.Animated = False
        Me.txtFAccountnumSp.Animated = False
        Me.txtATMsp.Animated =False

	End Sub



	Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
         Application.Exit()
	End Sub
End Class
