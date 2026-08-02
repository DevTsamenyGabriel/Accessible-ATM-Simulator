Imports System.Data.SqlClient

Imports System.Speech.Synthesis



Public Class ATMMENU

''DEV AI TTS

Dim talkNow As New SpeechSynthesizer()

Dim DEVTTS_AI As Boolean = False

'' GLOBAL VARIABLES

        Private circle1 As String
        Private circle2 As String
        Private circle3 As String

        Private textForkeySwitcher As String = "launch"
        
        Private pass_cheker As String = ""

''PROPERTY GLOBAL  VARIABLES

        Private _userFullName As String
        Private  _userAccountBalance As Decimal
        Private _userATMPIN As String
        Private _userEmail as String
        Private _userAccountnum As String


''DEPOSIT AMOUNT

      Private Dim depositAmounts As String = ""

''Withdraw Amount
      Private Dim withDrawAmounts As String = ""


''-------------------------------------------------------


''CONNECTION STRING
   Public Const CONNECTION_STRING As String = "Data Source=ANONYMOUSKAU;Initial Catalog=NEWATMGABE;Persist Security Info=True;User ID=sa;Password=2025;TrustServerCertificate=True"



''' '''''''''''''''' RETRIEVINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNGGGGGGGGGGGGGG BALANCE

Private Function RetrieveUpdatedBalance() As Decimal

   Dim newBalance As Decimal

   Dim AccountNum  = Me.UserSAccountNum

   Dim query As String = "SELECT AccountBalance FROM ATMGusers WHERE ACCOUNT_NUMBER = @accNum"

   Using connection As New SqlConnection(CONNECTION_STRING)

       Using command As New SqlCommand(query, connection)

         command.Parameters.Add("@accNum", SqlDbType.VarChar).Value = AccountNum

         Try

           connection.Open()

           ''retrieve balance
            Dim resultBalance = command.ExecuteScalar()

            If resultBalance IsNot Nothing AndAlso Not DBNull.Value.Equals(resultBalance) Then

               newBalance = Convert.ToDecimal(resultBalance)

             Else
              MessageBox.Show("Error: Account balance not found in database.", "Database Lookup Error")

            End If



         Catch ex As Exception
              MessageBox.Show(ex.Message)
         End Try

       End Using


   End Using  

 Return newBalance




End Function


''LOGIC  SET ITEMS IN THE DATABASESSSSSSSSSSSSSSSSSSSSSSSSSSSS















''LOGIC FOR FORMSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

'BUTTONS----------DEFINER--------------


   Public Sub BUTTON_CLICKED(ByVal entered_button)


        If Me.textForkeySwitcher = "pin" Then

            ''nested if statement for my pin logic
            If Me.Circle_pin1.Text = "" Then
               
              Me.Circle_pin1.Text = entered_button
              Me.pass_cheker += entered_button

            
             ElseIf Me.Circle_pin2.Text = "" then
                
              Me.Circle_pin1.Text = "*"
              Me.Circle_pin2.Text = entered_button
              Me.pass_cheker += entered_button

            ElseIf Me.Circle_pin3.Text = "" then
              
              Me.Circle_pin2.Text = "*"
              Me.Circle_pin3.Text = entered_button
              Me.pass_cheker += entered_button



            End If


         

         ElseIf Me.textForkeySwitcher = "deposit" then

            If Me.Deposittxt.Text = "" Then

               Me.Deposittxt.Text = entered_button
               depositAmounts += entered_button

             Else
              Me.Deposittxt.Text += entered_button
              depositAmounts += entered_button

            End If

            
            textForkeySwitcher = "deposit"


           ''deposite into database


         ElseIf Me.textForkeySwitcher = "withdraw" then
            
 


            If Me.withdrawtxt.Text = "" Then
              
              Me.withdrawtxt.Text = entered_button
               withDrawAmounts += entered_button

            Else

                Me.withdrawtxt.Text += entered_button
                withDrawAmounts += entered_button

            End If

          
           
           textForkeySwitcher = "withdraw"



        ''mother if end
        End If





   End Sub


''switches
''balance
''menu
''mini_state
''launch



	Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        
      Call BUTTON_CLICKED("1")
      
      


	End Sub

	Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click

        Call BUTTON_CLICKED("2")

	End Sub

	Private Sub button3_Click(sender As Object, e As EventArgs) Handles button3.Click

       Call  BUTTON_CLICKED("3")

	End Sub

	Private Sub button4_Click(sender As Object, e As EventArgs) Handles button4.Click

        Call BUTTON_CLICKED("4")

	End Sub

	Private Sub button5_Click(sender As Object, e As EventArgs) Handles button5.Click

        Call BUTTON_CLICKED("5")

	End Sub

	Private Sub button6_Click(sender As Object, e As EventArgs) Handles button6.Click

		Call BUTTON_CLICKED("6")

	End Sub

	Private Sub button7_Click(sender As Object, e As EventArgs) Handles button7.Click

		Call BUTTON_CLICKED("7")

	End Sub

	Private Sub button8_Click(sender As Object, e As EventArgs) Handles button8.Click

		Call BUTTON_CLICKED("8")

	End Sub

	Private Sub button9_Click(sender As Object, e As EventArgs) Handles button9.Click

		Call BUTTON_CLICKED("9")

	End Sub

	Private Sub button0_Click(sender As Object, e As EventArgs) Handles button0.Click

		Call BUTTON_CLICKED("0")

	End Sub


''' '''''''''''''WITHDRAAAAAAAAAAAAAAAAAAAAAAAAAAWWWWWWWWWWWWWWWWWWWWWWW BUTTON LOGIC
''' 

Private Sub DirectWithdrawalUpdateBabyDevilKau(ByVal withdrawalAmount As Decimal)

       Dim AccountNum  = Me.UserSAccountNum

       ''myquery
       Dim query As String = "UPDATE ATMGusers SET AccountBalance = AccountBalance - @amount WHERE ACCOUNT_NUMBER = @accNum"

       Using connection As New SqlConnection(CONNECTION_STRING)

           Using Command As New SqlCommand(query, connection)

            command.CommandType = CommandType.Text

            command.Parameters.Add("@amount", SqlDbType.Decimal).Value = withdrawalAmount
            command.Parameters.Add("@accNum", SqlDbType.VarChar, 20).Value = AccountNum

            Try
              connection.Open()

              Dim rowsAffected As Integer = command.ExecuteNonQuery()

              If rowsAffected > 0 Then
                 If DEVTTS_AI Then
                    talkNow.SpeakAsync("Withdrawal successful! Balance updated in Account. Success")
                 End If
                 MessageBox.Show("Withdrawal successful! Balance updated in Account.", "Success")
                 Dim updatedBalance As Decimal = Decimal.Parse(Me.Balancetxt.Text) - withdrawalAmount
                 Me.Balancetxt.Text = updatedBalance
                 withDrawAmounts = ""

              End If


            Catch ex As Exception

            End Try




           End Using

       End Using




End Sub






''' '''''''''''''DEPOSITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT BUTTON LOGIC
''' 
''' 
''' 
''' 




Public Sub DirectDeposit(ByVal depositedAmount) 


    Dim AccountNum  = Me.UserSAccountNum
    
   ''mywife quesry
   Dim query As String = "UPDATE ATMGusers SET AccountBalance = AccountBalance + @amount WHERE ACCOUNT_NUMBER = @accNum"

   Using connection As New SqlConnection(CONNECTION_STRING)
    
     Using Command As New SqlCommand(query, connection)

      command.CommandType = CommandType.Text


      ''params

      command.Parameters.Add("@amount", SqlDbType.Decimal).Value = depositedAmount
      command.Parameters.Add("@accNum", SqlDbType.VarChar, 20).Value = AccountNum

      Try

       connection.Open()

       Dim rowsAffected As Integer = command.ExecuteNonQuery()

       If rowsAffected > 0 Then
                 If DEVTTS_AI Then
                    talkNow.SpeakAsync("Deposit successful! Balance updated in database. Success")
                 End If
         MessageBox.Show("Deposit successful! Balance updated in database.", "Success")
          Dim updatedeposit As Decimal = Decimal.Parse(Me.Balancetxt.Text) + depositedAmount
          Me.Balancetxt.Text = updatedeposit.ToString
          Me.depositAmounts = ""
       
       Else
         MessageBox.Show("Error: Account not found or balance not updated.", "Update Failed")
               

       End If

      Catch ex As Exception

        MessageBox.Show(ex.Message)

      End Try



     End Using





   End Using












End Sub












Public sub EnterButton()


    If textForkeySwitcher = "pin" Then

        If Me.pass_cheker = UserSATMPIN Then
           Me.panel_login.Visible = False
           textForkeySwitcher = "menu"
           disableButton()
           Me.pass_cheker = "" 

        Else
           MessageBox.Show("Wrong Pin ")
           Me.pass_cheker = ""

        End If



    ElseIf textForkeySwitcher = "withdraw" then

            ''''''''''''''''''converting the enered devkau amount to withdraw 
            '''
            If withDrawAmounts <> "" Then

               Dim withDraw As Decimal = Decimal.Parse(withDrawAmounts)
               If withdraw  < Decimal.Parse(Me.Balancetxt.Text) And withDraw > 0 Then

                 DirectWithdrawalUpdateBabyDevilKau(withDraw)

               

               Else

                 MessageBox.Show("Enter a valid Amount")
                 Me.withdrawtxt.Text = ""

               End If
            Else 
              
            MessageBox.Show("Error Enter Amount!!!")

            End If

            
             
             

            


            'MessageBox.Show("hello" & withDrawAmounts.ToString)


     ElseIf textForkeySwitcher = "deposit" then

            '''''''''''''''''''''''my fucking deposit
            '''
            
          If depositAmounts <> "" Then

               Dim Deposit As Decimal = Decimal.Parse(depositAmounts)
               If Deposit < 10000 Then

                 DirectDeposit(Deposit)

              

               Else

                 MessageBox.Show("Is your Father Elon Musk Error")
                 Me.Deposittxt.Text = ""

                End If


          Else 
              
            MessageBox.Show("Error Enter Amount!!!")
            End If
        
    



    



    End If























End sub




	Private Sub button_enter_Click(sender As Object, e As EventArgs) Handles button_enter.Click

  
            
         EnterButton()

	End Sub




''' ''''''''''''''''''''''''CLRAEEEEEEEEEEEEEEEEEERRRRRRRRRRRRRRRR FUNCTION
''' 

''switches
''balance
''menu
''mini_state
''launch

Public sub CLEARER() 

  If Me.textForkeySwitcher = "pin" Then

     me.Circle_pin1.Text = ""
     me.Circle_pin2.Text = ""
     me.Circle_pin3.Text = ""



   ElseIf Me.textForkeySwitcher = "deposit" then

    Me.Deposittxt.Text = ""


   ElseIf Me.textForkeySwitcher = "withdraw" then

     Me.withdrawtxt.Text = ""


  End If


End sub





	Private Sub button_clear_Click(sender As Object, e As EventArgs) Handles button_clear.Click

      
      call CLEARER()

	End Sub


''---------------------------------------------------------------
''PROPERTY DEFINITION FOR DEVKAU ATMMENU
''---------------------------------------------------------------
''username getter
 Public Property UserSFullName() As String
        Get
            Return _userFullName
        End Get
        Set(value As String)
            _userFullName = value
        End Set
    End Property


''user Balance getter
Public Property UserSBalane() As Decimal
        Get
            Return _userAccountBalance
        End Get
        Set(value As Decimal)
            _userAccountBalance = value
        End Set
    End Property


''user pin
Public Property  UserSATMPIN() As String
        Get
            Return _userATMPIN
        End Get
        Set(value As String)
            _userATMPIN = value
        End Set
    End Property


''USER EMAIL


Public Property UserSEmail() As String
        Get
            Return _userEmail
        End Get
        Set(value As String)
            _userEmail = value
        End Set
    End Property


''USER  ACCOUNT NUMBER


Public Property UserSAccountNum() As String
        Get
            Return _userAccountnum
        End Get
        Set(value As String)
            _userAccountnum = value
        End Set
    End Property








''ATM LOADER CONSTRUCTOR




	Private Sub ATMMENU_Load(sender As Object, e As EventArgs) Handles MyBase.Load

     'Me.txtSwitcher.Text = UserSAccountNum
     'textForkeySwitcher = "launch"


	End Sub


    ''---------------------------------------------------------------------------





    'LOADER TIMER'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Me.bar_loading.Width += 3

        If Me.bar_loading.Width >= Me.bar_Luncher.Width

            Timer1.Enabled = False
            textForkeySwitcher = "pin"
            Me.panel_Luncher.Visible = False


        End If

        disableButton()

    End Sub



    'disable button======----------------------------------------------====================-------------------------------------------

    Private sub disableButton()

    If textForkeySwitcher = "launch" Or textForkeySwitcher = "balance" Or textForkeySwitcher = "menu" Or textForkeySwitcher = "mini_state" Then
           Me.button1.Enabled = False
           Me.button2.Enabled = False
           Me.button3.Enabled = False
           Me.button4.Enabled = False
           Me.button5.Enabled = False
           Me.button6.Enabled = False
           Me.button7.Enabled = False
           Me.button8.Enabled = False
           Me.button9.Enabled = False
           Me.button0.Enabled = False
           Me.button_clear.Enabled = False
           Me.button_enter.Enabled = False

    Else

           Me.button1.Enabled = True
           Me.button2.Enabled = True
           Me.button3.Enabled = True
           Me.button4.Enabled = True
           Me.button5.Enabled = True
           Me.button6.Enabled = True
           Me.button7.Enabled = True
           Me.button8.Enabled = True
           Me.button9.Enabled = True
           Me.button0.Enabled = True
           Me.button_clear.Enabled = True
           Me.button_enter.Enabled = True


   

    End If




    End sub


''''MENU           CHOICES BUTTONSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


	Private Sub Choice_Blance_Click(sender As Object, e As EventArgs) Handles Choice_Blance.Click
talkNow.SpeakAsyncCancelAll

     Me.panel_Balance.Visible = True
     'Me.panel_Deposit.Visible = False
     Me.panel_Menu.Visible = False
     'Me.panel_withdraw.Visible = False
     'Me.panel_statement.Visible = False
     'Me.panel_login.Visible = False

     Me.textForkeySwitcher = "balance"
     disableButton()
     Dim returnedBalance = RetrieveUpdatedBalance()


     Me.Balancetxt.Text = returnedBalance
     If Me.textForkeySwitcher = "balance" And DEVTTS_AI = True Then

        talkNow.SpeakAsync("Availaable BALANCE IS, " & Me.Balancetxt.Text)
     End If

	End Sub

	Private Sub Choice_Menu_Click(sender As Object, e As EventArgs) Handles choice_withdraw.Click
       talkNow.SpeakAsyncCancelAll
        Me.panel_Menu.Visible = False
        Me.panel_withdraw.Visible = True
        Me.panel_Balance.Visible = False
        Me.withdrawtxt.Text = ""

        Me.textForkeySwitcher = "withdraw"
     If Me.textForkeySwitcher = "withdraw" And DEVTTS_AI = True  Then

        talkNow.SpeakAsync("Enter amount to withdraw")
     End If
        Me.withdrawtxt.ReadOnly = True

        disableButton()

      
	End Sub

	Private Sub Choice_Deposit_Click(sender As Object, e As EventArgs) Handles Choice_Deposit.Click
          
       Me.panel_Menu.Visible = False
       Me.panel_Balance.Visible = False
       Me.panel_withdraw.Visible = False
       Me.panel_Deposit.Visible = True
       Me.Deposittxt.Text = ""
       
       Me.textForkeySwitcher = "deposit"
     If Me.textForkeySwitcher = "deposit" And DEVTTS_AI = True  Then

        talkNow.SpeakAsync("Enter amount to deposit")
     End If
       disableButton()

	End Sub

	Private Sub Choice_AccountInfo_Click(sender As Object, e As EventArgs) Handles Choice_AccountInfo.Click

       Me.panel_Menu.Visible = False
       Me.panel_Balance.Visible = False
       Me.panel_withdraw.Visible = False
       Me.panel_Deposit.Visible = False
       Me.panel_statement.Visible = True

        Me.miniNametxt.Text = UserSFullName
        Me.miniEmailtxt.Text = UserSEmail
        Me.miniPintxt.Text = UserSATMPIN
        Me.miniAccountNum.Text = UserSAccountNum

        Me.textForkeySwitcher = "mini_state"




	End Sub




''' ''''''''''''''''''''''''''''''''''''''''''FUNCTION TO GO HOME-------------------
''' 

Public sub GoHome()

       Me.panel_Menu.Visible = True
       Me.panel_Balance.Visible = True
       Me.panel_withdraw.Visible = True
       Me.panel_Deposit.Visible = True
       Me.panel_statement.Visible = True
       Me.textForkeySwitcher = "launch"
       
        disableButton()





End sub








	Private Sub homeBalance_Click(sender As Object, e As EventArgs) Handles homeBalance.Click
        GoHome()
	End Sub

	Private Sub homewithdraw_Click(sender As Object, e As EventArgs) Handles homewithdraw.Click 
          GoHome()
	End Sub

	Private Sub homeDeposit_Click(sender As Object, e As EventArgs) Handles homeDeposit.Click
           GoHome()
	End Sub

	Private Sub homeStatement_Click(sender As Object, e As EventArgs) Handles homeStatement.Click
       GoHome()
	End Sub

	Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
       Me.Hide

        Dim hompe As New GATMMENU()
        hompe.AccountFullName = Me.UserSFullName
        hompe.Show()
        
        
        Me.Close

	End Sub



''' OOOHHHHHHHH DEVIL KAU YOUR LOGIC FOR THE TEXT TO SPEECHHHHHHHHH''''''''''
private  sub USETTS(ByVal WORDS) 

  If DEVTTS_AI = True Then
   
   talkNow.Rate = 1
   talkNow.SpeakAsync(WORDS)


  End If



End sub





	Private Sub Choice_Blance_MouseEnter(sender As Object, e As EventArgs) Handles Choice_Blance.MouseEnter
       
      Call USETTS(Me.Choice_Blance.Text)

	End Sub

	Private Sub choice_withdraw_MouseEnter(sender As Object, e As EventArgs) Handles choice_withdraw.MouseEnter

      Call USETTS(Me.choice_withdraw.Text)

	End Sub

	Private Sub Choice_Deposit_MouseEnter(sender As Object, e As EventArgs) Handles Choice_Deposit.MouseEnter

     Call USETTS(Me.Choice_Deposit.Text)

	End Sub

	Private Sub Choice_AccountInfo_MouseEnter(sender As Object, e As EventArgs) Handles Choice_AccountInfo.MouseEnter

     Call USETTS(Me.Choice_AccountInfo.Text)

	End Sub



''' OHHHHHHHHH DEVVVVVVVV CANCEL SPEECH LOGIC '''''''JUST CANCEL ASCY
''' 
Private sub CancelSpeecch() 

  If DEVTTS_AI = True

    talkNow.SpeakAsyncCancelAll()
  End If

End sub




	Private Sub Choice_Blance_MouseLeave(sender As Object, e As EventArgs) Handles Choice_Blance.MouseLeave

     CancelSpeecch() 

	End Sub

	Private Sub choice_withdraw_MouseLeave(sender As Object, e As EventArgs) Handles choice_withdraw.MouseLeave

     CancelSpeecch() 

	End Sub

	Private Sub Choice_Deposit_MouseLeave(sender As Object, e As EventArgs) Handles Choice_Deposit.MouseLeave

     CancelSpeecch() 

	End Sub

	Private Sub Choice_AccountInfo_MouseLeave(sender As Object, e As EventArgs) Handles Choice_AccountInfo.MouseLeave

     CancelSpeecch() 

	End Sub


''CHECK BOX BABAY DEV

	Private Sub DEVAI_CheckedChanged(sender As Object, e As EventArgs) Handles DEVAI.CheckedChanged
       
        If DEVAI.Checked = True Then
           DEVTTS_AI = True

        Else 

          DEVTTS_AI = False
        End If

	End Sub

	Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click

	End Sub
End Class



'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''