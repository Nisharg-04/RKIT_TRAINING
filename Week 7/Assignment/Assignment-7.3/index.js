const usernameInput = document.getElementById("usernameInput");
const saveUsername = document.getElementById("saveUsername");

const savedName = localStorage.getItem("username");
if (savedName) {
    usernameInput.value = savedName;
}

saveUsername.addEventListener("click", () => {
    const username = usernameInput.value;
    localStorage.setItem("username", username);
    alert("Username saved successfully");
});

const clickBtn = document.getElementById("clickBtn");
const clickCount = document.getElementById("clickCount");

if (!sessionStorage.getItem("count")) {
    sessionStorage.setItem("count", 0);
}

clickCount.innerText = "Clicks this session: " + sessionStorage.getItem("count");
clickBtn.addEventListener("click", () => {
    let count = Number(sessionStorage.getItem("count"));
    count++;
    sessionStorage.setItem("count", count);
    clickCount.innerText = "Clicks this session: " + count;
});

function setCookie(name, value, days) {
    document.cookie = `${name}=${value}; max-age=${days * 24 * 60 * 60}; path=/`;
}

function getCookie(name) {
    const cookies = document.cookie.split("; ");
    for (let cookie of cookies) {
        const cookieItem = cookie.split("=");
        if (cookieItem[0] === name) {
            return cookieItem[1];
        }
    }
    return null;
}

if (!getCookie("consent")) {
    document.getElementById("cookieBanner").style.display = "block";
}
document.getElementById("acceptCookies").addEventListener("click", () => {
    setCookie("consent", "true", 7); // valid for 7 days
    document.getElementById("cookieBanner").style.display = "none";
});