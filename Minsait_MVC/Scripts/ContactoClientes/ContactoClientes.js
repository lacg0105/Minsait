$(document).ready(function () {
    DefinirGrids();
    fnObtenerContactoClientes();
});
//-------------------------------------------------------------------
function DefinirGrids() {
    $("#grdContactoCliente").kendoGrid({
        height: 550,
        reorderable: true,
        sortable: true,
        pageable: {
            messages: {
                display: "{0} - {1} de {2} Registros", //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records
                empty: "No hay elementos para mostrar",
                page: "Página",
                allPages: "Todas",
                of: "de {0}", //{0} is total amount of pages
                itemsPerPage: "Registros por página",
                first: "Ir a la primera página",
                previous: "Ir a la página anterior",
                next: "Ir a la página siguiente",
                last: "Ir a la última página",
                refresh: "Actualizar"
            },
            refresh: false,
            pageSizes: true,
            buttonCount: 5
        },
        filterable: {
            messages: {
                filter: "Filtrar", // sets the text for the "Filter" button
                clear: "Borrar", // sets the text for the "Clear" button
            }
        },
        columns: [
            { title: 'Nombre Contacto', field: 'NombreContacto', width: 100, filterable: { multi: true, search: true }, },
            { title: 'Puesto', field: 'PuestoContacto', width: 100, filterable: { multi: true, search: true }, },
            { title: 'Correo', field: 'CorreoContacto', width: 100, filterable: { multi: true, search: true }, },
            { title: 'Número', field: 'NumeroContacto', width: 100, filterable: { multi: true, search: true }, },
            {
                title: "Editar",
                width: 50,
                template: function (dataItem) {
                    return "<a class='editar btn btn-success btn-outline btn-circle' onclick='fnEditarContacto(\"" + dataItem.IdContactoCliente + " \")' ><i class='fa fa-edit'></i></a>"
                }
            },
            { title: 'IdContactoCliente', field: 'IdContactoCliente', hidden: true, },
            { title: 'IdCliente', field: 'IdCliente', hidden: true, },
            { title: 'Cliente/Razón Social', field: 'NombreCliente', hidden: true, },
        ],
    });
}
//-------------------------------------------------------------------
function fnObtenerContactoClientes() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "ObtenerContactoClientes",
        contentType: false,
        processData: false,
        success: function (operationResult) {
            var dataContactoCliente = new kendo.data.DataSource({
                transport: {
                    read:
                        function (options) {
                            options.success(operationResult);
                        }
                },
                ServerFiltering: false,
                sort: {
                    field: "IdCliente",
                    dir: "asc"
                },
                group: [
                    { field: "NombreCliente" },
                ],
                schema: {
                    model: {

                        fields: {
                            IdContactoCliente: { type: "number" },
                            IdCliente: { type: "number" },
                            NombreCliente: { type: "string" },
                            NumeroContacto: { type: "string" },
                            PuestoContacto: { type: "string" },
                            CorreoContacto: { type: "string" },
                            NombreContacto: { type: "string" }
                        }
                    }
                },
                pageSize: 50,
            });
            var grid = $("#grdContactoCliente").data("kendoGrid");
            grid.setDataSource(dataContactoCliente);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(thrownError, 'Error en el proceso');
        }
    });
}
//-------------------------------------------------------------------
function fnEditarContacto(IdContactoCliente) {
    window.location.href = $("#hdnUrlEditContacto").val() + "?IdContactoCliente=" + IdContactoCliente;
}
//-------------------------------------------------------------------