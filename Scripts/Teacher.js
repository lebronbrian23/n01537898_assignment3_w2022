
//generate base url
let base_url = window.location.protocol + "//" + window.location.hostname + ":" + window.location.port + "/";

//function to add a new teacher
function processForm() {
     //generate add teacher path
    let add_teacher_url = base_url + "api/Teacherdata/AddTeacher/";

    //form fields
    let fname = document.getElementById('teacherfname');
    let lname = document.getElementById('teacherlname');
    let salary = document.getElementById('salary');
    let hiredate = document.getElementById('hiredate');
    let employeenumber = document.getElementById('employeenumber');

    //form input validation
    if (fname.value === "" || fname.value === null) {
        fname.style.background = "red";
        fname.style.color = "white";
        fname.focus();
        return false;
    }

    if (lname.value === "" || lname.value === null) {
        lname.style.background = "red";
        lname.style.color = "white";
        lname.focus();
        return false;
    }
    if (employeenumber.value === "" || employeenumber.value === null) {
        employeenumber.style.background = "red";
        employeenumber.style.color = "white";
        employeenumber.focus();
        return false;
    }

    if (salary.value === "" || salary.value === null) {
        salary.style.background = "red";
        salary.style.color = "white";
        salary.focus();
        return false;
    }

    if (hiredate.value === "" || hiredate.value === null) {
        hiredate.style.background = "red";
        hiredate.style.color = "white";
        hiredate.focus();
        return false;
    }

    let xhttp = new XMLHttpRequest();

    let TeacherData = {
        'teacherfname': fname.value,
        "teacherlname": lname.value,
        "teacheremployeenumber": employeenumber.value,
        "teachersalary": salary.value,
        "teacherhiredate": hiredate.value
    }


    xhttp.open("POST", add_teacher_url, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.status === 204) {
            location.href = base_url + "Teacher/List";
        }
    };

    xhttp.send(JSON.stringify(TeacherData));

}


//function to process data from delete confirm page
function processDeleteForm() {

    //generate delete teacher path
    let delete_teacher_url = base_url + "api/delete/teacher/";

    let delete_teacher_id = document.getElementById('id');

    let id = delete_teacher_id.value;
    console.log(id)
    var xhttp = new XMLHttpRequest();

    xhttp.open("POST", delete_teacher_url + id, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.status === 204) {
            location.href = base_url + "Teacher/List";
        }
    };
    xhttp.send();


}

// function to update a teacher
function updateTeacherForm() {

    //generate add teacher path
    let add_teacher_url = base_url + "api/Teacherdata/UpdateTeacher";

    //form fields
    let teacherid = document.getElementById('teacherid');
    let fname = document.getElementById('teacherfname');
    let lname = document.getElementById('teacherlname');
    let salary = document.getElementById('salary');
    let hiredate = document.getElementById('hiredate');
    let employeenumber = document.getElementById('employeenumber');

    //form input validation
    if (fname.value === "" || fname.value === null) {
        fname.style.background = "red";
        fname.style.color = "white";
        fname.focus();
        return false;
    }

    if (lname.value === "" || lname.value === null) {
        lname.style.background = "red";
        lname.style.color = "white";
        lname.focus();
        return false;
    }
    if (employeenumber.value === "" || employeenumber.value === null) {
        employeenumber.style.background = "red";
        employeenumber.style.color = "white";
        employeenumber.focus();
        return false;
    }

    if (salary.value === "" || salary.value === null) {
        salary.style.background = "red";
        salary.style.color = "white";
        salary.focus();
        return false;
    }

    if (hiredate.value === "" || hiredate.value === null) {
        hiredate.style.background = "red";
        hiredate.style.color = "white";
        hiredate.focus();
        return false;
    }

    let xhttp = new XMLHttpRequest();

    let TeacherData = {
        'teacherfname': fname.value,
        "teacherlname": lname.value,
        "teacheremployeenumber": employeenumber.value,
        "teachersalary": salary.value,
        "teacherhiredate": hiredate.value
    }


    xhttp.open("POST", add_teacher_url + "?TeacherId=" + teacherid.value, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.status === 204) {
            location.href = base_url + "Teacher/Show/" + teacherid.value;
        }
    };

    xhttp.send(JSON.stringify(TeacherData));

}
