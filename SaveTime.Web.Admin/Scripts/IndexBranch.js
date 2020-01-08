$(".table").on("click", "a[class=branch-details]", function () {
    $("#branchDetails").load("/Branch/Details/" + $(this).attr("data-id"));
});

$(".table").on("click", "a[class=branch-edit]", function () {
    $("#partialView").load("/Branch/Edit/" + $(this).attr("data-id"), null, function () {
        load_js("/Scripts/EditBranch.js");
    });
});

$(".table").on("click", "a[class=branch-delete]", function () {
    $("#partialView").load("/Branch/Delete/" + $(this).attr("data-id"), null, function () {
        ;
    });
});

$("#branch-create").on("click", function () {
    $("#partialView").load("/Branch/Create/" + $(this).attr("data-id"), null, function () {
        load_js("CreateBranch.js");
    });
});

function load_js(src) {
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.src = src;
    head.appendChild(script);
}