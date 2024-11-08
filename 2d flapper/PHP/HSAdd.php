<?php  
 
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');
header('Access-Control-Expose-Headers: X-App-Signature');


$hostname = 'vickrpgcom.ipagemysql.com';
$username = 'tpapp';
$password = 'superslug';
$dbname = 'tphs';

// Create connection
$conn = new mysqli($hostname, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
    $name = $_POST["namePost"];
    $score = $_POST["scorePost"]; 
    $combo = $_POST["comboPost"];
    $perfect = $_POST["perfectionPost"]; 

	$stmt = "INSERT INTO HighScores (Name, Score, MaxCombo, Perfection) VALUES ('".$name."','".$score."','".$combo."','".$perfect."')";

  
if ($conn->query($stmt) === TRUE) {
  echo "New record created successfully";
} else {
  echo "Error: " . $stmt . "<br>" . $conn->error;
}

$conn->close();
?>