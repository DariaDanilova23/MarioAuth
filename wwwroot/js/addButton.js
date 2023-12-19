

$(document).ready(function () {
    function IsInShoppingCart(productId) {
        $.ajax({
            url: '/Shopping/IsInShoppingCart',
            type: 'GET',
            data: { productId: productId },
            success: function (result) {
                if (result.isInCart) {
                    console.log('Товар находится в корзине');

                } else {
                    console.log('Товар не находится в корзине');
                }
            },
            error: function (error) {
                console.error('Произошла ошибка при проверке корзины', error);
            }
        });
    }

}