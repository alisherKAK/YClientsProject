$("#btn-createCompany").on("click", function (e) {
    var company = $("#form-createCompany").serialize();

    $.post("/Company/Create", company, function (json) {
        var data = JSON.parse(json);
        if (data == undefined) {
            alert(json);
            return;
        }

    }).then(function () {
        $.get("/Company/GetCompanies", null, function (res) {
            var companies = JSON.parse(res);
            $("#tbody-company").html("");
            for (var i = 0; i < companies.length; i++) {
                var editRef = "<a data-id='" + companies[i].Id + "' class='company-edit'>Edit</a> | ";
                var detailsRef = "<a data-id='" + companies[i].Id + "' class='company-details'>Details</a> | ";
                var deleteRef = "<a data-id='" + companies[i].Id + "' class='company-delete'>Delete</a>";

                $("#tbody-company").append("<tr> <td>" + companies[i].Name + "</td> <td>" + companies[i].City + "</td> <td>" + editRef + detailsRef + deleteRef + "</td> </tr>");
            }
        });
    });
});