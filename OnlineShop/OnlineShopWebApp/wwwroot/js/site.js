// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// JavaScript функция для переключения описания
function toggleDescription(button) {
    var description = button.previousElementSibling; // получаем предыдущий элемент (описание)
    if (description.classList.contains('description')) {
        description.classList.replace('description', 'description-full');
        button.textContent = 'Свернуть';
    } else {
        description.classList.replace('description-full', 'description');
        button.textContent = 'Подробнее...';
    }
}