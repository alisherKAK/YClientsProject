$(".company-details").on("click", function () {
    $("#companyDetails").load("/Company/Details/" + $(this).attr("data-id"), null, function () {
        load_js();
    });
});

$(".company-edit").on("click", function () {
    $("#partialView").load("/Company/Edit/" + $(this).attr("data-id"), null, function () {
        load_js("/Scripts/EditCompany.js");
    });
});

$(".company-delete").on("click", function () {
    $("#partialView").load("/Company/Delete/" + $(this).attr("data-id"), null, function () {
        load_js();
    });
});

function load_js(src) {
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.src = src;
    head.appendChild(script);
}