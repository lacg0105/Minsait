$(document).ready(function () {
    fnObtenerClientes();

    $("#frmContacto").kendoValidator({
        rules: {
            matches: function (input) {
                var matches = input.data('matches');
                // if the `data-matches attribute was found`
                if (matches) {
                    // get the input to match
                    var match = $(matches);
                    // trim the values and check them
                    if ($.trim(input.val()) === $.trim(match.val())) {
                        // the fields match
                        return true;
                    } else {
                        // the fields don't match - validation fails
                        return false;
                    }
                }
                // don't perform any match validation on the input
                return true;
            }
        },
        messages: {
            email: "No es una dirección de correo válida",
            matches: function (input) {
                return input.data("matchesMsg");
            },
            required: "Favor de llenar este campo.",
        }
    });

    $("#btnGuardarContacto").click(function () {
        fnGuardar();
        return false;
    })


    if ($("#hdnMov").val() == "NUEVO") {
        fnControlesEstado("INICIO");
    }
    else {
        fnControlesEstado("EDITAR");
    }
});
//-------------------------------------------------------------------
function fnObtenerClientes() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "ObtenerClientes",
        contentType: false,
        processData: false,
        success: function (operationResult) {
            var SelCliente = $("#selCliente").kendoDropDownList({
                dataSource: new kendo.data.DataSource({
                    transport: {
                        read:
                            function (options) {
                                options.success(operationResult);
                            }
                    },
                    ServerFiltering: false,
                    sort: {
                        field: "NombreCliente",
                        dir: "asc"
                    },
                    schema: {
                        model: {

                            fields: {
                                IdCliente: { type: "number" },
                                NombreCliente: { type: "string" },
                            }
                        }
                    },
                    pageSize: 250,
                }),
                dataTextField: "NombreCliente",
                dataValueField: "IdCliente",
                optionLabel: "Seleccione una Cliente",
            }).data("kendoDropDownList");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(thrownError, 'Error en el proceso');
        }
    });
}
//-------------------------------------------------------------------
function fnGuardar() {
    var validator = $("#frmContacto").data("kendoValidator");
    if (validator.validate()) {

        if ($("#hdnMov").val() == "NUEVO") {
            var url = "CrearContactoCliente";
        }
        else {
            var url = "EditarContactoCliente";
        }

        $('#ibox2').children('.ibox-content').toggleClass('sk-loading');
        var datToSend = $("#frmContacto").serialize();
        $.ajax({
            method: "POST",
            url: url,
            //async: false,
            data: datToSend,
            success: function (operationResult) {
                $('#ibox2').children('.ibox-content').removeClass('sk-loading');
                if (operationResult == "\"1\"") {
                    swal({
                        title: "Proceso completado con éxito.",
                        text: "El registro del contacto se registró en BD.",
                        type: "success",
                        showConfirmButton: true
                    },
                        function () {
                            window.location.href = $("#hdnUrlContacto").val();
                        });


                }
                else {
                    console.log(operationResult);
                    swal("Validación", "Error al guardar el registro del contacto.", "error");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(thrownError, 'Error en el proceso');
                $('#ibox2').children('.ibox-content').removeClass('sk-loading');
            }
        });


    }
}
//-------------------------------------------------------------------
function fnControlesEstado(sEstado) {

    switch (sEstado) {
        case "INICIO":
            setTimeout(function () { $("#selCliente").data("kendoDropDownList").value() }, 1000);
            $("#txtNombreContacto").val("");
            $("#txtPuestoContacto").val("");
            $("#txtCorreo").val("");
            $("#txtNumeroContacto").val("");
            break;

        case "EDITAR":
            setTimeout(function () { $("#selCliente").data("kendoDropDownList").value($("#IdCliente").val()) }, 1000);
            break;
    }
}
//-------------------------------------------------------------------