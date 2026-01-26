document.getElementById("addBtn").addEventListener(
    "click",
    function () {
        console.log("Button clicked! using Vanilla JS file 2");
    }
)
function funclick(){
    console.log("Button clicked using Inline okClick file 2");
}
document.getElementById("outerBtn").addEventListener("click"  , function(){
    console.log("Outer Button Clicked using JS  Event Listener directly attached using JS file 2");
});