const baseURL = '/api/products';
const sectionCard = document.querySelector('#card');
const btns = document.querySelectorAll(".mybtn");
const storeProducts = document.querySelectorAll(".store-products");
//const search
for (let i = 0; i < btns.length; i++) {
    btns[i].addEventListener("click", (e) => {
        e.preventDefault();
        const filter = e.target.dataset.filter;
        console.log(filter);
        storeProducts.forEach((product) => {
            if (filter === "all") {
                product.style.display = "grid";
            } else {
                if (product.classList.contains(filter)) {
                    product.style.display = "grid";
                } else {
                    product.style.display = "none";
                }
            }
        });
    });
}
//Search Filter
const search = document.getElementById("search");
const productName = document.querySelectorAll(".product-details h4");
search.addEventListener("keyup", filterProducts);
function filterProducts(e) {
    const text = e.target.value.toLowerCase();
    productName.forEach(function (product) {
        console.log(product);
        const item = product.firstChild.textContent;
        if (item.toLowerCase().indexOf(text) != -1) {
            product.parentElement.parentElement.style.display = "block";
        } else {
            product.parentElement.parentElement.style.display = "none";
        }
    })
}