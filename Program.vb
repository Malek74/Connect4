'
' Created by SharpDevelop.
' User: Malek
' Date: 04/01/2021
' Time: 08:25 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Module Program
	Dim slot As Integer=0
	Dim turnCounter As Integer=0
	Dim grid(6,7) As Char
	Dim p1,p2 As String
	
	Sub Main()
		Dim choice As Integer=0

Console.WriteLine("Enter first player name (red):")
p1=Console.ReadLine()
Console.WriteLine("")
		Console.WriteLine("Enter second name (blue):")
		p2=Console.ReadLine()
		
		'intialises grid array
		For i=1 To 6
			For j=1 To 7
				grid(i,j)="X"
				
			Next
		Next
		
		update()
		Console.WriteLine("")
		
		While(True)
		choice=check(1)
		turn(choice,1,grid)
		update()
		
		If (win(choice,grid)=True) Then
			Console.WriteLine(p1 & " WINS!")
			Exit While
			
		End If
		
		choice=check(2)
		turn(choice,2,grid)
		update()
		
		If (win(choice,grid)=True) Then
			Console.WriteLine(  p2 &" WINS!")
			Exit While
		End If
		
		If(turnCounter=42) Then
			Console.WriteLine("GAME ENDED AS DRAW.")
			Exit While
		End If	
End While
      
        Console.WriteLine("")
		Console.Write("Press any key to continue . . . ")
		Console.ReadKey(True)
	End Sub
	
	Sub turn (byval choice As Integer,byval player As Integer , byref grid(,) As Char)
		Dim count As Integer=6
		Dim empty As Boolean=False
		
		While(empty=False And count>0)
			If(grid(count,choice)<>"X") Then
				count=count-1
				
			Else
				If(player=1) Then
					turnCounter=turnCounter+1
					slot=count
					grid(count,choice)="-"
					empty=True

				Else
					turnCounter=turnCounter+1
					slot=count
					grid(count,choice)="^"
					empty=True					
				End If		
			End If	
		End While
	End Sub
	'updates grid
	Sub update()
		
		Console.Clear()
		
		For i=1 To 7
			Console.Write(" ")
			Console.Write(i & " |")
		Next
		
		Console.WriteLine("")
		Console.WriteLine("----------------------------")
		
		For i=1 To 6
			For j=1 To 7
				'checks if slot belongs to p1
				If(grid(i,j)="-") Then
					Console.ForegroundColor=ConsoleColor.Red
					Console.Write(" *")
					Console.ResetColor()
					Console.Write(" |")
					
					'checks if slot belongs to p2
				Else If(grid(i,j)="^") Then
					Console.ForegroundColor=ConsoleColor.blue
					Console.Write(" *")
					Console.ResetColor()
					Console.Write(" |")			
			 Else
				Console.Write("   |")
				End If
			Next
			Console.WriteLine("")
			Console.WriteLine("----------------------------")
		Next
		Console.WriteLine("")
	End Sub	

	'prompts user for input and validates it
	Function check(Byval player As Integer) As Integer
		Dim choice As Integer
		
		'prompts user for input
		If(player=1) Then
			Console.WriteLine(p1 &"'s turn choose a column to play")
		Else
			Console.WriteLine(p2 &"'s turn choose a column to play")
			End If
			
	        Try
			choice=Console.ReadLine()
			'check input is of correct data type
		Catch ex As InvalidCastException
			Console.WriteLine("wrong input enter a number")
			choice=Console.ReadLine()
		End Try
		
		'checks input is within grid boundaries
		While(choice>7 Or choice<=0 )
			
			update()
			Console.WriteLine("choose a number between 1 & 7")
			Console.WriteLine("")
			Console.WriteLine("choose a column to play")
		        choice=Console.ReadLine()
		End While
		
		'checks slot is empty
		While (grid(1,choice)<>"X") 
		update()
		Console.WriteLine("slot is occupied")
		Console.WriteLine("")
		Console.WriteLine("choose a column to play")
		choice=Console.ReadLine()
		End While
	
			Return choice		
	End Function
	'checks if someone won
	Function win(byval choice As Integer,ByVal grid(,) As Char) As Boolean
		If(horizontal(choice)=4) Then
			Return True 
			
		Else If(diagonal(choice)=3) Then
			Return True
			
		Else If(vertical(choice)=3) Then
			Return True
		
		End If
		Return False
	End Function
	'checks a win vertically
	Function vertical (ByVal choice As Integer) As Integer
		Dim last as char=grid(slot,choice)
		Dim count As Integer=0
		
		Try
		If (slot>=4  ) Then
			For i=1 To 3
				If(grid(slot+i,choice)=last  ) Then
					count=count+1
					
				Else 
					count=0
				End If
			Next
		End If
		 If(slot<=4 ) Then
			For i=1 To 3
				If(grid(slot+i,choice)=last  ) Then
					count=count+1
					
				Else 
					count=0
				End If
			Next
		 End If
		Catch ex As IndexOutOfRangeException
			count=0
			
		End Try
		 Return count
	End Function
	
	'checks a win horizontally	
	Function horizontal(ByVal choice As Integer) As Integer
		Dim last As Char=grid(slot,choice)
		Dim count As Integer=0
		Dim finish As Boolean
		Dim start As Integer=1
		
		While(finish=False)
			For j=0 To 3
				If(grid(slot,j+start)=last) Then
					count=count+1
					
				Else 
					count=0
					Exit For
				End If
				
			Next
			start=start+1
			
			If(start=5 Or count=4) Then
				finish=True
		End If	
		End While
		Return count
	End Function
	
	'checks a win diagonally
	Function diagonal(ByVal choice As Integer)
		Dim last As Char=grid(slot,choice)
		Dim limit,max,count As Integer
		count=0
		
		If(choice>slot) Then
			max=7-choice
		Else
			limit=choice-1
		End If
			
		For i=1 To limit
			If(grid(slot-i,choice-i)=last) Then
				count=count+1
			Else 
				Exit For
				End if
			Next
			Try
				limit=7-choice
			For i=1 To limit
				
				If(grid(slot+i,choice+i)=last) Then
					count=count+1
					
				Else
					Exit For
				End If
				Next
			Catch ex As IndexOutOfRangeException
			End Try
	
			'resets counter to check for opposite diagonals
			If(count=3) Then
				Return count
			End If
			count=0
			Try
				limit=7-choice
				For i=1 To limit
					If(grid(slot-i,choice+i)=last) Then
						count=count+1					
					Else 
						Exit For	
					End If
				Next
				Catch m As IndexOutOfRangeException
				End Try
			
				Try
					limit=6-slot
					For i=1 To limit
						If(grid(slot+i,choice-i)=last) Then
							count=count+1
							
						Else 
							Exit For
						End If
					Next
					Catch lol As IndexOutOfRangeException
				End Try
		return count
	End Function		
End Module

