// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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