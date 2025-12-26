$(document).ready(function () {

    const API_URL = "https://jsonplaceholder.typicode.com/todos";
    function loadTasks() {
        $.ajax({
            url: API_URL,
            method: "GET",
            dataType: "json",
            beforeSend: function () {
                $("#loadingMessage").show();
            },
            complete: function () {
                $("#loadingMessage").hide();
            },
            success: function (tasks) {
                $("#taskList").empty();
                $.each(tasks.slice(0, 20), function (index, task) {
                    addTaskRow(task.id, task.title, task.completed);
                });
            }
        });
    }

    loadTasks();

    $("#addTask").click(function () {
        let taskTitle = $("#taskInput").val().trim();

        if (taskTitle === "") {
            alert("Please enter a task");
            return;
        }

        $.ajax({
            url: API_URL,
            method: "POST",
            data: {
                title: taskTitle,
                completed: false
            },
            success: function (response) {
                console.log(response);
                addTaskRow(response.id, response.title, false);
                $("#taskInput").val("");
            },
            beforeSend: function () {
                $("#loader").show();
            },
            complete: function () {
                $("#loader").hide();
            }
        });
    });

    $("#taskList").on("click", ".deleteBtn", function () {
        let row = $(this).closest("tr");
        let taskId = row[0].children[0].innerText;

        $.ajax({
            url: API_URL + "/" + taskId,
            method: "DELETE",

            beforeSend: function () {
                $("#loader").show();
            },
            success: function () {
                row.remove();
            },

            complete: function () {
                $("#loader").hide();
            }
        });
    });
    $("#taskList").on("click", ".task-title", function () {
        $(this).toggleClass("completed");
    });




    function addTaskRow(id, title, isCompleted) {
        let rowClass = isCompleted ? "completed" : "";
        $("#taskList").append(`
            <tr class="${rowClass}">
                <td>${id}</td>
                <td class="task-title">${title}</td>
                <td><button class="deleteBtn">Delete</button></td>
            </tr>
        `);
    }


});