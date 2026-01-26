
function inlineClick() {
    console.log("1Inline onclick");
  }

  document.getElementById("btn")
    .addEventListener("click", function () {
      console.log("addEventListener");
    });
  

  document.querySelector("#btn")
    .addEventListener("click", function () {
      console.log("querySelector addEventListener");
    });
  
 
  $("#btn").click(function () {
    console.log("jQuery .click()");
  });
  
 
  $("#btn").on("click", function () {
    console.log("jQuery .on()");
  });
  