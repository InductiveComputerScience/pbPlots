Imports System
Imports System.IO
Imports System.Math

Module SupportLib
	Function DoubleArrayToByteArray(data As double()) as byte()
		Dim bytes as byte()
		Dim i as double

		bytes = New byte (data.Length - 1){}

		i = 0
		While i < data.Length
			bytes(i) = data(i)
			i = i + 1
		End While

		return bytes
	End Function

	Sub WriteToFile(data As double(), filename As String)
		Dim bytes as byte()

		bytes = DoubleArrayToByteArray(data)
		File.WriteAllBytes(filename, bytes)
	End Sub
End Module
