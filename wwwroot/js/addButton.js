

$(document).ready(function () {
    function IsInShoppingCart(productId) {
        $.ajax({
            url: '/Shopping/IsInShoppingCart',
            type: 'GET',
            data: { productId: productId },
            success: function (result) {
                if (result.isInCart) {
                    console.log('����� ��������� � �������');

                } else {
                    console.log('����� �� ��������� � �������');
                }
            },
            error: function (error) {
                console.error('��������� ������ ��� �������� �������', error);
            }
        });
    }

}