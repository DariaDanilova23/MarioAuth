/*
var menu = document.getElementsByClassName("menu")[0];
menu.onclick = function (event) {
    var header = document.getElementsByClassName("collapse")[0];
    console.log(header);
    header.style.display = "block";
}
*/
function imgMouseOver(containerPhoto) {
   /* var descrip = document.getElementsByClassName("description")[0];
    var container = document.getElementsByClassName("containerPhoto")[0];*/
    console.log(window.innerWidth);
    if (window.innerWidth >= 600) {
        $(containerPhoto).find('.productPhoto').css("opacity", "0.4");
        $(containerPhoto).find('.description').css('display', 'block');
        containerPhoto.style.background = "#0000";
    }
    // Изменение стиля description
    //img.nextElementSibling.style.display = "block";
   /* container.style.background = "#0000";
    descrip.style.display = "block";*/

}
function imgMouseOut(containerPhoto) {
    $(containerPhoto).find('.productPhoto').css("opacity", "1");
    $(containerPhoto).find('.description').css("display", "none"); 
    containerPhoto.style.background = "";

   
    /*container.style.background = "";
    descrip.style.display = "none";*/
}
/*var img = document.getElementsByClassName("productPhoto");
console.log(img);
var descrip = document.getElementsByClassName("description")[0];
var container = document.getElementsByClassName("containerPhoto")[0];
img.onmouseover = function (event) {
    img.style.opacity = "0.6";
    container.style.background = "#0000";
    descrip.style.display = "block";
};

img.onmouseout = function (event) {
    img.style.opacity = "1";
    container.style.background = "";
    descrip.style.display = "none";
};*/