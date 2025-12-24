$("#increment-btn").click(function () {
    let currentValue = parseInt($("#counter").text());
    $("#counter").text(currentValue + 1);
    if (currentValue == 9) {
        $("#reset-btn").click();
    }
});
$("#reset-btn").click(function () {
    $("#counter").text(0);
});

