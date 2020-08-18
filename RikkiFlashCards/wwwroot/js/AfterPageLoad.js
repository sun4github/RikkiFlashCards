//$('#ImageFiles').change(function () {
//    alert('call - readURL');
//    readURL(this);
//});

const inputElement = document.querySelector('input[type="file"]');
const pond = FilePond.create(inputElement);
