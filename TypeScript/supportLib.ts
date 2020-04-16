import * as fs from 'fs';

export function ToByteArray(data : number []) : Uint8Array{
	let bytes : Uint8Array = new Uint8Array(data.length);

	for(var i = 0; i < data.length; i++){
		bytes[i] = data[i];
	}

	return bytes;
}

export function WriteToFile(data : number [], filename : string){
	var bytes = ToByteArray(data);
	var wstream = fs.createWriteStream(filename);
	wstream.write(bytes);
	wstream.end();
}
