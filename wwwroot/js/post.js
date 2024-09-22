let mypost;
$(document).ready(function () {
    mypost = $('#mypost').DataTable({
        "ajax": {
            "url": "/admin/Posts/GetAll", // Use your specified URL
            "type": "GET",
            "datatype": "json",
            "dataSrc": function (json) {
                // Assuming the product data is inside json.data.$values
                return json.data.$values || json; // Return the data correctly based on your API response
            }
        },
        "columns": [
            { "data": "title" },
            { "data": "body" },
            {
                "data": "image",
                "render": function (data) {
                    var img = '/content/blog/' + data;
                    return '<img src="' + img + '" height="50px" width="50px">';
                }
            },
            { "data": "description" },
            { "data": "category" },
            { "data": "tags" },
            {
                "data": "id",
                "render": function (data) {
                    return "<a class='btn btn-success' href='/Admin/Posts/Edit/" + data + "'>Edit</a>";
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return "<button class='btn btn-danger js-delete' data-post-id='" + data + "'>Delete</button>";
                }
            }
        ]
    });

    $("#mypost").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this post?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/posts/" + button.attr("data-post-id"), // Use data-post-id for the delete action
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    },
                    error: function () {
                        alert("Error deleting post.");
                    }
                });
            }
        });
    });
});
