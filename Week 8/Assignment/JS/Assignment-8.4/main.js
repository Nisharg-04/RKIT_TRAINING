async function loadUsers() {
    try {
        const response = await fetch('https://jsonplaceholder.typicode.com/users');
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }
        const users = await response.json();
        const usersContainer = document.getElementById('users');
        users.forEach(user => {
            const userDiv = document.createElement('div');
            userDiv.classList.add('user');
            const userName = document.createElement('h2');
            userName.textContent = `Name: ${user.name}`;
            const userEmail = document.createElement('p');
            userEmail.textContent = `Email: ${user.email}`;
            const userWebsite = document.createElement('a');
            userWebsite.href = `http://${user.website}`;
            userWebsite.textContent = `Website: ${user.website}`;
            userWebsite.target = '_blank';
            userDiv.appendChild(userName);
            userDiv.appendChild(userEmail);
            userDiv.appendChild(userWebsite);
            usersContainer.appendChild(userDiv);
        });

    } catch (error) {
        alert('Failed to load users. Please try again later.');
        console.error('There has been a problem with your fetch operation:', error);
    }
}
loadUsers();