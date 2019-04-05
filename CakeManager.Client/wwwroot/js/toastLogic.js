window.initializeToast = async () => {
    $(".toast").toast({
        animation: true,
        autohide: true,
        delay: 2000
    });
    return true;
};

window.showToast = async () => {
    $(".toast").toast("show");
    return true;
};

$(() => {
    window.initializeToast();
});