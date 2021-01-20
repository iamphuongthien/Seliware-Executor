const uri = 'api/Test';
let todos = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addLastNameTextbox = document.getElementById('add-lastname');
  const addFirstMidNameTextbox = document.getElementById('add-firstmidname');
  const addEnrollmentDateTextbox = document.getElementById('add-enrollmentdate');

  const Student = {
    LastName: addLastNameTextbox.value.trim(),
    FirstMidName: addFirstMidNameTextbox.value.trim(),
    EnrollmentDate: addEnrollmentDateTextbox.value.trim(),
  };

  var Students = new Array();
  Students.push(Student);
  Students.push(Student);

  fetch(`${uri}/Students`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(Students)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addLastNameTextbox.value = '';
      addFirstMidNameTextbox.value = '';
      addEnrollmentDateTextbox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    console.log(`${uri}/${id}`);
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);
    console.log(item.enrollmentDate);
  
  document.getElementById('edit-lastname').value = item.lastName;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-firstmidname').value = item.firstMidName;
  document.getElementById('edit-enrollmentdate').value = item.enrollmentDate.split('T')[0];
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    ID: parseInt(itemId, 10),
    LastName: document.getElementById('edit-lastname').value.trim(),
    FirstMidName: document.getElementById('edit-firstmidname').value.trim(),
    EnrollmentDate : document.getElementById('edit-enrollmentdate').value.trim()
  };

  fetch(`${uri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'to-do' : 'to-dos';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  stt = 1;

  data.forEach(item => {
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    console.log(item);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(stt++);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    textNode = document.createTextNode(item.lastName);
    td2.appendChild(textNode);

    let td3 = tr.insertCell(2);
    textNode = document.createTextNode(item.firstMidName);
    td3.appendChild(textNode);

    let td4 = tr.insertCell(3);
    textNode = document.createTextNode(item.enrollmentDate);
    td4.appendChild(textNode);

    let td5 = tr.insertCell(4);
    td5.appendChild(isCompleteCheckbox);

    let td6 = tr.insertCell(5);
    td6.appendChild(editButton);

    let td7 = tr.insertCell(6);
    td7.appendChild(deleteButton);
  });

  todos = data;
}