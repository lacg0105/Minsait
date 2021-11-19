$(document).ready(function () {
    DefinirGrids();
    fnObtenerCarteraClientes();
});
//-------------------------------------------------------------------
function DefinirGrids() {
    $("#grdCarteraCliente").kendoGrid({
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
            { title: 'Cliente/Razón Social', field: 'NombreCliente', width: 100, filterable: { multi: true, search: true }, },
            { title: 'Identificador Fiscal', field: 'IdentificadorFiscal', width: 100, filterable: { multi: true, search: true }, },
            { title: 'Email', field: 'Email', width: 100, filterable: { multi: true, search: true }, },
            { title: 'País', field: 'NombrePais', width: 100, filterable: { multi: true, search: true }, },
            { title: 'Mercado', field: 'NombreMercado', width: 100, filterable: { multi: true, search: true }, },
            {
                title: "Editar",
                width: 50,
                template: function (dataItem) {
                    return "<a class='editar btn btn-success btn-outline btn-circle' onclick='fnEditarCliente(\"" + dataItem.IdCliente + " \")' ><i class='fa fa-edit'></i></a>"
                }
            },
            { title: 'IdCliente', field: 'IdCliente', hidden: true, },
        ],
    });
}
//-------------------------------------------------------------------
function fnObtenerCarteraClientes() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "ObtenerCarteraClientes",
        contentType: false,
        processData: false,
        success: function (operationResult) {
            var dataCarteraCliente = new kendo.data.DataSource({
                transport: {
                    read:
                        function(options) {
                            options.success(operationResult);
                    }
                },
                ServerFiltering: false,
                sort: {
                    field: "IdCliente",
                    dir: "asc"
                },
                schema: {
                    model: {

                        fields: {
                            IdCliente: { type: "number" },
                            IdPais: { type: "number" },
                            IdMercado: { type: "number" },
                            NombreCliente: { type: "string" },
                            IdentificadorFiscal: { type: "string" },
                            Email: { type: "string" },
                            NombrePais: { type: "string" },
                            NombreMercado: { type: "string" }
                        }
                    }
                },
                pageSize: 50,
            });
            var grid = $("#grdCarteraCliente").data("kendoGrid");
            grid.setDataSource(dataCarteraCliente);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(thrownError, 'Error en el proceso');
        }
    });
}
//-------------------------------------------------------------------
function fnEditarCliente(IdCliente) {
    window.location.href = $("#hdnUrlEditCliente").val() + "?IdCliente=" + IdCliente;
}
//-------------------------------------------------------------------