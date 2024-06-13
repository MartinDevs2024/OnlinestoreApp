var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblTodos").DataTable({
        "ajax": {
            "url": "/Admin/TodoList/GetAll"
        },
        "columns": [
            { "data": "title", "width": "25%" },
            { "data": "description", "width": "25%" },
            { "data": "isVisible", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/todolist/edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a href="/Admin/todoList/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "40%"
            }
        ]
    });
}
