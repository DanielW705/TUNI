'use strict';
(() => {
    const body_page = document.getElementById('body_page');
    const vp = document.getElementById('vp');
    const inf = document.getElementById('inf');
    const resulte = document.getElementById('resulte');
    const url_test = '/Principal/_testvocacional';
    const url_visitar_uni = '/Principal/_vistaUNI';
    const url_resultados = '/Principal/resultados';
    const call_exit_message = () => {
        let timerInterval;
        Swal.fire({
            position: 'center',
            html: 'La paguina se va reiniciar en: <b></b> milliseconds.',
            timer: 2000,
            allowOutsideClick: false,
            allowEscapeKey: false,
            allowEnterKey: false,
            timerProgressBar: true,
            onBeforeOpen: () => {
                Swal.showLoading()
                timerInterval = setInterval(() => {
                    const content = Swal.getContent()
                    if (content) {
                        const b = content.querySelector('b')
                        if (b) {
                            b.textContent = Swal.getTimerLeft()
                        }
                    }
                }, 100)
            },
            onClose: () => {
                clearInterval(timerInterval)
            }
        }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {
                location.reload();
                document.getElementById('vp').setAttribute('hidden', '');
            }
        });
    }
    const llenar_test_vocacional = (body_page) => {
        let i = body_page.querySelector('input[name="seleccion"]:checked').values;
        let formData = new FormData();
        formData.append('i', i);
        fetch(url_test,
            {
                method: 'POST', body: formData
            })
            .then(async (response) => {
                response = await response.text();
                body_page.innerHTML = response;
                const btn_sig_pre = body_page.querySelector('#btnPregunta');
                if (btn_sig_pre)
                    btn_sig_pre.addEventListener('click', () => llenar_test_vocacional(body_page));
                else {
                    call_exit_message();
                    body_page.querySelector('#btnt').removeAttribute('disabled');
                    body_page.querySelector('#btnc').removeAttribute('disabled');
                    body_page.querySelector('#btnr').removeAttribute('disabled');
                    body_page.querySelector('#presoname').removeAttribute('disabled');
                }
            }).
            catch(error => console.error(error));
        if (i > 1) {
            document.getElementById('btnt').setAttribute('disabled', true);
            document.getElementById('btnc').setAttribute('disabled', true);
            document.getElementById('btnr').setAttribute('disabled', true);
            document.getElementById('presoname').setAttribute('disabled', true);
        }
    }
    const llamar_test_vocacional = () => {
        fetch(url_test, { method: 'GET' })
            .then(async (response) => {
                response = await response.text();
                body_page.innerHTML = response;
                vp.style.zIndex = 1;
                const btn_sig_pre = body_page.querySelector('#btnPregunta');
                btn_sig_pre.addEventListener('click', () => llenar_test_vocacional(body_page));
            }).catch(error => console.error(error));
    };


    const llamar_visita_uni = (id) => {
        let dataForm = new FormData();
        dataForm.append('id', id.value);
        fetch(url_visitar_uni,
            {
                method: 'POST',
                body: dataForm
            })
            .then(async (response) => {
                response = await response.text();
                inf.innerHTML = response;
            }).catch(error => console.error(error));
    }
    const llamar_los_resultados = () => {
        fetch(url_resultados, { method: 'GET' })
            .then(async (response) => {
                response = await response.json();
                document.getElementById('resulte').remove();
                let div = create_table(response);
                document.getElementById('mostarr').appendChild(div);
            }).catch(error => console.error(error));
    };


    const create_table = (data) => {
        let div = document.createElement('div');
        div.className = 'divtablitare';
        let table = document.createElement('table');
        table.className = 'tablitare';
        let thead = document.createElement('thead');
        let tableRow = document.createElement('tr');
        let tbody = document.createElement('tbody');
        for (let i = 0; i < 2; i++) {
            let tableHead = document.createElement('th');
            if (i == 0) {
                tableHead.textContent = 'Area';
            }
            if (i == 1) {
                tableHead.textContent = 'Valor';
            }
            tableRow.appendChild(tableHead);
            thead.appendChild(tableRow);
        }
        data.forEach((resultado) => {
            let tableRow = document.createElement('tr');
            let tableData_1 = document.createElement('td');
            let tableData_2 = document.createElement('td');
            tableData_1.textContent = resultado.area;
            tableData_2.textContent = resultado.total;
            tableRow.appendChild(tableData_1);
            tableRow.appendChild(tableData_2);
            tbody.appendChild(tableRow);
        });
        table.appendChild(thead);
        table.appendChild(tbody);
        div.appendChild(table);
        return div;
    }

    if (!model4)
        llamar_test_vocacional();
    let btn_invokar = document.getElementById('invokar');
    if (btn_invokar)
        btn_invokar.addEventListener('click', llamar_test_vocacional);

    let ids = document.querySelectorAll('[btns]');
    ids.forEach(id => id.addEventListener('click', () => llamar_visita_uni(id)));

    if (resulte)
        resulte.addEventListener('click', llamar_los_resultados);
})();