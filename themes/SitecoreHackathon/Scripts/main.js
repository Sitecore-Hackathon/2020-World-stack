function notOnPageEditor() {
    if (document.body.classList.contains("on-page-editor")) {
        return false;
    } else {
        return true;
    }
};

function notOnPreview() {
    if (document.body.classList.contains("preview")) {
        return false;
    } else {
        return true;
    }
};