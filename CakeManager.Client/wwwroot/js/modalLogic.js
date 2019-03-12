window.showModal = async (data) => {
    $("#" + data.id).modal('show');
    return true;
};

window.hideModal = async (data) => {
    $("#" + data.id).modal('hide');
    return true;
};