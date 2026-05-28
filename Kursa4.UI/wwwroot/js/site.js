// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showPassword(inputId, button) {
    const input = document.getElementById(inputId);
    input.type = "text";
    button.innerHTML = '<i class="bi bi-eye-slash"></i>';
}

function hidePassword(inputId, button) {
    const input = document.getElementById(inputId);
    input.type = "password";
    button.innerHTML = '<i class="bi bi-eye"></i>';
}


$(document).ready(function () {
    const userId = $('#user-info').data('user-id');
    if (userId) {
        GetUserInfo('user-info', userId);
    }
});

function translateRole(role) {
    var map = {
        'Client': 'Клиент',
        'Admin': 'Администратор',
        'Master': 'Мастер',
        'Economist': 'Экономист',
        'Owner': 'Владелец'
    };
    return map[role] || role;
}

function GetUserInfo(selectorId, userId) {
    $.ajax({
        url: '/Authorization/GetUserById',
        type: 'GET',
        data: { userId: userId },
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                var user = response.user;

                var displayText = user.surname + ' ' + user.name;
                var roleText = user.role ? translateRole(user.role) : '';
                $(`#${selectorId}`).html(displayText + '<br><small>' + roleText + '</small>');

            } else {
                console.error('Ошибка:', response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error('Ошибка при выполнении AJAX-запроса:', error);
        }
    });
}
