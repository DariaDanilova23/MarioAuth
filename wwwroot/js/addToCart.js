$(document).ready(function () {
    $(".product-form").submit(function (event) {
        event.preventDefault();
        let button = $(this)[0].querySelector('.addToCart');
        var formData = $(this).serialize();
        $.ajax({
            url: "/ShoppingCarts/AddToCart",
            type: "POST",
            data: formData,
            success: function (result) {
                console.log(result);
                button.innerHTML = "В корзине";
                button.disabled = true;
                button.classList.remove('addToCart');
                button.classList.add('cartItemBtn');
                button.id = "cartBtn";
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});
