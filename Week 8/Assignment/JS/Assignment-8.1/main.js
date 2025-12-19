let current = document.querySelector('#currentTime');
function updateTime() {
    current.textContent = new Date().toLocaleTimeString();
}
setInterval(updateTime, 1000);
updateTime()

let random = document.querySelector('#randomID');
function generateRandomID() {
    let randomNum = Math.floor((Math.random() * 10000) + 1000);
    random.textContent = randomNum;
}
generateRandomID();
let refreshButton = document.querySelector('#generateIDBtn');
refreshButton.addEventListener('click', generateRandomID);

let messyString = "     heLLO woRLD      ";
let trimmed = messyString.trim();
let parts = trimmed.split(" ");
let capitalizedParts = parts.map(part => part.charAt(0).toUpperCase() + part.slice(1).toLowerCase());
let cleanString = capitalizedParts.join(" ");
let displayString = document.querySelector('#cleanString');
displayString.textContent = cleanString;
