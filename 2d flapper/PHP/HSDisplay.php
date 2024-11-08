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
 
$sth = $dbh->query('SELECT * FROM HighScores ORDER BY Score DESC LIMIT 10');
$sth->setFetchMode(PDO::FETCH_ASSOC);
 
$result = $sth->fetchAll();
 
if (count($result) > 0) 
{
	echo "<table> \n ";
	foreach($result as $r) 
	{
		echo "<tr>\n <td>". $r['Name'] ."</td><split/> 
        <td>" .$r['Score'] . "</td><split/>
        <td>" .$r['MaxCombo']."</td><split/>
        <td>" .$r['Perfection'] . "</td> </tr><split/>\n";
	}
	echo "</table>";
}

?>