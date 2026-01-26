$("#addBtn").click(function () {
    console.log("Button clicked! using jQuery 1");
})
$("#addBtn").click(function () {
    console.log("Button clicked! using jQuery 2");
})
$("#outerBtn").click(function () {
    console.log("Outer Button Clicked using jQuery Click Method - directly attached using jquery");
}); 
$(document).on("click", "#outerBtn", function () {
    console.log("Outer Button Clicked using jQuery Event Listener");
});