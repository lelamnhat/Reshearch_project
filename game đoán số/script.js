let randomNumber = Math.floor(Math.random() * 100) + 1;
let attempts = 0;

const guessInput = document.getElementById('guessInput');
const submitGuess = document.getElementById('submitGuess');
const resultMessage = document.getElementById('resultMessage');
const restartGame = document.getElementById('restartGame');

submitGuess.addEventListener('click', () => {
    const userGuess = Number(guessInput.value);
    attempts++;

    if (userGuess === randomNumber) {
        resultMessage.textContent = `Congratulations! You guessed the number ${randomNumber} in ${attempts} attempts.`;
        restartGame.style.display = 'block';
        submitGuess.disabled = true;
    } else if (userGuess < randomNumber) {
        resultMessage.textContent = 'Too low! Try again.';
    } else if (userGuess > randomNumber) {
        resultMessage.textContent = 'Too high! Try again.';
    }

    guessInput.value = '';
    guessInput.focus();
});

restartGame.addEventListener('click', () => {
    randomNumber = Math.floor(Math.random() * 100) + 1;
    attempts = 0;
    resultMessage.textContent = '';
    guessInput.value = '';
    guessInput.focus();
    restartGame.style.display = 'none';
    submitGuess.disabled = false;
});