$(document).ready(function () {
    fnObtenerPaises();
    fnObtenerMercados();

    $("#frmCliente").kendoValidator({
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

    $("#btnGuardarCliente").click(function () {
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
function fnObtenerPaises() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "ObtenerPaises",
        contentType: false,
        processData: false,
        success: function (operationResult) {
            var SelContrapartes = $("#selPais").kendoDropDownList({
                dataSource: new kendo.data.DataSource({
                    transport: {
                        read:
                            function (options) {
                                options.success(operationResult);
                            }
                    },
                    ServerFiltering: false,
                    sort: {
                        field: "NombrePais",
                        dir: "asc"
                    },
                    schema: {
                        model: {

                            fields: {
                                IdPais: { type: "number" },
                                NombrePais: { type: "string" },
                            }
                        }
                    },
                    pageSize: 250,
                }),
                dataTextField: "NombrePais",
                dataValueField: "IdPais",
                optionLabel: "Seleccione una País",
            }).data("kendoDropDownList");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(thrownError, 'Error en el proceso');
        }
    });
}
//-------------------------------------------------------------------
function fnObtenerMercados() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "ObtenerMercados",
        contentType: false,
        processData: false,
        success: function (operationResult) {
            var SelContrapartes = $("#selMercado").kendoDropDownList({
                dataSource: new kendo.data.DataSource({
                    transport: {
                        read:
                            function (options) {
                                options.success(operationResult);
                            }
                    },
                    ServerFiltering: false,
                    sort: {
                        field: "NombreMercado",
                        dir: "asc"
                    },
                    schema: {
                        model: {

                            fields: {
                                IdMercado: { type: "number" },
                                NombreMercado: { type: "string" },
                            }
                        }
                    },
                    pageSize: 250,
                }),
                dataTextField: "NombreMercado",
                dataValueField: "IdMercado",
                optionLabel: "Seleccione una Mercado",
            }).data("kendoDropDownList");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(thrownError, 'Error en el proceso');
        }
    });
}
//-------------------------------------------------------------------
function fnGuardar() {
    var validator = $("#frmCliente").data("kendoValidator");
    if (validator.validate()) {

        if ($("#hdnMov").val() == "NUEVO") {
            var url = "CrearCliente";
        }
        else {
            var url = "EditarCliente";
        }

        $('#ibox2').children('.ibox-content').toggleClass('sk-loading');
        var datToSend = $("#frmCliente").serialize();
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
                        text: "El registro del cliente se registró en BD.",
                        type: "success",
                        showConfirmButton: true
                    },
                        function () {
                            window.location.href = $("#hdnUrlCliente").val();
                        });


                }
                else if (operationResult == "\"2\"") {
                    swal("Validación", "El cliente ya está registrado.", "error");
                }
                else {
                    console.log(operationResult);
                    swal("Validación", "Error al guardar el registro del cliente.", "error");
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
            setTimeout(function () { $("#selMercado").data("kendoDropDownList").value() }, 1000);
            setTimeout(function () { $("#selPais").data("kendoDropDownList").value() }, 1000);
            $("#txtCliente").val("");
            $("#txtIdentificadorFiscal").val("");
            $("#txtEmail").val("");
            break;

        case "EDITAR":
            setTimeout(function () { $("#selMercado").data("kendoDropDownList").value($("#hdnIdMercado").val()) }, 1000);
            setTimeout(function () { $("#selPais").data("kendoDropDownList").value($("#hdnIdPais").val()) }, 1000);
            break;
    }
}
//-------------------------------------------------------------------
