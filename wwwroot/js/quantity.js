function changeQuantity(value, id) {
    $.ajax({
        url: "/ShoppingCarts/ChangeQuantity",
        type: "POST",
        data: { cartItemId: id, quantityValue: value },
        success: function (result) {
            $('#totalCost').text(result.totalCost);
        },
        error: function (error) {
            console.log(error);
        }
    });
};
