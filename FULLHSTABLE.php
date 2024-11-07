<?php 
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');
header('Access-Control-Expose-Headers: X-App-Signature');

$hostname = 'vickrpgcom.ipagemysql.com';
$username = 'tpapp';
$password = 'superslug';
$database = 'tphs';
 
try 
{
	$dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, 
         $username, $password);
} 
catch(PDOException $e) 
{
	echo '<h1>An error has occurred.</h1><pre>', $e->getMessage()
            ,'</pre>';
}
 
$sth = $dbh->query('SELECT * FROM HighScores ORDER BY Score DESC LIMIT 1000');
$sth->setFetchMode(PDO::FETCH_ASSOC);
 
$result = $sth->fetchAll();
 
if (count($result) > 0) 
{
	echo "<link rel='stylesheet' href='TableStyle.cs'>";
	echo "<table class='center'>";
	echo "<tr><th>Rank</th><th>Name</th><th>Score</th></tr>";	
	$rank = 0;
	foreach($result as $r) 
	{
		$rank = $rank+1;
		echo "<tr> <td>".$rank."</td><td>". $r['Name'] ."</td>\n <td>" .$r['Score'] . "</td>\n";
	}
	echo "</table>";
}

?>
