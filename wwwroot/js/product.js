var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/admin/product/getall", // API endpoint for getting all products
            "type": "GET",
            "datatype": "json",
            "dataSrc": function (json) {
                // Assuming the product data is inside json.data.$values
                return json.data.$values;
            }
        },
        "columns": [
            { "data": "name", "width": "15%" },             // Name of the product
            { "data": "isbn", "width": "15%" },             // ISBN
            { "data": "price", "width": "15%" },            // Price
            { "data": "author", "width": "15%" },           // Author
            { "data": "category.name", 
              "render": function(data) {
                return data ? data : "No Category";
              },  
                "width": "15%" },    // Category name (nested under Category)
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> 
                            </a>
                            <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt"></i> 
                            </a>
                        </div>
                    `;
                }, "width": "40%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url, // Delete API call to the backend
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        // Reload the datatable after successful deletion
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    // Handle error scenarios
                    toastr.error('An error occurred while trying to delete the product.');
                }
            });
        }
    });
}
