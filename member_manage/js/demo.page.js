$(document).ready(function () {
    excel = new ExcelGen({
        "src_id": "list1",
        "show_header": true
    });
    $("#generate-excel").click(function () {
        console.log("excel1111111111");
        excel.generate();
    });
});