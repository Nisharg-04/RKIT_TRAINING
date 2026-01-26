function ajaxPromise(options) {
    return new Promise((resolve, reject) => {
      $.ajax({
        ...options,
        success: resolve,
        error: reject
      });
    });
  }
  


  $("#btn").click(async function () {
    try {
      console.log("Before API call");
  
      const user = await ajaxPromise({
        url: "https://jsonplaceholder.typicode.com/users/1",
        method: "GET"
      });
  
      console.log("User:", user);
  
      const posts = await ajaxPromise({
        url: "https://jsonplaceholder.typicode.com/posts",
        data: { userId: user.id }
      });
  
      console.log("Posts:", posts);
  
      $("#output").html(`
        <h3>${user.name}</h3>
        <p>Total Posts: ${posts.length}</p>
      `);
  
      console.log("After API call");
  
    } catch (err) {
      console.error("Error:", err);
    }
  });


console.log("Script loaded");