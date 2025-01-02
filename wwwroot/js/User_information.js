"use strict";
const get_all_info_user = async () => {
    const cookie = await cookieStore.get('DataUser');


    const decodeString = decodeURIComponent(cookie.value);

    return JSON.parse(decodeString);
}

const get_home_url = () => info_user.UserType === 'Alumno' ? '/Principal/IndexAlumno' : '/Principal/IndexUniversidad';

const info_user = await get_all_info_user();
export {
    get_home_url,
    info_user
}