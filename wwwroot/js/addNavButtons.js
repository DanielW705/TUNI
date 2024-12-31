'use strict';
(async () => {
    const get_all_info_user = async () => {
        const cookie = await cookieStore.get('DataUser');


        const decodeString = decodeURIComponent(cookie.value);

        return JSON.parse(decodeString);
    }
    const divNavOp = document.querySelector('.menu .login-options');

    const ancle_logout = document.createElement('a');

    const ancle_profile = document.createElement('a');

    const icono = document.getElementById('TUNI-ICON');

    const info_user = await get_all_info_user();

    const get_home_url = () => info_user.UserType === 'Alumno' ? '/Principal/IndexAlumno' : '/Principal/IndexUniversidad';

    icono.href = get_home_url();

    ancle_logout.classList.add('logout_icon');

    ancle_logout.href = '/Principal/cerrarsesion';

    ancle_profile.classList.add('profile_icon');

    ancle_profile.href = '/Principal/VerPerfil';

    divNavOp.append(ancle_profile, ancle_logout);
})()