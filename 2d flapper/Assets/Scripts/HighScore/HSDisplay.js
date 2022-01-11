var scoreText : GUIText; // Assign into Inspector the GUI Text you have created
 
// CHANGE THIS VALUE WITH YOUR ADDRESS
var urlDisplay = "http://www.lucedigitale.com/testgames/display.php";
   
function Start() {
    getScores(); // get and display the scores into GUIText scoreText
}
// Get Score START ###################################################################  
// Get the scores from the MySQL DB to display in a GUIText.
function getScores() {
    // First a loading message
    scoreText.text = "Loading Scores";
    // Start a download of the given URL
    var wwwDisplay : WWW = new WWW (urlDisplay);
    // Wait for download to complete
    yield wwwDisplay;
    // if it can't load the URL
    if(wwwDisplay.error) {
        // Write in the console: There was an error getting the high score: Could not resolve host: xxx; No data record of requested type
        print("There was an error getting the high score: " + wwwDisplay.error);
        // Display an error message
        scoreText.text = "No Data Record";
    } else {
        // This is a GUIText that will display the scores in game
        scoreText.text = wwwDisplay.text; 
    }
}
// Get Score END ###################################################################  // JavaScript source code
