var url = 'Log/prueba';
var data = new FormData();
var input = document.getElementById('file2');
var files = input.files;
var span = document.getElementById('spn');
const lanzarAjax = (e) => {
    e.preventDefault();
    if (files.length == 0) {
        Swal.fire({
            text: 'Debes tener un archivo para poder subirlo',
            icon: 'error',
            timer: 2000,
            allowOutsideClick: false,
            allowEscapeKey: true,
            allowEnterKey: false,
            stopKeydownPropagation: false,
            backdrop: false,
            showConfirmButton: false,
            background: 'linear-gradient(217deg, rgba(6,243,243,.8), rgba(0,128,128,.8) 100%), linear-gradient(127deg, rgba(49,145,126,.8), rgba(95,225,199,.8) 100%), linear-gradient(336deg, rgba(140,51,238,.8), rgba(119,57,187,.8) 100%)',
            customClass:
            {
                container: 'swetalertcontsuccA',
                popup: 'swetalert-succA',
                icon: 'swetalert-succiconA',
                content: 'swetalert-succtitleA',
            },
            showClass: {
                popup: 'animate__animated animate__bounceIn'
            },
            hideClass: {
                popup: 'animate__animated animate__rotateOut'
            }
        });
    }
    else {
        for (var i = 0; i < files.length; i++) {
            var file = document.getElementById('file2').files[i];
            data.append("file", file);
        }
        fetch(url, {
            method: 'POST',
            body: data
        }).then(async (response) => {
            response = await response.json();

        })
       .catch(error => console.error(error))
        $.ajax({
            url: url,
            type: 'POST',
            processData: false,
            contentType: false,
            data: data,
            async: true,
            timeout: 1000,
            success: function (succ) {
                if (succ == true) {
                    Swal.fire({
                        text: 'Se subido el archivo con exito',
                        icon: 'success',
                        confirmButtonText: 'Aceptar',
                        timer: 2000,
                        allowOutsideClick: false,
                        allowEscapeKey: true,
                        allowEnterKey: false,
                        stopKeydownPropagation: false,
                        backdrop: false,
                        showConfirmButton: false,
                        background: 'linear-gradient(217deg, rgba(6,243,243,.8), rgba(0,128,128,.8) 100%), linear-gradient(127deg, rgba(49,145,126,.8), rgba(95,225,199,.8) 100%), linear-gradient(336deg, rgba(140,51,238,.8), rgba(119,57,187,.8) 100%)',
                        customClass:
                        {
                            popup: 'swetalert-succA',
                            icon: 'swetalert-succiconA',
                            content: 'swetalert-succtitleA',
                        },
                        showClass: {
                            popup: 'animate__animated animate__backInDown'
                        },
                        hideClass: {
                            popup: 'animate__animated animate__backOutUp'
                        }
                    });
                }
                else {
                    Swal.fire({
                        text: 'A surgido un error desconocido',
                        icon: 'error',
                        confirmButtonText: 'Aceptar',
                        showCloseButton: true,
                        allowOutsideClick: false,
                        allowEscapeKey: true,
                        allowEnterKey: false,
                        stopKeydownPropagation: false,
                        backdrop: false
                    });
                }
            }
            , error: function (err) { console.log(err); alert(err) }
        });
        input.value = '';
        let output = document.getElementById("selector2");
        output.innerHTML = 'Subir archivo';
        output.classList.remove('fileactive');
    }
}
var loader = function (e) {
    let filer = e.target.files;
    let output = document.getElementById("selector2");
    output.innerHTML = `Selected File: ${filer[0].name}`;
    output.className = 'fileactive';
    span.innerHTML = '';
};
let fileInput2 = document.getElementById('file2');
fileInput2.addEventListener('change', loader);