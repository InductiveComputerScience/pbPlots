function ToByteArray(data){
	var bytes = new Uint8Array(data.length);

	for(var i = 0; i < data.length; i++){
		bytes[i] = data[i];
	}

	return bytes;
}

function WriteToFile(data, filename){
	var bytes = ToByteArray(data);
	var wstream = fs.createWriteStream(filename);
	wstream.write(bytes);
	wstream.end();
}
