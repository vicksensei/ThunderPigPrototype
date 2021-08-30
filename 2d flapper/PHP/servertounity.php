<?php
	$file = fopen("data.txt","r");
	echo fread($file, filesize("data.txt"));
	fclose($file);
?>