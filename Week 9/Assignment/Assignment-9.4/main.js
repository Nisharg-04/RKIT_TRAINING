$(document).ready(function () {
    $("#loadBtn").click(function () {

        $("#status").text("Loading dashboard...");
        $("#dashboard").empty();
        let userRequest = $.ajax({
            url: "https://jsonplaceholder.typicode.com/users/1",
            method: "GET"
        });
        let postsRequest = $.ajax({
            url: "https://jsonplaceholder.typicode.com/posts?userId=1",
            method: "GET"
        });
        $.when(userRequest, postsRequest)
            .done(function (userRes, postsRes) {

                let user = userRes[0];
                let posts = postsRes[0];

                $("#status").text("Dashboard Loaded Successfully!");

                let html = `
            <h2>User Profile</h2>
            <p><b>Name:</b> ${user.name}</p>
            <p><b>Email:</b> ${user.email}</p>

            <h2>User Posts</h2>
            <ul>
            ${posts.slice(0, 5).map(p => `<li>${p.title}</li>`).join("")}
             </ul>
             `;
                $("#dashboard").html(html);
            })
            .fail(function () {
                $("#status").html("<p class='error'>Failed to load dashboard. Please try again.</p>");
            });
    });
})