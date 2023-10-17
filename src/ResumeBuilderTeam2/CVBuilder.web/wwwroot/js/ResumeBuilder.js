function readyDoc() {
    // Get references to the input field and the output heading
    const inputField = document.getElementById('inputField1');
    const outputHeading = document.getElementById('outputHeading');

     //Add an 'input' event listener to the input field
     inputField.addEventListener('input', function() {
         // Update the content of the output heading with the value of the input field
         outputHeading.textContent = inputField.value;

     });

     const checkbox = document.getElementById('showHideCheckbox');
     const hiddenHeading = document.getElementById('outputHeading');

     checkbox.addEventListener('change', function() {
         hiddenHeading.style.display = this.checked ? 'block' : 'none';
     });


     const showHideCheckbox = document.getElementById('showHideCheckbox');
     const cardBody = document.getElementById('cardBody');

     showHideCheckbox.addEventListener('change', function() {
         cardBody.style.display = this.checked ? 'block' : 'none';
     });

    const showHideCheckbox = document.getElementById('showHideCheckbox');
    const cardBody = document.getElementById('introCardBody');

     introShowHideCheckbox.addEventListener('change', function () {
         cardBody.style.display = this.checked ? 'block' : 'none';
     });


    const showHideCheckbox2 = document.getElementById('showHideCheckbox2');
    const cardBody2 = document.getElementById('cardBody2');

    
    const showHideCheckbox3 = document.getElementById('showHideCheckbox3');
    const cardBody3 = document.getElementById('cardBody3');

    showHideCheckbox2.addEventListener('change', function () {
        cardBody2.style.display = this.checked ? 'block' : 'none';
    });


     var quill = new Quill('#editor', {
         theme: 'snow'
     });





}

document.addEventListener('DOMContentLoaded', function () {
    readyDoc();
    //SkillInputHandler();
    SkillInputHandler();
    introInputHandler();
    professionalSummaryHandler();
});



function SkillInputHandler () {
    const inputFieldsDiv = document.getElementById("SkillTab");
    
    inputFieldsDiv.addEventListener("click", function (event) {
        const target = event.target;
        const button = target.closest("button");

        if (button) {
            const inputGroup = button.closest(".input-group");

            if (button.id === "addButton" || button.classList.contains("addButton") ) {
                const newInputGroup = inputGroup.cloneNode(true);
                const inputField = newInputGroup.querySelector(".form-control");

                if(inputField.value.trim()!=""){

                    // const lastChild = inputFieldsDiv.lastElementChild;
                     const lastChildAddButton = inputGroup.querySelector(".btn-primary"); 
                     lastChildAddButton.disabled = true;
                     SkillItemHandler2(inputField.value,1);
                    inputField.value ="";
                    inputFieldsDiv.appendChild(newInputGroup);
                }
            } else if (button.id === "deleteButton" || button.classList.contains("deleteButton")) {
                inputFieldsDiv.removeChild(inputGroup);
                const lastChild = inputFieldsDiv.lastElementChild;
                const lastChildAddButton = lastChild.querySelector(".btn-primary"); 
                lastChildAddButton.disabled = false;

            }
        }
    });
}



function introInputHandler(){
    introName.addEventListener('input', function() {
        introNameView.textContent = introName.value;
    });

    introEmail.addEventListener('input', function() {
        introEmailView.textContent = introEmail.value;
    });

    introContact.addEventListener('input', function() {
        introContactView.textContent = introContact.value;
    });

    introSkype.addEventListener('input', function() {
        introSkypeView.textContent = introSkype.value;
    });

    introLinkedin.addEventListener('input', function() {
        introLinkedinView.textContent = introLinkedin.value;
    });
    introAddress.addEventListener('input', function() {
        introAddressView.textContent = introAddress.value;
    });

}

function professionalSummaryHandler(){
    professionalSummary.addEventListener('input', function() {
        professionalSummaryView.textContent = professionalSummary.value;
    });
}



function SkillItemHandler(value, isAdd) {
    const listContainers = document.querySelectorAll(".col-md-6 ul");
    
    listContainers.forEach((listContainer, index) => {
        console.log(listContainer);
        if (isAdd) {
            const listItem = document.createElement("li");
            listItem.textContent = value;
            listContainer.appendChild(listItem);
        } else {
            const listItems = listContainer.querySelectorAll("li");
            if (listItems.length > 0) {
                listContainer.removeChild(listItems[listItems.length - 1]);
            }
        }
    });
}


function SkillItemHandler2(value, isAdd) {
    const list1 = document.getElementById("SkillList1");
    const list2 = document.getElementById("SkillList2");
    const list1Count = ListItemCounter(list1);
    const list2Count = ListItemCounter(list2);
    var targetList = null;

    if (isAdd) {
        if (list1Count > list2Count) {
            targetList = list2;
        }
        else if (list1Count < list2Count) {
            targetList = list1;
        }
        else {
            targetList = list1;
        }
        
        const listItem = document.createElement("li");
        listItem.textContent = value;
        targetList.querySelector("ul").appendChild(listItem);
    } else {
        const listItems = targetList.querySelectorAll("li");
        if (listItems.length > 0) {
            targetList.querySelector("ul").removeChild(listItems[listItems.length - 1]);
        }
    }
}

function ListItemCounter(list){
    return list.querySelectorAll("li").length;
}
