

let uploadInput = document.querySelector('.upl-input');
let uploadButton = document.querySelector('.upl-btn');

uploadInput.addEventListener('input', () => {

    if (uploadInput.value !== '') {
        console.log(uploadInput.value);
        uploadButton.style.display = 'block';
    }
});

document.querySelector('.upl-btn').addEventListener('click', () => {

    setTimeout(() => {
        document.querySelector('.upl-input').value = '';
    }, 1000)
});