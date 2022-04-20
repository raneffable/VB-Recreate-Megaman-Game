Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks


Public Class Form1



    Dim bmp As Bitmap
    Dim Bg, Img As CImage
    Dim SpriteMap As CImage
    Dim SpriteMask As CImage
    Dim OctopusIntro, OctopusIntroDown, OctopusStand, OctopusLeap, OctopusShoot, OctopusWhirpool, OctopusPiranha, OctopusLeapShoot As CArrFrame
    Dim OctopusProjCreate1, OctopusProjUp, OctopusProjDown, OctopusPiranhaCreate1, OctopusPiranhaZonk As CArrFrame
    Dim MegamanStand, MegamanDie As CArrFrame

    Public Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Space Then
            SM.State(StateOctopus.Leap, 6)

        ElseIf e.KeyCode = Keys.W Then
            SM.State(StateOctopus.Shoot, 3)
            'If SP.PosX <= dummy.PosX Then
            '    dummy.State(StateMegaman.Die, 1)
            'End If

            'If SM.CurrState = StateOctopus.Stand And SM.PosY <= 50 Then
            '    SM.State(StateOctopus.Leap, 6)
            'End If
        End If
        If e.KeyCode = Keys.Q Then
            SM.State(StateOctopus.Whirpool, 4)
            If dummy.PosX = SM.PosX Then
                dummy.State(StateMegaman.Die, 1)

            End If
            'If dummy.PosX <= SM.PosX Then
            '    dummy.PosX = dummy.PosX + 5
            'ElseIf dummy.PosX >= SM.PosX Then
            '    dummy.PosX = dummy.PosX - 5


            'End If
        End If
        'If e.KeyCode = Keys.R Then
        '    dummy.State(StateMegaman.Stand, 0)
        '    Megaman()

        'End If
        If e.KeyCode = Keys.A Then
            SM.FDir = FaceDir.Left
            SM.PosX = SM.PosX - 10
            If SM.PosX <= 50 Then
                SM.PosX = 50
            End If
        End If
        If e.KeyCode = Keys.D Then
            SM.FDir = FaceDir.Right
            SM.PosX = SM.PosX + 10
            If SM.PosX >= 200 Then
                SM.PosX = 200
            End If
        End If

        If e.KeyCode = Keys.E Then
            SM.State(StateOctopus.Piranha, 5)
        End If

    End Sub



    Dim ListChar As New List(Of CCharacter)
    Public SM As CCharOctopus
    Public dummy As CCharMegaman

    Dim SP As CCharOctopusProjectile

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtX1.Visible = False
        txtX2.Visible = False
        txtY1.Visible = False
        txtY2.Visible = False

        'open image for background, assign to bg

        Randomize()

        Bg = New CImage
        Bg.OpenImage("Z:\College University Sem1-10\5.Semester V\Computer Graphic Animatiom\PA2\background.bmp")

        Bg.CopyImg(Img)

        SpriteMap = New CImage
        SpriteMap.OpenImage("Z:\College University Sem1-10\5.Semester V\Computer Graphic Animatiom\PA2\Gaya-cumi2new.bmp")

        SpriteMap.CreateMask(SpriteMask)

        'initialize sprites for Octopus
        OctopusIntroDown = New CArrFrame
        OctopusIntroDown.Insert(39, 387, 7, 357, 83, 414, 1)
        OctopusIntroDown.Insert(122, 388, 93, 359, 161, 415, 1)
        OctopusIntroDown.Insert(201, 392, 172, 362, 239, 416, 1)
        OctopusIntroDown.Insert(287, 395, 254, 363, 332, 419, 1)

        OctopusIntro = New CArrFrame
        OctopusIntro.Insert(44, 35, 5, 2, 75, 55, 2)
        OctopusIntro.Insert(123, 39, 82, 2, 153, 59, 2)
        OctopusIntro.Insert(198, 39, 161, 4, 229, 59, 2)
        OctopusIntro.Insert(273, 39, 236, 4, 302, 58, 4)
        OctopusIntro.Insert(349, 39, 310, 4, 379, 58, 7)

        OctopusStand = New CArrFrame
        OctopusStand.Insert(248, 264, 227, 221, 281, 282, 2)
        OctopusStand.Insert(316, 264, 290, 222, 346, 283, 2)

        OctopusLeapShoot = New CArrFrame
        OctopusLeapShoot.Insert(279, 329, 253, 295, 310, 354, 1)
        OctopusLeapShoot.Insert(351, 330, 325, 294, 383, 355, 1)

        OctopusLeap = New CArrFrame
        OctopusLeap.Insert(419, 329, 393, 291, 455, 354, 1)

        OctopusShoot = New CArrFrame
        OctopusShoot.Insert(585, 256, 560, 223, 616, 275, 2)
        OctopusShoot.Insert(41, 253, 16, 218, 71, 274, 2)


        OctopusPiranha = New CArrFrame
        OctopusPiranha.Insert(402, 255, 358, 226, 455, 274, 2)
        OctopusPiranha.Insert(507, 257, 470, 227, 548, 275, 2)


        OctopusWhirpool = New CArrFrame
        OctopusWhirpool.Insert(17, 105, 12, 77, 40, 130, 1)
        OctopusWhirpool.Insert(86, 100, 54, 72, 117, 127, 1)
        OctopusWhirpool.Insert(168, 107, 128, 74, 208, 131, 1)
        OctopusWhirpool.Insert(250, 106, 221, 77, 280, 133, 1)
        OctopusWhirpool.Insert(321, 107, 299, 79, 325, 134, 1)
        OctopusWhirpool.Insert(365, 105, 341, 75, 395, 130, 1)
        OctopusWhirpool.Insert(55, 178, 14, 147, 92, 204, 1)

        SM = New CCharOctopus
        ReDim SM.ArrSprites(7)
        SM.ArrSprites(0) = OctopusIntroDown
        SM.ArrSprites(1) = OctopusIntro
        SM.ArrSprites(2) = OctopusStand
        SM.ArrSprites(3) = OctopusShoot
        SM.ArrSprites(4) = OctopusWhirpool
        SM.ArrSprites(5) = OctopusPiranha
        SM.ArrSprites(6) = OctopusLeap
        SM.ArrSprites(7) = OctopusLeapShoot

        SM.PosX = 190
        SM.PosY = 40
        SM.Vx = 0
        SM.Vy = 0
        SM.State(StateOctopus.IntroDown, 0)
        SM.FDir = FaceDir.Left

        ListChar.Add(SM)

        'If SM.CurrState = StateOctopus.Stand Then
        '    Megaman()
        'End If
        Megaman()
        'initialize sprites for Sprite Projectiles
        OctopusProjCreate1 = New CArrFrame
        OctopusProjCreate1.Insert(451, 53, 449, 51, 460, 57, 2)

        OctopusProjUp = New CArrFrame
        OctopusProjUp.Insert(451, 53, 449, 51, 460, 57, 2)

        OctopusPiranhaCreate1 = New CArrFrame
        OctopusPiranhaCreate1.Insert(409, 52, 407, 43, 417, 56, 2)

        OctopusPiranhaZonk = New CArrFrame
        OctopusPiranhaZonk.Insert(1, 1, 1, 1, 1, 1, 1)

        OctopusProjDown = New CArrFrame
        OctopusProjDown.Insert(451, 53, 449, 51, 460, 57, 2)

        bmp = New Bitmap(Img.Width, Img.Height)

        DisplayImg()
        ResizeImg()


        Timer1.Enabled = True
    End Sub
    Sub Megaman()
        dummy = New CCharMegaman

        'MegamanStand = New CArrFrame
        'MegamanStand.Insert(38, 700, 20, 675, 53, 714, 1)
        ' MegamanStand.Insert(38, 700, 20, 675, 53, 714, 1)

        'MegamanDie = New CArrFrame
        'MegamanDie.Insert(83, 696, 63, 675, 105, 707, 7)
        'MegamanDie.Insert(137, 696, 118, 666, 157, 714, 8)
        'MegamanDie.Insert(186, 694, 162, 667, 208, 716, 8)
        'MegamanDie.Insert(236, 691, 211, 614, 258, 715, 8)

        MegamanStand = New CArrFrame
        'MegamanStand.Insert(1106, 47, 1087, 20, 1126, 63, 1)
        MegamanStand.Insert(1158, 47, 1138, 20, 1176, 63, 1)
        'MegamanStand.Insert(1207, 47, 1185, 20, 1227, 63, 1)

        MegamanDie = New CArrFrame
        MegamanDie.Insert(806, 426, 783, 398, 821, 445, 8)
        MegamanDie.Insert(855, 424, 827, 402, 870, 445, 8)
        MegamanDie.Insert(905, 422, 877, 402, 917, 445, 8)
        MegamanDie.Insert(956, 422, 924, 389, 973, 449, 8)

        dummy.PosX = 40
        dummy.PosY = 175
        'dummy.State(StateMegaman.Stand, 0)

        ReDim dummy.ArrSprites(1)
        dummy.ArrSprites(0) = MegamanStand
        dummy.ArrSprites(1) = MegamanDie
        ListChar.Add(dummy)


    End Sub

    Sub PutSprites()
        Dim cc As CCharacter

        'set background
        'Dim w, h As Integer
        'w = Img.Width - 1
        'h = Img.Height - 1

        'Parallel.For(0, w - 1, _
        '   Sub(i)
        '     Parallel.For(0, h - 1, _
        '       Sub(j)
        '         For j = 0 To h - 1
        '           Img.Elmt(i, j) = Bg.Elmt(i, j)
        '         Next

        '       End Sub)
        '   End Sub)


        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                Img.Elmt(i, j) = Bg.Elmt(i, j)
            Next
        Next


        For Each cc In ListChar
            Dim EF As CElmtFrame = cc.ArrSprites(cc.IdxArrSprites).Elmt(cc.FrameIdx)
            Dim spritewidth = EF.Right - EF.Left
            Dim spriteheight = EF.Bottom - EF.Top
            If cc.FDir = FaceDir.Left Then
                Dim spriteleft As Integer = cc.PosX - EF.CtrPoint.x + EF.Left
                Dim spritetop As Integer = cc.PosY - EF.CtrPoint.y + EF.Top
                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Left + i, EF.Top + j))
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Left + i, EF.Top + j))
                    Next
                Next
            Else 'facing right
                Dim spriteleft = cc.PosX + EF.CtrPoint.x - EF.Right
                Dim spritetop = cc.PosY - EF.CtrPoint.y + EF.Top
                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Right - i, EF.Top + j))
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Right - i, EF.Top + j))
                    Next
                Next

            End If

        Next


    End Sub

    Sub DisplayImg()
        'display bg and sprite on picturebox
        Dim i, j As Integer
        'Dim sw As New System.Diagnostics.Stopwatch

        'sw.Start()

        PutSprites()



        Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpdata As System.Drawing.Imaging.BitmapData = bmp.LockBits(rect,
     System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat)

        'Get the address of the first line.
        Dim ptr As IntPtr = bmpdata.Scan0


        'Declare an array to hold the bytes of the bitmap.
        Dim bytes As Integer = Math.Abs(bmpdata.Stride) * bmp.Height
        Dim rgbvalues(bytes) As Byte


        'Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes)

        Dim n As Integer = 0
        Dim col As System.Drawing.Color

        'Set every third value to 255. A 24bpp bitmap will look red.  
        For j = 0 To Img.Height - 1
            For i = 0 To Img.Width - 1
                col = Img.Elmt(i, j)
                rgbvalues(n) = col.B
                rgbvalues(n + 1) = col.G
                rgbvalues(n + 2) = col.R
                rgbvalues(n + 3) = col.A

                n = n + 4
            Next
        Next

        'Copy the RGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes)


        'Unlock the bits.
        bmp.UnlockBits(bmpdata)

        'Timer1.Enabled = False

        'MsgBox(CStr(bmp.GetPixel(0, 0).A) + " " + CStr(bmp.GetPixel(0, 0).R) + " " + CStr(bmp.GetPixel(0, 0).G) + " " + CStr(bmp.GetPixel(0, 0).B))
        'MsgBox(CStr(bmp.GetPixel(1, 0).A) + " " + CStr(bmp.GetPixel(1, 0).R) + " " + CStr(bmp.GetPixel(1, 0).G) + " " + CStr(bmp.GetPixel(1, 0).B))



        PictureBox1.Refresh()

        PictureBox1.Image = bmp
        PictureBox1.Width = bmp.Width
        PictureBox1.Height = bmp.Height
        PictureBox1.Top = 0
        PictureBox1.Left = 0





    End Sub



    Sub ResizeImg()
        Dim w, h As Integer

        w = PictureBox1.Width
        h = PictureBox1.Height

        Me.ClientSize = New Size(w, h)

    End Sub




    Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim CC As CCharacter

        PictureBox1.Refresh()

        For Each CC In ListChar
            CC.Update()

        Next

        If SM.CurrState = StateOctopus.Shoot And SM.CurrFrame = 1 Then
            CreateOctopusProjectile(1)

        End If
        If SM.CurrState = StateOctopus.Piranha And SM.CurrFrame = 1 Then
            CreateOctopusProjectile(2)
        End If

        Dim Listchar1 As New List(Of CCharacter)

        For Each CC In ListChar
            If Not CC.Destroy Then
                Listchar1.Add(CC)
            End If
        Next

        ListChar = Listchar1

        DisplayImg()

    End Sub

    Sub CreateOctopusProjectile(n As Integer)
        Dim SP As CCharOctopusProjectile
        Dim SP1 As CCharOctopusProjectile
        Dim SP2 As CCharOctopusProjectile
        Dim SP3 As CCharOctopusProjectile
        Dim SP4 As CCharOctopusProjectile

        SP4 = New CCharOctopusProjectile
        SP3 = New CCharOctopusProjectile
        SP2 = New CCharOctopusProjectile
        SP1 = New CCharOctopusProjectile
        SP = New CCharOctopusProjectile



        If SM.FDir = FaceDir.Left Then
            SP.PosX = SM.PosX - 20
            SP.FDir = FaceDir.Left
            SP1.PosX = SM.PosX - 20
            SP1.FDir = FaceDir.Left
            SP2.PosX = SM.PosX - 20
            SP2.FDir = FaceDir.Left
            SP3.PosX = SM.PosX + 20
            SP3.FDir = FaceDir.Right
            SP4.PosX = SM.PosX + 20
            SP4.FDir = FaceDir.Right

        Else
            SP.PosX = SM.PosX + 20
            SP.FDir = FaceDir.Right
            SP1.PosX = SM.PosX + 20
            SP1.FDir = FaceDir.Right
            SP2.PosX = SM.PosX + 20
            SP2.FDir = FaceDir.Right
            SP3.PosX = SM.PosX - 20
            SP3.FDir = FaceDir.Left
            SP4.PosX = SM.PosX - 20
            SP4.FDir = FaceDir.Left

        End If

        SP4.PosY = SM.PosY + 5
        SP3.PosY = SM.PosY - 10
        SP2.PosY = SM.PosY + 20
        SP1.PosY = SM.PosY - 10
        SP.PosY = SM.PosY + 5

        SP1.Vx = 0
        SP1.Vy = -5
        SP.Vx = 0
        SP.Vy = 0
        SP2.Vx = 0
        SP2.Vy = 5
        SP3.Vx = 0
        SP3.Vy = -5
        SP4.Vx = 0
        SP4.Vy = 0



        SP4.CurrState = StateOctopusProjectile.Create
        ReDim SP4.ArrSprites(1)
        If n = 1 Then
            SP4.ArrSprites(0) = OctopusPiranhaZonk
            SP4.ArrSprites(1) = OctopusPiranhaZonk
        ElseIf n = 2 Then
            SP4.ArrSprites(0) = OctopusPiranhaCreate1
            SP4.ArrSprites(1) = OctopusPiranhaCreate1
        End If


        SP3.CurrState = StateOctopusProjectile.Create
        ReDim SP3.ArrSprites(1)
        If n = 1 Then
            SP3.ArrSprites(0) = OctopusPiranhaZonk
            SP3.ArrSprites(1) = OctopusPiranhaZonk
        ElseIf n = 2 Then
            SP3.ArrSprites(0) = OctopusPiranhaCreate1
            SP3.ArrSprites(1) = OctopusPiranhaCreate1
        End If

        SP2.CurrState = StateOctopusProjectile.Create
        ReDim SP2.ArrSprites(1)
        If n = 1 Then
            SP2.ArrSprites(0) = OctopusProjCreate1
            SP2.ArrSprites(1) = OctopusProjCreate1
        ElseIf n = 2 Then

            SP2.ArrSprites(0) = OctopusPiranhaZonk
            SP2.ArrSprites(1) = OctopusPiranhaZonk
        End If

        SP1.CurrState = StateOctopusProjectile.Create
        ReDim SP1.ArrSprites(1)
        If n = 1 Then
            SP1.ArrSprites(0) = OctopusProjCreate1
            SP1.ArrSprites(1) = OctopusProjCreate1
        ElseIf n = 2 Then
            SP1.ArrSprites(0) = OctopusPiranhaCreate1
            SP1.ArrSprites(1) = OctopusPiranhaCreate1
        End If

        SP.CurrState = StateOctopusProjectile.Create
        ReDim SP.ArrSprites(1)
        If n = 1 Then
            SP.ArrSprites(0) = OctopusProjCreate1
            SP.ArrSprites(1) = OctopusProjCreate1
        ElseIf n = 2 Then
            SP.ArrSprites(0) = OctopusPiranhaCreate1
            SP.ArrSprites(1) = OctopusPiranhaCreate1
        End If


        'If SP.PosX <= dummy.PosX Then
        '    dummy.State(StateMegaman.Die, 1)
        'End If
        ListChar.Add(SP4)
        ListChar.Add(SP3)
        ListChar.Add(SP2)
        ListChar.Add(SP1)
        ListChar.Add(SP)
    End Sub


    Private Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        txtX1.Text = e.X
        txtY1.Text = e.Y
    End Sub



    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        txtX2.Text = e.X
        txtY2.Text = e.Y
    End Sub
End Class


