// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*navbar - collapse collapse d - sm - inline - flex justify - content - between*/
var menu = document.getElementsByClassName("menu")[0];
menu.onclick = function (event) {
    var header = document.getElementsByClassName("collapse")[0];
    console.log(header);
    header.style.display = "block";
}

var img = document.getElementsByClassName("productPhoto")[0];
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
};

