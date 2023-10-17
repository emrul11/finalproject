let editable = document.querySelectorAll('.editable');
let edittableId = document.getElementById('cvEditable');
let saveButton = document.getElementById('saveData');

let uploadImage = document.getElementById('image-upload');

let imageChange = document.getElementById('image-change');

let ulli = document.querySelectorAll('ul li');

const editButton = document.querySelector('.mkp__card');

const addRemoveIcon = document.querySelectorAll('.social-media-add-remove');

// Get the modal
let modal = document.getElementById("myModal");
// Get the <span> element that closes the modal
let span = document.getElementsByClassName("close")[0];

uploadImage.addEventListener('click', function (event) {
    modal.style.display = 'block'
    console.log(uploadImage);
});
span.addEventListener("click", function (event) {

    modal.style.display = 'none';

});

edittableId.addEventListener('click', function (ev) {
    ev.preventDefault();
    //console.log("clicked edit button");
    editable.forEach(function (el) {
        el.setAttribute('contenteditable', 'true');
        el.style.border = '1px solid #ddd';
        el.style.padding = '5px 10px';
        console.log("clicked edit button");
    })
    saveButton.style.display = 'inline-block';
    AddRemoveButtonShow();

});

function AddRemoveButtonShow() {
    addRemoveIcon.forEach(function (v) {
        console.log('hello')
        v.style.display = 'block';
    })
}



const addressForSocialLink = document.getElementById('addressuser');
addressForSocialLink.addEventListener('click', function (ev) {
    console.log(ev.target);
    // add element 
    if (ev.target.classList.contains('social-media-add')) {
        let parentElement = ev.target.parentNode.parentNode;

     let clone =   parentElement.cloneNode(true);
        parentElement.after(clone);
    }

    // remove element

    if (ev.target.classList.contains('social-media-remove')) {
        let parentElement = ev.target.parentNode.parentNode.parentNode;
        parentElement.removeChild(ev.target.parentNode.parentNode);
        
    }


})

const workexperence = document.querySelector('#workexperence');

workexperence.addEventListener('click', function (ev) {

    if (ev.target.classList.contains('social-media-add')) {
        let parentElement = ev.target.parentNode.parentNode;

        let clone = parentElement.cloneNode(true);
        parentElement.after(clone);
    }

    // remove element

    if (ev.target.classList.contains('social-media-remove')) {
        let parentElement = ev.target.parentNode.parentNode.parentNode;
        parentElement.removeChild(ev.target.parentNode.parentNode);
    }

})


const indistalProject = document.querySelector('.mkp__industrial__personal__project');
indistalProject.addEventListener('click', function (ev) {

    if (ev.target.classList.contains('social-media-add')) {
        let parentElement = ev.target.parentNode.parentNode;

        let clone = parentElement.cloneNode(true);
        parentElement.after(clone);
    }
    // remove element
    if (ev.target.classList.contains('social-media-remove')) {
        let parentElement = ev.target.parentNode.parentNode.parentNode;
        parentElement.removeChild(ev.target.parentNode.parentNode);
    }

})


const education = document.querySelector('.mkp__eduction');
education.addEventListener('click', function (ev) {

    if (ev.target.classList.contains('social-media-add')) {
        let parentElement = ev.target.parentNode.parentNode;

        let clone = parentElement.cloneNode(true);
        parentElement.after(clone);
    }
    // remove element
    if (ev.target.classList.contains('social-media-remove')) {
        let parentElement = ev.target.parentNode.parentNode.parentNode;
        parentElement.removeChild(ev.target.parentNode.parentNode);
    }

})



const referance = document.querySelector('.mkp__referrance');
referance.addEventListener('click', function (ev) {

    if (ev.target.classList.contains('social-media-add')) {
        let parentElement = ev.target.parentNode.parentNode;
        let clone = parentElement.cloneNode(true);
        parentElement.after(clone);
    }
    // remove element
    if (ev.target.classList.contains('social-media-remove')) {
        let parentElement = ev.target.parentNode.parentNode.parentNode;
        parentElement.removeChild(ev.target.parentNode.parentNode);
    }

})
