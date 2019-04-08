window.showToast = async (data) => {
    $("#Toast_" + data.id).toast("show");
    return true;
};