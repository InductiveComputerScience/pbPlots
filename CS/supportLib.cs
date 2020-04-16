using System;
using System.IO;

class SupportLib {
	public static byte [] DoubleArrayToByteArray(double [] data) {
		byte [] bytes = new byte [data.Length];
		for(int i = 0; i < data.Length; i++){
		    bytes[i] = (byte)data[i];
		}
		return bytes;
	}

	public static void WriteToFile(double [] data, string filename){
		byte [] bytes = DoubleArrayToByteArray(data);
		File.WriteAllBytes(filename, bytes);
	}
}
