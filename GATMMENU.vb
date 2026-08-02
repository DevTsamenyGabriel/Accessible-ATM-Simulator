Imports System.Linq.Expressions

Public Class GATMMENU

     'retrieve username forom login
     'Public Property AccountFullName As String

'PROPERTIES TO RETRIEVE DATA FROM DEVKAU DATABASE
     Private _currentAccountname As String
     Private _currentAccountBalance As Decimal
     Private _currentAccountEmail As String
     Private _currentAccountNumber As String
     Private _currentAccountATMPIN As String

    '----------------------------------------------------------------
    '                DEVKAU     PROPERTEY        
    '----------------------------------------------------------------
    ''AccountFullName   PROPERTEY
    Public Property AccountFullName() As String '  The Public Property 
        Get
            Return _currentAccountname
        End Get
        Set(value As String)
            _currentAccountname = value
        End Set
    End Property

    '-----------------------------------------------------------------

    ''AccountBalance    PROPERTEY

    Public Property CurrentBalance() As Decimal
        Get
            Return _currentAccountBalance
        End Get
        Set(value As Decimal)
            _currentAccountBalance = value
        End Set
    End Property

''-----------------------------------------------------------------

''AccountEmail   PROPERTEY


Public Property AccountEmail() As String

   Get 
        Return _currentAccountEmail
   End Get
    Set(value As String)
         _currentAccountEmail = value
    End Set

End Property

''--------------------------------------------------------------------

''AccountNumber   PROPERTEY


Public Property AccountNumber() As String

  Get
      Return _currentAccountNumber
  End Get
    Set(value As String)
       _currentAccountNumber = value

    End Set
End Property


''AccountATMPIN   PROPERTEY


Public Property AccountATMPIN() As String
    
    Get
        Return _currentAccountATMPIN

    End Get
    Set(value As String)

      _currentAccountATMPIN = value

    End Set
End Property



''END-----------------OF PROPETY DEFINITION--------------------------------------------------



''RETRIEVING DATA FROM PROPERTY DEVKAU



  'Dim FULLNAME As String = Me.AccountFullName
  Dim EMAIL = Me.AccountEmail
  Dim BALANCE As Decimal = Me.CurrentBalance
  Dim ACCOUNTNUM As String = Me.AccountNumber
  Dim ATMPIN As String = Me.AccountATMPIN



   

	Private Sub GATMMENU_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.txtWelcome1.Text = AccountFullName



    End Sub

''ATM LUNCHER BUTTON

	Private Sub btnATM_LUNCHER_Click(sender As Object, e As EventArgs) Handles btnATM_LUNCHER.Click

      Dim MAIN_ATMLUNCHFORM As New ATMMENU()

      MAIN_ATMLUNCHFORM.Visible = False ' <--- ADD THIS LINE

    MAIN_ATMLUNCHFORM.SuspendLayout()

''PASS RETRIVEING DATA TO ATMMENU DEVKAU

     MAIN_ATMLUNCHFORM.UserSFullName = AccountFullName
     MAIN_ATMLUNCHFORM.UserSEmail = Me.AccountEmail
     MAIN_ATMLUNCHFORM.UserSBalane = Me.CurrentBalance
     MAIN_ATMLUNCHFORM.UserSATMPIN = Me.AccountATMPIN
     MAIN_ATMLUNCHFORM.UserSAccountNum = Me.AccountNumber

    

     MAIN_ATMLUNCHFORM.ResumeLayout()
    
     MAIN_ATMLUNCHFORM.Show
     Me.Hide
          


	End Sub

	Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click

          Me.Hide

          Dim LoginMenu As new GLOGIN()


          LoginMenu.Show



	End Sub









End Class