$(".table").on("click", "a[class=company-details]", function () {
    $("#companyDetails").load("/Company/Details/" + $(this).attr("data-id"));
});

$(".table").on("click", "a[class=company-edit]", function () {
    $("#partialView").load("/Company/Edit/" + $(this).attr("data-id"), null, function () {
        load_js("/Scripts/EditCompany.js");
    });
});

$(".table").on("click", "a[class=company-delete]", function () {
    $("#partialView").load("/Company/Delete/" + $(this).attr("data-id"), null, function () {;
    });
});

$("#company-create").on("click", function () {
    $("#partialView").load("/Company/Create/" + $(this).attr("data-id"), null, function () {
        load_js("CreateCompany.js");
    });
});

function load_js(src) {
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.src = src;
    head.appendChild(script);
}