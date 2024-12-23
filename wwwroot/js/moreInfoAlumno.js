'use strict';
(() => {
    const areaa = document.getElementById('alertas');
    const fileInput = document.getElementById('file');
    const tabs = document.querySelectorAll('[data-tab-target]');
    const content = document.querySelectorAll('[data-tab-content]');


    const loader = (e) => {
        let filer = e.target.files;
        let output = document.getElementById("selector");
        output.innerHTML = `Selected File: ${filer[0].name}`;
        output.className = 'fileactive';
    };

    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            const target = document.querySelector(tab.dataset.tabTarget);
            content.forEach(con => con.setAttribute('hidden', true));
            tabs.forEach(tab => tab.classList.remove('active'));
            tab.classList.add('active');
            target.removeAttribute('hidden');
        });

    });
    fileInput.addEventListener('change', loader);
})()