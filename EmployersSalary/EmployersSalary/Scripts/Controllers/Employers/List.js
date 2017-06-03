﻿(function () {
    var table = $("#employers").DataTable({
        ajax: {
            url: "/api/employers",
            dataSrc: ""
        },
        columns: [
            {
                data: "firstName",
                render: function (data, type, employer) {
                    return "<a href='/employers/edit/?firstname=" + employer.firstName + "&lastname=" + employer.lastName + "'>" + employer.firstName + "</a>";
                }
            }
            ,
            {
                data: "lastName",
                render: function (data, type, employer) {
                    return "<a href='/employers/edit/?firstname=" + employer.firstName + "&lastname=" + employer.lastName + "'>" + employer.lastName + "</a>";
                }
            },
            {
                data: "netSalary",
                render: function (data) {
                    return Math.round(data * 132) / 100;
                }
            },
            {
                data: "netSalary",
                render: function (data) {
                    return data * 1;
                }
            },
            {
                data: "id",
                render: function (data, type, employer) {
                    return "<button class='btn-link js-delete' data-movie-name=?firstname=" + employer.firstName + "&lastname=" + employer.lastName + ">Delete</button>";
                }
            }
        ]
    });


    $("#employers").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this employer?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/employers" + button.attr("data-movie-name"),
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    });
}());
