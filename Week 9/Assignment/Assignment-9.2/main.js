$(document).ready(function () {
    $("#userForm").submit(function (event) {
        event.preventDefault();
        let formArray = $(this).serializeArray();
        let formData = {};
        $.each(formArray, function (i, field) {
            if (formData[field.name]) {
                if (!Array.isArray(formData[field.name])) {
                    formData[field.name] = [formData[field.name]];
                }
                formData[field.name].push(field.value);
            } else {
                formData[field.name] = field.value;
            }
        });
        let jsonString = JSON.stringify(formData, null, 4);
        $("#output").text(jsonString);

    });
});