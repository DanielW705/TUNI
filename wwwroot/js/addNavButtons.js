'use strict';
(() => {
    const divNavOp = document.querySelector('.menu .login-options');
    const ancle_logout = document.createElement('a');
    ancle_logout.classList.add('logout_icon');
    ancle_logout.href = '/Principal/cerrarsesion';
    const ancle_profile = document.createElement('a');
    ancle_profile.classList.add('profile_icon');
    ancle_profile.href = '/Principal/VerPerfil';
    divNavOp.append(ancle_logout, ancle_profile);
})()