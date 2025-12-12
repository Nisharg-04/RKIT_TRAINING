const students = [
    { name: "Sager", age: 20, grade: "A" },
    { name: "Akash", age: 22, grade: "B" },
    { name: "Rushi", age: 21, grade: "C" },
    { name: "Yash", age: 23, grade: "A" },
    { name: "Mihir", age: NaN, grade: "B" }
];


for (let student of students) {
    let doubleAge = student.age * 2;
    console.log(`Student: ${student.name}, Double Age: ${doubleAge}`);
}
console.table(students);

console.log(averageAge);