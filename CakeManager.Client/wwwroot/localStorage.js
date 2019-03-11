window.getItem = async (data) => {
    return window.localStorage.getItem(data.key);
};

window.setItem = async (data) => {
    window.localStorage.setItem(data.key, data.value);
    return true;
};

window.removeItem = async (data) => {
    window.localStorage.removeItem(data.key);
    return true;
}