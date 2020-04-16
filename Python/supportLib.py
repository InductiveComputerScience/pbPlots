import array

def DoubleArrayToByteArray(data):
	ints = [int(x) for x in data]
	bytes = array.array('B', ints)
	return bytes;

def WriteToFile(data, filename):
	f = open(filename, 'w+b')
	bytes = DoubleArrayToByteArray(data)
	f.write(bytes)
	f.close()

	return
