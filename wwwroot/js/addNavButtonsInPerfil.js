(async () => {

    const get_all_info_user = async () => {
        const cookie = await cookieStore.get('DataUser');


        const decodeString = decodeURIComponent(cookie.value);

        return JSON.parse(decodeString);
    }

    const info_user = await get_all_info_user();

    const get_home_url = () => info_user.UserType === 'Alumno' ? '/Principal/IndexAlumno' : '/Principal/IndexUniversidad';

    const divNavOp = document.querySelector('.menu .login-options');

    const perfil_icon = divNavOp.querySelector('.profile_icon');

    perfil_icon.remove();

    ancle_home = document.createElement('a');

    ancle_home.classList.add('home_icon');

    ancle_home.href = get_home_url();

    divNavOp.append(ancle_home);

})()