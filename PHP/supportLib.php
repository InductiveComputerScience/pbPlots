<?php

function WriteToFile($data, $filename){
    $ptr = fopen($filename, 'wb');
    for ($i = 0; $i < sizeof($data); $i++) {
        fwrite($ptr, pack('C', $data[$i]));
    }
    fclose($ptr);
}
