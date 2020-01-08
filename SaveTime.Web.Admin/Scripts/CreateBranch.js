$.get("/Company/GetCompanies", null, function (res) {
    var companies = JSON.parse(res);
    for (var i = 0; i < companies.length; i++) {
        $("#slct-company").append("<option value='" + companies[i].Id + "'>" + companies[i].Name + " - " + companies[i].City + "</option>");
    }
});

$("#btn-createBranch").on("click", function (e) {
    var company = $("#form-createBranch").serialize();

    $.post("/Branch/Create", company, function (json) {
        var data = JSON.parse(json);
        if (data == undefined) {
            alert(json);
            return;
        }

    }).then(function () {
        $.get("/Branch/GetBranches", null, function (res) {
            var barnches = JSON.parse(res);
            $("#tbody-branch").html("");
            for (var i = 0; i < barnches.length; i++) {
                var editRef = "<a data-id='" + barnches[i].Id + "' class='branch-edit'>Edit</a> | ";
                var detailsRef = "<a data-id='" + barnches[i].Id + "' class='branch-details'>Details</a> | ";
                var deleteRef = "<a data-id='" + barnches[i].Id + "' class='branch-delete'>Delete</a>";

                $("#tbody-branch").append("<tr> <td>" + barnches[i].Name + "</td> <td>" + barnches[i].City + "</td> <td>" + editRef + detailsRef + deleteRef + "</td> </tr>");
            }
        });
    });
});