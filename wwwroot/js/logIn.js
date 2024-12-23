"use strict";
(
    () => {
        let z = document.getElementById('btn');
        let y = document.getElementById('login');
        let x = document.getElementById('register');

        let btn_ingresar = document.querySelector(".toggle-btn1");

        let btn_registro = document.querySelector(".toggle-btn2");

        btn_ingresar.addEventListener('click', () => {
            z.style.left = "0%";
            y.style.right = "0%";
            x.style.left = "0%";
        }, false);

        btn_registro.addEventListener('click', () => {
            z.style.left = "50%";
            y.style.right = "100%";
            x.style.left = "-100%";
        }, false);
    }
)()