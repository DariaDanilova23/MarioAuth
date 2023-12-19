$(document).ready(function () {
    $(".product-form").submit(function (event) {
        // �������� ����������� ��������� �����
        event.preventDefault();
        let button = $(this)[0].querySelector('.addToCart');
        // �������� ������ �����
        var formData = $(this).serialize();

        // ��������� AJAX-������ �� ������
        $.ajax({
            url: "/ShoppingCarts/AddToCart",
            type: "POST",
            data: formData,
            success: function (result) {
                console.log(result);
               // $(button).html('<p>� �������</p>');
                button.innerHTML = "� �������";
                button.disabled = true;
                button.classList.remove('addToCart');
                button.classList.add('cartItemBtn');
                
                // �������������� ������, ���� ����������
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});
