const bulb = document.querySelector('#bulb');
const toggleBtn = document.querySelector('#togglebtn');
const clickCountDisplay = document.querySelector('#btnClicks');
let pageClicks = 0;

toggleBtn.addEventListener('click', (e) => {
    e.stopPropagation();
    if (bulb.style.backgroundColor === 'yellow') {
        bulb.style.backgroundColor = 'gray';
        toggleBtn.textContent = 'Turn On';
    } else {
        bulb.style.backgroundColor = 'yellow';
        toggleBtn.textContent = 'Turn Off';
    }

});

document.addEventListener('click', () => {
    pageClicks += 1;
    clickCountDisplay.textContent = `Page Clicks: ${pageClicks}`;

});