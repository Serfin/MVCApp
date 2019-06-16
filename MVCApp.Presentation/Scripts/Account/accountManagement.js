function login() {
    var data = $("#user-login-form").serialize();
    $.ajax({
        type: "POST",
        url: "/Home/ValidateUser",
        data: data,
        success: function () {
            updateNavbarAfterLogin();
            redirect("/Home/Index");
        }
    });
}

function logout() {
    $.ajax({
        type: "GET",
        url: "/Home/Logout",
        success: function (response) {
            updateNavbarAfterLogout(response);
        }
    });
}

function updateNavbarAfterLogin() {
    $("div:contains('Login')").css("display: none;");
    $("div:contains('Logout')").css("display: inline;");

    $(".nav-link [/Account/Index]").text();
}

function updateNavbarAfterLogout(user) {
    $("div:contains('Login')").css("display: inline;");
    $("div:contains('Logout')").css("display: none;");
}