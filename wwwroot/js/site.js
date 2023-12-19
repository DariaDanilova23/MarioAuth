// main.js
/*
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
                    button.style.backgroundColor = 'green';
                    button.innerHTML = 'В корзине'; 
                    button.disabled = true;
//                        .css("backgroundColor", "red");//.style.backgroundColor = 'red';
                    //.style.backgroundColor = 'red';

                    // Дополнительная логика, если необходимо
                },
                error: function (error) {
                    // Обработать ошибку
                    console.error(error);
                }
            });
        });
    });*/
/*
$(document).ready(function () {
    // Обработчик события, например, нажатия кнопки
    $('#addButtonId').on('click', function () {
        // Отправка AJAX-запроса на сервер
        // var formData = $("#myForm").serialize();
        var formData = $("#idGet");
        
        $.ajax({
            url: '/ShoppingCarts/AddToCart', // Путь к методу контроллера
            type: 'POST', // Метод запроса
            data: formData,
           
            success: function (data) {
                // Обработка успешного ответа от сервера
                console.log('Success', data);

                // Здесь вы можете обновить содержимое страницы на основе полученных данных
            },
            error: function (error) {
                // Обработка ошибки
                console.error('Error', error);
            }
        });
    });
});

*/



