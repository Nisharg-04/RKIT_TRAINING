let users = [{ name: "John", role: "admin" },
{ name: "Jane", role: "user" },
{ name: "Doe", role: "admin" },
{ name: "Smith", role: "user" }];

let adminUsers = $.grep(users, function (user) { return user.role === "admin" });
console.log(adminUsers);

let adminNames = $.map(adminUsers, function (user) { return user.name; });
console.log(adminNames);

let defaultSettings = { theme: "dark", notifications: true };
console.log($.extend({}, defaultSettings, { theme: "light" }));