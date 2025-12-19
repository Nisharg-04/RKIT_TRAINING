class BankAccount {
    constructor(acccountHolder, balance = 0) {
        this.acccountHolder = acccountHolder;
        this.balance = balance;
    }
    deposit(amount) {
        if (amount > 0) {
            this.balance += amount;
            console.log(`Deposited: ${amount}. New balance: ${this.balance}`);
        } else {
            console.log("Deposit amount must be positive.");
        }
    }
    withdraw(amount) {
        if (amount > 0 && amount <= this.balance) {
            this.balance -= amount;
            console.log(`Withdrew: ${amount}. New balance: ${this.balance}`);
        } else if (amount > this.balance) {
            console.log("Insufficient funds.");
        } else {
            console.log("Withdrawal amount must be positive.");
        }
    }
    static info() {
        return "Bank System v1.0";
    }
}
const account1 = new BankAccount("Nisharg", 5000);
const account2 = new BankAccount("Amit", 3000);
account1.deposit(2000);
account1.withdraw(1000);

account2.deposit(500);
account2.withdraw(2000);
console.log("Final Balance of Nisharg:", account1.balance);
console.log("Final Balance of Amit:", account2.balance);
console.log(BankAccount.info());