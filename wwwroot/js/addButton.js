$(document).ready(function () {
    $(".product-form").submit(function (event) {
        // Отменить стандартное поведение формы
        event.preventDefault();
        let button = $(this)[0].querySelector('.addToCart');
        // Получить данные формы
        var formData = $(this).serialize();

        // Отправить AJAX-запрос на сервер
        $.ajax({
            url: "/ShoppingCarts/AddToCart",
            type: "POST",
            data: formData,
            success: function (result) {
                console.log(result);
               // $(button).html('<p>В корзине</p>');
                button.innerHTML = "В корзине";
                button.disabled = true;
                button.classList.remove('addToCart');
                button.classList.add('cartItemBtn');
                
                // Дополнительная логика, если необходимо
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});
