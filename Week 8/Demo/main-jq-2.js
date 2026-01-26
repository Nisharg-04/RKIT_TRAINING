$("#addBtn").click(function () {
    console.log("Button clicked! using jQuery 1 - file 2");
})
$("#addBtn").click(function () {
    console.log("Button clicked! using jQuery 2 file 2");
})
$("#outerBtn").click(function () {
    console.log("Outer Button Clicked using jQuery Click Method - directly attached using jquery file 2");
}); 
$(document).on("click", "#outerBtn", function () {
    console.log("Outer Button Clicked using jQuery Event Listener file 2");
});