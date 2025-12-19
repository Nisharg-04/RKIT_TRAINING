export function getUser() {
    return {
        id: 1,
        name: "Nisharg",
        role: "Student"
    };
}
export function isAdmin(user) {
    return user.role === "Admin";
}