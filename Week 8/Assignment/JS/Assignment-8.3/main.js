import { BankAccount } from "./account.js";
const account1 = new BankAccount("Nisharg", 5000);
const account2 = new BankAccount("Amit", 3000);
account1.deposit(2000);
account1.withdraw(1000);

account2.deposit(500);
account2.withdraw(2000);
console.log("Final Balance of Nisharg:", account1.balance);
console.log("Final Balance of Amit:", account2.balance);
console.log(BankAccount.info());