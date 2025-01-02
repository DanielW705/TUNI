'use strict';
import { get_home_url, info_user } from './User_information.js';
(async () => {


    const divNavOp = document.querySelector('.menu .login-options');

    const ancle_logout = document.createElement('a');

    const ancle_profile = document.createElement('a');

    const icono = document.getElementById('TUNI-ICON');

    icono.href = get_home_url();

    ancle_logout.classList.add('logout_icon');

    ancle_logout.href = '/Principal/cerrarsesion';

    ancle_profile.classList.add('profile_icon');

    ancle_profile.href = '/Principal/VerPerfil';

    divNavOp.append(ancle_profile, ancle_logout);
})()