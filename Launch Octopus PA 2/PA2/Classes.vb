Imports System.IO


Public Enum StateMegaman
    Stand
    Die
End Enum
Public Enum StateOctopus
    Stand
    Leap
    Shoot
    Whirpool
    Intro
    Piranha
    IntroDown
    LeapShoot
End Enum

Public Enum StateOctopusProjectile
    Create
    Move
End Enum


Public Enum FaceDir
    Left
    Right
End Enum

Public Class CImage
    Public Width As Integer
    Public Height As Integer
    Public Elmt(,) As System.Drawing.Color
    Public ColorMode As Integer 'not used

    Sub OpenImage(ByVal FName As String)
        'open file .bmp, read file
        Dim s As String
        Dim L As Long
        Dim BR As BinaryReader
        Dim h, w, pos As Integer
        Dim r, g, b As Integer
        Dim pad As Integer

        BR = New BinaryReader(File.Open(FName, FileMode.Open))

        Try
            BlockRead(BR, 2, s)

            If s <> "BM" Then
                MsgBox("Not a BMP file")
            Else 'BMP file
                BlockReadInt(BR, 4, L) 'size
                'MsgBox("Size = " + CStr(L))
                BlankRead(BR, 4) 'reserved
                BlockReadInt(BR, 4, pos) 'start of data
                BlankRead(BR, 4) 'size of header
                BlockReadInt(BR, 4, Width) 'width
                'MsgBox("Width = " + CStr(I.Width))
                BlockReadInt(BR, 4, Height) 'height
                'MsgBox("Height = " + CStr(I.Height))
                BlankRead(BR, 2) 'color panels
                BlockReadInt(BR, 2, ColorMode) 'colormode
                If ColorMode <> 24 Then
                    MsgBox("Not a 24-bit color BMP")
                Else

                    BlankRead(BR, pos - 30)

                    ReDim Elmt(Width - 1, Height - 1)
                    pad = (4 - (Width * 3 Mod 4)) Mod 4

                    For h = Height - 1 To 0 Step -1
                        For w = 0 To Width - 1
                            BlockReadInt(BR, 1, b)
                            BlockReadInt(BR, 1, g)
                            BlockReadInt(BR, 1, r)
                            Elmt(w, h) = Color.FromArgb(r, g, b)

                        Next
                        BlankRead(BR, pad)

                    Next

                End If

            End If

        Catch ex As Exception
            MsgBox("Error")

        End Try

        BR.Close()


    End Sub


    Sub CreateMask(ByRef Mask As CImage)
        'create mask from *this*
        Dim i, j As Integer

        Mask = New CImage
        Mask.Width = Width
        Mask.Height = Height

        ReDim Mask.Elmt(Mask.Width - 1, Mask.Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Elmt(i, j).R = 0 And Elmt(i, j).G = 0 And Elmt(i, j).B = 0 Then
                    Mask.Elmt(i, j) = Color.FromArgb(255, 255, 255)
                Else
                    Mask.Elmt(i, j) = Color.FromArgb(0, 0, 0)
                End If
            Next
        Next

    End Sub


    Sub CopyImg(ByRef Img As CImage)
        'copies image to Img
        Img = New CImage
        Img.Width = Width
        Img.Height = Height
        ReDim Img.Elmt(Width - 1, Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Img.Elmt(i, j) = Elmt(i, j)
            Next
        Next

    End Sub

End Class

Public Class CCharacter
    Public PosX, PosY As Double
    Public Vx, Vy As Double
    Public FrameIdx As Integer
    Public CurrFrame As Integer
    Public ArrSprites() As CArrFrame
    Public IdxArrSprites As Integer
    Public FDir As FaceDir
    Public Destroy As Boolean = False


    Public Const gravity = 1

    'Public CurrState as ?

    Public Sub GetNextFrame()
        CurrFrame = CurrFrame + 1
        If CurrFrame = ArrSprites(IdxArrSprites).Elmt(FrameIdx).MaxFrameTime Then
            FrameIdx = FrameIdx + 1
            If FrameIdx = ArrSprites(IdxArrSprites).N Then
                FrameIdx = 0
            End If
            CurrFrame = 0

        End If

    End Sub

    Public Overridable Sub Update()

    End Sub


End Class

Public Class CCharMegaman
    Inherits CCharacter
    Public CurrState As StateMegaman
    Public Sub State(state As StateMegaman, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub
    Public Overrides Sub Update()
        Select Case CurrState
            Case StateMegaman.Stand
                GetNextFrame()
                'If dummy.posx <= SM.posx Then

                'End If

            Case StateMegaman.Die
                GetNextFrame()
                If FrameIdx = 3 Then
                    State(StateMegaman.Stand, 0)
                    'Destroy = True
                End If
        End Select
    End Sub
End Class
Public Class CCharOctopus
    Inherits CCharacter

    Public CurrState As StateOctopus

    Public Sub State(state As StateOctopus, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub


    Public Overrides Sub Update()
        Select Case CurrState
            Case StateOctopus.IntroDown

                PosY = PosY + Vy
                'FrameIdx = 0, CurrFrame = 0
                GetNextFrame()
                If PosY <= 170 Then
                    Vy = 5
                Else
                    State(StateOctopus.Intro, 1)

                    Vy = 0
                End If

            Case StateOctopus.Intro
                GetNextFrame()
                If FrameIdx = 4 Then
                    State(StateOctopus.Stand, 2)
                End If

            Case StateOctopus.Stand
                GetNextFrame()
                Vy = -8
                If FDir = FaceDir.Right Then
                    Vx = 10
                Else
                    Vx = -10
                End If

            Case StateOctopus.Leap
                GetNextFrame()
                PosY = PosY + Vy
                PosX = PosX + Vx

                If PosY <= 50 Then
                    PosY = 50
                    Vy = 8
                End If
                If PosY >= 170 Then
                    State(StateOctopus.Stand, 2)
                    GetNextFrame()
                    PosY = 170
                    Vy = 0

                End If
                If PosX <= 50 Then
                    PosX = 50
                    Vx = 0
                ElseIf PosX >= 200 Then
                    PosX = 200
                    Vx = 0
                End If

            Case StateOctopus.Shoot
                GetNextFrame()
                If FrameIdx = 1 Then
                    State(StateOctopus.Stand, 2)
                End If

            Case StateOctopus.Whirpool
                GetNextFrame()
                PosY = PosY + Vy
                Vx = 0
                If Form1.dummy.PosX = PosX Then
                    Form1.dummy.State(StateMegaman.Die, 1)

                End If
                If Form1.dummy.PosX <= PosX Then

                    Form1.dummy.PosX = Form1.dummy.PosX + 10
                ElseIf Form1.dummy.PosX >= PosX Then
                    Form1.dummy.PosX = Form1.dummy.PosX - 10

                End If


                If PosY <= 50 Then
                    Vy = 8
                ElseIf PosY >= 170 Then
                    Vy = -8
                End If

            Case StateOctopus.Piranha
                GetNextFrame()
                If FrameIdx = 1 Then
                    State(StateOctopus.Stand, 2)
                End If
        End Select



    End Sub
End Class

Public Class CCharOctopusProjectile
    Inherits CCharacter

    Public CurrState As StateOctopusProjectile

    Public Sub State(state As StateOctopusProjectile, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case StateOctopusProjectile.Create
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateOctopusProjectile.Move, 1)

                End If

            Case StateOctopusProjectile.Move
                GetNextFrame()
                PosX = PosX + Vx
                PosY = PosY + Vy

                If FDir = FaceDir.Left Then
                    Vx = -10
                    If Form1.dummy.PosX = PosX And PosY >= 151 And PosY <= 191 Then
                        Form1.dummy.State(StateMegaman.Die, 1)
                    End If
                    If PosX <= 20 Or PosY >= 210 Or PosY <= 10 Then
                        Destroy = True
                    ElseIf Form1.dummy.PosX = PosX And PosY >= 151 And PosY <= 191 Then
                        Destroy = True
                    End If

                ElseIf FDir = FaceDir.Right Then
                    Vx = 10
                    If Form1.dummy.PosX = PosX And PosY >= 151 And PosY <= 191 Then
                        Form1.dummy.State(StateMegaman.Die, 1)
                    End If
                    If PosX >= 240 Or PosY >= 210 Or PosY <= 10 Then
                        Destroy = True
                    ElseIf Form1.dummy.PosX = PosX And PosY >= 151 And PosY <= 191 Then
                        Destroy = True
                    End If

                End If




        End Select
    End Sub

End Class


Public Class CElmtFrame
    Public CtrPoint As TPoint
    Public Top, Bottom, Left, Right As Integer

    Public MaxFrameTime As Integer

    Public Sub New(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        CtrPoint.x = ctrx
        CtrPoint.y = ctry
        Top = t
        Bottom = b
        Left = l
        Right = r
        MaxFrameTime = mft

    End Sub
End Class

Public Class CArrFrame
    Public N As Integer
    Public Elmt As CElmtFrame()

    Public Sub New()
        N = 0
        ReDim Elmt(-1)
    End Sub

    Public Overloads Sub Insert(E As CElmtFrame)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1
    End Sub

    Public Overloads Sub Insert(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        Dim E As CElmtFrame
        E = New CElmtFrame(ctrx, ctry, l, t, r, b, mft)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1

    End Sub

End Class

Public Structure TPoint
    Dim x As Integer
    Dim y As Integer

End Structure

