$(document).ready(function () {
    $.validator.addMethod("passwordCheck", function (value, element) {
        return value.test(/[A-Z]/) && value.test(/[0-9]/) && value.test(/[\@\#\$\%\^\&\*\(\)\_\+\!]/) && value.length >= 8;
    });
    $("#myForm").validate({
        rules: {
            username: {
                required: true,
                minlength: 5
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                passwordCheck: true
            }
        },
        messages: {
            username: {
                required: "Please enter your username",
                minlength: "Your username must be at least 5 characters long"
            },
            email: {
                required: "Please enter your email",
                email: "Please enter a valid email address"
            },
            password: {
                required: "Please provide a password",
                passwordCheck: "Password must be at least 8 characters long and include an uppercase letter, a number, and a special character"
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });


});