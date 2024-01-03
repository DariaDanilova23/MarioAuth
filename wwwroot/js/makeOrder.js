function deliveryAdress() {
    const select = $('#delivery'); 
    const selectedValue = select.val();

    if (selectedValue == "home") { 
        var newInput = $("<input>").attr({
            type: "text",
            name: "address",
            id: "address",
            placeholder: "Введите адрес доставки"
        });

        select.after(newInput);
    } else { 
        const newInput = $('#address');
        if (newInput.length) {
            newInput.remove();
        }
    }
}
window.addEventListener("DOMContentLoaded", function () {
    [].forEach.call(document.querySelectorAll('.tel'), function (input) {
        var keyCode;
        function mask(event) {
            event.keyCode && (keyCode = event.keyCode);
            var pos = this.selectionStart;
            if (pos < 3) event.preventDefault();
            var matrix = "+7 (___) ___ ____",
                i = 0,
                def = matrix.replace(/\D/g, ""),
                val = this.value.replace(/\D/g, ""),
                new_value = matrix.replace(/[_\d]/g, function (a) {
                    return i < val.length ? val.charAt(i++) || def.charAt(i) : a
                });
            i = new_value.indexOf("_");
            if (i != -1) {
                i < 5 && (i = 3);
                new_value = new_value.slice(0, i)
            }
            var reg = matrix.substr(0, this.value.length).replace(/_+/g,
                function (a) {
                    return "\\d{1," + a.length + "}"
                }).replace(/[+()]/g, "\\$&");
            reg = new RegExp("^" + reg + "$");
            if (!reg.test(this.value) || this.value.length < 5 || keyCode > 47 && keyCode < 58) this.value = new_value;
            if (event.type == "blur" && this.value.length < 5) this.value = ""
        }

        input.addEventListener("input", mask, false);
        input.addEventListener("focus", mask, false);
        input.addEventListener("blur", mask, false);
        input.addEventListener("keydown", mask, false)

    });

});

function validateForm() {
    var tel = $("#tel").val().trim(); //Получение данных из поля ввода телефона
    var delivery = $("#delivery").val();//Получение данных из поля ввода адреса доставки
    var comment = $("#comment").val().trim(); //Получение данных из поля ввода комментария к заказу
    
    if (tel === "") { //Обработчик пустого поля ввода телефона
        markFieldAsError($("#tel"));
    } else {
        removeErrorMark($("#tel"));
    }

    if (delivery === "") { //Обработчик пустого поля ввода телефона
        markFieldAsError($("#delivery"));
    } else { //Если поле не пустое получение адресса доставки
        if (delivery=="home") {
            var address = $("#address").val().trim();
            if (address === "") {
                markFieldAsError($("#address"));
            }
            else {
                removeErrorMark($("#address"));
            }
        }
        removeErrorMark($("#delivery"));
    }

    if (tel === "" || delivery === "") {
        return false; 
    }

    return true;
}
function markFieldAsError(inputField) {
    inputField.addClass("error");
}

function removeErrorMark(inputField) {
    inputField.removeClass("error");
}