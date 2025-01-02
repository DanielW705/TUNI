import { get_home_url, info_user } from './User_information.js';
(async () => {

    const divNavOp = document.querySelector('.menu .login-options');

    const perfil_icon = divNavOp.querySelector('.profile_icon');

    perfil_icon.remove();

    const ancle_home = document.createElement('a');

    ancle_home.classList.add('home_icon');

    ancle_home.href = get_home_url();

    divNavOp.append(ancle_home);

})()