def DoubleArrayToByteArray(data)
	return data.pack("C*")
end

def WriteToFile(data, filename)
	bytes = DoubleArrayToByteArray(data)
	File.binwrite(filename, bytes)
end
