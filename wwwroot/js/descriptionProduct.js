function imgMouseOver(containerPhoto) {
    console.log(window.innerWidth);
    if (window.innerWidth >= 600) {
        $(containerPhoto).find('.productPhoto').css("opacity", "0.4");
        $(containerPhoto).find('.description').css('display', 'block');
        containerPhoto.style.background = "#0000";
    }
}
function imgMouseOut(containerPhoto) {
    $(containerPhoto).find('.productPhoto').css("opacity", "1");
    $(containerPhoto).find('.description').css("display", "none"); 
    containerPhoto.style.background = "";
}
