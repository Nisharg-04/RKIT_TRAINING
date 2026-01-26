document.getElementById("addBtn").addEventListener(
    "click",
    function () {
        console.log("Button clicked! using Vanilla JS");
    }
)
function funclick(){
    console.log("Button clicked using Inline okClick");
}
document.getElementById("outerBtn").addEventListener("click"  , function(){
    console.log("Outer Button Clicked using JS  Event Listener directly attached using JS");
});