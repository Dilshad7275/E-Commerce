var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/Category/AllCategories"
        },
        "columns":
            [
                { "data": "name" },
                { "data": "displayOrder" },
                
               
            ]
    });
});