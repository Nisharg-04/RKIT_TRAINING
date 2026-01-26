async function initApp() {
    console.log("Before await");

    window.appState.products = await getProducts();
    window.appState.productsLoaded = true;
    console.log("Products loaded");
    console.log("After await");
    console.log(window.appState.products);
    await new Promise((resolve) => setTimeout(resolve, 2000));
    console.log("2 seconds later");
}

initApp();

console.log("This runs immediately");
console.log(window.appState.products);
