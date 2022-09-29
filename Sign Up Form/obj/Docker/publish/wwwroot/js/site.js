// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function hideLoadingDiv() {
    setTimeout(function () {
        document.getElementById('success-alerting').classList.add('hidden');
    }, 2000); setTimeout(function () {
        document.getElementById('updateddb').classList.add('hidden');
    }, 2000);



}