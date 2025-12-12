let marks = [45, 67, 89, 23, 78, 90, 56, "halle"];

function calculateStatus(mark) {
    if (typeof mark !== "number") {
        return "Invalid mark";
    }
    return mark >= 50 ? "Pass" : "Fail";
}

for (let i of marks) {

    const status = calculateStatus(i);
    console.log(`Mark: ${i}, Status: ${status === "Invalid mark" ? "Invalid mark" : status}`);
}