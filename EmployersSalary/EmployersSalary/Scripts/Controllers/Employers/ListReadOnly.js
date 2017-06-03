(function () {
    var table = $("#employers").DataTable({
        ajax: {
            url: "/api/employers",
            dataSrc: ""
        },
        columns: [
            {
                data: "firstName"
            }
            ,
            {
                data: "lastName"
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
            }
        ]
    });

}());
