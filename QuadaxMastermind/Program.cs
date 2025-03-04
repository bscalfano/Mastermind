// CONSTANTS
const int totalRounds = 12;
const int passwordLength = 4;


Console.WriteLine("Welcome to Mastermind! You will have " + totalRounds + " rounds to figure out the clue. Good luck!");
playGame();

// MAIN GAME FUNCTION
void playGame()
{
    Boolean correctFlag = false;
    string password = generatePassword();
    // Console.WriteLine(password); // Prints correct password, uncomment for debugging

    // Loops for the guesses
    for (int i = 1; i <= totalRounds && !correctFlag; ++i)
    {
        string currentGuess = getGuess(i);
        correctFlag = verifyGuess(password, currentGuess);
    }

    if (correctFlag) Console.WriteLine("You solved it!");
    else Console.WriteLine("You lose :(");

    Console.WriteLine("Play again? Y/N");
    string playAgain = Console.ReadLine();
    if (playAgain.ToUpper() == "Y") playGame();
}


///// FUNCTIONS ////

// Creates the random password
string generatePassword()
{
    return "6661";
    string password = "";
    Random rand = new Random();

    for (int i = 0; i < passwordLength; ++i)
    {
        password += rand.Next(1, 7).ToString();
    }

    return password;
}

// Gets the user's guess, and ensures it meets the requirements
string getGuess(int guessNumber)
{
    Boolean validGuess = false;
    string guess = "";

    while (!validGuess)
    {
        Console.WriteLine("Guess #" + guessNumber + ": ");
        guess = Console.ReadLine();

        // Validates that all guess is 4 characters
        if (guess.Length != passwordLength)
        {
            Console.WriteLine("Guess must be " + passwordLength + " digits. Please enter another guess.");
            continue;
        }

        // Validates that all characters are digits from 1-6
        validGuess = true;
        for (int i = 0; i < passwordLength; ++i)
        {
            if (!(guess[i] == '1' || guess[i] == '2' || guess[i] == '3' || guess[i] == '4' || guess[i] == '5' || guess[i] == '6'))
            {
                Console.WriteLine("Guess must only contain digits from 1-6. Please enter another guess.");
                validGuess = false;
                break;
            }
        }
    }

    return guess;
}

// Determines how many characters of the guess match the password. Returns true if guess is correct, false if it is not
Boolean verifyGuess(string password, string guess)
{
    string result = "";

    if (password == guess)
    {
        Console.WriteLine("++++");
        return true;
    }

    // Checks if any characters match character and position
    for (int i = 0; i < guess.Length; ++i)
    {
        if (guess[i] == password[i])
        {
            result += "+";
            password = password.Substring(0, i) + "*" + password.Substring(i + 1); // Replaces matching character so it is not scored twice
            guess = guess.Substring(0, i) + "*" + guess.Substring(i + 1);
        }
    }

    // Checks if any remaining characters match
    for (int i = 0; i < guess.Length; ++i)
    {
        for (int j = 0; j < password.Length; ++j)
        {
            if (guess[i] == password[j] && guess[i] != '*')
            {
                result += "-";
                password = password.Substring(0, j) + "*" + password.Substring(j + 1); // Replaces matching character so it is not scored twice
                guess = guess.Substring(0, i) + "*" + guess.Substring(i + 1);
                break; // Found a match for this character, go to the next
            }
        }
    }

    Console.WriteLine(result);

    return false;
}