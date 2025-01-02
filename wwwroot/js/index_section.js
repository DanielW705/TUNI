"use strict";
(() => {

    const button_listener = (e, comment_button, comment_section) => {
        if (comment_section.querySelectorAll('.comment').length > 0) {
            if (comment_section.style.display === '') {
                comment_section.style.height = '0%';
                comment_section.style.display = 'none';
            }
            else {
                comment_section.style.display = null;
                comment_section.style.height = null;
            }
        }
    }

    const agregar_listener = (publicacion) => {
        let comment_button = publicacion.querySelector('button.comment');
        let comment_section = publicacion.querySelector('.comments');
        comment_button.addEventListener('click', (e) => button_listener(e, comment_button, comment_section), false);
    }

    const publicaciones = document.querySelectorAll('.publicacion');


    publicaciones.forEach(agregar_listener);
})()