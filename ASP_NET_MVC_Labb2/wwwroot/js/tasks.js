var idToBeUpdated, idToBeDeleted;

function openUpdateModal(id) {
    idToBeUpdated = id;
    var foundTask = tasks.find(task => task.Id === id);
    $('#updateTaskModal').modal();
    $('#updateTaskModal #Name').val(foundTask.Name);
    $('#updateTaskModal #CourseName').val(foundTask.CourseName);
    $('#updateTaskModal #Id').val(id);
}


function openDeleteModal(id) {
    idToBeDeleted = id;
    var foundTask = tasks.find(task => task.Id === id);
    $('#deleteTaskModal').modal();
    $('#deleteTaskModal #name').text(foundTask.Name);
    $('#deleteTaskModal #coursename').text(foundTask.CourseName);
    $('#deleteTaskModal #Id').val(id);
}

function checkTheBox(id, currentState) {
    if (currentState === 'True') {
        document.querySelector('#finish-form-' + id + ' #IsCompleted').value = false;
    } else if (currentState === 'False') {
        document.querySelector('#finish-form-' + id + ' #IsCompleted').value = true;
    }
    document.querySelector('#finish-form-' + id).submit();
}