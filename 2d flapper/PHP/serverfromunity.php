<?php
	$text1 = $_POST["test"];

	if ($text1 != "")
	{
		echo("Success!");
		echo("Field1: ".  $text1);
		$file = fopen("data.txt","a");
		fwrite($file, $text1);
		fclose($file);
	}
	else
	{
	echo ("Fail!");
	}
?>