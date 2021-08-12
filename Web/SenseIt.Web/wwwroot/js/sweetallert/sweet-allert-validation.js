function validateUpload() {
    if (document.getElementById("uploadBox").value == "") {
        Swal.fire(
            'Error!',
            'Please upload an image.',
            'error'
        )

        return false;
    }

    return true;

}